/* ================================================================================= 
 * Purpose		:	Wcf Exception Detail
 * File Name	:   WcfExceptionDetail.cs
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
using System.Globalization;
using System.Runtime.Serialization;

namespace BMC.CoreLib.WcfHelper.Contracts
{
    /// <summary>
    /// Wcf Exception Detail
    /// </summary>
    [DataContract()]
    public class WcfExceptionDetail
    {
        #region Constants
        public const string NS = "BMC.CoreLib.WcfHelper.Contracts";
        public const string NAME = "WcfExceptionDetail";
        public const string ACTION = NS + "." + NAME + "Fault";
        #endregion

        #region Variables
        private string _helpLink = string.Empty;
        private WcfExceptionDetail _innerException = null;
        private string _message = string.Empty;
        private string _stackTrace = string.Empty;
        private string _type = string.Empty;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="WcfExceptionDetail"/> class.
        /// </summary>
        public WcfExceptionDetail() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="WcfExceptionDetail"/> class.
        /// </summary>
        /// <param name="exception">The exception.</param>
        public WcfExceptionDetail(Exception exception)
        {
            if (exception == null)
            {
                throw new ArgumentNullException("exception");
            }
            this._helpLink = exception.HelpLink;
            this._message = exception.Message;
            this._stackTrace = exception.StackTrace;
            this._type = exception.GetType().ToString();
            if (exception.InnerException != null)
            {
                this._innerException = new WcfExceptionDetail(exception.InnerException);
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}\n{1}",
                new object[] { "An WcfExceptionDetail, likely created by [IncludeExceptionDetailInFaults = true], whose value is : ",
                    this.ToStringHelper(false) });
        }

        /// <summary>
        /// Toes the string helper.
        /// </summary>
        /// <param name="isInner">if set to <c>true</c> [is inner].</param>
        /// <returns></returns>
        private string ToStringHelper(bool isInner)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("{0}: {1}", this.Type, this.Message);
            if (this.InnerException != null)
            {
                builder.AppendFormat(" ----> {0}", this.InnerException.ToStringHelper(true));
            }
            else
            {
                builder.Append("\n");
            }
            builder.Append(this.StackTrace);
            if (isInner)
            {
                builder.AppendFormat("\n   {0}\n",
                    "--- End of inner WcfExceptionDetail stack trace ---");
            }
            return builder.ToString();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the help link.
        /// </summary>
        /// <value>The help link.</value>
        [DataMember]
        public string HelpLink
        {
            get
            {
                return this._helpLink;
            }
            private set
            {
                this._helpLink = value;
            }
        }

        /// <summary>
        /// Gets or sets the inner exception.
        /// </summary>
        /// <value>The inner exception.</value>
        [DataMember]
        public WcfExceptionDetail InnerException
        {
            get
            {
                return this._innerException;
            }
            private set
            {
                this._innerException = value;
            }
        }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        [DataMember]
        public string Message
        {
            get
            {
                return this._message;
            }
            private set
            {
                this._message = value;
            }
        }

        /// <summary>
        /// Gets or sets the stack trace.
        /// </summary>
        /// <value>The stack trace.</value>
        [DataMember]
        public string StackTrace
        {
            get
            {
                return this._stackTrace;
            }
            private set
            {
                this._stackTrace = value;
            }
        }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        [DataMember]
        public string Type
        {
            get
            {
                return this._type;
            }
            private set
            {
                this._type = value;
            }
        }
        #endregion
    }
}
