internal class StartupHook
{
    public static void Initialize()
    {
        try
        {
            System.IO.File.WriteAllText(@"C:\Users\bash\HOOK_LOADED.txt",
                $"Hook loaded at {System.DateTime.Now}\nPID: {System.Environment.ProcessId}");
        }
        catch (System.Exception ex)
        {
            System.IO.File.WriteAllText(@"C:\Users\bash\HOOK_ERROR.txt", ex.ToString());
        }
    }
}
