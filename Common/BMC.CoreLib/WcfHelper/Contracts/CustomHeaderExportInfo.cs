/* ================================================================================= 
 * Purpose		:	Custom Export Header Information
 * File Name	:   CustomHeaderExportInfo.cs
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
using System.Xml.Schema;

namespace BMC.CoreLib.WcfHelper.Contracts
{
    /// <summary>
    /// Custom Export Header Information
    /// </summary>
    public class CustomHeaderExportInfo : DisposableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomHeaderExportInfo"/> class.
        /// </summary>
        public CustomHeaderExportInfo() { }

        /// <summary>
        /// Gets or sets the export schema.
        /// </summary>
        /// <value>The export schema.</value>
        public XmlSchema ExportSchema { get; set; }

        /// <summary>
        /// Gets or sets the schema namespace.
        /// </summary>
        /// <value>The schema namespace.</value>
        public string SchemaNamespace { get; set; }

        /// <summary>
        /// Gets or sets the name of the schema.
        /// </summary>
        /// <value>The name of the schema.</value>
        public string SchemaName { get; set; }

        /// <summary>
        /// Gets or sets the export namespace.
        /// </summary>
        /// <value>The export namespace.</value>
        public string ExportNamespace { get; set; }

        /// <summary>
        /// Gets or sets the name of the export.
        /// </summary>
        /// <value>The name of the export.</value>
        public string ExportName { get; set; }

        /// <summary>
        /// Gets or sets the name of the part.
        /// </summary>
        /// <value>The name of the part.</value>
        public string PartName { get; set; }

        /// <summary>
        /// Gets or sets the namespace prefix.
        /// </summary>
        /// <value>The namespace prefix.</value>
        public string NamespacePrefix { get; set; }

        /// <summary>
        /// Gets or sets the is encoded.
        /// </summary>
        /// <value>The is encoded.</value>
        public bool IsEncoded { get; set; }
    }
}
