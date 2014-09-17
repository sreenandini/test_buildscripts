using BMC.EnterpriseBusiness.Entities;
using BMC.EnterpriseDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.EnterpriseBusiness.Business
{
    public class ShareScheduleBusiness
    {
        public int AddOrUpdateShareSchedule(int shareScheduleId,string ShareScheduleName, string ShareScheduleDescription, int? No_of_Schedule_Bands, string ShareScheduleBandType,ref  int? ShareScheduleIdOut)
        {
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                return DataContext.AddOrUpdateShareSchedule(shareScheduleId, ShareScheduleName, ShareScheduleDescription, No_of_Schedule_Bands, ShareScheduleBandType,ref ShareScheduleIdOut);
            }
        }

        public int AddOrUpdateShareBand(ShareScheduleEntity Entity)
        {
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                return DataContext.AddOrUpdateShareBand(Entity.Share_Schedule_Name,
                                                        Entity.Share_Band_ID,
                                                        Entity.Share_Schedule_ID,
                                                        Entity.Share_Band_Name,
                                                        Entity.Share_Band_Start_Date,
                                                        Entity.Share_Band_End_Date,
                                                        Entity.Share_Band_Description,
                                                        Entity.Share_Band_Supplier_Share,
                                                        Entity.Share_Band_Site_Share,
                                                        Entity.Share_Band_Company_Share,
                                                        Entity.Share_Band_Sec_Company_Share,
                                                        Entity.Share_Band_Future_Supplier_Share,
                                                        Entity.Share_Band_Future_Site_Share,
                                                        Entity.Share_Band_Future_Company_Share,
                                                        Entity.Share_Band_Future_Sec_Company_Share,
                                                        Entity.Share_Band_Future_Start_Date,
                                                        Entity.Share_Band_Past_Supplier_Share,
                                                        Entity.Share_Band_Past_Site_Share,
                                                        Entity.Share_Band_Past_Company_Share,
                                                        Entity.Share_Band_Past_Sec_Company_Share,
                                                        Entity.Share_Band_Past_End_Date,
                                                        Entity.Share_Band_Supplier_Rent,
                                                        Entity.Share_Band_Future_Supplier_Rent,
                                                        Entity.Share_Band_Past_Supplier_Rent,
                                                        Entity.Share_Band_Supplier_Rent_Guaranteed,
                                                        Entity.Share_Band_Future_Supplier_Rent_Guaranteed,
                                                        Entity.Share_Band_Past_Supplier_Rent_Guaranteed,
                                                        Entity.Share_Band_Supplier_Share_Guaranteed,
                                                        Entity.Share_Band_Future_Supplier_Share_Guaranteed,
                                                        Entity.Share_Band_Past_Supplier_Share_Guaranteed,
                                                        Entity.Share_Band_Company_Share_Guaranteed,
                                                        Entity.Share_Band_Future_Company_Share_Guaranteed,
                                                        Entity.Share_Band_Past_Company_Share_Guaranteed,
                                                        Entity.Share_Band_Site_Share_Guaranteed,
                                                        Entity.Share_Band_Future_Site_Share_Guaranteed,
                                                        Entity.Share_Band_Past_Site_Share_Guaranteed,
                                                        Entity.Share_Band_Sec_Company_Share_Guaranteed,
                                                        Entity.Share_Band_Future_Sec_Company_Share_Guaranteed,
                                                        Entity.Share_Band_Past_Sec_Company_Share_Guaranteed
                                                        );
            }
        }

        public int AddOrUpdateMachineClassShareBand(ShareScheduleEntity Entity)
        {
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                return DataContext.AddOrUpdateMachineClassShareBand(Entity.Machine_Class_Share_Band,
                                                                    Entity.Machine_Class_ID,
                                                                    Entity.Share_Band_ID_Present,
                                                                    Entity.Share_Band_ID_Future,
                                                                    Entity.Machine_Class_Share_Future_Date,
                                                                    Entity.Share_Band_ID_Past,
                                                                    Entity.Machine_Class_Share_Past_Date
                                                                    );
            }
        }

        public List<ShareScheduleEntity> GetShareScheduleDetails(int shareScheduleId=0)
        {
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                List<ShareScheduleEntity> result = new List<ShareScheduleEntity>();
                List<rsp_GetShareScheduleResult> dsShareSchedule = DataContext.GetShareScheduleDetails(shareScheduleId).ToList();
                foreach (rsp_GetShareScheduleResult ShareSchedule in dsShareSchedule)
                {
                    result.Add(new ShareScheduleEntity()
                    {
                        Share_Schedule_ID = ShareSchedule.Share_Schedule_ID,
                        Share_Schedule_Name = ShareSchedule.Share_Schedule_Name,
                        Share_Schedule_Start_Date = ShareSchedule.Share_Schedule_Start_Date,
                        Share_Schedule_End_Date = ShareSchedule.Share_Schedule_End_Date,
                        Share_Schedule_Description = ShareSchedule.Share_Schedule_Description,
                        Share_Schedule_No_Bands = ShareSchedule.Share_Schedule_No_Bands,
                        Share_Schedule_Bands_Name_Type = ShareSchedule.Share_Schedule_Bands_Name_Type,
                        Share_Machine_Change_Date = ShareSchedule.Share_Machine_Change_Date
                    });
                }
                return result;
            }
        }

        public List<ShareScheduleEntity> GetShareBandDetails(int shareScheduleId)
        {
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                List<ShareScheduleEntity> result = new List<ShareScheduleEntity>();
                List<rsp_GetShareBandResult> dsShareBand = DataContext.GetShareBandDetails(shareScheduleId).ToList();
                foreach (rsp_GetShareBandResult ShareBand in dsShareBand)
                {
                    result.Add(new ShareScheduleEntity()

                    {
                        Share_Band_ID = ShareBand.Share_Band_ID,
                        Share_Schedule_ID = ShareBand.Share_Schedule_ID,
                        Share_Band_Name = ShareBand.Share_Band_Name,
                        Share_Band_Start_Date = ShareBand.Share_Band_Start_Date,
                        Share_Band_End_Date = ShareBand.Share_Band_End_Date,
                        Share_Band_Description = ShareBand.Share_Band_Description,
                        Share_Band_Supplier_Share = ShareBand.Share_Band_Supplier_Share,
                        Share_Band_Site_Share = ShareBand.Share_Band_Site_Share,
                        Share_Band_Company_Share = ShareBand.Share_Band_Company_Share,
                        Share_Band_Sec_Company_Share = ShareBand.Share_Band_Sec_Company_Share,
                        Share_Band_Future_Supplier_Share = ShareBand.Share_Band_Future_Supplier_Share,
                        Share_Band_Future_Site_Share = ShareBand.Share_Band_Future_Site_Share,
                        Share_Band_Future_Company_Share = ShareBand.Share_Band_Future_Company_Share,
                        Share_Band_Future_Sec_Company_Share = ShareBand.Share_Band_Future_Sec_Company_Share,
                        Share_Band_Future_Start_Date = ShareBand.Share_Band_Future_Start_Date,
                        Share_Band_Past_Supplier_Share = ShareBand.Share_Band_Past_Supplier_Share,
                        Share_Band_Past_Site_Share = ShareBand.Share_Band_Past_Site_Share,
                        Share_Band_Past_Company_Share = ShareBand.Share_Band_Past_Company_Share,
                        Share_Band_Past_Sec_Company_Share = ShareBand.Share_Band_Past_Sec_Company_Share,
                        Share_Band_Past_End_Date = ShareBand.Share_Band_Past_End_Date,
                        Share_Band_Supplier_Rent = ShareBand.Share_Band_Supplier_Rent,
                        Share_Band_Future_Supplier_Rent = ShareBand.Share_Band_Future_Supplier_Rent,
                        Share_Band_Past_Supplier_Rent = ShareBand.Share_Band_Past_Supplier_Rent,
                        Share_Band_Supplier_Rent_Guaranteed = ShareBand.Share_Band_Supplier_Rent_Guaranteed,
                        Share_Band_Future_Supplier_Rent_Guaranteed = ShareBand.Share_Band_Future_Supplier_Rent_Guaranteed,
                        Share_Band_Past_Supplier_Rent_Guaranteed = ShareBand.Share_Band_Past_Supplier_Rent_Guaranteed,
                        Share_Band_Supplier_Share_Guaranteed = ShareBand.Share_Band_Supplier_Share_Guaranteed,
                        Share_Band_Future_Supplier_Share_Guaranteed = ShareBand.Share_Band_Future_Supplier_Share_Guaranteed,
                        Share_Band_Past_Supplier_Share_Guaranteed = ShareBand.Share_Band_Past_Supplier_Share_Guaranteed,
                        Share_Band_Company_Share_Guaranteed = ShareBand.Share_Band_Company_Share_Guaranteed,
                        Share_Band_Future_Company_Share_Guaranteed = ShareBand.Share_Band_Future_Company_Share_Guaranteed,
                        Share_Band_Past_Company_Share_Guaranteed = ShareBand.Share_Band_Past_Company_Share_Guaranteed,
                        Share_Band_Site_Share_Guaranteed = ShareBand.Share_Band_Site_Share_Guaranteed,
                        Share_Band_Future_Site_Share_Guaranteed = ShareBand.Share_Band_Future_Site_Share_Guaranteed,
                        Share_Band_Past_Site_Share_Guaranteed = ShareBand.Share_Band_Past_Site_Share_Guaranteed,
                        Share_Band_Sec_Company_Share_Guaranteed = ShareBand.Share_Band_Sec_Company_Share_Guaranteed,
                        Share_Band_Future_Sec_Company_Share_Guaranteed = ShareBand.Share_Band_Future_Sec_Company_Share_Guaranteed,
                        Share_Band_Past_Sec_Company_Share_Guaranteed = ShareBand.Share_Band_Past_Sec_Company_Share_Guaranteed
                    });
                }
                return result;
            }
        }

        public List<ShareScheduleEntity> GetMachineClassShareBand(int shareScheduleId)
        {
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                List<ShareScheduleEntity> result = new List<ShareScheduleEntity>();
                List<rsp_GetMachineClassShareBandResult> dsMachineClassShareBand = DataContext.GetMachineClassShareBand(shareScheduleId).ToList();
                foreach (rsp_GetMachineClassShareBandResult MachineClassShareBand in dsMachineClassShareBand)
                {
                    result.Add(new ShareScheduleEntity()

                    {
                        Machine_Class_ID = MachineClassShareBand.Machine_Class_ID,
                        Machine_Name = MachineClassShareBand.Machine_Name,
                        Machine_BACTA_Code = MachineClassShareBand.Machine_BACTA_Code,
                        Machine_Class_Share_Band = MachineClassShareBand.Machine_Class_Share_Band,
                        PastBandName = MachineClassShareBand.PastBandName,
                        BandName = MachineClassShareBand.BandName,
                        FutureBandName = MachineClassShareBand.FutureBandName,
                        Machine_Class_Share_Future_Date = MachineClassShareBand.Machine_Class_Share_Future_Date,
                        Machine_Class_Share_Past_Date = MachineClassShareBand.Machine_Class_Share_Past_Date,
                        Share_Band_ID_Present = MachineClassShareBand.Share_Band_ID,
                        Share_Band_ID_Past = MachineClassShareBand.Share_Band_ID_Past,
                        Share_Band_ID_Future = MachineClassShareBand.Share_Band_ID_Future
                    });
                }
                return result;
            }
        }

        public List<ShareScheduleEntity> GetMachineClass(string SearchCriteria)
        {
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                List<ShareScheduleEntity> result = new List<ShareScheduleEntity>();
                List<rsp_GetMachineClassResult> dsMachineClass = DataContext.GetMachineClass(SearchCriteria).ToList();
                foreach (rsp_GetMachineClassResult MachineClass in dsMachineClass)
                {
                    result.Add(new ShareScheduleEntity()

                    {
                        Machine_Class_ID = MachineClass.Machine_Class_ID,
                        Machine_Name = MachineClass.Machine_Type_Code + "-" + MachineClass.Machine_Name + (MachineClass.Machine_BACTA_Code != null ? "[" + MachineClass.Machine_BACTA_Code + "]" : string.Empty),
                        Machine_BACTA_Code = MachineClass.Machine_Name + ( MachineClass.Machine_BACTA_Code!=null?"[" + MachineClass.Machine_BACTA_Code + "]":string.Empty)
                       

                    });
                }
                return result;
            }
        }

        public int DeleteMachineClassShareBand(int MachineClassShareBand)
        {
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                return DataContext.DeleteMachineClassShareBand(MachineClassShareBand);
            }
        }
    }
}
