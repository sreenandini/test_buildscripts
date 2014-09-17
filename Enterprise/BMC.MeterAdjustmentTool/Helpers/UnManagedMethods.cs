using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace BMC.MeterAdjustmentTool.Helpers
{
    #region Private Structures
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct RECT
    {
        public int left;
        public int top;
        public int right;
        public int bottom;

        public override string ToString()
        {
            string ret = String.Format(
                "left = {0}, top = {1}, right = {2}, bottom = {3}",
                left, top, right, bottom);
            return ret;
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct PAINTSTRUCT
    {
        public IntPtr hdc;
        public int fErase;
        public RECT rcPaint;
        public int fRestore;
        public int fIncUpdate;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] rgbReserved;

        public override string ToString()
        {
            string ret = String.Format(
                "hdc = {0} , fErase = {1}, rcPaint = {2}, fRestore = {3}, fIncUpdate = {4}",
                hdc, fErase, rcPaint.ToString(), fRestore, fIncUpdate);
            return ret;
        }
    }

    #endregion

    #region UnManagedMethods
    public class UnManagedMethods
    {
        [DllImport("user32")]
        public extern static int GetClientRect(
            IntPtr hwnd,
            ref RECT lpRect);
        [DllImport("user32")]
        public extern static int BeginPaint(
            IntPtr hwnd,
            ref PAINTSTRUCT lpPaint);
        [DllImport("user32")]
        public extern static int EndPaint(
            IntPtr hwnd,
            ref PAINTSTRUCT lpPaint);
        [DllImport("user32", CharSet = CharSet.Auto)]
        public extern static uint SetClassLong(
            IntPtr hwnd,
            int nIndex,
            uint dwNewLong);
        [DllImport("user32")]
        public extern static int InvalidateRect(
            IntPtr hwnd,
            ref RECT lpRect,
            int bErase);

        public const int WM_PAINT = 0xF;
        public const int WM_ERASEBKGND = 0x14;
        public const int WM_SIZE = 0x5;

        public const int GCL_HBRBACKGROUND = (-10);

    }
    #endregion
}
