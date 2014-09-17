/*-------------------------------------------------------------------------- 
--
-- Description: Constants class.
-- Revision History
-- 
-- Author               Date                Remarks
-- Anuradha            03 Feb 2009          Initial Version
----------------------------------------------------------------------------- */


using System;
using System.Collections.Generic;
using System.Text;



namespace BMC.DBInterface.NetworkService
{
    public static class DBConstants
    {
        public const string RSP_SHOULDMACHINEBEENABLED = "RSP_SHOULDMACHINEBEENABLED";
        public const string USP_INSERTFAULTEVENT = "usp_InsertFaultEvent";
        public const string RSP_CHECKIFFAULTEXISTS = "rsp_checkiffaultexists";
        public const string RSP_CHECKSHOULDMACHINEBEENABLED = "rsp_ShouldMachineBeEnabled";
        public const string RSP_GETSETTING = "rsp_GetSetting";
        public const string USP_EDITSETTING = "usp_EditSetting";
        public const string RSP_GETINSTALLATIONDETAILS = "rsp_GetInstallationDetails";
        public const string USP_UPDATEBARPOSITIONFORMACHINECONTROL = "usp_UpdateBarPositionForMachineControl";

        public const string RSP_GETGAMEENABLEDISABLESTATUS = "rsp_GetGameEnableDisableStatus";



        //Parameter for rsp_getSetting
        public const string CONST_SP_PARAM_SETTINGID = "@Setting_ID";
        public const string CONST_SP_PARAM_SETTINGNAME = "@Setting_Name";
        public const string CONST_SP_PARAM_SETTINGDEFAULT = "@Setting_Default";
        public const string CONST_SP_PARAM_SETTINGVALUE = "@Setting_Value";
        public const string CONST_SP_PARAM_RETURNVALUE = "@RETURN_VALUE";


        //Parameter for usp_InsertFaultEvent
        public const string CONST_SP_PARAM_INSTALLATIONID = "@Installation_ID";
        public const string CONST_SP_PARAM_FAULTSOURCE = "@FaultSource";
        public const string CONST_SP_PARAM_FAULTDESC = "@FaultDesc";
        public const string CONST_SP_PARAM_POLLED = "@Polled";
        public const string CONST_SP_PARAM_EVENTFAULT = "@Event_Fault";
        public const string CONST_SP_PARAM_EVENTDATE = "@Event_Date";

        //Constants to call web service method for machine control
        public const string CONSTANT_WEBMETHOD_ENABLENOTEACCEPTOR = "EnableNoteAcceptor";
        public const string CONSTANT_WEBMETHOD_DISABLENOTEACCEPTOR = "DisableNoteAcceptor";
        public const string CONSTANT_WEBMETHOD_ENABLEMACHINE = "EnableMachines";
        public const string CONSTANT_WEBMETHOD_DISABLEMACHINE = "DisableMachines";


    }
}
