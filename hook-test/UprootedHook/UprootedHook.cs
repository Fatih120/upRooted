using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Uprooted
{
    public class Hook
    {
        // Default component_entry_point_fn signature
        public static int Initialize(IntPtr arg, int argLength)
        {
            try
            {
                var info = $"Managed code loaded at {DateTime.Now}\n" +
                    $"PID: {Environment.ProcessId}\n" +
                    $"Runtime: {RuntimeInformation.FrameworkDescription}\n" +
                    $"Process: {Environment.ProcessPath}\n" +
                    $"AppDomain: {AppDomain.CurrentDomain.FriendlyName}\n" +
                    $"BaseDir: {AppDomain.CurrentDomain.BaseDirectory}\n";

                // List all loaded assemblies
                info += "\nLoaded assemblies:\n";
                foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
                {
                    info += $"  {asm.FullName}\n";
                    if (asm.FullName?.Contains("DotNetBrowser") == true ||
                        asm.FullName?.Contains("Root") == true ||
                        asm.FullName?.Contains("Avalonia") == true)
                    {
                        info += $"    ** INTERESTING: {asm.Location}\n";
                    }
                }

                File.WriteAllText(@"C:\Users\bash\MANAGED_HOOK_LOADED.txt", info);
                return 0;
            }
            catch (Exception ex)
            {
                File.WriteAllText(@"C:\Users\bash\MANAGED_HOOK_ERROR.txt", ex.ToString());
                return -1;
            }
        }
    }
}
