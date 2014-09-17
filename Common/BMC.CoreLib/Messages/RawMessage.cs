using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using BMC.CoreLib;
using BMC.CoreLib.Xml;

namespace BMC.CoreLib.Messages
{
    /// <summary>
    /// Raw Message Interface
    /// </summary>
    public interface IRawMessage
    {
        /// <summary>
        /// Gets the raw message.
        /// </summary>
        /// <value>The raw message.</value>
        string RawMessageData { get; }
    }

    /// <summary>
    /// Abstract Raw Message
    /// </summary>
    public abstract class RawMessage : DisposableObject, IRawMessage
    {
        private string _rawMessage = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="RawMessageData"/> class.
        /// </summary>
        /// <param name="rawMesage">The raw mesage.</param>
        protected RawMessage(string rawMesage)
        {
            _rawMessage = rawMesage;
        }

        #region IRawMessage Members

        /// <summary>
        /// Gets the raw message.
        /// </summary>
        /// <value>The raw message.</value>
        public string RawMessageData
        {
            get
            {
                if (!_rawMessage.IsEmpty())
                {
                    return _rawMessage;
                }
                else
                {
                    return this.RawMessageDataInternal(null);
                }
            }
        }

        /// <summary>
        /// Gets the raw message internal.
        /// </summary>
        /// <value>The raw message internal.</value>
        internal virtual string RawMessageDataInternal(XmlWriterHelper helper)
        {
            return null;
        }

        #endregion

        /// <summary>
        /// Overridable method which releases the managed resources.
        /// </summary>
        protected override void DisposeManaged()
        {
            base.DisposeManaged();
            _rawMessage = null;
        }
    }
}
