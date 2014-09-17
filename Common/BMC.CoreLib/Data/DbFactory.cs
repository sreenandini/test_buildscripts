/* ================================================================================= 
 * Purpose		:	Database Factory Class
 * File Name	:   DbFactory.cs
 * Author		:	A.Vinod Kumar
 * Created  	:	21/12/2010
 * ================================================================================= 
 * Revision History :
 * ================================================================================= 
 * 21/12/2010		A.Vinod Kumar    Initial Version
 * ===============================================================================*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace BMC.CoreLib.Data {
    public static class DbFactory {
        /// <summary>
        /// Initializes the <see cref="DbFactory"/> class.
        /// </summary>
        static DbFactory() { }

        /// <summary>
        /// Opens the DB.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <returns>Opens the database.</returns>
        public static Database OpenDB(string connectionString) {
            Database db = new SqlDatabase(connectionString);
            db.Open();
            return db;
        }
    }
}
