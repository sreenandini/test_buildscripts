using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BMC.Common.ExceptionManagement;
using BMC.Transport.CashDeskOperatorEntity;
using BMC.Transport;
using BMC.Presentation.POS.Helper_classes;

namespace BMC.Presentation
{
	/// <summary>
	/// Interaction logic for McahineRemoval.xaml
	/// </summary>
    public partial class MachineRemoval : Window, IDisposable
	{
        PositionDetails objPositionDetails;
        public MachineRemoval(PositionDetails objPd)
		{
			this.InitializeComponent();
            MessageBox.childOwner = this;
            objPositionDetails = objPd;
            txt_GMU_No.Text = objPositionDetails.GMUNo;
            txt_MC_AssetNo.Text = objPositionDetails.AssetNo;
            txt_Position.Text = objPositionDetails.Position;

            txt_GMU_No.IsReadOnly = true;
            txt_MC_AssetNo.IsReadOnly = true;
            txt_Position.IsReadOnly = true;
            com_GMU_Type.Items.Add("MC300");
            com_GMU_Type.Items.Add("Connexus");
            com_GMU_Type.Items.Add("NoDevice");
            com_GMU_Type.SelectedIndex = 0;
            if (txt_GMU_No.Text.Trim().Length == 0 || txt_GMU_No.Text == "0")
                com_GMU_Type.SelectedIndex = 2;
		}

        private void btn_Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private string DisplayNumberPad(string keytext)
        {
            string strNumberPadText = string.Empty;
            NumberPadWind ObjNumberpadWind = new NumberPadWind();            

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

        private void txt_50_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void txt_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            ((TextBox)sender).Text = DisplayNumberPad(((TextBox)sender).Text.Trim());
            if (((TextBox)sender).Text.Trim()=="")
                ((TextBox)sender).Text ="0";

            txt_Total_Cash.Text = Double.Parse((Int32.Parse(txt_1.Text) + Int32.Parse(txt_2.Text) + Int32.Parse(txt_5.Text) + Int32.Parse(txt_10.Text) + Int32.Parse(txt_20.Text) + Int32.Parse(txt_50.Text) + Double.Parse(txt_Total_Coin.Text)).ToString()).ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.ShowBox("MessageID9", BMC_Icon.Warning, BMC_Button.OK);
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
                        this.txt_Total_Coin.PreviewMouseUp -= (this.txt_PreviewMouseUp);
                        this.txt_1.PreviewMouseUp -= (this.txt_PreviewMouseUp);
                        this.txt_2.PreviewMouseUp -= (this.txt_PreviewMouseUp);
                        this.txt_5.PreviewMouseUp -= (this.txt_PreviewMouseUp);
                        this.txt_10.PreviewMouseUp -= (this.txt_PreviewMouseUp);
                        this.txt_20.PreviewMouseUp -= (this.txt_PreviewMouseUp);
                        this.txt_50.PreviewMouseUp -= (this.txt_PreviewMouseUp);
                        this.btnRemove.Click -= (this.btnRemove_Click);
                        ((System.Windows.Controls.Button)(btn_Exit)).Click -= (this.btn_Exit_Click);

                    },
                    (c) =>
                    {
                    });
                    this.WriteLog("MachineRemoval objects are released successfully.");

                }
                disposed = true;
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="MachineRemoval"/> is reclaimed by garbage collection.
        /// </summary>
        ~MachineRemoval()
        {
            Dispose(false);
        }

        #endregion
	}
}