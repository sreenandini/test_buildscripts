using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using BMC.CashDeskOperator.BusinessObjects;
using BMC.Transport;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;

namespace CageBMCInterface
{
      public class Jackpot : JackpotEndPoint 
    {
        IHandpay handpay = HandpayBusinessObject.CreateInstance();
        public Jackpot()
        {
            SettingInitializer.Initialize();
        }
        #region JackpotEndPoint Members

        public jackpotProcessInfoDTO getJackpotStatusAmount(long sequenceNumber, long siteId)
        {
            jackpotProcessInfoDTO response = new jackpotProcessInfoDTO();
            try
            {

                response = handpay.getJackpotStatusAmount(sequenceNumber.ToString(), Convert.ToInt32(siteId));

            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
            return response;
        }

        public jackpotProcessInfoDTO payJackpot(long sequenceNumber, long siteId, string userId, string firstName, string lastName, string cashDeskLocation)
        {
            jackpotProcessInfoDTO response = new jackpotProcessInfoDTO();
            try
            {
                response = handpay.payJackpot(sequenceNumber.ToString(), Convert.ToInt32(siteId), userId, firstName, lastName, cashDeskLocation);
            }
            catch (Exception Ex) 
            {
                ExceptionManager.Publish(Ex);
                
            }
            return response;
        }

        public jackpotProcessInfoDTO voidJackpot(long sequenceNumber, long siteId, string userID, string firstName, string lastName, string cashDeskLocation)
        {
            throw new NotImplementedException();
        }
        
        #endregion

        #region JackpotEndPoint Members

        public getAllUnProcessedJackpotResponse getAllUnProcessedJackpot(getAllUnProcessedJackpotRequest request)
        {
            throw new NotImplementedException();
        }

        public getCashlessJackpotStatusAmountResponse1 getCashlessJackpotStatusAmount(getCashlessJackpotStatusAmountRequest request)
        {
            throw new NotImplementedException();
        }

        public getJackpotStatusAmountResponse1 getJackpotStatusAmount(getJackpotStatusAmountRequest request)
        {
            if (!CheckCageEnabled())
                return null;
            getJackpotStatusAmountResponse1 oResponse = new getJackpotStatusAmountResponse1();
            oResponse.getJackpotStatusAmountResponse = new getJackpotStatusAmountResponse();
            jackpotProcessInfoDTO response = new jackpotProcessInfoDTO();
            try
            {
                oResponse.getJackpotStatusAmountResponse.@return = handpay.getJackpotStatusAmount(request.getJackpotStatusAmount.arg0.ToString(), Convert.ToInt32(request.getJackpotStatusAmount.arg1));
                //oResponse.getJackpotStatusAmountResponse.@return = handpay.getJackpotStatusAmount(request.getJackpotStatusAmount.arg0.ToString(), 1030);

            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
            return oResponse;
        }

        public payJackpotResponse1 payJackpot(payJackpotRequest request)
        {
            if (!CheckCageEnabled())
                return null;
            payJackpotResponse1 oResponse = new payJackpotResponse1();
            oResponse.payJackpotResponse = new payJackpotResponse();
            jackpotProcessInfoDTO response = new jackpotProcessInfoDTO();
            try
            {
                oResponse.payJackpotResponse.@return = handpay.payJackpot(request.payJackpot.arg0.ToString(),int.Parse(request.payJackpot.arg1.ToString()) , request.payJackpot.arg2, request.payJackpot.arg3, request.payJackpot.arg4, request.payJackpot.arg5);
                //oResponse.payJackpotResponse.@return = handpay.payJackpot(request.payJackpot.arg0.ToString(), 1030, request.payJackpot.arg2, request.payJackpot.arg3, request.payJackpot.arg4, request.payJackpot.arg5);
              //response =                            handpay.payJackpot(sequenceNumber.ToString(), Convert.ToInt32(siteId), userId, firstName, lastName, cashDeskLocation);
                
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);

            }
            return oResponse;
        }

        public voidJackpotResponse1 voidJackpot(voidJackpotRequest request)
        {
            throw new NotImplementedException();
        }
        bool CheckCageEnabled()
        {
            if (!Settings.CAGE_ENABLED)
            {
                LogManager.WriteLog("Jackpot Service: Cage Not Enabled", LogManager.enumLogLevel.Info);

            }
            return Settings.CAGE_ENABLED;
        }
        #endregion
    }
}
