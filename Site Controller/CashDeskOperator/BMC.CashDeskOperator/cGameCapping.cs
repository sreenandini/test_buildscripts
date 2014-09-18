using BMC.Business.CashDeskOperator;
using BMC.Common.ExceptionManagement;
using BMC.Transport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.CashDeskOperator.BusinessObjects
{
    public class GameCappingBO
    {
        #region Private Variables
        GameCappingBiz _GameCappingBiz = new GameCappingBiz();         
        #endregion

        #region Constructor
        public GameCappingBO() { }
        #endregion        
        
        #region Public Function
        public List<GameCapDetails> GetGameCapDetails()
        {
            List<GameCapDetails> rsltGameCapDetails = null;           

            try
            {
                rsltGameCapDetails = _GameCappingBiz.GetGameCapDetails();
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }

            return rsltGameCapDetails;
        }

        public GameCapResult UpdateGameCapDetails(int GameCappingID, string UserName)
        {
            GameCapResult rsltGameCapResult = null;            

            try
            {
                rsltGameCapResult = _GameCappingBiz.UpdateGameCapDetails(GameCappingID, UserName);
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }

            return rsltGameCapResult;
        }

        public GameCapResult ValidateGameCapDetails(int InstallationNo)
        {
            GameCapResult rsltGameCapResult = null;

            try
            {
                rsltGameCapResult = _GameCappingBiz.ValidateGameCapDetails(InstallationNo);
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }

            return rsltGameCapResult;
        }
        #endregion
    }
}
