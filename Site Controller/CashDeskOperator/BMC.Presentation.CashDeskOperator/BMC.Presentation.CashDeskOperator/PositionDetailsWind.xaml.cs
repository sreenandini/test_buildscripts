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
	/// Interaction logic for PositionDetailsWind.xaml
	/// </summary>
    public partial class PositionDetailsWind : IDisposable
	{

        private SlotMachine _SlotMachine = null;
        public PositionDetail POSDetail;
        public event EventHandler ExitClicked;
        
		public PositionDetailsWind()
		{
			this.InitializeComponent();
            
			
			// Insert code required on object creation below this point.
		}


        public PositionDetailsWind(PositionDetail PositionDetail)
        {            
            this.InitializeComponent();
            ucPosDetailsComp.POSDetail = PositionDetail;            
        }

        private void PositionDetailsComp_Exit(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            if(ExitClicked !=null)
            ExitClicked.Invoke(this, EventArgs.Empty);
            

        }

        private void Drag_Event(object sender, MouseButtonEventArgs e)
        {

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                //this.Opacity = 0.7;
                //this.DragMove();
            }
             
        }



        public SlotMachine SlotMachine
        {
            get
            {
                return _SlotMachine;
            }
            set
            {
                _SlotMachine = value;
                UpdateDetails();
            }
        }

        private void UpdateDetails()
        {
            ucPosDetailsComp.PositionText =_SlotMachine.SlotNumber;
            ucPosDetailsComp.StatusColor = _SlotMachine.OuterRoundColor;
            ucPosDetailsComp.IsHandPay = _SlotMachine.IsHandPay;
        }

        private void Window_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Opacity = 1.0;
        }

        // Using a DependencyProperty as the backing store for IsHandPay.  This enables animation, styling, binding, etc...

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
                        this.Window.MouseUp -= (this.Window_MouseUp);
                    },
                    (c) =>
                    {
                    });
                    this.WriteLog("PositionDetailsWind objects are released successfully.");

                }
                disposed = true;
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="PositionDetailsWind"/> is reclaimed by garbage collection.
        /// </summary>
        ~PositionDetailsWind()
        {
            Dispose(false);
        }

        #endregion

	}


}