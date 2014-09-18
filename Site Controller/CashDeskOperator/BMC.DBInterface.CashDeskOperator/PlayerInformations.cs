using System;
using System.Data;
using System.Xml;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.Transport.CashDeskOperatorEntity;


namespace BMC.DBInterface.CashDeskOperator
{
    public static class PlayerInformations
    {
        #region Declaration
        static DataTable dtPrizeList = null;
        static DataColumn dcPrize = null;
        static DataRow drPrize = null;
        static string strProc = string.Empty;
        static XmlNodeList objPrizeList = null;
        #endregion

        #region Getplayers
        public static PlayerInformation[] CreatePlayerTable()
        {
            //Return the list of players available.
            return CreatePlayers();
        }
        #endregion Getplayers

        #region Create Players
       
        public static PlayerInformation[] CreatePlayers()
        {
            try
            {
                XmlNode objPlayer = null;
                XmlNodeList objChildNodeList = null;
                PlayerInformation[] objPlayerInfo;
                strProc = "Create Players";
                int h = 0;
                objPlayerInfo = new PlayerInformation[6];

                string strXmlPath = System.Windows.Forms.Application.StartupPath + "\\XMLData\\Players.xml";
                if (System.IO.File.Exists(strXmlPath))
                {
                    XmlDocument objXmlDoc = new XmlDocument();
                    objXmlDoc.Load(strXmlPath);

                    
                    XmlNodeList objPlayerList = objXmlDoc.SelectNodes("//PlayerInfo", null);
                    for (int j = 0; j < objPlayerList.Count; j++)
                    {
                        objPlayer = objPlayerList[j];

                        
                        objChildNodeList = objPlayer.ChildNodes;
                        objPlayerInfo[h] = new PlayerInformation();

                        //Populate the class with values from Player file.
                        for (int k = 0; k < objChildNodeList.Count; k++)
                        {
                            if (objChildNodeList[k].Name.ToUpper() == "ACCOUNTNUMBER")
                            {
                                objPlayerInfo[h].AccountNumber = objChildNodeList[k].InnerText;
                            }
                            else if (objChildNodeList[k].Name.ToUpper() == "FIRSTNAME")
                            {
                                objPlayerInfo[h].FirstName = objChildNodeList[k].InnerText;
                            }
                            else if (objChildNodeList[k].Name.ToUpper() == "LASTNAME")
                            {
                                objPlayerInfo[h].LastName = objChildNodeList[k].InnerText;
                            }
                            else if (objChildNodeList[k].Name.ToUpper() == "PLAYERID")
                            {
                                objPlayerInfo[h].PlayerID = Convert.ToInt32(objChildNodeList[k].InnerText);
                            }
                            else if (objChildNodeList[k].Name.ToUpper() == "POINTSBALANCE")
                            {
                                objPlayerInfo[h].PointsBalance = Convert.ToDouble(objChildNodeList[k].InnerText);
                            }
                            else if (objChildNodeList[k].Name.ToUpper() == "DISPLAYNAME")
                            {
                                objPlayerInfo[h].DisplayName = objChildNodeList[k].InnerText;
                            }
                            else if (objChildNodeList[k].Name.ToUpper() == "CLUBSTATUS")
                            {
                                objPlayerInfo[h].ClubStatus = objChildNodeList[k].InnerText;
                            }

                            else if (objChildNodeList[k].Name.ToUpper() == "CLUBSTATE")
                            {
                                objPlayerInfo[h].ClubState = objChildNodeList[k].InnerText;
                            }
                        }
                        h++;
                    }


                    return objPlayerInfo;
                }
                else
                {
                    LogManager.WriteLog("File not found", LogManager.enumLogLevel.Warning);
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(strProc + ex.Message, LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
                return null;
            }
        }

        #endregion Create Players

        #region Create Prizes List
        /// <summary>
        /// Create the prizes list.
        /// </summary>
        /// <param name=""></param>
        /// <returns >CashDeskOperatorEntity.PrizeInfo</returns>
        ///  /// Method Revision History
        ///
        /// Author             Date              Description
        /// ---------------------------------------------------
        /// Anuradha       12-Nov-2008          Intial Version 
        public static void CreatePrizeInfo()
        {
            
        }

        #endregion Create Prizes List

        #region Retrieve Prize List
        /// <summary>
        /// Retrieve the prize info for a player.
        /// </summary>
        /// <param name="PointsBal">PointsBalance</param>
        /// <returns >Datatable</returns>
        ///  /// Method Revision History
        ///
        /// Author             Date              Description
        /// ---------------------------------------------------
        /// Anuradha       12-Nov-2008          Intial Version 
        public static DataTable RetreivePrizeInfo(double PointsBal)
        {
            try
            {
                strProc = "RetrievePrizeInfo";
                //Load the xml with prize information.

                string strXmlPath = System.Windows.Forms.Application.StartupPath + "\\XMLData\\PlayerClub.xml";
                if (System.IO.File.Exists(strXmlPath))
                {
                    XmlDocument objXmlDoc = new XmlDocument();
                    objXmlDoc.Load(strXmlPath);

                    objPrizeList = objXmlDoc.SelectNodes("//PrizeInfo", null);

                    dtPrizeList = new DataTable();

                    //Create a table with prize columns.
                    XmlNodeList objColList = objPrizeList[0].ChildNodes;

                    for (int i = 0; i < objColList.Count; i++)
                    {

                        dcPrize = new DataColumn(objColList[i].Name);

                        dtPrizeList.Columns.Add(dcPrize);
                        dcPrize = null;
                    }

                    //Retrieve the prizes to be redeemed.
                    SetPrizeList(PointsBal);

                    //Return the table.
                    return dtPrizeList;
                }
                else
                {
                    LogManager.WriteLog("File not found", LogManager.enumLogLevel.Warning);
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(strProc + ex.Message, LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
                return null;
            }
        }


        /// <summary>
        /// Retrieve the list of prizes which can be redeemed for a player.
        /// </summary>
        /// <param name="PointsBal">PointsBalance</param>
        /// <returns ></returns>
        ///  /// Method Revision History
        ///
        /// Author             Date              Description
        /// ---------------------------------------------------
        /// Anuradha       12-Nov-2008          Intial Version 
        private static void SetPrizeList(double iReedeemPoints)
        {
            double iReedeem = 0.00;
            double iTempRedeem = 0.00;
            XmlNode objNode = null;
            XmlNode objPrizeID = null, objPrizeName = null, objAuthAward = null, objBonusPoints = null, objRedeemPoints = null, objBasePoints = null, objAwardUsed = null;

            strProc = "SetPrizeList";
            try
            {

                for (int i = 0; i < objPrizeList.Count; i++)
                {
                    objNode = objPrizeList[i];
                    iTempRedeem = iReedeem;

                    //Retreive the values of prizes.
                    if (objNode != null)
                    {

                        objAwardUsed = objNode.ChildNodes[0];
                        objPrizeName = objNode.ChildNodes[1];
                        objBonusPoints = objNode.ChildNodes[2];
                        objRedeemPoints = objNode.ChildNodes[3];
                        objBasePoints = objNode.ChildNodes[4];
                        objAuthAward = objNode.ChildNodes[5];
                        objPrizeID = objNode.ChildNodes[6];

                        //Check if the sum of redeem points is greater than points balance.
                        if (Convert.ToInt32(objRedeemPoints.InnerText) <= Convert.ToInt32(iReedeemPoints) && iReedeem <= iReedeemPoints)
                        {
                            iReedeem += Convert.ToInt32(objRedeemPoints.InnerText);
                        }

                        //Add the list of prizes which can be redeemed to table.
                        //if (Convert.ToInt32(objRedeemPoints.InnerText) <= Convert.ToInt32(iReedeemPoints) && iReedeem <= iReedeemPoints)
                        if (Convert.ToInt32(objRedeemPoints.InnerText) <= Convert.ToInt32(iReedeemPoints))
                        {
                            drPrize = dtPrizeList.NewRow();

                            drPrize["PrizeId"] = objPrizeID.InnerText;
                            drPrize["PrizeName"] = objPrizeName.InnerText;
                            drPrize["AuthAward"] = objAuthAward.InnerText;
                            drPrize["AwardUsed"] = objAwardUsed.InnerText;
                            drPrize["BasePoints"] = objBasePoints.InnerText;
                            drPrize["BonusPoints"] = objBonusPoints.InnerText;
                            drPrize["RedeemPoints"] = objRedeemPoints.InnerText;

                            dtPrizeList.Rows.Add(drPrize);
                            drPrize = null;
                        }
                        else
                        {
                            iReedeem = iTempRedeem;
                            continue;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(strProc + ex.Message, LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
        }

        #endregion Retrieve Prize List

        #region Update Redeemed Points
        /// <summary>
        /// Update the redeemed points.
        /// </summary>
        /// <param name=""></param>
        /// <returns >bool</returns>
        ///  /// Method Revision History
        ///
        /// Author             Date              Description
        /// ---------------------------------------------------
        /// Anuradha       12-Nov-2008          Intial Version 
        /// 
        public static bool UpdateRedeempoints(string strAccountNumber, double dReedeemedPoint)
        {
            strProc = "UpdateRedeemPoints";
            XmlNode objNode = null;            
            XmlNode objNewNode = null;
            XmlNode objOldNode = null;
            try
            {
                //CashDeskOperatorEntity.PlayerInfo[] objPlayerInfo = CreatePlayers();
                bool bUpdated = false;
                //Load the xml with prize information.

                string strXmlPath = System.Windows.Forms.Application.StartupPath + "\\XMLData\\Players.xml";
                if (System.IO.File.Exists(strXmlPath))
                {
                    XmlDocument objXmlDoc = new XmlDocument();
                    objXmlDoc.Load(strXmlPath);

                    XmlNodeList objPlayerList = objXmlDoc.SelectNodes("//PlayerInfo", null);


                    if (objPlayerList.Count > 0)
                    {
                        //Check if the account number matches,
                        for (int i = 0; i < objPlayerList.Count; i++)
                        {
                            objNode = objPlayerList[i];

                            if (objNode.SelectSingleNode("AccountNumber").InnerText == strAccountNumber)
                            {
                                objOldNode = objNode.SelectSingleNode("PointsBalance");
                                objNewNode = objXmlDoc.CreateElement("PointsBalance");
                                objNewNode.InnerText = dReedeemedPoint.ToString();

                                //Update the redeemed points to points balance.
                                objNode.ReplaceChild(objNewNode, objOldNode);
                                objXmlDoc.Save(strXmlPath);
                                bUpdated = true;
                                break;
                            }
                            else
                            {
                                bUpdated = false;
                            }
                        }
                    }
                }
                else
                {
                    LogManager.WriteLog("File not found", LogManager.enumLogLevel.Warning);
                    return false;
                }
                return bUpdated;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(strProc + ex.Message, LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
                return false;
            }
        }

        #endregion Update Redeemed Points

        #region Check Account Number
        /// <summary>
        /// Update the redeemed points.
        /// </summary>
        /// <param name=""></param>
        /// <returns >bool</returns>
        ///  /// Method Revision History
        ///
        /// Author             Date              Description
        /// ---------------------------------------------------
        /// Sudarsan       16-Nov-2008          Intial Version 
        /// 
        public static bool CheckAccountNumber(string AccountNumber)
        {
            XmlNode objNode = null;
            XmlDocument objXmlDoc = new XmlDocument();
            
            bool IsAvailable = false;

            try
            {
                //Load the xml with prize information.
                string XmlPath = System.Windows.Forms.Application.StartupPath + "\\XMLData\\Players.xml";
                if (System.IO.File.Exists(XmlPath))
                {
                    objXmlDoc.Load(XmlPath);
                    XmlNodeList objPlayerList = objXmlDoc.SelectNodes("//PlayerInfo", null);

                    if (objPlayerList.Count > 0)
                    {
                        for (int PlayerCount = 0; PlayerCount < objPlayerList.Count; PlayerCount++)
                        {
                            objNode = objPlayerList[PlayerCount];

                            if (objNode.SelectSingleNode("AccountNumber").InnerText == AccountNumber)
                            {
                                IsAvailable = true;
                                break;
                            }
                            else
                            {
                                IsAvailable = false;
                            }
                        }
                    }
                }
                else
                {
                    LogManager.WriteLog("File not found", LogManager.enumLogLevel.Warning);
                    //return false;
                }
                return IsAvailable;
            }
            catch (Exception ex)
            {
                //LogManager.WriteLog("Check Account Number" + ex.Message, LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
                return false;
            }
        }

        #endregion Check Account Number

        #region Update UnitCashPointValue
        /// <summary>
        /// Update the redeemed points.
        /// </summary>
        /// <param name=""></param>
        /// <returns >bool</returns>
        ///  /// Method Revision History
        ///
        /// Author             Date              Description
        /// ---------------------------------------------------
        /// Anuradha       12-Nov-2008          Intial Version 
        /// 
        public static bool UpdateUnitCashPointValue(string strPrizeName, int PointstobeRedeemed, float CashValue)
        {
            //strProc = "UpdateUnitCashPointValue";
            XmlNode objNode = null;
            //XmlNode objAcctNode = null;
            XmlNode objNewNode = null;
            XmlNode objOldNode = null;
            bool IsUpdated = false;
            XmlDocument objXmlDoc = new XmlDocument();

            try
            {
                //Load the xml with prize information.
                string XmlPath = System.Windows.Forms.Application.StartupPath + "\\XMLData\\PlayerClub.xml";
                if (System.IO.File.Exists(XmlPath))
                {
                    objXmlDoc.Load(XmlPath);
                    XmlNodeList objPlayerList = objXmlDoc.SelectNodes("//PrizeInfo", null);

                    if (objPlayerList.Count > 0)
                    {
                        //Check if the account number matches,
                        for (int i = 0; i < objPlayerList.Count; i++)
                        {
                            objNode = objPlayerList[i];

                            if (objNode.SelectSingleNode("PrizeName").InnerText == strPrizeName)
                            {

                                //Update the Total points which can be redeemed.
                                objOldNode = objNode.SelectSingleNode("RedeemPoints");
                                objNewNode = objXmlDoc.CreateElement("RedeemPoints");
                                objNewNode.InnerText = PointstobeRedeemed.ToString();

                                objNode.ReplaceChild(objNewNode, objOldNode);
                                //objNode.SelectSingleNode("RedeemPoints").InnerText = PointstobeRedeemed.ToString();
                                objXmlDoc.Save(XmlPath);

                                //Update the Unit Cash Value.
                                objOldNode = objNode.SelectSingleNode("AuthAward");
                                objNewNode = objXmlDoc.CreateElement("AuthAward");
                                objNewNode.InnerText = CashValue.ToString();


                                objNode.ReplaceChild(objNewNode, objOldNode);

                                objXmlDoc.Save(XmlPath);
                                IsUpdated = true;
                                break;
                            }
                            else
                            {
                                IsUpdated = false;
                            }
                        }
                    }
                }
                else
                {
                    LogManager.WriteLog("File not found", LogManager.enumLogLevel.Warning);
                    //return false;
                }
                return IsUpdated;
            }
            catch (Exception ex)
            {
                //LogManager.WriteLog(strProc + ex.Message, LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
                return false;
            }
        }

        #endregion Update UnitCashPointValue
    }
}
