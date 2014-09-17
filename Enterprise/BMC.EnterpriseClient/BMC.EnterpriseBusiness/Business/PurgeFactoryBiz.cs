using BMC.Common.ExceptionManagement;
using BMC.Common.Utilities;
using BMC.EnterpriseBusiness.Entities;
using BMC.EnterpriseDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.EnterpriseBusiness.Business
{
    public class PurgeFactoryBiz : IPurgeFactory
    {
        public static PurgeFactoryBiz _purgeBiz = null;

        /// <summary>
        /// create a single instance of the class.
        /// </summary>
        /// <returns></returns>
        public static PurgeFactoryBiz CreateInstance()
        {
            if (_purgeBiz == null)
            {
                _purgeBiz = new PurgeFactoryBiz();
            }
            return _purgeBiz;
        }

        /// <summary>
        /// Save teh purge category details.
        /// </summary>
        /// <param name="purgeItems"></param>
        /// <returns></returns>
        public bool SavePurgeCategoryDetails(List<PurgeCategory> purgeItems)
        {
            bool bResult = false;

            try
            {
                XMLUtilities utitlity = new XMLUtilities("PurgeRoot");
                string sXML = utitlity.CreateXML(purgeItems);

                using (EnterpriseDataContext context = new EnterpriseDataContext(DatabaseHelper.GetConnectionString()))
                    context.InsertPurgeCategories(sXML);
                bResult = true;

            }
            catch (Exception ex) { ExceptionManager.Publish(ex); bResult = false; }
            return bResult;
        }

        /// <summary>
        /// Load the tables list.
        /// </summary>
        /// <returns></returns>
        public List<PurgeTables> LoadPurgeTablesList()
        {
            List<PurgeTables> tables = new List<PurgeTables>();
            try
            {
                using (EnterpriseDataContext context = new EnterpriseDataContext(DatabaseHelper.GetConnectionString()))
                {
                    context.GetPurgeTableList().ToList().ForEach(item => tables.Add(new PurgeTables() 
                    { PurgeTableID = item.PT_Id, Purgetablename = item.Tablename, TableDisplayName = item.Alias }));
                }
            }
            catch (Exception ex) { ExceptionManager.Publish(ex); }

            return tables;
        }
    }

    public interface IPurgeFactory
    {
        bool SavePurgeCategoryDetails(List<PurgeCategory> purgeItems);
        List<PurgeTables> LoadPurgeTablesList();
    }
}
