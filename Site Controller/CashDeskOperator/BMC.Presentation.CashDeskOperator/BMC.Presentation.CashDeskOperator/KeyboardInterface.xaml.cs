using System;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using BMC.Presentation.POS.Helper_classes;

namespace BMC.Presentation
{
	public partial class KeyboardInterface : IDisposable
	{

        private string s_KeyboardString = string.Empty;
        private bool b_IsPwd = false;

        public string KeyString 
        { 
            get
            {
                return this.objKeyboard.CurrentValue;
            }
            set
            {
                this.objKeyboard.CurrentValue = value;
            
            }
        }

        public bool IsPwd 
        {
            get { return b_IsPwd; }
            set 
            { 
                b_IsPwd = value;
                this.objKeyboard.IsPasswordMode = b_IsPwd;
            }
        }
        
        public KeyboardInterface()
		{
			this.InitializeComponent();

         
            
			
			// Insert code required on object creation below this point.
		}

        private void CKeyboard_EnterClicked(object sender, EventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void objKeyboard_CancelClicked(object sender, EventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void objKeyboard_MoveKeyboard(object sender, EventArgs e)
        {
            this.DragMove();
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
                        objKeyboard.EnterClicked -= CKeyboard_EnterClicked;
                        objKeyboard.CancelClicked -= objKeyboard_CancelClicked;
                        objKeyboard.MoveKeyboard -= objKeyboard_MoveKeyboard;
                    },
                    (c) =>
                    {
                    });
                    this.WriteLog("KeyboardInterface objects are released successfully.");

                }
                disposed = true;
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="KeyboardInterface"/> is reclaimed by garbage collection.
        /// </summary>
        ~KeyboardInterface()
        {
            Dispose(false);
        }

        #endregion
	}
}