using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using BMC.CoreLib.Diagnostics;

namespace BMC.CoreLib.Win32
{
    internal static class AsyncProgressMessages
    {
        private static string[] _displayMessages = new string[] 
        {
            "L",
            "L o",
            "L o a",
            "L o a d",
            "L o a d i",
            "L o a d i n",
            "L o a d i n g",
            "L o a d i n g .",
            "L o a d i n g . .",
            "L o a d i n g . . .",
            "L o a d i n g . . . .",
            "L o a d i n g . . . . .",
            "L o a d i n g . . . . . .",
            "L o a d i n g . . . . . . .",
            "L o a d i n g . . . . . . . .",
            "L o a d i n g . . . . . . . . .",
        };

        internal static string GetMessage(int index)
        {
            ModuleProc PROC = new ModuleProc("", "GetMessage");
            string result = default(string);

            try
            {
                if (index > 0)
                {
                    int newIndex = (index % _displayMessages.Length);
                    if (newIndex >= 0 && newIndex < _displayMessages.Length)
                    {
                        result = _displayMessages[newIndex];
                    }
                }

                if(result.IsEmpty())
                {
                    result = _displayMessages[9];
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }
    }

    public interface IAsyncProgressTimer : IDisposable
    {
        void Start();
        void Stop();
        void StopAndStart();
        string Elapsed { get; }
    }

    public static class AsyncProgressTimerFactory
    {
        public static IAsyncProgressTimer Create(bool isdebug)
        {
            if (isdebug) return new AsyncProgressTimerDebug();
            else return new AsyncProgressTimerRelease();
        }

        public static IAsyncProgressTimer Create()
        {
            return Create(Extensions.GetAppSettingValueBool("ASYNCPROGRESSTIMERDEBUG", false));
        }
    }

    internal class AsyncProgressTimerRelease : DisposableObject, IAsyncProgressTimer
    {
        public void Start() { }

        public void Stop() { }

        public void StopAndStart() { }

        public string Elapsed
        {
            get { return string.Empty; }
        }
    }

    internal class AsyncProgressTimerDebug : DisposableObject, IAsyncProgressTimer
    {
        private Stopwatch _sw = null;

        public void Start()
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Start");

            try
            {
                if (_sw == null)
                {
                    _sw = new Stopwatch();
                    _sw.Start();
                }
                else
                {
                    _sw.Restart();
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        public void Stop()
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Stop");

            try
            {
                if (_sw != null)
                {
                    _sw.Stop();
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        public void StopAndStart()
        {
            this.Stop();
            this.Start();
        }

        public string Elapsed
        {
            get
            {
                string elapsed = string.Empty;

                if (_sw != null &&
                    _sw.IsRunning)
                {                    
                    elapsed = " [ " + _sw.Elapsed.ToString() + " ]";
                }

                return elapsed;
            }
        }
    }
}
