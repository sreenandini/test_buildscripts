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

namespace BMC.CoreLib.WcfHelper.Behaviors
{
    public class WcfSingleWsdlExportEndpoint : DisposableObject
    {
        private WsdlExporter _exporter = null;
        private WsdlEndpointConversionContext _context = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="WcfWsdlExport"/> class.
        /// </summary>
        /// <param name="exporter">The exporter.</param>
        /// <param name="context">The context.</param>
        internal WcfSingleWsdlExportEndpoint(WsdlExporter exporter, WsdlEndpointConversionContext context)
        {
            _exporter = exporter;
            _context = context;
        }
        public void ExportContract() { }

        public void ExportEndpoint()
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "ExportEndpoint");

            try
            {
                System.Web.Services.Description.ServiceDescription wsdl = _exporter.GeneratedWsdlDocuments[0];
                XmlSchemaSet schemaSet = _exporter.GeneratedXmlSchemas;
                XmlSchemas imports = new XmlSchemas();

                foreach (XmlSchema schema in _exporter.GeneratedXmlSchemas.Schemas())
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
    }
}
