using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib;
using BMC.CoreLib.Diagnostics;
using BMC.EnterpriseDataAccess;
using System.Reflection;
using System.Data.Linq.Mapping;
using System.IO;

namespace CombineProcedures
{
    class Program : DisposableObject
    {
        private string[] SUB_FOLDERS = new string[] { "Stored Procedures", "User Defined Functions", "Views","Stored Procedures\\Reports"};

        static void Main(string[] args)
        {
            Program p = new Program();
            p.Combine();

            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }

        private void Combine()
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Combine");
            string databaseFolder = Path.GetFullPath(Path.Combine(Extensions.GetStartupDirectory(),
                @"../../../../Database\Enterprise DB"));

            string outputFile = Path.Combine(databaseFolder, @"Stored Procedures\EnterpriseClient_Combined.sql");
            string star = new string('*', 120);
            StringBuilder sb = new StringBuilder();
            using (StreamWriter sw = new StreamWriter(File.Open(outputFile, FileMode.Create, FileAccess.Write, FileShare.Read)))
            {
                try
                {
                    Type typeContext = typeof(EnterpriseDataContext);
                    var methods = (from t in typeContext.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                                   from a in t.GetCustomAttributes(typeof(FunctionAttribute), false).OfType<FunctionAttribute>()
                                   select a).ToList<FunctionAttribute>();
                    if (methods != null)
                    {
                        int index = 1;
                        int validIndex = 1;
                        foreach (var method in methods)
                        {
                            string name = method.Name;
                            string file = this.FindFile(databaseFolder, name);
                            if (file.IsEmpty())
                            {
                                Console.WriteLine("{0:D} File {1} does not exist.", index++, name);
                                sb.AppendLine(Path.GetFileName(name));
                            }
                            else
                            {
                                string nameOnly = file.ToUpper().Replace(databaseFolder.ToUpper() + "\\", "");
                                sw.WriteLine("/*" + star);
                                sw.WriteLine(" * ({0:D}) [START] - {1}", validIndex, nameOnly);
                                sw.WriteLine(star + "*/");
                                sw.WriteLine();
                                
                                sw.WriteLine("USE ENTERPRISE");
                                sw.WriteLine("GO");
                                sw.WriteLine();

                                sw.WriteLine(File.ReadAllText(file));
                                sw.WriteLine("GO");
                                sw.WriteLine();

                                sw.WriteLine("/*" + star);
                                sw.WriteLine(" * ({0:D}) [END] - {1}", validIndex, nameOnly);
                                sw.WriteLine(star + "*/");
                                sw.WriteLine();
                                sw.Flush();
                                validIndex++;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.Exception(PROC, ex);
                }
                sw.Close();
            }
        }

        private string FindFile(string folder, string file)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "FindFile");
            string result = default(string);

            try
            {
                string fileOnly = file.ToUpper().Replace("DBO.", "");
                foreach (var subFolder in SUB_FOLDERS)
                {
                    string filePath = Path.Combine(Path.Combine(folder, subFolder), fileOnly + ".sql");
                    if (File.Exists(filePath))
                    {
                        return filePath;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        private string ReadFileContent(string file)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "ReadFileContent");
            string result = default(string);

            try
            {

            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }
    }
}
