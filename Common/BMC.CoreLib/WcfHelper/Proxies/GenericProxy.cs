using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Channels;
using BMC.CoreLib;
using BMC.CoreLib.Diagnostics;
using BMC.CoreLib.WcfHelper.Hosting;
using BMC.CoreLib.WcfHelper.Contracts;
using BMC.CoreLib.Concurrent;

namespace BMC.CoreLib.WcfHelper.Proxies.ServiceProxy
{
    public static class GenericProxy
    {
        private static IDictionary<string, object> _localInstances = null;

        static GenericProxy()
        {
            _localInstances = new SortedDictionary<string, object>(StringComparer.InvariantCultureIgnoreCase);
        }

        public static TClient GetService<TClient, TChannel>(string localPath, string remoteUri, Binding serviceBinding,
            Func<TChannel, TClient> createLocalChannel, Func<Binding, string, TClient> createRemoteChannel,
            string assemblyPath, string serviceTypeName)
            where TClient : WcfClientChannel<TChannel>
            where TChannel : IServiceContractBase
        {
            ModuleProc PROC = new ModuleProc("GenericProxy", "GetService<" + typeof(TChannel).Name + ">");
            TClient result = null;
            Type serviceType = null;
            string key = assemblyPath + ":" + serviceTypeName;

            try
            {
                // Preferences
                // 1. Always try to load the assembly from the current executing path
                string currentDirectory = AppDomain.CurrentDomain.RelativeSearchPath;
                if (!currentDirectory.IsEmpty())
                {
                    LoadAssemblyFromLocalPath<TClient, TChannel>(currentDirectory, assemblyPath, serviceTypeName, ref result, ref serviceType);
                }

                // 2. Local Path
                if (!localPath.IsEmpty() &&
                    serviceType == null)
                {
                    LoadAssemblyFromLocalPath<TClient, TChannel>(localPath, assemblyPath, serviceTypeName, ref result, ref serviceType);
                }

                // 3. load the type from the same domain (ASP.NET)
                if (serviceType == null)
                {
                    // find the assembly already loaded in appdomain
                    string asmName = Path.GetFileNameWithoutExtension(assemblyPath);
                    var asm = (from a in AppDomain.CurrentDomain.GetAssemblies()
                               where a.GetName().Name.IgnoreCaseCompare(asmName)
                               select a).FirstOrDefault();
                    if (asm != null)
                    {
                        serviceType = asm.GetType(serviceTypeName, false);
                    }
                    else
                    {
                        serviceType = Type.GetType(serviceTypeName, false);
                    }
                }

                // 4. load the type if found
                if (serviceType != null)
                {
                    result = createLocalChannel((TChannel)Activator.CreateInstance(serviceType));
                    if (result != null)
                    {
                        Log.Info(PROC, serviceTypeName + " instance was created successfully.");
                        _localInstances.Add(key, result);
                    }
                }

                // 5. Remote Uri
                if (result == null)
                {
                    result = createRemoteChannel(serviceBinding, remoteUri);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        private static void LoadAssemblyFromLocalPath<TClient, TChannel>(string directory, string assemblyPath, string serviceTypeName,
            ref TClient result, ref Type serviceType)
            where TClient : WcfClientChannel<TChannel>
            where TChannel : IServiceContractBase
        {
            string fullPath = Path.Combine(directory, assemblyPath);
            string key = string.Empty;

            try
            {
                key = fullPath + ":" + serviceTypeName;
                if (_localInstances.ContainsKey(key))
                {
                    result = _localInstances[key] as TClient;
                }
                else
                {
                    if (File.Exists(fullPath))
                    {
                        Assembly asm = Assembly.LoadFrom(fullPath);
                        if (asm != null)
                        {
                            serviceType = asm.GetType(serviceTypeName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
            }
        }
    }
}
