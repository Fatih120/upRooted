// verify-obfuscation — Post-build assertion for obfuscation-protected names.
// Reads the obfuscated DLL metadata (without loading the assembly) and verifies
// that entry points and reflection targets survived renaming.
// Returns exit code 1 on failure (fails the build).
//
// Usage: dotnet run --project tools/verify-obfuscation/ -- <path-to-UprootedHook.dll>

using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;

if (args.Length < 1)
{
    Console.Error.WriteLine("Usage: verify-obfuscation <path-to-UprootedHook.dll>");
    return 1;
}

var dllPath = Path.GetFullPath(args[0]);
if (!File.Exists(dllPath))
{
    Console.Error.WriteLine($"ERROR: DLL not found at {dllPath}");
    return 1;
}

// Read metadata without loading the assembly into the runtime
using var fs = File.OpenRead(dllPath);
using var peReader = new PEReader(fs);
var mdReader = peReader.GetMetadataReader();

// Collect all type full names
var typeNames = new HashSet<string>();
foreach (var typeHandle in mdReader.TypeDefinitions)
{
    var typeDef = mdReader.GetTypeDefinition(typeHandle);
    var ns = mdReader.GetString(typeDef.Namespace);
    var name = mdReader.GetString(typeDef.Name);
    var fullName = string.IsNullOrEmpty(ns) ? name : $"{ns}.{name}";
    typeNames.Add(fullName);
}

// Collect all method names keyed by their declaring type, and a flat set of all method names
var methods = new Dictionary<string, HashSet<string>>();
var allMethodNames = new HashSet<string>();
foreach (var typeHandle in mdReader.TypeDefinitions)
{
    var typeDef = mdReader.GetTypeDefinition(typeHandle);
    var ns = mdReader.GetString(typeDef.Namespace);
    var name = mdReader.GetString(typeDef.Name);
    var fullName = string.IsNullOrEmpty(ns) ? name : $"{ns}.{name}";

    foreach (var methodHandle in typeDef.GetMethods())
    {
        var methodDef = mdReader.GetMethodDefinition(methodHandle);
        var methodName = mdReader.GetString(methodDef.Name);
        if (!methods.ContainsKey(fullName))
            methods[fullName] = new HashSet<string>();
        methods[fullName].Add(methodName);
        allMethodNames.Add(methodName);
    }
}

bool failed = false;

void AssertTypeExists(string typeName)
{
    if (typeNames.Contains(typeName))
        Console.WriteLine($"  OK: {typeName}");
    else
    {
        Console.Error.WriteLine($"  FAIL: Type '{typeName}' not found");
        failed = true;
    }
}

void AssertMethodExists(string typeName, string methodName)
{
    if (methods.TryGetValue(typeName, out var ms) && ms.Contains(methodName))
        Console.WriteLine($"  OK: {typeName}.{methodName}");
    else
    {
        Console.Error.WriteLine($"  FAIL: Method '{typeName}.{methodName}' not found");
        failed = true;
    }
}

// For methods where the containing type is allowed to be renamed but the method
// name must survive (looked up via typeof().GetMethod("name") at runtime).
void AssertMethodNameSurvived(string methodName)
{
    if (allMethodNames.Contains(methodName))
        Console.WriteLine($"  OK: *.{methodName} (method name survived, type renamed)");
    else
    {
        Console.Error.WriteLine($"  FAIL: Method name '{methodName}' not found in any type");
        failed = true;
    }
}

Console.WriteLine($"Verifying obfuscation-protected names in {Path.GetFileName(dllPath)}");
Console.WriteLine();

// Entry points (must survive all obfuscation — type name AND members)
Console.WriteLine("Entry points:");
AssertTypeExists("UprootedHook.Entry");
AssertTypeExists("Uprooted.NativeEntry");
AssertTypeExists("StartupHook");
Console.WriteLine();

// Reflection targets where both type name and method name must survive
Console.WriteLine("Reflection targets (type + method):");
AssertMethodExists("Uprooted.ReconLogger", "Enable");
AssertMethodExists("Uprooted.ReconLogger", "Disable");
Console.WriteLine();

// Reflection targets where only the method name must survive
// (type is resolved via typeof() token, not by string name)
Console.WriteLine("Reflection targets (method name only):");
AssertMethodNameSurvived("SubscribeToObservable");
Console.WriteLine();

if (failed)
{
    Console.Error.WriteLine("OBFUSCATION VERIFICATION FAILED -- protected names were renamed!");
    return 1;
}

Console.WriteLine("All protected names verified.");
return 0;
