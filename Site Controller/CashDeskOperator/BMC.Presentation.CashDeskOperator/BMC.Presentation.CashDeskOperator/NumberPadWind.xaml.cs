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
    public partial class NumberPadWind : Window, IDisposable
	{

        private bool _isPlayerClub;
        public bool isPlayerClub
        {
            get { 
                return _isPlayerClub; 
            } 
            set
            { 
                _isPlayerClub = value;
                ucTicketEntry.isPlayerClub = value;
            } 
        }

        public void setMaxLength(int iLength)
        {
            ucTicketEntry.MaxLength=iLength;
        }
        public NumberPadWind()
		{
			this.InitializeComponent();
            isPlayerClub = false;
            ucTicketEntry.isCurrencyPad = false;
		// Insert code required on object creation below this point.
		}
        //
        public NumberPadWind(bool isCurrencyPad)
        {
            this.InitializeComponent();
            isPlayerClub = false;
            ucTicketEntry.isCurrencyPad = isCurrencyPad;

            // Insert code required on object creation below this point.
        }

        private void Drag_Event(object sender, MouseButtonEventArgs e)
        {
            try // Added to avoid exception on right click and drag
            {
                if(e.ChangedButton==MouseButton.Left)
                this.DragMove();
            }
            catch (Exception Ex)
            {
                BMC.Common.ExceptionManagement.ExceptionManager.Publish(Ex);
            }
        }

        private void CTicketEntry_EnterClicked(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }

        private void CTicketEntry_CancelClicked(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        public string ValueText 
        { 
            get
            {
                return ucTicketEntry.ValueText;
            }
            set
            {
                ucTicketEntry.ValueText = value;
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
                    },
                    (c) =>
                    {
                    });
                    this.WriteLog("NumberPadWind objects are released successfully.");

                }
                disposed = true;
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="NumberPadWind"/> is reclaimed by garbage collection.
        /// </summary>
        ~NumberPadWind()
        {
            Dispose(false);
        }

        #endregion
	}
}