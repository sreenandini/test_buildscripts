/* ================================================================================= 
 * Purpose		:	Wcf Wsdl Export Endpoint
 * File Name	:   WcfWsdlExportEndpoint.cs
 * Author		:	A.Vinod Kumar
 * Created  	:	15/12/2011
 * ================================================================================= 
 * Copyright (C) 2012 Bally Technologies, Inc. All rights reserved.
 * ================================================================================= 
 * Revision History :
 * ================================================================================= 
 * 15/12/2011		A.Vinod Kumar    Initial Version
 * ===============================================================================*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Description;
using BMC.CoreLib.WcfHelper.Contracts;
using System.Web.Services.Description;
using System.Xml;
using System.Xml.Schema;
using BMC.CoreLib.Diagnostics;

namespace BMC.CoreLib.WcfHelper.Behaviors
{
    /// <summary>
    /// Wcf Wsdl Export Endpoint
    /// </summary>
    public sealed class WcfCustomWsdlExportEndpoint : DisposableObject
    {
        private WsdlExporter _exporter = null;
        private WsdlEndpointConversionContext _context = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="WcfWsdlExport"/> class.
        /// </summary>
        /// <param name="exporter">The exporter.</param>
        /// <param name="context">The context.</param>
        internal WcfCustomWsdlExportEndpoint(WsdlExporter exporter, WsdlEndpointConversionContext context)
        {
            _exporter = exporter;
            _context = context;
        }

        /// <summary>
        /// Exports the endpoint.
        /// </summary>
        /// <param name="info">The info.</param>
        /// <returns>True if succeeded; otherwise false.</returns>
        public bool Export(CustomHeaderExportInfo info)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "WcfWsdlExport");
            bool result = default(bool);

            try
            {
                // custom header
                Message headerMessage = this.CreateHeaderMessage(info);
                bool canBeAdd = false;

                // append this header to each and every wsdl document
                ServiceDescriptionCollection wsdlDocuments = _exporter.GeneratedWsdlDocuments;
                if (wsdlDocuments != null)
                {
                    foreach (System.Web.Services.Description.ServiceDescription wsdl in wsdlDocuments)
                    {
                        // schema details
                        XmlSchema oldSchema = (from s in wsdl.Types.Schemas
                                               where s.TargetNamespace.IgnoreCaseCompare(info.ExportSchema.TargetNamespace)
                                               select s).FirstOrDefault();
                        if (oldSchema == null)
                        {
                            wsdl.Types.Schemas.Add(info.ExportSchema);
                        }

                        // namespace details
                        XmlQualifiedName oldNamespace = (from s in wsdl.Namespaces.ToArray()
                                               where s.Name.IgnoreCaseCompare(info.NamespacePrefix)
                                               && s.Namespace.IgnoreCaseCompare(info.ExportNamespace)
                                               select s).FirstOrDefault();
                        if (oldNamespace == null)
                        {
                            wsdl.Namespaces.Add(info.NamespacePrefix, info.ExportNamespace);
                        }

                        // message details
                        Message oldMessage = (from o in wsdl.Messages.OfType<Message>()
                                              where o.Name.IgnoreCaseCompare(headerMessage.Name)
                                              select o).FirstOrDefault();
                        if (oldMessage == null)
                        {
                            wsdl.Messages.Add(headerMessage);
                            canBeAdd = true;
                        }
                    }
                }

                // add to operations
                if (canBeAdd)
                {
                    this.AddHeaderToOperations(info, headerMessage);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        /// <summary>
        /// Creates the header message.
        /// </summary>
        /// <param name="info">The info.</param>
        /// <returns>Created message.</returns>
        private Message CreateHeaderMessage(CustomHeaderExportInfo info)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "CreateHeaderMessage");
            Message result = default(Message);

            try
            {
                // message
                result = new Message();
                result.Name = info.ExportName;

                // message part
                MessagePart part = new MessagePart();
                part.Name = info.PartName;
                part.Element = new System.Xml.XmlQualifiedName(info.SchemaName, info.SchemaNamespace);
                result.Parts.Add(part);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        /// <summary>
        /// Adds the header to operations.
        /// </summary>
        /// <param name="info">The info.</param>
        /// <param name="headerMessage">The header message.</param>
        private void AddHeaderToOperations(CustomHeaderExportInfo info, Message headerMessage)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "AddHeaderToOperations");

            try
            {
                // qualified name
                XmlQualifiedName qsName = new XmlQualifiedName(headerMessage.Name, headerMessage.ServiceDescription.TargetNamespace);

                // header added to each operations input and output
                foreach (OperationBinding binding in _context.WsdlBinding.Operations)
                {
                    this.ExportMessageHeaderBinding(info, binding.Input, qsName); // request
                    this.ExportMessageHeaderBinding(info, binding.Output, qsName);  // response
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        /// <summary>
        /// Exports the message header binding.
        /// </summary>
        /// <param name="info">The info.</param>
        /// <param name="messageBinding">The message binding.</param>
        /// <param name="qsName">Name of the qs.</param>
        /// <param name="isEncoded">if set to <c>true</c> [is encoded].</param>
        private void ExportMessageHeaderBinding(CustomHeaderExportInfo info, 
            MessageBinding messageBinding, XmlQualifiedName qsName)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "ExportMessageHeaderBinding");

            try
            {
                SoapHeaderBinding binding = new SoapHeaderBinding();
                binding.Part = info.PartName;
                binding.Message = qsName;
                binding.Use = (info.IsEncoded ? SoapBindingUse.Encoded : SoapBindingUse.Literal);

                messageBinding.Extensions.Add(binding);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }
    }
}
