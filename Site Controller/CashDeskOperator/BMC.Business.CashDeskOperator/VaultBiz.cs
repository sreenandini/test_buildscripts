using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.DBInterface.CashDeskOperator;
using System.Data;
using System.Data.SqlClient;
using BMC.Transport;
using System.Windows.Media;

namespace BMC.Business.CashDeskOperator
{

    public class VaultBiz
    {
        VaultDataAccess objVaultDataAccess = new VaultDataAccess(new SqlConnection(CommonUtilities.ExchangeConnectionString));
        public DataTable GetVaultDevices()
        {
            return objVaultDataAccess.GetVaultDevices();
        }
        public DataTable FillVault(int Device_ID, decimal Amount, decimal CurrentBalance, int iWitDrawl, string Type, int Reason_id, ref int iResult, bool sendAlert)
        {
            return objVaultDataAccess.FillVault(Device_ID, Amount, CurrentBalance, iWitDrawl, Type, Reason_id, ref iResult, sendAlert);
        }
        public DataTable GetFillHistory(int Device_ID, int No_Of_Records, int TransTypeID)
        {
            return objVaultDataAccess.GetFillHistory(Device_ID, No_Of_Records, TransTypeID);
        }

        public DataTable GetBalance(int Device_ID, ref int result)
        {
            return objVaultDataAccess.GetBalance(Device_ID, ref result);
        }
        public DataTable DropVault(int Device_ID, int Reason_id, bool FinalDrop, ref int result)
        {
            return objVaultDataAccess.DropVault(Device_ID, Reason_id, FinalDrop, ref result);
        }
        public DataTable GetTransactionReasons()
        {
            return objVaultDataAccess.GetTransactionReasons();

        }

        public DataSet GetVaultTransactionEvents(int Vault_Id, string Type, int No_Of_Records, string SearchKey, DateTime StartDate, DateTime EndDate)
        {
            return objVaultDataAccess.GetVaultTransactionEvents(Vault_Id, Type, No_Of_Records, SearchKey, StartDate, EndDate);

        }


        public List<GetUndeclaredVaultDrops> GetUndeclaredDrops(bool showcassettes)
        {

            List<GetUndeclaredVaultDrops> lst_vdrops = new List<GetUndeclaredVaultDrops>();

            System.Data.Linq.IMultipleResults result = objVaultDataAccess.GetUndeclaredDrops(false);
            List<CassetteDropsResult> lst_cassette = new List<CassetteDropsResult>();
            if (showcassettes)
            {
                System.Data.Linq.IMultipleResults cassetteresult = objVaultDataAccess.GetUndeclaredDrops(true);
                //List<CassetteDropsResult> lst_cassette = cassetteresult.GetResult<rsp_GetVaultCassetteDropsResult>().ToList();
                var redcolor = new System.Windows.Media.SolidColorBrush(Colors.DarkRed);
                var blackcolor = new System.Windows.Media.SolidColorBrush(Colors.Black);

                var BoldFont = System.Windows.FontWeights.Bold;

                var NormalFont = System.Windows.FontWeights.Normal;



                lst_cassette = (from d in cassetteresult.GetResult<rsp_GetVaultCassetteDropsResult>()

                                select new CassetteDropsResult
                                {
                                    Cassette_ID = d.Cassette_ID,
                                    Cassette_Name = d.Cassette_Name,
                                    DeclaredBalance = d.DeclaredBalance ?? 0,
                                    CassetteType_ID = d.CassetteType_ID,
                                    CassetteType_Name = d.CassetteType_Name,
                                    IsChecked = true,
                                    Quantity = Settings.AutoFillDeclaredAmount && d.EnableControls ? Convert.ToInt32(Math.Truncate(d.DeclaredBalance.Value / Convert.ToDecimal(d.Denom.Value))) : 0,
                                    Denom = d.Denom,
                                    Drop_ID = d.Drop_ID,
                                    VaultBalance = d.VaultBalance,
                                    EnableControls = d.EnableControls,
                                    MaxFillAmount = d.MaxFillAmount.Value,
                                    FontColor = d.EnableControls ? blackcolor : redcolor,
                                    CustomFontWeight = d.EnableControls ? NormalFont : BoldFont,
                                    IsBillCounterAmountEditable = (d.Cassette_ID == 0) ? false : Settings.IsBillCounterAmountEditable,
                                    IsBillCounterQuantityEditable = !Settings.IsBillCounterAmountEditable
                                }).ToList<CassetteDropsResult>();




            }
            lst_vdrops = (from c in result.GetResult<rsp_Vault_GetUndeclaredDropsResult>()
                          select new GetUndeclaredVaultDrops
                          {
                              AdjustmentAmount = c.AdjustmentAmount,
                              AuditDate = c.AuditDate,
                              AuditUser = c.AuditUser,
                              BleedAmount = c.BleedAmount,
                              CreatedDate = c.CreatedDate,
                              CreateUser = c.CreateUser,
                              Declared = c.Declared,
                              Declared_Balance = c.Declared_Balance ?? 0,
                              Vault_ID = c.Vault_ID,
                              Drop_ID = c.Drop_ID,
                              FillAmount = c.FillAmount,
                              Freezed = c.Freezed,
                              FreezedDate = c.FreezedDate,
                              FreezeUser = c.FreezeUser,
                              Manufacturer = c.Manufacturer,
                              Meter_Balance = c.Meter_Balance,
                              ModifiedDate = c.ModifiedDate,
                              ModifiedUser = c.ModifiedUser,
                              TypePrefix = c.TypePrefix,
                              Vault_Balance = c.Vault_Balance,
                              VaultName = c.VaultName,
                              UserName = c.UserName,
                              OpeningBalance = c.OpeningBalance,
                              ToDeclared = false,
                              Cassettes = lst_cassette.FindAll(obj => obj.Drop_ID == c.Drop_ID),
                              BillsTotal = 0,
                              TotalCoinsValueAsCurrency = 0,
                              VaultCapacity = c.VaultCapacity.Value
                          }).ToList<GetUndeclaredVaultDrops>();


            return lst_vdrops;

        }





        public bool UpdateVaultDrops(decimal declaredBalance, bool declared, long dropID, System.Xml.Linq.XElement CassetteXml, ref int ErrorCode)
        {
            int retval = objVaultDataAccess.UpdateVaultDrops(declaredBalance, declared, dropID, Security.SecurityHelper.CurrentUser.SecurityUserID, CassetteXml);
            if (retval == -2)
            {
                ErrorCode = retval;
            }
            return (retval >= 0);
        }

        public bool DropVaultCassettes(int VaultID, int user_ID, int transaction_Reason_ID, System.Xml.Linq.XElement cassetteXML, bool IsFinalDrop,
                                        ref int result, ref usp_Vault_DropResult v_res)
        {
            List<usp_Vault_DropResult> lst_drop = objVaultDataAccess.Vault_Drop(VaultID, user_ID, transaction_Reason_ID, cassetteXML, IsFinalDrop, ref result).ToList();
            if (lst_drop.Count > 0)
            {
                v_res = lst_drop[0];
            }
            return (result >= 0);
        }

        public List<GetNGADetailsResult> GetCassetteDetails(List<DenomCombo> lst_denom, int vaultID, bool isDropTransaction, VaultTransactionType vtype)
        {
            List<GetNGADetailsResult> lst_NGA = new List<GetNGADetailsResult>();


            var redcolor = new System.Windows.Media.SolidColorBrush(Colors.DarkRed);
            var blackcolor = new System.Windows.Media.SolidColorBrush(Colors.Black);

            var BoldFont = System.Windows.FontWeights.Bold;

            var NormalFont = System.Windows.FontWeights.Normal;

            bool isFinalDrop = false;
            bool isStandardFill = false;
            bool isAdjustment = false;

            switch (vtype)
            {
                case VaultTransactionType.NegativeAdjustment:
                case VaultTransactionType.PositiveAdjustment:
                    isAdjustment = true;
                    break;
                case VaultTransactionType.FinalDrop:
                    isFinalDrop = true;
                    break;
                case VaultTransactionType.StandardFill:
                    isStandardFill = true;
                    break;
            }
            bool _EnableTotal = (isAdjustment ? false : (!isDropTransaction));

            Func<int, bool, bool> fn_FillRejection = (int CassetteTypeID, bool FillRejection) =>
            {
                if (CassetteTypeID == (int)CassetteTypes.RejectionCassette)
                {
                    if (vtype == VaultTransactionType.StandardFill || vtype == VaultTransactionType.Fill)
                    {
                        return FillRejection;
                    }
                    else if (vtype == VaultTransactionType.Bleed)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }
            };
            lst_NGA = (from c in objVaultDataAccess.GetCassetteDetails(vaultID)
                       select new GetNGADetailsResult
                       {
                           Alert_Level = c.Alert_Level,
                           Capacity = c.Capacity,
                           Cassette_ID = c.Cassette_ID,
                           Cassette_Name = c.Cassette_Name,
                           Cassette_Type = c.Cassette_Type,
                           CassetteAlertLevel = c.CassetteAlertLevel,
                           Created_Date = c.Created_Date,
                           CurrentBalance = c.CurrentBalance,//dont change order or else u get exception
                           Quantity = isStandardFill ? c.StandardQuantity : (isAdjustment ? Convert.ToInt32((c.CurrentBalance.Value) / Convert.ToDecimal(c.Denom)) : 0),
                           CanChangeDenom = (isStandardFill) && (c.CanChangeDenom ?? false),
                           Denom = c.Denom,
                           Manufacturer = c.Manufacturer,
                           MaxBleedAmount = c.MaxBleedAmount,
                           MaxFillAmount = c.MaxFillAmount,
                           MinBleedAmount = c.MinBleedAmount,
                           MinFillAmount = c.MinFillAmount,
                           Serial_No = c.Serial_No,
                           StandardFillAmount = c.StandardFillAmount,
                           VaultType = c.VaultType,
                           Amount = isStandardFill ? c.StandardFillAmount : (isAdjustment ? (c.CurrentBalance.Value) : 0.0m),
                           Total = isStandardFill ? (c.StandardFillAmount + c.CurrentBalance ?? 0) : (c.CurrentBalance ?? 0.00m),
                           IsChecked = fn_FillRejection(c.Cassette_Type, c.FillRejection.Value),
                           IsNotFinalDrop = isFinalDrop ? false : (isAdjustment ? false : fn_FillRejection(c.Cassette_Type, c.FillRejection.Value)),
                           IsDROP = (!isDropTransaction),
                           EnableTotal = _EnableTotal,
                           lstDenoms = lst_denom.FindAll(o => o.CassetteTypes == c.Cassette_Type),
                           EnableControls = true,
                           FontColor = blackcolor,
                           DroppedRecently = c.DroppedRecently ?? false,
                           CustomFontWeight = NormalFont,
                           IsStandardFill = isStandardFill,
                           FillRejection = c.FillRejection ?? false,
                           OldAmount = isStandardFill ? c.StandardFillAmount : 0.00m//amount

                       }).ToList<GetNGADetailsResult>();



            if (lst_NGA != null && lst_NGA.Count > 0)
            {
                decimal tot_currentbal = lst_NGA.Sum(c => c.CurrentBalance ?? 0.00m);
                decimal tot_standardamount = lst_NGA.Sum(c => c.StandardFillAmount);
                lst_NGA.Add(new GetNGADetailsResult
                {
                    Alert_Level = 0,
                    Capacity = 0,
                    Cassette_ID = 0,
                    Cassette_Name = "Total",
                    Cassette_Type = 0,
                    CassetteAlertLevel = 0,
                    Created_Date = null,
                    CurrentBalance = tot_currentbal,//dont change order or else u get exception
                    Quantity = 0,
                    CanChangeDenom = false,
                    Denom = 0,
                    Manufacturer = "",
                    MaxBleedAmount = 0,
                    MaxFillAmount = 0,
                    MinBleedAmount = 0,
                    MinFillAmount = 0,
                    Serial_No = "",
                    StandardFillAmount = 0,
                    VaultType = "",
                    Amount = isStandardFill ? tot_standardamount : (isAdjustment ? (tot_currentbal) : 0.0m),
                    Total = isStandardFill ? (tot_standardamount + tot_currentbal) : tot_currentbal,
                    IsChecked = false,
                    IsNotFinalDrop = false,
                    IsDROP = false,
                    lstDenoms = null,
                    EnableControls = false,
                    DroppedRecently = false,
                    FontColor = redcolor,
                    CustomFontWeight = BoldFont,
                    OldAmount = 0
                });
            }
            return lst_NGA;
        }

        public List<NGA_GetCassetteDetailsResult> NGA_GetCassetteDetails(int VaultID)
        {
            List<NGA_GetCassetteDetailsResult> lst_NGA = objVaultDataAccess.NGA_GetCassetteDetails(VaultID).ToList<NGA_GetCassetteDetailsResult>();

            //(from c in objVaultDataAccess.NGA_GetCassetteDetails(VaultID)
            //       select new NGA_GetCassetteDetailsResult
            //       {
            //           Alert_Level = c.Alert_Level,
            //           Capacity = c.Capacity,
            //           Cassette_ID = c.Cassette_ID,
            //           Cassette_Name = c.Cassette_Name,
            //           Cassette_Type = c.Cassette_Type,
            //           CassetteAlertLevel = c.CassetteAlertLevel,
            //           Created_Date = c.Created_Date,                          
            //           Denom = c.Denom,
            //           Manufacturer = c.Manufacturer,
            //           MaxBleedAmount = c.MaxBleedAmount,
            //           MaxFillAmount = c.MaxFillAmount,
            //           MinBleedAmount = c.MinBleedAmount,
            //           MinFillAmount = c.MinFillAmount,
            //           Serial_No = c.Serial_No,
            //           StandardFillAmount = c.StandardFillAmount,
            //           VaultType = c.VaultType,
            //           ID=c.ID                           
            //       }).ToList<NGA_GetCassetteDetailsResult>();



            return lst_NGA;
        }

        public List<GetNGANameResult> GetNGAName(string type)
        {
            List<GetNGANameResult> lst_NGANames = new List<GetNGANameResult>();

            lst_NGANames = (from c in objVaultDataAccess.GetNGAName(type)
                            select new GetNGANameResult
                            {
                                Installation_No = c.Installation_No,
                                NGAID = c.NGAID,
                                NGAName = c.NGAName,
                                IsEnrolled = c.IsEnrolled
                            }).ToList<GetNGANameResult>();
            return lst_NGANames;
        }


        public List<GetNGATypesResult> GetNGATypes()
        {
            List<GetNGATypesResult> lst_NGAType = new List<GetNGATypesResult>();

            lst_NGAType = (from c in objVaultDataAccess.GetNGATypes()
                           select new GetNGATypesResult
                            {
                                Description = c.Description,
                                Name = c.Name,
                                Type_ID = c.Type_ID
                            }).ToList<GetNGATypesResult>();
            return lst_NGAType;
        }


        public bool EnrollNGA(int installation_no, int user_id)
        {
            int retval = objVaultDataAccess.EnrollNGA(installation_no, user_id);
            return (retval >= 0);
        }


        public bool FillAmountinCassettes(int device_ID, decimal fillamount, decimal currentBalance, int user_ID, int withDrawFlag, string type, bool? returnBalance,
            int transaction_Reason_ID, System.Xml.Linq.XElement cassetteXML, ref int result, ref usp_Vault_FillVaultResult v_res, bool sendAlert)
        {
            int? _result = 0;
            List<usp_Vault_FillVaultResult> lst_res = objVaultDataAccess.Vault_FillVault(device_ID, fillamount, currentBalance, user_ID, withDrawFlag, type, returnBalance, transaction_Reason_ID, ref _result, cassetteXML, sendAlert).ToList();
            if (lst_res.Count > 0)
            {
                v_res = lst_res[0];
            }
            result = (_result ?? 0);
            return (result >= 0);
        }

        public bool IsStandardFillDoneTwice(int vault_id)
        {
            List<rsp_Vault_CheckStandardFillsCountResult> lst_count = objVaultDataAccess.CheckStandardFillsCount(vault_id).ToList();
            return (lst_count.Count > 0 && lst_count[0].FillCount > 0);
        }

        public List<rsp_Vault_GetFillHistoryDetailsResult> GetFillHistoryDetails(long Transaction_ID)
        {
            List<rsp_Vault_GetFillHistoryDetailsResult> lst_FillHistory = objVaultDataAccess.GetFillHistoryDetails(Transaction_ID).ToList<rsp_Vault_GetFillHistoryDetailsResult>();
            if (lst_FillHistory.Count > 0)
            {
                decimal TotalFillAmount = 0;
                decimal TotalInitialBalance = 0;
                decimal TotalVaultBalance = 0;

                var redcolor = new System.Windows.Media.SolidColorBrush(Colors.DarkRed);
                var blackcolor = new System.Windows.Media.SolidColorBrush(Colors.Black);

                var BoldFont = new System.Windows.FontWeight();
                BoldFont = System.Windows.FontWeights.Bold;

                var NormalFont = new System.Windows.FontWeight();
                NormalFont = System.Windows.FontWeights.Normal;

                for (int i = 0; i < lst_FillHistory.Count; i++)
                {
                    TotalFillAmount += (lst_FillHistory[i].FillAmount ?? 0);
                    TotalInitialBalance += (lst_FillHistory[i].InitialBalance ?? 0);
                    TotalVaultBalance += (lst_FillHistory[i].VaultBalance ?? 0);
                    lst_FillHistory[i].EnableControls = true;
                    lst_FillHistory[i].FontColor = blackcolor;
                    lst_FillHistory[i].CustomFontWeight = NormalFont;
                }

                lst_FillHistory.Add(new rsp_Vault_GetFillHistoryDetailsResult
                {
                    Denom = 0,
                    FillAmount = TotalFillAmount,
                    Cassette_Name = "Total",
                    InitialBalance = TotalInitialBalance,
                    ID = 0,
                    Manufacturer = lst_FillHistory[0].Manufacturer,
                    TypePrefix = lst_FillHistory[0].TypePrefix,
                    TransactionDate = null,
                    UserName = "",
                    VaultBalance = TotalVaultBalance,
                    EnableControls = false,
                    FontColor = redcolor,
                    CustomFontWeight = BoldFont,
                });
            }
            return lst_FillHistory;
        }

        public List<rsp_Vault_GetTransactionTypesResult> GetTransactionTypes()
        {
            return objVaultDataAccess.GetTransactionTypes().ToList<rsp_Vault_GetTransactionTypesResult>();
        }



    }
}
