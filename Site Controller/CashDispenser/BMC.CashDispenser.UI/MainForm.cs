using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.CashDispenser.Core;
using BMC.Common.ExceptionManagement;

namespace BMC.CashDispenser.UI
{
    public partial class MainForm : Form
    {
        private ICashDispenser _dispenser = null;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.LoadItems();
        }

        private void LoadItems()
        {
            try
            {
                _dispenser = CashDispenserFactory.GetDispenser();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (_dispenser != null)
                {
                    dgvItems.Rows.Clear();

                    foreach (CashDispenserItem item in _dispenser.DispenserItems)
                    {
                        int rowIndex = dgvItems.Rows.Add();
                        DataGridViewRow row = dgvItems.Rows[rowIndex];
                        row.Cells[chdrSNo.Index].Value = (rowIndex + 1);
                        row.Cells[chdrCassetteAlias.Index].Value = item.CassetteAlias;
                        row.Cells[chdrDenomination.Index].Value = item.Denimination;
                        row.Cells[chdrTotalValue.Index].Value = item.TotalValue;
                        row.Tag = item;
                    }

                    dgvItems.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool overallResult = true;

            try
            {
                foreach (DataGridViewRow row in dgvItems.Rows)
                {
                    CashDispenserItem item = row.Tag as CashDispenserItem;
                    if (item != null)
                    {
                        int iValue = 0;
                        Decimal dValue = 0;
                        bool result = false;

                        item.CassetteAlias = row.Cells[chdrCassetteAlias.Index].Value.ToString();

                        Int32.TryParse(row.Cells[chdrDenomination.Index].Value.ToString(), out iValue);
                        item.Denimination = iValue;

                        Decimal.TryParse(row.Cells[chdrTotalValue.Index].Value.ToString(), out dValue);
                        item.TotalValue = dValue;

                        result = CashDispenserFactory.UpdateItem(item);
                        overallResult &= result;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (overallResult)
                {
                    MessageBox.Show("All the details are saved successfully.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Unable to save some items.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnGet_Click(object sender, EventArgs e)
        {

        }
    }
}
