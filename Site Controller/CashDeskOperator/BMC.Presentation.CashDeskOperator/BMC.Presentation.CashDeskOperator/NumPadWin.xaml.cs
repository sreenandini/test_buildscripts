using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BMC.Presentation.POS.Helper_classes;

namespace BMC.Presentation
{
	/// <summary>
	/// Interaction logic for NumberPadWind.xaml
	/// </summary>
    public partial class NumPadWin : Window, IDisposable
	{

        public event RoutedEventHandler EnterClicked;
        public event RoutedEventHandler CancelClicked;

        public NumPadWin()
		{
			this.InitializeComponent();
		// Insert code required on object creation below this point.
		}

        private void Drag_Event(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void EnterClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        public string ValueText 
        { 
            get
            {
                return vcValueCalcComp.ValueText;
            }
            set
            {
                vcValueCalcComp.ValueText = value;
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
                        this.Window.MouseDown -= (this.Drag_Event);
                        ((System.Windows.Controls.Button)(Button_11)).Click -= (this.EnterClick);
                        ((System.Windows.Controls.Button)(Button_12)).Click -= (this.CancelClick);
                    },
                    (c) =>
                    {
                    });
                    this.WriteLog("NumPadWin objects are released successfully.");

                }
                disposed = true;
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="NumPadWin"/> is reclaimed by garbage collection.
        /// </summary>
        ~NumPadWin()
        {
            Dispose(false);
        }

        #endregion
	}
}