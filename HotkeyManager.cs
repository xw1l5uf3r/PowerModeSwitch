using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

// to work with keys reading
namespace PowerModeSwitch
{
    public static class HotKeyManager
    {
        public const uint MOD_ALT = 0x0001;
        public const uint MOD_CONTROL = 0x0002;
        public const uint MOD_SHIFT = 0x0004;
        public const uint MOD_WIN = 0x0008;

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        public static bool Register(IntPtr handle, int id, uint modifiers, Keys key)
        {
            return RegisterHotKey(handle, id, modifiers, (uint)key);
        }

        public static bool Unregister(IntPtr handle, int id)
        {
            return UnregisterHotKey(handle, id);
        }

        public static uint GetModifiersFromKeys(Keys modifiers)
        {
            uint m = 0;
            if ((modifiers & Keys.Control) == Keys.Control) m |= MOD_CONTROL;
            if ((modifiers & Keys.Shift) == Keys.Shift) m |= MOD_SHIFT;
            if ((modifiers & Keys.Alt) == Keys.Alt) m |= MOD_ALT;
            return m;
        }
    }

}
