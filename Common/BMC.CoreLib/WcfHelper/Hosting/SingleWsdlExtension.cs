using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Schema;
using BMC.CoreLib.Diagnostics;
using System.Web.Services.Description;
using System.ServiceModel.Description;
using System.Collections;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Xml.Serialization;

namespace BMC.CoreLib.WcfHelper.Hosting
{
    public class SingleWsdlExtension : DisposableObject, IWsdlExportExtension, IEndpointBehavior
    {
        public void ExportContract(WsdlExporter exporter, WsdlContractConversionContext context) { }

        public void ExportEndpoint(WsdlExporter exporter, WsdlEndpointConversionContext context)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "ExportEndpoint");

            try
            {
                System.Web.Services.Description.ServiceDescription wsdl = exporter.GeneratedWsdlDocuments[0];
                XmlSchemaSet schemaSet = exporter.GeneratedXmlSchemas;
                XmlSchemas imports = new XmlSchemas();

                foreach (XmlSchema schema in exporter.GeneratedXmlSchemas.Schemas())
                {
                    imports.Add(schema);
                }
                foreach (XmlSchema schema in imports)
                {
                    schema.Includes.Clear();
                }

                wsdl.Types.Schemas.Clear();
                wsdl.Types.Schemas.Add(imports);
                //List<XmlSchema> importsList = new List<XmlSchema>();

                //    foreach (XmlSchema schema in wsdl.Types.Schemas)
                //    {
                //        AddImportedSchemas(schema, schemaSet, importsList);
                //    }

                //    wsdl.Types.Schemas.Clear();

                //    foreach (XmlSchema schema in importsList)
                //    {
                //        RemoveXsdImports(schema);
                //        wsdl.Types.Schemas.Add(schema);
                //    }
                //}
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        private void AddImportedSchemas(XmlSchema schema, XmlSchemaSet schemaSet, List<XmlSchema> importsList)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "AddImportedSchemas");

            try
            {
                foreach (XmlSchemaImport import in schema.Includes)
                {
                    ICollection realSchemas = schemaSet.Schemas(import.Namespace);

                    foreach (XmlSchema ixsd in realSchemas)
                    {
                        if (!importsList.Contains(ixsd))
                        {
                            importsList.Add(ixsd);
                            AddImportedSchemas(ixsd, schemaSet, importsList);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        private void RemoveXsdImports(XmlSchema schema)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "AddImportedSchemas");

            try
            {
                for (int i = 0; i < schema.Includes.Count; i++)
                {
                    if (schema.Includes[i] is XmlSchemaImport)
                        schema.Includes.RemoveAt(i--);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters) { }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime) { }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher) { }

        public void Validate(ServiceEndpoint endpoint) { }
    }
}
