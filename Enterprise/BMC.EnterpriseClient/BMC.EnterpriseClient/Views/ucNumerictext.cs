using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace BMC.EnterpriseClient.Views
{
    public delegate void InvalidDenomEvent(object Source,bool IsValid);

    public partial class NumberTextBox : TextBox
    {
        public object  Tag { get; set; }

        private decimal _MaxVaule = 999999.99M;

        public decimal MaxVaule
        {
            get
            {
                return _MaxVaule;
            }
            set
            {
                _MaxVaule = value;
            }
        }

        char decimalSeparator;
        public NumberTextBox()
        {
            DecimalLength = 2;
            MaxLength = 8;
            this.ShortcutsEnabled = false;
            decimalSeparator = '.';
        }

        public InvalidDenomEvent OnValidateDenom;

        [Browsable(true)]
        public bool AllowDecimal { get; set; }
        [Browsable(true)]
        public bool AllowNegative { get; set; }
        [Browsable(true)]
        public int DecimalLength { get; set; }

        [Browsable(true)]
        public decimal  Denom { get; set; }

        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);

            if (OnValidateDenom != null && Denom != 0)
            {
                if(CTL_DecimalValue%Denom >0)
                    OnValidateDenom(this,false);
                else
                    OnValidateDenom(this, true);
           }
        }

        public bool CapacityExceeded
        {
            get
            {
                if (this.CTL_DecimalValue > this.MaxVaule)
                {
                    return true ;
                }
                else
                {
                    return false;
                }             
            }            
        }        

        public bool IsValidDenom()
        {
            if (Denom != 0)
            {
                if (CTL_DecimalValue % Denom > 0)
                    return false;
                else
                    return true;
            }
            return true;
        }


        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            NumberFormatInfo numberFormatInfo = System.Globalization.CultureInfo.CurrentCulture.NumberFormat;
            decimalSeparator = numberFormatInfo.NumberDecimalSeparator[0];
            string groupSeparator = numberFormatInfo.NumberGroupSeparator;
            string negativeSign = numberFormatInfo.NegativeSign;

            string keyInput = e.KeyChar.ToString();

            if (Char.IsDigit(e.KeyChar) || e.KeyChar == decimalSeparator)
            {
                //Validated decimal lenght 
                if (this.AllowDecimal)
                {
                    //When no decimal poins is not found and  whole number lenght greater than textBox length - decimal lenght + 1( for ".")
                    if (e.KeyChar != decimalSeparator && this.Text.IndexOf(decimalSeparator) == -1 && (this.Text.Length >= this.MaxLength - (DecimalLength + 1)))
                     {
                         if (this.SelectedText.Length <= 0)
                         {
                             e.Handled = true;
                             return;
                         }
                     }
                    //When no decimal poins is found and whole number lenght greater than textBox length - decimal lenght + 1( for ".")
                    else if (this.Text.IndexOf(decimalSeparator) > -1 && (this.Text.Split('.')[0].Length >= this.MaxLength - (DecimalLength + 1)) && this.Text.IndexOf(decimalSeparator) >= this.SelectionStart)
                    {
                        //To allow typing when text is selected in the text box 
                        if (this.SelectedText.Length <= 0 || this.SelectedText==decimalSeparator.ToString())
                        {
                            e.Handled = true;
                            return;
                        }
                     }

                }
                if (e.KeyChar == decimalSeparator) // When "." is typed
                {
                    if (keyInput == decimalSeparator.ToString() && AllowDecimal && !this.Text.Contains(e.KeyChar) &&  ((this.Text.Length-this.SelectionStart) <= this.DecimalLength))
                    {
                        //Donot allow decimal based on property 
                        //Allow only one decimal symbol in the control 
                    }
                    else
                    {
                        e.Handled = true;
                        return;
                    }
                }
                else
                 if ((this.Text.IndexOf(decimalSeparator) == -1) || (this.Text.IndexOf(decimalSeparator) >= this.SelectionStart) || (this.Text.IndexOf(decimalSeparator) > -1 && (this.Text.Split(decimalSeparator)[1].Length < this.DecimalLength)))
                {

                }
                else if (this.SelectedText.Length > 0)
                {

                }
                else
                {
                    e.Handled = true;
                }
            }
            else if (AllowNegative && this.SelectionStart == 0 && keyInput.Equals(negativeSign))
            {

            }
            else if (e.KeyChar == '\b')
            {

            }
            else
            {
                e.Handled = true;
            }

        }
        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            if (AllowDecimal)
            {

                if (this.Text.IndexOf(decimalSeparator)== -1 && (this.Text.Length >this.MaxLength - (DecimalLength + 1)))
                {
                    this.Text = this.Text.Substring(0, this.MaxLength - (DecimalLength + 1));
                }
            }
        }
        [Browsable(false)]
        public int CTL_IntValue
        {
            get
            {
                return Convert.ToInt32(CTL_DecimalValue);
            }
        }
        [Browsable(false)]
        public decimal CTL_DecimalValue
        {
            get
            {
                decimal val;
                decimal.TryParse(this.Text,out val);
                return val;
            }
        }


    }
}
