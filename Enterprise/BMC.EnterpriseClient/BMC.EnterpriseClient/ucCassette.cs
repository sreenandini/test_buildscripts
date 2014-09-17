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
    public delegate void InvalidDenom(string Source,string Message);

    public partial class ucCassette : UserControl
    {
        private string _CassetteName;
        private decimal _Denom;
        private int _Quantity;
        private decimal _Amount;
        private decimal _DropAmount;
        private decimal _GroupSum;
        private bool _IsEditingEnabled;
        private CassetteType _Type;
        private bool _IsChildQuantityModified;
        private List<ucCassette> _GroupMembers = new List<ucCassette>();
        private bool _IsHeader;
        public EventHandler OnQuantityChange;
        public ucCassette GroupParent { get; set; }
        string _strCurrency;
        private bool _IsAmountEditable = false;
        private bool _Enabled = true;
        private decimal _MaxFillAmount = 999999.99M;
        public InvalidDenom OnInvalidDenom;
        public decimal MaxFillAmount
        {
            get
            {
                return _MaxFillAmount;
            }
            set
            {
                _MaxFillAmount = value;
            }
        }

        public bool CapacityExceeded
        {
            get
            {
                return ((_MaxFillAmount - _Amount) < 0) ? true : false;
            }
        }

        public bool IsAmountEditable
        {
            get
            {
                return _IsAmountEditable;
            }
            private set
            {
                _IsAmountEditable = value;
                if (_IsAmountEditable)
                {
                    txtQuantity.ReadOnly = true;
                    txtQuantity.BackColor = Color.Gainsboro;
                    txtQuantity.BorderStyle = BorderStyle.FixedSingle;
                    txtQuantity.TextChanged -= txtQuantity_TextChanged;

                    txtAmount.Enabled = true;
                    txtAmount.BackColor = Color.White;
                    txtAmount.ReadOnly = false;
                    txtAmount.BorderStyle = BorderStyle.Fixed3D;

                    if (_Type == CassetteType.Cassette)
                    {
                        txtAmount.AllowDecimal = false;
                        txtAmount.MaxLength = 7;
                    }
                    else
                    {
                        txtAmount.AllowDecimal = true;
                        txtAmount.DecimalLength = 2;
                        txtAmount.MaxLength = 10;
                    }
                    //txtAmount.OnValidateDenom +=new InvalidDenomEvent(txtAmount_InvalidDenomEvent);
                    txtAmount.KeyDown += txtAmount_KeyDown;
                }
            }
        }
        public bool IsValidDenom()
        {
            return txtAmount.IsValidDenom();
        }
        public void SetFocus()
        {
            if (this.IsAmountEditable)
                this.txtAmount.Focus();
            else
                this.txtQuantity.Focus();
        }
        void txtAmount_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                if (txtAmount.IsValidDenom())
                {
                    //this.txtAmount_InvalidDenomEvent(sender, txtAmount.IsValidDenom());
                    this.txtAmount_TextChanged(sender, new EventArgs());
                }
                else
                {
                    if (OnInvalidDenom != null)
                        OnInvalidDenom(this.CassetteName, this.Amount.ToString());
                }
            }
        }

        void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
        private void txtAmount_InvalidDenomEvent(object Sender, bool IsValid)
        {
            if (!IsValid)
            {
                txtAmount.Text = (txtAmount.CTL_DecimalValue - (txtAmount.CTL_DecimalValue % txtAmount.Denom)).ToString();
            }
            this.txtAmount_TextChanged(Sender, new EventArgs());
        }
        public ucCassette()
        {
            InitializeComponent();
            _strCurrency = new RegionInfo(BMC.Common.ConfigurationManagement.ConfigManager.Read("GetDefaultCultureForCurrency")).CurrencySymbol;
        }
        public ucCassette(CassetteType Type)
            : this()
        {
            _Type = Type;
            IsAmountEditable = false;

        }
        public ucCassette(CassetteType Type, bool bIsAmountEditable)
            : this()
        {
            _Type = Type;
            this.IsAmountEditable = bIsAmountEditable;
        }

        public int Cassette_ID { get; set; }

        public object Tag { get; set; }

        public bool IsChildQuantityModified
        {
            get
            {
                return _IsChildQuantityModified;
            }
        }

        public List<ucCassette> this[decimal Denom]
        {
            get
            {
                if (this.GroupMembers.Count > 0)
                {
                    return GroupMembers.FindAll(obj => obj._Denom == Denom);
                }
                return null;
            }
        }


        [Browsable(true)]
        public List<ucCassette> GroupMembers
        {
            get
            {
                return _GroupMembers;
            }
        }

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

        public bool IsHeader
        {
            get
            {
                return _IsHeader;
            }
            set
            {
                txtQuantity.BorderStyle = BorderStyle.None;
                txtAmount.BorderStyle = BorderStyle.None;
                txtDropAmount.BorderStyle = BorderStyle.None;
                txtQuantity.BorderStyle = BorderStyle.None;
                txtAmount.BorderStyle = BorderStyle.None;
                txtDropAmount.BorderStyle = BorderStyle.None;
                //txtQuantity.Font = new Font(txtQuantity.Font, FontStyle.Bold); 
                //txtAmount.Font = new Font(txtDropAmount.Font, FontStyle.Bold); 
                //txtDropAmount.Font = new Font(txtDropAmount.Font, FontStyle.Bold); 
                txtQuantity.BackColor = Color.SteelBlue;
                txtAmount.BackColor = Color.SteelBlue;
                txtDropAmount.BackColor = Color.SteelBlue;
                this.BackColor = Color.SteelBlue;

                lblCassetteName.Text = this.GetResourceTextByKey("Key_Cassette");                               //"Cassette/Hopper";
                lblDenom.Text = string.Format(this.GetResourceTextByKey("Key_DenomCurrency"), _strCurrency);   // "Denom ({0})"
                txtQuantity.Text = this.GetResourceTextByKey("Key_Quantity");                                   //"Quantity";
                txtAmount.Text = string.Format(this.GetResourceTextByKey("Key_DeclaredAmount"), _strCurrency);        // "Declared Amount ({0})"
                txtDropAmount.Text = string.Format(this.GetResourceTextByKey("Key_DroppedAmount"), _strCurrency);     // "Dropped Amount ({0})"

                lblCassetteName.ForeColor = Color.White;
                lblDenom.ForeColor = Color.White;
                txtQuantity.ForeColor = Color.White;
                txtAmount.ForeColor = Color.White;
                txtDropAmount.ForeColor = Color.White;

                chkIsSelected.Visible = !value;
            }
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

        public bool Enabled
        {
            get
            {
                return _Enabled;
            }
            set
            {
                _Enabled = value;
                txtAmount.Enabled = value && IsAmountEditable;
                txtQuantity.Enabled = value && !IsAmountEditable;
            }
        }
        public decimal Variance
        {
            get
            {
                return DropAmount - Amount;
            }
        }

        public decimal Denom
        {
            get
            {
                return _Denom;
            }
            set
            {
                lblDenom.Text = value.ToString();
                _Denom = value;
                txtAmount.Denom = _Denom;
            }
        }
        public int Quantity
        {
            get
            {
                return _Quantity;
            }
            set
            {
                txtQuantity.Text = value.ToString();
                _Quantity = value;
            }
        }
        public decimal Amount
        {
            get
            {
                return _Amount;
            }
            set
            {
                txtAmount.Text = value.ToString();
                _Amount = value;
            }
        }
        public decimal DropAmount
        {
            get
            {
                return _DropAmount;
            }
            set
            {
                txtDropAmount.Text = value.ToString();
                _DropAmount = value;
            }
        }
        public bool IsSelected
        {
            get
            {
                return chkIsSelected.Checked;
            }
        }

        public decimal GroupSum
        {
            get { return _GroupSum; }
        }

        public void SubscribeAsChild(ucCassette Child)
        {
            _GroupMembers.Add(Child);
            Child.GroupParent = this;
            this.IsEditingEnabled = false;
            this.BackColor = Color.SteelBlue;
            lblCassetteName.ForeColor = Color.White;

        }

        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            int itemp;
            int.TryParse(this.txtQuantity.Text, out itemp);

            if (this.GroupParent != null)
            {
                if (this._Quantity != itemp)
                    this.GroupParent._IsChildQuantityModified = true;
            }

            this._Quantity = itemp;
            Amount = itemp * this._Denom;

            if (this.GroupParent != null)
            {
                this.GroupParent.SumGroupsDeclared();
            }

            if (OnQuantityChange != null)
                OnQuantityChange(sender, e);
        }

        void txtAmount_TextChanged(object sender, EventArgs e)
        {

            Decimal itemp;
            Decimal.TryParse(this.txtAmount.Text, out itemp);

            if (this.GroupParent != null)
            {
                if (this.Amount != itemp)
                    this.GroupParent._IsChildQuantityModified = true;
            }

            this.Quantity = Convert.ToInt32(Math.Floor(itemp / this._Denom));
            Amount = itemp;

            if (this.GroupParent != null)
            {
                this.GroupParent.SumGroupsDeclared();
            }

            if (OnQuantityChange != null)
                OnQuantityChange(sender, e);

        }


        public void ResetChildren()
        {
            this.GroupMembers.Clear();
            this._IsChildQuantityModified = false;
            this.Amount = 0;
            this.Quantity = 0;
        }

        public void EnabledChildren()
        {
            foreach (ucCassette item in GroupMembers)
            {
                item.Enabled = true;
            }
        }

        public void DisableChildren()
        {
            foreach (ucCassette item in GroupMembers)
            {
                item.Enabled = false;
            }
        }


        public void SumGroupsDeclared()
        {
            _GroupSum = 0;
            foreach (ucCassette item in GroupMembers)
            {
                _GroupSum += item._Amount;
            }
            this.Amount = _GroupSum;
        }

        public void ClearGroup()
        {
            _GroupSum = 0;
            foreach (ucCassette item in GroupMembers)
            {
                item.Quantity = 0;
                item.Amount = 0;
            }
            SumGroupsDeclared();
        }

        public void SumGroupsDropped()
        {
            _GroupSum = 0;
            decimal _DeclaredGroupSum = 0;

            foreach (ucCassette item in GroupMembers)
            {
                _GroupSum += item._DropAmount;
                _DeclaredGroupSum += item.Amount;
            }
            txtDropAmount.Text = _GroupSum.ToString();
            txtAmount.Text = _DeclaredGroupSum.ToString();
            this.DropAmount = _GroupSum;
        }

        public XElement CasseteXML
        {
            //<Cassette Cassette_ID="15" Denom="1" FillAmount="50.00" />  
            get
            {
                return new XElement("Cassette",
                         new XAttribute("Cassette_ID", this.Cassette_ID),
                         new XAttribute("Denom", this.Denom),
                         new XAttribute("FillAmount", this.Amount)
                    );
            }
        }

        private void txtAmount_Enter(object sender, EventArgs e)
        {
            if (this.GroupMembers.Count > 0)
            {
                SendKeys.Send("{TAB}");
            }
            if (IsAmountEditable && ((TextBox)sender).Name == "txtQuantity")
                SendKeys.Send("{TAB}");

        }

        private void txtAmount_Leave(object sender, EventArgs e)
        {
            if (IsAmountEditable)
            {
                txtAmount_TextChanged(sender, e);
            }
        }
    }
}
