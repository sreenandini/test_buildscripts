using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using BMC.Common.ExceptionManagement;
using BMC.CashDeskOperator.BusinessObjects;
using BMC.Transport.CashDeskOperatorEntity;
using Audit.BusinessClasses;
using Audit.Transport;
using BMC.Transport;
using BMC.Presentation.POS.Helper_classes;
using BMC.Common.LogManagement;

namespace BMC.Presentation
{
	/// <summary>
	/// Interaction logic for CVoid.xaml
	/// </summary>
    public partial class CPlayerClub : IDisposable
    {
        #region  Declaration
        string s_KeyText = "";       
        Dictionary<string, string> objPlayer;
        
        bool bRegistry = false;
        bool bTab = false; 
        string strPrintCopy = string.Empty;
   		PrizeInfoDTO listReedeem;
        IPlayerInformation playerInformationBusinessObject = PlayerInformationBusinessObject.CreateInstance();
        #endregion Declaration


        public CPlayerClub()
        {
            this.InitializeComponent();
            txtAcctNo.Focus();
            GetRegistrySetting();
        }


        private void GetRegistrySetting()
        {

          //  bRegistry = playerInformationBusinessObject.CheckForPrefixSuffixSetting();
        }


        private void txtAcctNo_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!BMC.Transport.Settings.OnScreenKeyboard)
                return;

            string sAccountNo;
            sAccountNo = DisplayNumberPad(txtAcctNo.Text);
            txtAcctNo.Text = sAccountNo;
            if (!string.IsNullOrEmpty(sAccountNo))
            {
                LoadPlayerInfo();
                UpdateToRedeemPoints();
            }
            txtAcctNo.SelectionStart = txtAcctNo.Text.Length;
		}

        private string DisplayKeyboard(string KeyText)
        {
            s_KeyText = "";
            KeyboardInterface objKeyboard = new KeyboardInterface();
            objKeyboard.Closing += new System.ComponentModel.CancelEventHandler(objKeyboard_Closing);
            objKeyboard.KeyString = KeyText;
            objKeyboard.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            objKeyboard.ShowDialog();
            return s_KeyText;
        }

        void objKeyboard_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (((KeyboardInterface)sender).DialogResult == true)
            {
                s_KeyText = ((KeyboardInterface)sender).KeyString;
            }
        }

        private void txtPrizeQty_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!BMC.Transport.Settings.OnScreenKeyboard)
                return;

            txtPrizeQty.Text = DisplayNumberPad(txtPrizeQty.Text);
            txtPrizeQty.SelectionStart = txtPrizeQty.Text.Length;
            txtAcctNo.Focus();
            txtPrizeQty.Focus();
        }

        private string DisplayNumberPad(string keytext)
        {
            string strNumberPadText = string.Empty;
            NumberPadWind ObjNumberpadWind = new NumberPadWind();
            ObjNumberpadWind.isPlayerClub = true;
            
            try
            {
               
                ObjNumberpadWind.ValueText = keytext;
               
                if (ObjNumberpadWind.ShowDialog() == true)
                {
                    if (ObjNumberpadWind.ValueText == "")
                    {
                        strNumberPadText = "0";
                    }
                    else
                    {
                        Regex objRegexNumberValidate = new Regex("^[0-9]+$", RegexOptions.IgnoreCase);
                        MatchCollection objMatchCollect;

                        if (ObjNumberpadWind.ValueText != "")
                        {
                            objMatchCollect = objRegexNumberValidate.Matches(ObjNumberpadWind.ValueText);
                            if (objMatchCollect.Count > 0)
                            {
                                strNumberPadText = GetPlayerInfo(ObjNumberpadWind.ValueText);
                            }
                            else
                            {
                                strNumberPadText = "1";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                strNumberPadText = ObjNumberpadWind.ValueText;
                ObjNumberpadWind.Close();
                ExceptionManager.Publish(ex);
                
            }
            return strNumberPadText;

        }
        private void btn_Up_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btn_Up.IsEnabled = false;
                Regex objRegexNumberValidate = new Regex("^[0-9]+$", RegexOptions.IgnoreCase);
                MatchCollection objMatchCollect;

                if (txtPrizeQty.Text != "")
                {
                    objMatchCollect = objRegexNumberValidate.Matches(txtPrizeQty.Text);
                    if (objMatchCollect.Count > 0)
                    {
                        txtPrizeQty.Text = ((Convert.ToInt32(txtPrizeQty.Text)) + 1).ToString();
                        UpdateToRedeemPoints();
                    }
                    else
                    {
                        MessageBox.ShowBox("MessageID83", BMC_Icon.Information);
                        txtPrizeQty.Text = string.Empty;
                        txtPrizeQty.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.ShowBox("MessageID84", BMC_Icon.Information);
                txtPrizeQty.Text = string.Empty;
            }
            finally
            {
                btn_Up.IsEnabled = true;
            }
        }

        private void btn_Down_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btn_Down.IsEnabled = false;
                Regex objRegexNumberValidate = new Regex("^[0-9]+$", RegexOptions.IgnoreCase);
                MatchCollection objMatchCollect;

                if (txtPrizeQty.Text != "")
                {
                    objMatchCollect = objRegexNumberValidate.Matches(txtPrizeQty.Text);
                    if (objMatchCollect.Count > 0)
                    {
                        txtPrizeQty.Text = ((Convert.ToInt32(txtPrizeQty.Text)) - 1).ToString();
                        if ((Convert.ToInt32(txtPrizeQty.Text)) > 0)
                        {
                            UpdateToRedeemPoints();
                        }
                        else if (Convert.ToInt32(txtPrizeQty.Text) == 0)
                        {
                            MessageBox.ShowBox("MessageID841", BMC_Icon.Information);
                            txtPrizeQty.Text = "1";
                            UpdateToRedeemPoints();
                        }
                        else
                        {
                            txtPrizeQty.Text = "1";
                        }
                    }
                    else
                    {
                        MessageBox.ShowBox("MessageID86", BMC_Icon.Information);
                        txtPrizeQty.Text = string.Empty;
                        txtPrizeQty.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.ShowBox("MessageID84", BMC_Icon.Information);
                txtPrizeQty.Text = string.Empty;
            }
            finally
            {
                btn_Down.IsEnabled = true;
            }
        }

        private void UpdateToRedeemPoints()
        {
            try
            {                
                if (String.IsNullOrEmpty(txtPrizeQty.Text)) txtPrizeQty.Text = "1";
                if (int.Parse(lblUnitPointValue.Content.ToString()) != 0 && int.Parse(txtPrizeQty.Text) != 0)
                {
                    PrizeInfoDTO listPrizeDTO = null;
                    if (lvRedeemPrizeList.Items.Count > 0)
                    {
                        if (lvRedeemPrizeList.SelectedItem != null)
                        {
                            listPrizeDTO = (PrizeInfoDTO)lvRedeemPrizeList.SelectedItem;
                            if (listPrizeDTO != null)
                            {
                                lblUnitPointValue.Content = Convert.ToString(int.Parse(listPrizeDTO.RedeemPoints) * int.Parse(txtPrizeQty.Text));
                                lblCashValue.Content = (oCommonUtilities.CreateInstance()).GetCurrency(double.Parse(listPrizeDTO.AuthAward.Substring(1)) * double.Parse(txtPrizeQty.Text));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return;
            }

        }

        private void txtPrizeQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                Regex objRegexNumberValidate = new Regex("^[0-9]+$", RegexOptions.IgnoreCase);
                MatchCollection objMatchCollect;

                if (txtPrizeQty.Text != "")
                {
                    objMatchCollect = objRegexNumberValidate.Matches(txtPrizeQty.Text);
                    if (objMatchCollect.Count > 0)
                    {
                        txtAcctNo.Text = GetPlayerInfo(txtAcctNo.Text);
                        LoadPlayerInfo();
                        UpdateToRedeemPoints();
                    }
                }
            }
        }

        private string GetPlayerInfo(string CardNumber)
        {

            string[] returnValue;

            returnValue = playerInformationBusinessObject.GetCardInformation(CardNumber);

            Regex Rx = new Regex(returnValue[0], RegexOptions.IgnoreCase);
            Regex RxInner = new Regex(returnValue[1], RegexOptions.IgnoreCase);

            if (Rx.Matches(CardNumber).Count >= 1)
            {
                if (Rx.Matches(CardNumber)[0].Value.Replace(";", "").Replace("?", "").Length > int.Parse(returnValue[2]))
                    return RxInner.Matches(Rx.Matches(CardNumber)[0].Value)[1].Value.Remove(0, int.Parse(returnValue[2]));
                else
                    return CardNumber;
            }
            else
                return CardNumber;
        }
        
        private List<Key> AllowedKeys = new List<Key> 
        {
            //Numbers 0-9
            Key.D0, 
            Key.D1,
            Key.D2,
            Key.D3,
            Key.D4,
            Key.D6,
            Key.D7,
            Key.D8,
            Key.D9,

            //Keypad 0-9
            Key.NumPad0,
            Key.NumPad1,
            Key.NumPad2,
            Key.NumPad3,
            Key.NumPad4,
            Key.NumPad5,
            Key.NumPad6,
            Key.NumPad7,
            Key.NumPad8,
            Key.NumPad9,

            //Backspace,Decimal,enter and delete keys
            Key.Enter,
            Key.Back,
            Key.Delete,
            Key.Tab,
        };
       
        private void CreatePlayerInfo()
        {
            try
            {                
                objPlayer = playerInformationBusinessObject.GetPlayerInfo(txtAcctNo.Text);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
                
        private void LoadPlayerInfo()
        {
            try
            {
                if (txtAcctNo.Text.Length > 0)
                {
                    
                    bool bFound = false;

                    string PrizeQty = txtPrizeQty.Text;
                    CreatePlayerInfo();
                    if (objPlayer != null)
                    {

                        if (objPlayer["AccountNumber"] == txtAcctNo.Text && !String.IsNullOrEmpty(objPlayer["FirstName"]))
                        {
                            lblPlayerName.Content = objPlayer["FirstName"] + " " + objPlayer["LastName"];                            
                            lblClubState.Content = objPlayer["ClubState"];
                            lblPointsBal.Content = objPlayer["PointsBalance"];
                                                        
                            FillPrizeList();
                            txtPrizeQty.Text = PrizeQty;
                            bFound = true;
                        }
                        else
                        {
                            bFound = false;
                        }
                       
                    }
                    if (bFound == false)
                    {
                        MessageBox.ShowBox("MessageID88", BMC_Icon.Information);
                        lblPlayerName.Content = string.Empty;                        
                        lblClubState.Content = string.Empty;
                        lblPointsBal.Content = string.Empty;
                        txtAcctNo.Text = string.Empty;

                        Binding bind = new Binding();
                        bind.Source = null;
                        lvRedeemPrizeList.SetBinding(ListView.ItemsSourceProperty, bind);                        
                        lblCashValue.Content = (oCommonUtilities.CreateInstance()).GetCurrency(0.00);
                        lblUnitPointValue.Content = "0";
                        lblPrizeDescription.Content = string.Empty;
                        lblPlayerName.Content = string.Empty;                       
                        lblClubState.Content = string.Empty;
                        lblPointsBal.Content = string.Empty;
                    }
                }
                else
                {
                    MessageBox.ShowBox("MessageID89", BMC_Icon.Information);
                    Binding bind = new Binding();
                    bind.Source = null;
                    lvRedeemPrizeList.SetBinding(ListView.ItemsSourceProperty, bind);                    
                    lblCashValue.Content = (oCommonUtilities.CreateInstance()).GetCurrency(0.00);
                    lblUnitPointValue.Content = "0";
                    lblPrizeDescription.Content = string.Empty;
                    lblPlayerName.Content = string.Empty;                   
                    lblClubState.Content = string.Empty;
                    lblPointsBal.Content = string.Empty;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void FillPrizeList()
        {
            List<PrizeInfoDTO> objPrizeList = null;
            Binding bind = null;
            try
            {
                objPrizeList = playerInformationBusinessObject.RetreivePrizeInfo(txtAcctNo.Text);
                IList<PrizeInfoDTO> objPrizeInfoDTOList = new List<PrizeInfoDTO>();
                PrizeInfoDTO objPrizeInfoDTO = null;
                
                if (objPrizeList != null)
                {
                    foreach (PrizeInfoDTO objPInfo in objPrizeList)
                    {
                        objPrizeInfoDTO = new PrizeInfoDTO();
                        objPrizeInfoDTO.AuthAward = CommonBusinessObjects.GetCurrency(objPInfo.AuthAward);
                        objPrizeInfoDTO.AwardUsed = objPInfo.AwardUsed.ToString();
                        objPrizeInfoDTO.BasePoints = objPInfo.BasePoints.ToString();
                        objPrizeInfoDTO.BonusPoints = objPInfo.BonusPoints.ToString();
                        objPrizeInfoDTO.PrizeId = objPInfo.PrizeId.ToString();
                        objPrizeInfoDTO.PrizeName = objPInfo.PrizeName.ToString();
                        objPrizeInfoDTO.RedeemPoints = objPInfo.RedeemPoints.ToString();
                        objPrizeInfoDTOList.Add(objPrizeInfoDTO);
                    }
                    bind = new Binding();
                    bind.Source = objPrizeInfoDTOList;
                    lvRedeemPrizeList.SetBinding(ListView.ItemsSourceProperty, bind);
                    bind.Source = null;
                }
                else
                {
                    bind = new Binding();
                    bind.Source = null;
                    lvRedeemPrizeList.SetBinding(ListView.ItemsSourceProperty, bind);                    
                    lblCashValue.Content = (oCommonUtilities.CreateInstance()).GetCurrency(0.00);
                    lblUnitPointValue.Content = "0";
                    lblPrizeDescription.Content = string.Empty;
                }                
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void lvRedeemPrizeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            try
            {
               if (lvRedeemPrizeList.Items.Count > 0)
                {
                    if (lvRedeemPrizeList.SelectedItem != null)
                    {
                        listReedeem = (PrizeInfoDTO)lvRedeemPrizeList.SelectedItem;

                        lblPrizeDescription.Content = listReedeem.PrizeName;
                        lblUnitPointValue.Content = listReedeem.RedeemPoints;
                        txtPrizeQty.Text = "1";
                        lblCashValue.Content = (oCommonUtilities.CreateInstance()).GetCurrency(double.Parse(listReedeem.AuthAward.Substring(1)) * double.Parse(txtPrizeQty.Text));
                        
                    }
                }
                if (int.Parse(lblUnitPointValue.Content.ToString()) != 0)
                {
                    UpdateToRedeemPoints();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnRedeem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.Wait;
                if (Settings.IsKioskRequired)
                {
                    if (lblUnitPointValue.Content.ToString().Length > 0 && int.Parse(lblUnitPointValue.Content.ToString()) > 0 && txtPrizeQty.Text.Length > 0 && int.Parse(txtPrizeQty.Text) > 0)
                    {
                        LoginInfoDTO objLoginInfo = playerInformationBusinessObject.GetLoginInformation();
                        PlayerInfoDTO objPlayerInfo = playerInformationBusinessObject.GetPlayerInformation(txtAcctNo.Text);
                        IPlayerInformation objCashDeskOperator = PlayerInformationBusinessObject.CreateInstance();
                        if (objCashDeskOperator.UpdateRedeempoints(txtAcctNo.Text, listReedeem.PrizeId, int.Parse(txtPrizeQty.Text),
                            int.Parse(listReedeem.RedeemPoints), objLoginInfo, objPlayerInfo) == true)
                        {
                            if (objCashDeskOperator.CheckEnableRedeemPrintCDO() == true)
                            {
                                EpsonReceiptPrint();
                            }
                            MessageBox.ShowBox("MessageID90", BMC_Icon.Information);

                            AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                            {

                                AuditModuleName = ModuleName.PlayerClub,
                                Audit_Screen_Name = "PlayerClub",
                                Audit_Desc = "Player club points redeemed",
                                AuditOperationType = OperationType.ADD,
                                Audit_Field = "Reedeem Points",
                                Audit_New_Vl = listReedeem.RedeemPoints
                            });

                            LoadPlayerInfo();
                            lblUnitPointValue.Content = listReedeem.RedeemPoints;
                            lblCashValue.Content = listReedeem.AuthAward;
                            txtPrizeQty.Text = "1";

                        }
                        else
                        {
                            MessageBox.ShowBox("MessageID91", BMC_Icon.Information);

                            AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                            {

                                AuditModuleName = ModuleName.PlayerClub,
                                Audit_Screen_Name = "PlayerClub",
                                Audit_Desc = "Unable to redeem points",
                                AuditOperationType = OperationType.ADD,

                            });
                        }
                    }
                    else if (txtAcctNo.Text == "0" || txtAcctNo.Text == "")
                    {
                        MessageBox.ShowBox("MessageID92", BMC_Icon.Information);
                    }
                    else if (txtPrizeQty.Text.Length <= 0)
                    {
                        MessageBox.ShowBox("MessageID93", BMC_Icon.Information);
                        txtPrizeQty.Text = "1";
                    }
                    else if (int.Parse(txtPrizeQty.Text) <= 0)
                    {
                        MessageBox.ShowBox("MessageID93", BMC_Icon.Information);
                        txtPrizeQty.Text = "1";
                    }
                    else if (txtAcctNo.Text != "")
                        LoadPlayerInfo();
                }

                else
                {
                    MessageBox.ShowBox("MessageID376", BMC_Icon.Information);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.ShowBox("MessageID94", BMC_Icon.Information);
                txtPrizeQty.Text = "1";
                ExceptionManager.Publish(ex);
            }
            finally
            {
                this.Cursor = Cursors.Arrow;
            }
       
        }
        private void txtAcctNo_LostFocus(object sender, RoutedEventArgs e)
        {                    
            txtAcctNo.Text = GetPlayerInfo(txtAcctNo.Text);
            if (txtAcctNo.Text.Length > 0)
            {
                if (Settings.IsKioskRequired)
                {
                    LoadPlayerInfo();
                }
                
            }
            else
            {
                MessageBox.ShowBox("MessageID95", BMC_Icon.Information);
                return;
            }
        }

        private void UpdateToRedeemPointsCash(string strPrizeName, int Unitpointvalue, float UnitCashvalue)
        {
            try
            {
                if (Unitpointvalue != 0)
                {
                    if ((PlayerInformationBusinessObject.CreateInstance()).UpdateUnitCashPoints(strPrizeName, Unitpointvalue, UnitCashvalue) == false)
                    {
                        MessageBox.ShowBox("MessageID96", BMC_Icon.Error);
                    }
                    LoadPlayerInfo();
                }
                else
                {
                    MessageBox.ShowBox("MessageID97", BMC_Icon.Information);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }

        private void GetAccDetails_Click(object sender, RoutedEventArgs e)
        {
            if (Settings.IsKioskRequired)
            {
                txtPrizeQty.Text = "1";
                LoadPlayerInfo();
                UpdateToRedeemPoints();
            }
            else
            {
                MessageBox.ShowBox("MessageID376", BMC_Icon.Information);
                return;
            }
        }

        public bool EpsonReceiptPrint()
        {
            bool bSuccess = false;
            try
            {
                strPrintCopy = "Retailer Copy";
                PrintDocument pDoc = new PrintDocument();
                pDoc.DocumentName = "Retailer Copy";
                pDoc.PrintPage += new PrintPageEventHandler(pDoc_PrintPage);
                pDoc.Print();
                
                strPrintCopy = "Customer Copy";
                pDoc = new PrintDocument();
                pDoc.DocumentName = "Customer Copy";
                pDoc.PrintPage += new PrintPageEventHandler(pDoc_PrintPage);
                pDoc.Print();
                bSuccess = true;
                return bSuccess;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                bSuccess = false;
                return bSuccess;
            }
        }

        void pDoc_PrintPage(object sender, PrintPageEventArgs ev)
        {
            try
            {
                float yPos;
                float leftMargin;
                float topMargin;
                Font printFont = new Font("Courier", 11, System.Drawing.FontStyle.Bold );
                
                char ctab = ' ';
                string sSiteName = "BMCX";
                DataSet objDSSite;
                
                objDSSite = (oCommonUtilities.CreateInstance()).GetSiteDetails();

                if (objDSSite != null)
                {                    
                    sSiteName = objDSSite.Tables[0].Rows[0]["Name"].ToString();
                }

                yPos = 0;
                // Left margin changed by Vineetha for setting data alignment in the receipt
                leftMargin = ev.MarginBounds.Left-100;
                topMargin = ev.MarginBounds.Top;
                String line = null;                
                
               
                string sPlayerName = lblPlayerName.Content.ToString();
                string sAccNo = txtAcctNo.Text;
                string sPrizeName = lblPrizeDescription.Content.ToString();
                double sUnitValue = Convert.ToDouble(lblUnitPointValue.Content);//lblUnitPointValue.Content.ToString();
                string sQty = txtPrizeQty.Text;
                double sTotalPoints = Convert.ToDouble(lblUnitPointValue.Content);//Convert.ToString(lblPointsBal.Content);
                string sTotalCash = Convert.ToString(lblCashValue.Content);//Convert.ToDouble(lblCashValue.Content);
                string sUserName = MainScreen.StrUserName;

                line = "Player Club Redemption Voucher";
                yPos = topMargin + (printFont.GetHeight(ev.Graphics));
                ev.Graphics.DrawString(line, printFont, System.Drawing.Brushes.Black, leftMargin, yPos);

                printFont = new Font("Courier", 10, System.Drawing.FontStyle.Regular);
                line = strPrintCopy;
                yPos = topMargin + (3 * printFont.GetHeight(ev.Graphics));
                ev.Graphics.DrawString(line, printFont, System.Drawing.Brushes.Black, leftMargin, yPos, new StringFormat());

                line = sSiteName;
                yPos = topMargin + (5 * printFont.GetHeight(ev.Graphics));
                ev.Graphics.DrawString(line, printFont, System.Drawing.Brushes.Black, leftMargin, yPos, new StringFormat());

                line = sPlayerName;
                yPos = topMargin + (7 * printFont.GetHeight(ev.Graphics));
                ev.Graphics.DrawString(line, printFont, System.Drawing.Brushes.Black, leftMargin, yPos, new StringFormat());

                line = "Acct:".PadRight(7,ctab) + sAccNo;
                yPos = topMargin + (8 * printFont.GetHeight(ev.Graphics));
                ev.Graphics.DrawString(line, printFont, System.Drawing.Brushes.Black, leftMargin, yPos, new StringFormat());

                line = sPrizeName;
                yPos = topMargin + (10 * printFont.GetHeight(ev.Graphics));
                ev.Graphics.DrawString(line, printFont, System.Drawing.Brushes.Black, leftMargin, yPos, new StringFormat());

                // needs gap

                line = "Unit Value".PadRight(40,ctab) + String.Format("{0:0,0}", sUnitValue);
                yPos = topMargin + (12 * printFont.GetHeight(ev.Graphics));
                ev.Graphics.DrawString(line, printFont, System.Drawing.Brushes.Black, leftMargin, yPos);

                line = "Qty".PadRight(45, ctab) + String.Format("{0:0,0}", sQty);
                yPos = topMargin + (13 * printFont.GetHeight(ev.Graphics));
                ev.Graphics.DrawString(line, printFont, System.Drawing.Brushes.Black, leftMargin, yPos);

                line = "Total Points Redeemed".PadRight(29, ctab) + String.Format("{0:0,0}", sTotalPoints);
                yPos = topMargin + (14 * printFont.GetHeight(ev.Graphics));
                ev.Graphics.DrawString(line, printFont, System.Drawing.Brushes.Black, leftMargin, yPos);

                line = "Total Cash Equivalent".PadRight(32, ctab) + String.Format("{0:0,0.00}", sTotalCash);
                yPos = topMargin + (15 * printFont.GetHeight(ev.Graphics));
                ev.Graphics.DrawString(line, printFont, System.Drawing.Brushes.Black, leftMargin, yPos);

                line = System.DateTime.Now.ToShortDateString() +  " " + System.DateTime.Now.ToShortTimeString() + sUserName.ToUpper().PadLeft(21,ctab);
                yPos = topMargin + (18 * printFont.GetHeight(ev.Graphics));
                ev.Graphics.DrawString(line, printFont, System.Drawing.Brushes.Black, leftMargin, yPos);

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }            
        }

        private void txtPrizeQty_LostFocus(object sender, RoutedEventArgs e)
        {
            Regex objRegexNumberValidate = new Regex("^[0-9]+$", RegexOptions.IgnoreCase);
            MatchCollection objMatchCollect;           

            if (!bTab)
            {
                if (txtPrizeQty.Text.Length > 0)
                {
                    objMatchCollect = objRegexNumberValidate.Matches(txtPrizeQty.Text);
                    if (objMatchCollect.Count > 0)
                    {
                        LoadPlayerInfo();
                        UpdateToRedeemPoints();
                    }
                    else
                    {
                      //  MessageBox.ShowBox("MessageID83", BMC_Icon.Information);
                        txtPrizeQty.Text = string.Empty;
                        txtPrizeQty.Focus();
                        return;
                    }
                }
               
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            txtAcctNo.Focus();
        }

        #region IDisposable Members

        /// <summary>
        /// Variable used to identity whether this object is already disposed or not.
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        private void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    this.CleanupWPFObjectTopControls((i) =>
                    {
                        // events
                        this.UserControl.Loaded -= (this.UserControl_Loaded);
                        this.txtAcctNo.PreviewMouseUp -= (this.txtAcctNo_PreviewMouseUp);
                        this.txtAcctNo.KeyDown -= (this.txtPrizeQty_KeyDown);
                        this.lvRedeemPrizeList.SelectionChanged -= (this.lvRedeemPrizeList_SelectionChanged);
                        this.txtPrizeQty.PreviewMouseUp -= (this.txtPrizeQty_PreviewMouseUp);
                        this.txtPrizeQty.KeyDown -= (this.txtPrizeQty_KeyDown);
                        this.txtPrizeQty.LostFocus -= (this.txtPrizeQty_LostFocus);
                        this.GetAccDetails.Click -= (this.GetAccDetails_Click);
                        this.btn_Up.Click -= (this.btn_Up_Click);
                        this.btn_Down.Click -= (this.btn_Down_Click);
                    },
                    (c) =>
                    {
                    });
                    LogManager.WriteLog("|=> CPlayerClub objects are released successfully.", LogManager.enumLogLevel.Info);
                }
                disposed = true;
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="CPlayerClub"/> is reclaimed by garbage collection.
        /// </summary>
        ~CPlayerClub()
        {
            Dispose(false);
        }

        #endregion
    }
}