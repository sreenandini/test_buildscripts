using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.Transport.CashDeskOperatorEntity;
using BMC.CashDeskOperator.BusinessObjects;
using BMC.Transport;
using BMC.Presentation.POS.Helper_classes;

namespace BMC.Presentation
{
    /// <summary>
    /// Interaction logic for MachineEnrolment.xaml
    /// </summary>
    public partial class MachineEnrolment : Window, IDisposable
    {
        private bool IsEnrol;
        private string Position;
        string AssetNo;
        string s_KeyText;
        PositionDetails objPositiondetails;
        IEnrollment Enrollment = EnrollmentBusinessObject.CreateInstance();        

        public MachineEnrolment(bool IsEnrol, string Position)
        {
            this.InitializeComponent();
            MessageBox.childOwner = this;
            this.IsEnrol = IsEnrol;
            this.Position = Position;
            txt_Position.Text = Position;
            com_GMU_Type.Items.Add("MC300");
            com_GMU_Type.Items.Add("Connexus");
            com_GMU_Type.Items.Add("NoDevice");
            com_GMU_Type.SelectedIndex = 0;

            if (IsEnrol)
            {
                panel_Enrol_Button.Visibility = Visibility.Visible;
                panel_Remove_Button.Visibility = Visibility.Hidden;
                txt_MC_AssetNo.PreviewMouseUp += new MouseButtonEventHandler(txt_MC_AssetNo_PreviewMouseUp);
                txt_GMU_No.PreviewMouseUp += new MouseButtonEventHandler(txt_GMU_No_PreviewMouseUp);
            }
            else
            {
                panel_Enrol_Button.Visibility = Visibility.Hidden;
                panel_Remove_Button.Visibility = Visibility.Visible;
                objPositiondetails = Enrollment.GetPositionDetails(Position);
                txt_Mc_Serial_No.Text = objPositiondetails.SerialNo;
                txt_Mc_Alt_Serial_No.Text = objPositiondetails.AltSerialNo;
                txt_Manufacturer.Text = objPositiondetails.Manufacturer;
                txt_Mc_Type.Text = objPositiondetails.MachineType;
                txt_GameCode.Text = objPositiondetails.GameCode;
                txt_GameCat.Text = objPositiondetails.GameCategory;
                txt_Game.Text = objPositiondetails.Game;
                txt_GMU_No.Text = objPositiondetails.GMUNo;
                txt_MC_AssetNo.Text = objPositiondetails.AssetNo;
                lbl_Title.Text = "Machine Removal";

                if (txt_GMU_No.Text.Trim().Length == 0 || txt_GMU_No.Text.Trim() == "0")
                    com_GMU_Type.SelectedIndex = 2;

                txt_GMU_No.IsReadOnly = true;
                txt_Position.IsReadOnly = true;
                txt_MC_AssetNo.IsReadOnly = true;
            }
        }

        void txt_GMU_No_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            string GMUNo;
            if (txt_GMU_No.IsReadOnly)
                return;
            GMUNo = DisplayNumberPad(txt_GMU_No.Text);
            txt_GMU_No.Text = GMUNo;
            txt_GMU_No.SelectionStart = txt_GMU_No.Text.Length;
        }

        private void btn_Exit_Click(object sender, RoutedEventArgs e)
        {
            if (btnEnrol.Content.ToString() == "Install Machine")
            {
                btnEnrol.Content = "Get Details";
                txt_Mc_Serial_No.Text = "";
                txt_Mc_Alt_Serial_No.Text = "";
                txt_Manufacturer.Text = "";
                txt_Mc_Type.Text = "";
                txt_GameCode.Text = "";
                txt_GameCat.Text = "";
                txt_Game.Text = "";
                txt_GMU_No.Text = "";
                txt_MC_AssetNo.Text = "";
                return;
            }
            this.Close();
        }

        private void btnremove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnremove.IsEnabled = false;
                if (MessageBox.ShowBox("MessageID4", BMC_Icon.Question, BMC_Button.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    MachineRemoval objMachineRemoval = new MachineRemoval(objPositiondetails);
                    objMachineRemoval.Owner = this;
                    objMachineRemoval.ShowDialogEx(this);
                }
            }
            finally
            {
                btnremove.IsEnabled = true;
            }
        }

        private void btnChangeGame_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.ShowBox("MessageID6");
        }

        private void EnrollMachine()
        {
            MessageBox.ShowBox("MessageID6", BMC_Icon.Warning, BMC_Button.OK);

        }

        private void btnEnrol_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnEnrol.IsEnabled = false;
                if (btnEnrol.Content.ToString() == "Install Machine")
                {
                    EnrollMachine();
                    return;
                }
                int SuccessCode;

                try
                {

                    AssetNo = txt_MC_AssetNo.Text.Trim();
                    DataTable dtAssetDetails = Enrollment.GetAssetDetails(AssetNo, Settings.SiteCode);

                    if (dtAssetDetails != null)
                    {
                        SuccessCode = Int32.Parse(dtAssetDetails.Rows[0][0].ToString());

                        switch (SuccessCode)
                        {
                            case 0:
                                txt_Mc_Serial_No.Text = dtAssetDetails.Rows[0]["SerialNo"].ToString();
                                txt_Mc_Alt_Serial_No.Text = dtAssetDetails.Rows[0]["AltSerialNo"].ToString();
                                txt_Manufacturer.Text = dtAssetDetails.Rows[0]["Manufacturer_Name"].ToString();
                                txt_Mc_Type.Text = dtAssetDetails.Rows[0]["MachineTypeCode"].ToString();
                                txt_GameCode.Text = dtAssetDetails.Rows[0]["GameCode"].ToString();
                                txt_GameCat.Text = dtAssetDetails.Rows[0]["GameCategory"].ToString();
                                txt_Game.Text = dtAssetDetails.Rows[0]["Game"].ToString();
                                btnEnrol.Content = "Install Machine";
                                break;
                            case -1:
                                MessageBox.ShowBox("MessageID7", BMC_Icon.Information, BMC_Button.OK, AssetNo);
                                break;
                            case -2:
                                MessageBox.ShowBox("MessageID8", BMC_Icon.Information, BMC_Button.OK, AssetNo, dtAssetDetails.Rows[0]["Site_Name"].ToString(), dtAssetDetails.Rows[0]["Bar_Position_Name"].ToString());
                                break;
                            case -3:
                                MessageBox.ShowBox("MessageID342", BMC_Icon.Information, BMC_Button.OK, AssetNo, dtAssetDetails.Rows[0]["Site_Name"].ToString());
                                break;
                        }

                    }

                }


                catch (Exception ex)
                {

                    LogManager.WriteLog(ex.Message.ToString(), LogManager.enumLogLevel.Error);
                }
            }
            finally
            {
                btnEnrol.IsEnabled = true;
            }

        }

        private string DisplayKeyboard(string KeyText, string type)
        {
            s_KeyText = "";
            KeyboardInterface objKeyboard = new KeyboardInterface();
            if (type == "Pwd")
            {
                objKeyboard.IsPwd = true;
            }
            objKeyboard.Closing += new System.ComponentModel.CancelEventHandler(objKeyboard_Closing);
            objKeyboard.KeyString = KeyText;
            objKeyboard.Top = this.Top + this.Height - objKeyboard.Height;
            objKeyboard.Left = this.Left + this.Width / 2 - objKeyboard.Width / 2;
            objKeyboard.ShowDialogEx(this);
            return s_KeyText;
        }

        void objKeyboard_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (((KeyboardInterface)sender).DialogResult == true)
            {
                s_KeyText = ((KeyboardInterface)sender).KeyString;
            }
        }

        private string DisplayNumberPad(string keytext)
        {
            string strNumberPadText = string.Empty;
            NumberPadWind ObjNumberpadWind = new NumberPadWind();
            //ObjNumberpadWind.isPlayerClub = true;

            try
            {

                ObjNumberpadWind.ValueText = keytext;

                if (ObjNumberpadWind.ShowDialogEx(this) == true)
                {
                    if (ObjNumberpadWind.ValueText == "")
                    {
                        strNumberPadText = "0";
                    }
                    else
                    {
                        strNumberPadText = ObjNumberpadWind.ValueText;
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

        private void txt_MC_AssetNo_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            txt_MC_AssetNo.Text = DisplayKeyboard(txt_MC_AssetNo.Text, string.Empty);
            txt_MC_AssetNo.SelectionStart = txt_MC_AssetNo.Text.Length;
        }

        private void com_GMU_Type_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (com_GMU_Type.SelectedItem.ToString().ToUpper() == "NODEVICE")
            {
                txt_GMU_No.Text = "0";
                txt_GMU_No.IsReadOnly = true;


            }
            else
            {
                txt_GMU_No.Text = "";
                txt_GMU_No.IsReadOnly = false;
            }
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
                        this.btn_Exit.Click -= (this.btn_Exit_Click);
                        this.btnEnrol.Click -= (this.btnEnrol_Click);
                        this.btnCancel.Click -= (this.btn_Exit_Click);
                        this.btnremove.Click -= (this.btnremove_Click);
                        this.btnChangeGame.Click -= (this.btnChangeGame_Click);
                        this.btnRemoveCancel.Click -= (this.btn_Exit_Click);
                        this.com_GMU_Type.SelectionChanged -= (this.com_GMU_Type_SelectionChanged);

                    },
                    (c) =>
                    {
                    });
                    this.WriteLog("MachineEnrolment objects are released successfully.");

                }
                disposed = true;
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="MachineEnrolment"/> is reclaimed by garbage collection.
        /// </summary>
        ~MachineEnrolment()
        {
            Dispose(false);
        }

        #endregion
    }
}