using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BMC.CashDeskOperator.BusinessObjects;
using BMC.Common.ExceptionManagement;
using BMC.Transport;
using System.Windows.Documents;
using System.ComponentModel;
using BMC.Presentation.POS.Views;
using BMC.Presentation.POS.Helper_classes;
using BMC.Common.LogManagement;

namespace BMC.Presentation
{
    /// <summary>
    /// Interaction logic for CCurrentCalls.xaml
    /// </summary>
    public partial class CCurrentCalls : UserControl, IDisposable
    {
        #region Private Variables
        private string strBarPosNos = string.Empty;
        private string strSiteCode = string.Empty;
        private string strBarPosName = string.Empty;
        private string strStartBarPos, strEndBarPos;
        private IFieldService  objCashDesk = FieldServiceBusinessObject.CreateInstance();
        private Int32 iRows;

        private GridViewColumnHeader _CurSortCol = null;
        private SortAdorner _CurAdorner = null;
        #endregion        
        
        #region Constructor
        public CCurrentCalls()
        {
            InitializeComponent();
            //SiteCode = objCashDesk.GetCurrentSiteCode();
            SiteCode = Settings.SiteCode;
            //SplitBarPos();
        }
        #endregion Constructor  

        #region Properties

        public string SiteCode
        {
            get
            {
                return strSiteCode;
            }
            set
            {
                strSiteCode = value;
            }
        }
        public string BarPosNames
        {
            get
            {
                return strBarPosNos;
            }
            set
            {
                strBarPosNos = value;
            }
        }
        public string BarPosName
        {
            get
            {
                return strBarPosName;
            }
            set
            {
                strBarPosName = value;
            }
        }
        #endregion

        #region Events

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            DisplayAll();
        }      

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnPosAll_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnPosAll.IsEnabled = false;
                DisplayAll();
            }
            finally
            {
                btnPosAll.IsEnabled = true;
            }
        }

        private void DisplayAll()
        {
            try
            {
                UserControl.Cursor = Cursors.Wait;
                BarPosName = string.Empty;
                BindServiceCalls("", "");
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                UserControl.Cursor = Cursors.Arrow;
            }
        }

        private void btnPos1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UserControl.Cursor = Cursors.Wait;
                strStartBarPos = btnPos1.Content.ToString().Substring(4, 3).ToString();
                strEndBarPos = btnPos1.Content.ToString().Substring(10, 3).ToString();
                //BarPosName = "011";
                BindServiceCalls(strStartBarPos, strEndBarPos);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                UserControl.Cursor = Cursors.Arrow;
            }
        }

        private void btnPos2_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                UserControl.Cursor = Cursors.Wait; strStartBarPos = btnPos2.Content.ToString().Substring(4, 3).ToString();
                strEndBarPos = btnPos2.Content.ToString().Substring(10, 3).ToString();                
                BindServiceCalls(strStartBarPos, strEndBarPos);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                UserControl.Cursor = Cursors.Arrow;
            }
        }

        private void btnPos3_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UserControl.Cursor = Cursors.Wait;
                strStartBarPos = btnPos3.Content.ToString().Substring(4, 3).ToString();
                strEndBarPos = btnPos3.Content.ToString().Substring(10, 3).ToString();
                //BarPosName = "031";
                BindServiceCalls(strStartBarPos, strEndBarPos);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                UserControl.Cursor = Cursors.Arrow;
            }
        }

        private void btnPos4_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UserControl.Cursor = Cursors.Wait;
                strStartBarPos = btnPos4.Content.ToString().Substring(4, 3).ToString();
                strEndBarPos = btnPos4.Content.ToString().Substring(10, 3).ToString();
                //BarPosName = "041";
                BindServiceCalls(strStartBarPos, strEndBarPos);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                UserControl.Cursor = Cursors.Arrow;
            }
        }

        private void btnPos5_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UserControl.Cursor = Cursors.Wait;
                strStartBarPos = btnPos5.Content.ToString().Substring(4, 3).ToString();
                strEndBarPos = btnPos5.Content.ToString().Substring(10, 3).ToString();
                //BarPosName = "051";
                BindServiceCalls(strStartBarPos, strEndBarPos);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                UserControl.Cursor = Cursors.Arrow;
            }
        }

        public void BindServiceCalls(string strStart, string strEnd)
        {
            DataTable dtCurrentServiceCalls = new DataTable();

            try
            {
                IFieldService objCashDesk = FieldServiceBusinessObject.CreateInstance();

                lstCurrentCalls.DataContext = null;
                dtCurrentServiceCalls = objCashDesk.GetCurrentServiceCalls(SiteCode, strStart, strEnd);

                if (dtCurrentServiceCalls.Rows.Count > 0)
                {
                    //lstCurrentCalls.Visibility = Visibility.Visible;
                    lstCurrentCalls.DataContext = dtCurrentServiceCalls.DefaultView;                    
                }
                else 
                {
                    lstCurrentCalls.DataContext = null;
                    MessageBox.ShowBox("MessageID62");
                    //lstCurrentCalls.Visibility = Visibility.Hidden;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                MessageBox.ShowBox("MessageID63");
            }
            finally
            {
                dtCurrentServiceCalls.Dispose();
            }
        }
        #endregion Events        

        #region Get Bar Position List

        public void SplitBarPos()
        {
            Int32 newI, mod, split, start, end;
            char cZero = '0';

            //iRows = 76;
            start = 1;

            try
            {
                IFieldService objCDO = FieldServiceBusinessObject.CreateInstance();
                DataTable odtPosition;

                odtPosition = objCDO.GetPositionList();

                if (odtPosition != null)
                {
                    iRows = odtPosition.DefaultView.Count;

                    if (iRows >= 50)
                    {
                        mod = iRows % 5;
                        newI = iRows + (10 - mod);
                        split = newI / 5;
                        end = split;



                        btnPos1.Content = "Pos " + start.ToString().PadLeft(3, cZero) + " - " + end.ToString().PadLeft(3, cZero);
                        start = start + split;
                        end = end + split;
                        btnPos2.Content = "Pos " + start.ToString().PadLeft(3, cZero) + " - " + end.ToString().PadLeft(3, cZero);
                        start = start + split;
                        end = end + split;
                        btnPos3.Content = "Pos " + start.ToString().PadLeft(3, cZero) + " - " + end.ToString().PadLeft(3, cZero);
                        start = start + split;
                        end = end + split;
                        btnPos4.Content = "Pos " + start.ToString().PadLeft(3, cZero) + " - " + end.ToString().PadLeft(3, cZero);
                        start = start + split;
                        end = iRows;
                        btnPos5.Content = "Pos " + start.ToString().PadLeft(3, cZero) + " - " + end.ToString().PadLeft(3, cZero);
                    }
                    else if (iRows <= 10)
                    {
                        btnPos1.Content = "Pos 001 - " + iRows.ToString().PadLeft(3, cZero);
                        btnPos2.Visibility = Visibility.Hidden;
                        btnPos3.Visibility = Visibility.Hidden;
                        btnPos4.Visibility = Visibility.Hidden;
                        btnPos5.Visibility = Visibility.Hidden;
                    }
                    else if (iRows <= 20)
                    {
                        btnPos1.Content = "Pos 001 - 010";
                        btnPos2.Content = "Pos 011 - " + iRows.ToString().PadLeft(3, cZero);
                        btnPos3.Visibility = Visibility.Hidden;
                        btnPos4.Visibility = Visibility.Hidden;
                        btnPos5.Visibility = Visibility.Hidden;
                    }
                    else if (iRows <= 30)
                    {
                        btnPos1.Content = "Pos 001 - 010";
                        btnPos2.Content = "Pos 011 - 020";
                        btnPos3.Content = "Pos 021 - " + iRows.ToString().PadLeft(3, cZero);
                        btnPos4.Visibility = Visibility.Hidden;
                        btnPos5.Visibility = Visibility.Hidden;
                    }
                    else if (iRows <= 40)
                    {
                        btnPos1.Content = "Pos 001 - 010";
                        btnPos2.Content = "Pos 011 - 020";
                        btnPos3.Content = "Pos 021 - 030";
                        btnPos4.Content = "Pos 031 - " + iRows.ToString().PadLeft(3, cZero);
                        btnPos5.Visibility = Visibility.Hidden;
                    }
                    else if (iRows <= 50)
                    {
                        btnPos1.Content = "Pos 001 - 010";
                        btnPos2.Content = "Pos 011 - 020";
                        btnPos3.Content = "Pos 021 - 030";
                        btnPos4.Content = "Pos 031 - 040";
                        btnPos5.Content = "Pos 041 - " + iRows.ToString().PadLeft(3, cZero);
                    }
                    else
                    {
                        btnPos1.Visibility = Visibility.Hidden;
                        btnPos2.Visibility = Visibility.Hidden;
                        btnPos3.Visibility = Visibility.Hidden;
                        btnPos4.Visibility = Visibility.Hidden;
                        btnPos5.Visibility = Visibility.Hidden;
                    }
                }
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }

        }

        #endregion Get Bar Position List   
     
        private void SortClick(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader column = sender as GridViewColumnHeader;
            String field = column.Tag as String;
            if (_CurSortCol != null)
            {
                AdornerLayer.GetAdornerLayer(_CurSortCol).Remove(_CurAdorner);
                lstCurrentCalls.Items.SortDescriptions.Clear();
            }
            ListSortDirection newDir = ListSortDirection.Ascending;
            if (_CurSortCol == column && _CurAdorner.Direction == newDir)
                newDir = ListSortDirection.Descending;
            _CurSortCol = column;
            _CurAdorner = new SortAdorner(_CurSortCol, newDir);
            AdornerLayer.GetAdornerLayer(_CurSortCol).Add(_CurAdorner);
            lstCurrentCalls.Items.SortDescriptions.Add(new SortDescription(field, newDir));
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
                        this.UserControl.Loaded -= (this.UserControl_Loaded);
                        ((System.Windows.Controls.GridViewColumnHeader)(GridViewColumnHeader_1)).Click -= (this.SortClick);
                        ((System.Windows.Controls.GridViewColumnHeader)(GridViewColumnHeader_2)).Click -= (this.SortClick);
                        ((System.Windows.Controls.GridViewColumnHeader)(GridViewColumnHeader_3)).Click -= (this.SortClick);
                        ((System.Windows.Controls.GridViewColumnHeader)(GridViewColumnHeader_5)).Click -= (this.SortClick);
                        ((System.Windows.Controls.GridViewColumnHeader)(GridViewColumnHeader_6)).Click -= (this.SortClick);
                        ((System.Windows.Controls.GridViewColumnHeader)(GridViewColumnHeader_7)).Click -= (this.SortClick);
                        this.btnPrint.Click -= (this.btnPrint_Click);
                        this.btnPosAll.Click -= (this.btnPosAll_Click);
                        this.btnPos1.Click -= (this.btnPos1_Click);
                        this.btnPos2.Click -= (this.btnPos2_Click);
                        this.btnPos3.Click -= (this.btnPos3_Click);
                        this.btnPos4.Click -= (this.btnPos4_Click);
                        this.btnPos5.Click -= (this.btnPos5_Click);
                    },
                    (c) =>
                    {
                    });
                    LogManager.WriteLog("|=> CCurrentCalls objects are released successfully.", LogManager.enumLogLevel.Info);
                }
                disposed = true;
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="CCurrentCalls"/> is reclaimed by garbage collection.
        /// </summary>
        ~CCurrentCalls()
        {
            Dispose(false);
        }

        #endregion

    }
}
