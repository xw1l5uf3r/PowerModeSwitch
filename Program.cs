using System.IO;

namespace PowerModeSwitch
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //check if there is a folder with locales
            if (!Directory.Exists("locales"))
            {
                Directory.CreateDirectory("locales");
                File.WriteAllBytes("locales\\English.json", locales.English);
                File.WriteAllBytes("locales\\French.json", locales.French);
                File.WriteAllBytes("locales\\Russian.json", locales.Russian);
                File.WriteAllBytes("locales\\Spanish.json", locales.Spanish);
            }
            else
                if (!File.Exists("\\locales\\English.json")) //at least one language is needed
                File.WriteAllBytes("English.json", locales.English);

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}