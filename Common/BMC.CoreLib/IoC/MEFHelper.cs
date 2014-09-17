// -----------------------------------------------------------------------
// <copyright file="MEFHelper.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

#if !MEF
#define MEF
#endif

#if !NET4
#define NET4
#endif

namespace BMC.CoreLib.IoC
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.IO;
    using System.Reflection;
    using BMC.CoreLib.Diagnostics;

#if NET4 && MEF
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;
    using System.ComponentModel.Composition.Primitives;
    using System.Linq.Expressions;
#endif

#if NET4
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class MEFHelper
    {
        private static ComposablePartCatalog _catalog = null;
        private static CompositionContainer _container = null;

        static MEFHelper()
        {
            AggregateCatalog catalog = new AggregateCatalog();

#if !SILVERLIGHT
            string directory = string.Empty;
            Assembly asmStart = Assembly.GetEntryAssembly();

            if (asmStart != null)
            {
                directory = Path.GetDirectoryName(asmStart.Location);
            }
            else
            {
                directory = AppDomain.CurrentDomain.RelativeSearchPath;
            }
            catalog.Catalogs.Add(new DirectoryCatalog(directory));
#else
#endif
            _container = new CompositionContainer((_catalog = catalog));
#if !SILVERLIGHT
            AddAssembly(asmStart);
#endif
        }

        public static void AddAssembly(Assembly assembly)
        {
            ModuleProc PROC = new ModuleProc("", "AddAssembly");
            if (assembly == null) return;

            try
            {
                AggregateCatalog catalog = ((AggregateCatalog)_catalog);
                AssemblyCatalog asmCatalog = (from c in catalog.Catalogs.OfType<AssemblyCatalog>()
                                              where c.Assembly.FullName.IgnoreCaseCompare(assembly.FullName)
                                              select c).FirstOrDefault();
                if (asmCatalog == null)
                {
                    catalog.Catalogs.Add(new AssemblyCatalog(assembly));
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        public static void AddDirectory(Assembly assembly)
        {
            AddDirectory(Path.GetDirectoryName(assembly.Location));
        }

        public static void AddDirectory(string directory)
        {
            AggregateCatalog catalog = ((AggregateCatalog)_catalog);
            DirectoryCatalog asmCatalog = (from c in catalog.Catalogs.OfType<DirectoryCatalog>()
                                           where c.FullPath.IgnoreCaseCompare(directory)
                                           select c).FirstOrDefault();
            if (asmCatalog == null)
            {
                catalog.Catalogs.Add(new DirectoryCatalog(directory));
            }
        }

        public static T GetExportedValue<T>()
        {
            return _container.GetExportedValue<T>();
        }

        public static T GetExportedValue<T>(string contractName)
        {
            return _container.GetExportedValue<T>(contractName);
        }

        public static IEnumerable<T> GetExportedValues<T>()
        {
            return _container.GetExportedValues<T>();
        }

        public static void ComposeParts(object instance)
        {
            ModuleProc PROC = new ModuleProc("", "ComposeParts");

            try
            {
                _container.ComposeParts(instance);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }
    }
#endif
}
