// Developed by A.Vinod Kumar
// Initial Release on : 18/10/2010

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.CoreLib.Win32
{
    /// <summary>
    /// IUserInterface class
    /// </summary>
    public interface IUserInterface
    {
        /// <summary>
        /// Loads the ui components.
        /// </summary>
        void LoadUI();

        /// <summary>
        /// Saves the ui components.
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if succeeded; otherwise, <c>false</c>.
        /// </returns>
        bool SaveUI();

        /// <summary>
        /// Validates ui components.
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if succeeded; otherwise, <c>false</c>.
        /// </returns>
        bool ValidateUI();
    }
}
