using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Channels;

namespace WCFHelperUtils
{
    /// <summary>
    /// WCFMethodMessage
    /// </summary>
    public sealed class WCFMethodMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WCFMethodMessage"/> class.
        /// </summary>
        /// <param name="methodName">Name of the method.</param>
        public WCFMethodMessage(string methodName)
        {
            this.MethodName = methodName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WCFMethodMessage"/> class.
        /// </summary>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="input">The input.</param>
        /// <param name="output">The output.</param>
        public WCFMethodMessage(string methodName, string input, string output)
        {
            this.MethodName = methodName;
            this.Input = input;
            this.Output = output;
        }

        /// <summary>
        /// Gets or sets the action.
        /// </summary>
        /// <value>The action.</value>
        public string Action { get; set; }

        /// <summary>
        /// Gets or sets the name of the method.
        /// </summary>
        /// <value>The name of the method.</value>
        public string MethodName { get; set; }

        /// <summary>
        /// Gets or sets the input.
        /// </summary>
        /// <value>The input.</value>
        public string Input { get; set; }

        /// <summary>
        /// Gets or sets the output.
        /// </summary>
        /// <value>The output.</value>
        public string Output { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is message contract.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is message contract; otherwise, <c>false</c>.
        /// </value>
        public bool IsMessageContract { get; set; }
    }
}
