using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.Business.CashDeskOperator;
using System.Data;
using BMC.Transport;

namespace BMC.CashDeskOperator
{
    public class Vault
    {
        VaultBiz objVaultBiz = new VaultBiz();
        private static Vault _vault = null;
        public static Vault CreateInstance()
        {
            if (_vault == null)
                _vault = new Vault();
            return _vault;
        }

        public DataTable GetVaultDevices()
        {
            return objVaultBiz.GetVaultDevices();
        }

        public DataTable FillVault(int Device_ID, decimal Amount, decimal CurrentBalance, int iWitDrawl, string strType, int Reason_id, ref int iResult, bool sendAlert)
        {
            return objVaultBiz.FillVault(Device_ID, Amount, CurrentBalance, iWitDrawl, strType, Reason_id, ref iResult, sendAlert);
        }

        public DataTable GetFillHistory(int Device_ID, int No_Of_Records,int TransType)
        {
            return objVaultBiz.GetFillHistory(Device_ID, No_Of_Records, TransType);
        }

        public DataTable GetBalance(int Device_ID,ref int result)
        {
            return objVaultBiz.GetBalance(Device_ID,ref result);
        }
        public DataTable DropVault(int Device_ID, int Reason_id, bool FinalDrop, ref int result)
        {
            return objVaultBiz.DropVault(Device_ID, Reason_id, FinalDrop,ref result);
        }
        public DataTable GetTransactionReasons()
        {
            return objVaultBiz.GetTransactionReasons();
        }
        public DataSet GetVaultTransactionEvents(int Vault_Id, string Type, int No_Of_Records, string SearchKey, DateTime StartDate, DateTime EndDate)
        {
            return objVaultBiz.GetVaultTransactionEvents(Vault_Id, Type, No_Of_Records, SearchKey,StartDate,EndDate);

        }
        public List<GetUndeclaredVaultDrops> GetUndeclaredDrops(bool showCassettes)
        {
            return objVaultBiz.GetUndeclaredDrops(showCassettes);
        }

        public bool UpdateVaultDrops(decimal declaredBalance, bool declared, long dropID, System.Xml.Linq.XElement CassetteXml, ref int ErrorCode)
        {
            return objVaultBiz.UpdateVaultDrops(declaredBalance, declared, dropID, CassetteXml, ref ErrorCode);
        }


        public List<GetNGADetailsResult> GetCassetteDetails(List<DenomCombo> Denom, int vaultID, bool isDropTransaction, VaultTransactionType vtype)
        {
            return objVaultBiz.GetCassetteDetails(Denom ,vaultID, isDropTransaction,vtype);
        }

        public List<NGA_GetCassetteDetailsResult> GetNGADetails(int VaultID)
        {
            return objVaultBiz.NGA_GetCassetteDetails(VaultID);
        }

        public bool DropVaultCassettes(int VaultID, int user_ID, int transaction_Reason_ID, System.Xml.Linq.XElement cassetteXML, bool IsFinalDrop, ref int result, ref usp_Vault_DropResult v_res)
        {
            return objVaultBiz.DropVaultCassettes(VaultID, user_ID, transaction_Reason_ID, cassetteXML, IsFinalDrop,ref result, ref  v_res);
        }

        public List<GetNGANameResult> GetNGAName(string type)
        {
            return objVaultBiz.GetNGAName(type);
        }

        public List<GetNGATypesResult> GetNGATypes()
        {
            return objVaultBiz.GetNGATypes();
        }

        public bool EnrollNGA(int installation_no, int user_id)
        {
            return objVaultBiz.EnrollNGA(installation_no, user_id);
        }

        public bool FillAmountinCassettes(int device_ID, decimal fillamount, decimal currentBalance, int user_ID, int withDrawFlag, string type, bool? returnBalance, int transaction_Reason_ID, System.Xml.Linq.XElement cassetteXML, ref int result, ref usp_Vault_FillVaultResult v_res,bool sendAlert)
        {
            return objVaultBiz.FillAmountinCassettes(device_ID, fillamount, currentBalance, user_ID, withDrawFlag, type, returnBalance, transaction_Reason_ID, cassetteXML, ref result, ref  v_res, sendAlert);
        }

        public bool IsStandardFillDoneTwice(int vault_id)
        {
            return objVaultBiz.IsStandardFillDoneTwice(vault_id);
        }

        public List<rsp_Vault_GetFillHistoryDetailsResult> GetFillHistoryDetails(long Transaction_ID)
        {
            return objVaultBiz.GetFillHistoryDetails(Transaction_ID);

        }
    }
}
