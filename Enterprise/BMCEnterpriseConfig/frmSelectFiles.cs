using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.Common.ExceptionManagement;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Security.Permissions;
using System.Security;

namespace BMC
{
    public partial class frmSelectFiles : Form
    {
        public const string strMessageBoxCaption = "BMC Enterprise Configuration";

        List<string> lstMdfFile = new List<string>();
        List<string> lstLdfFile = new List<string>();

        private delegate bool DirectoryExistsDelegate(string folder);

        #region Properties

        private string _strDataFilePath;
        private string _strLogFilePath;

        public string StrDataFilePath
        {
            get { return _strDataFilePath; }
        }

        public string StrLogFilePath
        {
            get { return _strLogFilePath; }
        }

        #endregion Properties


        public frmSelectFiles(bool _bEnterpriseDBExistis, bool _bAuditDBExists, bool _bMeterAnalysisDBExists)
        {
            InitializeComponent();

            if (!_bEnterpriseDBExistis)
            {
                lstMdfFile.Add("Enterprise.mdf");
                lstLdfFile.Add("Enterprise.ldf");
            }
            if (!_bAuditDBExists)
            {
                lstMdfFile.Add("Audit.mdf");
                lstLdfFile.Add("Audit.ldf");
            }
            if (!_bMeterAnalysisDBExists)
            {
                lstMdfFile.Add("MeterAnalysis.mdf");
                lstLdfFile.Add("MeterAnalysis.ldf");
            }
        }

        private void btn_BrowseDataFilePath_Click(object sender, EventArgs e)
        {
            try
            {
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    txtDataFilePath.Text = folderBrowserDialog1.SelectedPath;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btn_BrowseLogFilePath_Click(object sender, EventArgs e)
        {
            try
            {
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    txtLogFilePath.Text = folderBrowserDialog1.SelectedPath;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        bool DirectoryExistsTimeout(string path, int millisecondsTimeout)
        {
            try
            {
                DirectoryExistsDelegate callback = new DirectoryExistsDelegate(Directory.Exists);
                IAsyncResult result = callback.BeginInvoke(path, null, null);

                if (result.AsyncWaitHandle.WaitOne(millisecondsTimeout, false))
                {
                    return callback.EndInvoke(result);
                }
                else
                {
                    callback.EndInvoke(result);

                    return false;
                }
            }

            catch (Exception)
            {
                return false;
            }
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            string strDataFilePath = txtDataFilePath.Text.Trim();
            string logFilePath = txtLogFilePath.Text.Trim();
            try
            {
                if (strDataFilePath == string.Empty)
                {
                    MessageBox.Show("Please select Database Data File Path", strMessageBoxCaption);
                    txtDataFilePath.Focus();
                    return;
                }
                if (logFilePath == string.Empty)
                {
                    MessageBox.Show("Please select Database Log File Path", strMessageBoxCaption);
                    txtLogFilePath.Focus();
                    return;
                }

                if (!DirectoryExistsTimeout(txtDataFilePath.Text, 5000))
                {
                    MessageBox.Show("Data File Path is not valid. Please select a valid path", strMessageBoxCaption);
                    txtDataFilePath.SelectAll();
                    txtDataFilePath.Focus();
                    return;
                }

                if (!DirectoryExistsTimeout(txtLogFilePath.Text, 5000))
                {
                    MessageBox.Show("Log File Path is not valid. Please select a valid path", strMessageBoxCaption);
                    txtLogFilePath.SelectAll();
                    txtLogFilePath.Focus();
                    return;
                }

                foreach (string mdf in lstMdfFile)
                {
                    if (File.Exists(txtDataFilePath.Text + "\\" + mdf ))
                    {
                    MessageBox.Show("Select another Database Data File Path. " + mdf + " already located in this path", strMessageBoxCaption);
                    txtDataFilePath.SelectAll();
                    txtDataFilePath.Focus();
                    return;
                    }                    
                }

                foreach (string ldf in lstLdfFile)
                {
                    if (File.Exists(txtLogFilePath.Text + "\\" + ldf))
                    {
                        MessageBox.Show("Select another Database Log File Path. " + ldf + " already located in this path", strMessageBoxCaption);
                        txtDataFilePath.SelectAll();
                        txtDataFilePath.Focus();
                        return;
                    }
                }

                _strDataFilePath = txtDataFilePath.Text;
                _strLogFilePath = txtLogFilePath.Text;
                this.Close();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
