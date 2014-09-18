using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BMC.Business.ExchangeConfig;

namespace BMC.UI.ExchangeConfig
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
             PaintGradient();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            txtUserName.Text = "bally";
        }

        //private void PaintGradient()
        //{

        //    System.Drawing.Drawing2D.LinearGradientBrush gradBrush;

        //    System.Drawing.Drawing2D.ColorBlend clrbld;
        //    clrbld = new System.Drawing.Drawing2D.ColorBlend(3);
        //    gradBrush = new System.Drawing.Drawing2D.LinearGradientBrush(new
        //           Point(0, 0), new Point(this.Width, this.Height), Color.FromArgb(0, 100, 220), Color.White);
        //    //Color.FromArgb(143, 199, 255)
        //    Bitmap bmp = new Bitmap(this.Width, this.Height);

        //    Graphics g = Graphics.FromImage(bmp);

        //    g.FillRectangle(gradBrush, new Rectangle(0, 0, this.Width, this.Height));

        //    this.BackgroundImage = bmp;
        //    this.BackgroundImageLayout = ImageLayout.Stretch;
        //}

        private void btnLogin_Click(object sender, EventArgs e)
        {
            CheckUsers();
        }

        private void btnLogin_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            CheckUsers();
        }

        private void txtPassword_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CheckUsers();
            }
        }
        /// <summary>
        /// To set the button and form color
        /// </summary>
        /// <param name=></param>
        /// <returns></returns>      
        /// Author             Date              Description
        /// ---------------------------------------------------
        /// Vineetha Mathew      19-02-2009        Intial Version 
        /// 
        private void PaintGradient()
        {
            string strBMPath = string.Empty;
            System.Drawing.Drawing2D.LinearGradientBrush gradBrushButton;
            Graphics grObject;
            System.Drawing.Drawing2D.ColorBlend clrblend = null;
            Rectangle objrect;
            Bitmap objbmp = null;
          
                Color[] clrSet = new Color[4]{                                      
                                    Color.FromArgb(119,187,255),                                                                        
                                     Color.FromArgb(210,232,255),
                                     Color.FromArgb(232,244,255),
                                    Color.FromArgb(255,255,255)};
                clrblend = new System.Drawing.Drawing2D.ColorBlend();
                clrblend.Colors = clrSet;
                Single[] bPts = new Single[4]{
                                            0,                                          
                                            0.5F,
                                            0.8F,                                          
                                            1};
                clrblend.Positions = bPts;
                gradBrushButton = new System.Drawing.Drawing2D.LinearGradientBrush(new
                       Point(0, 0), new Point(this.Width, this.Height), Color.FromArgb(217, 230, 255), Color.White);
                gradBrushButton.InterpolationColors = clrblend;
                objrect = new Rectangle(0, 0, this.Width, this.Height);
                objbmp = new Bitmap(this.Width, this.Height);              

                grObject = Graphics.FromImage(objbmp);
                grObject.FillRectangle(gradBrushButton, objrect);

                btnCancel.BackgroundImage = objbmp;
                btnLogin.BackgroundImage = objbmp;
                btnExit.BackgroundImage = objbmp;
                btnCancel.BackgroundImageLayout = ImageLayout.Stretch;
                btnLogin.BackgroundImageLayout = ImageLayout.Stretch;
                btnExit.BackgroundImageLayout = ImageLayout.Stretch;

                this.BackgroundImage = objbmp;
                this.BackgroundImageLayout = ImageLayout.Stretch;
          
        }

        /// <summary>
        /// Check if the user credentials are valid
        /// </summary>
        /// <param name="ConnectionString"></param>
        /// <returns>success or failure</returns>
        /// Method Revision History
        ///
        /// Author             Date              Description
        /// ---------------------------------------------------
        /// Anuradha        05-Jan-2009        Intial Version 
        /// 
        private void CheckUsers()
        {
            if (ValidateEnteredText())
            {
                bool bValid = Credentials.Checkuser(txtUserName.Text, txtPassword.Text);
                if (bValid)
                {
                    frmBMCExchangeConfig objMainForm = new frmBMCExchangeConfig();
                    this.Hide();
                    //this.Close();
                    objMainForm.ShowDialog();
                   
                }
                else
                {
                    MessageBox.Show("Invalid UserName / Password, Please Try again...", "BMC Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.Focus();
                    return;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtUserName.Text = "bally";
            txtPassword.Text = string.Empty;
            txtUserName.Focus();
        }

        private bool ValidateEnteredText()
        {
            bool bValidate = false;
            if (txtUserName.Text.Length == 0)
            {
                errLoginValidate.SetError(txtUserName, "Enter UserName");
                bValidate= false;
            }
            else if (txtUserName.Text.Length > 0)
            {
                errLoginValidate.SetError(txtUserName, string.Empty);
                bValidate= true;
            }
            if (txtPassword.Text.Length == 0)
            {
                errLoginValidate.SetError(txtPassword, "Enter Password");
                bValidate= false;
            }
            else if (txtPassword.Text.Length > 0)
            {
                errLoginValidate.SetError(txtPassword, string.Empty);
                bValidate= true;
            }

            return bValidate;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}