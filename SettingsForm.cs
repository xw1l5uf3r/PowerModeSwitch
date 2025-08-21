using System.Text.Json;
using System.Diagnostics;

namespace PowerModeSwitch
{
    public partial class SettingsForm : Form
    {
        private Form1 f;
        private Keys currentModifiers = Keys.None;
        private Keys currentKey = Keys.None;

        private bool suppressEvents = false;

        //initial download of everything need
        public SettingsForm(Form1 formm, bool visible = false)
        {
            InitializeComponent();

            f = formm;

            loadLanguages();
            loadConfig();

            this.KeyPreview = true;
            switchKeyTextBox.KeyDown += switchKeyTextBox_KeyDown;

            applyButton.Parent = tabControl1.Parent;

            applyButton.BringToFront();

        }
        private void saveConfig()
        {
            if(!switchKeyTextBox.Text.Contains("Press the key"))
                Config.config["switchKey"] = switchKeyTextBox.Text;
            Config.config["mode1"] = powerModes.IndexOf(mode1ComboBox.Text).ToString();
            Config.config["mode2"] = powerModes.IndexOf(mode2ComboBox.Text).ToString();
            Config.config["autorun"] = autorunCheckBox.Checked == true ? "true" : "false";
            Config.config["language"] = languageComboBox.Text;

            string dir = "C:\\Users\\" + Environment.UserName + "\\AppData\\Local\\Power Mode Switch";
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            Form1.UnregisterHotKey(f.Handle, Form1.HOTKEY_ID);
            Form1.TryParseHotkeyString(Config.config["switchKey"]);
            Form1.RegisterHotKey(f.Handle, Form1.HOTKEY_ID, Form1.modifiers, Form1.keys);

            File.WriteAllText(dir + "\\user_config.json", JsonSerializer.Serialize(Config.config, new JsonSerializerOptions { WriteIndented = true }));
        }
        //load user config in folder C:\users\user\appdata\local\Power Mode Switch\user_config.json
        private void loadConfig()
        {
            string dir = "C:\\Users\\" + Environment.UserName + "\\AppData\\Local\\Power Mode Switch";
            string path = dir + "\\user_config.json";

            try
            {
                Config.config = JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText(path));
            }
            catch
            {
                Config.config = Config.standartConfig;
            }

            if (!checkCfg())
                Config.config = Config.standartConfig;

            switchKeyTextBox.Text = Config.config["switchKey"];
            mode1ComboBox.SelectedIndex = Convert.ToInt16(Config.config["mode1"]);
            mode2ComboBox.SelectedIndex = Convert.ToInt16(Config.config["mode2"]);

            languageComboBox.Text = Config.config["language"];
            initLanguage();

            autorunCheckBox.Checked = Config.config["autorun"] == "true" ? true : false;
        }
        //load all locales in folder
        private void loadLanguages()
        {
            string localesDir = Environment.CurrentDirectory + "\\locales\\";
            string[] files;
            if (Directory.Exists(localesDir))
                files = Directory.GetFiles(localesDir, "*.json");

            else return;

            foreach (string file in files)
            {
                Dictionary<string, string> locale;
                try
                {
                    locale = JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText(file));
                }
                catch { return; }

                if (!checkLocale(locale)) continue;

                Language.locales.Add(file.Split('\\')[file.Split('\\').Length - 1].Replace(".json", ""), locale);

                languageComboBox.Items.Add(file.Split('\\')[file.Split('\\').Length - 1].Replace(".json", ""));
            }
        }

        private bool checkLocale(Dictionary<string, string> locale)
        {
            foreach (KeyValuePair<string, string> cfg in locale)
            {
                bool haveKey = false;
                foreach (KeyValuePair<string, string> localEng in Language.localEng)
                    if (localEng.Key == cfg.Key) haveKey = true;

                if (!haveKey) return false;
            }
            return true;
        }

        private bool checkCfg()
        {
            foreach (KeyValuePair<string, string> cfg in Config.config)
            {
                bool haveKey = false;
                foreach (KeyValuePair<string, string> standartCfg in Config.standartConfig)
                    if (standartCfg.Key == cfg.Key) haveKey = true;

                if (!haveKey) return false;
            }
            return true;
        }

        //to make it easier for the user to use their keyboard shortcuts
        private void switchKeyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;

            Keys keyCode = e.KeyCode;

            if (keyCode == Keys.ControlKey || keyCode == Keys.ShiftKey ||
               keyCode == Keys.Menu || keyCode == Keys.LWin || keyCode == Keys.RWin)
            {
                currentModifiers = e.Modifiers;
                currentKey = Keys.None;
                switchKeyTextBox.Text = FormatHotkey(currentModifiers, currentKey);
                return;
            }

            currentModifiers = e.Modifiers;
            currentKey = keyCode;

            switchKeyTextBox.Text = FormatHotkey(currentModifiers, currentKey);
        }
        private string FormatHotkey(Keys modifiers, Keys key)
        {
            string s = "";
            if ((modifiers & Keys.Control) == Keys.Control) s += "Ctrl + ";
            if ((modifiers & Keys.Alt) == Keys.Alt) s += "Alt + ";
            if ((modifiers & Keys.Shift) == Keys.Shift) s += "Shift + ";

            if (key == Keys.None)
                s += "<Press the key>";
            else
                s += ReadableKeyName(key);

            return s;
        }

        private string ReadableKeyName(Keys key)
        {
            switch (key)
            {
                case Keys.Oem1: return ";";
                case Keys.Oem2: return "/";
                case Keys.Oem3: return "`";
                case Keys.Oem4: return "[";
                case Keys.Oem5: return "\\";
                case Keys.Oem6: return "]";
                case Keys.Oem7: return "'";
                case Keys.Oemplus: return "+";
                case Keys.OemMinus: return "-";
                case Keys.OemPeriod: return ".";
                case Keys.Oemcomma: return ",";
                case Keys.Oem102: return "\\";
                default:
                    return key.ToString();
            }
        }

        //initializing the selected language
        private void initLanguage()
        {
            languageLabel.Text = Language.locales[languageComboBox.Text]["langLabel"];
            switchKeyLabel.Text = Language.locales[languageComboBox.Text]["switchKey"];
            mode1label.Text = Language.locales[languageComboBox.Text]["mode1"];
            mode2label.Text = Language.locales[languageComboBox.Text]["mode2"];

            powerModes[0] = Language.locales[languageComboBox.Text]["highPerformanceOverlayLabel"];
            powerModes[1] = Language.locales[languageComboBox.Text]["energySavingOverlayLabel"];
            powerModes[2] = Language.locales[languageComboBox.Text]["balancedOverlayLabel"];

            systemTabPage.Text = Language.locales[languageComboBox.Text]["sysPage"];
            appTabPage.Text = Language.locales[languageComboBox.Text]["appPage"];
            aboutTabPage.Text = Language.locales[languageComboBox.Text]["aboutPage"];

            autorunCheckBox.Text = Language.locales[languageComboBox.Text]["autorunCheckBox"];
            applyButton.Text = Language.locales[languageComboBox.Text]["applyButton"];

            if (f.trayMenu != null)
            {
                f.trayMenu.Items[0].Text = Language.locales[languageComboBox.Text]["settingsItem"];
                f.trayMenu.Items[1].Text = Language.locales[languageComboBox.Text]["exitItem"];
            }
            updateCombos();
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            if (Config.config["language"] != languageComboBox.Text)
                initLanguage();
            if (!autorunCheckBox.Checked && (Config.config["autorun"] == "true"))
            {
                AutoStart.DisableAutoStart();
            }
            else if (autorunCheckBox.Checked && (Config.config["autorun"] == "false"))
            {
                AutoStart.EnableAutoStart();
            }
            saveConfig();
        }

        private readonly List<string> powerModes = new List<string>
        {
            "High performance",
            "Energy saving",
            "Balanced"
        };


        private void mode1ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (suppressEvents) return;

            try
            {
                suppressEvents = true;
                updateCombo(source: mode1ComboBox, target: mode2ComboBox);
            }
            finally { suppressEvents = false; }
        }

        //just in case, for correct display
        private void updateCombos()
        {
            mode1ComboBox.Items.Clear();
            mode2ComboBox.Items.Clear();

            for (int i = 0; i < 3; i++)
            {
                mode1ComboBox.Items.Add(powerModes[i]);
                mode2ComboBox.Items.Add(powerModes[i]);
            }

            mode1ComboBox.Items.Remove(powerModes[Convert.ToInt16(Config.config["mode2"])]);
            mode2ComboBox.Items.Remove(powerModes[Convert.ToInt16(Config.config["mode1"])]);
            mode2ComboBox.SelectedItem = powerModes[Convert.ToInt16(Config.config["mode2"])];
            mode1ComboBox.SelectedItem = powerModes[Convert.ToInt16(Config.config["mode1"])];
        }
        //deletes one combobox item that is in the current other combobox
        private void updateCombo(ComboBox source, ComboBox target)
        {
            string sourceSelected = source.SelectedItem as string;

            string prevTargetSelection = target.SelectedItem as string;

            target.Items.Clear();

            foreach (var item in powerModes)
            {
                if (item == sourceSelected) continue;
                target.Items.Add(item);
            }

            if (!string.IsNullOrEmpty(prevTargetSelection) && target.Items.Contains(prevTargetSelection))
                target.SelectedItem = prevTargetSelection;
            else target.SelectedIndex = -1;
        }

        private void mode2ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (suppressEvents) return;

            try
            {
                suppressEvents = true;
                updateCombo(source: mode2ComboBox, target: mode1ComboBox);
            }
            finally { suppressEvents = false; }
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {   
            ProcessStartInfo gitHub = new ProcessStartInfo()
            {
                FileName = "https://github.com/xw1l5uf3r",
                UseShellExecute = true
            };
            Process.Start(gitHub);
        }
    }
}
