using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.EnterpriseBusiness.Entities;
using BMC.EnterpriseBusiness.Business;
using BMC.Common.LogManagement;
using BMC.Common.ExceptionManagement;
using BMC.CoreLib.Win32;
using Audit.BusinessClasses;
using BMC.Common.Utilities;
using Audit.Transport;
using BMC.Security;
using BMC.Common;

namespace BMC.EnterpriseClient.Views
{
    public partial class frmModifyDeclaration : BMC.EnterpriseClient.Helpers.BMCExtendedForm
    {
        Dictionary<string, DenomText> _ErrControlList = new Dictionary<string, DenomText>();
        ViewSites_DropBreakdown objBiz = new ViewSites_DropBreakdown();
        int _Collection_ID = 0;
        int _Site_ID = 0;
        List<DeclaredCollection> _lstCollection = null;
         //BMC.EnterpriseClient.Helpers.Datawatcher objDatawatcher = null;

        public frmModifyDeclaration()
        {
            InitializeComponent();
            SetTagProperty();
            //Handle invalid denoms
            txt_Diff_1.OnInValidDenom += new EventHandler(txt_OnInvalidDenom);
            txt_Diff_2.OnInValidDenom += new EventHandler(txt_OnInvalidDenom);
            txt_Diff_5.OnInValidDenom += new EventHandler(txt_OnInvalidDenom);
            txt_Diff_10.OnInValidDenom += new EventHandler(txt_OnInvalidDenom);
            txt_Diff_20.OnInValidDenom += new EventHandler(txt_OnInvalidDenom);
            txt_Diff_50.OnInValidDenom += new EventHandler(txt_OnInvalidDenom);
            txt_Diff_100.OnInValidDenom += new EventHandler(txt_OnInvalidDenom);
            txt_Diff_200.OnInValidDenom += new EventHandler(txt_OnInvalidDenom);
            txt_Diff_500.OnInValidDenom += new EventHandler(txt_OnInvalidDenom);
            //Handle valid denoms
            txt_Diff_1.OnValidDenom += new EventHandler(txt_OnValidDenom);
            txt_Diff_2.OnValidDenom += new EventHandler(txt_OnValidDenom);
            txt_Diff_5.OnValidDenom += new EventHandler(txt_OnValidDenom);
            txt_Diff_10.OnValidDenom += new EventHandler(txt_OnValidDenom);
            txt_Diff_20.OnValidDenom += new EventHandler(txt_OnValidDenom);
            txt_Diff_50.OnValidDenom += new EventHandler(txt_OnValidDenom);
            txt_Diff_100.OnValidDenom += new EventHandler(txt_OnValidDenom);
            txt_Diff_200.OnValidDenom += new EventHandler(txt_OnValidDenom);
            txt_Diff_500.OnValidDenom += new EventHandler(txt_OnValidDenom);

            txt_Diff_1.LostFocus += new EventHandler(txt_Diff_LostFocus);
            txt_Diff_2.LostFocus += new EventHandler(txt_Diff_LostFocus);
            txt_Diff_5.LostFocus += new EventHandler(txt_Diff_LostFocus);
            txt_Diff_10.LostFocus += new EventHandler(txt_Diff_LostFocus);
            txt_Diff_20.LostFocus += new EventHandler(txt_Diff_LostFocus);
            txt_Diff_50.LostFocus += new EventHandler(txt_Diff_LostFocus);
            txt_Diff_100.LostFocus += new EventHandler(txt_Diff_LostFocus);
            txt_Diff_200.LostFocus += new EventHandler(txt_Diff_LostFocus);
            txt_Diff_500.LostFocus += new EventHandler(txt_Diff_LostFocus);
            txt_Diff_TicketOut.LostFocus += new EventHandler(txt_Diff_LostFocus);
            txt_Diff_Ticketsin.LostFocus += new EventHandler(txt_Diff_LostFocus);
            txt_Diff_Hp.LostFocus += new EventHandler(txt_Diff_LostFocus);
            txt_Diff_Prog.LostFocus += new EventHandler(txt_Diff_LostFocus);


            txt_New_1.LostFocus += new EventHandler(txt_New_LostFocus);
            txt_New_2.LostFocus += new EventHandler(txt_New_LostFocus);
            txt_New_5.LostFocus += new EventHandler(txt_New_LostFocus);
            txt_New_10.LostFocus += new EventHandler(txt_New_LostFocus);
            txt_New_20.LostFocus += new EventHandler(txt_New_LostFocus);
            txt_New_50.LostFocus += new EventHandler(txt_New_LostFocus);
            txt_New_100.LostFocus += new EventHandler(txt_New_LostFocus);
            txt_New_200.LostFocus += new EventHandler(txt_New_LostFocus);
            txt_New_500.LostFocus += new EventHandler(txt_New_LostFocus);
            txt_New_TicketOut.LostFocus += new EventHandler(txt_New_LostFocus);
            txt_New_Ticketsin.LostFocus += new EventHandler(txt_New_LostFocus);
            txt_New_Hp.LostFocus += new EventHandler(txt_New_LostFocus);
            txt_New_Prog.LostFocus += new EventHandler(txt_New_LostFocus);

        }

        public frmModifyDeclaration(int iCollection_Id, int iSite_ID)
            : this()
        {
            this._Collection_ID = iCollection_Id;
            this._Site_ID = iSite_ID;

        }
        /// <summary>
        /// Assigns the Resource Key names to the controls--Created by kishore sivagnanam
        /// </summary>
        public void SetTagProperty()
        {
            this.btn_Apply.Tag = "Key_Apply";
            this.btn_Cancel.Tag = "Key_CloseCaption";
            this.Current.Tag = "Key_Current";
            this.lbl_Diff.Tag = "Key_Diff";
            this.lbl_Hp.Tag = "Key_HandpayincJPColon";
            this.Tag = "Key_ModifyDeclaration";
            this.lbl_net.Tag = "Key_NetColon";
            this.lbl_New.Tag = "Key_New";
            this.lbl_Prog.Tag = "Key_ProgHandpayColon";
            this.lbl_Ticketin.Tag = "Key_TicketsInColon";
            this.lbl_TicketsOut.Tag = "Key_TicketsOutColon";
            this.lbl_TotalBills.Tag = "Key_TotalBillsColon";
            this.lbl_TotalIn.Tag = "Key_TotalInColon";
            this.lbl_TotOut.Tag = "Key_TotalOutColon";

        }

        private void frmModifyDeclaration_Load(object sender, EventArgs e)
        {
            try
            {
                this.ResolveResources();
                LoadData();
                //objDatawatcher = new Helpers.Datawatcher(this);
            }
            catch (Exception Ex)
            {

                ExceptionManager.Publish(Ex);

            }

        }
        void LoadData()
        {
            _lstCollection = objBiz.GetCollectionToEdit(this._Collection_ID, this._Site_ID);
            foreach (DeclaredCollection item in _lstCollection)
            {
                LoadregionalSettings(_lstCollection[0].Region);

                txt_Curr_1.Text = _lstCollection[0].Cash_Collected_100p.ToString();
                txt_Curr_2.Text = _lstCollection[0].Cash_Collected_200p.ToString();
                txt_Curr_5.Text = _lstCollection[0].Cash_Collected_500p.ToString();
                txt_Curr_10.Text = _lstCollection[0].Cash_Collected_1000p.ToString();
                txt_Curr_20.Text = _lstCollection[0].Cash_Collected_2000p.ToString();
                txt_Curr_50.Text = _lstCollection[0].Cash_Collected_5000p.ToString();
                txt_Curr_100.Text = _lstCollection[0].Cash_Collected_10000p.ToString();
                txt_Curr_200.Text = _lstCollection[0].Cash_Collected_20000p.ToString();
                txt_Curr_500.Text = _lstCollection[0].Cash_Collected_50000p.ToString();
                txt_Curr_TicketOut.Text = _lstCollection[0].Tickets_Printed.ToString("0.00");
                txt_Curr_Ticketsin.Text = _lstCollection[0].Declared_Tickets.ToString("0.00");
                txt_Curr_Hp.Text = _lstCollection[0].Hand_Pay.ToString("0.00");
                txt_Curr_Prog.Text = _lstCollection[0].Progressive_Value_Declared.ToString("0.00");


                txt_New_1.Text = _lstCollection[0].Cash_Collected_100p.ToString();
                txt_New_2.Text = _lstCollection[0].Cash_Collected_200p.ToString();
                txt_New_5.Text = _lstCollection[0].Cash_Collected_500p.ToString();
                txt_New_10.Text = _lstCollection[0].Cash_Collected_1000p.ToString();
                txt_New_20.Text = _lstCollection[0].Cash_Collected_2000p.ToString();
                txt_New_50.Text = _lstCollection[0].Cash_Collected_5000p.ToString();
                txt_New_100.Text = _lstCollection[0].Cash_Collected_10000p.ToString();
                txt_New_200.Text = _lstCollection[0].Cash_Collected_20000p.ToString();
                txt_New_500.Text = _lstCollection[0].Cash_Collected_50000p.ToString();
                txt_New_TicketOut.Text = _lstCollection[0].Tickets_Printed.ToString("0.00");
                txt_New_Ticketsin.Text = _lstCollection[0].Declared_Tickets.ToString("0.00");
                txt_New_Hp.Text = _lstCollection[0].Hand_Pay.ToString("0.00");
                txt_New_Prog.Text = _lstCollection[0].Progressive_Value_Declared.ToString("0.00");

                CalculateCurrentTotal();
                CalculateNewTotal();
                FormatAll();
            }
        }

        private void LoadregionalSettings(string Region)
        {
            try
            {
                switch (Region.ToUpper())
                {
                    case "UK":
                        tableLayoutPanel1.RowStyles[tableLayoutPanel1.GetRow(lbl_1)].Height = 0;
                        tableLayoutPanel1.RowStyles[tableLayoutPanel1.GetRow(lbl_2)].Height = 0;
                        lbl_5.Text = string.Format("£{0}:", this.GetResourceTextByKey("Key_5"));
                        lbl_10.Text = string.Format("£{0}:",this.GetResourceTextByKey("Key_10"));
                        lbl_20.Text = string.Format("£{0}:",this.GetResourceTextByKey("Key_20"));
                        lbl_50.Text = string.Format("£{0}:",this.GetResourceTextByKey("Key_50"));
                        tableLayoutPanel1.RowStyles[tableLayoutPanel1.GetRow(lbl_100)].Height = 0;
                        tableLayoutPanel1.RowStyles[tableLayoutPanel1.GetRow(lbl_200)].Height = 0;
                        tableLayoutPanel1.RowStyles[tableLayoutPanel1.GetRow(lbl_500)].Height = 0;
                        lbl_1.Visible = false;
                        txt_Curr_1.Visible = false;
                        txt_New_1.Visible = false;
                        txt_Diff_1.Visible = false;
                        lbl_2.Visible = false;
                        txt_Curr_2.Visible = false;
                        txt_New_2.Visible = false;
                        txt_Diff_2.Visible = false;
                        lbl_100.Visible = false;
                        txt_Curr_100.Visible = false;
                        txt_New_100.Visible = false;
                        txt_Diff_100.Visible = false;
                        lbl_500.Visible = false;
                        txt_Curr_500.Visible = false;
                        txt_New_500.Visible = false;
                        txt_Diff_500.Visible = false;
                        lbl_200.Visible = false;
                        txt_Curr_200.Visible = false;
                        txt_New_200.Visible = false;
                        txt_Diff_200.Visible = false;
                        break;

                    case "US":
                        lbl_1.Text = string.Format("${0}:",this.GetResourceTextByKey("Key_1"));
                        tableLayoutPanel1.RowStyles[tableLayoutPanel1.GetRow(lbl_2)].Height = 0;
                        lbl_2.Text = string.Format("${0}:", this.GetResourceTextByKey("Key_2"));
                        lbl_5.Text = string.Format("${0}:", this.GetResourceTextByKey("Key_5"));
                        lbl_10.Text = string.Format("${0}:", this.GetResourceTextByKey("Key_10"));
                        lbl_20.Text = string.Format("${0}:", this.GetResourceTextByKey("Key_20"));
                        lbl_50.Text = string.Format("${0}:", this.GetResourceTextByKey("Key_50"));
                        lbl_100.Text = string.Format("${0}:", this.GetResourceTextByKey("Key_100"));
                        tableLayoutPanel1.RowStyles[tableLayoutPanel1.GetRow(lbl_200)].Height = 0;
                        tableLayoutPanel1.RowStyles[tableLayoutPanel1.GetRow(lbl_500)].Height = 0;
                        lbl_2.Visible = false;
                        txt_Curr_2.Visible = false;
                        txt_New_2.Visible = false;
                        txt_Diff_2.Visible = false;
                        lbl_500.Visible = false;
                        txt_Curr_500.Visible = false;
                        txt_New_500.Visible = false;
                        txt_Diff_500.Visible = false;
                        lbl_200.Visible = false;
                        txt_Curr_200.Visible = false;
                        txt_New_200.Visible = false;
                        txt_Diff_200.Visible = false;
                        break;

                    case "AR":
                        tableLayoutPanel1.RowStyles[tableLayoutPanel1.GetRow(lbl_1)].Height = 0;
                        lbl_1.Text = string.Format("${0}:", this.GetResourceTextByKey("Key_1"));
                        lbl_2.Text = string.Format("${0}:", this.GetResourceTextByKey("Key_2"));
                        lbl_5.Text = string.Format("${0}:", this.GetResourceTextByKey("Key_5"));
                        lbl_10.Text = string.Format("${0}:", this.GetResourceTextByKey("Key_10"));
                        lbl_20.Text = string.Format("${0}:", this.GetResourceTextByKey("Key_20"));
                        lbl_50.Text = string.Format("${0}:", this.GetResourceTextByKey("Key_50"));
                        lbl_100.Text = string.Format("${0}:", this.GetResourceTextByKey("Key_100"));
                        tableLayoutPanel1.RowStyles[tableLayoutPanel1.GetRow(lbl_200)].Height = 0;
                        tableLayoutPanel1.RowStyles[tableLayoutPanel1.GetRow(lbl_500)].Height = 0;
                        lbl_1.Visible = false;
                        txt_Curr_1.Visible = false;
                        txt_New_1.Visible = false;
                        txt_Diff_1.Visible = false;
                        lbl_500.Visible = false;
                        txt_Curr_500.Visible = false;
                        txt_New_500.Visible = false;
                        txt_Diff_500.Visible = false;
                        lbl_200.Visible = false;
                        txt_Curr_200.Visible = false;
                        txt_New_200.Visible = false;
                        txt_Diff_200.Visible = false;
                        break;
                    default:
                        tableLayoutPanel1.RowStyles[tableLayoutPanel1.GetRow(lbl_1)].Height = 0;
                        tableLayoutPanel1.RowStyles[tableLayoutPanel1.GetRow(lbl_2)].Height = 0;
                        lbl_5.Text = string.Format("£{0}:", this.GetResourceTextByKey("Key_5"));
                        lbl_10.Text = string.Format("£{0}:", this.GetResourceTextByKey("Key_10"));
                        lbl_20.Text = string.Format("£{0}:", this.GetResourceTextByKey("Key_20"));
                        lbl_50.Text = string.Format("£{0}:", this.GetResourceTextByKey("Key_50"));
                        tableLayoutPanel1.RowStyles[tableLayoutPanel1.GetRow(lbl_100)].Height = 0;
                        tableLayoutPanel1.RowStyles[tableLayoutPanel1.GetRow(lbl_200)].Height = 0;
                        tableLayoutPanel1.RowStyles[tableLayoutPanel1.GetRow(lbl_500)].Height = 0;
                        lbl_1.Visible = false;
                        txt_Curr_1.Visible = false;
                        txt_New_1.Visible = false;
                        txt_Diff_1.Visible = false;
                        lbl_2.Visible = false;
                        txt_Curr_2.Visible = false;
                        txt_New_2.Visible = false;
                        txt_Diff_2.Visible = false;
                        lbl_100.Visible = false;
                        txt_Curr_100.Visible = false;
                        txt_New_100.Visible = false;
                        txt_Diff_100.Visible = false;
                        lbl_500.Visible = false;
                        txt_Curr_500.Visible = false;
                        txt_New_500.Visible = false;
                        txt_Diff_500.Visible = false;
                        lbl_200.Visible = false;
                        txt_Curr_200.Visible = false;
                        txt_New_200.Visible = false;
                        txt_Diff_200.Visible = false;
                        break;
                }
            }
            catch (Exception Ex)
            {

                ExceptionManager.Publish(Ex);

            }
        }

        void txt_Diff_LostFocus(object sender, EventArgs e)
        {
            try
            {
                switch (((DenomText)sender).Name)
                {
                    case "txt_Diff_1":
                        txt_New_1.Text = (txt_Curr_1.IntValue + txt_Diff_1.IntValue).ToString();
                        txt_New_1.ValidateDenom(e);
                        break;
                    case "txt_Diff_2":
                        txt_New_2.Text = (txt_Curr_2.IntValue + txt_Diff_2.IntValue).ToString();
                        txt_New_2.ValidateDenom(e);
                        break;
                    case "txt_Diff_5":
                        txt_New_5.Text = (txt_Curr_5.IntValue + txt_Diff_5.IntValue).ToString();
                        txt_New_5.ValidateDenom(e);
                        break;
                    case "txt_Diff_10":
                        txt_New_10.Text = (txt_Curr_10.IntValue + txt_Diff_10.IntValue).ToString();
                        txt_New_10.ValidateDenom(e);
                        break;
                    case "txt_Diff_20":
                        txt_New_20.Text = (txt_Curr_20.IntValue + txt_Diff_20.IntValue).ToString();
                        txt_New_20.ValidateDenom(e);
                        break;
                    case "txt_Diff_50":
                        txt_New_50.Text = (txt_Curr_50.IntValue + txt_Diff_50.IntValue).ToString();
                        txt_New_50.ValidateDenom(e);
                        break;
                    case "txt_Diff_100":
                        txt_New_100.Text = (txt_Curr_100.IntValue + txt_Diff_100.IntValue).ToString();
                        txt_New_100.ValidateDenom(e);
                        break;
                    case "txt_Diff_200":
                        txt_New_200.Text = (txt_Curr_200.IntValue + txt_Diff_200.IntValue).ToString();
                        txt_New_200.ValidateDenom(e);
                        break;
                    case "txt_Diff_500":
                        txt_New_500.Text = (txt_Curr_500.IntValue + txt_Diff_500.IntValue).ToString();
                        txt_New_500.ValidateDenom(e);
                        break;

                    case "txt_Diff_TicketOut":
                        txt_New_TicketOut.Text = (txt_Curr_TicketOut.DecimalValue + txt_Diff_TicketOut.DecimalValue).ToString();
                        txt_New_TicketOut.ValidateDenom(e);
                        break;
                    case "txt_Diff_Ticketsin":
                        txt_New_Ticketsin.Text = (txt_Curr_Ticketsin.DecimalValue + txt_Diff_Ticketsin.DecimalValue).ToString();
                        txt_New_Ticketsin.ValidateDenom(e);
                        break;
                    case "txt_Diff_Hp":
                        txt_New_Hp.Text = (txt_Curr_Hp.DecimalValue + txt_Diff_Hp.DecimalValue).ToString();
                        txt_New_Hp.ValidateDenom(e);
                        break;
                    case "txt_Diff_Prog":
                        txt_New_Prog.Text = (txt_Curr_Prog.DecimalValue + txt_Diff_Prog.DecimalValue).ToString();
                        txt_New_Prog.ValidateDenom(e);
                        break;



                }
                CalculateDiffTotal();
                CalculateNewTotal();
                FormatAll();
            }
            catch (Exception Ex)
            {

                ExceptionManager.Publish(Ex);
            }
        }

        void txt_New_LostFocus(object sender, EventArgs e)
        {

            try
            {
                switch (((DenomText)sender).Name)
                {
                    case "txt_New_1":
                        txt_Diff_1.Text = (txt_New_1.IntValue - txt_Curr_1.IntValue).ToString();
                        txt_Diff_1.ValidateDenom(e);
                        break;
                    case "txt_New_2":
                        txt_Diff_2.Text = (txt_New_2.IntValue - txt_Curr_2.IntValue).ToString();
                        txt_Diff_2.ValidateDenom(e);
                        break;
                    case "txt_New_5":
                        txt_Diff_5.Text = (txt_New_5.IntValue - txt_Curr_5.IntValue).ToString();
                        txt_Diff_5.ValidateDenom(e);
                        break;
                    case "txt_New_10":
                        txt_Diff_10.Text = (txt_New_10.IntValue - txt_Curr_10.IntValue).ToString();
                        txt_Diff_10.ValidateDenom(e);
                        break;
                    case "txt_New_20":
                        txt_Diff_20.Text = (txt_New_20.IntValue - txt_Curr_20.IntValue).ToString();
                        txt_Diff_20.ValidateDenom(e);
                        break;
                    case "txt_New_50":
                        txt_Diff_50.Text = (txt_New_50.IntValue - txt_Curr_50.IntValue).ToString();
                        txt_Diff_50.ValidateDenom(e);
                        break;
                    case "txt_New_100":
                        txt_Diff_100.Text = (txt_New_100.IntValue - txt_Curr_100.IntValue).ToString();
                        txt_Diff_100.ValidateDenom(e);
                        break;
                    case "txt_New_200":
                        txt_Diff_200.Text = (txt_New_200.IntValue - txt_Curr_200.IntValue).ToString();
                        txt_Diff_200.ValidateDenom(e);
                        break;
                    case "txt_New_500":
                        txt_Diff_500.Text = (txt_New_500.IntValue - txt_Curr_500.IntValue).ToString();
                        txt_Diff_500.ValidateDenom(e);
                        break;

                    case "txt_New_TicketOut":
                        txt_Diff_TicketOut.Text = (txt_New_TicketOut.DecimalValue - txt_Curr_TicketOut.DecimalValue).ToString();
                        txt_Diff_TicketOut.ValidateDenom(e);
                        break;
                    case "txt_New_Ticketsin":
                        txt_Diff_Ticketsin.Text = (txt_New_Ticketsin.DecimalValue - txt_Curr_Ticketsin.DecimalValue).ToString();
                        txt_Diff_Ticketsin.ValidateDenom(e);
                        break;
                    case "txt_New_Hp":
                        txt_Diff_Hp.Text = (txt_New_Hp.DecimalValue - txt_Curr_Hp.DecimalValue).ToString();
                        txt_Diff_Hp.ValidateDenom(e);
                        break;
                    case "txt_New_Prog":
                        txt_Diff_Prog.Text = (txt_New_Prog.DecimalValue - txt_Curr_Prog.DecimalValue).ToString();
                        txt_Diff_Prog.ValidateDenom(e);
                        break;
                }
                CalculateDiffTotal();
                CalculateNewTotal();
                FormatAll();
            }
            catch (Exception Ex)
            {

                ExceptionManager.Publish(Ex);

            }
        }

        private void txt_OnInvalidDenom(object sender, EventArgs e)
        {
            try
            {
                if (!_ErrControlList.ContainsKey(((DenomText)sender).UniqueNo))
                    _ErrControlList.Add(((DenomText)sender).UniqueNo, ((DenomText)sender));
     
            }
            catch (Exception Ex)
            {

                ExceptionManager.Publish(Ex);

            }


        }

        private void txt_OnValidDenom(object sender, EventArgs e)
        {
            try
            {
                _ErrControlList.Remove(((DenomText)sender).UniqueNo);

            }
            catch (Exception Ex)
            {

                ExceptionManager.Publish(Ex);

            }

        }

        private void btn_Apply_Click(object sender, EventArgs e)
        {
            try
            {
                SaveDeclaration();
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        private void SaveDeclaration()
        {
           
                if (_ErrControlList.Count > 0)
                {
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_VALID_DENOM"), this.Text);
                    ((DenomText)_ErrControlList.First().Value).Focus();
                    return;

                }
                objBiz.EditCollection(this._Collection_ID, txt_Diff_1.IntValue, txt_Diff_2.IntValue, txt_Diff_5.IntValue, txt_Diff_10.IntValue, txt_Diff_20.IntValue, txt_Diff_50.IntValue, txt_Diff_100.IntValue, txt_Diff_Ticketsin.DecimalValue, txt_Diff_TicketOut.DecimalValue, txt_Diff_Hp.DecimalValue  + txt_Diff_Prog.DecimalValue, txt_Diff_Prog.DecimalValue);
                Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_SAVE_SUCCESS"), this.Text);
                this.AuditChanges();
                this.SuppressConfirmMessageBox = false;
                this.Close();
            
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AuditChanges()
        {
            try
            {
                StringBuilder strMessage = new StringBuilder();                
                if (txt_Diff_1.IntValue > 0)
                    strMessage.Append(string.Format(" Declared values for {0} updated by {1}", "Bill1", txt_Diff_1.IntValue));
                if (txt_Diff_2.IntValue > 0)
                    strMessage.Append(string.Format(" Declared values for {0} updated by {1}", "Bill2", txt_Diff_2.IntValue));
                if (txt_Diff_5.IntValue > 0)
                    strMessage.Append(string.Format(" Declared values for {0} updated by {1}", "Bill5", txt_Diff_5.IntValue));
                if (txt_Diff_10.IntValue > 0)
                    strMessage.Append(string.Format(" Declared values for {0} updated by {1}", "Bill10", txt_Diff_10.IntValue));
                if (txt_Diff_20.IntValue > 0)
                    strMessage.Append(string.Format(" Declared values for {0} updated by {1}", "Bill20", txt_Diff_20.IntValue));
                if (txt_Diff_50.IntValue > 0)
                    strMessage.Append(string.Format(" Declared values for {0} updated by {1}", "Bill50", txt_Diff_50.IntValue));
                if (txt_Diff_100.IntValue > 0)
                    strMessage.Append(string.Format(" Declared values for {0} updated by {1}", "Bill100", txt_Diff_100.IntValue));
                if (txt_Diff_200.IntValue > 0)
                    strMessage.Append(string.Format(" Declared values for {0} updated by {1}", "Bill200", txt_Diff_200.IntValue));
                if (txt_Diff_500.IntValue > 0)
                    strMessage.Append(string.Format(" Declared values for {0} updated by {1}", "Bill500", txt_Diff_500.IntValue));
                if (txt_Diff_Ticketsin.DecimalValue > 0)
                    strMessage.Append(string.Format(" Declared values for {0} updated by {1}", "Tickets Inserted", txt_Diff_Ticketsin.DecimalValue));
                if (txt_Diff_Hp.DecimalValue > 0)
                    strMessage.Append(string.Format(" Declared values for {0} updated by {1}", "Handpay", txt_Diff_Hp.DecimalValue));
                if (txt_Diff_Prog.DecimalValue > 0)
                    strMessage.Append(string.Format(" Declared values for {0} updated by {1}", "Progressive", txt_Diff_Prog.DecimalValue));
                if (txt_Diff_TotalOut.DecimalValue > 0)
                    strMessage.Append(string.Format(" Declared values for {0} updated by {1}", "Tickets Printed", txt_Diff_TotalOut.DecimalValue));
                
                if (strMessage.Length != 0)
                    objBiz.Auditusers(DateTime.Now.ToString(), _Site_ID.ToString(), _lstCollection[0].Batch_ID, AppGlobals.Current.UserName, strMessage.ToString());

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error While Adding Audit Log for Template Update: Error Message:" + ex.Message, LogManager.enumLogLevel.Error);
            }

        }

        private void CalculateCurrentTotal()
        {

            txt_Curr_TotalBills.Text = FormatNegative(
                                                   txt_Curr_1.IntValue
                                                   + txt_Curr_2.IntValue
                                                   + txt_Curr_5.IntValue
                                                   + txt_Curr_10.IntValue
                                                   + txt_Curr_20.IntValue
                                                   + txt_Curr_50.IntValue
                                                   + txt_Curr_100.IntValue
                                                   + txt_Curr_200.IntValue
                                                   + txt_Curr_500.IntValue);

            //Total In

            txt_Curr_TotalIn.Text = FormatNegative(txt_Curr_TotalBills.DecimalValue + txt_Curr_Ticketsin.DecimalValue);
            //Total Out
            txt_Curr_TotalOut.Text = FormatNegative(txt_Curr_Hp.DecimalValue + txt_Curr_TicketOut.DecimalValue + txt_Curr_Prog.DecimalValue);
            //Net
            txt_Curr_Net.Text = FormatNegative(txt_Curr_TotalIn.DecimalValue + txt_Curr_TotalOut.DecimalValue);

        }

        private void CalculateDiffTotal()
        {

            txt_Diff_TotalBills.Text = FormatNegative(
                                                     txt_Diff_1.IntValue
                                                     + txt_Diff_2.IntValue
                                                     + txt_Diff_5.IntValue
                                                     + txt_Diff_10.IntValue
                                                     + txt_Diff_20.IntValue
                                                     + txt_Diff_50.IntValue
                                                     + txt_Diff_100.IntValue
                                                     + txt_Diff_200.IntValue
                                                     + txt_Diff_500.IntValue);
            //Total In
            txt_Diff_TotalIn.Text = FormatNegative(txt_Diff_TotalBills.DecimalValue + txt_Diff_Ticketsin.DecimalValue);
            //Total Out
            txt_Diff_TotalOut.Text = FormatNegative(txt_Diff_Hp.DecimalValue + txt_Diff_TicketOut.DecimalValue + txt_Diff_Prog.DecimalValue);
            //Net
            txt_Diff_Net.Text = FormatNegative(txt_Diff_TotalIn.DecimalValue + txt_Diff_TotalOut.DecimalValue);


        }

        private void CalculateNewTotal()
        {

            txt_New_TotalBills.Text = FormatNegative(txt_New_1.IntValue
                                                        + txt_New_2.IntValue
                                                             + txt_New_5.IntValue
                                                             + txt_New_10.IntValue
                                                             + txt_New_20.IntValue
                                                             + txt_New_50.IntValue
                                                             + txt_New_100.IntValue
                                                             + txt_New_200.IntValue
                                                             + txt_New_500.IntValue);

            //Total In
            txt_New_TotalIn.Text = FormatNegative(txt_New_TotalBills.DecimalValue + txt_New_Ticketsin.DecimalValue);
            //Total Out
            txt_New_TotalOut.Text = FormatNegative(txt_New_Hp.DecimalValue + txt_New_TicketOut.DecimalValue + txt_New_Prog.DecimalValue);
            //Net
            txt_New_Net.Text = FormatNegative(txt_New_TotalIn.DecimalValue + txt_New_TotalOut.DecimalValue);

        }

        private string FormatNegative(decimal dValue)
        {

            return dValue.ToString();
        }

        private void FormatAll()
        {
         
            foreach (Control cont in tableLayoutPanel1.Controls )
            {
                if (cont.GetType().Name == "DenomText")
                {
                    DenomText temp = (DenomText)cont;
                    try
                    {
                        if (temp.Text == string.Empty)
                            temp.Text = "0";
                        if (temp.DecimalValue < 0)
                        {
                            if (temp.AllowDecimal)
                                temp.Text = string.Format("({0})", (temp.DecimalValue * -1).ToString("#,##0.00"));
                            else
                                temp.Text = string.Format("({0})", (temp.DecimalValue * -1).ToString("#,##0"));
                        }
                        else
                        {
                            if (temp.AllowDecimal)
                                cont.Text = (temp.DecimalValue).ToString("#,##0.00");
                            else
                                cont.Text = (temp.DecimalValue).ToString("#,##0");
                        }
                    }
                    catch
                    {
                        temp.Text="0";
                    }

                }

            }
           

        }

       
    }
}
