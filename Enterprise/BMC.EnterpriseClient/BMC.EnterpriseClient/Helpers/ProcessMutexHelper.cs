using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;
using System.IO;

namespace BMC.EnterpriseClient.Helpers
{
    internal static class ProcessMutexHelper
    {
        [DllImport("user32.dll")]
        private static extern int ShowWindow(IntPtr hWnd, int nCmdShow);
        [DllImport("user32.dll")]
        private static extern int SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll")]
        private static extern int IsIconic(IntPtr hWnd);

        private static string CombingKey(string mutexKey, int processId)
        {
            return mutexKey + "_" + processId.ToString();
        }

        internal static bool FindInstance(string mutexKey)
        {

            //#if DEBUG
            //            Debugger.Break();
            //#endif
            //// create the mutex
            //bool createNew = false;
            //Mutex mutant = new Mutex(true, mutexKey, out createNew);
            //if (createNew) return true;            
            string exeName = Path.GetFileNameWithoutExtension(typeof(ProcessMutexHelper).Assembly.Location);
            Process[] runningProcesses = (from p in Process.GetProcesses().Where(p => p.Id != Process.GetCurrentProcess().Id)
                                          where string.Compare(p.ProcessName, exeName, true) == 0
                                          select p).ToArray();
            bool InstanceRunning = false;
            long runningId = 50000;

            try
            {
                if (runningProcesses == null ||
                    runningProcesses.Length == 0) return true;

                foreach (Process p in runningProcesses)
                {
                    Mutex mutant = null;
                    try
                    {
                        //Mutex newinstanceMutex =
                        //  Mutex.OpenExisting(CombingKey(mutexKey, p.Id));
                        ////we check if a Mutex that respects our format is already created
                        //try
                        //{
                        //    newinstanceMutex.ReleaseMutex();
                        //}
                        //catch { }
                        //InstanceRunning = true;
                        ////if the upper Mutex.OpenExisting succeeds, a mutex is already created, so
                        ////an instance signaling the searched mutex is already running
                        //runningId = p.Id;
                        bool createNew = false;
                        mutant = new Mutex(true, CombingKey(mutexKey, p.Id), out createNew);
                        if (!createNew)
                        {
                            InstanceRunning = true;
                            runningId = p.Id;
                            break;
                        }
                    }
                    catch { }
                    finally
                    {
                        if (mutant != null)
                        {
                            mutant.ReleaseMutex();
                        }
                    }
                }

                if (!InstanceRunning)
                {
                    Mutex currentMutex = new Mutex(true, CombingKey(mutexKey, Process.GetCurrentProcess().Id));
                    //currentMutex.ReleaseMutex();
                    return true;
                }
                else
                {
                    ActivePresiousInstance((int)runningId);
                    Environment.Exit(0);
                }
            }
            catch { }

            return false;
        }

        internal static void ActivePresiousInstance(int processId)
        {
            //this code execution occurs if a running application instance was found
            IntPtr winHandle = Process.GetProcessById(processId).MainWindowHandle;

            //we now bring the process with PID = runningID to front
            if (winHandle != IntPtr.Zero)
            {
                const int SW_RESTORE = 9;
                if (IsIconic(winHandle) != 0) ShowWindow(winHandle, SW_RESTORE);
                SetForegroundWindow(winHandle);
            }
        }
    }
}
