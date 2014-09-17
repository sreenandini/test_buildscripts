using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using BMC.CoreLib.Diagnostics;

namespace BMC.CoreLib.WMI.Win32
{
    internal class MsiNativeHelper
    {
        // MsiOpenDatabase persist predefine values, otherwise output database path is used
        public static class Persist
        {
            public const string MSIDBOPEN_READONLY = "0";//  // database open read-only, no persistent changes
            public const string MSIDBOPEN_TRANSACT = "1";//  // database read/write in transaction mode
            public const string MSIDBOPEN_DIRECT = "2";//  // database direct read/write without transaction
            public const string MSIDBOPEN_CREATE = "3";//  // create new database, transact mode read/write
            public const string MSIDBOPEN_CREATEDIRECT = "4";//  // create new database, direct mode read/write
            public const string MSIDBOPEN_PATCHFILE = "32";// // add flag to indicate patch file
        }

        public const int ERROR_NO_MORE_ITEMS = 259;

        public const string INSTALLPROPERTY_LOCALPACKAGE = "LocalPackage";
        public const string INSTALLPROPERTY_INSTALLEDPRODUCTNAME =
        "InstalledProductName";
        public const string INSTALLPROPERTY_VERSIONSTRING = "VersionString";
        public const string INSTALLPROPERTY_INSTALLDATE = "InstallDate";
        // This MsiOpenDatabase signature works only with a filename in "persist"
        [DllImport("msi")]
        public static extern int MsiOpenDatabase(string dbpath, string persist,
           ref IntPtr msihandle);
        [DllImport("msi")]
        public static extern int MsiDatabaseOpenView(IntPtr handle, string query,
           ref IntPtr viewhandle);
        [DllImport("msi")]
        public static extern int MsiViewExecute(IntPtr viewhandle, IntPtr recordhandle);
        [DllImport("msi")]
        public static extern int MsiViewFetch(IntPtr viewhandle,
           ref IntPtr recordhandle);

        [DllImport("msi.dll", ExactSpelling = true)]
        public static extern int MsiRecordGetFieldCount(IntPtr hRecord);

        [DllImport("msi", CharSet = CharSet.Auto)]
        public static extern int MsiRecordGetString(IntPtr recordhandle, int recno,
           StringBuilder szbuff, ref int len);
        [DllImport("msi")]
        public static extern int MsiCloseHandle(IntPtr handle);
        [DllImport("msi")]
        public static extern int MsiViewClose(IntPtr viewhandle);
        [DllImport("msi", CharSet = CharSet.Auto)]
        public static extern int MsiEnumProducts(int index, StringBuilder guid);
        [DllImport("msi", CharSet = CharSet.Auto)]
        public static extern int MsiEnumComponents(int index, StringBuilder guid);
        [DllImport("msi", CharSet = CharSet.Auto)]
        public static extern int MsiEnumClients(string compguid, int index,
           StringBuilder prodguid);
        [DllImport("msi", CharSet = CharSet.Auto)]
        public static extern int MsiGetProductInfo(string guid, string propertyname,
           StringBuilder retprop, ref int szbuf);
    }

    public class MsiFieldParts : DisposableObject
    {
        public MsiFieldParts(string field)
        {
            this.Parse(field);
        }

        private void Parse(string field)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Parse");

            try
            {
                string[] parts = field.Split('|');
                if (parts != null &&
                    parts.Length > 0)
                {
                    if (parts.Length == 1)
                    {
                        this.Field = parts[0];
                    }
                    if (parts.Length >= 2)
                    {
                        this.Hash = parts[0];
                        this.Field = parts[1];
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        public string Hash { get; private set; }

        public string Field { get; private set; }

        public override string ToString()
        {
            return this.Field;
        }
    }

    internal interface IMsiRecordHierarchy : IDisposable
    {
        string HierarchyKey { get; }

        //string ParentHierarchy { get; set; }

        MsiRecordHierarchies Children { get; }
    }

    internal class MsiRecordHierarchy : DisposableObject, IMsiRecordHierarchy
    {
        public MsiRecordHierarchy(string hierarchyKey)
        {
            this.HierarchyKey = hierarchyKey;
            this.Children = new MsiRecordHierarchies();
        }

        public override string ToString()
        {
            return this.HierarchyKey;
        }

        public virtual string HierarchyKey { get; private set; }

        //public virtual string ParentHierarchy { get; set; }

        public MsiRecordHierarchies Children { get; private set; }
    }

    internal class MsiRecordHierarchies : SortedDictionary<string, IMsiRecordHierarchy>
    {
        public MsiRecordHierarchies()
            : base(StringComparer.InvariantCultureIgnoreCase) { }
    }

    public class MsiRecord : List<string>
    {
        public MsiRecord() { }
    }

    public class MsiRecords : List<MsiRecord>
    {
        public MsiRecords() { }

        public MsiRecords(IEnumerable<MsiRecord> collection)
            : base(collection) { }
    }

    public class MsiRecord_File : DisposableObject
    {
        public string File { get; set; }
        public string Component { get; set; }
        public MsiFieldParts FileName { get; set; }
        public long FileSize { get; set; }
        public string Version { get; set; }
        public string Language { get; set; }
        public string Attributes { get; set; }
        public long Sequence { get; set; }

        public override string ToString()
        {
            return this.File;
        }
    }

    public class MsiRecord_Files : List<MsiRecord_File>
    {
        public MsiRecord_Files() { }

        public MsiRecord_Files(IEnumerable<MsiRecord_File> collection)
            : base(collection) { }
    }

    public class MsiRecord_Directory : DisposableObject
    {
        public string Directory { get; set; }
        public string ParentDirectory { get; set; }
        public MsiFieldParts DefaultPath { get; set; }

        public override string ToString()
        {
            return this.Directory;
        }
    }

    public class MsiRecord_Directories : List<MsiRecord_Directory>
    {
        public MsiRecord_Directories() { }

        public MsiRecord_Directories(IEnumerable<MsiRecord_Directory> collection)
            : base(collection) { }
    }

    public class MsiInstaller : DisposableObject
    {
        private IntPtr _fileHandle = IntPtr.Zero;

        public MsiInstaller(string msiFile)
        {
            this.MsiFile = msiFile;
        }

        public string MsiFile { get; private set; }

        public bool Open()
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Open");
            bool result = default(bool);

            try
            {
                if (_fileHandle != IntPtr.Zero) return false;

                int status = MsiNativeHelper.MsiOpenDatabase(this.MsiFile, MsiNativeHelper.Persist.MSIDBOPEN_READONLY, ref _fileHandle);
                result = (_fileHandle != IntPtr.Zero);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        public MsiRecords ExecuteQuery(string query)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Method");
            MsiRecords result = new MsiRecords();
            if (_fileHandle == IntPtr.Zero) return result;

            try
            {
                IntPtr viewHandle = IntPtr.Zero;
                IntPtr recHandle = IntPtr.Zero;

                int hr = MsiNativeHelper.MsiDatabaseOpenView(_fileHandle, query, ref viewHandle);
                if (hr == 0)
                {
                    hr = MsiNativeHelper.MsiViewExecute(viewHandle, IntPtr.Zero);
                    if (hr == 0)
                    {
                        while ((hr = MsiNativeHelper.MsiViewFetch(viewHandle, ref recHandle)) != MsiNativeHelper.ERROR_NO_MORE_ITEMS)
                        {
                            if (recHandle != IntPtr.Zero)
                            {
                                int fields = MsiNativeHelper.MsiRecordGetFieldCount(recHandle);
                                if (fields > 0)
                                {
                                    MsiRecord record = new MsiRecord();
                                    for (int i = 1; i <= fields; i++)
                                    {
                                        int len = 255;
                                        StringBuilder sb = new StringBuilder(len);

                                        hr = MsiNativeHelper.MsiRecordGetString(recHandle, i, sb, ref len);
                                        if (hr == 0)
                                        {
                                            record.Add(sb.ToString());
                                        }
                                    }
                                    result.Add(record);
                                }
                                MsiNativeHelper.MsiCloseHandle(recHandle);
                            }
                        }
                    }
                }

                if (viewHandle != IntPtr.Zero)
                {
                    MsiNativeHelper.MsiCloseHandle(viewHandle);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        public MsiRecord_Files GetRecord_Files()
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "GetRecord_Files");
            MsiRecord_Files result = new MsiRecord_Files();

            try
            {
                MsiRecords records = this.ExecuteQuery("SELECT * FROM `File`");
                foreach (MsiRecord record in records)
                {
                    if (record.Count == 8)
                    {
                        MsiRecord_File file = new MsiRecord_File()
                        {
                            File = record[0],
                            Component = record[1],
                            FileName = new MsiFieldParts(record[2]),
                            FileSize = TypeSystem.GetValueInt64(record[3]),
                            Version = record[4],
                            Language = record[5],
                            Attributes = record[6],
                            Sequence = TypeSystem.GetValueInt64(record[7]),
                        };
                        result.Add(file);
                    }
                }

                result = new MsiRecord_Files((from r in result
                                              orderby r.File
                                              select r));
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        public MsiRecord_Directories GetRecord_Directories()
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "GetRecord_Directories");
            MsiRecord_Directories result = new MsiRecord_Directories();

            try
            {
                MsiRecords records = this.ExecuteQuery("SELECT * FROM `Directory`");
                foreach (MsiRecord record in records)
                {
                    if (record.Count == 3)
                    {
                        MsiRecord_Directory file = new MsiRecord_Directory()
                        {
                            Directory = record[0],
                            ParentDirectory = record[1],
                            DefaultPath = new MsiFieldParts(record[2]),
                        };
                        result.Add(file);
                    }
                }

                result = new MsiRecord_Directories((from r in result
                                                    orderby r.Directory
                                                    select r));
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }



        public MsiRecord_Directories GetRecord_DirectoriesHierarchy()
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "GetRecord_DirectoriesHierarchy");
            MsiRecord_Directories result = null;

            try
            {
                //IDictionary<string, MsiRecord> dict = new SortedDictionary<string, MsiRecord>(StringComparer.InvariantCultureIgnoreCase);
                //IDictionary<string, DirItem> dict2 = new SortedDictionary<string, DirItem>(StringComparer.InvariantCultureIgnoreCase);
                MsiRecordHierarchies root = new MsiRecordHierarchies();
                MsiRecord_Directories directories = this.GetRecord_Directories();

                foreach (var directory in directories)
                {
                    string parent = directory.ParentDirectory;
                    string child = directory.Directory;
                    IMsiRecordHierarchy parentItem = null;
                    IMsiRecordHierarchy childItem = null;

                    // parent item
                    if (!root.ContainsKey(parent))
                    {
                        // push all the nodes
                        MsiRecordHierarchies found = root;
                        IMsiRecordHierarchy current = null;

                        if (root.Count > 0)
                        {
                            Stack<IMsiRecordHierarchy> st = new Stack<IMsiRecordHierarchy>();

                            foreach (var node in root.Values)
                            {
                                st.Push(node);
                            }

                            while (st.Count != 0)
                            {
                                current = st.Pop();
                                if (current.Children.ContainsKey(parent))
                                {
                                    found = current.Children;
                                    break;
                                }

                                foreach (var node in current.Children.Values)
                                {
                                    st.Push(node);
                                }
                            }
                        }

                        if (found != null)
                        {
                            parentItem = new MsiRecordHierarchy(parent);
                            found.Add(parentItem.HierarchyKey, parentItem);
                        }
                    }
                    else
                    {
                        parentItem = root[parent];
                    }

                    // remove the child item from the root dictionary
                    if (root.ContainsKey(child))
                    {
                        root.Remove(child);
                    }

                    // child item
                    if (!parentItem.Children.ContainsKey(child))
                    {
                        childItem = new MsiRecordHierarchy(child);
                        parentItem.Children.Add(childItem.HierarchyKey, childItem);
                    }
                    else
                    {
                        childItem = parentItem.Children[child];
                    }

                    //DirItem parentItem = null;
                    //DirItem childItem = null;

                    //if (!dict2.ContainsKey(parent))
                    //{
                    //    parentItem = new DirItem(parent);
                    //    dict2.Add(parent, parentItem);
                    //}
                    //else
                    //{
                    //    parentItem = dict2[parent];
                    //}

                    //if (!dict2.ContainsKey(child))
                    //{
                    //    childItem = new DirItem(child);
                    //    dict2.Add(child, childItem);
                    //}
                    //else
                    //{
                    //    childItem = dict2[child];
                    //}

                    //parentItem.Children.Add(childItem);

                    //if (!dict.ContainsKey(child))
                    //{
                    //    dict.Add(child, new MsiRecord());
                    //}
                    //if (!dict.ContainsKey(parent))
                    //{
                    //    dict.Add(parent, new MsiRecord());
                    //}
                    //dict[parent].Add(child);
                }

                int i = 6;
                //foreach (var directory in directories)
                //{
                //    string dir = directory.Directory;
                //    if (!dirOrder.ContainsKey(dir))
                //    {
                //        //directory.Order = 1;
                //        dirOrder.Add(dir, -1);
                //    }
                //}

                //foreach (var directory in directories)
                //{
                //    string child = directory.Directory;
                //    string parent = directory.ParentDirectory;
                //    int parentIndex = dirOrder[parent];

                //    if (parentIndex == -1)
                //    {
                //        int count = dirList.Count;
                //        dirList.Add(parent);
                //        dirOrder[parent] = (parentIndex = count);
                //    }

                //    int childIndex = (parentIndex + 1);
                //    if (childIndex >= dirList.Count)
                //    {
                //        int count = dirList.Count;
                //        dirList.Add(child);
                //        dirOrder[child] = (childIndex = count);
                //    }
                //    else
                //    {
                //        dirList.Insert(childIndex, );
                //    }
                //}

                //result = new MsiRecord_Directories((from d in dirOrder.Values
                //                                    select d));
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        public bool Close()
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Close");
            bool result = default(bool);

            try
            {
                if (_fileHandle != IntPtr.Zero)
                {
                    MsiNativeHelper.MsiCloseHandle(_fileHandle);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        protected override void DisposeManaged()
        {
            base.DisposeManaged();
            this.Close();
        }
    }
}
