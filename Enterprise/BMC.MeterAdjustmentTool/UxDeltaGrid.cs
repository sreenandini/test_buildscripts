using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.MeterAdjustmentTool.Exchange;
using BMC.Common.ExceptionManagement;
using BMC.MeterAdjustmentTool.Helpers;
using System.Collections;
using BMC.Common;
using System.Globalization;

namespace BMC.MeterAdjustmentTool
{
    public partial class UxDeltaGrid : UserControl
    {
        //private int _selectedRowIndex = -1;
        private DataRowView _selectedRow = null;
        private GridRowSelectedEventArgs _selRowArgs = null;
        private GridEditItemEventArgs _editArgs = null;
        public DataSet _dsRisky = null;
        public DataTable _dtOriginal = null;
        public DataTable _dtTranslated = null;
        public DataSet _dsDetails = null;

        private int installationNo, mgmdInstallationNo = 0;
        private DateTime readDateTime, hourDateTime, gamingDate, mgmdStartDatetime, mgmdEndDatetime = new DateTime();
        private string hour, assetNo = string.Empty;
        Hashtable dictRes;

        static string currencySymbol = CurrencyHelper.GetCurrencySymbol();

        public UxDeltaGrid()
        {
            InitializeComponent();
            _selRowArgs = new GridRowSelectedEventArgs();
            _editArgs = new GridEditItemEventArgs();
            this.EnableDisableEditButton();

            // Set Tags for controls
            SetTagProperty();

            // For externalization
            this.ResolveResources();

            dictRes = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
        }

        private void SetTagProperty()
        {
            btnEdit.Tag = "Key_EditCaption";
        }

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public string HeaderText
        {
            get
            {
                return uxHeader.Text;
            }
            set
            {
                uxHeader.Text = value;
            }
        }

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public bool IsEditable
        {
            get
            {
                return btnEdit.Visible;
            }
            set
            {
                btnEdit.Visible = value;
                tblContainer.RowStyles[1].Height = (value ? 35 : 0);
            }
        }

        [Browsable(false)]
        public ProcessEventArgs ProcessedArgs { get; set; }

        [Browsable(false)]
        public CreateDataInterfaceHandler CreateDataInterface { get; set; }

        [Browsable(false)]
        public DeltaGridLoadGridHandler LoadGridItems { get; set; }

        [Browsable(false)]
        public DeltaGridEditItemHandler EditItem { get; set; }

        [Browsable(false)]
        public DeltaGridUpdateItemHandler UpdateItem { get; set; }

        public event GridEditItemEventHandler EditClicked = null;

        private void OnEditClicked(GridEditItemEventArgs e)
        {
            if (this.EditClicked != null)
            {
                try
                {
                    this.EditClicked(this, e);
                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                    // this.ShowErrorMessageBox(ex.Message);
                }
            }
        }

        public event GridRowSelectedEventHandler RowSelected = null;

        private void OnRowSelected(GridRowSelectedEventArgs e)
        {
            if (this.RowSelected != null)
            {
                try
                {
                    this.RowSelected(this, e);
                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                    // this.ShowErrorMessageBox(ex.Message);
                }
            }
        }

        public event DeltaDetailsFormClosedHandler DeltaFormClosed = null;

        private void OnDeltaFormClosed(DeltaDetailsCloseEventArgs e)
        {
            if (this.DeltaFormClosed != null)
            {
                try
                {
                    this.DeltaFormClosed(this, e);
                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                }
            }
        }

        public void Clear()
        {
            this.ProcessedArgs = null;
            this.ClearDataSources();
        }

        private void ClearDataSources()
        {
            dgvData.DataSource = null;
            Extensions.DisposeObject(ref _dsRisky);
            Extensions.DisposeObject(ref _dsDetails);
            _dtOriginal = null;
            _dtTranslated = null;
        }

        public bool LoadGrid()
        {
            bool result = default(bool);
            try
            {
                this.ClearDataSources();

                if (this.LoadGridItems != null)
                {
                    using (IDataInterface data = this.CreateDataInterface())
                    {
                        IQueryExecutor<DataSet> sqlExec = this.LoadGridItems(this.ProcessedArgs);
                        _dsRisky = data.SelectQuery(sqlExec);
                        if (_dsRisky != null)
                        {
                            if (_dsRisky.Tables.Contains(MeterGlobals.ORIGINAL_TABLE))
                            {
                                _dtOriginal = _dsRisky.Tables[MeterGlobals.ORIGINAL_TABLE];

                                // Translate DataSet is called here for Externalization implementaion
                                _dsRisky = TranslateDataSetForRead(_dsRisky, data.GetType());
                            }
                            if (_dsRisky.Tables.Contains(MeterGlobals.TRANSLATED_TABLE))
                            {
                                _dtTranslated = _dsRisky.Tables[MeterGlobals.TRANSLATED_TABLE];
                            }

                            if (_dtOriginal == null)
                            {
                                _dtOriginal = _dtTranslated;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                try
                {
                    dgvData.DataSource = (_dtTranslated != null ? _dtTranslated : _dtOriginal);
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                    Win32Extensions.ShowErrorMessageBox(this, ex.Message);
                }
            }

            return result;
        }

        private DataSet TranslateDataSetForRead(DataSet ds, Type typeOfData)
        {
            try
            {
                DataTable dtTobeBounded = null;

                IEnumerator enumDict = ds.Tables[0].Columns.GetEnumerator();
                dtTobeBounded = ds.Tables[0].Copy();

                while (enumDict.MoveNext())
                {
                    DataColumn dc = (DataColumn)enumDict.Current;
                    string newColnName = string.Empty;
                    if (typeOfData == typeof(CollectionDetails))
                    {
                        switch (dc.ColumnName)
                        {
                            case "Bar_pos_Name": newColnName = GetNewColumnHeader("Key_CollSch_Bar_pos_Name"); break;
                            case "CASH_IN_100000P": newColnName = GetNewColumnHeader("Key_CollSch_CASH_IN_100000P"); break;
                            case "CASH_IN_10000P": newColnName = GetNewColumnHeader("Key_CollSch_CASH_IN_10000P"); break;
                            case "CASH_IN_1000P": newColnName = GetNewColumnHeader("Key_CollSch_CASH_IN_1000P"); break;
                            case "CASH_IN_100P": newColnName = GetNewColumnHeader("Key_CollSch_CASH_IN_100P"); break;
                            case "CASH_IN_10P": newColnName = GetNewColumnHeader("Key_CollSch_CASH_IN_10P"); break;
                            case "CASH_IN_1P": newColnName = GetNewColumnHeader("Key_CollSch_CASH_IN_1P"); break;
                            case "CASH_IN_20000P": newColnName = GetNewColumnHeader("Key_CollSch_CASH_IN_20000P"); break;
                            case "CASH_IN_2000P": newColnName = GetNewColumnHeader("Key_CollSch_CASH_IN_2000P"); break;
                            case "CASH_IN_200P": newColnName = GetNewColumnHeader("Key_CollSch_CASH_IN_200P"); break;
                            case "CASH_IN_20P": newColnName = GetNewColumnHeader("Key_CollSch_CASH_IN_20P"); break;
                            case "CASH_IN_2P": newColnName = GetNewColumnHeader("Key_CollSch_CASH_IN_2P"); break;
                            case "CASH_IN_50000P": newColnName = GetNewColumnHeader("Key_CollSch_CASH_IN_50000P"); break;
                            case "CASH_IN_5000P": newColnName = GetNewColumnHeader("Key_CollSch_CASH_IN_5000P"); break;
                            case "CASH_IN_500P": newColnName = GetNewColumnHeader("Key_CollSch_CASH_IN_500P"); break;
                            case "CASH_IN_50P": newColnName = GetNewColumnHeader("Key_CollSch_CASH_IN_50P"); break;
                            case "CASH_IN_5P": newColnName = GetNewColumnHeader("Key_CollSch_CASH_IN_5P"); break;
                            case "Cashable_EFT_IN": newColnName = GetNewColumnHeader("Key_CollSch_Cashable_EFT_IN"); break;
                            case "Cashable_EFT_OUT": newColnName = GetNewColumnHeader("Key_CollSch_Cashable_EFT_OUT"); break;
                            case "Coin_Var": newColnName = GetNewColumnHeader("Key_CollSch_Coin_Var"); break;
                            case "Collection_Date": newColnName = GetNewColumnHeader("Key_CollSch_Collection_Date"); break;
                            case "COLLECTION_RDC_CANCELLED_CREDITS": newColnName = GetNewColumnHeader("Key_CollSch_COLLECTION_RDC_CANCELLED_CREDITS"); break;
                            case "COLLECTION_RDC_COIN_DROP": newColnName = GetNewColumnHeader("Key_CollSch_COLLECTION_RDC_COIN_DROP"); break;
                            case "COLLECTION_RDC_COINS_IN": newColnName = GetNewColumnHeader("Key_CollSch_COLLECTION_RDC_COINS_IN"); break;
                            case "COLLECTION_RDC_COINS_OUT": newColnName = GetNewColumnHeader("Key_CollSch_COLLECTION_RDC_COINS_OUT"); break;
                            case "COLLECTION_RDC_CURRENT_CREDITS": newColnName = GetNewColumnHeader("Key_CollSch_COLLECTION_RDC_CURRENT_CREDITS"); break;
                            case "COLLECTION_RDC_GAMES_BET": newColnName = GetNewColumnHeader("Key_CollSch_COLLECTION_RDC_GAMES_BET"); break;
                            case "COLLECTION_RDC_GAMES_LOST": newColnName = GetNewColumnHeader("Key_CollSch_COLLECTION_RDC_GAMES_LOST"); break;
                            case "COLLECTION_RDC_GAMES_WON": newColnName = GetNewColumnHeader("Key_CollSch_COLLECTION_RDC_GAMES_WON"); break;
                            case "COLLECTION_RDC_HANDPAY": newColnName = GetNewColumnHeader("Key_CollSch_COLLECTION_RDC_HANDPAY"); break;
                            case "COLLECTION_RDC_JACKPOT": newColnName = GetNewColumnHeader("Key_CollSch_COLLECTION_RDC_JACKPOT"); break;
                            case "COLLECTION_RDC_TICKETS_INSERTED_VALUE": newColnName = GetNewColumnHeader("Key_CollSch_COLLECTION_RDC_TICKETS_INSERTED_VALUE"); break;
                            case "COLLECTION_RDC_TICKETS_PRINTED_VALUE": newColnName = GetNewColumnHeader("Key_CollSch_COLLECTION_RDC_TICKETS_PRINTED_VALUE"); break;
                            case "COLLECTION_RDC_TRUE_COIN_IN": newColnName = GetNewColumnHeader("Key_CollSch_COLLECTION_RDC_TRUE_COIN_IN"); break;
                            case "COLLECTION_RDC_TRUE_COIN_OUT": newColnName = GetNewColumnHeader("Key_CollSch_COLLECTION_RDC_TRUE_COIN_OUT"); break;
                            case "HandpayVar": newColnName = GetNewColumnHeader("Key_CollSch_HandpayVar"); break;
                            case "MeterVar": newColnName = GetNewColumnHeader("Key_CollSch_MeterVar"); break;
                            case "Mystery_Attendant_Paid": newColnName = GetNewColumnHeader("Key_CollSch_Mystery_Attendant_Paid"); break;
                            case "Mystery_Machine_Paid": newColnName = GetNewColumnHeader("Key_CollSch_Mystery_Machine_Paid"); break;
                            case "Name": newColnName = GetNewColumnHeader("Key_CollSch_Name"); break;
                            case "NonCashable_EFT_IN": newColnName = GetNewColumnHeader("Key_CollSch_NonCashable_EFT_IN"); break;
                            case "NonCashable_EFT_OUT": newColnName = GetNewColumnHeader("Key_CollSch_NonCashable_EFT_OUT"); break;
                            case "Note_Var": newColnName = GetNewColumnHeader("Key_CollSch_Note_Var"); break;
                            case "PreviousCollectionDate": newColnName = GetNewColumnHeader("Key_CollSch_PreviousCollectionDate"); break;
                            case "Progressive_Value_Variance": newColnName = GetNewColumnHeader("Key_CollSch_Progressive_Value_Variance"); break;
                            case "progressive_win_Handpay_value": newColnName = GetNewColumnHeader("Key_CollSch_progressive_win_Handpay_value"); break;
                            case "progressive_win_value": newColnName = GetNewColumnHeader("Key_CollSch_progressive_win_value"); break;
                            case "Promo_Cashable_EFT_IN": newColnName = GetNewColumnHeader("Key_CollSch_Promo_Cashable_EFT_IN"); break;
                            case "Promo_Cashable_EFT_OUT": newColnName = GetNewColumnHeader("Key_CollSch_Promo_Cashable_EFT_OUT"); break;
                            case "RDC_TICKETS_INSERTED_NONCASHABLE_VALUE": newColnName = GetNewColumnHeader("Key_CollSch_RDC_TICKETS_INSERTED_NONCASHABLE_VALUE"); break;
                            case "RDC_TICKETS_PRINTED_NONCASHABLE_VALUE": newColnName = GetNewColumnHeader("Key_CollSch_RDC_TICKETS_PRINTED_NONCASHABLE_VALUE"); break;
                            case "RDCVar": newColnName = GetNewColumnHeader("Key_CollSch_RDCVar"); break;
                            case "Stock_no": newColnName = GetNewColumnHeader("Key_CollSch_Stock_no"); break;
                            case "TICKETS_INSERTED_NONCASHABLE_QTY": newColnName = GetNewColumnHeader("Key_CollSch_TICKETS_INSERTED_NONCASHABLE_QTY"); break;
                            case "TICKETS_INSERTED_QTY": newColnName = GetNewColumnHeader("Key_CollSch_TICKETS_INSERTED_QTY"); break;
                            case "TicketVar": newColnName = GetNewColumnHeader("Key_CollSch_TicketVar"); break;

                            default: newColnName = string.Empty; break;
                        }
                    }
                    else if (typeOfData == typeof(DailyRead))
                    {
                        switch (dc.ColumnName)
                        {
                            case "Bar_Pos_Name": newColnName = GetNewColumnHeader("Key_DailyRdSch_Bar_Pos_Name"); break;
                            case "Cashable_EFT_IN": newColnName = GetNewColumnHeader("Key_DailyRdSch_Cashable_EFT_IN"); break;
                            case "Cashable_EFT_OUT": newColnName = GetNewColumnHeader("Key_DailyRdSch_Cashable_EFT_OUT"); break;
                            case "CollectedDateTime": newColnName = GetNewColumnHeader("Key_DailyRdSch_CollectedDateTime"); break;
                            case "Mystery_Attendant_Paid": newColnName = GetNewColumnHeader("Key_DailyRdSch_Mystery_Attendant_Paid"); break;
                            case "Mystery_Machine_Paid": newColnName = GetNewColumnHeader("Key_DailyRdSch_Mystery_Machine_Paid"); break;
                            case "NonCashable_EFT_IN": newColnName = GetNewColumnHeader("Key_DailyRdSch_NonCashable_EFT_IN"); break;
                            case "NonCashable_EFT_OUT": newColnName = GetNewColumnHeader("Key_DailyRdSch_NonCashable_EFT_OUT"); break;
                            case "progressive_win_Handpay_value": newColnName = GetNewColumnHeader("Key_DailyRdSch_progressive_win_Handpay_value"); break;
                            case "progressive_win_value": newColnName = GetNewColumnHeader("Key_DailyRdSch_progressive_win_value"); break;
                            case "Promo_Cashable_EFT_IN": newColnName = GetNewColumnHeader("Key_DailyRdSch_Promo_Cashable_EFT_IN"); break;
                            case "Promo_Cashable_EFT_OUT": newColnName = GetNewColumnHeader("Key_DailyRdSch_Promo_Cashable_EFT_OUT"); break;
                            case "READ_COIN_DROP": newColnName = GetNewColumnHeader("Key_DailyRdSch_READ_COIN_DROP"); break;
                            case "READ_COINS_IN": newColnName = GetNewColumnHeader("Key_DailyRdSch_READ_COINS_IN"); break;
                            case "READ_COINS_OUT": newColnName = GetNewColumnHeader("Key_DailyRdSch_READ_COINS_OUT"); break;
                            case "READ_DATE": newColnName = GetNewColumnHeader("Key_DailyRdSch_READ_DATE"); break;
                            case "READ_GAMES_BET": newColnName = GetNewColumnHeader("Key_DailyRdSch_READ_GAMES_BET"); break;
                            case "READ_GAMES_WON": newColnName = GetNewColumnHeader("Key_DailyRdSch_READ_GAMES_WON"); break;
                            case "READ_HANDPAY": newColnName = GetNewColumnHeader("Key_DailyRdSch_READ_HANDPAY"); break;
                            case "READ_RDC_BILL_1": newColnName = GetNewColumnHeader("Key_DailyRdSch_READ_RDC_BILL_1"); break;
                            case "READ_RDC_BILL_10": newColnName = GetNewColumnHeader("Key_DailyRdSch_READ_RDC_BILL_10"); break;
                            case "READ_RDC_BILL_100": newColnName = GetNewColumnHeader("Key_DailyRdSch_READ_RDC_BILL_100"); break;
                            case "READ_RDC_BILL_10000": newColnName = GetNewColumnHeader("Key_DailyRdSch_READ_RDC_BILL_10000"); break;
                            case "READ_RDC_BILL_100000": newColnName = GetNewColumnHeader("Key_DailyRdSch_READ_RDC_BILL_100000"); break;
                            case "READ_RDC_BILL_2": newColnName = GetNewColumnHeader("Key_DailyRdSch_READ_RDC_BILL_2"); break;
                            case "READ_RDC_BILL_20": newColnName = GetNewColumnHeader("Key_DailyRdSch_READ_RDC_BILL_20"); break;
                            case "READ_RDC_BILL_200": newColnName = GetNewColumnHeader("Key_DailyRdSch_READ_RDC_BILL_200"); break;
                            case "READ_RDC_BILL_20000": newColnName = GetNewColumnHeader("Key_DailyRdSch_READ_RDC_BILL_20000"); break;
                            case "READ_RDC_BILL_250": newColnName = GetNewColumnHeader("Key_DailyRdSch_READ_RDC_BILL_250"); break;
                            case "READ_RDC_BILL_5": newColnName = GetNewColumnHeader("Key_DailyRdSch_READ_RDC_BILL_5"); break;
                            case "READ_RDC_BILL_50": newColnName = GetNewColumnHeader("Key_DailyRdSch_READ_RDC_BILL_50"); break;
                            case "READ_RDC_BILL_500": newColnName = GetNewColumnHeader("Key_DailyRdSch_READ_RDC_BILL_500"); break;
                            case "READ_RDC_BILL_50000": newColnName = GetNewColumnHeader("Key_DailyRdSch_READ_RDC_BILL_50000"); break;
                            case "READ_RDC_CANCELLED_CREDITS": newColnName = GetNewColumnHeader("Key_DailyRdSch_READ_RDC_CANCELLED_CREDITS"); break;
                            case "READ_RDC_CURRENT_CREDITS": newColnName = GetNewColumnHeader("Key_DailyRdSch_READ_RDC_CURRENT_CREDITS"); break;
                            case "READ_RDC_GAMES_LOST": newColnName = GetNewColumnHeader("Key_DailyRdSch_READ_RDC_GAMES_LOST"); break;
                            case "READ_RDC_JACKPOT": newColnName = GetNewColumnHeader("Key_DailyRdSch_READ_RDC_JACKPOT"); break;
                            case "READ_RDC_TRUE_COIN_IN": newColnName = GetNewColumnHeader("Key_DailyRdSch_READ_RDC_TRUE_COIN_IN"); break;
                            case "READ_RDC_TRUE_COIN_OUT": newColnName = GetNewColumnHeader("Key_DailyRdSch_READ_RDC_TRUE_COIN_OUT"); break;
                            case "READ_TICKET": newColnName = GetNewColumnHeader("Key_DailyRdSch_READ_TICKET"); break;
                            case "READ_TICKET_IN_SUSPENSE": newColnName = GetNewColumnHeader("Key_DailyRdSch_READ_TICKET_IN_SUSPENSE"); break;
                            case "READ_TICKET_IN_SUSPENSE_VALUE": newColnName = GetNewColumnHeader("Key_DailyRdSch_READ_TICKET_IN_SUSPENSE_VALUE"); break;
                            case "READ_TICKET_VALUE": newColnName = GetNewColumnHeader("Key_DailyRdSch_READ_TICKET_VALUE"); break;
                            case "TICKETS_INSERTED_NONCASHABLE_VALUE": newColnName = GetNewColumnHeader("Key_DailyRdSch_TICKETS_INSERTED_NONCASHABLE_VALUE"); break;
                            case "TICKETS_PRINTED_NONCASHABLE_VALUE": newColnName = GetNewColumnHeader("Key_DailyRdSch_TICKETS_PRINTED_NONCASHABLE_VALUE"); break;

                            default: newColnName = string.Empty; break;
                        }
                    }
                    else if (typeOfData == typeof(MGMDDelta))
                    {
                        switch (dc.ColumnName)
                        {
                            case "Bar_Pos_Name": newColnName = GetNewColumnHeader("Key_MGMD_Bar_Pos_Name"); break;
                            case "MGMD_BILL_1": newColnName = GetNewColumnHeader("Key_MGMD_BILL_1"); break;
                            case "MGMD_BILL_10": newColnName = GetNewColumnHeader("Key_MGMD_BILL_10"); break;
                            case "MGMD_BILL_100": newColnName = GetNewColumnHeader("Key_MGMD_BILL_100"); break;
                            case "MGMD_BILL_20": newColnName = GetNewColumnHeader("Key_MGMD_BILL_20"); break;
                            case "MGMD_BILL_200": newColnName = GetNewColumnHeader("Key_MGMD_BILL_200"); break;
                            case "MGMD_BILL_5": newColnName = GetNewColumnHeader("Key_MGMD_BILL_5"); break;
                            case "MGMD_BILL_50": newColnName = GetNewColumnHeader("Key_MGMD_BILL_50"); break;
                            case "MGMD_BILL_500": newColnName = GetNewColumnHeader("Key_MGMD_BILL_500"); break;
                            case "MGMD_CANCELLED_CREDITS": newColnName = GetNewColumnHeader("Key_MGMD_CANCELLED_CREDITS"); break;
                            case "MGMD_Cashable_EFT_IN": newColnName = GetNewColumnHeader("Key_MGMD_Cashable_EFT_IN"); break;
                            case "MGMD_Cashable_EFT_OUT": newColnName = GetNewColumnHeader("Key_MGMD_Cashable_EFT_OUT"); break;
                            case "MGMD_COIN_DROP": newColnName = GetNewColumnHeader("Key_MGMD_COIN_DROP"); break;
                            case "MGMD_COINS_IN": newColnName = GetNewColumnHeader("Key_MGMD_COINS_IN"); break;
                            case "MGMD_COINS_OUT": newColnName = GetNewColumnHeader("Key_MGMD_COINS_OUT"); break;
                            case "MGMD_CURRENT_CREDITS": newColnName = GetNewColumnHeader("Key_MGMD_CURRENT_CREDITS"); break;
                            case "MGMD_End_DateTime": newColnName = GetNewColumnHeader("Key_MGMD_End_DateTime"); break;
                            case "MGMD_GAMES_BET": newColnName = GetNewColumnHeader("Key_MGMD_GAMES_BET"); break;
                            case "MGMD_GAMES_LOST": newColnName = GetNewColumnHeader("Key_MGMD_GAMES_LOST"); break;
                            case "MGMD_GAMES_WON": newColnName = GetNewColumnHeader("Key_MGMD_GAMES_WON"); break;
                            case "MGMD_HANDPAY": newColnName = GetNewColumnHeader("Key_MGMD_HANDPAY"); break;
                            case "MGMD_Installation_No": newColnName = GetNewColumnHeader("Key_MGMD_Installation_No"); break;
                            case "MGMD_JACKPOT": newColnName = GetNewColumnHeader("Key_MGMD_JACKPOT"); break;
                            case "MGMD_Mystery_Attendant_Paid": newColnName = GetNewColumnHeader("Key_MGMD_Mystery_Attendant_Paid"); break;
                            case "MGMD_Mystery_Machine_Paid": newColnName = GetNewColumnHeader("Key_MGMD_Mystery_Machine_Paid"); break;
                            case "MGMD_NonCashable_EFT_IN": newColnName = GetNewColumnHeader("Key_MGMD_NonCashable_EFT_IN"); break;
                            case "MGMD_NonCashable_EFT_OUT": newColnName = GetNewColumnHeader("Key_MGMD_NonCashable_EFT_OUT"); break;
                            case "MGMD_progressive_win_Handpay_value": newColnName = GetNewColumnHeader("Key_MGMD_progressive_win_Handpay_value"); break;
                            case "MGMD_progressive_win_value": newColnName = GetNewColumnHeader("Key_MGMD_progressive_win_value"); break;
                            case "MGMD_Promo_Cashable_EFT_IN": newColnName = GetNewColumnHeader("Key_MGMD_Promo_Cashable_EFT_IN"); break;
                            case "MGMD_Promo_Cashable_EFT_OUT": newColnName = GetNewColumnHeader("Key_MGMD_Promo_Cashable_EFT_OUT"); break;
                            case "MGMD_Start_DateTime": newColnName = GetNewColumnHeader("Key_MGMD_Start_DateTime"); break;
                            case "MGMD_TICKET_INSERTED_QTY": newColnName = GetNewColumnHeader("Key_MGMD_TICKET_INSERTED_QTY"); break;
                            case "MGMD_TICKET_INSERTED_VALUE": newColnName = GetNewColumnHeader("Key_MGMD_TICKET_INSERTED_VALUE"); break;
                            case "MGMD_TICKET_PRINTED_QTY": newColnName = GetNewColumnHeader("Key_MGMD_TICKET_PRINTED_QTY"); break;
                            case "MGMD_TICKET_PRINTED_VALUE": newColnName = GetNewColumnHeader("Key_MGMD_TICKET_PRINTED_VALUE"); break;
                            case "MGMD_TICKETS_INSERTED_NONCASHABLE_QTY": newColnName = GetNewColumnHeader("Key_MGMD_TICKETS_INSERTED_NONCASHABLE_QTY"); break;
                            case "MGMD_TICKETS_INSERTED_NONCASHABLE_VALUE": newColnName = GetNewColumnHeader("Key_MGMD_TICKETS_INSERTED_NONCASHABLE_VALUE"); break;
                            case "MGMD_TICKETS_PRINTED_NONCASHABLE_QTY": newColnName = GetNewColumnHeader("Key_MGMD_TICKETS_PRINTED_NONCASHABLE_QTY"); break;
                            case "MGMD_TICKETS_PRINTED_NONCASHABLE_VALUE": newColnName = GetNewColumnHeader("Key_MGMD_TICKETS_PRINTED_NONCASHABLE_VALUE"); break;
                            case "MGMD_TRUE_COIN_IN": newColnName = GetNewColumnHeader("Key_MGMD_TRUE_COIN_IN"); break;
                            case "MGMD_TRUE_COIN_OUT": newColnName = GetNewColumnHeader("Key_MGMD_TRUE_COIN_OUT"); break;
                            case "Read_Date": newColnName = GetNewColumnHeader("Key_MGMD_Read_Date"); break;

                            default: newColnName = string.Empty; break;
                        }
                    }
                    else if (typeOfData == typeof(HourlyVTP))
                    {
                        switch (dc.ColumnName)
                        {
                            case "AVG_BET": newColnName = GetNewColumnHeader("Key_HrlyRdSch_AVG_BET"); break;
                            case "Bar_Pos_Name": newColnName = GetNewColumnHeader("Key_HrlyRdSch_Bar_Pos_Name"); break;
                            case "CANCELLED_CREDITS": newColnName = GetNewColumnHeader("Key_HrlyRdSch_CANCELLED_CREDITS"); break;
                            case "CASHABLE_EFT_IN": newColnName = GetNewColumnHeader("Key_HrlyRdSch_CASHABLE_EFT_IN"); break;
                            case "CASHABLE_EFT_OUT": newColnName = GetNewColumnHeader("Key_HrlyRdSch_CASHABLE_EFT_OUT"); break;
                            case "CASINO_WIN": newColnName = GetNewColumnHeader("Key_HrlyRdSch_CASINO_WIN"); break;
                            case "CREDITS_WAGERED": newColnName = GetNewColumnHeader("Key_HrlyRdSch_CREDITS_WAGERED"); break;
                            case "CREDITS_WON": newColnName = GetNewColumnHeader("Key_HrlyRdSch_CREDITS_WON"); break;
                            case "DROP": newColnName = GetNewColumnHeader("Key_HrlyRdSch_DROP"); break;
                            case "GAMES_BET": newColnName = GetNewColumnHeader("Key_HrlyRdSch_GAMES_BET"); break;
                            case "GAMES_LOST": newColnName = GetNewColumnHeader("Key_HrlyRdSch_GAMES_LOST"); break;
                            case "GAMES_WON": newColnName = GetNewColumnHeader("Key_HrlyRdSch_GAMES_WON"); break;
                            case "HANDPAY": newColnName = GetNewColumnHeader("Key_HrlyRdSch_HANDPAY"); break;
                            case "HS_Date": newColnName = GetNewColumnHeader("Key_HrlyRdSch_HS_Date"); break;
                            case "HS_Hour": newColnName = GetNewColumnHeader("Key_HrlyRdSch_HS_Hour"); break;
                            case "JACKPOT": newColnName = GetNewColumnHeader("Key_HrlySch_JACKPOT"); break;
                            case "MYSTERY_ATTENDANT_PAID": newColnName = GetNewColumnHeader("Key_HrlySch_MYSTERY_ATTENDANT_PAID"); break;
                            case "MYSTERY_MACHINE_PAID": newColnName = GetNewColumnHeader("Key_HrlySch_MYSTERY_MACHINE_PAID"); break;
                            case "NON_CASHABLE_EFT_IN": newColnName = GetNewColumnHeader("Key_HrlySch_NON_CASHABLE_EFT_IN"); break;
                            case "NON_CASHABLE_EFT_OUT": newColnName = GetNewColumnHeader("Key_HrlySch_NON_CASHABLE_EFT_OUT"); break;
                            case "NON_CASHABLE_VOUCHERS_IN_QTY": newColnName = GetNewColumnHeader("Key_HrlySch_NON_CASHABLE_VOUCHERS_IN_QTY"); break;
                            case "NON_CASHABLE_VOUCHERS_IN_VALUE": newColnName = GetNewColumnHeader("Key_HrlySch_NON_CASHABLE_VOUCHERS_IN_VALUE"); break;
                            case "NON_CASHABLE_VOUCHERS_OUT_QTY": newColnName = GetNewColumnHeader("Key_HrlySch_NON_CASHABLE_VOUCHERS_OUT_QTY"); break;
                            case "NON_CASHABLE_VOUCHERS_OUT_VALUE": newColnName = GetNewColumnHeader("Key_HrlySch_NON_CASHABLE_VOUCHERS_OUT_VALUE"); break;
                            case "OCCUPANCY": newColnName = GetNewColumnHeader("Key_HrlySch_OCCUPANCY"); break;
                            case "PROGRESSIVE_HANDPAY": newColnName = GetNewColumnHeader("Key_HrlySch_PROGRESSIVE_HANDPAY"); break;
                            case "PROGRESSIVE_WIN": newColnName = GetNewColumnHeader("Key_HrlySch_PROGRESSIVE_WIN"); break;
                            case "PROMO_CASHABLE_EFT_IN": newColnName = GetNewColumnHeader("Key_HrlySch_PROMO_CASHABLE_EFT_IN"); break;
                            case "PROMO_CASHABLE_EFT_OUT": newColnName = GetNewColumnHeader("Key_HrlySch_PROMO_CASHABLE_EFT_OUT"); break;
                            case "TICKETS_INSERTED_QTY": newColnName = GetNewColumnHeader("Key_HrlySch_TICKETS_INSERTED_QTY"); break;
                            case "TICKETS_INSERTED_VALUE": newColnName = GetNewColumnHeader("Key_HrlySch_TICKETS_INSERTED_VALUE"); break;
                            case "TICKETS_PRINTED_QTY": newColnName = GetNewColumnHeader("Key_HrlySch_TICKETS_PRINTED_QTY"); break;
                            case "TICKETS_PRINTED_VALUE": newColnName = GetNewColumnHeader("Key_HrlySch_TICKETS_PRINTED_VALUE"); break;
                            //case "Type":                     newColnName = GetNewColumnHeader("Key_HrlySch_Type"); break;
                            case "Type": newColnName = "Column"; break;    // using this column name as it is used in lot of places and is not visible

                            default: newColnName = string.Empty; break;
                        }
                    }

                    if (newColnName == string.Empty)
                        dtTobeBounded.Columns.Remove(dc.ColumnName);
                    else
                    {
                        // Add entry to dictionary
                        if (!dictRes.ContainsKey(dc.ColumnName))
                            dictRes.Add(dc.ColumnName, newColnName);
                        dtTobeBounded.Columns[dc.ColumnName].ColumnName = newColnName;
                        for (int i = 0; i < dtTobeBounded.Rows.Count; i++)
                        {
                            dtTobeBounded.Rows[i][newColnName] = (string.IsNullOrEmpty(dtTobeBounded.Rows[i][newColnName].ToString())) ? "0" : dtTobeBounded.Rows[i][newColnName].ToString();
                        }
                    }
                }
                dtTobeBounded.TableName = MeterGlobals.TRANSLATED_TABLE;
                ds.Tables.Add(dtTobeBounded.Copy());

                if (typeOfData == typeof(HourlyVTP))
                    ds = TranslateDataSetHourly(ds);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return ds;
        }

        private DataSet TranslateDataSetForEdit(DataSet ds)
        {
            try
            {
                ds.Tables[0].Columns.Add(new DataColumn("Meter"));
                ds.Tables[0].Columns["Meter"].SetOrdinal(0);
                IEnumerator enumDict = ds.Tables[0].Rows.GetEnumerator();
                while (enumDict.MoveNext())
                {
                    if (dictRes.ContainsKey(((DataRow)enumDict.Current)["Column"].ToString().Trim().ToUpper()))
                    {
                        ((DataRow)enumDict.Current)["Meter"] = dictRes[((DataRow)enumDict.Current)["Column"].ToString().ToUpper().Trim()];
                    }
                    else
                    {
                        string meterName = ((DataRow)enumDict.Current)["Column"].ToString();
                        // If column in edit screen is not displayed in main grid, add cases here
                        if (meterName == "BILLS_IN")
                            ((DataRow)enumDict.Current)["Meter"] = this.GetResourceTextByKey("Key_HrlySch_BILLS_IN");
                        else
                            ((DataRow)enumDict.Current)["Meter"] = meterName.ToUpper();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return ds;
        }

        private DataSet TranslateDataSetHourly(DataSet ds)
        {
            try
            {
                string hourColumn = this.GetResourceTextByKey("Key_Hour");                // "Hour";
                string typeColumn = this.GetResourceTextByKey("Key_HrlySch_Type");

                ds.Tables[MeterGlobals.TRANSLATED_TABLE].Columns.Add(new DataColumn(hourColumn));
                ds.Tables[MeterGlobals.TRANSLATED_TABLE].Columns[hourColumn].SetOrdinal(2);
                IEnumerator enumDict = ds.Tables[1].Rows.GetEnumerator();
                while (enumDict.MoveNext())
                {
                    string hrData = ((DataRow)enumDict.Current)["Column"].ToString().Trim();
                    string hrValue = string.Empty;

                    switch (hrData)
                    {
                        case "HS_Hour1": hrValue = GetNewColumnHeader("Key_HrlyRdSch_HS_Hour1"); break;
                        case "HS_Hour10": hrValue = GetNewColumnHeader("Key_HrlyRdSch_HS_Hour10"); break;
                        case "HS_Hour11": hrValue = GetNewColumnHeader("Key_HrlyRdSch_HS_Hour11"); break;
                        case "HS_Hour12": hrValue = GetNewColumnHeader("Key_HrlySch_HS_Hour12"); break;
                        case "HS_Hour13": hrValue = GetNewColumnHeader("Key_HrlySch_HS_Hour13"); break;
                        case "HS_Hour14": hrValue = GetNewColumnHeader("Key_HrlySch_HS_Hour14"); break;
                        case "HS_Hour15": hrValue = GetNewColumnHeader("Key_HrlySch_HS_Hour15"); break;
                        case "HS_Hour16": hrValue = GetNewColumnHeader("Key_HrlySch_HS_Hour16"); break;
                        case "HS_Hour17": hrValue = GetNewColumnHeader("Key_HrlySch_HS_Hour17"); break;
                        case "HS_Hour18": hrValue = GetNewColumnHeader("Key_HrlySch_HS_Hour18"); break;
                        case "HS_Hour19": hrValue = GetNewColumnHeader("Key_HrlySch_HS_Hour19"); break;
                        case "HS_Hour2": hrValue = GetNewColumnHeader("Key_HrlySch_HS_Hour2"); break;
                        case "HS_Hour20": hrValue = GetNewColumnHeader("Key_HrlySch_HS_Hour20"); break;
                        case "HS_Hour21": hrValue = GetNewColumnHeader("Key_HrlySch_HS_Hour21"); break;
                        case "HS_Hour22": hrValue = GetNewColumnHeader("Key_HrlySch_HS_Hour22"); break;
                        case "HS_Hour23": hrValue = GetNewColumnHeader("Key_HrlySch_HS_Hour23"); break;
                        case "HS_Hour24": hrValue = GetNewColumnHeader("Key_HrlySch_HS_Hour24"); break;
                        case "HS_Hour3": hrValue = GetNewColumnHeader("Key_HrlySch_HS_Hour3"); break;
                        case "HS_Hour4": hrValue = GetNewColumnHeader("Key_HrlySch_HS_Hour4"); break;
                        case "HS_Hour5": hrValue = GetNewColumnHeader("Key_HrlySch_HS_Hour5"); break;
                        case "HS_Hour6": hrValue = GetNewColumnHeader("Key_HrlySch_HS_Hour6"); break;
                        case "HS_Hour7": hrValue = GetNewColumnHeader("Key_HrlySch_HS_Hour7"); break;
                        case "HS_Hour8": hrValue = GetNewColumnHeader("Key_HrlySch_HS_Hour8"); break;
                        case "HS_Hour9": hrValue = GetNewColumnHeader("Key_HrlySch_HS_Hour9"); break;
                    }

                    if (hrValue != string.Empty)
                    {
                        ((DataRow)enumDict.Current)[hourColumn] = hrValue;
                    }
                    else
                    {
                        ((DataRow)enumDict.Current)[hourColumn] = hrData;
                    }
                }
                ds.Tables[MeterGlobals.TRANSLATED_TABLE].Columns.Remove("Column");
                return ds;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return ds;
        }

        private string GetNewColumnHeader(string hdrKey)
        {
            string hdrNewKey = this.GetResourceTextByKey(hdrKey);
            if (hdrNewKey.Contains("#"))
            {
                hdrNewKey = hdrNewKey.Replace("#", currencySymbol);
            }
            return hdrNewKey;
        }

        public void SetBatchDetailsHeader()
        {
            // Get Header Texts from Resource
            foreach (DataGridViewColumn dc in dgvData.Columns)
            {
                switch (dc.Name.ToUpper())
                {
                    case "SITE BATCH NO":
                        dc.HeaderText = this.GetResourceTextByKey("Key_SiteBatchNo"); break;
                    case "ROUTE":
                        dc.HeaderText = this.GetResourceTextByKey("Key_Route"); break;
                    case "GAMING DATE":
                        dc.HeaderText = this.GetResourceTextByKey("Key_GamingDate"); break;
                    case "DATE COLLECTED":
                        dc.HeaderText = this.GetResourceTextByKey("Key_DateCollected"); break;
                    case "USER_NO":
                        dc.HeaderText = this.GetResourceTextByKey("Key_UserNo"); break;
                    case "USER NAME":
                        dc.HeaderText = this.GetResourceTextByKey("Key_UserNameHdr"); break;
                }
            }
        }

        public String CreateUpdateSets(Hashtable htChanges, int flag)
        {
            StringBuilder sbUpdateSet = new StringBuilder();
            bool initialized = false;

            if (flag == 1)
            {
                foreach (String col in htChanges.Keys)
                {
                    sbUpdateSet.Append((initialized ? "," : "") + col + "=" + htChanges[col]);
                    if (!initialized) initialized = true;
                }
            }
            else
            {
                foreach (String col in htChanges.Keys)
                {
                    sbUpdateSet.Append((initialized ? "," : "") +
                        _dsDetails.Tables[0]
                        .Select("Column='" + col.ToString() + "'")[0]["Meter"].ToString() + "=" + htChanges[col]);
                    if (!initialized) initialized = true;
                }

            }
            return sbUpdateSet.ToString();
        }

        private void dgvData_SelectionChanged(object sender, EventArgs e)
        {
            this.EnsureRowSelected();
        }

        private bool EnsureRowSelected()
        {
            return this.EnsureRowSelected(false);
        }

        private bool EnsureRowSelected(bool fireOnSelect)
        {
            try
            {
                //int rowIndex = -1;
                //bool isAlreadySelected = false;
                DataRowView selectedRow = null;
                DataRow drOriginal = null;
                DataRow drTranslated = null;

                if (dgvData.SelectedRows.Count > 0)
                {
                    selectedRow = dgvData.SelectedRows[0].DataBoundItem as DataRowView;
                }

                if (selectedRow != null)
                {
                    if (selectedRow != _selectedRow)
                    {
                        DataRow row = selectedRow.Row;
                        int rowId = Convert.ToInt32(row.GetType().GetField("_rowID", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(row)) - 1;
                        _selectedRow = selectedRow;
                        _selRowArgs.SelectedRow = _dtOriginal.Rows[rowId];
                        _editArgs.SelectedRow = _dtOriginal.Rows[rowId];
                        _editArgs.InstallationNo = this.ProcessedArgs.InstallationNo;

                        drOriginal = _dtOriginal.GetDataRow(rowId);
                        if (drOriginal.Table.Columns.Contains("installation_NO"))
                            installationNo = Convert.ToInt32(drOriginal["installation_NO"]);
                        if (drOriginal.Table.Columns.Contains("CollectedDatetime"))
                            readDateTime = Convert.ToDateTime(drOriginal["CollectedDatetime"]);
                        if (drOriginal.Table.Columns.Contains("HS_Date"))
                            hourDateTime = Convert.ToDateTime(drOriginal["HS_Date"]);
                        if (drOriginal.Table.Columns.Contains("MGMD_Installation_No"))
                            mgmdInstallationNo = Convert.ToInt32(drOriginal["MGMD_Installation_No"]);
                        if (drOriginal.Table.Columns.Contains("MGMD_Start_Datetime"))
                            mgmdStartDatetime = Convert.ToDateTime(drOriginal["MGMD_Start_Datetime"]);
                        if (drOriginal.Table.Columns.Contains("MGMD_End_Datetime"))
                            mgmdEndDatetime = Convert.ToDateTime(drOriginal["MGMD_End_Datetime"]);

                        drTranslated = _dtTranslated.GetDataRow(rowId);
                        if (drTranslated != null)
                        {
                            string hourColumn = this.GetResourceTextByKey("Key_Hour");
                            if (drTranslated.Table.Columns.Contains(hourColumn))              // "Hour"
                                hour = Convert.ToString(drTranslated[hourColumn]);                // "Hour"
                        }
                    }
                }
                else
                {
                    _selectedRow = null;
                    _selRowArgs.SelectedRow = null;
                    _editArgs.InstallationNo = null;
                    _editArgs.SelectedRow = null;
                    drOriginal = null;
                    drTranslated = null;
                }

                /*
                if (dgvData.SelectedCells.Count > 0)
                {
                    rowIndex = dgvData.SelectedCells[0].RowIndex;
                }

                if (rowIndex != -1)
                {
                    if (rowIndex != _selectedRowIndex)
                    {
                        _selectedRowIndex = rowIndex;
                        _selRowArgs.SelectedRow = null;
                        _editArgs.SelectedRow = null;
                        _editArgs.SelectedRowIndex = -1;
                    }
                    else
                    {
                        isAlreadySelected = true;
                    }
                }
                else
                {
                    _selectedRowIndex = -1;
                    _selRowArgs.SelectedRow = null;
                    _editArgs.InstallationNo = null;
                    _editArgs.SelectedRow = null;
                    _editArgs.SelectedRowIndex = -1;
                }

                if (!isAlreadySelected)
                {
                    if (_selectedRowIndex != -1 &&
                        dgvData.DataSource != null)
                    {
                        DataTable dt = _dtOriginal;// dgvData.DataSource as DataTable;
                        if (dt != null &&
                            (_selectedRowIndex >= 0 && _selectedRowIndex < dt.Rows.Count))
                        {
                            _selRowArgs.SelectedRow = dt.Rows[_selectedRowIndex];
                            _editArgs.SelectedRow = dt.Rows[_selectedRowIndex];
                            _editArgs.SelectedRowIndex = _selectedRowIndex;
                            _editArgs.InstallationNo = this.ProcessedArgs.InstallationNo;
                        }
                    }
                }
                 * */

                if (fireOnSelect)
                {
                    this.OnRowSelected(_selRowArgs);
                }
                this.EnableDisableEditButton();
                return true;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                this.ShowErrorMessageBox(ex.Message);
                return false;
            }
        }

        public void EnableDisableEditButton()
        {
            btnEdit.Enabled = (_selRowArgs.SelectedRow != null);
        }

        private void EditItemInternal()
        {
            if (!this.IsEditable) return;

            try
            {
                DataRowView selectedRow;
                if (!this.EnsureRowSelected())
                {
                    this.ParentForm.ShowErrorMessageBox(this.GetResourceTextByKey(1, "MSG_MAT_INVALIDROW"));        //"Invalid row selected.");
                    return;
                }

                this.Cursor = Cursors.WaitCursor;
                Extensions.DisposeObject(ref _dsDetails);

                if (this.EditItem != null)
                {
                    using (IDataInterface data = this.CreateDataInterface())
                    {
                        IQueryExecutor<DataSet> sqlExec = this.EditItem(_editArgs);
                        _dsDetails = data.SelectQuery(sqlExec);

                        // Translate DataSet is called here for Externalization implementaion
                        _dsDetails = TranslateDataSetForEdit(_dsDetails);
                    }
                }

                this.OnEditClicked(_editArgs);
                if (_dtTranslated != null)
                {
                    DataTable dtInfo = _dtTranslated.Clone();
                    dtInfo.TableName = MeterGlobals.INFO_TABLE;
                    DataRow dr = dtInfo.NewRow();
                    dr.ItemArray = _selectedRow.Row.ItemArray;

                    dtInfo.Rows.Add(dr);
                    _dsDetails.Tables.Add(dtInfo.Copy());

                    selectedRow = dgvData.SelectedRows[0].DataBoundItem as DataRowView;
                    DataRow row = selectedRow.Row;
                    int rowId = Convert.ToInt32(row.GetType().GetField("_rowID", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(row)) - 1;

                    DataRow collectionDetails = _dtTranslated.GetDataRow(rowId);
                    string assetColmName = this.GetResourceTextByKey("Key_CollSch_Stock_no");
                    string gamingDateColmName = this.GetResourceTextByKey("Key_CollSch_Collection_Date");

                    if (collectionDetails.Table.Columns.Contains(assetColmName))              //"Asset No"
                        assetNo = Convert.ToString(collectionDetails[assetColmName]);
                    if (collectionDetails.Table.Columns.Contains(gamingDateColmName))                // "Gaming Date"
                        gamingDate = Convert.ToDateTime(collectionDetails[gamingDateColmName]);

                    DeltaDetailsForm detailsForm = new DeltaDetailsForm(_dsDetails, installationNo, readDateTime, hourDateTime, HeaderText, hour, assetNo, gamingDate, mgmdInstallationNo, mgmdStartDatetime, mgmdEndDatetime);
                    detailsForm.UpdateClick += new DeltaDetailsUpdateHandler(OnDetailsForm_UpdateClick);
                    detailsForm.CloseClick += new DeltaDetailsCloseHandler(OnDetailsForm_CloseClick);

                    bool hasChanges = false;
                    detailsForm.ShowDialogExResultAndDestroy(this.ParentForm, null,
                        (f) =>
                        {
                            hasChanges = f.HasChanges;
                        });
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        void OnDetailsForm_UpdateClick(object sender, DeltaDetailsUpdateEventArgs e)
        {
            try
            {
                if (e.Changed().Count > 0)
                {
                    if (this.UpdateItem != null)
                    {
                        using (IDataInterface data = this.CreateDataInterface())
                        {
                            IQueryExecutor<bool> sqlExec = this.UpdateItem(_editArgs);
                            if (sqlExec is InstallationUpdateExecutorBase)
                            {
                                IUpdateSet update = (IUpdateSet)sqlExec;
                                update.UpdateSet = this.CreateUpdateSets(e.Changed(), 1);
                                update.CurrentSet = this.CreateUpdateSets(e.Current(), 2);
                            }
                            bool result = data.UpdateQuery(sqlExec);
                        }
                    }
                    e.OwnerForm._dtDetail = _dsDetails.Tables[MeterGlobals.ORIGINAL_TABLE];
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        void OnDetailsForm_CloseClick(object sender, DeltaDetailsUpdateEventArgs e)
        {
            try
            {
                if (e.Changed().Count > 0)
                {
                    this.OnDeltaFormClosed(new DeltaDetailsCloseEventArgs(true)
                    {
                        ViewInfo = e.OwnerForm._dtViewInfo
                    });
                    this.LoadGrid();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            this.EditItemInternal();
        }

        private void dgvData_DoubleClick(object sender, EventArgs e)
        {
            this.EnsureRowSelected(true);
        }
    }
}
