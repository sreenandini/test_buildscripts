/* ================================================================================= 
 * Purpose		:	Wcf Message Context Base
 * File Name	:   WcfMessageContextBase.cs
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
using System.ServiceModel;
using System.Xml.Serialization;
using System.ServiceModel.Dispatcher;

namespace BMC.CoreLib.WcfHelper.Contracts
{
    /// <summary>
    /// Wcf Message Context Base
    /// </summary>
    [MessageContract(IsWrapped = false)]
    public class WcfMessageContextBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageContextBase"/> class.
        /// </summary>
        protected WcfMessageContextBase()
        {
            this.Exception = null;
        }        
        
        /// <summary>
        /// Gets the exceptions.
        /// </summary>
        /// <value>The exceptions.</value>
        [XmlIgnore]
        public WcfExceptionDetail Exception { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance has errors.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has errors; otherwise, <c>false</c>.
        /// </value>
        [XmlIgnore]
        public bool HasErrors
        {
            get { return (this.Exception != null); }
        }
    }
}
