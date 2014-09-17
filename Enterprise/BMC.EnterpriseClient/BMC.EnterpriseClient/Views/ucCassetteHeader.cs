using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.EnterpriseClient.Views;
using System.Globalization;
using System.Xml.Linq;
using BMC.Common;

namespace BMC.EnterpriseClient.Views
{
    public partial class ucCassetteHeader : UserControl
    {
        private string _CassetteName;
        private bool _IsEditingEnabled;
        private CassetteType _Type;

        public EventHandler OnQuantityChange;
        public ucCassette GroupParent { get; set; }
        string _strCurrency;

        public ucCassetteHeader()
        {
            InitializeComponent();
            _strCurrency = new RegionInfo(BMC.Common.ConfigurationManagement.ConfigManager.Read("GetDefaultCultureForCurrency")).CurrencySymbol;

        }

        public ucCassetteHeader(CassetteType Type)
            : this()
        {
            _Type = Type;

        }
        public int Cassette_ID { get; set; }
        public object Tag { get; set; }
        public bool IsEditingEnabled
        {
            get
            {
                return _IsEditingEnabled;
            }
            set
            {
                txtQuantity.Visible = value;
                lblDenom.Visible = value;
                chkIsSelected.Checked = value;
                chkIsSelected.Visible = value;
                _IsEditingEnabled = value;
            }
        }

        public void setLabels()
        {
            lblCassetteName.Text = this.GetResourceTextByKey("Key_Cassette");                               // "Cassette/Hopper";
            lblDenom.Text = string.Format(this.GetResourceTextByKey("Key_DenomCurrency"), _strCurrency);    //"Denom ({0})"
            txtQuantity.Text = this.GetResourceTextByKey("Key_Quantity");                                   // "Quantity";
            txtAmount.Text = string.Format(this.GetResourceTextByKey("Key_DeclaredAmount"), _strCurrency);      // "Declared Amount ({0})"
            txtDropAmount.Text = string.Format(this.GetResourceTextByKey("Key_DroppedAmount"), _strCurrency);   // Dropped Amount ({0})
        }
        public string CassetteName
        {
            get
            {
                return _CassetteName;
            }
            set
            {
                lblCassetteName.Text = value;
                _CassetteName = value;
            }
        }
    }
}
