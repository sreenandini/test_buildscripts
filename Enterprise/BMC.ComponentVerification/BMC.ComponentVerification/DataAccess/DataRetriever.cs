using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.Data;
using System.Data.Linq.Mapping;
using System.Reflection;
using BMC.ComponentVerification.BusinessEntities;

namespace BMC.ComponentVerification.DataAccess
{
    class DataRetriever : DataContext
    {
        public DataRetriever(IDbConnection dbConnection)
            : base(dbConnection)
        {
        }

        [Function(Name = "dbo.rsp_GetDefaultComponentTypes")]
        public ISingleResult<ComponentTypesData> GetDefaultComponentTypes()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<ComponentTypesData>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_InsertComponentType")]
        public int InsertComponentType([Parameter(Name = "CompName", DbType = "VarChar(50)")] string compName, [Parameter(Name = "CompDesc", DbType = "VarChar(50)")] string compDesc)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), compName, compDesc);
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetAlgorithmTypes")]
        public ISingleResult<AlgorithmTypesData> GetAlgorithmTypes([Parameter(Name = "CompID", DbType = "INT")] int iCompID, [Parameter(Name = "New", DbType = "INT")] int iNew)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), iCompID, iNew);
            return ((ISingleResult<AlgorithmTypesData>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_InsertComponentDetails")]
        public int InsertComponentDetails([Parameter(Name = "CompName", DbType = "VarChar(50)")] string compName, [Parameter(Name = "ModelName", DbType = "VarChar(50)")] string ModelName, [Parameter(Name = "CompType", DbType = "Int")] System.Nullable<int> compType, [Parameter(Name = "AlgoType", DbType = "Int")] System.Nullable<int> algoType, [Parameter(Name = "Seed", DbType = "VarChar(150)")] string seed, [Parameter(Name = "Hash", DbType = "VarChar(150)")] string hash, [Parameter(Name = "IsSuccess", DbType = "Int")] ref System.Nullable<int> isSuccess)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), compName, ModelName, compType, algoType, seed, hash, isSuccess);
            isSuccess = ((System.Nullable<int>)(result.GetParameterValue(6)));
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetAllComponentDetails")]
        public ISingleResult<AllComponentDetailsData> GetAllComponentDetails()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<AllComponentDetailsData>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetComponentDetails")]
        public ISingleResult<ComponentDetailsData> GetComponentDetails([Parameter(Name = "CCD_ID", DbType = "Int")] System.Nullable<int> cCD_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), cCD_ID);
            return ((ISingleResult<ComponentDetailsData>) (result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_UpdateComponentDetails")]
        public int UpdateComponentDetails([Parameter(Name = "CompName", DbType = "VarChar(50)")] string compName, 
            [Parameter(Name = "ModelName", DbType = "VarChar(50)")] string ModelName, 
            [Parameter(Name = "CompType", DbType = "Int")] System.Nullable<int> compType, 
            [Parameter(Name = "AlgoType", DbType = "Int")] System.Nullable<int> algoType, 
            [Parameter(Name = "Seed", DbType = "VarChar(150)")] string seed, 
            [Parameter(Name = "Hash", DbType = "VarChar(150)")] string hash, 
            [Parameter(Name = "IsSuccess", DbType = "Int")] ref System.Nullable<int> isSuccess)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), 
                compName, ModelName, compType, algoType, seed, hash, isSuccess);
            isSuccess = ((System.Nullable<int>)(result.GetParameterValue(6)));
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_InsertComponentDetailsEHRecord")]
        public int InsertComponentDetailsEHRecord([Parameter(Name = "ID", DbType = "Int")] System.Nullable<int> iD, 
            [Parameter(Name = "Site_Code", DbType = "VarChar(50)")] string site_Code)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), iD, site_Code);
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetVerificationCompDetails")]
        public ISingleResult<VerificationCompDetailsData> GetVerificationComponentDetails([Parameter(Name = "FromDate", DbType = "Datetime")] DateTime dtFrom, 
            [Parameter(Name = "ToDate", DbType = "Datetime")] DateTime dtTo, [Parameter(Name = "ComponentType", DbType = "VARCHAR(50)")] string strCompType, 
            [Parameter(Name = "VerificationType", DbType = "VARCHAR(50)")] string strVerType, 
            [Parameter(Name = "SiteName", DbType = "VARCHAR(50)")] string strSiteName, 
            [Parameter(Name = "SeriaNo", DbType = "VARCHAR(50)")] string strSeriaNo, 
            [Parameter(Name = "CompName", DbType = "VARCHAR(50)")] string strCompName)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), dtFrom, dtTo, 
                strCompType, strVerType, strSiteName, strSeriaNo, strCompName);
            return ((ISingleResult<VerificationCompDetailsData>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetAllMachineForSite")]
        public ISingleResult<MachineDetailsData> GetAllMachineForSite([Parameter(Name = "SiteId", DbType = "Int")] System.Nullable<int> siteId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), siteId);
            return ((ISingleResult<MachineDetailsData>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetAllSiteForVerification")]
        public ISingleResult<SiteDetailsData> GetAllSiteForVerification()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<SiteDetailsData>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetAllComponentForMachine")]
        public ISingleResult<MachineCompDetails> GetAllComponentForMachine([Parameter(Name = "Serial_No", DbType = "VarChar(30)")] string serial_No)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), serial_No);
            return ((ISingleResult<MachineCompDetails>)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_InsertComponentVerificationRecord")]
        public int InsertComponentVerificationRecord([Parameter(Name = "MachineSerialNo", DbType = "VarChar(30)")] string machineSerialNo, [Parameter(Name = "ComponentTypeID", DbType = "Int")] System.Nullable<int> componentID, [Parameter(Name = "VerificationType", DbType = "Int")] int verificationType)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), machineSerialNo, componentID, verificationType);
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_CheckComponentVerificationStatus")]
        public int CheckComponentVerificationStatus([Parameter(Name = "Serial_No", DbType = "VarChar(30)")] string serial_No, [Parameter(Name = "ComponentID", DbType = "Int")] System.Nullable<int> componentID, [Parameter(Name = "Success", DbType = "Int")] ref System.Nullable<int> success)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), serial_No, componentID, success);
            success = ((System.Nullable<int>)(result.GetParameterValue(2)));
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetDefaultVerificationTypes")]
        public ISingleResult<VerificationTypesData> GetDefaultVerificationTypes()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<VerificationTypesData>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetCompVerificationRecordForExport")]
        public ISingleResult<ComponentVerificationData> GetCompVerificationRecordForExport([Parameter(Name = "SerialNo", DbType = "VarChar(30)")] string serialNo, [Parameter(Name = "CompTypeID", DbType = "Int")] System.Nullable<int> compTypeID, [Parameter(Name = "VerificationType", DbType = "Int")] System.Nullable<int> verificationType)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), serialNo, compTypeID, verificationType);
            return ((ISingleResult<ComponentVerificationData>)(result.ReturnValue));
        }
    }
 
}
