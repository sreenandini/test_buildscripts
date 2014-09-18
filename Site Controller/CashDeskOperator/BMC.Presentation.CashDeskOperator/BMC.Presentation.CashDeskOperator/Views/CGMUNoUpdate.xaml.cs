using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BMC.CashDeskOperator;
using System.Collections.ObjectModel;
using BMC.Common.ExceptionManagement;
using Microsoft.Windows.Controls;
using BMC.Business.CashDeskOperator.WebServices;
using BMC.Transport;
using BMC.Common.LogManagement;
using System.Threading;
using BMC.Business.CashDeskOperator;
using BMC.DBInterface.CashDeskOperator;
using System.ComponentModel;
using System.Collections;
using Audit.BusinessClasses;
using Audit.Transport;

namespace BMC.Presentation.POS.Views
{
    /// <summary>
    /// Interaction logic for CGMUNoUpdate.xaml
    /// </summary>
    public partial class CGMUNoUpdate : UserControl
    {
        #region Private Variables
        private List<BMC.Transport.AGSdetails> lst_ags = null;
        private const string strScreenName = "GMUNo Update==> ";
        private const string strNULL = "NULL";
       
        private string strKeyText = "";
        private string[] strKeypad = { "txtF_Asset"};

        //private AutoResetEvent ar_wait = new AutoResetEvent(true);
        //private int RetryCount = 3;
        //private Dictionary<string, Func<int, int, bool>> di_Method = new Dictionary<string, Func<int, int, bool>>();
        private IDictionary<string, string> dic_filter = new Dictionary<string, string>();
        #endregion

        #region Constructor
        public CGMUNoUpdate()
        {
            InitializeComponent();
        }
        #endregion

        #region Vaildation Methods
        /// <summary>
        /// Validating Existence of Asset,New GMU NO, Serial No
        /// </summary>
        /// <param name="item_ags"></param>
        /// <param name="ErrorMsg"></param>
        /// <returns></returns>
        private bool Vaildate(BMC.Transport.AGSdetails item_ags, ref string ErrorMsg)
        {
            bool retval = true;

            try
            {
                int mygmuno = 0;
                int.TryParse(item_ags.GMUNo, out mygmuno);
                if (mygmuno == 0)
                {
                    ErrorMsg = "MessageID524";
                    retval = false;
                }
                else if (mygmuno > 0 && mygmuno > 99999)
                {
                    ErrorMsg = "MessageID525";
                    retval = false;
                }
                else if (UpdateGMUNo.CreateInstance().CheckAGSCombination(item_ags.ActualAssetNo, item_ags.GMUNo, item_ags.SerialNo))
                {
                    ErrorMsg = "MessageID510";
                    retval = false;
                }
            }
            catch (Exception ex)
            {
                retval = false;
                ErrorMsg = "MessageID511";
                ExceptionManager.Publish(ex);
            }
            return retval;
        }

        #endregion

        #region Miscellaneous Methods
        ///// <summary>
        ///// This Method will retry around certain number of times if it fails
        ///// </summary>
        ///// <param name="Key"></param>
        ///// <param name="Installation_No"></param>
        ///// <param name="Bar_Pos_Port"></param>
        ///// <param name="DisableMachine"></param>
        ///// <returns></returns>
        //private bool InvokeMethod(string Key, int Installation_No, int Bar_Pos_Port, int DisableMachine)
        //{
        //    bool retVal = false;
        //    int Count = 0;

        //    try
        //    {
        //        while (Count < RetryCount)
        //        {
        //            ar_wait.WaitOne(2000);
        //            if (Key == "RemoveMachineFromPollingList")
        //            {
        //                if (di_Method[Key].Invoke(Installation_No, DisableMachine))
        //                {
        //                    ar_wait.Set();
        //                    retVal = true;
        //                    break;
        //                }
        //            }
        //            else
        //            {
        //                if (di_Method[Key].Invoke(Installation_No, Bar_Pos_Port))
        //                {
        //                    ar_wait.Set();
        //                    retVal = true;
        //                    break;
        //                }
        //            }
        //            Count++;
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        ExceptionManager.Publish(Ex);
        //    }
        //    return retVal;
        //}

        /// <summary>
        /// Remove machine from polling list and then adding to polling list with same installtion 
        /// </summary>
        /// <param name="item_ags"></param>
        /// <returns>true or false</returns>
        private bool SendDetailsToExcomms(BMC.Transport.AGSdetails item_ags)
        {
            bool retVal = false;
            try
            {
                EnrollmentDataAccess enrollmentDataAccess = new EnrollmentDataAccess();
                int Bar_Pos_Port = enrollmentDataAccess.GetBarPosPort(item_ags.Installation_No.Value);

                if (item_ags.IsNull)
                {
                    if (UpdateGMUNo.CreateInstance().RemoveMachineFromPollingList(item_ags.Installation_No.Value, 0))
                    {
                        LogManager.WriteLog(strScreenName + " Remove Machine From PollingList Succeed InstallationNo:" + item_ags.Installation_No.Value, LogManager.enumLogLevel.Info);
                        LogManager.WriteLog(strScreenName + "'NULL' string updated successfully for an asset :" + item_ags.AssetNo, LogManager.enumLogLevel.Info);
                        bool retGMUEvent = UpdateGMUNo.CreateInstance().UpdateGMUDownEvent(item_ags.Installation_No.Value);
                        LogManager.WriteLog(strScreenName + "'GMUDownEvent inserted - " + retGMUEvent.ToString() + " -  for an asset :" + item_ags.AssetNo, LogManager.enumLogLevel.Info);
                        retVal = true;
                    }
                    else
                    {
                        BMC.Presentation.MessageBox.ShowBox("MessageID208", BMC_Icon.Error);
                        LogManager.WriteLog(strScreenName + "Unable to Remove Machine From PollingList InstallationNo:" + item_ags.Installation_No.Value, LogManager.enumLogLevel.Info);
                    }
                }
                else
                {
                    if (UpdateGMUNo.CreateInstance().AddToPollingList(item_ags.Installation_No.Value, Bar_Pos_Port))
                    {
                        LogManager.WriteLog(strScreenName + "Add Machine To PollingList Succeed InstallationNo:" + item_ags.Installation_No.Value, LogManager.enumLogLevel.Info);
                        retVal = true;
                    }
                    else
                    {
                        BMC.Presentation.MessageBox.ShowBox("MessageID508", BMC_Icon.Error);
                        LogManager.WriteLog(strScreenName + "Unable to Add Machine To PollingList InstallationNo:" + item_ags.Installation_No.Value, LogManager.enumLogLevel.Info);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return retVal;
        }

        /// <summary>
        /// Gets Current Item from List
        /// </summary>
        /// <param name="AssetNo"></param>
        /// <returns></returns>
        private BMC.Transport.AGSdetails GetCurrentItem(string AssetNo)
        {
            return lst_ags.Find(o => o.AssetNo == AssetNo);
        }

        private void ClearFilters()
        {
            txtF_ActualAssetNo.Text = "";
            txtF_Asset.Text = "";
            txtF_BarPos.Text = "";
            txtF_GMUNo.Text = "";
            txtF_SerialNo.Text = "";
        }

        private List<AGSdetails> EqualsOperator()
        {
            List<AGSdetails> lstFilter = new List<AGSdetails>();

            foreach (KeyValuePair<string, string> kv in dic_filter)
            {
                List<AGSdetails> lstFilterTemp = (lst_ags.FindAll(obj => GetPropValue(obj, kv.Key).ToString().StartsWith(kv.Value, StringComparison.InvariantCultureIgnoreCase)));
                if (lstFilter.Count > 0)
                {
                    lstFilter = lstFilter.Intersect(lstFilterTemp).ToList();
                }
                else
                {
                    if (lstFilterTemp.Count == 0)
                    {
                        break;
                    }
                    lstFilter.AddRange(lstFilterTemp);
                }
            }
            return lstFilter;
        }

        public static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }
        #endregion

        #region Events
        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                TextBox txtFilter = sender as TextBox;
                ICollectionView view = CollectionViewSource.GetDefaultView(lvGMUNo.ItemsSource);

                if (dic_filter.ContainsKey(txtFilter.Tag.ToString()))
                {
                    if (txtFilter.Text.Trim() == "")
                    {
                        dic_filter.Remove(txtFilter.Tag.ToString());
                    }
                    else
                    {
                        dic_filter[txtFilter.Tag.ToString()] = txtFilter.Text;
                    }
                }
                else
                {
                    if (txtFilter.Text.Trim() != "")
                    {
                        dic_filter.Add(txtFilter.Tag.ToString(), txtFilter.Text);
                    }
                }

                List<AGSdetails> temp_ags = EqualsOperator();
                view.Filter = null;
                if (dic_filter.Count > 0)
                {

                    view.Filter = obj =>
                    {
                        AGSdetails ags = obj as AGSdetails;
                        return temp_ags.Contains(ags);
                    };
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }

        private void chkFilter_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!chkFilter.IsChecked.Value)
                {
                    ClearFilters();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btn_ClearFilter_Click(object sender, RoutedEventArgs e)
        {
            ClearFilters();
        }

        private void txt_GMUNo_GotFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                TextBox txtGMUNo = sender as TextBox;
                BMC.Transport.AGSdetails item_ags = GetCurrentItem(txtGMUNo.Tag.ToString());
                if (item_ags != null && txtGMUNo.Text.Trim() != "")
                {
                    item_ags.OldGMUNo = txtGMUNo.Text.Trim();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void txt_GMUNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {

                //if (!Char.IsDigit((char)KeyInterop.VirtualKeyFromKey(e.Key)) & e.Key != Key.Back | e.Key == Key.Space)
                //{
                //    e.Handled = true;
                //}
                e.Handled = !IsNumberKey(e.Key) && !IsDelOrBackspaceOrTabKey(e.Key);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private bool IsNumberKey(Key inKey)
        {
            if (inKey < Key.D0 || inKey > Key.D9)
            {
                if (inKey < Key.NumPad0 || inKey > Key.NumPad9)
                {
                    return false;
                }
            }
            return true;
        }

        private bool IsDelOrBackspaceOrTabKey(Key inKey)
        {
            return inKey == Key.Delete || inKey == Key.Back || inKey == Key.Tab;
        }

        private void txt_GMUNo_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                TextBox txtGMUNo = sender as TextBox;
                if (txtGMUNo.Text != strNULL)
                {
                    int myint = 0;
                    if (!int.TryParse(txtGMUNo.Text, out myint))
                    {
                        txtGMUNo.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }


        #endregion

        #region Load Methods
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                LogManager.WriteLog(strScreenName + "Get Machine AGS Combination Details", LogManager.enumLogLevel.Info);
                lst_ags = UpdateGMUNo.CreateInstance().GetAGSDetails();//Load Machine Details (i.e. GMUNo,AssetNo)

                lvGMUNo.ItemsSource = lst_ags;

                if (Settings.AGSValue.Trim() != "16")
                {
                    border_GMU.IsEnabled = false;
                }
                else
                {
                    txtGMUDisable.Visibility = Visibility.Hidden;
                }
                //di_Method.Add("RemoveMachineFromPollingList", UpdateGMUNo.CreateInstance().RemoveMachineFromPollingList);
                //di_Method.Add("AddToPollingList", UpdateGMUNo.CreateInstance().AddToPollingList);
                //RetryCount = Convert.ToInt32(BMC.Common.ConfigurationManagement.ConfigManager.Read("GMUNoUpdateRetryCount"));
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }

        }
        #endregion

        #region Updation Methods

        private void btn_EditSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button bt_edit = sender as Button;
                BMC.Transport.AGSdetails item_ags = GetCurrentItem(bt_edit.Tag.ToString());
                lvGMUNo.SelectedItem = item_ags;

                if (item_ags.EditSave == "Edit")
                {
                    item_ags.IsNull = false;
                    int tmpGMU = 0;
                    item_ags.OldGMUNo = item_ags.GMUNo;
                    if (int.TryParse(item_ags.GMUNo, out tmpGMU))
                    {
                        item_ags.GMUNo = strNULL;
                        item_ags.IsNull = true;
                    }
                    else
                    {
                        item_ags.GMUNo = "";
                    }
                    item_ags.IsEnabled = !item_ags.IsNull;
                    item_ags.EditSave = "Save";

                }
                else
                {
                    item_ags.IsEnabled = false;
                    item_ags.EditSave = "Edit";
                    string ErrorMsg = "";
                    item_ags.OldGMUNo = item_ags.OldGMUNo.Trim();
                    int tmpGMU = 0;
                    if (int.TryParse(item_ags.GMUNo, out tmpGMU))
                    {
                        item_ags.GMUNo = tmpGMU.ToString();
                    }
                    else
                    {
                        item_ags.GMUNo = item_ags.GMUNo.Trim();
                    }

                    if (item_ags.IsNull || Vaildate(item_ags, ref ErrorMsg))
                    {
                        if (BMC.Presentation.MessageBox.ShowBox("MessageID509", BMC_Icon.Question, BMC_Button.YesNo) == System.Windows.Forms.DialogResult.Yes)
                        {
                            SaveAGSCombination(item_ags);
                            item_ags.OldGMUNo = item_ags.GMUNo;
                        }
                        else
                        {
                            item_ags.GMUNo = item_ags.OldGMUNo;
                        }
                    }
                    else
                    {
                        LogManager.WriteLog(strScreenName + "Vaildation Failed ErrorMsg:" + ErrorMsg, LogManager.enumLogLevel.Info);
                        BMC.Presentation.MessageBox.ShowBox(ErrorMsg, BMC_Icon.Error);
                        item_ags.GMUNo = item_ags.OldGMUNo;
                    }

                }

            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }

        }

        /// <summary>
        /// Send AGS details to enterprise
        /// </summary>
        /// <param name="item_ags"></param>
        private void SaveAGSCombination(BMC.Transport.AGSdetails item_ags)
        {
            try
            {
                string strXml = UpdateGMUNo.CreateInstance().ExportAGSDetails(item_ags.MachineID, item_ags.GMUNo);
                Proxy oProxy = new Proxy(Settings.SiteCode);

                if (oProxy.UpdateGMUNumber(strXml))//Send AGS Details to Enterprise
                {
                    LogManager.WriteLog(strScreenName + "Enterprise GMU Number Updated Successfully", LogManager.enumLogLevel.Info);

                    if (UpdateGMUNo.CreateInstance().UpdateGMUDetails(item_ags.MachineID, item_ags.GMUNo, item_ags.Installation_No.Value))//After Vaildating in Enterprise Updating GMUNo in Exchange
                    {
                        if (SendDetailsToExcomms(item_ags))//Send to Excomms i.e. GMU No is modified
                        {
                            BMC.Presentation.MessageBox.ShowBox("MessageID506", BMC_Icon.Information);
                            LogManager.WriteLog(strScreenName + "Exchange GMU Number Updated Successfully GMUNo: " + item_ags.OldGMUNo + "-->" + item_ags.GMUNo, LogManager.enumLogLevel.Info);
                            AuditModule("GMU Updated Successfully for an Asset ", item_ags.AssetNo, item_ags.OldGMUNo, item_ags.GMUNo);

                        }
                        else
                        {
                            AuditModule("GMU Updated Failed for an Asset ", item_ags.AssetNo, item_ags.OldGMUNo, item_ags.GMUNo);
                            UpdateGMUNo.CreateInstance().UpdateGMUDetails(item_ags.MachineID, item_ags.OldGMUNo, item_ags.Installation_No.Value);
                            LogManager.WriteLog(strScreenName + "Rollback GMU Number Updation in Exchange GMUNo: " + item_ags.GMUNo + "-->" + item_ags.OldGMUNo, LogManager.enumLogLevel.Info);
                            strXml = UpdateGMUNo.CreateInstance().ExportAGSDetails(item_ags.MachineID, item_ags.OldGMUNo);
                            oProxy.UpdateGMUNumber(strXml);
                            LogManager.WriteLog(strScreenName + "Rollback GMU Number Updation in Enterprise GMUNo: " + item_ags.GMUNo + "-->" + item_ags.OldGMUNo, LogManager.enumLogLevel.Info);
                            item_ags.GMUNo = item_ags.OldGMUNo;
                            AuditModule("Rollback GMU Number Updation for an Asset ", item_ags.AssetNo, item_ags.GMUNo, item_ags.OldGMUNo);
                        }
                    }
                    else
                    {
                        AuditModule("GMU Updated Failed for an Asset ", item_ags.AssetNo, item_ags.OldGMUNo, item_ags.GMUNo);
                        BMC.Presentation.MessageBox.ShowBox("MessageID507", BMC_Icon.Information);
                        LogManager.WriteLog(strScreenName + "Exchange GMU Number Updated Failed for GMUNo: " + item_ags.GMUNo, LogManager.enumLogLevel.Info);
                        item_ags.GMUNo = item_ags.OldGMUNo;
                    }
                }
                else
                {
                    BMC.Presentation.MessageBox.ShowBox("MessageID529", BMC_Icon.Information);
                    // AuditModule("GMU Updated Failed for an Asset ", item_ags.AssetNo, item_ags.OldGMUNo, item_ags.GMUNo);
                    LogManager.WriteLog(strScreenName + "Enterprise GMU Number Updated Failed for GMUNo: " + item_ags.GMUNo, LogManager.enumLogLevel.Info);
                    item_ags.GMUNo = item_ags.OldGMUNo;
                }

            }
            catch (Exception Ex)
            {
                AuditModule("GMU Updated Failed for an Asset ", item_ags.AssetNo, item_ags.OldGMUNo, item_ags.GMUNo);
                BMC.Presentation.MessageBox.ShowBox("MessageID507", BMC_Icon.Information);
                item_ags.GMUNo = item_ags.OldGMUNo;
                ExceptionManager.Publish(Ex);
            }

        }
        #endregion

        #region VirtualKeyboardMethods
        private void txt_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (!Settings.OnScreenKeyboard)
                    return;
                TextBox txtMouseUp = sender as TextBox;
                if (strKeypad.Any(o => o.ToString() == txtMouseUp.Name))
                {
                    txtMouseUp.Text = DisplayKeyboard(txtMouseUp.Text);
                }
                else
                {
                    string strNumberPadValue = DisplayNumberPad(txtMouseUp.Text.Equals(strNULL) ? "" : txtMouseUp.Text);
                    //  strNumberPadValue = (strNumberPadValue.Trim().Length == 0) ? "0" : strNumberPadValue;
                    if (txtMouseUp.Name == "txt_GMUNo")
                    {
                        txtMouseUp.Text = (strNumberPadValue.Length <= 5) ? strNumberPadValue : "";
                    }
                    else
                    {
                        txtMouseUp.Text = strNumberPadValue;
                    }
                }
                txtMouseUp.SelectionStart = txtMouseUp.Text.Length;


            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public string DisplayKeyboard(string keyText)
        {
            strKeyText = "";
            KeyboardInterface objKeyboard = null;

            try
            {
                Window w = Window.GetWindow(this);
                Point pt = default(Point);
                Size sz = default(Size);
                if (w != null)
                {
                    pt = new Point(w.Left, w.Top);
                    sz = new Size(w.Width, w.Height);
                }

                objKeyboard = new KeyboardInterface();
                objKeyboard.Owner = w;
                objKeyboard.Closing += new System.ComponentModel.CancelEventHandler(objKeyboard_Closing);
                objKeyboard.KeyString = keyText;
                objKeyboard.Top = pt.Y + (sz.Height - objKeyboard.Height);
                objKeyboard.Left = pt.X + (sz.Width / 2) - (objKeyboard.Width / 2);
                objKeyboard.ShowInTaskbar = false;
                objKeyboard.ShowDialog();

                if (objKeyboard != null)
                {
                    objKeyboard.Closing -= this.objKeyboard_Closing;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {

            }
            return strKeyText;
        }

        private string DisplayNumberPad(string keytext)
        {
            string strNumberPadText = string.Empty;
            NumberPadWind ObjNumberpadWind = new NumberPadWind();

            try
            {

                ObjNumberpadWind.ValueText = keytext;

                if (ObjNumberpadWind.ShowDialog() == true)
                {
                    strNumberPadText = ObjNumberpadWind.ValueText;
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

        void objKeyboard_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside objKeyboard_Closing", LogManager.enumLogLevel.Info);

                if (((KeyboardInterface)sender).DialogResult == true)
                {
                    strKeyText = ((KeyboardInterface)sender).KeyString;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        #endregion

        #region Audit Module
        private void AuditModule(string Description, string AssetNo, string OldGMUNo, string NewGMUNo)
        {
            try
            {
                AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                {

                    AuditModuleName = ModuleName.UpdateGmuNo,
                    Audit_Screen_Name = "Update GMU No For Asset",
                    Audit_Desc = Description + " GMUNo: " + OldGMUNo + "-->" + NewGMUNo,
                    AuditOperationType = OperationType.MODIFY,
                    Audit_Slot = AssetNo,
                    Audit_Field = "GMUNo",
                    Audit_Old_Vl = OldGMUNo,
                    Audit_New_Vl = NewGMUNo
                });
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }

        }
        #endregion

    }


}

