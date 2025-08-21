using System.Diagnostics;
using System.Runtime.InteropServices;

namespace PowerModeSwitch
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, Keys vk);

        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        [DllImport("gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse);

        private const uint MOD_CONTROL = 0x0002;
        private const uint MOD_SHIFT = 0x0004;
        public const int HOTKEY_ID = 1;

        public static uint modifiers = 0;
        public static Keys keys = Keys.None;

        private bool mode1 = false;

        private readonly Dictionary<string, (string Guid, string langKey)> powerProfiles = new Dictionary<string, (string Guid, string langKey)>()
        {
            {"0",("8c5e7fda-e8bf-4a96-9a85-a6e23a8c635c", "highPerformanceOverlayLabel" )},
            {"1",("a1841308-3541-4fab-bc81-f71556f20b4a", "energySavingOverlayLabel") },
            {"2",("381b4222-f694-41f0-9685-ff5bb260df2e", "balancedOverlayLabel") }
        };

        private Form overlayForm;
        private Label overlayLabel;

        SettingsForm settings;

        private NotifyIcon trayIcon;
        public ContextMenuStrip trayMenu;

        public Form1()
        {
            InitializeComponent();

            settings = new SettingsForm(this);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Visible = false;
            this.ShowInTaskbar = false;

            TryParseHotkeyString(Config.config["switchKey"]);
            RegisterHotKey(this.Handle, HOTKEY_ID, modifiers, keys);

            CreateOverlay();

            trayMenu = new ContextMenuStrip();
            trayMenu.Items.Add(Language.locales[settings.languageComboBox.Text]["settingsItem"], null, onSettingsClick);
            trayMenu.Items.Add(Language.locales[settings.languageComboBox.Text]["exitItem"], null, onExitClick);

            trayIcon = new NotifyIcon();
            trayIcon.Text = "Power Mode Switch";

            string currentGuid = getCurrentGuid();
            foreach (var guid in powerProfiles.Values)
            {
                if (currentGuid.Contains(guid.Guid))
                {
                    if (guid.langKey.Contains("Performance"))
                        trayIcon.Icon = Icons.red;
                    else if(guid.langKey.Contains("Saving"))
                        trayIcon.Icon = Icons.green;
                    else trayIcon.Icon = Icons.orange;
                }
            }

            trayIcon.ContextMenuStrip = trayMenu;
            trayIcon.Visible = true;
        }

        private string getCurrentGuid()
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "powercfg",
                    Arguments = "/getactivescheme",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            string guid = null;

            var start = output.IndexOf(':');
            if (start != -1)
            {
                guid = output.Substring(start + 1).Trim();
            }
            return guid;
        }

        private void onSettingsClick(object sender, EventArgs e)
        {
            settings.ShowDialog();
        }

        private void onExitClick(object sender, EventArgs e)
        {
            trayIcon.Visible=false;
            Application.Exit();
        }

        private void CreateOverlay()
        {
            overlayForm = new Form();
            overlayForm.FormBorderStyle = FormBorderStyle.None;
            overlayForm.StartPosition = FormStartPosition.CenterScreen;
            overlayForm.Size = new System.Drawing.Size(400, 200);
            overlayForm.BackColor = Color.Black;
            overlayForm.Opacity = 0.7;
            overlayForm.ShowInTaskbar = false;
            overlayForm.TopMost = true;
            overlayForm.FormBorderStyle= FormBorderStyle.None;
            int radius = 30;
            overlayForm.Region = Region.FromHrgn(CreateRoundRectRgn(0,0,overlayForm.Width, overlayForm.Height, radius, radius));

            overlayLabel = new Label();
            overlayLabel.Dock = DockStyle.Fill;
            overlayLabel.Font = new Font("Arial", 26, FontStyle.Bold);
            overlayLabel.ForeColor = Color.White;
            overlayLabel.TextAlign = ContentAlignment.MiddleCenter;

            overlayForm.Controls.Add(overlayLabel);
        }

        private void SwitchPowerMode()
        {
            string guid;
            if (mode1)
            {
                guid = powerProfiles[Config.config["mode1"]].Guid;
                RunCmd("powercfg /setactive " + guid);
                overlayLabel.Text = Language.locales[Config.config["language"]]["powermode"] + ": " +
                    Language.locales[Config.config["language"]][powerProfiles[Config.config["mode1"]].langKey];
                mode1 = false;
            }
            else
            {
                guid = powerProfiles[Config.config["mode2"]].Guid;
                RunCmd("powercfg /setactive " + guid);
                overlayLabel.Text = Language.locales[Config.config["language"]]["powermode"] + ": " +
                    Language.locales[Config.config["language"]][powerProfiles[Config.config["mode2"]].langKey];
                mode1 = true;
            }

            foreach(KeyValuePair<string, (string Guid, string langKey)> pair in powerProfiles)
            {
                if (guid == pair.Value.Guid) {
                    if (pair.Value.langKey.Contains("high"))
                        trayIcon.Icon = Icons.red;
                    else if (pair.Value.langKey.Contains("balance"))
                        trayIcon.Icon = Icons.orange;
                    else trayIcon.Icon = Icons.green;
                }
            }

            ShowOverlay();
        }
        private void RunCmd(string cmd)
        {
            ProcessStartInfo psi = new ProcessStartInfo("cmd.exe", "/c " + cmd)
            {
                CreateNoWindow = true,
                UseShellExecute = false
            };
            Process.Start(psi);
        }
        private async void ShowOverlay()
        {
            overlayForm.Show();
            await System.Threading.Tasks.Task.Delay(1500);
            overlayForm.Hide();
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_HOTKEY = 0x0312;
            if (m.Msg == WM_HOTKEY && m.WParam.ToInt32() == HOTKEY_ID)
            {
                SwitchPowerMode();
            }
            base.WndProc(ref m);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            UnregisterHotKey(this.Handle, HOTKEY_ID);
            base.OnFormClosing(e);
        }

        private static readonly Dictionary<string, Keys> specialMap = new Dictionary<string, Keys>(StringComparer.OrdinalIgnoreCase)
    {
        { "\\", Keys.Oem5 },
        { "BACKSLASH", Keys.Oem5 },
        { "/", Keys.Oem2 },
        { "SLASH", Keys.Oem2 },
        { ";", Keys.Oem1 },
        { "SEMICOLON", Keys.Oem1 },
        { "'", Keys.Oem7 },
        { "APOSTROPHE", Keys.Oem7 },
        { "[", Keys.Oem4 },
        { "LEFTBRACKET", Keys.Oem4 },
        { "]", Keys.Oem6 },
        { "RIGHTBRACKET", Keys.Oem6 },
        { ",", Keys.Oemcomma },
        { "COMMA", Keys.Oemcomma },
        { ".", Keys.OemPeriod },
        { "PERIOD", Keys.OemPeriod },
        { "-", Keys.OemMinus },
        { "_", Keys.OemMinus },
        { "=", Keys.Oemplus },
        { "+", Keys.Oemplus },
        { "`", Keys.Oem3 },
        { "TILDE", Keys.Oem3 },
        { "\\\\", Keys.Oem5 }, 
        { "OEM102", Keys.Oem102 },
        { "PLUS", Keys.Oemplus } 
    };

        public static bool TryParseHotkeyString(string input)
        {
            modifiers = 0;
            keys = Keys.None;

            if (string.IsNullOrWhiteSpace(input))
            {
                return false;
            }

            var parts = input.Split(new[] { '+', ',' }, StringSplitOptions.RemoveEmptyEntries);
            Keys parsedModifiers = Keys.None;
            Keys parsedKey = Keys.None;

            foreach (var raw in parts)
            {
                var token = raw.Trim();
                if (string.IsNullOrEmpty(token)) continue;

                if (token.Equals("ctrl", StringComparison.OrdinalIgnoreCase) || token.Equals("control", StringComparison.OrdinalIgnoreCase))
                {
                    parsedModifiers |= Keys.Control;
                    continue;
                }
                if (token.Equals("shift", StringComparison.OrdinalIgnoreCase))
                {
                    parsedModifiers |= Keys.Shift;
                    continue;
                }
                if (token.Equals("alt", StringComparison.OrdinalIgnoreCase))
                {
                    parsedModifiers |= Keys.Alt;
                    continue;
                }
                if (token.Equals("win", StringComparison.OrdinalIgnoreCase) || token.Equals("windows", StringComparison.OrdinalIgnoreCase))
                {
                    parsedModifiers |= Keys.LWin;
                    continue;
                }

                if (token.Length == 1 && char.IsDigit(token[0]))
                {
                    char d = token[0];
                    if (d == '0') parsedKey = Keys.D0;
                    else parsedKey = (Keys)Enum.Parse(typeof(Keys), "D" + d);
                    continue;
                }

                if (token.Length == 1 && char.IsLetter(token[0]))
                {
                    parsedKey = (Keys)Enum.Parse(typeof(Keys), token.ToUpper());
                    continue;
                }

                if (specialMap.TryGetValue(token, out Keys special))
                {
                    parsedKey = special;
                    continue;
                }

                if (Enum.TryParse(typeof(Keys), token, true, out object enumVal))
                {
                    parsedKey = (Keys)enumVal;
                    continue;
                }

                switch (token.ToLowerInvariant())
                {
                    case "del":
                        parsedKey = Keys.Delete; break;
                    case "ins":
                    case "insert":
                        parsedKey = Keys.Insert; break;
                    case "bksp":
                    case "backspace":
                        parsedKey = Keys.Back; break;
                    case "esc":
                    case "escape":
                        parsedKey = Keys.Escape; break;
                    case "pgup":
                    case "pageup":
                        parsedKey = Keys.PageUp; break;
                    case "pgdn":
                    case "pagedown":
                        parsedKey = Keys.PageDown; break;
                    case "space":
                    case "sp":
                        parsedKey = Keys.Space; break;
                    case "return":
                        parsedKey = Keys.Enter; break;
                    default:
                        return false;
                }
            }

            if (parsedKey == Keys.None)
            {
                return false;
            }

            uint regMods = 0;
            if ((parsedModifiers & Keys.Control) == Keys.Control) regMods |= HotKeyManager.MOD_CONTROL;
            if ((parsedModifiers & Keys.Shift) == Keys.Shift) regMods |= HotKeyManager.MOD_SHIFT;
            if ((parsedModifiers & Keys.Alt) == Keys.Alt) regMods |= HotKeyManager.MOD_ALT;
            if ((parsedModifiers & Keys.LWin) == Keys.LWin)
            {
                regMods |= HotKeyManager.MOD_WIN;
            }

            modifiers = regMods;
            keys = parsedKey;
            return true;
        }

    }
}

