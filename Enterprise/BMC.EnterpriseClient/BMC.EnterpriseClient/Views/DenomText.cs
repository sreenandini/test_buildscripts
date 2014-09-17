using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.Common.LogManagement;
using System.Globalization;

namespace BMC.EnterpriseClient.Views
{
    public partial class DenomText : NumberTextBox
    {

        bool _IsInvalid;
        public EventHandler OnInValidDenom;
        public EventHandler OnValidDenom;
        [Browsable(true)]
        public float Denom { get; set; }
        decimal _DecimalValue;
        int _IntValue;

        [Browsable(false)]
        public bool IsInvalid
        {
            get
            {
                return _IsInvalid;
            }
            set
            {
                if (value)
                {
                    this.BackColor = Color.Bisque;
                }
                else
                {
                    this.BackColor = Color.White;

                }
                _IsInvalid = value;

            }
        }

        [Browsable(false)]
        public string UniqueNo = Guid.NewGuid().ToString();

        string _text = string.Empty;
        
        public DenomText()
        {
            InitializeComponent();
            this.Text = "0";
        }

        public decimal DecimalValue
        {
            get
            {
                try
                {
                    if (this.Text.IndexOf("(") > -1)
                        return decimal.Parse(string.Format("-{0}", this.Text.Replace("(", "").Replace(")", "")), NumberStyles.Number, CultureInfo.CurrentUICulture);
                    else
                        return decimal.Parse(this.Text, NumberStyles.Number, CultureInfo.CurrentUICulture);
                }
                catch 
                {
                    return 0;
                }
          
            }
        }

        public int IntValue
        {
            get
            {

                try
                {
                    if (this.Text.IndexOf("(") > -1)
                        return int.Parse(string.Format("-{0}", this.Text.Replace("(", "").Replace(")", "")), NumberStyles.Number, CultureInfo.CurrentUICulture);
                    else
                        return int.Parse(this.Text, NumberStyles.Number, CultureInfo.CurrentUICulture);

                }
                catch 
                {
                    return 0;
                }
                //return _IntValue;
            }
        }

        protected override void OnGotFocus(EventArgs e)
        {
            if (this.AllowDecimal)
            {
                this.Text = this.DecimalValue.ToString("0.00");
            }
            else
                this.Text = this.IntValue.ToString();

        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            ValidateDenom(e);
        }

        public void ValidateDenom(EventArgs e)
        {

            if (Denom != 0)  
            {

                decimal.TryParse(this.Text,out _DecimalValue);
                int.TryParse(this.Text,out _IntValue);

                if (this.DecimalValue % decimal.Parse(Denom.ToString()) != 0)
                {
                    this.IsInvalid = true;
                    if (OnInValidDenom != null)
                        OnInValidDenom(this,e);
                }
                else
                {
                    this.IsInvalid = false;
                    if (OnValidDenom != null)
                        OnValidDenom(this,e);
                }
            }

            //if (this.DecimalValue < 0)
            //{
            //    if (this.AllowDecimal)
            //        this.Text = string.Format("({0})", (this.DecimalValue * -1).ToString("#,##0.00"));
            //    else
            //        this.Text = string.Format("({0})", (this.DecimalValue * -1).ToString("#,##0"));
            //}
            //else
            //{
            //    if (this.AllowDecimal)
            //        this.Text = (this.DecimalValue).ToString("#,##0.00");
            //    else
            //        this.Text = (this.DecimalValue).ToString("#,##0"); ; 
            //}


        }
    }
}
