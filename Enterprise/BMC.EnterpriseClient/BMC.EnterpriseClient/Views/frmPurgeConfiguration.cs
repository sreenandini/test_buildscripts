using BMC.EnterpriseBusiness.Business;
using BMC.EnterpriseBusiness.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BMC.EnterpriseClient.Views
{

    delegate void OnProces(object sender, EventArgs e);
    public partial class frmPurgeConfiguration : Form, IPurgeEditor
    {
        public event SaveItemsPurgeHandler SavePurgeItems;
        public event AddItemsHandler AddItem;
        public event LoadTableListHandler LoadTables;
        private PurgeViewPresenter presenter;

        List<PurgeCategory> categories = null;

        public frmPurgeConfiguration()
        {
            InitializeComponent();

            this.btnAddDBItem.Click += OnAddItem;
            this.btnAddLogItem.Click += OnAddItem;
            categories = new List<PurgeCategory>();
        }


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.presenter = new PurgeViewPresenter(this);

            LoadPurgetables(LoadTables());
            tvtables.Enabled = (tvDBPurge.Nodes.Count > 0 || tvLogPurge.Nodes.Count > 0) ? true : false;
        }

        /// <summary>
        /// load the table list
        /// </summary>

        private void LoadPurgetables(List<PurgeTables> lPList)
        {
            tvtables.Nodes.Add(new TreeNode("Tables"));
            TreeNode node = null;

            lPList.ForEach(item =>
            {
                node = new TreeNode(item.TableDisplayName);
                node.Tag = item.PurgeTableID;
                tvtables.Nodes[0].Nodes.Add(node);
            });
            tvtables.ExpandAll();
        }

        /// <summary>
        /// add item to tree view.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnAddItem(object sender, EventArgs e)
        {
            try
            {
                Button ctrl = sender as Button;
                PurgeCategory item = new PurgeCategory();
                if (ctrl.Name.ToUpper().Contains("DB"))
                {
                    item.PCCategoryName = txtDBItem.Text;
                    item.PCTypeID = 1;
                    item.PCIsActive = true;

                }
                else if (ctrl.Name.ToUpper().Contains("LOG"))
                {
                    item.PCCategoryName = txtLogItem.Text;
                    item.PCTypeID = 2;
                    item.PCIsActive = true;
                }

                categories = AddItem(item);
                AddPurgeItemstoTree(item);
            }
            catch (Exception ex)
            { }
        }



        /// <summary>
        /// save purge categories
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            SavePurgeItems(categories);
        }

        /// <summary>
        /// add items to tree.
        /// </summary>
        /// <param name="item"></param>
        private void AddPurgeItemstoTree(PurgeCategory item)
        {
            if (!string.IsNullOrEmpty(txtDBItem.Text))
            {
                if (tvDBPurge.Nodes.Count == 0)
                { tvDBPurge.Nodes.Add(new TreeNode("Root")); }

                bool IsExists = false;
                TreeNode node = new TreeNode(txtDBItem.Text);

                tvDBPurge.Nodes[0].Nodes.Cast<TreeNode>().TakeWhile(child => child.Text.SequenceEqual(node.Text)).ToList().ForEach(
                    child => IsExists = true);
                if (!IsExists)
                    tvDBPurge.Nodes[0].Nodes.Add(node);

                tvtables.Enabled = true;
            }

            if (!string.IsNullOrEmpty(txtLogItem.Text))
            {
                if (tvLogPurge.Nodes.Count == 0)
                { tvLogPurge.Nodes.Add(new TreeNode("Root")); }

                TreeNode node = new TreeNode(txtLogItem.Text);
                if (!tvLogPurge.Nodes[0].Nodes.Cast<TreeNode>().Contains(node))
                    tvLogPurge.Nodes[0].Nodes.Add(node);
            }

        }

    }
}
