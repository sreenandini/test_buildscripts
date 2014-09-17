/* ================================================================================= 
 * Purpose		:	Query Interface
 * File Name	:   IQueryInterface.cs
 * Author		:	A.Vinod Kumar
 * Created  	:	28/11/2011
 * ================================================================================= 
 * Revision History :
 * ================================================================================= 
 * 28/11/2011		A.Vinod Kumar    Initial Version
 * ===============================================================================*/
using System;

namespace BMC.CoreLib
{
    /// <summary>
    /// Query Interface
    /// </summary>
    public interface IQueryInterface : IDisposable
    {
        #region Methods and Properties
        /// <summary>
        /// Queries the interface.
        /// </summary>
        /// <typeparam name="T">Type of the interface.</typeparam>
        /// <returns>Interface Implementation.</returns>
        T QueryInterface<T>()
            where T : class, IQueryInterface; 
        #endregion
    }
}
