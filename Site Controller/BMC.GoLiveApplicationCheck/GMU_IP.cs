using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Microsoft.Win32;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.Common.ConfigurationManagement;
using BMC.DataAccess;
using System.Net.NetworkInformation;
using BMC.Common.Utilities;

namespace BMC_GoLiveApplication_Check
{
    public partial class GMU_IP : Form
    {
        string strConnection = string.Empty;
        DataSet objDs;

        public GMU_IP()
        {
            ConfigManager.SetConfigurationMode(ConfigManager.ConfigurationMode.AppConfig);
            InitializeComponent();
            strConnection = GetConnectionString();
            objDs = new DataSet();
            //timer1.Enabled = true;
            //timer1.Interval = 1000 * int.Parse(ConfigManager.Read("TimerIntervalinSecs"));
        }

        private string GetConnectionString()
        {
            return BMC.Common.Utilities.DatabaseHelper.GetConnectionString();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            PageRefresh();
        }

        /// <summary>
        /// Reloads the dataset
        /// </summary>
        private void PageRefresh()
        {
            dataGridView1.DataSource = null; 
            
            objDs.Clear();
            try
            {
                objDs = SqlHelper.ExecuteDataset(strConnection, CommandType.StoredProcedure, "rsp_FetchGMULogin");
                dataGridView1.DataSource = objDs.Tables[0];
                objDs.Tables[0].Columns.Add("Ping Status");
                objDs.Tables[0].Columns.Add("Sector Status");
                label3.Text = DateTime.Now.ToShortTimeString();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            finally
            {
                objDs.Dispose();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            PageRefresh();
        }

        private void GMU_IP_Load(object sender, EventArgs e)
        {
            PageRefresh();
        }


        /// <summary>
        /// Pings the selected GMU
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPing_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount == 0)
                return;
            string strIP = string.Empty;
            bool reply;

            try
            {
                strIP = dataGridView1.SelectedCells[2].Value.ToString();

                reply = Ping(strIP);

                if (reply)
                {
                    dataGridView1.SelectedCells[3].Value = "OK";
                    MessageBox.Show("Ping GMU - SUCCESS", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    dataGridView1.SelectedCells[3].Value = "Failed";
                    dataGridView1.SelectedCells[3].ErrorText = "Failed";
                    MessageBox.Show("Ping GMU - FAILED", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
            }
        }
        

        /// <summary>
        /// Pings all displayed GMUs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount == 0)
                return;
            String strIP;

            try
            {

                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    dataGridView1[1, i].Selected = true;
                    strIP = dataGridView1[2, i].Value.ToString();
                    if (Ping(strIP))
                    {
                        dataGridView1[3, i].ErrorText = "";
                        dataGridView1[3, i].Value = "OK";
                    }
                    else
                    {
                        dataGridView1[3, i].Value = "Failed";
                        dataGridView1[3, i].ErrorText = "Failed";

                    }

                }
            }
            catch (Exception Ex)
            { }

        }

        /// <summary>
        /// Gets Sectors for selected GMU
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount == 0)
                return;
            String DatapakNo;

            try
            {
                DatapakNo = dataGridView1.SelectedCells[1].Value.ToString();
                if (GetSectors(Int16.Parse(DatapakNo)))
                {
                    dataGridView1.SelectedCells[4].ErrorText = "";
                    dataGridView1.Refresh();
                    dataGridView1.SelectedCells[4].Value = "OK";
                    MessageBox.Show("Fetching sectors - SUCCESS ", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    dataGridView1.SelectedCells[4].Value = "Failed";
                    dataGridView1.SelectedCells[4].ErrorText = "Failed";
                    MessageBox.Show("Fetching sectors - FAILED!!! ", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            catch (Exception Ex)
            { }
           
        }

        /// <summary>
        /// Gets Sectors for all displayed GMUs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount == 0)
                return;
            String DatapakNo;

            try
            {

                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    dataGridView1[1, i].Selected = true;
                    DatapakNo = dataGridView1[1, i].Value.ToString();
                    if (GetSectors(Int16.Parse(DatapakNo)))
                    {
                        dataGridView1.SelectedCells[4].ErrorText = "";
                        dataGridView1[4, i].Value = "OK";
                    }
                    else
                    {
                        dataGridView1[4, i].Value = "Failed";
                        dataGridView1[4, i].ErrorText = "Failed";
                    }

                }
            }
            catch (Exception Ex)
            {
            }
            finally
            { }
        }

        /// <summary>
        /// Gets sectors for datapak
        /// </summary>
        /// <param name="DatapakNo"></param>
        /// <returns></returns>
        private bool GetSectors(short DatapakNo)
        {
            //bool bFailed=false;
            //bool bForceOnline = true;
            //this.Cursor = Cursors.WaitCursor;
            //BGSComExchange.clsAsyncComExchange objComExchange = new BGSComExchange.clsAsyncComExchange();
            //BGSComExchange.EnumExComSectors Sectors = new BGSComExchange.EnumExComSectors();
            //Sectors = BGSComExchange.EnumExComSectors.READ_ESSENTIAL;
            //objComExchange.ExchangeReadAsyncBodge(ref DatapakNo, ref Sectors, ref Sectors, ref bFailed, ref bForceOnline);
            //this.Cursor = Cursors.Arrow;
            ////objComExchange.ExchangeReadAsyncBodge(ref DatapakNo,BGSComExchange.EnumExComSectors.READ_ESSENTIAL, BGSComExchange.EnumExComSectors.READ_ALL_FINANCIAL,ref bFailed,ref bFailed);
            //return bFailed;
            return true;

        }

        /// <summary>
        /// Pings the IP
        /// </summary>
        /// <param name="strIP"></param>
        /// <returns></returns>
        private bool Ping(String strIP)
        {

            Ping ping = new Ping();
            PingReply reply;

            try
            {
                this.Cursor = Cursors.WaitCursor;
                reply = ping.Send(strIP, 1000);

                if (reply.Status == IPStatus.Success)
                {
                    return (true);
                }

            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
                return (false);
            }
            finally
            {
                this.Cursor = Cursors.Arrow;
            }
            return (false);
        }

        private void GMU_IP_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmGoLive.GMUFormOpened = false;
        }

       
    }
}