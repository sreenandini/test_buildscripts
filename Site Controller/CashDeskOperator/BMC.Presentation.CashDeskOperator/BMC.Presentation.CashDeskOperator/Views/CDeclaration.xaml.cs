using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using BMC.CashDeskOperator;
using BMC.Presentation.POS.Views;
using BMC.Security;
using BMC.Transport;
using BMC.Common.Utilities;
using BMC.Presentation.POS.Helper_classes;
using BMC.CashDeskOperator.BusinessObjects;
using Audit.BusinessClasses;
using Audit.Transport;
using System.Data;
using BMC.Common.ExceptionManagement;
using System.Data.Linq;
using BMC.Common.ConfigurationManagement;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;
using BMC.DBInterface.CashDeskOperator;
using BMC.Business.CashDeskOperator;
using BMC.Common.LogManagement;
using System.ComponentModel;
using System.Threading;
using System.Configuration;
using System.Linq;
using BMC.Presentation.POS;


namespace BMC.Presentation
{
    /// <summary>
    /// Interaction logic for CDeclaration.xaml
    /// </summary>
    /// 
    public partial class CDeclaration : IDisposable, INotifyPropertyChanged
    {
        //
        #region Variable Declaration
        //
        public static event PropertyChangedEventHandler BusyPropertyChanged;
        CollectionHelper _collectionHelper;
        Declaration oDeclaration = null;
        CDeclareExceptionTickets oWindow = null;
        public static bool isCommonCDOforDeclaration;
        private string ExchangeConnectionString;
        private string TicketingConnectionString;
        private string SiteCode;
        private int _macCnt = 0;
        private int _currentThreadProcessed = 0;
        private int _NoOfMachinesPerThread = 0;
        private int _WaitTime = 1;
        private int[] _collectionNos = null;
        private readonly object _lockDeclaration = new object();
        private readonly object _lockProgess = new object();
        BackgroundWorker[] _bgDeclarationArray = null;
        System.Threading.ManualResetEvent[] _mDeclarationEvent = null;
        private UndeclaredCollection _selectedMachine = null;
        private DeclarationFilterColumn _filteredColumn = null;
        private bool _isInvalidMachine = false;
        private bool _isNoneCriteria = false;
        private string _sKeyText = string.Empty;
        public bool IsPartCollectionDeclaration = false;
        //For log the declared user - CR# 157474 -Starts

        private readonly int? _userNo = SecurityHelper.CurrentUser.SecurityUserID;

        //  CR# 157474 -Ends
        //
        #endregion Variable Declaration
        //
        #region Constructor
        //
        public CDeclaration()
        {
            try
            {
                LogInfo("START Loading Declaration Screen");
                InitializeComponent();
                ExchangeConnectionString = String.Empty;
                TicketingConnectionString = String.Empty;
                SiteCode = String.Empty;
                _NoOfMachinesPerThread = Convert.ToInt32(ConfigurationManager.AppSettings["NoOfDeclarationMachinesPerThread"]);
                if (_NoOfMachinesPerThread == 0) _NoOfMachinesPerThread = 1;
                _WaitTime = Convert.ToInt32(ConfigurationManager.AppSettings["DeclarationWaitTime"]);
                if (_WaitTime == 0) _WaitTime = 1;
                ManualCashEntry.sSiteCode = String.Empty;
                pgDeclaration.Visibility = Visibility.Collapsed;
                txtPGStatus.Visibility = Visibility.Collapsed;
                txtFilterText.PreviewMouseUp += new System.Windows.Input.MouseButtonEventHandler(txtFilterText_PreviewMouseUp);
               
                _collectionHelper = new CollectionHelper();

                isCommonCDOforDeclaration = false;

                if (Login._siteconfig != null && Login._siteconfig.Count > 0)
                {
                    isCommonCDOforDeclaration = true;
                    this.cboSiteCode.SelectionChanged -= cboSiteCode_SelectionChanged;
                    cboSiteCode.ItemsSource = Login._siteconfig;
                    this.cboSiteCode.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.cboSiteCode_SelectionChanged);
                    cboSiteCode.SelectedItem = Login._siteconfig.Find(m => m.SiteCode == Settings.SiteCode);
                }
                else
                {
                    CustomizeDeclaration();
                }

                CustomizeColumns();
                btnCashEntry.Visibility = Visibility.Collapsed;
                bool IsCounterEnabled = _collectionHelper.IsNoteCounterVisible();

                if (IsCounterEnabled)
                {
                    btnBillCounter.Content = FindResource("CDeclaration_xaml_btnBillCounter").ToString();
                }
                else
                {
                    btnBillCounter.Content = FindResource("CDeclaration_xaml_btnCashEntry").ToString();
                }

                if (Settings.CentralizedDeclaration)
                {
                    btnAcceptAll.Visibility = Visibility.Hidden;
                    btnBillCounter.Visibility = Visibility.Hidden;
                   
                }
                IsPartCollectionDeclaration = Security.SecurityHelper.HasAccess("BMC.Presentation.CDeclaration.PartCollectionDeclaration");
                SetCombo();
                LogInfo("END Loading Declaration Screen");
                
            }
            catch (Exception Ex)
            {
                MessageBox.ShowBox("MessageID425", BMC_Icon.Error);
                LogError("CDeclaration", Ex);
                SetDefaultCombo();
                dgDeclaration.ItemsSource = null;
            }
            finally
            {
                SetVisibilityForCurrencys();
            }
        }

        void SetVisibilityForCurrencys()
        {
            string[] sCurrencyList = null;

            try
            {
                sCurrencyList = ConfigurationManager.AppSettings[ExtensionMethods.CurrentSiteCulture].ToString().Split(',');
            }
            catch (Exception ex)
            {
                sCurrencyList = new string[] { "ONES", "TWOS", "FIVES", "TENS", "TWENTIES", "FIFTIES", "HUNDREDS" };
                ExceptionManager.Publish(ex);
            }
            try
            {
                foreach (var Denom in sCurrencyList)
                {
                    switch (Denom)
                    {
                        case "ONES":
                            dgDeclaration.Columns[15].Visibility = Visibility.Visible;  
                            break;
                        case "TWOS":
                            dgDeclaration.Columns[14].Visibility = Visibility.Visible;  
                            break;
                        case "FIVES":
                            dgDeclaration.Columns[13].Visibility = Visibility.Visible;  
                            break;
                        case "TENS":
                            dgDeclaration.Columns[12].Visibility = Visibility.Visible;  
                            break;
                        case "TWENTIES":
                            dgDeclaration.Columns[11].Visibility = Visibility.Visible;  
                            break;
                        case "FIFTIES":
                            dgDeclaration.Columns[10].Visibility = Visibility.Visible;  
                            break;
                        case "HUNDREDS":
                            dgDeclaration.Columns[9].Visibility = Visibility.Visible;  
                            break;
                        case "TWO_HUNDREDS":
                            dgDeclaration.Columns[8].Visibility = Visibility.Visible;  
                            break;
                        case "FIVE_HUNDREDS":
                            dgDeclaration.Columns[7].Visibility = Visibility.Visible;  
                            break;
                        case "THOUSANDS":
                            dgDeclaration.Columns[6].Visibility = Visibility.Visible;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }
        void CheckDeclarationMethod()
        {
            try
            {
                bool IsCounterEnabled = _collectionHelper.IsNoteCounterVisible();

                if (IsCounterEnabled)
                {
                    btnBillCounter.Content = FindResource("CDeclaration_xaml_btnBillCounter").ToString();
                }
                else
                {
                    btnBillCounter.Content = FindResource("CDeclaration_xaml_btnCashEntry").ToString();
                }
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }
        //
        #endregion Constructor
        //
        #region Background Thread Methods
        //
        void OnDeclarationInitialize(object sender, DoWorkEventArgs e)
        {
            try
            {
                int Start = (e.Argument as DeclarationParams)._UniqueId * _NoOfMachinesPerThread;
                for (int i = Start; i < Start + _NoOfMachinesPerThread; i++)
                {
                    if (i > dgDeclaration.Items.Count - 1) continue;
                    if ((dgDeclaration.Items[i] as UndeclaredCollectionRecord).Zone == "Total") continue;
                    lock (_lockDeclaration)
                    {
                        LogInfo("START Declaration");
                        try
                        {
                            if ((((DeclarationParams) e.Argument)._BatchId )== 0) // Handle part collection 
                            {
                                _collectionHelper.DeclarePartCollection(SecurityHelper.CurrentUser.SecurityUserID, (dgDeclaration.Items[i] as UndeclaredCollectionRecord).CollectionNo);
                            }
                            else
                            {
                                _collectionHelper.SaveFullCollection((dgDeclaration.Items[i] as UndeclaredCollectionRecord),
                                                     SecurityHelper.CurrentUser.SecurityUserID); 
                            }
                        }
                        catch (Exception ex) { LogError("OnDeclarationInitialize-SaveFullCollection", ex); }
                        LogInfo("END Declaration");
                    }
                    try
                    {
                        lock (_lockProgess)
                        {
                            _macCnt++;
                            UpdateProgressStatus(_macCnt + " Of " + TOTAL_COLLECTION, (_macCnt * 100) / TOTAL_COLLECTION);
                        }
                    }
                    catch (Exception ex) { LogError("OnDeclarationInitialize-DropProgress", ex); }
                    if (_mDeclarationEvent[(e.Argument as DeclarationParams)._UniqueId].WaitOne(_WaitTime))
                    {
                        break;
                    }
                }
                e.Result = e.Argument;
            }
            catch (Exception ex) { LogError("OnDeclarationInitialize", ex); }
        }
        //        
        void OnDeclarationComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                DeclarationParams _params = e.Result as DeclarationParams;
                _currentThreadProcessed++;
                if (_params._MaxThreads != _currentThreadProcessed) return;
                _currentThreadProcessed = 0;
                pgDeclaration.Visibility = Visibility.Collapsed;
                txtPGStatus.Visibility = Visibility.Collapsed;
                LogInfo("END Finish Declaration");
                if (((DeclarationParams)e.Result)._BatchId != 0) // If part collection skip this part 
                {

                    if (Settings.SGVI_Enabled && Settings.Client.ToUpper() == "SGVI")
                    {
                        CLiquidationDetails liquidationDetails = new CLiquidationDetails(_params._BatchId);
                        liquidationDetails.Owner = MessageBox.parentOwner;
                        liquidationDetails.ShowDialog();
                    }
                    _collectionHelper.InsertIntoExportHistory(_params._BatchId);
                }
                Audit(true);
                if (MessageBox.ShowBox("MessageID251", BMC_Icon.Question, BMC_Button.YesNo) == DialogResult.Yes)
                {
                    _collectionHelper.PrintCollectionBatch((IList<UndeclaredCollectionRecord>)dgDeclaration.ItemsSource, SecurityHelper.CurrentUser.UserName);
                }
                if (_params._BatchId != 0)
                {
                    CollectionBatchReports oReports = null;
                    if (isCommonCDOforDeclaration == false)
                    {

                        oReports = new CollectionBatchReports(_params._BatchId, Window.GetWindow(this));
                    }
                    else
                    {
                        oReports = new CollectionBatchReports(_params._BatchId, Window.GetWindow(this), (cboSiteCode.SelectedItem as SiteConfig).ExchangeConnectionString, (cboSiteCode.SelectedItem as SiteConfig).TicketConnectionString, (cboSiteCode.SelectedItem as SiteConfig).SiteName);
                    }

                    // Pass owner of form and set ShowInTaskbar as false                    
                    oReports.ShowDialogEx(this);
                }
                SetCombo();
                EnableDisableDeclarationScreen(true);
                IsBusy = false;
            }
            catch (Exception ex)
            {
                LogError("OnDeclarationComplete", ex);
                Audit(false);
                EnableDisableDeclarationScreen(true);
                IsBusy = false;
            }
        }
        //
        #endregion Background Thread Methods
        //
        #region Event Methods
        //
        private void btnAcceptAll_Click(object sender, RoutedEventArgs e)
        {
            int maxThreads = 0;
            int batchId = 0;
            _macCnt = 0;
            _currentThreadProcessed = 0;
            
            batchId = (cboMachineType.SelectedItem as UndeclaredCollection).Collection_Batch_No;

            MarkUndeclaredCollection();

            if (Validate()) return;

            try
            {
                //
                EnableDisableDeclarationScreen(false);
                //
                pgDeclaration.Visibility = Visibility.Visible;
                txtPGStatus.Visibility = Visibility.Visible;
                LogInfo("START Finish Declaration");
                IsBusy = true;
                maxThreads = (dgDeclaration.Items.Count / _NoOfMachinesPerThread) + (dgDeclaration.Items.Count % _NoOfMachinesPerThread);
                _bgDeclarationArray = new BackgroundWorker[maxThreads];
                _mDeclarationEvent = new ManualResetEvent[maxThreads];
                for (int i = 0; i < maxThreads; i++)
                {
                    _mDeclarationEvent[i] = new ManualResetEvent(false);
                    _bgDeclarationArray[i] = new BackgroundWorker();
                    _bgDeclarationArray[i].DoWork += OnDeclarationInitialize;
                    _bgDeclarationArray[i].RunWorkerCompleted += OnDeclarationComplete;
                    _bgDeclarationArray[i].RunWorkerAsync(new DeclarationParams()
                    {
                        _BatchId = batchId,
                        _UniqueId = i,
                        _MaxThreads = maxThreads
                    });
                    _bgDeclarationArray[i].WorkerSupportsCancellation = true;
                }

            }
            catch (Exception Ex) { LogError("btnAcceptAll_Click", Ex); }
        }

           //
        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            Print((IList<UndeclaredCollectionRecord>)dgDeclaration.ItemsSource, SecurityHelper.CurrentUser.UserName);
        }
        //
        private void cboSiteCode_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                LogManager.WriteLog("cboSiteCode_SelectionChanged Entry", LogManager.enumLogLevel.Debug);
                LogManager.WriteLog("Selected Site Code is " + (cboSiteCode.SelectedItem as SiteConfig).SiteCode, LogManager.enumLogLevel.Debug);
                if (!CollectionHelper.IsServerConnected((cboSiteCode.SelectedItem as SiteConfig).ExchangeConnectionString))
                {
                    LogManager.WriteLog("Unable to connect to the server", LogManager.enumLogLevel.Debug);
                    SetDefaultSiteCode(false, e);
                    LogManager.WriteLog("Returning without any message", LogManager.enumLogLevel.Debug);
                    return;
                }
                _collectionHelper = new CollectionHelper((cboSiteCode.SelectedItem as SiteConfig).ExchangeConnectionString);

                if (!_collectionHelper.IsAuthorized(SecurityHelper.CurrentUser.SecurityUserID, "BMC.Presentation.CommonCDOforDeclaration"))
                {
                    LogManager.WriteLog("User does not have authentication", LogManager.enumLogLevel.Debug);
                    SetDefaultSiteCode(true,e);
                    return;
                }

                CheckDeclarationMethod();

                SetCombo();
            }
            catch (Exception Ex)
            {
                LogError("cboSiteCode_SelectionChanged", Ex);
                SetDefaultCombo();
                dgDeclaration.ItemsSource = null;
            }
            LogManager.WriteLog("cboSiteCode_SelectionChanged Exit", LogManager.enumLogLevel.Debug);
        }

        private void SetDefaultSiteCode(bool bUserAuthenticationFailed, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (e.RemovedItems.Count > 0)
            {
                MessageBox.ShowBox(bUserAuthenticationFailed ? "MessageID405" : "MessageID404", BMC_Icon.Warning, 
                    (e.AddedItems[0] as SiteConfig).SiteCode);
                e.Handled = false;
                cboSiteCode.SelectedItem = Login._siteconfig.Find(x => x.SiteCode == Settings.SiteCode);
            }
        }
        //
        private void btnBillCounter_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                string SiteCode = string.Empty;
                UndeclaredCollection result = cboMachineType.SelectedItem as UndeclaredCollection;
                CashEntry oent = null;
                int BatchNo = result.Collection_Batch_No;                
                System.Windows.Forms.DialogResult dlgResult;
                bool bIntializeCashEntry = false;
                int selectedCollectionId = 0;
                int CollectionId = 0;
                int partCollectionId=  0;
                //
                string collectionType = string.Empty;
                //
                if (cboSiteCode.IsVisible)
                    SiteCode = (cboSiteCode.SelectedItem as SiteConfig).SiteCode;
                int CurrentCollection = 0;
                try
                {
                    CurrentCollection = ((IList<UndeclaredCollectionRecord>)dgDeclaration.ItemsSource).IndexOf((UndeclaredCollectionRecord)dgDeclaration.SelectedItem);

                }
                catch
                {
                    CurrentCollection = dgDeclaration.SelectedIndex;
                }

                collectionType = (dgDeclaration.Items[CurrentCollection] as UndeclaredCollectionRecord).Type;

                selectedCollectionId = (dgDeclaration.Items[CurrentCollection] as UndeclaredCollectionRecord).CollectionNo;
                if(collectionType == "Part") 
                    partCollectionId=selectedCollectionId;
                else
                    CollectionId=selectedCollectionId;

                var batchId = (dgDeclaration.Items[CurrentCollection] as UndeclaredCollectionRecord).CollectionBatchNo;
                if (Settings.AllowMultipleDrops && _collectionHelper.IsBatchProcessedByAnotherUser(batchId,selectedCollectionId))
                {
                    MessageBox.ShowBox("MessageID381", BMC_Icon.Error);
                    return;
                }
                if (Settings.AllowMultipleDrops && !isCommonCDOforDeclaration)
                {
                    switch (_collectionHelper.CheckPreviousDeclarationStatus(partCollectionId,CollectionId,0))
                    {
                        case 0:
                            bIntializeCashEntry = true;
                            break;
                        case 1:
                            dlgResult = MessageBox.ShowBox("MessageID438", BMC_Icon.Question, BMC_Button.YesNo);
                            if (dlgResult == System.Windows.Forms.DialogResult.Yes)
                                bIntializeCashEntry = true;
                            break;
                        case 2:
                            MessageBox.ShowBox("MessageID439", BMC_Icon.Warning, BMC_Button.OK);
                            break;
                        case 3:
                            dlgResult = MessageBox.ShowBox("MessageID440", BMC_Icon.Question, BMC_Button.YesNo);
                            if (dlgResult == System.Windows.Forms.DialogResult.Yes)
                                bIntializeCashEntry = true;
                            break;
                        default:
                            break;

                    }
                }
                else
                    bIntializeCashEntry = true;

                if (!bIntializeCashEntry)
                    return;

                if (isCommonCDOforDeclaration)
                    oent = new CashEntry(BatchNo, DeclarationFilterBy.None, string.Empty, SiteCode,
                       CurrentCollection, CommonUtilities.SiteConnectionString((cboSiteCode.SelectedItem as SiteConfig).ExchangeConnectionString),
                       CommonUtilities.TicketingConnectionString((cboSiteCode.SelectedItem as SiteConfig).TicketConnectionString), (IList<UndeclaredCollectionRecord>)dgDeclaration.ItemsSource);
                else
                    oent = new CashEntry(BatchNo, DeclarationFilterBy.None, string.Empty, Settings.SiteCode, CurrentCollection,
                          (IList<UndeclaredCollectionRecord>)dgDeclaration.ItemsSource);
                oent.Owner = Window.GetWindow(this);
                oent.ShowDialog();
                this.RefreshData();

            }
            catch (Exception ex) { LogError("btnBillCounter_Click", ex); }
        }

       

              
        //
        private void cboMachineType_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                _selectedMachine = cboMachineType.SelectedItem as UndeclaredCollection;
                this.CheckAndFilterData();
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }
        //
        private void cboFilterBy_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                _filteredColumn = cboFilterBy.SelectedItem as DeclarationFilterColumn;
                _isNoneCriteria = true;
                if (_filteredColumn != null)
                {
                    if (_filteredColumn.ValueName != DeclarationFilterBy.None)
                    {
                        _isNoneCriteria = false;
                    }
                    //for part collection disable type filter
                    if (_selectedMachine!=null && _selectedMachine.Collection_Batch_No == 0 && _filteredColumn.ValueName == DeclarationFilterBy.Type)
                    {
                        _isNoneCriteria = true;
                    }
                }
            

                if (this.IsInitialized)
                {
                    if (_isNoneCriteria) txtFilterText.Text = "";
                }
                this.EnableDisableFilterControls();

            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
            
        }
        //
        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            this.FillData();
        }
        //
        void txtFilterText_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!Settings.OnScreenKeyboard)
                return;
            txtFilterText.Text = DisplayKeyboard(string.Empty);
            txtFilterText.SelectAll();
        }
        //
        void ObjKeyboardClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (((KeyboardInterface)sender).DialogResult == true)
            {
                _sKeyText = ((KeyboardInterface)sender).KeyString;
            }
        }
        //
        #endregion Event Methods
        //
        #region Data Holder
        //
        private string SELECTED_COLLECTION { get; set; }
        //
        private int TOTAL_COLLECTION { get; set; }
        //
        private int TOTAL_INDEX { get; set; }
        //
        #endregion Data Holder
        //
        #region Functionality Methods
        //
        private void LogError(string methodName, Exception ex)
        {
            LogManager.WriteLog("CPerformDrop Error in " + methodName + " : " + ex.Message, LogManager.enumLogLevel.Error);
            ExceptionManager.Publish(ex);
        }
        //
        private void LogInfo(string content)
        {
            LogManager.WriteLog("CDeclaration Info - " + content + " : " +
                DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff tt"), LogManager.enumLogLevel.Info);
        }
        //
        private void CustomizeColumns()
        {
            int[] columnsIndex = new int[] { 8 };
           
            if (Settings.Declaration_ShowoutValues)
            {
                this.dgDeclaration.Columns[6].Header = FindResource("CDeclaration_xaml_GridViewColumn_32").ToString();

                //this.dgDeclaration.Columns[3].CellTemplate = null;
                //this.dgDeclaration.Columns[3].Width = 0;

                // Changed code from Width=0 to Visibility=Hidden as we are using Datagrid
                this.dgDeclaration.Columns[3].Visibility = Visibility.Hidden;
            }
            else
            {
                foreach (int i in columnsIndex)
                {

                     //this.dgDeclaration.Columns[i].CellTemplate = null;
                     //this.dgDeclaration.Columns[i].Width = 0;

                    // Changed code from Width=0 to Visibility=Hidden as we are using Datagrid
                    this.dgDeclaration.Columns[i].Visibility = Visibility.Hidden;
                }
            }
        }
       
        private void CustomizeDeclaration()
        {
            cboSiteCode.Visibility = Visibility.Collapsed;
            cboMachineType.Margin = new Thickness(13, 8, 0, 0);
            cboFilterBy.Width = 180;
            cboFilterBy.Margin = new Thickness(270, 8, 0, 0);
            cboFilterBy.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            txtFilterText.Margin = new Thickness(460, 8, 0, 0);
            txtFilterText.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            btnFilter.Margin = new Thickness(670, 8, 0, 0);
            btnFilter.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
        }
        //
        private SiteConfig GetSiteItem(string sitecode)
        {
            try
            {
                return Login._siteconfig.Find(m => m.SiteCode == sitecode);
            }
            catch (Exception ex) { return null; }
        }
        //
        private void SetCombo()
        {
            LogInfo("START SetCombo");
            cboFilterBy.ItemsSource = _collectionHelper.GetDeclarationFilterColumns(this);

            List<UndeclaredCollection> machineTypeSource = new List<UndeclaredCollection>();
            UndeclaredCollection variable = new UndeclaredCollection
            {
                Collection_Batch_Name = "",
                Collection_Batch_No = -1,
                DisplayName = FindResource("PleaseSelectbatch") as string
            };
            machineTypeSource.Add(variable);
            machineTypeSource.AddRange(_collectionHelper.GetUndeclaredCollectionList(false, IsPartCollectionDeclaration));
            cboMachineType.ItemsSource = machineTypeSource;
            if (cboMachineType.Items.Count > 1)
            {
                cboMachineType.SelectedIndex = 1;
                if (!Settings.CentralizedDeclaration)
                {
                    btnBillCounter.Visibility = Visibility.Visible;
                }
                else
                {
                    btnBillCounter.Visibility = Visibility.Hidden;
                }
                
            }
            else
            {
                cboMachineType.SelectedIndex = 0;
                btnBillCounter.Visibility = Visibility.Hidden;
            }
            LogInfo("END SetCombo");
        }
        //
        private void SetDefaultCombo()
        {
            try
            {
                List<UndeclaredCollection> enumerator = new List<UndeclaredCollection>();
                UndeclaredCollection variable = new UndeclaredCollection
                {
                    Collection_Batch_Name = "",
                    Collection_Batch_No = -1,
                    DisplayName = FindResource("PleaseSelectbatch") as string
                };
                enumerator.Add(variable);
                if (cboMachineType != null)
                {
                    cboMachineType.ItemsSource = enumerator;
                }
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }
        //
        private static bool isBusy;
        public static bool IsBusy
        {
            get
            { return isBusy; }
            set
            {
                isBusy = value;
                OnPropertyChanged("IsBusy");
            }
        }

        // Create the OnPropertyChanged method to raise the event 
        public static void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = BusyPropertyChanged;
            if (handler != null)
            {
                handler(new CDeclaration(), new PropertyChangedEventArgs(name));
            }
        }

        //
        private void EnableDisableDeclarationScreen(bool Enable)
        {
            btnAcceptAll.IsEnabled = Enable;
            cboMachineType.IsEnabled = Enable;
            btnFilter.IsEnabled = Enable;
            cboSiteCode.IsEnabled = Enable;
            cboFilterBy.IsEnabled = Enable;
            txtFilterText.IsEnabled = Enable;
            dgDeclaration.IsEnabled = Enable;
        }
        //
        private bool Validate()
        {
            try
            {

                int batchID = (cboMachineType.SelectedItem as UndeclaredCollection).Collection_Batch_No;
                
                if (batchID == -1 || TOTAL_COLLECTION == 0)
                {
                    MessageBox.ShowBox("MessageID64", BMC_Icon.Warning);
                    return true;
                }

                if (Convert.ToBoolean(AppSettings.Is_Confirmation_Required_on_Declaration))
                {
                    if (((UndeclaredCollectionRecord)dgDeclaration.Items[TOTAL_INDEX]).TotalAmountValue <= 0)
                        if (MessageBox.ShowBox("MessageID250", BMC_Icon.Question, BMC_Button.YesNo) == DialogResult.No) return true;
                        else
                            if (MessageBox.ShowBox("MessageID65", BMC_Icon.Question, BMC_Button.YesNo) == DialogResult.No) return true;

                    if (MessageBox.ShowBox("MessageID106", BMC_Icon.Question, BMC_Button.YesNo) ==
                            System.Windows.Forms.DialogResult.No) return true;
                }
                int CurrentCollection = 0;
                try
                {
                    CurrentCollection = ((IList<UndeclaredCollectionRecord>)dgDeclaration.ItemsSource).IndexOf((UndeclaredCollectionRecord)dgDeclaration.SelectedItem);

                }
                catch
                {
                    CurrentCollection = dgDeclaration.SelectedIndex;
                }

                var selectedCollectionId = (dgDeclaration.Items[CurrentCollection] as UndeclaredCollectionRecord).CollectionNo;
                int selectedBatchId = (dgDeclaration.Items[CurrentCollection] as UndeclaredCollectionRecord).CollectionBatchNo;

                if (_collectionHelper.IsBatchProcessedByAnotherUser(selectedBatchId, selectedCollectionId))
                {
                    MessageBox.ShowBox("MessageID381", BMC_Icon.Error);
                    SetCombo();
                    return true;
                }

                //Multiple Drops 
                //If any previous declaration is pending for the same VLT then user would not be allowed to declare for that collection.
                //if(_collectionHelper.PreviousDropDeclarationStatus(
                

                return false;
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
            return true;
        }
        //
        private void UpdateProgressStatus(string progress, int iProMac)
        {
            /* 
             * Used dispatcher as the UI thread will be accessed from another thread
             */
            System.Windows.Application.Current.Dispatcher.Invoke((ThreadStart)delegate
            {
                txtPGStatus.Text = progress;
                pgDeclaration.Value = iProMac;
            });
        }
        //
        private void Audit(bool success)
        {
            try
            {
                // Part_Collection 
                string PartCollectionMachines = string.Empty;
                if (((UndeclaredCollection)cboMachineType.SelectedItem).Collection_Batch_No == 0) 
                {
                    ((List<UndeclaredCollectionRecord>)dgDeclaration.ItemsSource).ForEach((s) => { if (s.CollectionNo != 0)  PartCollectionMachines += (PartCollectionMachines == string.Empty) ? ":" + s.Position : "," + s.Position; });
                }
                // End Part_Collection 

                LogInfo("START Audit");
                AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                {
                    AuditModuleName = ModuleName.LocalDeclaration,
                    Audit_Screen_Name = "MachineDrop|Declaration",
                    Audit_Desc = "Declaration " + (success ? "" : "not") + " Successful for the Batch: "
                                    + ((UndeclaredCollection)cboMachineType.SelectedItem).DisplayName,
                    AuditOperationType = OperationType.ADD,
                    Audit_Field = "Batch",
                    Audit_New_Vl = ((UndeclaredCollection)cboMachineType.SelectedItem).Collection_Batch_Name + PartCollectionMachines

                });
                LogInfo("END Audit");
            }
            catch (Exception ex) { LogError("Audit", ex); }
        }
        //
        private DataSet GetBatchDataset(IList<UndeclaredCollectionRecord> collectionRecords, bool addShortPay)
        {
            DataTable lineItem = new DataTable("DeclarationPrint");
            lineItem.Columns.Add("Asset", typeof(string));
            lineItem.Columns.Add("Pos", typeof(string));
            lineItem.Columns.Add("Bills", typeof(decimal));
            lineItem.Columns.Add("CoinsIn", typeof(decimal));
            lineItem.Columns.Add("TicketsIn", typeof(decimal));
            lineItem.Columns.Add("EFTIn", typeof(decimal));
            lineItem.Columns.Add("TotalCashIn", typeof(decimal));
            lineItem.Columns.Add("TicketsOut", typeof(decimal));
            lineItem.Columns.Add("ShortPay", typeof(decimal));
            lineItem.Columns.Add("CancelledCredits", typeof(decimal));
            lineItem.Columns.Add("Collection_Batch_Name", typeof(string));

            foreach (UndeclaredCollectionRecord collectionRecord in collectionRecords)
            {
                if (!((collectionRecord.Zone + "").ToUpper().Trim().Equals("TOTAL")))
                {
                    DataRow dr = lineItem.NewRow();
                    dr["Bills"] = collectionRecord.TotalBillValue;
                    dr["CoinsIn"] = collectionRecord.TotalCoinsValue;
                    dr["TicketsIn"] = collectionRecord.TicketsInValue;
                    dr["EFTIn"] = collectionRecord.EFTInValue;
                    dr["TotalCashIn"] = (collectionRecord.TotalBillValue + collectionRecord.TotalCoinsValue + collectionRecord.TicketsInValue);
                    dr["TicketsOut"] = collectionRecord.TicketsOutValue;
                    dr["ShortPay"] = collectionRecord.ShortPayValue;
                    dr["CancelledCredits"] = collectionRecord.AttendantPayValue;
                    dr["Asset"] = collectionRecord.AssetNo;
                    dr["Collection_Batch_Name"] = collectionRecord.Collection_Batch_Name;
                    //if ((collectionRecord.Zone + "").ToUpper().Trim().Equals("TOTAL"))
                    //    dr["Pos"] = "Total";
                    //else
                        dr["Pos"] = collectionRecord.Position;
                    lineItem.Rows.Add(dr);
                }
            }
            DataSet DSDeclaration = new DataSet("DeclarationPrint");
            DSDeclaration.Tables.Add(lineItem);
            return DSDeclaration;
        }
        //
        private void Print(IList<UndeclaredCollectionRecord> collectionRecords, string userName)
        {
            int batchNo = 0;
            int nMachineCount = 0;
            string sDropType = string.Empty;
            string sExchangeConnectionString = string.Empty;
            string sTicketConnectionString = string.Empty;
            DataSet DSDeclaration;
            ILiquidationDetails details = LiquidationBusinessObject.CreateInstance();
            try
            {
                batchNo = ((UndeclaredCollection)cboMachineType.SelectedItem).Collection_Batch_No;
                nMachineCount = collectionRecords.Count - 1;
                sDropType = string.Empty;
                foreach (UndeclaredCollectionRecord Collection in collectionRecords)
                {
                    /*if (sDropType == string.Empty)
                    {
                        sDropType = Collection.Type == "Defloat" ? "Final" : "Standard";
                    }
                    else*/
                    {
                        switch (Collection.Type)
                        {
                            case "Defloat":
                                if (!sDropType.Contains("Final"))
                                    sDropType = sDropType + (sDropType.Length >0?", ":"") + "Final";
                                break;
                            case "Total":
                                break;
                            default:
                                if (!sDropType.Contains("Standard"))
                                    sDropType = sDropType + (sDropType.Length > 0 ? ", " : "") + "Standard";
                                break;
                        }
                        
                }
                }
                

                if (collectionRecords.Count >1)
                {
                    if (collectionRecords[1].CollectionBatchNo == 0)
                    {
                        sDropType = "Part";
                    }
                }

                bool bAddShortPay = false;
                try
                {   bAddShortPay = Convert.ToBoolean(details.GetSetting("AddShortpayInVoucherOut"));    }
                catch{}

                DSDeclaration = GetBatchDataset(collectionRecords, bAddShortPay);
                if (DSDeclaration.Tables[0].Rows.Count == 0)
                {
                    MessageBox.ShowBox("MessageID261", BMC_Icon.Information);
                    return;
                }
                if (isCommonCDOforDeclaration)
                {
                    sExchangeConnectionString = (cboSiteCode.SelectedItem as SiteConfig).ExchangeConnectionString;
                    sTicketConnectionString = (cboSiteCode.SelectedItem as SiteConfig).TicketConnectionString;
                }
                using (CReportViewer cReportViewer = new CReportViewer())
                {
                    cReportViewer.ShowDeclarationReport(DSDeclaration, userName, batchNo, nMachineCount, sDropType,
                        sExchangeConnectionString, sTicketConnectionString);
                    cReportViewer.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                MessageBox.ShowBox("MessageID262", BMC_Icon.Error);
            }

        }
        //
        private void RefreshData()
        {
            if (btnFilter.IsEnabled)
                this.FillData();
            else
                this.CheckAndFilterData();
        }
        //
        private void CheckAndFilterData()
        {
            _isInvalidMachine = true;
            if (_selectedMachine != null)
            {
                if (_selectedMachine.Collection_Batch_No != -1)
                {
                    _isInvalidMachine = false;
                }
            }
            if (cboFilterBy.SelectedIndex != 0)
            {
                cboFilterBy.SelectedIndex = 0;
            }
            txtFilterText.Text = "";
            
            this.EnableDisableFilterControls();
            this.FillData();
        }
        //
        private void FillData()
        {

            try
            {
                dgDeclaration.ItemsSource = null;

                int batchNo = -1;
                DeclarationFilterBy filterBy = DeclarationFilterBy.None;
                string filterValue = string.Empty;
                if (_selectedMachine != null)
                {
                    batchNo = _selectedMachine.Collection_Batch_No;
                }
                if (_filteredColumn != null)
                {
                    filterBy = _filteredColumn.ValueName;
                }
                if (filterBy != DeclarationFilterBy.None)
                {
                    filterValue = txtFilterText.Text;
                    
                    if (filterBy == DeclarationFilterBy.Type)
                    {
                        if (!string.IsNullOrEmpty(filterValue))
                        {
                           if (string.Compare(filterValue, "Final", true) == 0)
                            {
                                filterValue = "1";
                            }
                          else if (string.Compare(filterValue, "Standard", true) == 0)
                           {
                               filterValue = "0";
                           }                           
                          else
                          {
                               filterValue = "None";
                               MessageBox.ShowBox("MessageID548", BMC_Icon.Information);
                          }
                           
                        }
                    }
                }
                
                if(string.Compare(filterValue, "None", true) == 0)
                    return;

                dgDeclaration.ItemsSource = _collectionHelper.GetUndeclaredCollectionByBatchNo(batchNo, filterBy, filterValue);
                if (dgDeclaration.Items.Count > 1 && !Settings.CentralizedDeclaration)
                {
                    dgDeclaration.SelectedIndex = 1;
                    btnBillCounter.Visibility = Visibility.Visible;
                }
                else
                {
                    btnBillCounter.Visibility = Visibility.Hidden;
                }
                }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }
        //
        private int[] GetCollections
        {
            get
            {
                return _collectionNos;
            }
        }
        //
        private void MarkUndeclaredCollection()
        {
            _collectionNos = new int[dgDeclaration.Items.Count];
            for (int i = 0; i < dgDeclaration.Items.Count; i++)
            {
                if (((UndeclaredCollectionRecord)dgDeclaration.Items[i]).CollectionNo > 0)
                {
                    SELECTED_COLLECTION += (string.IsNullOrEmpty(SELECTED_COLLECTION) ? "" : ", ") +
                        ((UndeclaredCollectionRecord)dgDeclaration.Items[i]).CollectionNo.ToString();
                }
                else
                    TOTAL_INDEX = i;

                _collectionNos[i] = ((UndeclaredCollectionRecord)dgDeclaration.Items[i]).CollectionNo;
            }
            TOTAL_COLLECTION = dgDeclaration.Items.Count - 1;
        }
        //
        public void EnableDisableFilterControls()
        {
            bool enableCombo = !_isInvalidMachine;
            bool enableTextAndButton = !_isNoneCriteria;
            if (!this.IsInitialized) return;

            cboFilterBy.IsEnabled = enableCombo;
            txtFilterText.IsEnabled = enableTextAndButton;
            btnFilter.IsEnabled = enableCombo;
        }
        //
        public string DisplayKeyboard(string keyText)
        {
            _sKeyText = "";
            KeyboardInterface objKeyboard = null;

            try
            {
                Window w = Window.GetWindow(this);
                Point pt = default(Point);
                Size sz = default(Size);
                if (w != null)
                {
                    pt = new Point(w.Left, w.Top);
                    sz = new Size(w.Width, w.Height);
                }

                objKeyboard = new KeyboardInterface();
                objKeyboard.Owner = w;
                objKeyboard.Closing += ObjKeyboardClosing;
                objKeyboard.KeyString = keyText;
                objKeyboard.Top = pt.Y + (sz.Height - objKeyboard.Height);
                objKeyboard.Left = pt.X + (sz.Width / 2) - (objKeyboard.Width / 2);
                objKeyboard.ShowInTaskbar = false;
                objKeyboard.ShowDialog();

                if (objKeyboard != null)
                {
                    objKeyboard.Closing -= ObjKeyboardClosing;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {

            }
            return _sKeyText;
        }
        //
        #endregion Functionality Methods
        //
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
            foreach (ManualResetEvent mResetevent in _mDeclarationEvent)
            {
                mResetevent.Set();
            }
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
                        this.btnAcceptAll.Click -= (this.btnAcceptAll_Click);
                    },
                    (c) =>
                    {
                    });
                    this.WriteLog("CDeclaration objects are released successfully.");

                }
                disposed = true;
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="CDeclaration"/> is reclaimed by garbage collection.
        /// </summary>
        ~CDeclaration()
        {
            Dispose(false);
        }

        #endregion
        //

        public event PropertyChangedEventHandler PropertyChanged;

        //
        private void dgDeclaration_Sorting(object sender, Microsoft.Windows.Controls.DataGridSortingEventArgs e)
        {
            try
            {
                ICollectionView view = CollectionViewSource.GetDefaultView(dgDeclaration.ItemsSource);
                ListSortDirection direction = (e.Column.SortDirection != ListSortDirection.Ascending) ? ListSortDirection.Ascending : ListSortDirection.Descending;
                e.Column.SortDirection = direction;
                e.Handled = true;
                view.SortDescriptions.Clear();
                view.SortDescriptions.Insert(0, new SortDescription() { PropertyName = "IsTotalRow", Direction = ListSortDirection.Descending });
                view.SortDescriptions.Insert(1, new SortDescription() { PropertyName = e.Column.SortMemberPath, Direction = direction });
                view.Refresh();
            }
            catch (Exception Ex)
            {

                ExceptionManager.Publish(Ex);
            }
        }

        private void dgDeclaration_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                UndeclaredCollectionRecord sel_item = e.AddedItems[0] as UndeclaredCollectionRecord;
                if (sel_item != null)
                {
                    btnBillCounter.Visibility = sel_item.CollectionNo > 0 && Settings.CentralizedDeclaration == false ? Visibility.Visible : Visibility.Hidden;
                }
            }
            catch (Exception Ex)
            {

                ExceptionManager.Publish(Ex);
            }
        }

        private void btnCashEntry_Click(object sender, RoutedEventArgs e)
        {

        }
    }
    //
    public class DeclarationParams
    {
        public int _UniqueId { get; set; }
        public int _MaxThreads { get; set; }
        public int _BatchId { get; set; }
    }
}
