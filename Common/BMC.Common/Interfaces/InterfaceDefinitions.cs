using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Xml;
using System.Text;
using System.Reflection;
using System.Runtime.Remoting;

namespace BMC.Common.Interfaces
{
    #region Exception Management Interfaces

    public interface IExceptionPublisher
    {
        void Publish(Exception exception, NameValueCollection additionalInfo, NameValueCollection configSettings);
        void Publish(string fileName, Exception exception, NameValueCollection additionalInfo, NameValueCollection configSettings);
    }
    #endregion

    #region Configuration Management Interfaces

    public interface IConfigurationAdapter
    {
        #region Methods

        /// <summary>
        /// Reads from Configuration file
        /// </summary>
        /// <param name="strKey">Key</param>
        /// <returns>Value</returns>
        string Read(string strKey);


        /// <summary>
        /// Write into the Configuration
        /// </summary>
        /// <param name="strKey">Key</param>
        /// <param name="strValue">Value</param>
        /// <returns>Success code</returns>
        bool Write(string strKey, string strValue);

        /// <summary>
        /// Modifies the Configuration settings
        /// </summary>
        /// <param name="strKey">Key</param>
        /// <param name="strValue">Value</param>
        /// <returns>Success code</returns>
        bool Modify(string strKey, string strValue);

        /// <summary>
        /// Reads Section details from Configuration file
        /// </summary>
        /// <param name="strKey">Key to get the value from Config</param>
        /// <returns>Returns the Section as XML </returns>
        string ReadSection(string strKey);

        /// <summary>
        /// Reads Connection String details from Configuration 
        /// </summary>
        /// <param name="strKey">Key to get the value from Config</param>
        /// <returns>Returns the Connection String as string </returns>
        string ReadConnectionString(string strKey);

        /// <summary>
        /// Writes Connection String details to Configuration 
        /// </summary>
        /// <param name="strKey">Key to get the value from Config</param>
        /// <returns>Writes the Connection String to configuration </returns>
        bool WriteConnectionString(string strKey, string strValue);

        /// <summary>
        /// Free any occupide resources
        /// </summary>
        /// <returns>Success Code</returns>
        bool UnInit();
        #endregion

    }

    #endregion

    #region Log Management Interfaces

    public interface ILoggingAdapter
    {
        # region Member Functions

        /// <summary>
        /// This method will write the log in a text file that is configured in the .config file.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="level"></param>
        void WriteLog(string message, int level);


        /// <summary>
        /// This method will write the log in the given text file.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="message"></param>
        /// <param name="level"></param>
        void WriteLog(string fileName, string message, int level);

        # endregion Member Functions
    }
    #endregion

    #region Display Form Interface
    public interface IAppInvokeEntryPoint
    {
        void DisplayEntryForm(string[] args, Action<System.Windows.Forms.Form> doWork);
    }

    public class AppInvokeEntryPointResult : MarshalByRefObject
    {
        public AppInvokeEntryPointResult()
        {
            this.LoadedAssemblies = new SortedDictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
        }

        public IDictionary<string, string> LoadedAssemblies { get; set; }
        public ObjectHandle Instance { get; set; }

        public string AssemblyName { get; set; }
        public string TypeName { get; set; }
    }

    public class AppInvokeEntryPointFactory : MarshalByRefObject
    {
        public AppInvokeEntryPointFactory() { }

        public AppInvokeEntryPointResult FindForm(AppDomain ad, string assemblyFile, string configFile)
        {
            //IAppInvokeEntryPoint result = null;
            AppInvokeEntryPointResult result = new AppInvokeEntryPointResult();

            try
            {
                // Load the primary assembly here
                Assembly asmEntryPoint = Assembly.LoadFrom(assemblyFile);

                // other assemblies
                Assembly[] asms = AppDomain.CurrentDomain.GetAssemblies();

                // entry point
                if (asmEntryPoint.EntryPoint == null)
                {
                    // find the entry point assembly
                    foreach (Assembly asm in asms)
                    {
                        if (asm.EntryPoint != null)
                        {
                            asmEntryPoint = asm;
                            break;
                        }
                    }
                }

                // Load all the assemblies                
                IDictionary<string, string> loadedAssemblies = result.LoadedAssemblies;
                foreach (Assembly asm in asms)
                {
                    string fullName = asm.FullName;
                    if (!loadedAssemblies.ContainsKey(fullName))
                    {
                        loadedAssemblies.Add(fullName, asm.Location);
                    }

                    foreach (var asmRef in asm.GetReferencedAssemblies())
                    {
                        fullName = asmRef.FullName;

                        if (!loadedAssemblies.ContainsKey(fullName))
                        {
                            Assembly asm2 = ad.Load(asmRef);
                            loadedAssemblies.Add(fullName, asm2.Location);
                        }
                    }
                }

                // find the entry point class implements our interface
                Type typeEntryPoint = asmEntryPoint.EntryPoint.DeclaringType;
                if (typeof(IAppInvokeEntryPoint).IsAssignableFrom(typeEntryPoint))
                {
                    AssemblyName asm = AssemblyName.GetAssemblyName(assemblyFile);
                    //result = ad.CreateInstanceAndUnwrap(asm.FullName, typeEntryPoint.FullName) as IAppInvokeEntryPoint;
                    result.AssemblyName = asm.FullName;
                    result.TypeName = typeEntryPoint.FullName;
                    result.Instance = ad.CreateInstance(asm.FullName, typeEntryPoint.FullName);
                }
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
            }

            return result;
        }
    }
    #endregion
}