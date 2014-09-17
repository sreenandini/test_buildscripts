using BMC.CoreLib;
using BMC.CoreLib.Diagnostics;
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMC.RegParser
{
    public class RegKeyValuePair
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public Type ValueType { get; set; }

        public override string ToString()
        {
            return "(" + this.ValueType.Name + ") " + this.Key + " => " + this.Value;
        }
    }

    public class RegKeyValuePairs : List<RegKeyValuePair>
    {
        public override string ToString()
        {
            return "Total : " + this.Count.ToString();
        }
    }

    

    public class EmbedResource
    {
        public string ResourceName { get; set; }
        public string FileName { get; set; }

        public string Category { get; set; }
    }

    public class Program
    {
        private List<EmbedResource> EmbedResources = new List<EmbedResource>()
        {
            new EmbedResource() { ResourceName="BMC.RegParser.EnterpriseServer.reg", Category="Enterprise", FileName="Config_EnterpriseServer" },
            new EmbedResource() { ResourceName="BMC.RegParser.EnterpriseClient.reg", Category="Enterprise", FileName="Config_EnterpriseClient" },
            new EmbedResource() { ResourceName="BMC.RegParser.ExchangeServer.reg", Category="Exchange", FileName="Config_ExchangeServer" },
            new EmbedResource() { ResourceName="BMC.RegParser.ExchangeClient.reg", Category="Exchange", FileName="Config_ExchangeClient" },
        };

        private const string REG_HEADER = "Windows Registry Editor Version 5.00";

        static void Main(string[] args)
        {
            Console.WriteLine("RegParser : <START>");
            Program p = new Program();
            p.Parse();
            Console.WriteLine("RegParser : <END>");
        }

        private class DictStringRegKeyValuePairs : SortedDictionary<string, RegKeyValuePairs>
        {
            public DictStringRegKeyValuePairs() :
                base(StringComparer.InvariantCultureIgnoreCase) { }

            public string Category { get; set; }
        }

        private class DictResourceStringRegKeyValuePairs : SortedDictionary<string, DictStringRegKeyValuePairs>
        {
            public DictResourceStringRegKeyValuePairs() :
                base(StringComparer.InvariantCultureIgnoreCase) { }
        }

        private class CommonPropertyValue
        {
            public int Count { get; set; }
            public string TypeName { get; set; }
            public string FileName { get; set; }
        }

        private class DictCommonProperties : SortedDictionary<string, CommonPropertyValue>
        {
            public DictCommonProperties() :
                base(StringComparer.InvariantCultureIgnoreCase) { }

            public string FileName { get; set; }
        }

        private class DictConfigCategories : SortedDictionary<string, DictCommonProperties>
        {
            public DictConfigCategories() :
                base(StringComparer.InvariantCultureIgnoreCase) { }
        }

        public void Parse()
        {
            ModuleProc PROC = new ModuleProc("", "Method");

            try
            {
                string[] splitChars = new string[] { "\"=\"" };
                string[] splitChars2 = new string[] { "\"=" };
                DictResourceStringRegKeyValuePairs dictOverall = new DictResourceStringRegKeyValuePairs();

                string fileName = Path.GetFullPath(Extensions.GetStartupDirectory() + @"\.\..\..\..\..\\BMC.Common\Persistence\ConfigApplications.cs");
                CodeDomProvider provider = CodeDomProvider.CreateProvider("csharp");
                CodeCompileUnit unit = new CodeCompileUnit();
                CodeNamespace cns = new CodeNamespace("BMC.Common.Persistence");
                unit.Namespaces.Add(cns);
                cns.Imports.Add(new CodeNamespaceImport("System"));

                foreach (var embedResource in EmbedResources)
                {
                    string resourceName = embedResource.ResourceName;
                    string cfgFileName = embedResource.FileName;
                    DictStringRegKeyValuePairs dictValues = new DictStringRegKeyValuePairs()
                    {
                        Category = embedResource.Category,
                    };
                    dictOverall.Add(cfgFileName, dictValues);

                    using (Stream st = typeof(Program).Assembly.GetManifestResourceStream(resourceName))
                    {
                        StreamReader sr = new StreamReader(st);
                        RegKeyValuePairs regValue = null;
                        string lastLine = string.Empty;

                        while (!sr.EndOfStream)
                        {
                            string line = sr.ReadLine();

                            if (line.IgnoreCaseCompare(REG_HEADER) || line.IsEmpty()) continue;

                            if (line.Contains("[HKEY_LOCAL_MACHINE"))
                            {
                                string actualLine = line.Replace(@"HKEY_LOCAL_MACHINE\SOFTWARE\", "").Replace("[", "").Replace("]", "");
                                if (dictValues.ContainsKey(actualLine))
                                {
                                    regValue = dictValues[actualLine];
                                }
                                else
                                {
                                    regValue = new RegKeyValuePairs();
                                    dictValues.Add(actualLine, regValue);
                                }
                                lastLine = actualLine;
                                continue;
                            }

                            string[] keyvalues = line.Split(splitChars, StringSplitOptions.None);
                            if (keyvalues == null || keyvalues.Length < 2)
                            {
                                keyvalues = line.Split(splitChars2, StringSplitOptions.None);
                            }
                            if (keyvalues != null && keyvalues.Length == 2)
                            {
                                string key = keyvalues[0].Replace("\"", "");
                                string value = keyvalues[1].Replace("\"", "");
                                Type valueType = typeof(string);

                                if (value.IgnoreCaseCompare("true") ||
                                    value.IgnoreCaseCompare("false"))
                                {
                                    valueType = typeof(bool);
                                }
                                else if (value.StartsWith("dword:"))
                                {
                                    value = value.Replace("dword:", "");

                                    if (value.Length > 10)
                                        valueType = typeof(long);
                                    else
                                        valueType = typeof(int);
                                }
                                RegKeyValuePair kvpair = new RegKeyValuePair()
                                {
                                    Key = key,
                                    Value = value,
                                    ValueType = valueType
                                };
                                regValue.Add(kvpair);
                            }
                        }
                    }
                }                          

                // categorize the stuff
                DictConfigCategories cfgCategories = new DictConfigCategories();
                DictCommonProperties ccmnProperties = new DictCommonProperties();
                IDictionary<string, string> dictInterfaces = new SortedDictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
                foreach (var pair1 in dictOverall)
                {
                    DictCommonProperties cmnProperties = null;
                    dictInterfaces.Add(pair1.Key, pair1.Value.Category);

                    if (cfgCategories.ContainsKey(pair1.Value.Category))
                    {
                        cmnProperties = cfgCategories[pair1.Value.Category];
                    }
                    else
                    {
                        cmnProperties = new DictCommonProperties()
                        {
                            FileName = pair1.Key,
                        };
                        cfgCategories.Add(pair1.Value.Category, cmnProperties);
                    }

                    foreach (var pair2 in pair1.Value)
                    {
                        string sectionName = pair2.Key.Replace("\\", "_");

                        foreach (var pair3 in pair2.Value)
                        {
                            string fullPropertyName = sectionName + "_" + pair3.Key;
                            string typeName = typeof(string).FullName;
                            if (pair3.ValueType == typeof(long))
                            {
                                typeName = typeof(long).FullName;
                            }
                            else if (pair3.ValueType == typeof(int))
                            {
                                typeName = typeof(int).FullName;
                            }
                            else if (pair3.ValueType == typeof(bool))
                            {
                                typeName = typeof(bool).FullName;
                            }

                            if (1 == 1)
                            {
                                CommonPropertyValue propValue = null;

                                if (cmnProperties.ContainsKey(fullPropertyName))
                                {
                                    propValue = cmnProperties[fullPropertyName];
                                }
                                else
                                {
                                    propValue = new CommonPropertyValue();
                                    cmnProperties.Add(fullPropertyName, propValue);
                                }

                                propValue.Count += 1;
                                propValue.TypeName = typeName;
                                propValue.FileName = pair1.Key;
                            }

                            if (1 == 1)
                            {
                                CommonPropertyValue propValue = null;

                                if (ccmnProperties.ContainsKey(fullPropertyName))
                                {
                                    propValue = ccmnProperties[fullPropertyName];
                                }
                                else
                                {
                                    propValue = new CommonPropertyValue();
                                    ccmnProperties.Add(fullPropertyName, propValue);
                                }

                                propValue.Count += 1;
                                propValue.TypeName = typeName;
                                propValue.FileName = pair1.Key;
                            }
                        }
                    }
                }

                // create the root class (honeyframe)
                var honeyframeProperties = (from a in ccmnProperties
                                            where a.Value.Count == EmbedResources.Count
                                            orderby a.Key
                                            select a).ToArray();
                if (1 == 1)
                {
                    string ifaceName = "IConfig_Honeyframe";
                    CodeTypeDeclaration clsIface = new CodeTypeDeclaration();
                    clsIface.IsInterface = true;
                    clsIface.Name = ifaceName;
                    clsIface.Attributes = MemberAttributes.Public | MemberAttributes.Final;
                    clsIface.BaseTypes.Add(new CodeTypeReference("BMC.Common.Persistence.IConfigApplication"));
                    cns.Types.Add(clsIface);

                    foreach (var pair2 in honeyframeProperties)
                    {
                        // property
                        CodeMemberProperty domProp = new CodeMemberProperty();
                        domProp.Attributes = MemberAttributes.Public | MemberAttributes.Final;
                        domProp.Name = pair2.Key;
                        domProp.HasGet = true;
                        domProp.HasSet = true;
                        domProp.Type = new CodeTypeReference(pair2.Value.TypeName);
                        clsIface.Members.Add(domProp);
                    }

                    // remove the references from the sub classes
                    foreach (var pair1 in cfgCategories)
                    {
                        foreach (var pair2 in honeyframeProperties)
                        {
                            string key = pair2.Key;
                            if (pair1.Value.ContainsKey(key))
                            {
                                pair1.Value.Remove(key);
                            }
                        }
                    }
                }

                // create the sub classes (Exchange/Enterprise)
                foreach (var pair1 in cfgCategories)
                {
                    string ifaceNameCategory = string.Empty;
                    if (1 == 1)
                    {
                        string ifaceName = "IConfig_" + pair1.Key;
                        ifaceNameCategory = "BMC.Common.Persistence." + ifaceName;
                        CodeTypeDeclaration clsIface = new CodeTypeDeclaration();
                        clsIface.IsInterface = true;
                        clsIface.Name = ifaceName;
                        clsIface.Attributes = MemberAttributes.Public | MemberAttributes.Final;
                        clsIface.BaseTypes.Add(new CodeTypeReference("BMC.Common.Persistence.IConfig_Honeyframe"));
                        cns.Types.Add(clsIface);

                        // common exchange/enterprise properties
                        var commonProperties = (from a in pair1.Value
                                                where a.Value.Count > 1
                                                orderby a.Key
                                                select a);

                        foreach (var pair2 in commonProperties)
                        {
                            // property
                            CodeMemberProperty domProp = new CodeMemberProperty();
                            domProp.Attributes = MemberAttributes.Public | MemberAttributes.Final;
                            domProp.Name = pair2.Key;
                            domProp.HasGet = true;
                            domProp.HasSet = true;
                            domProp.Type = new CodeTypeReference(pair2.Value.TypeName);
                            clsIface.Members.Add(domProp);
                        }
                    }

                    // uncommon exchange/enterprise properties
                    if (1 == 1)
                    {
                        var uncommonProperties1 = (from a in pair1.Value
                                                   where a.Value.Count == 1
                                                   group a by a.Value.FileName);
                        foreach (var pair2 in uncommonProperties1)
                        {
                            if (dictInterfaces.ContainsKey(pair2.Key))
                            {
                                dictInterfaces.Remove(pair2.Key);
                            }
                            string ifaceName = "I" + pair2.Key;
                            CodeTypeDeclaration clsIface = new CodeTypeDeclaration();
                            clsIface.IsInterface = true;
                            clsIface.Name = ifaceName;
                            clsIface.Attributes = MemberAttributes.Public | MemberAttributes.Final;
                            clsIface.BaseTypes.Add(new CodeTypeReference(ifaceNameCategory));
                            cns.Types.Add(clsIface);

                            foreach (var pair3 in pair2)
                            {
                                // property
                                CodeMemberProperty domProp = new CodeMemberProperty();
                                domProp.Attributes = MemberAttributes.Public | MemberAttributes.Final;
                                domProp.Name = pair3.Key;
                                domProp.HasGet = true;
                                domProp.HasSet = true;
                                domProp.Type = new CodeTypeReference(pair3.Value.TypeName);
                                clsIface.Members.Add(domProp);
                            }
                        }
                    }

                    // empty interfaces
                    foreach (var pair2 in dictInterfaces)
                    {
                        if (pair2.Value == pair1.Key)
                        {
                            string ifaceName = "I" + pair2.Key;
                            CodeTypeDeclaration clsIface = new CodeTypeDeclaration();
                            clsIface.IsInterface = true;
                            clsIface.Name = ifaceName;
                            clsIface.Attributes = MemberAttributes.Public | MemberAttributes.Final;
                            clsIface.BaseTypes.Add(new CodeTypeReference(ifaceNameCategory));
                            cns.Types.Add(clsIface);
                        }
                    }
                }

                foreach (var pair1 in dictOverall)
                {
                    string sClassName = pair1.Key;
                    string ifaceName = "I" + sClassName;

                    CodeTypeDeclaration cls = new CodeTypeDeclaration();
                    cls.IsClass = true;
                    cls.Name = pair1.Key;
                    cls.TypeAttributes = System.Reflection.TypeAttributes.Sealed | System.Reflection.TypeAttributes.NotPublic;
                    cls.BaseTypes.Add(new CodeTypeReference("BMC.Common.Persistence.ConfigApplicationBase"));
                    cls.BaseTypes.Add(new CodeTypeReference("BMC.Common.Persistence." + ifaceName));
                    cns.Types.Add(cls);

                    //_configProvider
                    //string configName = sClassName + ".xml";
                    //CodeMemberField domFieldConfig = new CodeMemberField();
                    string configProvider = "_configProvider";
                    //domFieldConfig.Name = configProvider;
                    //domFieldConfig.Attributes = MemberAttributes.Private;
                    //domFieldConfig.Type = new CodeTypeReference("BMC.Common.Persistence.IConfigProvider");
                    //domFieldConfig.InitExpression = new CodeMethodInvokeExpression(
                    //    new CodeTypeReferenceExpression("BMC.Common.Persistence.ConfigApplicationFactory"),
                    //    "GetProvider");
                    CodeFieldReferenceExpression domFieldConfigRef = new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), configProvider);

                    //cls.Members.Add(domFieldConfig);

                    CodeConstructor domCtor = new CodeConstructor();
                    domCtor.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference("BMC.Common.Persistence.IConfigProvider"), "configProvider"));
                    domCtor.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(bool).FullName), "load"));
                    domCtor.Attributes = MemberAttributes.Public;
                    CodeAssignStatement assignConfigProvider = new CodeAssignStatement();
                    assignConfigProvider.Left = domFieldConfigRef;
                    assignConfigProvider.Right = new CodeVariableReferenceExpression("configProvider");
                    domCtor.Statements.Add(assignConfigProvider);
                    cls.Members.Add(domCtor);

                    // Initialize
                    CodeMemberMethod domMethodInitialize = new CodeMemberMethod();
                    domMethodInitialize.Name = "Initialize";
                    domMethodInitialize.Attributes = MemberAttributes.Public | MemberAttributes.Override;
                    domMethodInitialize.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(bool).FullName), "load"));
                    cls.Members.Add(domMethodInitialize);

                    CodeMethodReferenceExpression mrObjInitialize = new CodeMethodReferenceExpression();
                    mrObjInitialize.TargetObject = new CodeThisReferenceExpression();
                    mrObjInitialize.MethodName = "Initialize";                    
                    CodeMethodInvokeExpression miObjInitialize = new CodeMethodInvokeExpression(mrObjInitialize,
                        new CodeVariableReferenceExpression("load"));
                    domCtor.Statements.Add(miObjInitialize);

                    CodeAssignStatement asgnisMetadataInitialized = new CodeAssignStatement();
                    asgnisMetadataInitialized.Left = new CodePropertyReferenceExpression(
                        domFieldConfigRef, "IsMetadataInitialized");
                    asgnisMetadataInitialized.Right = new CodePrimitiveExpression(true);

                    // this.Load();
                    CodeMethodInvokeExpression thisLoadInvoke = new CodeMethodInvokeExpression();
                    CodeMethodReferenceExpression thisLoadRef = new CodeMethodReferenceExpression();
                    thisLoadRef.TargetObject = new CodeThisReferenceExpression();
                    thisLoadRef.MethodName = "Load";
                    thisLoadInvoke.Method = thisLoadRef;

                    // Load
                    CodeMemberMethod domMethodLoad = new CodeMemberMethod();
                    domMethodLoad.Name = "Load";
                    domMethodLoad.Attributes = MemberAttributes.Public | MemberAttributes.Override;
                    CodeMethodReferenceExpression mrLoad = new CodeMethodReferenceExpression();
                    mrLoad.TargetObject = domFieldConfigRef;
                    mrLoad.MethodName = "Load";
                    CodeMethodInvokeExpression miLoad = new CodeMethodInvokeExpression(mrLoad);
                    domMethodLoad.Statements.Add(miLoad);
                    cls.Members.Add(domMethodLoad);

                    // Save
                    CodeMemberMethod domMethodSave = new CodeMemberMethod();
                    domMethodSave.Name = "Save";
                    domMethodSave.Attributes = MemberAttributes.Public | MemberAttributes.Override;
                    CodeMethodReferenceExpression mrSave = new CodeMethodReferenceExpression();
                    mrSave.TargetObject = domFieldConfigRef;
                    mrSave.MethodName = "Save";
                    CodeMethodInvokeExpression miSave = new CodeMethodInvokeExpression(mrSave);
                    domMethodSave.Statements.Add(miSave);
                    cls.Members.Add(domMethodSave);

                    // create the methods
                    foreach (var pair2 in pair1.Value)
                    {
                        string sectionName = pair2.Key.Replace("\\", "_");
                        string methodName = "Initialize_" + sectionName;
                        CodeMemberMethod domMethod = new CodeMemberMethod();
                        domMethod.Attributes = MemberAttributes.Private | MemberAttributes.Final;
                        domMethod.Name = methodName;
                        cls.Members.Add(domMethod);

                        CodeMemberField domFieldSection = new CodeMemberField();
                        string sSectionName = "_s_" + sectionName;
                        domFieldSection.Name = sSectionName;
                        domFieldSection.Attributes = MemberAttributes.Private | MemberAttributes.Const;
                        domFieldSection.Type = new CodeTypeReference(typeof(string).FullName);
                        domFieldSection.InitExpression = new CodePrimitiveExpression(pair2.Key);
                        CodeFieldReferenceExpression domFieldSectionRef = new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(cls.Name), sSectionName);
                        cls.Members.Add(domFieldSection);

                        CodeMethodInvokeExpression domMethodInvoke = new CodeMethodInvokeExpression();
                        domMethodInvoke.Method = new CodeMethodReferenceExpression(new CodeThisReferenceExpression(), methodName);

                        foreach (var pair3 in pair2.Value)
                        {
                            string fullPropertyName = sectionName + "_" + pair3.Key;
                            object value = pair3.Value.ToString();
                            string typeName = typeof(string).FullName;
                            if (pair3.ValueType == typeof(long))
                            {
                                typeName = typeof(long).FullName;
                                value = Convert.ToInt64(pair3.Value, 16);// Convert.ChangeType(item.Value, typeof(long));
                            }
                            else if (pair3.ValueType == typeof(int))
                            {
                                typeName = typeof(int).FullName;
                                value = Convert.ToInt32(pair3.Value, 16);// Convert.ChangeType(item.Value, typeof(int));
                            }
                            else if (pair3.ValueType == typeof(bool))
                            {
                                typeName = typeof(bool).FullName;
                                value = pair3.Value.IgnoreCaseCompare("true") ? true : false;
                            }

                            CodeExpression[] initializeValueParams = new CodeExpression[3] {
                                domFieldSectionRef,
                                new CodePrimitiveExpression(pair3.Key),
                                new CodePrimitiveExpression(value)
                            };
                            CodeExpression[] setValueParams = new CodeExpression[3] {
                                domFieldSectionRef,
                                new CodePrimitiveExpression(pair3.Key),
                                new CodeVariableReferenceExpression("value")
                            };
                            CodeExpression[] getValueParams = new CodeExpression[3] {
                                domFieldSectionRef,
                                new CodePrimitiveExpression(pair3.Key),
                                new CodePrimitiveExpression(value),
                            };

                            CodeMethodReferenceExpression mrInitialize = new CodeMethodReferenceExpression();
                            mrInitialize.TargetObject = new CodeThisReferenceExpression();
                            mrInitialize.MethodName = "Initialize";
                            mrInitialize.TypeArguments.Add(new CodeTypeReference(typeName));
                            CodeMethodInvokeExpression miInitialize = new CodeMethodInvokeExpression(mrInitialize, initializeValueParams);
                            domMethod.Statements.Add(miInitialize);

                            CodeMethodReferenceExpression mrSetValue = new CodeMethodReferenceExpression();
                            mrSetValue.TargetObject = domFieldConfigRef;
                            mrSetValue.MethodName = "SetValue";
                            mrSetValue.TypeArguments.Add(new CodeTypeReference(typeName));
                            CodeMethodInvokeExpression miSetValue = new CodeMethodInvokeExpression(mrSetValue, setValueParams);

                            CodeMethodReferenceExpression mrGetValue = new CodeMethodReferenceExpression();
                            mrGetValue.TargetObject = domFieldConfigRef;
                            mrGetValue.MethodName = "GetValue";
                            mrGetValue.TypeArguments.Add(new CodeTypeReference(typeName));
                            CodeMethodInvokeExpression miGetValue = new CodeMethodInvokeExpression(mrGetValue, getValueParams);
                            CodeMethodReturnStatement mrtGetValue = new CodeMethodReturnStatement(miGetValue);

                            // property
                            CodeMemberProperty domProp = new CodeMemberProperty();
                            domProp.Attributes = MemberAttributes.Public | MemberAttributes.Final;
                            domProp.Name = fullPropertyName;
                            domProp.HasGet = true;
                            domProp.HasSet = true;
                            domProp.GetStatements.Add(mrtGetValue);
                            domProp.SetStatements.Add(miSetValue);
                            domProp.Type = new CodeTypeReference(typeName);
                            cls.Members.Add(domProp);

                            CodeAttributeDeclaration displayName = new CodeAttributeDeclaration(new CodeTypeReference("System.ComponentModel.DisplayName"),
                                new CodeAttributeArgument(new CodePrimitiveExpression(pair3.Key)));
                            domProp.CustomAttributes.Add(displayName);
                            CodeAttributeDeclaration category = new CodeAttributeDeclaration(new CodeTypeReference("System.ComponentModel.Category"),
                                new CodeAttributeArgument(new CodePrimitiveExpression(pair2.Key)));
                            domProp.CustomAttributes.Add(category);
                            //clsIface.Members.Add(domProp);
                        }
                        domMethodInitialize.Statements.Add(domMethodInvoke);
                    }

                    domMethodInitialize.Statements.Add(asgnisMetadataInitialized);

                    // if(load == true) { this.Load(); }                    
                    CodeBinaryOperatorExpression boLoad = new CodeBinaryOperatorExpression(
                        new CodeVariableReferenceExpression("load"),
                        CodeBinaryOperatorType.ValueEquality,
                        new CodePrimitiveExpression(true));                                        
                    CodeConditionStatement coLoad = new CodeConditionStatement();
                    coLoad.Condition = boLoad;
                    coLoad.TrueStatements.Add(thisLoadInvoke);
                    domMethodInitialize.Statements.Add(coLoad);
                }

                using (Stream st2 = File.Open(fileName, FileMode.Create, FileAccess.Write, FileShare.Read))
                {
                    StreamWriter sw = new StreamWriter(st2);
                    ICodeGenerator gen = provider.CreateGenerator(fileName);
                    provider.GenerateCodeFromCompileUnit(unit, sw, new CodeGeneratorOptions()
                    {
                        BracingStyle = "C",
                    });
                    sw.Flush();
                    sw.Close();
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }
    }
}
