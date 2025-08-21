using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;

class Config
{
    public static readonly Dictionary<string, string> standartConfig = new Dictionary<string, string>()
    {
        {"switchKey", "Alt + F" },
        {"mode1", "0" },
        {"mode2", "1" },
        {"autorun", "false" },
        {"language", "English" },
        //{"", "" },
    };
    public static Dictionary<string, string> config = standartConfig;
}

class Language
{

    public static Dictionary<string, string> localEng = new Dictionary<string, string>() 
    {
        {"settingsItem", "Settings" },
        {"exitItem", "Exit" },
        {"pmsTray", "Power Mode Switch" },
        {"highPerformanceOverlayLabel", "High Performance" },
        {"energySavingOverlayLabel", "Energy saving" },
        {"balancedOverlayLabel", "Balanced" },
        {"langLabel", "Language" },
        {"applyButton", "Apply" },
        {"appPage", "Application" },
        {"sysPage", "System" },
        {"aboutPage", "About" },
        {"autorunCheckBox", "Autorun" },
        {"switchKey", "Switch Key" },
        {"mode1", "Mode 1" },
        {"mode2", "Mode 2" },
        {"powermode", "Power Mode" },
        //{"", "" },

    };

    //locales are stored here
    public static Dictionary<string, Dictionary<string, string>> locales = new Dictionary<string, Dictionary<string, string>>();
}