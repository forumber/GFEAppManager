using System;
using System.Runtime.InteropServices;

namespace GFEAppManager
{
    class ExternalOperations
    {
        public enum ShowWindowAsyncModes
        {
            SW_SHOWNORMAL = 1,
            SW_SHOWMINIMIZED,
            SW_SHOWMAXIMIZED
        }

        [DllImport("user32.dll")]
        public static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
    }
}
