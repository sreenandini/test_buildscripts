/* ================================================================================= 
 * Purpose		:	Object base class with Logger support.
 * File Name	:   DisposableObjectLogger.cs
 * Author		:	A.Vinod Kumar
 * Created  	:	22/12/2010
 * ================================================================================= 
 * Revision History :
 * ================================================================================= 
 * 22/12/2010		A.Vinod Kumar    Initial Version
 * ===============================================================================*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib.Diagnostics;
using System.Diagnostics;
using System.Reflection;
using System.ComponentModel;
using System.Xml.Serialization;

namespace BMC.CoreLib.Diagnostics
{
    /// <summary>
    /// Object base class with Logger support.
    /// </summary>
#if !SILVERLIGHT
    [Serializable]
#endif
    public class DisposableObjectLogger
        : DisposableObjectNotify
    {
        #region Variables
        [NonSerialized]
        private ILogger _logger = null;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="DisposableObjectLogger"/> class.
        /// </summary>
        public DisposableObjectLogger()
        {
            this.Logger = SharedData.ActiveLogger;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DisposableObjectLogger"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public DisposableObjectLogger(ILogger logger)
        {
            this.Logger = logger;
        }
        #endregion

        #region Logger
        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        /// <value>The logger.</value>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [XmlIgnore]
        public ILogger Logger
        {
            get { return _logger; }
            set { _logger = value; }
        }
        #endregion
    }
}
