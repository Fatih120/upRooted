using System.Xml;
using Confuser.Core;
using Confuser.Core.Project;

// Usage: dotnet run --project tools/confuse/ -- <crproj> <baseDir> <outputDir>
if (args.Length < 3)
{
    Console.Error.WriteLine("Usage: confuse <crproj-path> <base-dir> <output-dir>");
    return 1;
}

var crprojPath = Path.GetFullPath(args[0]);
var baseDir = Path.GetFullPath(args[1]);
var outputDir = Path.GetFullPath(args[2]);

if (!File.Exists(crprojPath))
{
    Console.Error.WriteLine($"ERROR: .crproj not found: {crprojPath}");
    return 1;
}

// Load project from .crproj
var project = new ConfuserProject();
var xmlDoc = new XmlDocument();
xmlDoc.Load(crprojPath);
project.Load(xmlDoc);

// Override paths — the .crproj uses placeholders that only the CLI resolves
project.BaseDirectory = baseDir;
project.OutputDirectory = outputDir;

// Clear template modules (our .crproj has {InputPath} placeholder) and add the real assembly
project.Clear();
foreach (var dll in Directory.GetFiles(baseDir, "*.dll"))
{
    var name = Path.GetFileName(dll);
    if (name.Equals("UprootedHook.dll", StringComparison.OrdinalIgnoreCase))
    {
        project.Add(new ProjectModule { Path = name });
        break;
    }
}

if (project.Count == 0)
{
    Console.Error.WriteLine($"ERROR: UprootedHook.dll not found in {baseDir}");
    return 1;
}

// Add probe path for .NET runtime references
var runtimeDir = System.Runtime.InteropServices.RuntimeEnvironment.GetRuntimeDirectory();
if (!string.IsNullOrEmpty(runtimeDir))
    project.ProbePaths.Add(runtimeDir);

var logger = new ConsoleLogger();
var parameters = new ConfuserParameters
{
    Project = project,
    Logger = logger
};

Console.WriteLine($"ConfuserEx {ConfuserEngine.Version}");
Console.WriteLine($"  Base:   {baseDir}");
Console.WriteLine($"  Output: {outputDir}");
Console.WriteLine($"  Module: {project[0].Path}");
Console.WriteLine();

await ConfuserEngine.Run(parameters, null);

return logger.HasError ? 1 : 0;

class ConsoleLogger : ILogger
{
    public bool HasError { get; private set; }

    public void Debug(string msg) { }
    public void DebugFormat(string format, params object[] args) { }
    public void Info(string msg) => Console.WriteLine(msg);
    public void InfoFormat(string format, params object[] args) => Console.WriteLine(format, args);
    public void Warn(string msg) => Console.WriteLine($"WARN: {msg}");
    public void WarnFormat(string format, params object[] args) => Console.WriteLine($"WARN: {string.Format(format, args)}");
    public void WarnException(string msg, Exception ex) => Console.WriteLine($"WARN: {msg}\n{ex}");
    public void Error(string msg) { Console.Error.WriteLine($"ERROR: {msg}"); HasError = true; }
    public void ErrorFormat(string format, params object[] args) { Console.Error.WriteLine($"ERROR: {string.Format(format, args)}"); HasError = true; }
    public void ErrorException(string msg, Exception ex) { Console.Error.WriteLine($"ERROR: {msg}\n{ex}"); HasError = true; }
    public void Progress(int progress, int overall) { }
    public void EndProgress() { }
    public void Finish(bool successful)
    {
        if (successful)
            Console.WriteLine("Obfuscation completed successfully.");
        else
        {
            Console.Error.WriteLine("Obfuscation FAILED.");
            HasError = true;
        }
    }
}
