using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.MeterAdjustmentTool.Helpers;
using BMC.Common.ExceptionManagement;
using BMC.Common;

namespace BMC.MeterAdjustmentTool
{
    public partial class CalculatedMeterInfoForm : DialogFormBase
    {
        private DataTable _meterInfo = null;
        static string currencySymbol = CurrencyHelper.GetCurrencySymbol();

        public CalculatedMeterInfoForm(DataTable dtUserInfo)
        {
            _meterInfo = dtUserInfo;
            InitializeComponent();

            // Set Tags for controls
            SetTagProperty();   
        }

        private void SetTagProperty()
        {
            btnClose.Tag = "Key_CloseCaption";
            this.Tag = "Key_RelatedMeters";
            uxHeader.Tag = "Key_RelatedMeters";
        }

        protected override void LoadChanges()
        {
            base.LoadChanges();           

            //_meterInfo = (DataTable)this.Tag;

            try
            {
                if (_meterInfo != null && _meterInfo.Rows.Count > 0)
                {
                    dgvMeterInfo.DataSource = _meterInfo;
                    if (_meterInfo.Rows[0].ItemArray[0].ToString() == GetMeterNameWithCurrency("Key_HrlyRdSch_CREDITS_WAGERED"))              // ("CREDITS WAGERED"))
                    {
                        if (_meterInfo.Rows[1].ItemArray[0].ToString() == GetMeterNameWithCurrency("Key_HrlyRdSch_GAMES_BET"))               // ("GAMES BET"))
                            uxHeader.Tag="Key_AverageBetFormula";      //" 'AverageBet' = 'CreditsWagered' / 'GamesBet'";
                        else if (_meterInfo.Rows[1].ItemArray[0].ToString() == GetMeterNameWithCurrency("Key_HrlyRdSch_CREDITS_WON"))        // ("CREDITS WON"))
                            uxHeader.Tag = "Key_CasinoWinFormula";      //" 'CasinoWin' = 'CreditsWagered' - 'CreditsWon'";
                    }
                    else if (_meterInfo.Rows[0].ItemArray[0].ToString() == GetMeterNameWithCurrency("Key_HrlyRdSch_GAMES_BET"))              // ("GAMES BET"))
                    {
                        uxHeader.Tag ="Key_OccupancyFormula";      //" 'Occupancy' = ('Games_Bet'/'Games per hour') * 100";
                    }

                    // Get Header Texts from Resource
                    foreach (DataGridViewColumn dc in dgvMeterInfo.Columns)
                    {                       
                        switch (dc.Name.ToUpper())
                        {
                            case "METER":
                                dc.Tag = "Key_Meter"; 
                                break;
                            case "START":
                                dc.Tag = "Key_Start"; break;
                            case "END":
                                dc.Tag = "Key_End"; break;
                            case "CURRENTDELTA":
                                dc.Tag = "Key_CurrentDelta"; 
                                break;
                        }
                    }
                }

                // For externalization
                this.ResolveResources();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private string GetMeterNameWithCurrency(string hdrKey)
        {
            string hdrNewKey = this.GetResourceTextByKey(hdrKey);
            if (hdrNewKey.Contains("#"))
            {
                hdrNewKey = hdrNewKey.Replace("#", currencySymbol);
            }
            return hdrNewKey;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
