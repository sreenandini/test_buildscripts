using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.EnterpriseBusiness.Entities;
using BMC.Common.LogManagement;
using BMC.Common.ExceptionManagement;
using BMC.EnterpriseDataAccess;

namespace BMC.EnterpriseBusiness.Business
{



    public class Default2business
    {
        private static Default2business _Default2;
        public Default2business() { }
        public static Default2business CreateInstance()
        {
            if (_Default2 == null)
                _Default2 = new Default2business();
            return _Default2;
        }

        EnterpriseDataContext dbContextdefault = null;


        public List<DefaultEntity> DefaultTermsgroupDefaultsResult(int SiteID)
        {
            List<DefaultEntity> objcoll = new List<DefaultEntity>();
            try
            {
                dbContextdefault = EnterpriseDataContextHelper.GetDataContext();
                var Termsgroupresult = dbContextdefault.GetSubCompanyDefault(SiteID);
                var TermsgroupDefaultsResults = Termsgroupresult.GetResult<EnterpriseDataContext.TermsgroupDefaults>();
               
                LogManager.WriteLog("Inside Get DefaultTermsgroupDefaultsResult", LogManager.enumLogLevel.Info);
                
                foreach (var entity in TermsgroupDefaultsResults)
                {
                    objcoll.Add(new DefaultEntity()
                    {
                        Terms_Group_ID = Convert.ToInt32(entity.Terms_group_id),
                        Terms_Group_Name=entity.Terms_group_Name
                        
                    });
                }
                LogManager.WriteLog("End of Get DefaultTermsgroupDefaultsResult", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return objcoll;

        }

        public List<DefaultEntity> AcessKeyDefaultResult(int SiteID)
        {
            List<DefaultEntity> obj22 = new List<DefaultEntity>();
            try
            {
                dbContextdefault = EnterpriseDataContextHelper.GetDataContext();
                var AcessKeyresult = dbContextdefault.GetSubCompanyDefault(SiteID);
                var AcessKeyDefaultResults = AcessKeyresult.GetResult<EnterpriseDataContext.AcessKeyDefaultResult>();
                LogManager.WriteLog("Inside Get AcessKeyDefaultResult", LogManager.enumLogLevel.Info);
               
                foreach (var entity in AcessKeyDefaultResults)
                {
                    obj22.Add(new DefaultEntity()
                    {
                        Access_Key_ID = entity.Access_Key_ID,
                        Access_Key_Name = entity.Access_Key_Name,

                    });
                }
                LogManager.WriteLog("End of Get AcessKeyDefaultResult", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return obj22;
        }

        public  List<DefaultEntity> RepresentitiveDefaultResult(int SiteID)
        {
            List<DefaultEntity> objcoll = new List<DefaultEntity>();
            LogManager.WriteLog("Inside Get  RepresentitiveDefaultResult", LogManager.enumLogLevel.Info);
            try
            {
                dbContextdefault = EnterpriseDataContextHelper.GetDataContext();
                var resultRepresentitive = dbContextdefault.GetSubCompanyDefault(SiteID);
                var RepresentitiveDefaultResults = resultRepresentitive.GetResult<EnterpriseDataContext.RepresentitiveDefaultResult>();
                foreach (var entity in RepresentitiveDefaultResults)
                {
                    objcoll.Add(new DefaultEntity()
                    {
                        Staff_ID = entity.Staff_ID,
                        Staff_First_Name = entity.Staff_First_Name,
                        Staff_Last_Name = entity.Staff_Last_Name
                    });
                }
                LogManager.WriteLog("End of Get  RepresentitiveDefaultResult", LogManager.enumLogLevel.Info);
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return objcoll;
        }
        /// <summary>
        /// Apply button click
        /// </summary>
        /// <param name="site_ID"></param>
        /// <returns></returns>
        public List<DefaultEntity> TermsgroupApply(int site_ID)
        {
            List<DefaultEntity> objcollTerms = new List<DefaultEntity>();
            try
            {
                dbContextdefault = EnterpriseDataContextHelper.GetDataContext();
                var TermsgroupApplyresult = dbContextdefault.DefaultScreenApply(site_ID);
                var TermsgroupApplyDefaultsResults = TermsgroupApplyresult.GetResult<EnterpriseDataContext.DefaultTerms_Group_IDResult>();

                LogManager.WriteLog("Inside Get DefaultTermsgroupDefaultsResult", LogManager.enumLogLevel.Info);
                

                foreach (var entity in TermsgroupApplyDefaultsResults)
                {
                    objcollTerms.Add(new DefaultEntity()
                    {
                        Terms_Group_ID = entity.Terms_Group_ID,
                    });
                }
                LogManager.WriteLog("End of Get DefaultTermsgroupDefaultsResult", LogManager.enumLogLevel.Info);
               
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return objcollTerms;
           
        }


                
        public List<DefaultEntity> AccesskeyApply(int site_ID)
        {
            List<DefaultEntity> objcollaccess = new List<DefaultEntity>();
            try
            {
                dbContextdefault = EnterpriseDataContextHelper.GetDataContext();
                var AccesskeyApplyresult = dbContextdefault.DefaultScreenApply(site_ID);
                var AccesskeyApplyDefaultsResults = AccesskeyApplyresult.GetResult<EnterpriseDataContext.DefaultAccess_Key_IDResult>();

                LogManager.WriteLog("Inside Get DefaultTermsgroupDefaultsResult", LogManager.enumLogLevel.Info);


                foreach (var entity in AccesskeyApplyDefaultsResults)
                {
                    objcollaccess.Add(new DefaultEntity()
                    {
                        Access_Key_ID = entity.Access_Key_ID,
                    });
                }
                LogManager.WriteLog("End of Get DefaultTermsgroupDefaultsResult", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
          return objcollaccess;
        }

        public List<DefaultEntity>RepresntitiveApply(int site_ID)
        {

            List<DefaultEntity> objcollrepresntitive = new List<DefaultEntity>();
            LogManager.WriteLog("Inside Get DefaultTermsgroupDefaultsResult", LogManager.enumLogLevel.Info);
            try
            {
                dbContextdefault = EnterpriseDataContextHelper.GetDataContext();
                var represntitiveApplyresult = dbContextdefault.DefaultScreenApply(site_ID);
                var represntitiveApplyDefaultsResults = represntitiveApplyresult.GetResult<EnterpriseDataContext.DefaultSub_CompanyStaff_IDResult>();
                foreach (var entity in represntitiveApplyDefaultsResults)
                {
                    objcollrepresntitive.Add(new DefaultEntity()
                    {
                        Staff_ID = entity.DStaff_ID,
                    });
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            LogManager.WriteLog("End of Get DefaultTermsgroupDefaultsResult", LogManager.enumLogLevel.Info);
            return objcollrepresntitive;
        }
        public List<DefaultEntity> GetTermsgroupidname()
        {
            List<DefaultEntity> objcoll = new List<DefaultEntity>();
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    foreach (var entity in DataContext.rsp_GetTermsGroup())
                    {
                        objcoll.Add(new DefaultEntity()
                        {

                            Terms_Group_ID = entity.Terms_Group_ID,
                            Terms_Group_Name = entity.Terms_Group_Name

                        });
                    }
                }
            }
                catch(Exception ex)
                {
                    ExceptionManager.Publish(ex);
                }
                LogManager.WriteLog("End of Get DefaultTermsgroupDefaultsResult", LogManager.enumLogLevel.Info);
                return objcoll;

            }
        
        public List<DefaultEntity> GetAccesskeyResult()
        {
            List<DefaultEntity> objcoll = new List<DefaultEntity>();
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    foreach (var entity in DataContext.rsp_GetAccesskey())
                    {
                        objcoll.Add(new DefaultEntity()
                        {

                            Access_Key_ID = entity.Access_Key_ID,
                            Access_Key_Name = entity.Access_Key_Name,
                            Access_Key_Ref = entity.Access_Key_Ref,
                            Access_Key_Manufacturer = entity.Access_Key_Manufacturer,
                            Access_Key_Type = entity.Access_Key_Type

                        });
                    }
                }
            }
            catch(Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
                LogManager.WriteLog("End of Get DefaultTermsgroupDefaultsResult", LogManager.enumLogLevel.Info);
                return objcoll;
        }
        public List<DefaultEntity> GetRepresentativecheck()
        {
            List<DefaultEntity> objcolRep = new List<DefaultEntity>();
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    foreach (var entity in DataContext.GetRepresentativecheck())
                    {
                        objcolRep.Add(new DefaultEntity()
                        {

                            Staff_ID = entity.Staff_ID,
                            Staff_First_Name = entity.Staff_First_Name,
                            Staff_Last_Name = entity.Staff_Last_Name

                        });
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
                LogManager.WriteLog("End of Get DefaultTermsgroupDefaultsResult", LogManager.enumLogLevel.Info);
                return objcolRep;

        }
        public bool CascadeSiteUpdateTermsGroup(System.Nullable<int> siteID, System.Nullable<int> theValue, System.Nullable<int> CascadeType, System.Nullable<bool> BeingSetAsDefault)
        {
            try
            {
                
                //bool  Response ;
                //Response = true;
                string DefaultFieldName;

                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                                        
                    DataContext.UpdateTermsGroup(siteID,theValue,BeingSetAsDefault);
                    DefaultFieldName = BeingSetAsDefault.ToString();

                    LogManager.WriteLog("End of Get DefaultTermsgroupDefaultsResult", LogManager.enumLogLevel.Info);
                }
               
                return false; 
               
            }
            catch(Exception ex)
            {                             
                ExceptionManager.Publish(ex);
                return true;
            }
            
        }

        public bool CascadeSiteUpdateAccessKey(System.Nullable<int> siteID, System.Nullable<int> theValue, System.Nullable<int> CascadeType, System.Nullable<bool> BeingSetAsDefault)
        {
            try
            {

                //bool Response;
                //Response = true;
                string DefaultFieldName;

                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {

                    DataContext.UpdateAccesskey(siteID, theValue, BeingSetAsDefault);
                    DefaultFieldName = BeingSetAsDefault.ToString();
                    LogManager.WriteLog("End of Get DefaultTermsgroupDefaultsResult", LogManager.enumLogLevel.Info);
                }
                
              
                return false;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return true;
            }

        }
        public bool CascadeSiteUpdateResntitive(System.Nullable<int> siteID, System.Nullable<int> theValue, System.Nullable<int> CascadeType, System.Nullable<bool> BeingSetAsDefault)
        {
            try
            {

                //bool Response;
                //Response = true;
                string DefaultFieldName;

                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {

                    DataContext.UpdateRepresentitive(siteID, theValue, BeingSetAsDefault);
                    DefaultFieldName = BeingSetAsDefault.ToString();

                    LogManager.WriteLog("End of Get DefaultTermsgroupDefaultsResult", LogManager.enumLogLevel.Info);
                }


                return false;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return true;
            }

        }
          
    }
    }






