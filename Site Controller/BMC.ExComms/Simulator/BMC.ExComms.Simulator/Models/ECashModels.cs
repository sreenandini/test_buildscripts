using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Simulator.Models
{
    public class ECashTextValueModel
    {
        public int SNo { get; set; }

        public string Text { get; set; }

        public string Value { get; set; }

        public string TextValue
        {
            get
            {
                return this.Text + " (" + this.Value + ")";
            }
        }
    } 
    
    public abstract class ECashTextValueModelBaseCollection : ObservableCollection<ECashTextValueModel>
    {
        public ECashTextValueModelBaseCollection (){}

        public void AddTextValue(string text, string value)
        {
            ECashTextValueModel model = new ECashTextValueModel();
            model.SNo = this.Count + 1;
            model.Text = text;
            model.Value = value;
            this.Add(model);
        }
    }

    public class ECashTextValueModelCollection : ECashTextValueModelBaseCollection
    {
        public ECashTextValueModelCollection()
        {
            this.AddTextValue("Option 1 Withdrawal Amount", "100");
            this.AddTextValue("Option 2 Withdrawal Amount", "500");
            this.AddTextValue("Option 3 Withdrawal Amount", "1000");
            this.AddTextValue("Option 4 Withdrawal Amount", "2000");
            this.AddTextValue("Option 5 Withdrawal Amount", "5000");
            this.AddTextValue("Max Withdrawal Amount", "");
            this.AddTextValue("Max Deposit Amount", "");
        }
    }

    public class ECashWithdrawOptionModelCollection : ECashTextValueModelBaseCollection
    {
        public ECashWithdrawOptionModelCollection()
        {
            this.AddTextValue("Promo", "1");
            this.AddTextValue("Points", "2");
            this.AddTextValue("Cash", "3");
        }
    }
}
