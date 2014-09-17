using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using System.Reflection;
using System.Threading;
using System.Collections;

namespace BMC.CoreLib
{
#if _NET_4
    public static class WmiSearcher {
        public static void Search<T>(SynchronizationContext syncContext, string namespacePath, string query,
            Action<int> doStart, Action<T, int, CancellationTokenSource> doWork)
            where T : System.ComponentModel.Component {
            try {
                ManagementPath mgPath = new ManagementPath(namespacePath);
                ManagementScope scope = new ManagementScope(mgPath);
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, new SelectQuery(query));
                ManagementObjectCollection coll = searcher.Get();
                Search<T>(syncContext, coll, doStart, doWork);
            } finally {
            }
        }

        public static void Search<T>(SynchronizationContext syncContext, ICollection coll,
            Action<int> doStart, Action<T, int, CancellationTokenSource> doWork)
            where T : System.ComponentModel.Component {
            Type typeOfT = typeof(T);
            ConstructorInfo ci = typeOfT.GetConstructor(new Type[] { typeof(ManagementPath) });

            if (ci != null && doWork != null) {
                CancellationTokenSource ct = new CancellationTokenSource();
                CancellationToken ctk = ct.Token;

                try {
                    if (coll != null && coll.Count > 0) {
                        if (doStart != null) {
                            syncContext.Send((o) => {
                                doStart(((ICollection)o).Count);
                            }, coll);
                        }

                        int index = 1;
                        foreach (object obj in coll) {
                            IDisposable objDest = obj as T;
                            bool isNewInstance = false;

                            try {
                                if (objDest == null && obj is ManagementObject) {
                                    isNewInstance = true;
                                    objDest = ci.Invoke(new object[] { ((ManagementObject)obj).Path }) as T;
                                }

                                if (objDest != null) {
                                    syncContext.Send((o) => {
                                        doWork((T)o, index, ct);
                                    }, objDest);
                                }
                                if (ct.IsCancellationRequested) break;
                                ctk.WaitHandle.WaitOne(1);
                            } finally {
                                if (isNewInstance) {
                                    objDest.Dispose();
                                }
                            }

                            index++;
                        }
                    }
                } finally {
                    if (ct != null) {
                        ct.Dispose();
                    }
                }
            }
        }
    }
#endif
}
