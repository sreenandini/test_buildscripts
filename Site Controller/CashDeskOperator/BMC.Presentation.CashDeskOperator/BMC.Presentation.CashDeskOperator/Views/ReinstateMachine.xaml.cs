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
using System.Windows.Shapes;
using System.Text.RegularExpressions;

using BMC.CashDeskOperator;
using BMC.CashDeskOperator.BusinessObjects;
using BMC.Security;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.Common.Utilities;
using System.Globalization;
using BMC.Transport;
using BMC.Presentation.POS.Helper_classes;

namespace BMC.Presentation.POS.Views
{
    /// <summary>
    /// Interaction logic for ReinstateMachine.xaml
    /// </summary>
    public partial class ReinstateMachine : Window, IDisposable
    {
        PositionDetail _PositionDetail = null;
        public readonly CollectionHelper CollectionHelper = new CollectionHelper();
        public ReinstateMachine()
        {
            InitializeComponent();
        }

        public ReinstateMachine(PositionDetail oPositionDetail)
        {
            InitializeComponent();
            _PositionDetail = oPositionDetail;
            this.txtPositionValue.Text = Convert.ToInt32(_PositionDetail.PostionNumber).ToString();
            this.txtMachineValue.Text = _PositionDetail.AssetNumber;

            txtFloatValue.TextChanged += new TextChangedEventHandler(txtFloatValue_TextChanged);

            txtFloatValue.Text = string.Format("0{0}00", ExtensionMethods.GetCurrencyDecimalDelimiter());
            txtFloatValue.MaxLength = 8;
            txtFloatValue.Focus();

            if (Settings.CD_Not_Use_Hoppers)
            {
                GroupBox_2.Visibility = Visibility.Collapsed;
                txtFloatValue.Visibility = Visibility.Collapsed;
                spPanel.Height = 300;
            }




        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void btnReinstate_Click(object sender, RoutedEventArgs e)
        {
            int nResult;

            try
            {
                btnReinstate.IsEnabled = false;
                if (string.IsNullOrEmpty(txtFloatValue.Text) || Regex.IsMatch(txtFloatValue.Text, "[^0-9.,]+"))
                {
                    //"Please enter a valid refloat value"
                    MessageBox.ShowBox("MessageID318", BMC_Icon.Warning, BMC_Button.OK);
                    return;
                }


                int nReturn = CollectionHelper.GetHandPayPlayCreditStatus(_PositionDetail.InstallationNo);
                switch (nReturn)
                {
                    case 1:

                        MessageBox.ShowBox("MessageID313", BMC_Icon.Warning);
                        return;
                    case 2:

                        MessageBox.ShowBox("MessageID316", BMC_Icon.Warning);
                        return;
                    case 3:

                        MessageBox.ShowBox("MessageID317", BMC_Icon.Warning);
                        return;

                }



                if (CollectionHelper.IsEventsUnCleared(_PositionDetail.InstallationNo))
                {

                    if (MessageBox.ShowBox("MessageID315", BMC_Icon.Question, BMC_Button.YesNo) == System.Windows.Forms.DialogResult.Yes)
                    {
                        IEventDetails objCDO = EventsBusinessObject.CreateInstance();

                        objCDO.UpdateEventDetails("MACHINE", string.Empty, 0, _PositionDetail.InstallationNo);
                        return;

                    }

                }
                else
                {
                    var oDataContext =
                        new InstallationDataContext(oCommonUtilities.CreateInstance().GetConnectionString());

                    nResult = oDataContext.InsertReinstateMachine
                         (_PositionDetail.InstallationNo,
                             SecurityHelper.CurrentUser.SecurityUserID,
                             Convert.ToDouble(txtFloatValue.Text),
                             "Float Issued",
                             "FLOAT",
                             0,
                             0,
                             "SITE",
                             Settings.CD_Not_Use_Hoppers);

                    if (nResult == 0)
                    {
                        MessageBox.ShowBox("MessageID319", BMC_Icon.Information, BMC_Button.OK);
                        this.DialogResult = true;
                        this.Close();

                    }
                    else
                        MessageBox.ShowBox("MessageID320", BMC_Icon.Information, BMC_Button.OK);
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                btnReinstate.IsEnabled = true;
            }
        }


        private string DisplayNumberPad(string keytext)
        {
            string strNumberPadText = string.Empty;
            NumPadWin ObjNumberpadWind = new NumPadWin();

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
                        //Regex objRegexNumberValidate = new Regex("^[0-9]+$", RegexOptions.IgnoreCase);
                        //MatchCollection objMatchCollect;

                        if (ObjNumberpadWind.ValueText != "")
                        {
                            //objMatchCollect = objRegexNumberValidate.Matches(ObjNumberpadWind.ValueText);
                            //if (objMatchCollect.Count > 0)
                            //{
                            //    strNumberPadText = GetPlayerInfo(ObjNumberpadWind.ValueText);
                            //}
                            //else
                            //{
                            //    strNumberPadText = "1";
                            //}

                            strNumberPadText = ObjNumberpadWind.ValueText;
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

        private void txtFloatValue_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            string sValue;
            sValue = DisplayNumberPad(txtFloatValue.Text);

            if (!string.IsNullOrEmpty(sValue))
                txtFloatValue.Text = sValue;
            else
                txtFloatValue.Text = string.Format("0{0}00", ExtensionMethods.GetCurrencyDecimalDelimiter());
        }



        void txtFloatValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFloatValue.Text))
            {
                txtFloatValue.Text = string.Format("0{0}00", ExtensionMethods.GetCurrencyDecimalDelimiter());
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
                        this.txtFloatValue.PreviewMouseUp -= (this.txtFloatValue_PreviewMouseUp);
                        this.btnReinstate.Click -= (this.btnReinstate_Click);
                        this.btnCancel.Click -= (this.btnCancel_Click);
                    },
                    (c) =>
                    {
                    });
                    this.WriteLog("ReinstateMachine objects are released successfully.");

                }
                disposed = true;
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="ReinstateMachine"/> is reclaimed by garbage collection.
        /// </summary>
        ~ReinstateMachine()
        {
            Dispose(false);
        }

        #endregion


    }
}
