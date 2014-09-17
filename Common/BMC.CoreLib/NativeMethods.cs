using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace BMC.CoreLib
{
    public static class NativeMethods
    {
        private const string WS2_32 = "ws2_32.dll";

        public enum ShowCommands : int
        {
            SW_HIDE = 0,
            SW_SHOWNORMAL = 1,
            SW_NORMAL = 1,
            SW_SHOWMINIMIZED = 2,
            SW_SHOWMAXIMIZED = 3,
            SW_MAXIMIZE = 3,
            SW_SHOWNOACTIVATE = 4,
            SW_SHOW = 5,
            SW_MINIMIZE = 6,
            SW_SHOWMINNOACTIVE = 7,
            SW_SHOWNA = 8,
            SW_RESTORE = 9,
            SW_SHOWDEFAULT = 10,
            SW_FORCEMINIMIZE = 11,
            SW_MAX = 11
        }

        public const UInt32 WM_QUIT = 0x0012;

        [DllImport("shell32.dll")]
        public static extern IntPtr ShellExecute(
             IntPtr hwnd,
             string lpOperation,
             string lpFile,
             string lpParameters,
             string lpDirectory,
             ShowCommands nShowCmd);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        public static void OpenApplication(string file, string paramaters)
        {
            ShellExecute(IntPtr.Zero, "Open", file, paramaters, null, ShowCommands.SW_SHOWNORMAL);
        }

        public static void PostMessageSafe(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
        {
            bool returnValue = PostMessage(hWnd, msg, wParam, lParam);
            if (!returnValue)
            {
                // An error occured
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        public static class WinSock
        {
            private const int microcnv = 1000000; 

            public struct TimeValue
            {
                public int Seconds;
                public int Microseconds;
            };

            public static void MicrosecondsToTimeValue(long microSeconds, ref TimeValue timeVal)
            {
                timeVal.Seconds = (int)(microSeconds / microcnv);
                timeVal.Microseconds = (int)(microSeconds % microcnv);
            }

            [DllImport(WS2_32, SetLastError = true)]
            internal static extern int select(
                                           [In] int ignoredParameter,
                                           [In, Out] IntPtr[] readfds,
                                           [In, Out] IntPtr[] writefds,
                                           [In, Out] IntPtr[] exceptfds,
                                           [In] ref TimeValue timeout
                                           );

            [DllImport(WS2_32, SetLastError = true)]
            internal static extern int select(
                                           [In] int ignoredParameter,
                                           [In, Out] IntPtr[] readfds,
                                           [In, Out] IntPtr[] writefds,
                                           [In, Out] IntPtr[] exceptfds,
                                           [In] IntPtr nullTimeout
                                           );
        }
    }
}
