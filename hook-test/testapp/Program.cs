Console.WriteLine("Test app running!");
Console.WriteLine($"DOTNET_STARTUP_HOOKS = {Environment.GetEnvironmentVariable("DOTNET_STARTUP_HOOKS")}");
if (System.IO.File.Exists(@"C:\Users\bash\HOOK_LOADED.txt"))
{
    Console.WriteLine("HOOK_LOADED.txt EXISTS - hook worked!");
    Console.WriteLine(System.IO.File.ReadAllText(@"C:\Users\bash\HOOK_LOADED.txt"));
}
else
{
    Console.WriteLine("HOOK_LOADED.txt does NOT exist - hook did NOT run");
}
