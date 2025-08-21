using Microsoft.Win32;

namespace PowerModeSwitch
{
    public static class AutoStart
    {
        private static string AppName = "Power Mode Switch";

        private static string ExePath = Environment.ProcessPath;

        public static void EnableAutoStart()
        {
            try
            {
                string value = Quote(ExePath);
                RegistryKey baseKey = Registry.CurrentUser;
                string subKey = @"Software\Microsoft\Windows\CurrentVersion\Run";

                using var key = baseKey.CreateSubKey(subKey, true);

                key.SetValue(AppName, value, RegistryValueKind.String);
            }
            catch { }
        }

        public static void DisableAutoStart()
        {
            RegistryKey baseKey = Registry.CurrentUser;
            string subKey = @"Software\Microsoft\Windows\CurrentVersion\Run";

            using var key = baseKey.OpenSubKey(subKey, writable: true);
            key?.DeleteValue(AppName, throwOnMissingValue: false);
        }

        private static string Quote(string path)
            => path.Contains(' ') && !path.StartsWith("\"") ? $"\"{path}\"" : path;

    }
}
