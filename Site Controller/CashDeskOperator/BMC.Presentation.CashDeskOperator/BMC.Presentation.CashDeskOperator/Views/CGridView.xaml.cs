namespace BMC.Presentation
{
    #region Namespaces
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using BMC.CashDeskOperator.BusinessObjects;
    using BMC.CashDeskOperator;
    using System.Windows.Media.Animation;
    using System.ComponentModel;
    using BMC.Common.ExceptionManagement;
    using BMC.Transport;
    using BMC.Common.LogManagement;
    using BMC.Presentation.POS.Helper_classes;
    using System.Threading;
    using System.Windows.Data;
    #endregion Namespaces

    #region Class
    /// <summary>
    /// Interaction logic for CGridView.xaml
    /// </summary>
    public partial class CGridView : UserControl, IDisposable, INotifyPropertyChanged
    {
        #region Private Members
        private double panelHeight;

        private bool _Carded            =   true;        
        private bool _GamePlay          =   true;        
        private bool _FloatUnDeclared   =   true;
        private bool _UnclearedEvent    =   true;
        private bool _ClearedEvent      =   true;
        private bool _GameCapping       =   true;

        private Int32 _TotalAsset           =   0;
        private double? _TotalBuyIn         =   0;
        private double? _TotalWinLoss       =   0;
        private double? _TotalTimePlayed    =   0;
        
        private List<SessionGamePlayDetails> resultSet          =   null;
        private List<SessionGamePlayDetails> summaryResultSet   =   null;        

        #endregion Private Members

        #region ReadOnly Members
        readonly ISessionGamePlayDetails _iSessionGamePlay  =   GamePlayDetailsBusinessObject.CreateInstance();
        #endregion ReadOnly Members

        #region Constructor
        public CGridView()
        {
            InitializeComponent();            
        }
        #endregion Constructor

        #region Properties
        public bool Carded
        {
            get { return _Carded; }
            set
            {
                _Carded = value;
                OnPropertyChanged("Carded"); FilterGridView();
               
            }
        }

        public bool GamePlay
        {
            get { return _GamePlay; }
            set
            {
                _GamePlay = value;
                OnPropertyChanged("GamePlay"); FilterGridView();
               
            }
        }   

        public bool FloatUnDeclared
        {
            get { return _FloatUnDeclared; }
            set
            {
                _FloatUnDeclared = value;
                OnPropertyChanged("FloatUnDeclared"); FilterGridView();
               
            }
        }

        public bool UnclearedEvent
        {
            get { return _UnclearedEvent; }
            set
            {
                _UnclearedEvent = value;
                OnPropertyChanged("UnclearedEvent"); FilterGridView();
               
            }
        }

        public bool ClearedEvent
        {
            get { return _ClearedEvent; }
            set
            {
                _ClearedEvent = value;
                OnPropertyChanged("ClearedEvent"); FilterGridView();
                
            }
        }

        public bool GameCapping
        {
            get { return _GameCapping; }
            set
            {
                _GameCapping = value;
                OnPropertyChanged("GameCapping"); FilterGridView();
                
            }
        }

        public Int32 TotalAsset
        {
            get { return _TotalAsset; }
            set
            {
                _TotalAsset = value;
                OnPropertyChanged("TotalAsset"); 
            }
        }

        public double? TotalBuyIn
        {
            get { return _TotalBuyIn; }
            set
            {
                _TotalBuyIn = value;
                OnPropertyChanged("TotalBuyIn");
            }
        }

        public double? TotalWinLoss
        {
            get { return _TotalWinLoss; }
            set
            {
                _TotalWinLoss = value;
                OnPropertyChanged("TotalWinLoss");
            }
        }

        public double? TotalTimePlayed
        {
            get { return _TotalTimePlayed; }
            set
            {
                _TotalTimePlayed = value;
                OnPropertyChanged("TotalTimePlayed");
            }
        }             
        #endregion

        #region Events
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var item in spPanel.Children)
                if (((UIElement)item).Visibility == Visibility.Visible)
                    panelHeight += ((UIElement)item).RenderSize.Height;

            panelHeight += 10;

            txtLegend.Triggers.Add(OpenAnimation(Button.ClickEvent));
            txtLegend_.Triggers.Add(CloseAnimation(Button.ClickEvent));
            GetSessionGamePlayDetails();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside btnRefresh_Click", LogManager.enumLogLevel.Info);                
                    //_workerThread   =   new Thread(new ThreadStart(MeterForcePeriodic));                
                MeterForcePeriodic();
                FilterGridView();
                
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }            
        }

     
       
        private void txtLegend_Click(object sender, RoutedEventArgs e)
        {
            txtLegend.Visibility = Visibility.Hidden;
            txtLegend_.Visibility = Visibility.Visible;
        }

        private void txtLegend__Click(object sender, RoutedEventArgs e)
        {
            txtLegend_.Visibility = Visibility.Hidden;
            txtLegend.Visibility = Visibility.Visible;
        }
        #endregion Events

        #region Private Methods
        public void GetSessionGamePlayDetails()
        {
            try
            {
                LogManager.WriteLog("Inside GetSessionGamePlayDetails", LogManager.enumLogLevel.Info);

                resultSet           =   _iSessionGamePlay.GetSessionGamePlayDetails().ToList();
                summaryResultSet    =   resultSet;
                BindGridViewSummary();
                Helper_classes.Common.BindDataGrid(resultSet, dgGridView);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }            
        }

        private void FilterGridView()
        {
            try
            {
                LogManager.WriteLog("Inside FilterGridView", LogManager.enumLogLevel.Info);

                var filteredResultSet = from item in resultSet
                                where (_Carded && item.Play_Status == 2)
                                || (_GamePlay && item.Play_Status == 1)
                                || (_FloatUnDeclared && item.Drop_Status == 2)
                                || (_UnclearedEvent && item.Event_Status == true)
                                || (_ClearedEvent && item.Event_Status == false)
                                || (_GameCapping && item.GameCapping == 1
                                )
                                select item;
              
                summaryResultSet    =   filteredResultSet.ToList();
                BindGridViewSummary();
                Helper_classes.Common.BindDataGrid(summaryResultSet, dgGridView);
            }
            catch (Exception ex) 
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void BindGridViewSummary()
        {
            try
            {
                LogManager.WriteLog("Inside BindGridViewSummary", LogManager.enumLogLevel.Info);
          
                TotalAsset      =   summaryResultSet.Count();
                TotalBuyIn      =   summaryResultSet.Sum(x => x.Total_BuyIn);
                TotalWinLoss    =   summaryResultSet.Sum(x => x.Win_Loss);
                TotalTimePlayed =   summaryResultSet.Sum(x => x.Time_Played);
                int TotTimePlayed =   summaryResultSet.Sum(x => x.Time_Played);

                summaryResultSet.Insert(0, new SessionGamePlayDetails()
                {
                    Position = FindResource("CGridView_xaml_lblTotal") as string,
                    Asset_No = TotalAsset.ToString(),
                    IsTotalRow = true,
                    Total_BuyIn = TotalBuyIn.Value,
                    Win_Loss = TotalWinLoss.Value,
                    Time_Played = TotTimePlayed,
                    Total_BuyIn_Color = "#D7E9EA",
                    Win_Loss_Color = "#D7E9EA",
                    Time_Played_Color = "#D7E9EA"
                });                
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void dgGridView_Sorting(object sender, Microsoft.Windows.Controls.DataGridSortingEventArgs e)
        {
            try
            {
                ICollectionView view = CollectionViewSource.GetDefaultView(dgGridView.ItemsSource);
                e.Column.SortDirection = (e.Column.SortDirection != ListSortDirection.Ascending) ? ListSortDirection.Ascending : ListSortDirection.Descending;
                e.Handled = true;
                view.SortDescriptions.Clear();
                view.SortDescriptions.Insert(0, new SortDescription() { PropertyName = "IsTotalRow", Direction = ListSortDirection.Descending });
                    view.SortDescriptions.Insert(1, new SortDescription() { PropertyName = e.Column.SortMemberPath, Direction = e.Column.SortDirection.Value });
                view.Refresh();

            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }
        private void SortGridView(string sortType)
        {
            try
            {
                LogManager.WriteLog("Inside SortGridView", LogManager.enumLogLevel.Info);

                switch (sortType)
                {
                    case "BuyIn":
                        var sortBuyInResultSet = summaryResultSet.OrderByDescending(x => x.Total_BuyIn);
                        Helper_classes.Common.BindDataGrid(sortBuyInResultSet, dgGridView);
                        break;
                    case "WinLoss":
                        var sortWinLossResultSet = summaryResultSet.OrderByDescending(x => x.Win_Loss);
                        Helper_classes.Common.BindDataGrid(sortWinLossResultSet, dgGridView);
                        break;
                    case "Time":
                        var sortTimeResultSet = summaryResultSet.OrderByDescending(x => x.Time_Played);
                        Helper_classes.Common.BindDataGrid(sortTimeResultSet, dgGridView);
                        break;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void MeterForcePeriodic()
        {   
            List<ActiveSessionInstallations> _activeSessionInstallations          =   null;

            try
            {
                LogManager.WriteLog("Inside MeterForcePeriodic", LogManager.enumLogLevel.Info);                              

                Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(delegate()
                {
                    btnRefresh.IsEnabled = false;
                }));               

                _activeSessionInstallations = _iSessionGamePlay.GetInstallationForActiveSession().ToList();
                
                foreach (ActiveSessionInstallations result in _activeSessionInstallations)                                       
                    _iSessionGamePlay.GetCurrentMeters(result.Installation_No);         

                GetSessionGamePlayDetails();

                
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(delegate()
                {
                    btnRefresh.IsEnabled = true;
                }));               

                if (_activeSessionInstallations != null)
                    _activeSessionInstallations = null;
                
            }
        }
        #endregion Private Methods

        #region Triggers
        private EventTrigger OpenAnimation(RoutedEvent RE)
        {
            EventTrigger eventTrigger = new EventTrigger(RE);
            BeginStoryboard beginStoryBoard = new BeginStoryboard();
            Storyboard storyBoard = new Storyboard();
            DoubleAnimation doubleAnimation = new DoubleAnimation();
            doubleAnimation.From = 0;
            doubleAnimation.To = panelHeight;
            doubleAnimation.Duration = new Duration(new TimeSpan(5000000));
            Storyboard.SetTargetName(doubleAnimation, "spPanel");
            Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("(StackPanel.Height)"));
            storyBoard.Children.Add(doubleAnimation);
            beginStoryBoard.Storyboard = storyBoard;
            eventTrigger.Actions.Add(beginStoryBoard);
            return eventTrigger;
        }

        private EventTrigger CloseAnimation(RoutedEvent RE)
        {
            EventTrigger eventTrigger = new EventTrigger(RE);
            BeginStoryboard beginStoryBoard = new BeginStoryboard();
            Storyboard storyBoard = new Storyboard();
            DoubleAnimation doubleAnimation = new DoubleAnimation();
            doubleAnimation.From = panelHeight;
            doubleAnimation.To = 0;
            doubleAnimation.Duration = new Duration(new TimeSpan(5000000));
            Storyboard.SetTargetName(doubleAnimation, "spPanel");
            Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("(StackPanel.Height)"));
            storyBoard.Children.Add(doubleAnimation);
            beginStoryBoard.Storyboard = storyBoard;
            eventTrigger.Actions.Add(beginStoryBoard);
            return eventTrigger;
        }
        #endregion

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion        
    
        #region IDisposable Members
        private bool disposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    this.ReleaseControls();
                }
                disposed = true;
            }
        }

        private void ReleaseControls()
        {
            try
            {                              
                this.CleanupWPFObjectTopControls((i) =>
                {
                    // unsubscribe the events
                         
                    txtLegend_.Click-= txtLegend__Click;
                    txtLegend.Click -= txtLegend_Click;
                    btnRefresh.Click -= btnRefresh_Click;                    
                    this.Loaded -= this.UserControl_Loaded;
                },
                (c) =>
                {
                });
                LogManager.WriteLog("|=> CGridView objects are released successfully.", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        ~CGridView()
        {   
            Dispose(false);
        }
        #endregion
    }
    #endregion Class
}
