using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.EnterpriseBusiness.Entities;
using BMC.EnterpriseDataAccess;
using BMC.Common.LogManagement;
using System.Xml.Linq;
using BMC.Common.ExceptionManagement;

namespace BMC.EnterpriseBusiness.Business
{
    public class ownerBusiness
    {

        private static ownerBusiness _owner;

        public ownerBusiness() { }

        public static ownerBusiness CreateInstance()
        {
            if (_owner == null)
                _owner = new ownerBusiness();
            return _owner;
        }

        public List<ownerEntity> owner_Loadcompany()
        {
            try
            {
                LogManager.WriteLog("Inside owner_Loadcompany", LogManager.enumLogLevel.Info);
                List<ownerEntity> objcoll = new List<ownerEntity>();
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {

                    foreach (var entity in DataContext.GetCompanyName())
                    {
                        objcoll.Add(new ownerEntity()
                        {
                           company_name=entity.Company_Name,
                           Company_ID=entity.Company_ID
                          

                        });
                    }
                    LogManager.WriteLog("End of owner_Loadcompany" , LogManager.enumLogLevel.Info);
                    return objcoll;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                throw ex;
            }

        }
        public List<ownerEntity> owner_subcompany(int _companyID)
        {
            try
            {
                LogManager.WriteLog("Inside owner_subcompany", LogManager.enumLogLevel.Info);
                List<ownerEntity> objcoll = new List<ownerEntity>();

                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    foreach (var entity in DataContext.Getscompany(_companyID))
                    {
                        objcoll.Add(new ownerEntity()
                        {
                           company_subcompany=entity.Sub_Company_Name,
                           Sub_Company_ID=entity.Sub_Company_ID
                          
                                                      
                        });
                    }
                    LogManager.WriteLog("End of owner_subcompany" , LogManager.enumLogLevel.Info);
                    return objcoll;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                throw ex;
            }
        }


        public List<ownerEntity> owner_psubcompany(int subvalue)
        {
            try
            {
                LogManager.WriteLog("Inside owner_subcompany", LogManager.enumLogLevel.Info);
                List<ownerEntity> objcoll = new List<ownerEntity>();

                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    foreach (var entity in DataContext.Getscompany(subvalue))
                    {
                        objcoll.Add(new ownerEntity()
                        {
                            company_subcompany = entity.Sub_Company_Name,
                            Sub_Company_ID=entity.Sub_Company_ID
                            
                        });
                    }
                    LogManager.WriteLog("End of owner_subcompany", LogManager.enumLogLevel.Info);
                    return objcoll;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                throw ex;
            }
        }

        public List<ownerEntity> owner_nsubcompany(int subvalue)
            {
            try
            {
                LogManager.WriteLog("Inside owner_subcompany", LogManager.enumLogLevel.Info);
                List<ownerEntity> objcoll = new List<ownerEntity>();

                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    foreach (var entity in DataContext.Getscompany(subvalue))
                    {
                        objcoll.Add(new ownerEntity()
                        {
                           company_subcompany=entity.Sub_Company_Name,
                           Sub_Company_ID=entity.Sub_Company_ID
                                                                                
                        });
                    }
                    LogManager.WriteLog("End of owner_subcompany" , LogManager.enumLogLevel.Info);
                    return objcoll;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                throw ex;
            }


        }
        //public List<ownerEntity> sendSubcompanyvalue(int Subcompanyvalue)
        //   {
        //       try
        //       {
        //           LogManager.WriteLog("Inside owner_subcompany", LogManager.enumLogLevel.Info);
        //           List<ownerEntity> objcoll = new List<ownerEntity>();
        //           using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
        //           {
                       //foreach (var entity in DataContext.SendSubCompanyValue(Subcompanyvalue))
        //               {
        //                   objcoll.Add(new ownerEntity()
        //                   {
        //              Sub_Company_Region_Name=entity.Sub_Company_Region_Name,
        //              Staff_ID=entity.Staff_ID,
        //              Staff_Last_Name=entity.Staff_Last_Name,
        //              Staff_First_Name=entity.Staff_First_Name
        //                   });
        //               }
        //               LogManager.WriteLog("End of owner_subcompany", LogManager.enumLogLevel.Info);
        //               return objcoll;
        //           }
        //       }
        //       catch (Exception ex)
        //       {
        //           ExceptionManager.Publish(ex);
        //           throw ex;
        //       }
        //   }
    

         public List<ownerEntity> GetCustomerAccesstoCompany(int staffId)
         {
             try
             {
                 LogManager.WriteLog("Inside owner_subcompany", LogManager.enumLogLevel.Info);
                 List<ownerEntity> objcoll = new List<ownerEntity>();
                 using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                 {
                     foreach (var entity in DataContext.GetCompanyAccesstoCustomer(staffId))
                     {
                         objcoll.Add(new ownerEntity()
                         {
                           Company_ID = entity.Company_ID,
                           Company_Name=entity.Company_Name
                           
                         });
                     }
                     LogManager.WriteLog("End of owner_subcompany", LogManager.enumLogLevel.Info);
                     return objcoll;
                 }
             }
             catch (Exception ex)
             {
                 ExceptionManager.Publish(ex);
                 throw ex;
             }


         }
        
         public List<ownerEntity>GetCustomerAccessSubCompany(int CompanyID,int StaffId)
         {
             try
             {
                 LogManager.WriteLog("Inside GetCustomerAccessSubCompany", LogManager.enumLogLevel.Info);
                 List<ownerEntity> objcoll = new List<ownerEntity>();

                 using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                 {
                     foreach (var entity in DataContext.GetCustomerAccessSubCompany(CompanyID,StaffId))
                     {
                         objcoll.Add(new ownerEntity()
                         {
                             Sub_Company_ID = entity.Sub_Company_ID,
                             Sub_Company_Name = entity.Sub_Company_Name,

                         });
                     }
                     LogManager.WriteLog("End of GetCustomerAccessSubCompany", LogManager.enumLogLevel.Info);
                     return objcoll;
                 }
             }
             catch (Exception ex)
             {
                 ExceptionManager.Publish(ex);
                 throw ex;
            }
         }
    }

 }
 


