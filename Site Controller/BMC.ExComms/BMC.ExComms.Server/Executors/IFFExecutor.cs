using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib;
using BMC.CoreLib.Concurrent;
using BMC.ExComms.Contracts.DTO.Freeform;

namespace BMC.ExComms.Server.Executors
{
    public interface IFFExecutor : IDisposable
    {
        bool ProcessMessage(FFMsg_G2H request);
        bool ProcessMessage(FFMsg_H2G request);

        bool AddInstallations(IEnumerable<int> installationNos);
        bool AddInstallation(int installationNo);
        bool RemoveInstallation(int installationNo);
    }

    public static class FFExecutorFactory
    {
        #region Single Thread Helper (Instance)

        private static SingletonThreadHelper<IFFExecutor> _executorHelper = null;

        public static IFFExecutor Instance
        {
            get { return _executorHelper.Current; }
        }

        #endregion

        internal static IFFExecutor RegisterAndGet(IExecutorService executorService)
        {
            using (ILogMethod method = Log.LogMethod("FFExecutorFactory", "Method"))
            {
                try
                {
                    _executorHelper = new SingletonThreadHelper<IFFExecutor>(
                                       new Lazy<IFFExecutor>(() => new FFExecutor_InMemory(executorService)));
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return Instance;
            }
        }
    }
}
