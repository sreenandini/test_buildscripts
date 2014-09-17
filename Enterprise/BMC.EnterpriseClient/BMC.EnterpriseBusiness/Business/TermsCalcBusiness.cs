namespace BMC.EnterpriseBusiness.Business
{
    #region Namespace
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BMC.Common.ExceptionManagement;
    using BMC.Common.LogManagement;
    using BMC.EnterpriseBusiness.Entities;
    using BMC.EnterpriseDataAccess;
    using System.Data.Linq;
    #endregion Namespace

    #region enum
    public enum RentMode { Past, Current, Future }
    public enum RentBandMode { Past, Current, Future }
    #endregion enum

    #region Class
    public class TermsCalcBusiness
    {
        #region Constants
        const int TERMS_NONE = 0;
        const int TERMS_LEFT_ON_SITE = 1;
        const int TERMS_SITE = 2;
        const int TERMS_SUPPLIER = 3;
        const int TERMS_SITE_GROUP = 4;
        const int TERMS_INVOICED = 5;
        const int TERMS_SECONDARY_GROUP = 6;
        const int TERMS_CUSTOMS_AND_EXCISE = 7;
        const int TERMS_COLLECTOR_ACCOUNT = 8;
        const int TERMS_CHEQUE = 9;
        const int TERMS_STANDING_ORDER = 10;
        const int TERMS_DIRECT_DEBIT = 11;
        const int TERMS_CASH_BOX = 12;
        const int TERMS_LICENCE_AUTHORITY = 13;
        const int TERMS_RENT = 20;
        const int TERMS_RENT_FULL = 21;
        const int TERMS_RENT_ONLY = 22;
        const int TERMS_RENT_FIXED = 23;
        const int TERMS_COURTESY = 24;
        const int TERMS_FRONT_MONEY = 25;
        const int TERMS_MIN = 26;
        const int TERMS_MAX = 27;
        const int TERMS_RENT_SCHEDULE = 28;
        const int TERMS_SHARE_SCHEDULE = 29;
        const int TERMS_PUBMASTER = 30;
        const int TERMS_PRIMARY_FULL = 41;
        const int TERMS_PRIMARY_TO_ZERO = 42;
        const int TERMS_SECONDARY_REMAINDER = 43;
        const int TERMS_SECONDARY_TO_ZERO = 44;
        const int TERMS_PERCENTAGE = 45;
        const int TERMS_CARRIED_FORWARD = 46;
        const int TERMS_USE_PARTNERS_GUARANTORS = 47;
        const int TERMS_FREQUENCY_PER_WEEK = 0;
        const int TERMS_FREQUENCY_PER_COLLECTION = 1;
        const float SystemVATRate = 0.175f;
        #endregion Constants

        #region Private Members
        EnterpriseDataContext DataContext           =   null;
        TermsResults _termsResults                  =   null;
        InstallationTermsResult _termsInfoResult    =   null;
        TermsInfo _termsInfo                        =   null;
        TermsConfigurationInfo _termsConfigInfo     =   null;
        DateTime _collectionDate;
        float? _collectionGross;
        float? _prize;
        int? _collectionId;
        int? _installationId;
        int? _colDays;
        #endregion Private Members

        #region Constructor

        public TermsCalcBusiness()
        {
            _termsResults = new TermsResults();
            _termsInfo = new TermsInfo();
            _termsConfigInfo = new TermsConfigurationInfo();
            DataContext = EnterpriseDataContextHelper.GetDataContext();
        }

        public TermsCalcBusiness(int? collectionId)
        {
            _collectionId = collectionId;
            _termsResults = new TermsResults();
            _termsInfo = new TermsInfo();
            _termsConfigInfo = new TermsConfigurationInfo();
            DataContext = EnterpriseDataContextHelper.GetDataContext();
        }

        #endregion Constructor

        #region Public Methods

        public bool CalculateTermsForCollectionID()
        {   
            try
            {
                LogManager.WriteLog("Inside CalculateTermsForCollectionID...", LogManager.enumLogLevel.Info);
                LogManager.WriteLog((string.Format("Starting Terms Calculation for CollectionID - [{0}]", _collectionId.ToString())), LogManager.enumLogLevel.Info);

                if (!SetDefaultInfoForTermsCalculation())
                {   
                    LogManager.WriteLog((string.Format("Error Occured in SetDefaultInfoForTermsCalculation. Skipping Terms Calculation for CollectionID - [{0}]", _collectionId.ToString())), LogManager.enumLogLevel.Info);
                    return false;
                }

                if (!SetTermsInfoForTermsCalculation())
                {
                    LogManager.WriteLog((string.Format("Error Occured in SetTermsInfoForTermsCalculation. Skipping Terms Calculation for CollectionID - [{0}]", _collectionId.ToString())), LogManager.enumLogLevel.Info);
                    return false;
                }

                if (!CalculateTermsResultsForCollectionID())                
                {
                    LogManager.WriteLog((string.Format("Error Occured in CalculateTermsResultsForCollectionID. Skipping Terms Calculation for CollectionID - [{0}]", _collectionId.ToString())), LogManager.enumLogLevel.Info);
                    return false;
                }

                if (!UpdateTermsResultsForCollectionID())
                {
                    LogManager.WriteLog((string.Format("Error Occured in UpdateTermsResultsForCollectionID. Skipping Terms Calculation for CollectionID - [{0}]", _collectionId.ToString())), LogManager.enumLogLevel.Info);
                    return false;
                }

                LogManager.WriteLog((string.Format("Terms Calculation completed successfully for CollectionID - [{0}]", _collectionId.ToString())), LogManager.enumLogLevel.Info);
                return true;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog((string.Format("Terms Calculation failed for CollectionID - [{0}]", _collectionId.ToString())), LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
            }

            return false;
        }

        public Tuple<TermsConfigurationInfo, TermsInfo> GetTermsInfoForCollectionID(int installationId, DateTime collectionDate)
        {
            int? period = 0;
            int? returnVal = 0;

            try
            {
                LogManager.WriteLog("Inside GetTermsInfoForCollectionID...", LogManager.enumLogLevel.Info);

                _installationId = installationId;
                _collectionDate = collectionDate;
                
                SetDefaultTermsConfigInfo();

                LogManager.WriteLog((string.Format("Fetching Bar Postition details for InstallationID - [{0}]", _installationId)), LogManager.enumLogLevel.Info);
                BarPositionResult _barPositionResult = DataContext.GetBarPositionInfoForTermsCalculation(_installationId).SingleOrDefault();
                if (_barPositionResult != null)
                {
                    returnVal = ReturnValueFromChangeDates(1, _barPositionResult.Terms_Group_Past_Changeover_Date, 2, _barPositionResult.Terms_Group_Changeover_Date, 3, _collectionDate.ToString());
                    switch (returnVal)
                    {
                        case 1:
                            period = 0; // Past
                            break;
                        case 2:
                            period = 1; // Current
                            break;
                        case 3:
                            period = 2; // Future
                            break;
                        default:
                            period = 1; // Current
                            break;
                    }
                }
                else
                {
                    period = 1; // Current
                }

                LogManager.WriteLog((string.Format("Fetching Terms Info for InstallationID - [{0}]...", _installationId.ToString())), LogManager.enumLogLevel.Info);
                _termsInfoResult = DataContext.GetTermsInfoForInstallation(_installationId, period).SingleOrDefault();

                if (_termsInfoResult != null)
                {
                    _termsConfigInfo.TermsSet = _termsInfoResult.Terms_Group_Name;

                    _termsInfo.BarPosID = _termsInfoResult.Bar_Position_ID;
                    _termsInfo.CollectionDate = _collectionDate.ToString("dd/MMM/yyyy");
                    _termsInfo.InstallationID = _installationId;
                    _termsInfo.MachineClassID = _termsInfoResult.Machine_Class_ID;
                    _termsInfo.MachineID = _termsInfoResult.Machine_ID;
                    _termsInfo.BarPosSetForNoTerms = !_termsInfoResult.Bar_Position_Use_Terms.GetValueOrDefault();
                    _termsInfo.UseSplitRents = _termsInfoResult.Sub_Company_Use_Split_Rents;

                    if (IsValidDateTime(_termsInfoResult.Bar_Position_Last_Collection_Date))
                    {
                        if (((Convert.ToDateTime(_termsInfoResult.Bar_Position_Last_Collection_Date) - _collectionDate).TotalDays <= 100.0)
                            && ((Convert.ToDateTime(_termsInfoResult.Bar_Position_Last_Collection_Date) - _collectionDate).TotalDays >= 0.0))
                        {
                            _termsInfo.CollectionDays = Convert.ToInt32((Convert.ToDateTime(_termsInfoResult.Bar_Position_Last_Collection_Date) - _collectionDate).TotalDays);
                            _termsInfo.LastCollectionDate = Convert.ToDateTime(_termsInfoResult.Bar_Position_Last_Collection_Date).ToString("dd/MMM/yyyy");
                        }
                        else
                        {
                            _termsInfo.CollectionDays = 7;
                            _termsInfo.LastCollectionDate = Convert.ToDateTime(_collectionDate).AddDays(-7).ToString("dd/MMM/yyyy");
                        }
                    }
                    else
                    {
                        _termsInfo.CollectionDays = 7;
                        _termsInfo.LastCollectionDate = Convert.ToDateTime(_collectionDate).AddDays(-7).ToString("dd/MMM/yyyy");
                    }

                    if (IsValidDateTime(_termsInfoResult.Bar_Position_Collection_Rent_Paid_Until))
                    {
                        if (((Convert.ToDateTime(_termsInfoResult.Bar_Position_Collection_Rent_Paid_Until) - _collectionDate).TotalDays <= 100.0)
                            && ((Convert.ToDateTime(_termsInfoResult.Bar_Position_Collection_Rent_Paid_Until) - _collectionDate).TotalDays >= 0.0))
                        {
                            _termsInfo.RentPaidUntil = Convert.ToDateTime(_termsInfoResult.Bar_Position_Collection_Rent_Paid_Until).ToString("dd/MMM/yyyy");
                        }
                        else
                        {
                            if (IsValidDateTime(_termsInfoResult.Installation_Start_Date))
                                _termsInfo.RentPaidUntil = Convert.ToDateTime(_termsInfoResult.Installation_Start_Date).ToString("dd/MMM/yyyy");
                            else
                                _termsInfo.RentPaidUntil = Convert.ToDateTime(_termsInfo.LastCollectionDate).ToString("dd/MMM/yyyy");
                        }
                    }
                    else
                    {
                        if (IsValidDateTime(_termsInfoResult.Installation_Start_Date))
                            _termsInfo.RentPaidUntil = Convert.ToDateTime(_termsInfoResult.Installation_Start_Date).ToString("dd/MMM/yyyy");
                        else
                            _termsInfo.RentPaidUntil = Convert.ToDateTime(_termsInfo.LastCollectionDate).ToString("dd/MMM/yyyy");
                    }

                    SetSupplierTermsInfo();
                    SetSiteTermsInfo();
                    SetGroupTermsInfo();
                    SetSecGroupTermsInfo();
                    SetVATTermsInfo();
                    SetLicenseTermsInfo();
                    SetPrizeTermsInfo();
                    SetConsultancyTermsInfo();
                    SetRoyaltyTermsInfo();
                    SetOtherTermsInfo();
                    SetLondonRentTermsInfo();

                    _termsInfo.TermsValid = true;

                    return Tuple.Create<TermsConfigurationInfo, TermsInfo>(_termsConfigInfo, _termsInfo);
                }
                else
                {
                    LogManager.WriteLog((string.Format("Terms Info for InstallationID - [{0}] is Empty. Hence returning...", _installationId.ToString())), LogManager.enumLogLevel.Info);                    
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return null;
        }

        #endregion Public Methods

        #region Private Methods

        #region SetDefaultInfoForTermsCalculation
        private bool SetDefaultInfoForTermsCalculation()
        {
            float? _cashBox = 0;            
            float? _sundriesSupported = 0;
            float? _sundriesUnSupported = 0;
            float? _supplierFloat = 0;
            float? _licenceFloat = 0;
            long? _downDays = 0;            
            float? _cashRefills = 0;
            CashTakeResult _cashTakeResult = null;
            CollectionInfoResult _CollectionInfoResult = null;

            try
            {
                LogManager.WriteLog("Inside SetDefaultInfoForTermsCalculation...", LogManager.enumLogLevel.Info);

                LogManager.WriteLog((string.Format("Fetching Collection Gross Value for CollectionID - [{0}]", _collectionId.ToString())), LogManager.enumLogLevel.Info);
                 _cashTakeResult = DataContext.GetCashTake(_collectionId).SingleOrDefault();
                _collectionGross = (float)_cashTakeResult.Cash_Take;

                LogManager.WriteLog((string.Format("Fetching default Collection details for CollectionID - [{0}]", _collectionId.ToString())), LogManager.enumLogLevel.Info);
                _CollectionInfoResult = DataContext.GetCollectionInfoForTermsCalculation(_collectionId).SingleOrDefault();
                _installationId = _CollectionInfoResult.Installation_ID;
                _cashBox = _CollectionInfoResult.CashCollected;
                _collectionDate = IsValidDateTime(_CollectionInfoResult.Collection_Date) ? Convert.ToDateTime(_CollectionInfoResult.Collection_Date) : System.DateTime.Now; ;
                _colDays = _CollectionInfoResult.Collection_Days;
                _sundriesSupported = _CollectionInfoResult.Collection_Sundries;
                _sundriesUnSupported = _CollectionInfoResult.Collection_Sundries_Unsupported;
                _supplierFloat = _CollectionInfoResult.Collection_Supplier_Float_Recovered;
                _licenceFloat = _CollectionInfoResult.Collection_Non_Supplier_Float_Recovered + _CollectionInfoResult.Collection_Treasury_Defloat;
                _downDays = _CollectionInfoResult.Down_Days;
                _prize = _CollectionInfoResult.Collection_Prize_Value;
                _cashRefills = _CollectionInfoResult.CashRefills;

                return true;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return false;
        }
        #endregion SetDefaultInfoForTermsCalculation

        #region SetTermsInfoForTermsCalculation
        private bool SetTermsInfoForTermsCalculation()
        {
            int? period = 0;
            int? returnVal = 0;
            string previousCollectionDate = string.Empty; 

            try
            {
                LogManager.WriteLog("Inside SetTermsInfoForTermsCalculation...", LogManager.enumLogLevel.Info);

                previousCollectionDate = _collectionDate.AddDays(0 - _colDays.GetValueOrDefault()).ToString();

                SetDefaultTermsConfigInfo();

                LogManager.WriteLog((string.Format("Fetching Bar Postition details for InstallationID - [{0}], CollectionID - [{1}]...", _installationId, _collectionId.ToString())), LogManager.enumLogLevel.Info);
                BarPositionResult _barPositionResult = DataContext.GetBarPositionInfoForTermsCalculation(_installationId).SingleOrDefault();
                if (_barPositionResult != null)
                {
                    returnVal = ReturnValueFromChangeDates(1, _barPositionResult.Terms_Group_Past_Changeover_Date, 2, _barPositionResult.Terms_Group_Changeover_Date, 3, _collectionDate.ToString());
                    switch (returnVal)
                    {
                        case 1:
                            period = 0; // Past
                            break;
                        case 2:
                            period = 1; // Current
                            break;
                        case 3:
                            period = 2; // Future
                            break;
                        default:
                            period = 1; // Current
                            break;
                    }
                }
                else
                {
                    period = 1; // Current
                }

                LogManager.WriteLog((string.Format("Fetching Terms Info for InstallationID - [{0}], CollectionID - [{1}], Period - [{2}]...", _installationId.ToString(), _collectionId.ToString(), period.ToString())), LogManager.enumLogLevel.Info);
                _termsInfoResult = DataContext.GetTermsInfoForInstallation(_installationId, period).SingleOrDefault();

                if (_termsInfoResult != null)
                {   
                    _termsConfigInfo.TermsSet = _termsInfoResult.Terms_Group_Name;

                    _termsInfo.BarPosID = _termsInfoResult.Bar_Position_ID;
                    _termsInfo.CollectionDate = _collectionDate.ToString("dd/MMM/yyyy");
                    _termsInfo.InstallationID = _installationId;
                    _termsInfo.MachineClassID = _termsInfoResult.Machine_Class_ID;
                    _termsInfo.MachineID = _termsInfoResult.Machine_ID;
                    _termsInfo.BarPosSetForNoTerms = !_termsInfoResult.Bar_Position_Use_Terms.GetValueOrDefault();
                    _termsInfo.UseSplitRents = _termsInfoResult.Sub_Company_Use_Split_Rents;

                    if (IsValidDateTime(_termsInfoResult.Bar_Position_Last_Collection_Date))
                    {
                        if (((Convert.ToDateTime(_termsInfoResult.Bar_Position_Last_Collection_Date) - _collectionDate).TotalDays <= 100.0)
                            && ((Convert.ToDateTime(_termsInfoResult.Bar_Position_Last_Collection_Date) - _collectionDate).TotalDays >= 0.0))
                        {
                            _termsInfo.CollectionDays = Convert.ToInt32((Convert.ToDateTime(_termsInfoResult.Bar_Position_Last_Collection_Date) - _collectionDate).TotalDays);
                            _termsInfo.LastCollectionDate = Convert.ToDateTime(_termsInfoResult.Bar_Position_Last_Collection_Date).ToString("dd/MMM/yyyy");
                        }
                        else
                        {
                            _termsInfo.CollectionDays = 7;
                            _termsInfo.LastCollectionDate = Convert.ToDateTime(_collectionDate).AddDays(-7).ToString("dd/MMM/yyyy");
                        }
                    }
                    else
                    {
                        _termsInfo.CollectionDays = 7;
                        _termsInfo.LastCollectionDate = Convert.ToDateTime(_collectionDate).AddDays(-7).ToString("dd/MMM/yyyy");
                    }

                    if (IsValidDateTime(_termsInfoResult.Bar_Position_Collection_Rent_Paid_Until))
                    {
                        if (((Convert.ToDateTime(_termsInfoResult.Bar_Position_Collection_Rent_Paid_Until) - _collectionDate).TotalDays <= 100.0)
                            && ((Convert.ToDateTime(_termsInfoResult.Bar_Position_Collection_Rent_Paid_Until) - _collectionDate).TotalDays >= 0.0))
                        {
                            _termsInfo.RentPaidUntil = Convert.ToDateTime(_termsInfoResult.Bar_Position_Collection_Rent_Paid_Until).ToString("dd/MMM/yyyy");
                        }
                        else
                        {
                            if (IsValidDateTime(_termsInfoResult.Installation_Start_Date))
                                _termsInfo.RentPaidUntil = Convert.ToDateTime(_termsInfoResult.Installation_Start_Date).ToString("dd/MMM/yyyy");
                            else
                                _termsInfo.RentPaidUntil = Convert.ToDateTime(_termsInfo.LastCollectionDate).ToString("dd/MMM/yyyy");
                        }
                    }
                    else
                    {
                        if (IsValidDateTime(_termsInfoResult.Installation_Start_Date))
                            _termsInfo.RentPaidUntil = Convert.ToDateTime(_termsInfoResult.Installation_Start_Date).ToString("dd/MMM/yyyy");
                        else
                            _termsInfo.RentPaidUntil = Convert.ToDateTime(_termsInfo.LastCollectionDate).ToString("dd/MMM/yyyy");
                    }

                    if (IsValidDateTime(previousCollectionDate))
                    {
                        _termsInfo.LastCollectionDate = Convert.ToDateTime(previousCollectionDate).ToString("dd/MMM/yyyy");
                        _termsInfo.RentPaidUntil = Convert.ToDateTime(previousCollectionDate).ToString("dd/MMM/yyyy");
                        _termsInfo.CollectionDays = Convert.ToInt32((_collectionDate - Convert.ToDateTime(_termsInfo.LastCollectionDate)).TotalDays);
                    }

                    SetSupplierTermsInfo();
                    SetSiteTermsInfo();
                    SetGroupTermsInfo();
                    SetSecGroupTermsInfo();
                    SetVATTermsInfo();
                    SetLicenseTermsInfo();
                    SetPrizeTermsInfo();
                    SetConsultancyTermsInfo();
                    SetRoyaltyTermsInfo();
                    SetOtherTermsInfo();
                    SetLondonRentTermsInfo();

                    _termsInfo.TermsValid = true;
                }
                else
                {
                    LogManager.WriteLog((string.Format("Terms Info for InstallationID - [{0}], CollectionID - [{1}], Period - {[2]} is Empty. Hence returning...", _installationId, _collectionId.ToString(), period.ToString())), LogManager.enumLogLevel.Info);
                    return false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return true;
        }

        private void SetDefaultTermsConfigInfo()
        {
            try
            {
                LogManager.WriteLog("Inside SetDefaultTermsConfigInfo...", LogManager.enumLogLevel.Info);

                _termsConfigInfo.BarPosOverrideLicence = false;
                _termsConfigInfo.BarPosOverrideRent = false;
                _termsConfigInfo.BarPosOverrideRentSchedule = false;
                _termsConfigInfo.BarPosOverrideShares = false;
                _termsConfigInfo.BarPosOverrideShareSchedule = false;
                _termsConfigInfo.RentSchedule = string.Empty;
                _termsConfigInfo.ShareSchedule = string.Empty;
                _termsConfigInfo.TermsSet = string.Empty;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void SetSupplierTermsInfo()
        {
            ShareScheduleInfo _shareScheduleInfo = null;
            RentInfo _rentInfo = null;
            List<AccessoryInstallationResult> _lstAccessoryInstallationInfo = null;                        
            string rentStartDate = string.Empty;
            string rentScheduleName = string.Empty;
            string shareScheduleName = string.Empty;
            string previousDate = string.Empty;
            string futureDate = string.Empty;

            try
            {
                LogManager.WriteLog("Inside SetSupplierTermsInfo...", LogManager.enumLogLevel.Info);

                if (_termsInfoResult.Terms_Profile_Partners_Supplier_Use.GetValueOrDefault())
                {   
                    _termsInfo.SupplierUse = _termsInfoResult.Terms_Profile_Partners_Supplier_Index;
                    _termsInfo.SupplierCashDestination = _termsInfoResult.Terms_Profile_Partners_Supplier_Cash_Destination <= 0 ? TERMS_SUPPLIER : _termsInfoResult.Terms_Profile_Partners_Supplier_Cash_Destination;
                    _termsInfo.SupplierDeferredDest = _termsInfoResult.Terms_Profile_Partners_Supplier_Deferred_Remittance;
                    _termsInfo.SupplierType = _termsInfoResult.Terms_Profile_Partners_Supplier_Type;

                    // Calculate Rent
                    if (_termsInfoResult.Bar_Position_Override_Rent_From_Schedule_To_Rent.GetValueOrDefault())
                    {   
                        if ((_collectionDate - Convert.ToDateTime(_termsInfoResult.Bar_Position_Override_Rent_From_Schedule_To_Rent_Date)).TotalDays > 0)
                        {
                            //use schedule
                            _termsConfigInfo.BarPosOverrideRentSchedule = true;
                            //Use Bar Pos Override
                            _termsInfo.SupplierType = TERMS_RENT_SCHEDULE;

                            // SupplierValueAfter
                            _rentInfo = RentCostCurrentWithStartDate(_termsInfoResult.Bar_Position_Rent_Schedule_ID_From, _collectionDate, ref rentStartDate, ref rentScheduleName);
                            _termsInfo.SupplierValueAfter = _rentInfo.Rent + _rentInfo.SuppCharge;
                            _termsInfo.SupplierSupplementalChargeAfter = _rentInfo.SuppChargeEndPeriod;

                            if (IsValidDateTime(rentStartDate))
                            {
                                // SupplierValue
                                _termsInfo.SupplierValueAfterChangeDate = rentStartDate;
                                _rentInfo = RentCostCurrentWithStartDate(_termsInfoResult.Bar_Position_Rent_Schedule_ID_From, Convert.ToDateTime(rentStartDate).AddDays(-1), ref rentStartDate, ref rentScheduleName);
                                _termsInfo.SupplierValue = _rentInfo.Rent + _rentInfo.SuppCharge;
                                _termsInfo.SupplierSupplementalCharge = _rentInfo.SuppChargeEndPeriod;

                                if (IsValidDateTime(rentStartDate))
                                {
                                    // SupplierValueBefore
                                    _termsInfo.SupplierValueBeforeChangeDate = rentStartDate;
                                    _rentInfo = RentCostCurrentWithStartDate(_termsInfoResult.Bar_Position_Rent_Schedule_ID_From, Convert.ToDateTime(rentStartDate).AddDays(-1), ref rentStartDate, ref rentScheduleName);
                                    _termsInfo.SupplierValueBefore = _rentInfo.Rent + _rentInfo.SuppCharge;
                                    _termsInfo.SupplierSupplementalChargeBefore = _rentInfo.SuppChargeEndPeriod;
                                }
                                else
                                {
                                    _termsInfo.SupplierValueBefore = _termsInfo.SupplierValue + _rentInfo.SuppCharge;
                                    _termsInfo.SupplierSupplementalChargeBefore = _termsInfo.SupplierSupplementalCharge;
                                }
                            }
                            else
                            {
                                _termsInfo.SupplierValue = _termsInfo.SupplierValueAfter;
                                _termsInfo.SupplierValueBefore = _termsInfo.SupplierValueAfter;

                                _termsInfo.SupplierSupplementalCharge = _termsInfo.SupplierSupplementalChargeAfter;
                                _termsInfo.SupplierSupplementalChargeBefore = _termsInfo.SupplierSupplementalChargeAfter;
                            }
                        }
                        else
                        {
                            //use rent
                            _termsConfigInfo.BarPosOverrideRent = true;

                            if (IsValidDateTime(_termsInfoResult.Bar_Position_Rent_Past_Date))
                                previousDate = Convert.ToDateTime(_termsInfoResult.Bar_Position_Rent_Past_Date).ToString("dd/MMM/yyyy");
                            else
                                previousDate = "01/01/1950";

                            if (IsValidDateTime(_termsInfoResult.Bar_Position_Rent_Future_Date))
                                futureDate = Convert.ToDateTime(_termsInfoResult.Bar_Position_Rent_Future_Date).ToString("dd/MMM/yyyy");
                            else
                                futureDate = System.DateTime.Now.AddYears(50).ToString("dd/MMM/yyyy");
                            
                            _termsInfo.SupplierValueBefore = _termsInfoResult.Bar_Position_Rent_Previous;
                            _termsInfo.SupplierValueBeforeChangeDate = previousDate;
                            _termsInfo.SupplierValue = _termsInfoResult.Bar_Position_Rent;
                            _termsInfo.SupplierValueAfterChangeDate = futureDate;
                            _termsInfo.SupplierValueAfter = _termsInfoResult.Bar_Position_Rent_Future;
                        }
                    }
                    else if (_termsInfoResult.Bar_Position_Override_Rent_From_Rent_To_Schedule.GetValueOrDefault())
                    {
                        if ((_collectionDate - Convert.ToDateTime(_termsInfoResult.Bar_Position_Override_Rent_From_Rent_To_Schedule_Date)).TotalDays > 0)
                        {
                            //use rent
                            _termsConfigInfo.BarPosOverrideRent = true;

                            if (IsValidDateTime(_termsInfoResult.Bar_Position_Rent_Past_Date))
                                previousDate = Convert.ToDateTime(_termsInfoResult.Bar_Position_Rent_Past_Date).ToString("dd/MMM/yyyy");
                            else
                                previousDate = "01/01/1950";

                            if (IsValidDateTime(_termsInfoResult.Bar_Position_Rent_Future_Date))
                                futureDate = Convert.ToDateTime(_termsInfoResult.Bar_Position_Rent_Future_Date).ToString("dd/MMM/yyyy");
                            else
                                futureDate = System.DateTime.Now.AddYears(50).ToString("dd/MMM/yyyy");

                            _termsInfo.SupplierValueBefore = _termsInfoResult.Bar_Position_Rent_Previous;
                            _termsInfo.SupplierValueBeforeChangeDate = previousDate;
                            _termsInfo.SupplierValue = _termsInfoResult.Bar_Position_Rent;
                            _termsInfo.SupplierValueAfterChangeDate = futureDate;
                            _termsInfo.SupplierValueAfter = _termsInfoResult.Bar_Position_Rent_Future;  
                        }
                        else
                        {
                            //use schedule
                            _termsConfigInfo.BarPosOverrideRentSchedule = true;
                            //Use Bar Pos Override
                            _termsInfo.SupplierType = TERMS_RENT_SCHEDULE;

                            if (IsValidDateTime(rentStartDate))
                            {
                                // SupplierValue
                                _termsInfo.SupplierValueAfterChangeDate = rentStartDate;
                                _rentInfo = RentCostCurrentWithStartDate(_termsInfoResult.Bar_Position_Rent_Schedule_ID_From, Convert.ToDateTime(rentStartDate).AddDays(-1), ref rentStartDate, ref rentScheduleName);
                                _termsInfo.SupplierValue = _rentInfo.Rent + _rentInfo.SuppCharge;
                                _termsInfo.SupplierSupplementalCharge = _rentInfo.SuppChargeEndPeriod;

                                if (IsValidDateTime(rentStartDate))
                                {
                                    // SupplierValueBefore
                                    _termsInfo.SupplierValueBeforeChangeDate = rentStartDate;
                                    _rentInfo = RentCostCurrentWithStartDate(_termsInfoResult.Bar_Position_Rent_Schedule_ID_From, Convert.ToDateTime(rentStartDate).AddDays(-1), ref rentStartDate, ref rentScheduleName);
                                    _termsInfo.SupplierValueBefore = _rentInfo.Rent + _rentInfo.SuppCharge;
                                    _termsInfo.SupplierSupplementalChargeBefore = _rentInfo.SuppChargeEndPeriod;
                                }
                                else
                                {
                                    _termsInfo.SupplierValueBefore = _termsInfo.SupplierValue + _rentInfo.SuppCharge;
                                    _termsInfo.SupplierSupplementalChargeBefore = _termsInfo.SupplierSupplementalCharge;
                                }
                            }
                            else
                            {
                                _termsInfo.SupplierValue = _termsInfo.SupplierValueAfter;
                                _termsInfo.SupplierValueBefore = _termsInfo.SupplierValueAfter;

                                _termsInfo.SupplierSupplementalCharge = _termsInfo.SupplierSupplementalChargeAfter;
                                _termsInfo.SupplierSupplementalChargeBefore = _termsInfo.SupplierSupplementalChargeAfter;
                            }                            
                        }
                    }
                    else if (_termsInfoResult.Bar_Position_Override_Rent.GetValueOrDefault())
                    {   
                        //use rent
                        _termsConfigInfo.BarPosOverrideRent = true;

                        if (IsValidDateTime(_termsInfoResult.Bar_Position_Rent_Past_Date))
                            previousDate = Convert.ToDateTime(_termsInfoResult.Bar_Position_Rent_Past_Date).ToString("dd/MMM/yyyy");
                        else
                            previousDate = "01/01/1950";

                        if (IsValidDateTime(_termsInfoResult.Bar_Position_Rent_Future_Date))
                            futureDate = Convert.ToDateTime(_termsInfoResult.Bar_Position_Rent_Future_Date).ToString("dd/MMM/yyyy");
                        else
                            futureDate = System.DateTime.Now.AddYears(50).ToString("dd/MMM/yyyy");

                        _termsInfo.SupplierValueBefore = _termsInfoResult.Bar_Position_Rent_Previous;
                        _termsInfo.SupplierValueBeforeChangeDate = previousDate;
                        _termsInfo.SupplierValue = _termsInfoResult.Bar_Position_Rent;
                        _termsInfo.SupplierValueAfterChangeDate = futureDate;
                        _termsInfo.SupplierValueAfter = _termsInfoResult.Bar_Position_Rent_Future; 
                    }
                    else if (_termsInfoResult.Terms_Profile_Partners_Supplier_Type.GetValueOrDefault() == TERMS_RENT_SCHEDULE
                        || _termsInfoResult.Terms_Profile_Partners_Supplier_Type.GetValueOrDefault() == TERMS_PUBMASTER
                        || _termsInfoResult.Bar_Position_Override_Rent_Schedule.GetValueOrDefault() == true)
                    {
                        if (_termsInfoResult.Bar_Position_Override_Rent_Schedule.GetValueOrDefault() == true)
                        {
                            //use schedule
                            _termsConfigInfo.BarPosOverrideRentSchedule = true;
                            //Use Bar Pos Override
                            _termsInfo.SupplierType = TERMS_RENT_SCHEDULE;

                            // SupplierValueAfter
                            _rentInfo = RentCostCurrentWithStartDate(_termsInfoResult.Bar_Position_Rent_Schedule_ID_From, _collectionDate, ref rentStartDate, ref rentScheduleName);
                            _termsInfo.SupplierValueAfter = _rentInfo.Rent + _rentInfo.SuppCharge;
                            _termsInfo.SupplierSupplementalChargeAfter = _rentInfo.SuppChargeEndPeriod;

                            if (IsValidDateTime(rentStartDate))
                            {
                                // SupplierValue
                                _termsInfo.SupplierValueAfterChangeDate = rentStartDate;
                                _rentInfo = RentCostCurrentWithStartDate(_termsInfoResult.Bar_Position_Rent_Schedule_ID_From, Convert.ToDateTime(rentStartDate).AddDays(-1), ref rentStartDate, ref rentScheduleName);
                                _termsInfo.SupplierValue = _rentInfo.Rent + _rentInfo.SuppCharge;
                                _termsInfo.SupplierSupplementalCharge = _rentInfo.SuppChargeEndPeriod;

                                if (IsValidDateTime(rentStartDate))
                                {
                                    // SupplierValueBefore
                                    _termsInfo.SupplierValueBeforeChangeDate = rentStartDate;
                                    _rentInfo = RentCostCurrentWithStartDate(_termsInfoResult.Bar_Position_Rent_Schedule_ID_From, Convert.ToDateTime(rentStartDate).AddDays(-1), ref rentStartDate, ref rentScheduleName);
                                    _termsInfo.SupplierValueBefore = _rentInfo.Rent + _rentInfo.SuppCharge;
                                    _termsInfo.SupplierSupplementalChargeBefore = _rentInfo.SuppChargeEndPeriod;
                                }
                                else
                                {
                                    _termsInfo.SupplierValueBefore = _termsInfo.SupplierValue + _rentInfo.SuppCharge;
                                    _termsInfo.SupplierSupplementalChargeBefore = _termsInfo.SupplierSupplementalCharge;
                                }
                            }
                            else
                            {
                                _termsInfo.SupplierValue = _termsInfo.SupplierValueAfter;
                                _termsInfo.SupplierValueBefore = _termsInfo.SupplierValueAfter;

                                _termsInfo.SupplierSupplementalCharge = _termsInfo.SupplierSupplementalChargeAfter;
                                _termsInfo.SupplierSupplementalChargeBefore = _termsInfo.SupplierSupplementalChargeAfter;
                            }
                        }
                        else  //Use standard terms info
                        {
                            // SupplierValueAfter
                            _rentInfo = RentCostCurrentWithStartDate(_termsInfoResult.Bar_Position_Rent_Schedule_ID_From, _collectionDate, ref rentStartDate, ref rentScheduleName);
                            _termsInfo.SupplierValueAfter = _rentInfo.Rent + _rentInfo.SuppCharge;
                            _termsInfo.SupplierSupplementalChargeAfter = _rentInfo.SuppChargeEndPeriod;

                            if (IsValidDateTime(rentStartDate))
                            {
                                // SupplierValue
                                _termsInfo.SupplierValueAfterChangeDate = rentStartDate;
                                _rentInfo = RentCostCurrentWithStartDate(_termsInfoResult.Bar_Position_Rent_Schedule_ID_From, Convert.ToDateTime(rentStartDate).AddDays(-1), ref rentStartDate, ref rentScheduleName);
                                _termsInfo.SupplierValue = _rentInfo.Rent + _rentInfo.SuppCharge;
                                _termsInfo.SupplierSupplementalCharge = _rentInfo.SuppChargeEndPeriod;

                                if (IsValidDateTime(rentStartDate))
                                {
                                    // SupplierValueBefore
                                    _termsInfo.SupplierValueBeforeChangeDate = rentStartDate;
                                    _rentInfo = RentCostCurrentWithStartDate(_termsInfoResult.Bar_Position_Rent_Schedule_ID_From, Convert.ToDateTime(rentStartDate).AddDays(-1), ref rentStartDate, ref rentScheduleName);
                                    _termsInfo.SupplierValueBefore = _rentInfo.Rent + _rentInfo.SuppCharge;
                                    _termsInfo.SupplierSupplementalChargeBefore = _rentInfo.SuppChargeEndPeriod;
                                }
                                else
                                {
                                    _termsInfo.SupplierValueBefore = _termsInfo.SupplierValue;
                                    _termsInfo.SupplierSupplementalChargeBefore = _termsInfo.SupplierSupplementalCharge;
                                }
                            }
                            else
                            {
                                _termsInfo.SupplierValue = _termsInfo.SupplierValueAfter;
                                _termsInfo.SupplierValueBefore = _termsInfo.SupplierValueAfter;

                                _termsInfo.SupplierSupplementalCharge = _termsInfo.SupplierSupplementalChargeAfter;
                                _termsInfo.SupplierSupplementalChargeBefore = _termsInfo.SupplierSupplementalChargeAfter;
                            }                            
                        }
                    }
                    else if (_termsInfoResult.Terms_Profile_Partners_Supplier_Type == TERMS_SHARE_SCHEDULE || _termsInfoResult.Bar_Position_Override_Share_Schedule == true)
                    {
                        if (_termsInfoResult.Bar_Position_Override_Share_Schedule.GetValueOrDefault() == true)
                        {
                            _termsConfigInfo.BarPosOverrideShareSchedule = true;
                            _termsInfo.SupplierType = TERMS_SHARE_SCHEDULE;

                            _shareScheduleInfo = GetShareScheduleInfo(_termsInfoResult.Bar_Position_Share_Schedule_ID, ref shareScheduleName);
                        }
                        else
                        {
                            _shareScheduleInfo = GetShareScheduleInfo(_termsInfoResult.Terms_Profile_Partners_Supplier_Share_Schedule, ref shareScheduleName);
                        }

                        _termsInfo.SupplierShare = _shareScheduleInfo.SupplierShare;
                        _termsInfo.SiteShare = _shareScheduleInfo.SiteShare;
                        _termsInfo.GroupShare = _shareScheduleInfo.CompanyShare;
                        _termsInfo.SecGroupShare = _shareScheduleInfo.SecCompanyShare;

                        _termsInfo.SupplierValue = _shareScheduleInfo.SupplierRent;
                        _termsInfo.SupplierValueAfter = _shareScheduleInfo.SupplierRent;
                        _termsInfo.SupplierValueBefore = _shareScheduleInfo.SupplierRent;
                        _termsInfo.SupplierValueAfterChangeDate = System.DateTime.Now.AddYears(1).ToString("dd/MMM/yyyy");
                        _termsInfo.SupplierValueBeforeChangeDate = System.DateTime.Now.AddYears(-1).ToString("dd/MMM/yyyy");

                        _termsInfo.SupplierValueGuaranteed = _shareScheduleInfo.SupplierRentGuaranteed;
                        _termsInfo.SupplierShareGuaranteed = _shareScheduleInfo.SupplierShareGuaranteed;
                        _termsInfo.SiteShareGuaranteed = _shareScheduleInfo.SiteShareGuaranteed;
                        _termsInfo.GroupShareGuaranteed = _shareScheduleInfo.CompanyShareGuaranteed;
                        _termsInfo.SecGroupShareGuaranteed = _shareScheduleInfo.SecCompanyShareGuaranteed;
                    }
                    else
                    {
                        _termsConfigInfo.RentSchedule = "Terms";
                        _termsInfo.SupplierValue = _termsInfoResult.Terms_Profile_Partners_Supplier_Value;
                        _termsInfo.SupplierValueAfter = _termsInfoResult.Terms_Profile_Partners_Supplier_Value;
                        _termsInfo.SupplierValueBefore = _termsInfoResult.Terms_Profile_Partners_Supplier_Value;
                    }

                    _termsInfo.SupplierValueGuaranteed = _termsInfoResult.Terms_Profile_Partners_Supplier_Value_Guaranteed;

                    // Calc Shares
                    if (_termsInfoResult.Bar_Position_Override_Shares.GetValueOrDefault())
                    {
                        _termsConfigInfo.BarPosOverrideShares = true;

                        if (IsValidDateTime(_termsInfoResult.Bar_Position_Share_Past_Date))
                            previousDate = Convert.ToDateTime(_termsInfoResult.Bar_Position_Share_Past_Date).ToString("dd/MMM/yyyy");
                        else
                            previousDate = "01/01/1950";

                        if (IsValidDateTime(_termsInfoResult.Bar_Position_Share_Future_Date))
                            futureDate = Convert.ToDateTime(_termsInfoResult.Bar_Position_Share_Future_Date).ToString("dd/MMM/yyyy");
                        else
                            futureDate = System.DateTime.Now.AddYears(50).ToString("dd/MMM/yyyy");

                        if ((Convert.ToDateTime(previousDate) - _collectionDate).TotalDays < 0.0)
                            _termsInfo.SupplierShare = _termsInfoResult.Bar_Position_Supplier_Share_Previous;
                        else if ((Convert.ToDateTime(futureDate) - _collectionDate).TotalDays >= 0.0)
                            _termsInfo.SupplierShare = _termsInfoResult.Bar_Position_Supplier_Share_Future;
                        else
                            _termsInfo.SupplierShare = _termsInfoResult.Bar_Position_Supplier_Share;
                    }
                    else
                    {
                        if (_termsInfoResult.Terms_Profile_Partners_Supplier_Type != TERMS_SHARE_SCHEDULE)
                            _termsInfo.SupplierShare = _termsInfoResult.Terms_Profile_Partners_Supplier_Share;
                    }

                    _termsInfo.SupplierShareGuaranteed = _termsInfoResult.Terms_Profile_Partners_Supplier_Share_Guaranteed;
                    _termsInfo.SupplierGuarantor = _termsInfoResult.Terms_Profile_Partners_Supplier_Guarantor;
                    _termsInfo.SupplierGuarantorPercentage = _termsInfoResult.Terms_Profile_Partners_Supplier_Guarantor_Percentage;

                    // If there are any ancillary items to charge for, add them on
                    _lstAccessoryInstallationInfo = DataContext.GetAccessoryInstallationInfoForTermsCalculation(_termsInfoResult.Bar_Position_ID).ToList<AccessoryInstallationResult>();
                    foreach (AccessoryInstallationResult accessoryInstallationInfo in _lstAccessoryInstallationInfo)
                    {
                        _termsInfo.SupplierValue = _termsInfo.SupplierValue + accessoryInstallationInfo.Accessory_Installation_Charge;
                        _termsInfo.SupplierValueBefore = _termsInfo.SupplierValueBefore + accessoryInstallationInfo.Accessory_Installation_Charge;
                        _termsInfo.SupplierValueAfter = _termsInfo.SupplierValueAfter + accessoryInstallationInfo.Accessory_Installation_Charge;
                    }

                    _termsConfigInfo.RentSchedule = !string.IsNullOrEmpty(rentScheduleName) ? rentScheduleName : _termsConfigInfo.RentSchedule;
                    _termsConfigInfo.ShareSchedule = !string.IsNullOrEmpty(shareScheduleName) ? shareScheduleName : _termsConfigInfo.ShareSchedule;                    
                }
                else //if (_termsInfoResult.Terms_Profile_Partners_Supplier_Use.GetValueOrDefault())
                {
                    _termsInfo.SupplierUse = 0;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void SetSiteTermsInfo()
        {
            string previousDate = string.Empty;
            string futureDate = string.Empty;

            try
            {
                LogManager.WriteLog("Inside SetSiteTermsInfo...", LogManager.enumLogLevel.Info);
                if (_termsInfoResult.Terms_Profile_Partners_Site_Use.GetValueOrDefault())
                {
                    _termsInfo.SiteUse = _termsInfoResult.Terms_Profile_Partners_Site_Index;
                    _termsInfo.SiteCashDestination = _termsInfoResult.Terms_Profile_Partners_Site_Cash_Destination <= 0 ? TERMS_LEFT_ON_SITE : _termsInfoResult.Terms_Profile_Partners_Site_Cash_Destination;
                    _termsInfo.SiteDeferredDest = _termsInfoResult.Terms_Profile_Partners_Site_Deferred_Remittance;
                    _termsInfo.SiteType = _termsInfoResult.Terms_Profile_Partners_Site_Type;
                    _termsInfo.SiteValue = _termsInfoResult.Terms_Profile_Partners_Site_Value;
                    _termsInfo.SiteValueGuaranteed = _termsInfoResult.Terms_Profile_Partners_Site_Value_Guaranteed;

                    // Calc Shares
                    if (_termsInfoResult.Bar_Position_Override_Shares.GetValueOrDefault())
                    {
                        if (IsValidDateTime(_termsInfoResult.Bar_Position_Share_Past_Date))
                            previousDate = Convert.ToDateTime(_termsInfoResult.Bar_Position_Share_Past_Date).ToString("dd/MMM/yyyy");
                        else
                            previousDate = "01/01/1950";

                        if (IsValidDateTime(_termsInfoResult.Bar_Position_Share_Future_Date))
                            futureDate = Convert.ToDateTime(_termsInfoResult.Bar_Position_Share_Future_Date).ToString("dd/MMM/yyyy");
                        else
                            futureDate = System.DateTime.Now.AddYears(50).ToString("dd/MMM/yyyy");

                        if ((Convert.ToDateTime(previousDate) - _collectionDate).TotalDays < 0.0)
                            _termsInfo.SiteShare = _termsInfoResult.Bar_Position_Site_Share_Previous;
                        else if ((Convert.ToDateTime(futureDate) - _collectionDate).TotalDays >= 0.0)
                            _termsInfo.SiteShare = _termsInfoResult.Bar_Position_Site_Share_Future;
                        else
                            _termsInfo.SiteShare = _termsInfoResult.Bar_Position_Site_Share;
                    }
                    else
                    {
                        if (_termsInfoResult.Terms_Profile_Partners_Supplier_Type != TERMS_SHARE_SCHEDULE && _termsInfoResult.Terms_Profile_Partners_Supplier_Use.GetValueOrDefault())
                            _termsInfo.SiteShare = _termsInfoResult.Terms_Profile_Partners_Site_Share;
                    }

                    _termsInfo.SiteShareGuaranteed = _termsInfoResult.Terms_Profile_Partners_Site_Share_Guaranteed;
                    _termsInfo.SiteGuarantor = _termsInfoResult.Terms_Profile_Partners_Site_Guarantor;
                    _termsInfo.SiteGuarantorPercentage = _termsInfoResult.Terms_Profile_Partners_Site_Guarantor_Percentage;
                }
                else
                {
                    _termsInfo.SiteUse = 0;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void SetGroupTermsInfo()
        {
            string previousDate = string.Empty;
            string futureDate = string.Empty;

            try
            {
                LogManager.WriteLog("Inside SetGroupTermsInfo...", LogManager.enumLogLevel.Info);
                if (_termsInfoResult.Terms_Profile_Partners_Group_Use.GetValueOrDefault())
                {
                    _termsInfo.GroupUse = _termsInfoResult.Terms_Profile_Partners_Group_Index;
                    _termsInfo.GroupCashDestination = _termsInfoResult.Terms_Profile_Partners_Group_Cash_Destination <= 0 ? TERMS_LEFT_ON_SITE : _termsInfoResult.Terms_Profile_Partners_Group_Cash_Destination;
                    _termsInfo.GroupDeferredDest = _termsInfoResult.Terms_Profile_Partners_Group_Deferred_Remittance;
                    _termsInfo.GroupType = _termsInfoResult.Terms_Profile_Partners_Group_Type;
                    _termsInfo.GroupValue = _termsInfoResult.Terms_Profile_Partners_Group_Value;
                    _termsInfo.GroupValueGuaranteed = _termsInfoResult.Terms_Profile_Partners_Group_Value_Guaranteed;

                    //Calc Shares
                    if (_termsInfoResult.Bar_Position_Override_Shares.GetValueOrDefault())
                    {
                        if (IsValidDateTime(_termsInfoResult.Bar_Position_Share_Past_Date))
                            previousDate = Convert.ToDateTime(_termsInfoResult.Bar_Position_Share_Past_Date).ToString("dd/MMM/yyyy");
                        else
                            previousDate = "01/01/1950";

                        if (IsValidDateTime(_termsInfoResult.Bar_Position_Share_Future_Date))
                            futureDate = Convert.ToDateTime(_termsInfoResult.Bar_Position_Share_Future_Date).ToString("dd/MMM/yyyy");
                        else
                            futureDate = System.DateTime.Now.AddYears(50).ToString("dd/MMM/yyyy");

                        if ((Convert.ToDateTime(previousDate) - _collectionDate).TotalDays < 0.0)
                            _termsInfo.GroupShare = _termsInfoResult.Bar_Position_Owners_Share_Previous;
                        else if ((Convert.ToDateTime(futureDate) - _collectionDate).TotalDays >= 0.0)
                            _termsInfo.GroupShare = _termsInfoResult.Bar_Position_Owners_Share_Future;
                        else
                            _termsInfo.GroupShare = _termsInfoResult.Bar_Position_Owners_Share;
                    }
                    else
                    {
                        if (_termsInfoResult.Terms_Profile_Partners_Supplier_Type != TERMS_SHARE_SCHEDULE && _termsInfoResult.Terms_Profile_Partners_Supplier_Use.GetValueOrDefault())
                            _termsInfo.GroupShare = _termsInfoResult.Terms_Profile_Partners_Group_Share;
                    }

                    _termsInfo.GroupShareGuaranteed = _termsInfoResult.Terms_Profile_Partners_Group_Share_Guaranteed;
                    _termsInfo.GroupGuarantor = _termsInfoResult.Terms_Profile_Partners_Group_Guarantor;
                    _termsInfo.GroupGuarantorPercentage = _termsInfoResult.Terms_Profile_Partners_Group_Guarantor_Percentage;
                }
                else
                {
                    _termsInfo.GroupUse = 0;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void SetSecGroupTermsInfo()
        {
            string previousDate = string.Empty;
            string futureDate = string.Empty;

            try
            {
                LogManager.WriteLog("Inside SetSecGroupTermsInfo...", LogManager.enumLogLevel.Info);
                if (_termsInfoResult.Terms_Profile_Partners_Sec_Group_Use.GetValueOrDefault())
                {
                    _termsInfo.SecGroupUse = _termsInfoResult.Terms_Profile_Partners_Sec_Group_Index;
                    _termsInfo.SecGroupCashDestination = _termsInfoResult.Terms_Profile_Partners_Sec_Group_Cash_Destination <= 0 ? TERMS_LEFT_ON_SITE : _termsInfoResult.Terms_Profile_Partners_Sec_Group_Cash_Destination;
                    _termsInfo.SecGroupDeferredDest = _termsInfoResult.Terms_Profile_Partners_Sec_Group_Deferred_Remittance;
                    _termsInfo.SecGroupType = _termsInfoResult.Terms_Profile_Partners_Sec_Group_Type;
                    _termsInfo.SecGroupValue = _termsInfoResult.Terms_Profile_Partners_Sec_Group_Value;
                    _termsInfo.SecGroupValueGuaranteed = _termsInfoResult.Terms_Profile_Partners_Sec_Group_Value_Guaranteed;

                    //Calc Shares
                    if (_termsInfoResult.Bar_Position_Override_Shares.GetValueOrDefault())
                    {
                        if (IsValidDateTime(_termsInfoResult.Bar_Position_Share_Past_Date))
                            previousDate = Convert.ToDateTime(_termsInfoResult.Bar_Position_Share_Past_Date).ToString("dd/MMM/yyyy");
                        else
                            previousDate = "01/01/1950";

                        if (IsValidDateTime(_termsInfoResult.Bar_Position_Share_Future_Date))
                            futureDate = Convert.ToDateTime(_termsInfoResult.Bar_Position_Share_Future_Date).ToString("dd/MMM/yyyy");
                        else
                            futureDate = System.DateTime.Now.AddYears(50).ToString("dd/MMM/yyyy");

                        if ((Convert.ToDateTime(previousDate) - _collectionDate).TotalDays < 0.0)
                            _termsInfo.SecGroupShare = _termsInfoResult.Bar_Position_Secondary_Owners_Share_Previous;
                        else if ((Convert.ToDateTime(futureDate) - _collectionDate).TotalDays >= 0.0)
                            _termsInfo.SecGroupShare = _termsInfoResult.Bar_Position_Secondary_Owners_Share_Future;
                        else
                            _termsInfo.SecGroupShare = _termsInfoResult.Bar_Position_Secondary_Owners_Share;
                    }
                    else
                    {
                        if (_termsInfoResult.Terms_Profile_Partners_Supplier_Type != TERMS_SHARE_SCHEDULE && _termsInfoResult.Terms_Profile_Partners_Supplier_Use.GetValueOrDefault())
                            _termsInfo.SecGroupShare = _termsInfoResult.Terms_Profile_Partners_Sec_Group_Share;
                    }

                    _termsInfo.SecGroupShareGuaranteed = _termsInfoResult.Terms_Profile_Partners_Sec_Group_Share_Guaranteed;
                    _termsInfo.SecGroupGuarantor = _termsInfoResult.Terms_Profile_Partners_Sec_Group_Guarantor;
                    _termsInfo.SecGroupGuarantorPercentage = _termsInfoResult.Terms_Profile_Partners_Sec_Group_Guarantor_Percentage;
                }
                else
                {
                    _termsInfo.SecGroupUse = 0;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void SetVATTermsInfo()
        {
            try
            {
                LogManager.WriteLog("Inside SetVATTermsInfo...", LogManager.enumLogLevel.Info);
                // VAT
                if (_termsInfoResult.Terms_Profile_VAT_Output_Use.GetValueOrDefault())
                {
                    _termsInfo.VATOutputUse = _termsInfoResult.Terms_Profile_VAT_Output_Index;
                    _termsInfo.VATCashDestination = _termsInfoResult.Terms_Profile_VAT_Output_Cash_Destination;
                    _termsInfo.VATDeferredDest = _termsInfoResult.Terms_Profile_VAT_Output_Deferred_Remittance;
                }
                if (_termsInfoResult.Terms_Profile_VAT_Supplier_Use.GetValueOrDefault())
                {
                    _termsInfo.VATSupplierUse = _termsInfoResult.Terms_Profile_VAT_Supplier_Index;
                    _termsInfo.VATSupplierDest = _termsInfoResult.Terms_Profile_VAT_Supplier_Cash_Destination <= 0 ? TERMS_SUPPLIER : _termsInfoResult.Terms_Profile_VAT_Supplier_Cash_Destination;
                    _termsInfo.VATSupplierDeferredDest = _termsInfoResult.Terms_Profile_VAT_Supplier_Deferred_Remittance;
                    _termsInfo.VATSupplierShortfallGuarantor = _termsInfoResult.Terms_Profile_VAT_Supplier_Guarantor;
                }
                if (_termsInfoResult.Terms_Profile_VAT_Site_Use.GetValueOrDefault())
                {
                    _termsInfo.VATSiteUse = _termsInfoResult.Terms_Profile_VAT_Site_Index;
                    _termsInfo.VATSiteDest = _termsInfoResult.Terms_Profile_VAT_Site_Cash_Destination <= 0 ? TERMS_LEFT_ON_SITE : _termsInfoResult.Terms_Profile_VAT_Site_Cash_Destination;
                    _termsInfo.VatSiteDeferredDest = _termsInfoResult.Terms_Profile_VAT_Site_Deferred_Remittance;
                    _termsInfo.VATSiteShortfallGuarantor = _termsInfoResult.Terms_Profile_VAT_Site_Guarantor;
                }
                if (_termsInfoResult.Terms_Profile_VAT_Group_Use.GetValueOrDefault())
                {
                    _termsInfo.VATGroupUse = _termsInfoResult.Terms_Profile_VAT_Group_Index;
                    _termsInfo.VATSupplierDest = _termsInfoResult.Terms_Profile_VAT_Group_Cash_Destination <= 0 ? TERMS_LEFT_ON_SITE : _termsInfoResult.Terms_Profile_VAT_Group_Cash_Destination;
                    _termsInfo.VATGroupDeferredDest = _termsInfoResult.Terms_Profile_VAT_Group_Deferred_Remittance;
                    _termsInfo.VATGroupShortfallGuarantor = _termsInfoResult.Terms_Profile_VAT_Group_Guarantor;
                }
                if (_termsInfoResult.Terms_Profile_VAT_Sec_Group_Use.GetValueOrDefault())
                {
                    _termsInfo.VatSecGroupUse = _termsInfoResult.Terms_Profile_VAT_Sec_Group_Index;
                    _termsInfo.VATSecGroupDest = _termsInfoResult.Terms_Profile_VAT_Sec_Group_Cash_Destination <= 0 ? TERMS_LEFT_ON_SITE : _termsInfoResult.Terms_Profile_VAT_Sec_Group_Cash_Destination;
                    _termsInfo.VATSecGroupDeferredDest = _termsInfoResult.Terms_Profile_VAT_Sec_Group_Deferred_Remittance;
                    _termsInfo.VATSecGroupShortfallGuarantor = _termsInfoResult.Terms_Profile_VAT_Sec_Group_Guarantor;
                }

                //GPT
                if (Convert.ToBoolean(_termsInfoResult.Terms_Profile_GPT_Use))
                {
                    _termsInfo.GPTUse = _termsInfoResult.Terms_Profile_GPT_Use;
                    _termsInfo.GPTCashDestination = _termsInfoResult.Terms_Profile_GPT_Cash_Destination;
                    _termsInfo.GPTDeferredDestination = _termsInfoResult.Terms_Profile_GPT_Deferred_Remittance;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void SetLicenseTermsInfo()
        {
            string previousDate = string.Empty;
            string futureDate = string.Empty;

            try
            {
                LogManager.WriteLog("Inside SetLicenseTermsInfo...", LogManager.enumLogLevel.Info);
                if (_termsInfoResult.Terms_Profile_Other_Licence_Use.GetValueOrDefault())
                {
                    _termsInfo.OtherLicenceUse = _termsInfoResult.Terms_Profile_Other_Licence_Index;
                    _termsInfo.OtherLicenceVAT = _termsInfoResult.Terms_Profile_Other_Licence_Vat;
                    _termsInfo.OtherLicenceCashDest = _termsInfoResult.Terms_Profile_Other_Licence_Cash_Destination <= 0 ? TERMS_LEFT_ON_SITE : _termsInfoResult.Terms_Profile_Other_Licence_Cash_Destination;
                    _termsInfo.OtherLicenceDeferredDest = _termsInfoResult.Terms_Profile_Other_Licence_Deferred_Remittance;
                    _termsInfo.OtherLicencePaidBy = _termsInfoResult.Terms_Profile_Other_Licence_Paid_By;
                    _termsInfo.OtherLicenceGuarantor = _termsInfoResult.Terms_Profile_Other_Licence_Guarantor;
                    _termsInfo.OtherLicenceFrequency = _termsInfoResult.Terms_Profile_Other_Licence_Frequency;

                    if (_termsInfoResult.Bar_Position_Override_Licence.GetValueOrDefault())
                    {
                        if (IsValidDateTime(_termsInfoResult.Bar_Position_Licence_Past_Date))
                            previousDate = Convert.ToDateTime(_termsInfoResult.Bar_Position_Licence_Past_Date).ToString("dd/MMM/yyyy");
                        else
                            previousDate = "01/01/1950";

                        if (IsValidDateTime(_termsInfoResult.Bar_Position_Licence_Future_Date))
                            futureDate = Convert.ToDateTime(_termsInfoResult.Bar_Position_Licence_Future_Date).ToString("dd/MMM/yyyy");
                        else
                            futureDate = System.DateTime.Now.AddYears(50).ToString("dd/MMM/yyyy");

                        if ((Convert.ToDateTime(previousDate) - _collectionDate).TotalDays < 0.0)
                            _termsInfo.OtherLicenceCharge = _termsInfoResult.Bar_Position_Licence_Previous;
                        else if ((Convert.ToDateTime(futureDate) - _collectionDate).TotalDays >= 0.0)
                            _termsInfo.OtherLicenceCharge = _termsInfoResult.Bar_Position_Licence_Future;
                        else
                            _termsInfo.OtherLicenceCharge = _termsInfoResult.Bar_Position_Licence_Charge;
                    }
                    else
                    {
                        _termsInfo.OtherLicenceCharge = _termsInfoResult.Terms_Profile_Other_Licence_Charge;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void SetPrizeTermsInfo()
        {
            try
            {
                LogManager.WriteLog("Inside SetPrizeTermsInfo...", LogManager.enumLogLevel.Info);
                if (_termsInfoResult.Terms_Profile_Other_Prize_Use.GetValueOrDefault())
                {
                    _termsInfo.OtherPrizeUse = _termsInfoResult.Terms_Profile_Other_Prize_Index;
                    _termsInfo.OtherPrizeVAT = _termsInfoResult.Terms_Profile_Other_Prize_Vat;
                    _termsInfo.OtherPrizeCashDest = _termsInfoResult.Bar_Position_Prize_LOS.GetValueOrDefault() == true ? TERMS_LEFT_ON_SITE
                        : _termsInfoResult.Terms_Profile_Other_Prize_Cash_Destination <= 0 ? TERMS_LEFT_ON_SITE : _termsInfoResult.Terms_Profile_Other_Prize_Cash_Destination;
                    _termsInfo.OtherPrizeDeferredDest = _termsInfoResult.Terms_Profile_Other_Prize_Deferred_Remittance;
                    _termsInfo.OtherPrizeCharge = _termsInfoResult.Terms_Profile_Other_Prize_Charge;
                    _termsInfo.OtherPrizePaidBy = _termsInfoResult.Terms_Profile_Other_Prize_Paid_By;
                    _termsInfo.OtherPrizeGuarantor = _termsInfoResult.Terms_Profile_Other_Prize_Guarantor;
                    _termsInfo.OtherPrizeFrequency = _termsInfoResult.Terms_Profile_Other_Prize_Frequency;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void SetConsultancyTermsInfo()
        {
            try
            {
                LogManager.WriteLog("Inside SetConsultancyTermsInfo...", LogManager.enumLogLevel.Info);
                if (_termsInfoResult.Terms_Profile_Other_Consultancy_Use.GetValueOrDefault())
                {
                    _termsInfo.OtherConsultancyUse = _termsInfoResult.Terms_Profile_Other_Consultancy_Index;
                    _termsInfo.OtherConsultancyVAT = _termsInfoResult.Terms_Profile_Other_Consultancy_Vat;
                    _termsInfo.OtherConsultancyCashDest = _termsInfoResult.Terms_Profile_Other_Consultancy_Cash_Destination <= 0 ? TERMS_LEFT_ON_SITE : _termsInfoResult.Terms_Profile_Other_Consultancy_Cash_Destination;
                    _termsInfo.OtherConsultancyDeferredDest = _termsInfoResult.Terms_Profile_Other_Consultancy_Deferred_Remittance;
                    _termsInfo.OtherConsultancyCharge = _termsInfoResult.Terms_Profile_Other_Consultancy_Charge;
                    _termsInfo.OtherConsultancyPaidBy = _termsInfoResult.Terms_Profile_Other_Consultancy_Paid_By;
                    _termsInfo.OtherConsultancyGuarantor = _termsInfoResult.Terms_Profile_Other_Consultancy_Guarantor;
                    _termsInfo.OtherConsultancyFrequency = _termsInfoResult.Terms_Profile_Other_Consultancy_Frequency;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void SetRoyaltyTermsInfo()
        {
            try
            {
                LogManager.WriteLog("Inside SetRoyaltyTermsInfo...", LogManager.enumLogLevel.Info);
                if (_termsInfoResult.Terms_Profile_Other_Royalty_Use.GetValueOrDefault())
                {
                    _termsInfo.OtherRoyaltyUse = _termsInfoResult.Terms_Profile_Other_Royalty_Index;
                    _termsInfo.OtherRoyaltyVAT = _termsInfoResult.Terms_Profile_Other_Royalty_Vat;
                    _termsInfo.OtherRoyaltyCashDest = _termsInfoResult.Terms_Profile_Other_Royalty_Cash_Destination <= 0 ? TERMS_LEFT_ON_SITE : _termsInfoResult.Terms_Profile_Other_Royalty_Cash_Destination;
                    _termsInfo.OtherRoyaltyDeferredDest = _termsInfoResult.Terms_Profile_Other_Royalty_Deferred_Remittance;
                    _termsInfo.OtherRoyaltyCharge = _termsInfoResult.Terms_Profile_Other_Royalty_Charge;
                    _termsInfo.OtherRoyaltyPaidBy = _termsInfoResult.Terms_Profile_Other_Royalty_Paid_By;
                    _termsInfo.OtherRoyaltyGuarantor = _termsInfoResult.Terms_Profile_Other_Royalty_Guarantor;
                    _termsInfo.OtherRoyaltyFrequency = _termsInfoResult.Terms_Profile_Other_Royalty_Frequency;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void SetOtherTermsInfo()
        {
            try
            {
                LogManager.WriteLog("Inside SetOtherTermsInfo...", LogManager.enumLogLevel.Info);
                if (_termsInfoResult.Terms_Profile_Other_Other1_Use.GetValueOrDefault())
                {
                    _termsInfo.OtherOther1Use = _termsInfoResult.Terms_Profile_Other_Other1_Index;
                    _termsInfo.OtherOther1VAT = _termsInfoResult.Terms_Profile_Other_Other1_Vat;
                    _termsInfo.OtherOther1CashDest = _termsInfoResult.Terms_Profile_Other_Other1_Cash_Destination <= 0 ? TERMS_LEFT_ON_SITE : _termsInfoResult.Terms_Profile_Other_Other1_Cash_Destination;
                    _termsInfo.OtherOther1DeferredDest = _termsInfoResult.Terms_Profile_Other_Other1_Deferred_Remittance;
                    _termsInfo.OtherOther1Charge = _termsInfoResult.Terms_Profile_Other_Other1_Charge;
                    _termsInfo.OtherOther1PaidBy = _termsInfoResult.Terms_Profile_Other_Other1_Paid_By;
                    _termsInfo.OtherOther1Guarantor = _termsInfoResult.Terms_Profile_Other_Other1_Guarantor;
                    _termsInfo.OtherOther1Frequency = _termsInfoResult.Terms_Profile_Other_Other1_Frequency;
                }

                if (_termsInfoResult.Terms_Profile_Other_Other2_Use.GetValueOrDefault())
                {
                    _termsInfo.OtherOther2Use = _termsInfoResult.Terms_Profile_Other_Other2_Index;
                    _termsInfo.OtherOther2VAT = _termsInfoResult.Terms_Profile_Other_Other2_Vat;
                    _termsInfo.OtherOther2CashDest = _termsInfoResult.Terms_Profile_Other_Other2_Cash_Destination <= 0 ? TERMS_LEFT_ON_SITE : _termsInfoResult.Terms_Profile_Other_Other2_Cash_Destination;
                    _termsInfo.OtherOther2DeferredDest = _termsInfoResult.Terms_Profile_Other_Other2_Deferred_Remittance;
                    _termsInfo.OtherOther2Charge = _termsInfoResult.Terms_Profile_Other_Other2_Charge;
                    _termsInfo.OtherOther2PaidBy = _termsInfoResult.Terms_Profile_Other_Other2_Paid_By;
                    _termsInfo.OtherOther2Guarantor = _termsInfoResult.Terms_Profile_Other_Other2_Guarantor;
                    _termsInfo.OtherOther2Frequency = _termsInfoResult.Terms_Profile_Other_Other2_Frequency;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void SetLondonRentTermsInfo()
        {
            try
            {
                LogManager.WriteLog("Inside SetLondonRentTermsInfo...", LogManager.enumLogLevel.Info);
                _termsInfo.LondonRent = 0;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private ShareScheduleInfo GetShareScheduleInfo(int? shareScheduleID, ref string scheduleName)
        {
            MachineClassResult _machineClassIDResult        =   null;
            ShareScheduleInfo _shareScheduleInfo            =   null;
            ShareScheduleResult _shareScheduleInfoResult    =   null;
            ShareBandResult _shareBandResult                =   null;
            
            int? _shareBandID = 0;

            try
            {
                LogManager.WriteLog("Inside GetShareScheduleInfo...", LogManager.enumLogLevel.Info);

                _shareScheduleInfo = new ShareScheduleInfo();
                
                LogManager.WriteLog((string.Format("Fetching Machine Class ID for InstallationID - [{0}]...", _installationId)), LogManager.enumLogLevel.Info);
                _machineClassIDResult = DataContext.GetMachineClassIDForTermsCalculation(_installationId).SingleOrDefault();

                if (_machineClassIDResult != null)
                {
                    LogManager.WriteLog((string.Format("Fetching Share Schedule Information for Share Schedule ID - [{0}], Machine Class ID - [{1}]...", _installationId, _machineClassIDResult.Machine_Class_ID)), LogManager.enumLogLevel.Info);
                    _shareScheduleInfoResult = DataContext.GetShareScheduleInfoForTermsCalculation(shareScheduleID, _machineClassIDResult.Machine_Class_ID).SingleOrDefault();

                    if (_shareScheduleInfoResult != null)
                    {
                        scheduleName = _shareScheduleInfoResult.Share_Schedule_Name;

                        if ((_collectionDate - Convert.ToDateTime(_shareScheduleInfoResult.Machine_Class_Share_Past_Date)).TotalDays > 0)
                            _shareBandID = _shareScheduleInfoResult.Share_Band_ID_Past;
                        else if ((Convert.ToDateTime(_shareScheduleInfoResult.Machine_Class_Share_Future_Date) - _collectionDate).TotalDays >= 0)
                            _shareBandID = _shareScheduleInfoResult.Share_Band_ID_Future;
                        else
                            _shareBandID = _shareScheduleInfoResult.Share_Band_ID;

                        LogManager.WriteLog((string.Format("Fetching Share Band Information for Share Band ID - [{0}]...", _installationId)), LogManager.enumLogLevel.Info);
                        _shareBandResult = DataContext.GetShareBandInfoForTermsCalculation(_shareBandID).SingleOrDefault();

                        if (_shareBandResult != null)
                        {
                            if ((_collectionDate - Convert.ToDateTime(_shareBandResult.Share_Band_Past_End_Date)).TotalDays > 0)
                            {
                                _shareScheduleInfo.SupplierShare = _shareBandResult.Share_Band_Past_Supplier_Share;
                                _shareScheduleInfo.SiteShare = _shareBandResult.Share_Band_Past_Site_Share;
                                _shareScheduleInfo.CompanyShare = _shareBandResult.Share_Band_Past_Company_Share;
                                _shareScheduleInfo.SecCompanyShare = _shareBandResult.Share_Band_Past_Sec_Company_Share;
                                _shareScheduleInfo.SupplierRent = _shareBandResult.Share_Band_Past_Supplier_Rent;
                                _shareScheduleInfo.SupplierRentGuaranteed = _shareBandResult.Share_Band_Past_Supplier_Rent_Guaranteed;
                                _shareScheduleInfo.SupplierShareGuaranteed = _shareBandResult.Share_Band_Past_Supplier_Share_Guaranteed;
                                _shareScheduleInfo.SiteShareGuaranteed = _shareBandResult.Share_Band_Past_Site_Share_Guaranteed;
                                _shareScheduleInfo.CompanyShareGuaranteed = _shareBandResult.Share_Band_Past_Company_Share_Guaranteed;
                                _shareScheduleInfo.SecCompanyShareGuaranteed = _shareBandResult.Share_Band_Past_Sec_Company_Share_Guaranteed;

                            }
                            else if ((Convert.ToDateTime(_shareBandResult.Share_Band_Future_Start_Date) - _collectionDate).TotalDays > 0)
                            {
                                _shareScheduleInfo.SupplierShare = _shareBandResult.Share_Band_Future_Supplier_Share;
                                _shareScheduleInfo.SiteShare = _shareBandResult.Share_Band_Future_Site_Share;
                                _shareScheduleInfo.CompanyShare = _shareBandResult.Share_Band_Future_Company_Share;
                                _shareScheduleInfo.SecCompanyShare = _shareBandResult.Share_Band_Future_Sec_Company_Share;
                                _shareScheduleInfo.SupplierRent = _shareBandResult.Share_Band_Future_Supplier_Rent;
                                _shareScheduleInfo.SupplierRentGuaranteed = _shareBandResult.Share_Band_Future_Supplier_Rent_Guaranteed;
                                _shareScheduleInfo.SupplierShareGuaranteed = _shareBandResult.Share_Band_Future_Supplier_Share_Guaranteed;
                                _shareScheduleInfo.SiteShareGuaranteed = _shareBandResult.Share_Band_Future_Site_Share_Guaranteed;
                                _shareScheduleInfo.CompanyShareGuaranteed = _shareBandResult.Share_Band_Future_Company_Share_Guaranteed;
                                _shareScheduleInfo.SecCompanyShareGuaranteed = _shareBandResult.Share_Band_Future_Sec_Company_Share_Guaranteed;
                            }
                            else
                            {
                                _shareScheduleInfo.SupplierShare = _shareBandResult.Share_Band_Supplier_Share;
                                _shareScheduleInfo.SiteShare = _shareBandResult.Share_Band_Site_Share;
                                _shareScheduleInfo.CompanyShare = _shareBandResult.Share_Band_Company_Share;
                                _shareScheduleInfo.SecCompanyShare = _shareBandResult.Share_Band_Sec_Company_Share;
                                _shareScheduleInfo.SupplierRent = _shareBandResult.Share_Band_Past_Supplier_Rent;
                                _shareScheduleInfo.SupplierRentGuaranteed = _shareBandResult.Share_Band_Supplier_Rent_Guaranteed;
                                _shareScheduleInfo.SupplierShareGuaranteed = _shareBandResult.Share_Band_Supplier_Share_Guaranteed;
                                _shareScheduleInfo.SiteShareGuaranteed = _shareBandResult.Share_Band_Site_Share_Guaranteed;
                                _shareScheduleInfo.CompanyShareGuaranteed = _shareBandResult.Share_Band_Company_Share_Guaranteed;
                                _shareScheduleInfo.SecCompanyShareGuaranteed = _shareBandResult.Share_Band_Sec_Company_Share_Guaranteed;
                            }
                        }
                        else
                        {
                            _shareScheduleInfo.SupplierShare = 0;
                            _shareScheduleInfo.SiteShare = 0;
                            _shareScheduleInfo.CompanyShare = 0;
                            _shareScheduleInfo.SecCompanyShare = 0;
                            _shareScheduleInfo.SupplierRent = 0;
                            _shareScheduleInfo.SupplierRentGuaranteed = false;
                            _shareScheduleInfo.SupplierShareGuaranteed = false;
                            _shareScheduleInfo.SiteShareGuaranteed = false;
                            _shareScheduleInfo.CompanyShareGuaranteed = false;
                            _shareScheduleInfo.SecCompanyShareGuaranteed = false;
                        }
                    }
                    else
                    {
                        _shareScheduleInfo.SupplierShare = 0;
                        _shareScheduleInfo.SiteShare = 0;
                        _shareScheduleInfo.CompanyShare = 0;
                        _shareScheduleInfo.SecCompanyShare = 0;
                        _shareScheduleInfo.SupplierRent = 0;
                        _shareScheduleInfo.SupplierRentGuaranteed = false;
                        _shareScheduleInfo.SupplierShareGuaranteed = false;
                        _shareScheduleInfo.SiteShareGuaranteed = false;
                        _shareScheduleInfo.CompanyShareGuaranteed = false;
                        _shareScheduleInfo.SecCompanyShareGuaranteed = false;
                    }
                }
                else
                {
                    _shareScheduleInfo.SupplierShare = 0;
                    _shareScheduleInfo.SiteShare = 0;
                    _shareScheduleInfo.CompanyShare = 0;
                    _shareScheduleInfo.SecCompanyShare = 0;
                    _shareScheduleInfo.SupplierRent = 0;
                    _shareScheduleInfo.SupplierRentGuaranteed = false;
                    _shareScheduleInfo.SupplierShareGuaranteed = false;
                    _shareScheduleInfo.SiteShareGuaranteed = false;
                    _shareScheduleInfo.CompanyShareGuaranteed = false;
                    _shareScheduleInfo.SecCompanyShareGuaranteed = false;

                }                
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return _shareScheduleInfo;
        }

        private RentInfo RentCostCurrentWithStartDate(int? rentScheduleID, DateTime collectionDate, ref string rentStartDate, ref string rentScheduleName)
        {
            MachineClassResult _machineClassIDResult    =   null;
            RentInfo _rentInfo                          =   null;

            try
            {
                LogManager.WriteLog("Inside RentCostCurrentWithStartDate...", LogManager.enumLogLevel.Info);

                LogManager.WriteLog((string.Format("Fetching Machine Class ID for InstallationID - [{0}]...", _installationId)), LogManager.enumLogLevel.Info);
                _machineClassIDResult = DataContext.GetMachineClassIDForTermsCalculation(_installationId).SingleOrDefault();

                if (_machineClassIDResult != null)
                {
                    _rentInfo = RentCostCurrentWithStartDateFromMachineClassID(_machineClassIDResult.Machine_Class_ID, rentScheduleID, collectionDate, ref rentStartDate, ref rentScheduleName);
                }
                else
                {
                    _rentInfo = new RentInfo();
                    _rentInfo.Rent = 0;
                    _rentInfo.SuppCharge = 0;
                    _rentInfo.SuppChargeEndPeriod = 0;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return _rentInfo;
        }

        private RentInfo RentCostCurrentWithStartDateFromMachineClassID(int? machineClassID, int? rentScheduleID, DateTime collectionDate, ref string rentStartDate, ref string rentScheduleName)
        {
            RentResult _rentResult = null;
            RentInfo _rentInfo = new RentInfo();
            RentBandMode _rentBandMode = RentBandMode.Current;
            RentMode _rentMode = RentMode.Current;
            string _rentPastDate;
            string _rentFutureDate;
            float? _rentPast;
            float? _rentFuture;
            float? _rentCurrent;
            float? _supCharge;
            float? _supChargePast;
            float? _supChargeCurrent;
            float? _supChargeFuture;
            bool? _supChargePerc;
            
            try
            {
                LogManager.WriteLog("Inside RentCostCurrentWithStartDateFromMachineClassID...", LogManager.enumLogLevel.Info);

                LogManager.WriteLog((string.Format("Fetching Rent Info for Machine Class ID - [{0}], Rent Schedule ID - [{1}]...", machineClassID, rentScheduleID)), LogManager.enumLogLevel.Info);
                _rentResult = DataContext.GetRentInfoForTermsCalculation(machineClassID, rentScheduleID).SingleOrDefault();

                if (_rentResult != null)
                {
                    rentScheduleName = _rentResult.Rent_Schedule_Name;                   

                    if (IsValidDateTime(_rentResult.Machine_Class_Past_Date) && IsValidDateTime(_rentResult.Machine_Class_Future_Date))                    
                        _rentBandMode = (Convert.ToDateTime(_rentResult.Machine_Class_Past_Date) - collectionDate).TotalDays < 0.0 ? RentBandMode.Past
                            : (Convert.ToDateTime(_rentResult.Machine_Class_Future_Date) - collectionDate).TotalDays >= 0.0 ? RentBandMode.Future : RentBandMode.Current;                    
                    else                    
                        _rentBandMode = RentBandMode.Current;

                    _supCharge = _rentResult.Machine_Class_Rent_Band_Supplemental_Charge;
                    _supChargePerc = _rentResult.Rent_Schedule_Supplemental_Is_Percentage;

                    switch (_rentBandMode)
                    {
                        case RentBandMode.Current:
                            _rentPast = _rentResult.Current_Past_Price;
                            _rentFuture = _rentResult.Current_Future_Price;
                            _rentCurrent = _rentResult.Current_Current_Price;
                            _rentPastDate = _rentResult.Current_Past_Date;
                            _rentFutureDate = _rentResult.Current_Future_Date;

                            _supChargePast = _rentResult.Current_Past_SupCharge;
                            _supChargeFuture = _rentResult.Current_Future_SupCharge;
                            _supChargeCurrent = _rentResult.Current_Current_SupCharge;
                            break;
                        case RentBandMode.Future:
                            _rentPast = _rentResult.Future_Past_Price;
                            _rentFuture = _rentResult.Future_Future_Price;
                            _rentCurrent = _rentResult.Future_Current_Price;
                            _rentPastDate = _rentResult.Future_Past_Date;
                            _rentFutureDate = _rentResult.Future_Future_Date;

                            _supChargePast = _rentResult.Future_Past_SupCharge;
                            _supChargeFuture = _rentResult.Future_Future_SupCharge;
                            _supChargeCurrent = _rentResult.Future_Current_SupCharge;
                            break;
                        case RentBandMode.Past:
                            _rentPast = _rentResult.Past_Past_Price;
                            _rentFuture = _rentResult.Past_Future_Price;
                            _rentCurrent = _rentResult.Past_Current_Price;
                            _rentPastDate = _rentResult.Past_Past_Date;
                            _rentFutureDate = _rentResult.Past_Future_Date;

                            _supChargePast = _rentResult.Past_Past_SupCharge;
                            _supChargeFuture = _rentResult.Past_Future_SupCharge;
                            _supChargeCurrent = _rentResult.Past_Current_SupCharge;
                            break;
                        default:
                            _rentPast = _rentResult.Current_Past_Price;
                            _rentFuture = _rentResult.Current_Future_Price;
                            _rentCurrent = _rentResult.Current_Current_Price;
                            _rentPastDate = _rentResult.Current_Past_Date;
                            _rentFutureDate = _rentResult.Current_Future_Date;

                            _supChargePast = _rentResult.Current_Past_SupCharge;
                            _supChargeFuture = _rentResult.Current_Future_SupCharge;
                            _supChargeCurrent = _rentResult.Current_Current_SupCharge;
                            break;
                    }

                    if (IsValidDateTime(_rentPastDate) && IsValidDateTime(_rentFutureDate))
                        _rentMode = (Convert.ToDateTime(_rentPastDate) - collectionDate).TotalDays < 0.0 ? RentMode.Past 
                            : (Convert.ToDateTime(_rentFutureDate) - collectionDate).TotalDays >= 0.0 ? RentMode.Future : RentMode.Current;
                    else
                        _rentMode = RentMode.Current;

                    switch (_rentMode)
                    {
                        case RentMode.Current:
                            _rentInfo.Rent = _rentCurrent;
                            _rentInfo.SuppCharge = _supChargePerc == true ? ((_rentCurrent * _supCharge) / 100) : _supCharge;
                            _rentInfo.SuppChargeEndPeriod = _supChargeCurrent;
                            rentStartDate = _rentPastDate;
                            break;
                        case RentMode.Future:
                            _rentInfo.Rent = _rentFuture;
                            _rentInfo.SuppCharge = _supChargePerc == true ? ((_rentFuture * _supCharge) / 100) : _supCharge;
                            _rentInfo.SuppChargeEndPeriod = _supChargeFuture;
                            rentStartDate = _rentFutureDate;
                            break;
                        case RentMode.Past:
                            _rentInfo.Rent = _rentPast;
                            _rentInfo.SuppCharge = _supChargePerc == true ? ((_rentPast * _supCharge) / 100) : _supCharge;
                            _rentInfo.SuppChargeEndPeriod = _supChargePast;                            
                            break;
                        default:
                            _rentInfo.Rent = _rentCurrent;
                            _rentInfo.SuppCharge = _supChargePerc == true ? ((_rentCurrent * _supCharge) / 100) : _supCharge;
                            _rentInfo.SuppChargeEndPeriod = _supChargeCurrent;
                            rentStartDate = _rentPastDate;
                            break;
                    }
                }
                else
                {
                    _rentInfo.Rent = 0;
                    _rentInfo.SuppCharge = 0;
                    _rentInfo.SuppChargeEndPeriod = 0;                    
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return null;
        }

        private int ReturnValueFromChangeDates(int _previousValue, string _previousDate, int _currentValue, string _futureDate, int _futureValue, string _reqDate)
        {
            string _theDate = string.Empty;

            try
            {
                LogManager.WriteLog("Inside ReturnValueFromChangeDates...", LogManager.enumLogLevel.Info);

                if (string.IsNullOrEmpty(_reqDate))
                    _theDate = System.DateTime.Now.ToString("dd/MMM/yyyy");
                else if (!IsValidDateTime(_reqDate))
                    _theDate = System.DateTime.Now.ToString("dd/MMM/yyyy");
                else
                    _theDate = Convert.ToDateTime(_reqDate).ToString("dd/MMM/yyyy");

                if (!string.IsNullOrEmpty(_futureDate) && IsValidDateTime(_futureDate))
                    if (((Convert.ToDateTime(_futureDate) - Convert.ToDateTime(_theDate)).TotalDays) > 0.0)
                        return _futureValue;

                if (!string.IsNullOrEmpty(_futureDate) && IsValidDateTime(_previousDate))
                    if (((Convert.ToDateTime(_theDate) - Convert.ToDateTime(_previousDate)).TotalDays) >= 0.0)
                        return _previousValue;

                if ((_previousDate != _futureDate) || (string.IsNullOrEmpty(_previousDate) && string.IsNullOrEmpty(_futureDate)))
                    return _currentValue;
                else
                    return _previousValue;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return _currentValue;
        }
        #endregion SetTermsInfoForTermsCalculation

        #region CalculatePostTerms
        private bool CalculateTermsResultsForCollectionID()
        {
            int? _tmpValue = 0;

            try
            {
                LogManager.WriteLog("Inside CalculateTermsResultsForCollectionID...", LogManager.enumLogLevel.Info);

                if (!_termsInfo.TermsValid.GetValueOrDefault())
                    return false;

                if (_termsInfo.TermsValid.GetValueOrDefault())
                {
                    if (_termsInfo.BarPosSetForNoTerms.GetValueOrDefault())
                    {
                        _termsResults.NoTermsProcessingSet = true;
                        return false;
                    }
                }

                //Deal with PubMaster terms...
                if (_termsInfo.SupplierType.GetValueOrDefault() == TERMS_PUBMASTER)
                {
                    _termsInfo.SupplierType = TERMS_RENT_SCHEDULE;

                    //If 7 day average <=150 then
                    if ((_collectionGross <= ((Convert.ToDouble(150) * Convert.ToDouble(_colDays) / Convert.ToDouble(7))) || _colDays == 0))
                    {
                        if (_termsInfo.SecGroupUse > 0 && _termsInfo.SiteUse == 0)
                        {
                            _termsInfo.SecGroupShare = 50;
                            _termsInfo.GroupShare = 50;

                            //Site share is after rent
                            if (_termsInfo.SecGroupUse < _termsInfo.SupplierUse)
                            {
                                _tmpValue = _termsInfo.SecGroupUse;
                                _termsInfo.SecGroupUse = _termsInfo.SupplierUse;
                                _termsInfo.SupplierUse = _tmpValue;
                            }
                            if (_termsInfo.GroupUse < _termsInfo.SupplierUse)
                            {
                                _tmpValue = _termsInfo.GroupUse;
                                _termsInfo.GroupUse = _termsInfo.SupplierUse;
                                _termsInfo.SupplierUse = _tmpValue;
                            }
                        }
                        else
                        {
                            // Use the Site
                            _termsInfo.SiteShare = 50;
                            _termsInfo.GroupShare = 50;

                            //Site share is after rent
                            if (_termsInfo.SiteUse < _termsInfo.SupplierUse)
                            {
                                _tmpValue = _termsInfo.SiteUse;
                                _termsInfo.SiteUse = _termsInfo.SupplierUse;
                                _termsInfo.SupplierUse = _tmpValue;
                            }
                            if (_termsInfo.GroupUse < _termsInfo.SupplierUse)
                            {
                                _tmpValue = _termsInfo.GroupUse;
                                _termsInfo.GroupUse = _termsInfo.SupplierUse;
                                _termsInfo.SupplierUse = _tmpValue;
                            }
                        }
                    }
                    else
                    {
                        if (_termsInfo.SecGroupUse > 0 && _termsInfo.SiteUse == 0)
                        {
                            _termsInfo.SecGroupShare = 33.33f;
                            _termsInfo.GroupShare = 66.67f;

                            //Site share is before rent
                            if (_termsInfo.SecGroupUse < _termsInfo.SupplierUse)
                            {
                                _tmpValue = _termsInfo.SecGroupUse;
                                _termsInfo.SecGroupUse = _termsInfo.SupplierUse;
                                _termsInfo.SupplierUse = _tmpValue;
                            }
                            if (_termsInfo.GroupUse < _termsInfo.SupplierUse)
                            {
                                _tmpValue = _termsInfo.GroupUse;
                                _termsInfo.GroupUse = _termsInfo.SupplierUse;
                                _termsInfo.SupplierUse = _tmpValue;
                            }
                        }
                        else
                        {
                            // Use the Site
                            _termsInfo.SiteShare = 33.33f;
                            _termsInfo.GroupShare = 66.67f;

                            //Site share is before rent
                            if (_termsInfo.SiteUse > _termsInfo.SupplierUse)
                            {
                                _tmpValue = _termsInfo.SiteUse;
                                _termsInfo.SiteUse = _termsInfo.SupplierUse;
                                _termsInfo.SupplierUse = _tmpValue;
                            }
                            if (_termsInfo.GroupUse < _termsInfo.SupplierUse)
                            {
                                _tmpValue = _termsInfo.GroupUse;
                                _termsInfo.GroupUse = _termsInfo.SupplierUse;
                                _termsInfo.SupplierUse = _tmpValue;
                            }
                        }
                    }
                }

                _termsResults.CollectionValue = _collectionGross;
                _termsResults.CollectionValueOutputVatable = _collectionGross;
                _termsResults.Remainder = _collectionGross;
                _termsResults.CollectionDate = _collectionDate.ToString("dd/MMM/yyyy");
                _termsResults.CollectionDays = _colDays;
                _termsResults.UseSplitRent = _termsInfo.UseSplitRents.GetValueOrDefault();

                if (_prize > 0)
                {
                    //If they have specified a prize value, it will be a fixed charge for the collection
                    //not per week
                    _termsResults.InputPrizeValue = _prize;
                    _termsInfo.OtherPrizeFrequency = TERMS_FREQUENCY_PER_COLLECTION;
                }
                else
                {
                    _termsResults.InputPrizeValue = _termsInfo.OtherPrizeCharge.GetValueOrDefault();
                }

                /*
                STEP 0.5
                           If the supplier is rent only, don't bother with the rest of the terms
                           just work it out
                
                STEP 1
                           Loop through all Other Charges that come before the Output VAT and come from
                           the cash box
                
                STEP 2
                           Calculate Output VAT
                
                STEP 3
                           Loop through all Rent and Other Charges
                           (but only other charges that come after VAT and are from the cash box)
                
                STEP 4
                           Loop through all Shares for partners
                
                STEP 5
                           Process the Shortfall Guarantors
                
                STEP 6
                           Calculate How much the Partner VAT should be
                
                STEP 7
                           Calculate the Actual Partners Vat, and the Vat Shortfalls
                
                STEP 8
                           Calculate the Other Charges (that haven't already been calculated)
                           All charges that are not from the cash box.
                
                STEP 9
                           Now all the values are sorted out, work out what has to go to who
                           The destination and the deferred remittance
                
                STEP 10
                           Work out the supplier supplemental Period End charge
                
                */

                //#################
                //     STEP 0.5
                //#################

                //If the supplier is rent only, don't bother with the rest of the terms
                //just work it out

                if (_termsInfo.SupplierUse > 0 && _termsInfo.SupplierType == TERMS_RENT_ONLY)
                {
                    //Always use the site as a shortfall guarantor
                    CalculatePostTermsPartnerSupplierRentOnly();
                    CalculatePostTermsOtherLicence();

                    //Also add the new database fields
                    switch (_termsInfo.SupplierCashDestination)
                    {
                        case TERMS_SUPPLIER:
                            _termsResults.SupplierRent = _termsResults.SupplierRentShouldGet.GetValueOrDefault() + _termsResults.SupplierShareShouldGet.GetValueOrDefault();
                            _termsResults.SupplierVat = SystemVATRate * _termsResults.SupplierRent.GetValueOrDefault();
                            _termsResults.SiteVat = 0 - _termsResults.SupplierVat.GetValueOrDefault();
                            _termsResults.VATLOS = 0 - _termsResults.SupplierVat.GetValueOrDefault();
                            _termsResults.VATBankedToSupplier = _termsResults.SupplierVat.GetValueOrDefault();
                            _termsResults.SupplierVATBankedToSupplier = _termsResults.SupplierVat.GetValueOrDefault();
                            _termsResults.SiteVATLos = _termsResults.SiteVat.GetValueOrDefault();
                            _termsResults.LicenceChargeBankedToSupplier = _termsResults.LicenceCharge.GetValueOrDefault();
                            _termsResults.SiteRent = _termsResults.SiteRent - _termsResults.SupplierRent.GetValueOrDefault();
                            _termsResults.SupplierShareBankedToSupplier = _termsResults.SupplierRent.GetValueOrDefault();
                            _termsResults.SiteShareLOS = _termsResults.SiteRent.GetValueOrDefault();
                            _termsResults.BankedToCompany = 0;
                            _termsResults.BankedToSupplier = _termsResults.SupplierShareBankedToSupplier.GetValueOrDefault() + _termsResults.LicenceChargeBankedToSupplier.GetValueOrDefault() + _termsResults.VATBankedToSupplier.GetValueOrDefault();

                            //Add defferred
                            //supplier vat to c&e
                            _termsResults.VATSupplierBankedToSupplier_toCustoms = _termsResults.SupplierVat.GetValueOrDefault();
                            break;
                        case TERMS_SITE_GROUP:
                            //same as supplier but banked to brewery
                            //add defferred
                            //rent and vat from brewery to supplier                
                            _termsResults.SupplierRent = _termsResults.SupplierRentShouldGet.GetValueOrDefault() + _termsResults.SupplierShareShouldGet.GetValueOrDefault();
                            _termsResults.SupplierVat = SystemVATRate * _termsResults.SupplierRent.GetValueOrDefault();
                            _termsResults.SiteVat = 0 - _termsResults.SupplierVat.GetValueOrDefault();
                            _termsResults.VATLOS = 0 - _termsResults.SupplierVat.GetValueOrDefault();
                            _termsResults.VATBankedToCompany = _termsResults.SupplierVat.GetValueOrDefault();
                            _termsResults.SupplierVATBankedToCompany = _termsResults.SupplierVat.GetValueOrDefault();
                            _termsResults.SiteVATLos = _termsResults.SiteVat.GetValueOrDefault();
                            _termsResults.LicenceChargeBankedToCompany = _termsResults.LicenceCharge.GetValueOrDefault();
                            _termsResults.SiteRent = _termsResults.SiteRent - _termsResults.SupplierRent.GetValueOrDefault();
                            _termsResults.SupplierShareBankedToCompany = _termsResults.SupplierRent.GetValueOrDefault();
                            _termsResults.SiteShareLOS = _termsResults.SiteRent.GetValueOrDefault();
                            _termsResults.BankedToSupplier = 0;
                            _termsResults.BankedToCompany = _termsResults.SupplierShareBankedToCompany.GetValueOrDefault() + _termsResults.LicenceChargeBankedToCompany.GetValueOrDefault() + _termsResults.VATBankedToCompany.GetValueOrDefault();
                            _termsResults.VATSupplierBankedToGroup_toSupplier = _termsResults.SupplierVat.GetValueOrDefault();
                            _termsResults.SupplierShareBankedToGroup_toSupplier = _termsResults.SupplierRent.GetValueOrDefault();
                            break;
                        case TERMS_LEFT_ON_SITE:
                        case TERMS_SITE:
                            //banked=0
                            //los=0
                            //defferred from site to supplier (vat and rent)
                            _termsResults.SupplierRent = _termsResults.SupplierRentShouldGet.GetValueOrDefault() + _termsResults.SupplierShareShouldGet.GetValueOrDefault();
                            _termsResults.SupplierVat = SystemVATRate * _termsResults.SupplierRent.GetValueOrDefault();
                            _termsResults.SiteVat = 0 - _termsResults.SupplierVat.GetValueOrDefault();
                            _termsResults.VATLOS = 0;
                            _termsResults.VATBankedToSupplier = 0;
                            _termsResults.SupplierVATLOS = _termsResults.SupplierVat.GetValueOrDefault();
                            _termsResults.SiteVATLos = _termsResults.SiteVat.GetValueOrDefault();
                            _termsResults.LicenceChargeBankedToSupplier = 0;
                            _termsResults.LicenceChargeLOS = _termsResults.LicenceCharge.GetValueOrDefault();
                            _termsResults.SiteRent = _termsResults.SiteRent.GetValueOrDefault() - _termsResults.SupplierRent.GetValueOrDefault();
                            _termsResults.SupplierShareLOS = _termsResults.SupplierRent.GetValueOrDefault();
                            _termsResults.SiteShareLOS = _termsResults.SiteRent.GetValueOrDefault();
                            _termsResults.BankedToCompany = 0;
                            _termsResults.BankedToSupplier = 0;

                            //Add defferred
                            //supplier vat to c&e
                            _termsResults.VATSupplierLos_toSupplier = _termsResults.SupplierVat.GetValueOrDefault();
                            _termsResults.SupplierShareLOS_toSupplier = _termsResults.SupplierRent.GetValueOrDefault();
                            break;
                        case TERMS_INVOICED:
                            //will not come through here - ignore it
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    //#################
                    //     STEP 1
                    //#################

                    //Process Other Charges that come before the Output VAT and Come from the Cash Box
                    for (int i = 1; i <= 16; i++)
                    {
                        //OTHER - Licence
                        if (_termsInfo.OtherLicenceUse == i)
                        {
                            if (_termsInfo.OtherLicencePaidBy == TERMS_CASH_BOX && _termsInfo.OtherLicenceUse < _termsInfo.VATOutputUse)
                                CalculatePostTermsOtherLicence();
                        }

                        //OTHER - Prize
                        else if (_termsInfo.OtherPrizeUse == i && _termsInfo.OtherPrizePaidBy == TERMS_CASH_BOX)
                        {
                            //The Prize Calculations have to be worked out twice...
                            //here (if the machine in question is a cash prize)
                            //then at the very end (if it is a goods prize)
                            //We can find out which the machine is by looking at the Machine_Class_Table

                            if (_termsInfo.OtherPrizeUse < _termsInfo.VATOutputUse && _termsInfo.VATOutputUse > 0)
                            {
                                if (_termsInfo.PrizeType.ToUpper() != "GOODS")
                                    CalculatePostTermsOtherPrizeCash();
                            }
                        }

                        //OTHER - Consultancy
                        else if (_termsInfo.OtherConsultancyUse == i && _termsInfo.OtherConsultancyPaidBy == TERMS_CASH_BOX)
                        {
                            if (_termsInfo.OtherConsultancyUse < _termsInfo.VATOutputUse && _termsInfo.VATOutputUse > 0)
                                CalculatePostTermsOtherConsultancy();
                        }

                        //OTHER - Royalty
                        else if (_termsInfo.OtherRoyaltyUse == i && _termsInfo.OtherRoyaltyPaidBy == TERMS_CASH_BOX)
                        {
                            if (_termsInfo.OtherRoyaltyUse < _termsInfo.VATOutputUse && _termsInfo.VATOutputUse > 0)
                                CalculatePostTermsOtherRoyalty();
                        }

                        //OTHER - Other1
                        else if (_termsInfo.OtherOther1Use == i && _termsInfo.OtherOther1PaidBy == TERMS_CASH_BOX)
                        {
                            if (_termsInfo.OtherOther1Use < _termsInfo.VATOutputUse && _termsInfo.VATOutputUse > 0)
                                CalculatePostTermsOtherOther1();
                        }

                        //OTHER - Other2
                        else if (_termsInfo.OtherOther2Use == i && _termsInfo.OtherOther2PaidBy == TERMS_CASH_BOX)
                        {
                            if (_termsInfo.OtherOther2Use < _termsInfo.VATOutputUse && _termsInfo.VATOutputUse > 0)
                                CalculatePostTermsOtherOther2();
                        }
                    }

                    //#################
                    //     STEP 2
                    //#################

                    //Process the Output VAT        
                    if (_termsInfo.VATOutputUse > 0)
                        CalculatePostTermsVatOutput();

                    //Process the GPT        
                    if (_termsInfo.GPTUse > 0)
                        CalculatePostTermsGPT();

                    //#################
                    //     STEP 3
                    //#################

                    //need to loop through and take off the rental from all items that come from the cashbox
                    //Partners:
                    //    TERMS_RENT
                    //    TERMS_RENT_FIXED
                    //    TERMS_RENT_FULL
                    //    TERMS_RENT_ONLY
                    //    TERMS_FRONT_MONEY (just the rent bit)
                    //Other Charges
                    //    Only if they come from the cash box... and are after the Output Vat (Or if output Vat isn't used)

                    //then we can process the following loop (but remove all of the above rental deductions...

                    for (int i = 1; i <= 16; i++)
                    {
                        //PARTNERS - Supplier
                        if (_termsInfo.SupplierUse == i)
                        {
                            //If GPT, calc shares before rent as shares are calculated on the total contribution
                            if (_termsInfo.GPTUse == 1)
                            {
                                CalculatePostTermsPartnerSupplierSharesOnly();
                                CalculatePostTermsPartnerSupplierRentOnly();
                            }
                            else
                            {
                                CalculatePostTermsPartnerSupplierRentOnly();
                                CalculatePostTermsPartnerSupplierSharesOnly();
                            }
                        }

                        //PARTNERS - Site
                        else if (_termsInfo.SiteUse == i)
                        {
                            CalculatePostTermsPartnerSiteRentOnly();
                            CalculatePostTermsPartnerSiteSharesOnly();
                        }

                        //PARTNERS - Site Group
                        else if (_termsInfo.GroupUse == i)
                        {
                            CalculatePostTermsPartnerSiteGroupRentOnly();
                            CalculatePostTermsPartnerSiteGroupSharesOnly();
                        }

                        //PARTNERS - Sec Site Group
                        else if (_termsInfo.SecGroupUse == i)
                        {
                            CalculatePostTermsPartnerSecSiteGroupRentOnly();
                            CalculatePostTermsPartnerSecSiteGroupSharesOnly();
                        }

                        //OTHER - Licence
                        else if (_termsInfo.OtherLicenceUse == i && _termsInfo.OtherLicencePaidBy == TERMS_CASH_BOX)
                        {
                            if (_termsInfo.OtherLicenceUse > _termsInfo.VATOutputUse || _termsInfo.VATOutputUse == 0)
                                CalculatePostTermsOtherLicence();
                        }

                        //OTHER - Prize
                        else if (_termsInfo.OtherPrizeUse == i && _termsInfo.OtherPrizePaidBy == TERMS_CASH_BOX)
                        {
                            //The Prize Calculations have to be worked out twice...
                            //here (if the machine in question is a cash prize)
                            //then at the very end (if it is a goods prize)
                            //We can find out which the machine is by looking at the Machine_Class_Table

                            if (_termsInfo.OtherPrizeUse > _termsInfo.VATOutputUse || _termsInfo.VATOutputUse == 0)
                            {
                                if (_termsInfo.PrizeType.ToUpper() != "GOODS")
                                    CalculatePostTermsOtherPrizeCash();
                            }
                        }

                        //OTHER - Consultancy
                        else if (_termsInfo.OtherConsultancyUse == i && _termsInfo.OtherConsultancyPaidBy == TERMS_CASH_BOX)
                        {
                            if (_termsInfo.OtherConsultancyUse > _termsInfo.VATOutputUse || _termsInfo.VATOutputUse == 0)
                                CalculatePostTermsOtherConsultancy();
                        }

                        //OTHER - Royalty
                        else if (_termsInfo.OtherRoyaltyUse == i && _termsInfo.OtherRoyaltyPaidBy == TERMS_CASH_BOX)
                        {
                            if (_termsInfo.OtherRoyaltyUse > _termsInfo.VATOutputUse || _termsInfo.VATOutputUse == 0)
                                CalculatePostTermsOtherRoyalty();
                        }

                        //OTHER - Other1
                        else if (_termsInfo.OtherOther1Use == i && _termsInfo.OtherOther1PaidBy == TERMS_CASH_BOX)
                        {
                            if (_termsInfo.OtherOther1Use > _termsInfo.VATOutputUse || _termsInfo.VATOutputUse == 0)
                                CalculatePostTermsOtherOther1();
                        }

                        //OTHER - Other2
                        else if (_termsInfo.OtherOther2Use == i && _termsInfo.OtherOther2PaidBy == TERMS_CASH_BOX)
                        {
                            if (_termsInfo.OtherOther2Use > _termsInfo.VATOutputUse || _termsInfo.VATOutputUse == 0)
                                CalculatePostTermsOtherOther2();
                        }
                    }

                    //#################
                    //     STEP 4
                    //#################
                    //Now we can loop through again, but this time looking at everything else
                    //apart from Rent and Other charges from the Cash Box
                    //For partners, this is the shares..
                    //For other charges, it is deductions from suppliers

                    for (int i = 1; i <= 16; i++)
                    {
                        //Find which item is to be calculated...

                        //For all of the Other Charges, only calculate them if they do not come from
                        //the cash box

                        //OTHER - Licence
                        if (_termsInfo.OtherLicenceUse == i && _termsInfo.OtherLicencePaidBy != TERMS_CASH_BOX)
                            CalculatePostTermsOtherLicence();

                        //OTHER - Prize
                        else if (_termsInfo.OtherPrizeUse == i && _termsInfo.OtherPrizePaidBy != TERMS_CASH_BOX)
                        {
                            //The Prize Calculations have to be worked out twice...
                            //here (if the machine in question is a cash prize)
                            //then at the very end (if it is a goods prize)
                            //We can find out which the machine is by looking at the Machine_Class_Table

                            if (_termsInfo.PrizeType.ToUpper() != "GOODS")
                                CalculatePostTermsOtherPrizeCash();
                        }

                        //OTHER - Consultancy
                        else if (_termsInfo.OtherConsultancyUse == i && _termsInfo.OtherConsultancyPaidBy != TERMS_CASH_BOX)
                            CalculatePostTermsOtherConsultancy();

                        //OTHER - Royalty
                        else if (_termsInfo.OtherRoyaltyUse == i && _termsInfo.OtherRoyaltyPaidBy != TERMS_CASH_BOX)
                            CalculatePostTermsOtherRoyalty();

                        //OTHER - Other1
                        else if (_termsInfo.OtherOther1Use == i && _termsInfo.OtherOther1PaidBy != TERMS_CASH_BOX)
                            CalculatePostTermsOtherOther1();

                        //OTHER - Other2
                        else if (_termsInfo.OtherOther2Use == i && _termsInfo.OtherOther2PaidBy != TERMS_CASH_BOX)
                            CalculatePostTermsOtherOther2();
                    }

                    //we have now worked out for each partner (and Other) how much they should get, and also
                    //how much the cash box has allowed them to have
                    //we can now process the shortfall calculations to see how much they all get in the end

                    //#################
                    //     STEP 5
                    //#################

                    //Shortfall guarantees
                    //for each partnerand other charge, work out how much they actually get...

                    for (int i = 1; i <= 16; i++)
                    {
                        if (_termsInfo.SupplierUse == i)
                            CalculatePostTermsSupplierShortfall();

                        if (_termsInfo.SiteUse == i)
                            CalculatePostTermsSiteShortfall();

                        if (_termsInfo.GroupUse == i)
                            CalculatePostTermsSiteGroupShortfall();

                        if (_termsInfo.SecGroupUse == i)
                            CalculatePostTermsSecSiteGroupShortfall();

                        if (_termsInfo.OtherLicenceUse == i)
                            CalculatePostTermsOtherLicenceShortfall();

                        if (_termsInfo.OtherPrizeUse == i)
                            CalculatePostTermsOtherPrizeShortfall();

                        if (_termsInfo.OtherConsultancyUse == i)
                            CalculatePostTermsOtherConsultancyShortfall();

                        if (_termsInfo.OtherRoyaltyUse == i)
                            CalculatePostTermsOtherRoyaltyShortfall();

                        if (_termsInfo.OtherOther1Use == i)
                            CalculatePostTermsOtherOther1Shortfall();

                        if (_termsInfo.OtherOther2Use == i)
                            CalculatePostTermsOtherOther2Shortfall();
                    }

                    //we now know exactly how much they all get
                    //so we can work out the partners VAT

                    //#################
                    //     STEP 6
                    //#################

                    //process them in order..
                    for (int i = 1; i <= 16; i++)
                    {
                        if (_termsInfo.VATSupplierUse > 0 && _termsInfo.SupplierUse > 0 && _termsInfo.VATSupplierUse == i)
                        {
                            if (_termsInfo.GPTUse == 1)
                                CalculatePostTermsVATSupplierWhenGPT();
                            else
                                CalculatePostTermsVatSupplier();
                        }

                        if (_termsInfo.VATSiteUse > 0 && _termsInfo.SiteUse > 0 && _termsInfo.VATSiteUse == i)
                            CalculatePostTermsVatSite();

                        if (_termsInfo.VATGroupUse > 0 && _termsInfo.GroupUse > 0 && _termsInfo.VATGroupUse == i)
                            CalculatePostTermsVatSiteGroup();

                        if (_termsInfo.VatSecGroupUse > 0 && _termsInfo.SecGroupUse > 0 && _termsInfo.VatSecGroupUse == i)
                            CalculatePostTermsVatSecSiteGroup();
                    }

                    //#################
                    //     STEP 7
                    //#################

                    //process the partners' vat in order
                    for (int i = 1; i <= 16; i++)
                    {
                        if (_termsInfo.SupplierUse == i)
                            CalculatePostTermsVatSupplierShortfall();

                        if (_termsInfo.SiteUse == i)
                            CalculatePostTermsVatSiteShortfall();

                        if (_termsInfo.GroupUse == i)
                            CalculatePostTermsVatSiteGroupShortfall();

                        if (_termsInfo.SecGroupUse == i)
                            CalculatePostTermsVatSecSiteGroupShortfall();
                    }
                    //Now rebuild the output vat
                    if (_termsInfo.VATOutputUse != null && _termsInfo.VATOutputUse == 1)
                        _termsResults.OutputVAT = _termsResults.CollectionValueOutputVatable.GetValueOrDefault() - (_termsResults.CollectionValueOutputVatable.GetValueOrDefault() / (1 + SystemVATRate));

                    //#################
                    //     STEP 8
                    //#################

                    //Process all other charges (that do not come from the cash box)

                    //now work out Prize calculation (if prize type = "Goods")
                    //Do this at the very end
                    if (_termsInfo.PrizeType != null && _termsInfo.PrizeType.ToUpper() == "GOODS" && _termsInfo.OtherPrizeUse > 0)
                        CalculatePostTermsOtherPrizeGoods();

                    //#################
                    //     STEP 9
                    //#################

                    //Sort out what has to go to where
                    //may as well do it in order

                    for (int i = 1; i <= 16; i++)
                    {
                        //Find which item is to be calculated...
                        //PARTNERS - Supplier
                        if (_termsInfo.SupplierUse == i)
                            AllocateSupplierMoneyToDestination();

                        //PARTNERS - Site
                        else if (_termsInfo.SiteUse == i)
                            AllocateSiteMoneyToDestination();

                        //PARTNERS - Site Group
                        else if (_termsInfo.GroupUse == i)
                            AllocateSiteGroupMoneyToDestination();

                        else if (_termsInfo.SecGroupUse == i)
                            AllocateSecSiteGroupMoneyToDestination();

                        //else if (_termsInfo.VATOutputUse == i)
                        //AllocateOutputVatMoneyToDestination();

                        else if (_termsInfo.VATSupplierUse == i)
                            AllocateSupplierVatMoneyToDestination();

                        else if (_termsInfo.VATSiteUse == i)
                            AllocateSiteVatMoneyToDestination();

                        else if (_termsInfo.VATGroupUse == i)
                            AllocateSiteGroupVatMoneyToDestination();

                        else if (_termsInfo.VatSecGroupUse == i)
                            AllocateSecSiteGroupVatMoneyToDestination();

                        else if (_termsInfo.GPTUse == i)
                            AllocateGPTMoneyToDestination();

                        //OTHER - Licence
                        else if (_termsInfo.OtherLicenceUse == i)
                            AllocateLicenceMoneyToDestination();

                        //OTHER - Prize
                        else if (_termsInfo.OtherPrizeUse == i)
                            AllocatePrizeMoneyToDestination();

                        //OTHER - Consultancy
                        else if (_termsInfo.OtherConsultancyUse == i)
                            AllocateConsultancyMoneyToDestination();

                        //OTHER - Royalty
                        else if (_termsInfo.OtherRoyaltyUse == i)
                            AllocateRoyaltyMoneyToDestination();

                        //OTHER - Other1
                        else if (_termsInfo.OtherOther1Use == i)
                            AllocateOther1MoneyToDestination();

                        //OTHER - Other2
                        else if (_termsInfo.OtherOther2Use == i)
                            AllocateOther2MoneyToDestination();
                    }

                    //#################
                    //     STEP 10
                    //#################

                    //Supplier Supplemental Charge
                    CalculatePostTermsSupplierSupplementalCharge();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return true;
        }

        private void CalculatePostTermsPartnerSupplierRentOnly()
        {
            float? _tempRent = 0;
            float? _tempRentFormatted = 0;
            float? _supplierRent = 0;
            float? _supplierRentFormatted = 0;
            float? _dailyRate = 0;
            int? _numOfDays = 0;
            int? _tempDays = 0;
            int? _numOfWeeks = 0;
            int? _collectionPeriod = 0;
            bool? _bUsedFuture;
            string _previousDate = string.Empty;
            string _futureDate = string.Empty;
            DateTime _collectionStartDate;
            DateTime _collectionEndDate;

            try
            {
                LogManager.WriteLog("Inside CalculatePostTermsPartnerSupplierRentOnly...", LogManager.enumLogLevel.Info);

                _collectionPeriod = 7;

                if (_termsInfo.SupplierType.GetValueOrDefault() == TERMS_RENT_FULL || _termsInfo.SupplierType.GetValueOrDefault() == TERMS_RENT_SCHEDULE
                    || _termsInfo.SupplierType.GetValueOrDefault() == TERMS_FRONT_MONEY || _termsInfo.SupplierType.GetValueOrDefault() == TERMS_RENT_ONLY)
                    _numOfDays = _termsResults.CollectionDays.GetValueOrDefault() - _termsInfo.DownDays;
                else
                    _numOfDays = _termsResults.CollectionDays.GetValueOrDefault();

                switch (_termsInfo.SupplierType)
                {
                    case TERMS_RENT_FULL:
                    case TERMS_RENT_ONLY:
                    case TERMS_FRONT_MONEY:
                    case TERMS_RENT_SCHEDULE:
                    case TERMS_SHARE_SCHEDULE:
                        if (_termsInfo.UseSplitRents.GetValueOrDefault())
                        {
                            _previousDate = IsValidDateTime(_termsInfo.SupplierValueBeforeChangeDate)
                                ? Convert.ToDateTime(_termsInfo.SupplierValueBeforeChangeDate).ToString("dd/MMM/yyyy") : Convert.ToDateTime(_termsInfo.CollectionDate).AddYears(-10).ToString("dd/MMM/yyyy");

                            _futureDate = IsValidDateTime(_termsInfo.SupplierValueAfterChangeDate)
                                ? Convert.ToDateTime(_termsInfo.SupplierValueAfterChangeDate).ToString("dd/MMM/yyyy") : Convert.ToDateTime(_termsInfo.CollectionDate).AddYears(10).ToString("dd/MMM/yyyy");

                            //AW 20051004 - added in down days to the calc
                            _collectionStartDate = Convert.ToDateTime(_termsInfo.CollectionDate).AddDays(0 - (_termsInfo.CollectionDays.GetValueOrDefault() - _termsInfo.DownDays.GetValueOrDefault()) + 1);
                            _collectionEndDate = Convert.ToDateTime(_termsInfo.CollectionDate);

                            if (IsValidDateTime(_termsInfo.SupplierValueBeforeChangeDate))
                            {
                                if ((_collectionStartDate - Convert.ToDateTime(_termsInfo.SupplierValueBeforeChangeDate)).TotalDays < 0)
                                {
                                    _tempDays = Convert.ToInt32((_collectionStartDate - Min<DateTime>(Convert.ToDateTime(_termsInfo.SupplierValueBeforeChangeDate), _collectionEndDate.AddDays(1))).TotalDays);

                                    if (_tempDays > 0)
                                    {
                                        _dailyRate = _termsInfo.SupplierValueBefore / _collectionPeriod;
                                        _tempRent = (_dailyRate * _tempDays);
                                        _numOfDays = _numOfDays - _tempDays;
                                        _termsResults.SupplierWeeklyRent = _dailyRate * 7;
                                        _collectionStartDate = _collectionStartDate.AddDays(Convert.ToDouble(_tempDays));
                                    }
                                }
                            }

                            _bUsedFuture = false;
                            if (IsValidDateTime(_termsInfo.SupplierValueAfterChangeDate))
                            {
                                if ((Convert.ToDateTime(_termsInfo.SupplierValueAfterChangeDate).AddDays(-1) - _collectionEndDate).TotalDays <= 0)
                                {
                                    _tempDays = Convert.ToInt32(((Max<DateTime>(_collectionStartDate.AddDays(-1), Convert.ToDateTime(_termsInfo.SupplierValueAfterChangeDate).AddDays(-1)) - _collectionEndDate).TotalDays));

                                    if (_tempDays > 0)
                                    {
                                        _dailyRate = _termsInfo.SupplierValueAfter / _collectionPeriod;
                                        _tempRent = _tempRent + (_dailyRate * _tempDays);
                                        _numOfDays = _numOfDays - _tempDays;
                                        _termsResults.SupplierWeeklyRent = _dailyRate * 7;
                                        _collectionEndDate = _collectionEndDate.AddDays(Convert.ToDouble(0 - _tempDays));
                                        _bUsedFuture = true;
                                    }
                                }
                            }

                            if (_numOfDays > 0)
                            {
                                //Charge some at current
                                _dailyRate = _termsInfo.SupplierValue / _collectionPeriod;
                                _tempRent = _tempRent + (_dailyRate * _numOfDays);
                                if (!_bUsedFuture.GetValueOrDefault())
                                    _termsResults.SupplierWeeklyRent = _dailyRate * 7;
                            }
                        }
                        else
                        {
                            //if afterdate is valid
                            if (IsValidDateTime(_termsInfo.SupplierValueAfterChangeDate))
                            {
                                if ((Convert.ToDateTime(_termsInfo.SupplierValueAfterChangeDate) - Convert.ToDateTime(_termsInfo.CollectionDate)).TotalDays >= 0) //if col is after the after date, use after value
                                {
                                    _dailyRate = _termsInfo.SupplierValueAfter / _collectionPeriod;
                                    _tempRent = _numOfDays * _dailyRate;
                                    _termsResults.SupplierWeeklyRent = 7 * _dailyRate;
                                }
                                else if (IsValidDateTime(_termsInfo.SupplierValueBeforeChangeDate)) //if the before date is valid
                                {
                                    if ((Convert.ToDateTime(_termsInfo.SupplierValueBeforeChangeDate) - Convert.ToDateTime(_termsInfo.CollectionDate)).TotalDays >= 0) //if col is after the before date, use current value
                                    {
                                        _dailyRate = _termsInfo.SupplierValue / _collectionPeriod;
                                        _tempRent = _numOfDays * _dailyRate;
                                        _termsResults.SupplierWeeklyRent = 7 * _dailyRate;
                                    }
                                    else // else use before
                                    {
                                        _dailyRate = _termsInfo.SupplierValueBefore / _collectionPeriod;
                                        _tempRent = _numOfDays * _dailyRate;
                                        _termsResults.SupplierWeeklyRent = 7 * _dailyRate;
                                    }
                                }
                                else //the before date is invalid so use current
                                {
                                    _dailyRate = _termsInfo.SupplierValue / _collectionPeriod;
                                    _tempRent = _numOfDays * _dailyRate;
                                    _termsResults.SupplierWeeklyRent = 7 * _dailyRate;
                                }
                            }

                            //if the before date is valid
                            else if (IsValidDateTime(_termsInfo.SupplierValueBeforeChangeDate))
                            {
                                if (((Convert.ToDateTime(_termsInfo.SupplierValueBeforeChangeDate) - Convert.ToDateTime(_termsInfo.CollectionDate)).TotalDays >= 0)) //if col is after the before date, use current value
                                {
                                    _dailyRate = _termsInfo.SupplierValue / _collectionPeriod;
                                    _tempRent = _numOfDays * _dailyRate;
                                    _termsResults.SupplierWeeklyRent = 7 * _dailyRate;
                                }
                                else //else use before
                                {
                                    _dailyRate = _termsInfo.SupplierValueBefore / _collectionPeriod;
                                    _tempRent = _numOfDays * _dailyRate;
                                    _termsResults.SupplierWeeklyRent = 7 * _dailyRate;
                                }
                            }

                            //the before date is invalid so use current
                            else
                            {
                                _dailyRate = _termsInfo.SupplierValue / _collectionPeriod;
                                _tempRent = _numOfDays * _dailyRate;
                                _termsResults.SupplierWeeklyRent = 7 * _dailyRate;
                            }
                        }
                        break;
                    case TERMS_RENT_FIXED:
                        //need to find out how many days are 'outstanding'
                        //if it is below 10 or below, charge for 7 days, if above, charge for 14
                        //or 21 etc.

                        //Does not do Split rent!!!
                        //do we need to check bar position override?
                        _numOfDays = Convert.ToInt32((Convert.ToDateTime(_termsInfo.RentPaidUntil) - System.DateTime.Now).TotalDays);
                        _numOfWeeks = ((_numOfDays + (_collectionPeriod / 2)) / _collectionPeriod);

                        _tempRent = _numOfWeeks * _termsInfo.SupplierValue * (_collectionPeriod / 7);
                        _termsResults.SupplierWeeklyRent = _termsInfo.SupplierValue;
                        break;
                    default:
                        break;
                }

                _tempRentFormatted = Format2DPs(_tempRent.GetValueOrDefault());
                _termsResults.SupplierRentShouldGet = _tempRentFormatted != -1 ? _tempRentFormatted : _tempRent;

                if (_termsInfo.GPTUse.GetValueOrDefault() == 1)
                    _termsResults.SupplierRentShouldGet = _termsResults.SupplierRentShouldGet.GetValueOrDefault() * (1 + SystemVATRate);

                _supplierRent = Min<float>(_termsResults.SupplierRentShouldGet.GetValueOrDefault(), (Max<float>(_termsResults.Remainder.GetValueOrDefault(), 0)));
                _supplierRentFormatted = Format2DPs(_supplierRent.GetValueOrDefault());
                _termsResults.SupplierRent = _supplierRentFormatted != -1 ? _supplierRentFormatted : _supplierRent;

                _termsResults.Remainder = _termsResults.Remainder.GetValueOrDefault() - _termsResults.SupplierRent.GetValueOrDefault();

                //Do not work out where the money goes, that has to be done later as we haven't yet done the shortfalls...
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void CalculatePostTermsPartnerSupplierSharesOnly()
        {
            float? _tempShares = 0;
            float? _sharesToGo = 0;
            long? _colDays = 0;
            float? _supShare = 0;
            float? _locShare = 0;
            float? _groupShare = 0;
            float? _secGroupShare = 0;
            float? _supplierShare = 0;
            float? _supplierShareFormatted = 0;

            try
            {
                LogManager.WriteLog("Inside CalculatePostTermsPartnerSupplierSharesOnly...", LogManager.enumLogLevel.Info);

                //if the Supplier is before the Output vat, then
                //  Take % from CashBoxVatable and decrement the remainder
                //if it is after, then
                //  Take % from remainder and decrement remainder

                //if shares are used...
                //to work out the true %, we need to find the sum of all remaining shares
                //and work out the % of that, not 100.
                //this is because other charges may be made between calculating each
                //individual share, thus adding up to a value above the cash box value

                _colDays = _termsInfo.CollectionDays;
                _supShare = _termsInfo.SupplierShare;
                _locShare = _termsInfo.SiteShare;
                _groupShare = _termsInfo.GroupShare;
                _secGroupShare = _termsInfo.SecGroupShare;

                _sharesToGo = _supShare;
                if (_termsInfo.SiteUse > 0 && (_termsInfo.SiteType == TERMS_MIN || _termsInfo.SiteType == TERMS_MAX || _termsInfo.SiteType == TERMS_FRONT_MONEY) && _termsInfo.SiteUse > _termsInfo.SupplierUse)
                    _sharesToGo = _sharesToGo + _locShare;

                if (_termsInfo.GroupUse > 0 && (_termsInfo.GroupType == TERMS_MIN || _termsInfo.GroupType == TERMS_MAX || _termsInfo.GroupType == TERMS_FRONT_MONEY) && _termsInfo.GroupUse > _termsInfo.SupplierUse)
                    _sharesToGo = _sharesToGo + _groupShare;

                if (_termsInfo.SecGroupUse > 0 && (_termsInfo.SecGroupType == TERMS_MIN || _termsInfo.SecGroupType == TERMS_MAX || _termsInfo.SecGroupType == TERMS_FRONT_MONEY) && _termsInfo.SecGroupUse > _termsInfo.SupplierUse)
                    _sharesToGo = _sharesToGo + _secGroupShare;

                if (_sharesToGo == 0) return;

                _termsResults.SupplierPerc = _supShare;

                //Calculate required shares
                switch (_termsInfo.SupplierType)
                {
                    case TERMS_FRONT_MONEY:
                    case TERMS_SHARE_SCHEDULE:
                        //then calculate share
                        //if the supplier share is after output vat, then no problem
                        //if it is b4, shares have to be modified to reflect it                        
                        if (_termsInfo.VATOutputUse > 0 && _termsInfo.SupplierUse < _termsInfo.VATOutputUse)
                        {
                            //before output vat
                            _tempShares = _supShare;
                            _tempShares = _tempShares / _sharesToGo;
                            _tempShares = _tempShares * _termsResults.CollectionValueOutputVatable.GetValueOrDefault();
                        }
                        else //Output vat is not used, or is b4 supplier share
                        {
                            if (_sharesToGo > 0)
                            {
                                _tempShares = _supShare;
                                _tempShares = _tempShares / 100;
                                _tempShares = _tempShares * _termsResults.CollectionValue.GetValueOrDefault();
                            }
                        }
                        break;
                    case TERMS_MIN:
                        if (_termsInfo.VATOutputUse > 0 && (_termsInfo.SupplierUse < _termsInfo.VATOutputUse))
                        {
                            if (_sharesToGo > 0)
                            {
                                _tempShares = _supShare;
                                _tempShares = _tempShares / _sharesToGo;
                                _tempShares = _tempShares * _termsResults.CollectionValueOutputVatable.GetValueOrDefault();
                                _tempShares = Max<float>(_tempShares.GetValueOrDefault(), ((_termsInfo.SupplierValue.GetValueOrDefault() / 7) * (float)_colDays));
                            }
                            else
                            {
                                _tempShares = _supShare;
                                _tempShares = _tempShares / _sharesToGo;
                                _tempShares = _tempShares * _termsResults.Remainder.GetValueOrDefault();
                                _tempShares = Max<float>(_tempShares.GetValueOrDefault(), ((_termsInfo.SupplierValue.GetValueOrDefault() / 7) * (float)_colDays));
                            }
                        }
                        break;
                    case TERMS_MAX:
                        if (_termsInfo.VATOutputUse > 0 && _termsInfo.SupplierUse < _termsInfo.VATOutputUse)
                        {
                            _tempShares = _supShare;
                            _tempShares = _tempShares / _sharesToGo;
                            _tempShares = _tempShares * _termsResults.CollectionValueOutputVatable.GetValueOrDefault();
                            _tempShares = Min<float>(_tempShares.GetValueOrDefault(), ((_termsInfo.SupplierValue.GetValueOrDefault() / 7) * (float)_colDays));
                        }
                        else
                        {
                            _tempShares = _supShare;
                            _tempShares = _tempShares / _sharesToGo;
                            _tempShares = _tempShares * _termsResults.Remainder.GetValueOrDefault();
                            _tempShares = Min<float>(_tempShares.GetValueOrDefault(), ((_termsInfo.SupplierValue.GetValueOrDefault() / 7) * (float)_colDays));
                        }
                        break;
                    default:
                        break;
                }

                _supplierShare = _tempShares;
                _supplierShareFormatted = Format2DPs(_tempShares.GetValueOrDefault());
                _termsResults.SupplierShareShouldGet = _supplierShareFormatted != -1 ? _supplierShareFormatted : _supplierShare;

                //if GPT, then share should be VAT inclusive
                if (_termsInfo.GPTUse.GetValueOrDefault() == 1)
                    _termsResults.SupplierShareShouldGet = _termsResults.SupplierShareShouldGet.GetValueOrDefault() * (1 + SystemVATRate);

                _termsResults.SupplierShare = Format2DPs(Min<float>(_termsResults.SupplierShareShouldGet.GetValueOrDefault(), Max<float>(_termsResults.CollectionValue.GetValueOrDefault(), 0)));

                _termsResults.Remainder = _termsResults.Remainder.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault();

                //Do not work out where the money goes, that has to be done later as we haven't yet done the shortfalls...
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void CalculatePostTermsPartnerSiteRentOnly()
        {
            float _tempRent = 0;
            try
            {
                LogManager.WriteLog("Inside CalculatePostTermsPartnerSiteRentOnly...", LogManager.enumLogLevel.Info);

                //this function is only used to calculate the rent
                //the shares will be calculated later

                //calculate required value
                switch (_termsInfo.SiteType)
                {
                    case TERMS_RENT:
                    case TERMS_FRONT_MONEY:
                        _tempRent = _termsInfo.SiteValue.GetValueOrDefault();
                        break;
                    default:
                        break;
                }
                _termsResults.SiteRentShouldGet = Format2DPs(_tempRent);
                _termsResults.SiteRent = Format2DPs(Min<float>(_termsResults.SiteRentShouldGet.GetValueOrDefault(), Max<float>(_termsResults.Remainder.GetValueOrDefault(), 0)));
                _termsResults.Remainder = _termsResults.Remainder.GetValueOrDefault() - _termsResults.SiteRent.GetValueOrDefault();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void CalculatePostTermsPartnerSiteSharesOnly()
        {
            float? _tempShares = 0;
            float? _sharesToGo = 0;
            long? _colDays = 0;
            float? _supShare = 0;
            float? _locShare = 0;
            float? _groupShare = 0;
            float? _secGroupShare = 0;

            try
            {
                LogManager.WriteLog("Inside CalculatePostTermsPartnerSiteSharesOnly...", LogManager.enumLogLevel.Info);

                //this function is only used to calculate the shares
                //the rent has already been calculated

                //if the Site is before the Output vat, then
                //   Take % from CashBoxVatable and decrement the remainder
                //if it is after, then
                //   Take % from remainder and decrement remainder

                _colDays = _termsInfo.CollectionDays;
                _supShare = _termsInfo.SupplierShare;
                _locShare = _termsInfo.SiteShare;
                _groupShare = _termsInfo.GroupShare;
                _secGroupShare = _termsInfo.SecGroupShare;

                //if shares are used...
                //to work out the true %, we need to find the sum of all remaining shares
                //and work out the % of that, not 100.
                //this is because other charges may be made between calculating each
                //individual share, thus adding up to a value above the cash box value

                _sharesToGo = _locShare;
                if (_termsInfo.SupplierUse > 0 && (_termsInfo.SupplierType == TERMS_SHARE_SCHEDULE || _termsInfo.SupplierType == TERMS_MIN || _termsInfo.SupplierType == TERMS_MAX || _termsInfo.SupplierType == TERMS_FRONT_MONEY) && _termsInfo.SupplierUse > _termsInfo.SiteUse)
                    _sharesToGo = _sharesToGo + _supShare;
                if (_termsInfo.GroupUse > 0 && (_termsInfo.GroupType == TERMS_MIN || _termsInfo.GroupType == TERMS_MAX || _termsInfo.GroupType == TERMS_FRONT_MONEY) && _termsInfo.GroupUse > _termsInfo.SiteUse)
                    _sharesToGo = _sharesToGo + _groupShare;
                if (_termsInfo.SecGroupUse > 0 && (_termsInfo.SecGroupType == TERMS_MIN || _termsInfo.SecGroupType == TERMS_MAX || _termsInfo.SecGroupType == TERMS_FRONT_MONEY) && _termsInfo.SecGroupUse > _termsInfo.SiteUse)
                    _sharesToGo = _sharesToGo + _secGroupShare;
                if (_sharesToGo == 0) return;

                _termsResults.SitePerc = _locShare;

                //calculate required value
                switch (_termsInfo.SiteType)
                {
                    case TERMS_MIN:
                        if (_termsInfo.VATOutputUse > 0 && _termsInfo.SiteUse < _termsInfo.VATOutputUse)
                        {
                            _tempShares = _locShare;
                            _tempShares = _tempShares / _sharesToGo;
                            _tempShares = _tempShares * _termsResults.CollectionValueOutputVatable.GetValueOrDefault();
                            _tempShares = Max<float>(_tempShares.GetValueOrDefault(), ((_termsInfo.SiteValue.GetValueOrDefault() / 7) * (float)_colDays));
                        }
                        else
                        {
                            _tempShares = _locShare;
                            _tempShares = _tempShares / _sharesToGo;
                            _tempShares = _tempShares * _termsResults.Remainder.GetValueOrDefault();
                            _tempShares = Max<float>(_tempShares.GetValueOrDefault(), ((_termsInfo.SiteValue.GetValueOrDefault() / 7) * (float)_colDays));
                        }
                        break;
                    case TERMS_MAX:
                        if (_termsInfo.VATOutputUse > 0 && _termsInfo.SiteUse < _termsInfo.VATOutputUse)
                        {
                            _tempShares = _locShare;
                            _tempShares = _tempShares / _sharesToGo;
                            _tempShares = _tempShares * _termsResults.CollectionValueOutputVatable.GetValueOrDefault();
                            _tempShares = Min<float>(_tempShares.GetValueOrDefault(), ((_termsInfo.SiteValue.GetValueOrDefault() / 7) * (float)_colDays));
                        }
                        else
                        {
                            _tempShares = _locShare;
                            _tempShares = _tempShares / _sharesToGo;
                            _tempShares = _tempShares * _termsResults.Remainder.GetValueOrDefault();
                            _tempShares = Min<float>(_tempShares.GetValueOrDefault(), ((_termsInfo.SiteValue.GetValueOrDefault() / 7) * (float)_colDays));
                        }
                        break;
                    case TERMS_FRONT_MONEY:
                        if (_termsInfo.VATOutputUse > 0 && _termsInfo.SiteUse < _termsInfo.VATOutputUse)
                        {
                            _tempShares = _locShare;
                            _tempShares = _tempShares / _sharesToGo;
                            _tempShares = _tempShares * _termsResults.CollectionValueOutputVatable.GetValueOrDefault();
                        }
                        else
                        {
                            _tempShares = _locShare;
                            _tempShares = _tempShares / 100;
                            _tempShares = _tempShares * _termsResults.CollectionValue.GetValueOrDefault();
                        }
                        break;
                    default:
                        break;
                }

                _termsResults.SiteShareShouldGet = Format2DPs(_tempShares.GetValueOrDefault());
                _termsResults.SiteShare = Format2DPs(Min<float>(_termsResults.SiteShareShouldGet.GetValueOrDefault(), Max<float>(_termsResults.CollectionValue.GetValueOrDefault(), 0)));
                _termsResults.Remainder = _termsResults.Remainder.GetValueOrDefault() - _termsResults.SiteShare.GetValueOrDefault();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void CalculatePostTermsPartnerSiteGroupRentOnly()
        {
            float? _tempRent = 0;

            try
            {
                LogManager.WriteLog("Inside CalculatePostTermsPartnerSiteGroupRentOnly...", LogManager.enumLogLevel.Info);

                // this only works out the Site Group's rent
                // the shares will be worked out later\

                //calculate required value
                switch (_termsInfo.GroupType)
                {
                    case TERMS_RENT:
                    case TERMS_FRONT_MONEY:
                        _tempRent = _termsInfo.GroupValue;
                        break;
                    default:
                        break;
                }

                _termsResults.SiteGroupRentShouldGet = Format2DPs(_tempRent.GetValueOrDefault());
                _termsResults.SiteGroupRent = Format2DPs(Min<float>(_termsResults.SiteGroupRentShouldGet.GetValueOrDefault(), Max<float>(_termsResults.Remainder.GetValueOrDefault(), 0)));
                _termsResults.Remainder = _termsResults.Remainder.GetValueOrDefault() - _termsResults.SiteGroupRent.GetValueOrDefault();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void CalculatePostTermsPartnerSiteGroupSharesOnly()
        {
            float? _tempShares = 0;
            float? _sharesToGo = 0;
            long? _colDays = 0;
            float? _supShare = 0;
            float? _locShare = 0;
            float? _groupShare = 0;
            float? _secGroupShare = 0;

            try
            {
                LogManager.WriteLog("Inside CalculatePostTermsPartnerSiteGroupSharesOnly...", LogManager.enumLogLevel.Info);

                //this only works out the shares
                //the rent has already been done..

                //if the Site Group is before the Output vat, then
                //  Take % from CashBoxVatable and decrement the remainder
                //if it is after, then
                //  Take % from remainder and decrement remainder

                _colDays = _termsInfo.CollectionDays;
                _supShare = _termsInfo.SupplierShare;
                _locShare = _termsInfo.SiteShare;
                _groupShare = _termsInfo.GroupShare;
                _secGroupShare = _termsInfo.SecGroupShare;

                //if shares are used...
                //to work out the true %, we need to find the sum of all remaining shares
                //and work out the % of that, not 100.
                //this is because other charges may be made between calculating each
                //individual share, thus adding up to a value above the cash box value

                _sharesToGo = _groupShare;
                if (_termsInfo.SupplierUse > 0 && (_termsInfo.SupplierType == TERMS_SHARE_SCHEDULE || _termsInfo.SupplierType == TERMS_MIN || _termsInfo.SupplierType == TERMS_MAX || _termsInfo.SupplierType == TERMS_FRONT_MONEY) && _termsInfo.SupplierUse > _termsInfo.GroupUse)
                    _sharesToGo = _sharesToGo + _supShare;
                if (_termsInfo.SiteUse > 0 && (_termsInfo.SiteType == TERMS_MIN || _termsInfo.SiteType == TERMS_MAX || _termsInfo.SiteType == TERMS_FRONT_MONEY) && _termsInfo.SiteUse > _termsInfo.GroupUse)
                    _sharesToGo = _sharesToGo + _locShare;
                if (_termsInfo.SecGroupUse > 0 && (_termsInfo.SecGroupType == TERMS_MIN || _termsInfo.SecGroupType == TERMS_MAX || _termsInfo.SecGroupType == TERMS_FRONT_MONEY) && _termsInfo.SecGroupUse > _termsInfo.GroupUse)
                    _sharesToGo = _sharesToGo + _secGroupShare;
                if (_sharesToGo == 0) return;

                _termsResults.SiteGroupPerc = _groupShare;

                //calculate required value
                switch (_termsInfo.GroupType)
                {
                    case TERMS_MIN:
                        if (_termsInfo.VATOutputUse > 0 && _termsInfo.GroupUse < _termsInfo.VATOutputUse)
                        {
                            _tempShares = _groupShare;
                            _tempShares = _tempShares / _sharesToGo;
                            _tempShares = _tempShares * _termsResults.CollectionValueOutputVatable.GetValueOrDefault();
                            _tempShares = Max<float>(_tempShares.GetValueOrDefault(), ((_termsInfo.GroupValue.GetValueOrDefault() / 7) * (float)_colDays));
                        }
                        else
                        {
                            _tempShares = _groupShare;
                            _tempShares = _tempShares / _sharesToGo;
                            _tempShares = _tempShares * _termsResults.Remainder.GetValueOrDefault();
                            _tempShares = Max<float>(_tempShares.GetValueOrDefault(), ((_termsInfo.GroupValue.GetValueOrDefault() / 7) * (float)_colDays));
                        }
                        break;
                    case TERMS_MAX:
                        if (_termsInfo.VATOutputUse > 0 && _termsInfo.GroupUse < _termsInfo.VATOutputUse)
                        {
                            _tempShares = _groupShare;
                            _tempShares = _tempShares / _sharesToGo;
                            _tempShares = _tempShares * _termsResults.CollectionValueOutputVatable.GetValueOrDefault();
                            _tempShares = Min<float>(_tempShares.GetValueOrDefault(), ((_termsInfo.GroupValue.GetValueOrDefault() / 7) * (float)_colDays));
                        }
                        else
                        {
                            _tempShares = _groupShare;
                            _tempShares = _tempShares / _sharesToGo;
                            _tempShares = _tempShares * _termsResults.Remainder.GetValueOrDefault();
                            _tempShares = Min<float>(_tempShares.GetValueOrDefault(), ((_termsInfo.GroupValue.GetValueOrDefault() / 7) * (float)_colDays));
                        }
                        break;
                    case TERMS_FRONT_MONEY:
                        if (_termsInfo.VATOutputUse > 0 && _termsInfo.GroupUse < _termsInfo.VATOutputUse)
                        {
                            _tempShares = _groupShare;
                            _tempShares = _tempShares / _sharesToGo;
                            _tempShares = _tempShares * _termsResults.CollectionValueOutputVatable.GetValueOrDefault();
                        }
                        else
                        {
                            _tempShares = _groupShare;
                            _tempShares = _tempShares / 100;
                            _tempShares = _tempShares * _termsResults.CollectionValue.GetValueOrDefault();
                        }
                        break;
                    default:
                        break;
                }

                _termsResults.SiteGroupShareShouldGet = Format2DPs(_tempShares.GetValueOrDefault());
                _termsResults.SiteGroupShare = Format2DPs(Min<float>(_termsResults.SiteGroupShareShouldGet.GetValueOrDefault(), Max<float>(_termsResults.CollectionValue.GetValueOrDefault(), 0)));
                _termsResults.Remainder = _termsResults.Remainder.GetValueOrDefault() - _termsResults.SiteGroupShare.GetValueOrDefault();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void CalculatePostTermsPartnerSecSiteGroupRentOnly()
        {
            float? _tempRent = 0;

            try
            {
                LogManager.WriteLog("Inside CalculatePostTermsPartnerSecSiteGroupRentOnly...", LogManager.enumLogLevel.Info);

                // this only works out the Site Group's rent
                // the shares will be worked out later

                //calculate required value
                switch (_termsInfo.SecGroupType)
                {
                    case TERMS_RENT:
                    case TERMS_FRONT_MONEY:
                        _tempRent = _termsInfo.SecGroupValue;
                        break;
                    default:
                        break;
                }

                _termsResults.SecSiteGroupRentShouldGet = Format2DPs(_tempRent.GetValueOrDefault());
                _termsResults.SecSiteGroupRent = Format2DPs(Min<float>(_termsResults.SecSiteGroupRentShouldGet.GetValueOrDefault(), Max<float>(_termsResults.Remainder.GetValueOrDefault(), 0)));
                _termsResults.Remainder = _termsResults.Remainder.GetValueOrDefault() - _termsResults.SecSiteGroupRent.GetValueOrDefault();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void CalculatePostTermsPartnerSecSiteGroupSharesOnly()
        {
            float? _tempShares = 0;
            float? _sharesToGo = 0;
            long? _colDays = 0;
            float? _supShare = 0;
            float? _locShare = 0;
            float? _groupShare = 0;
            float? _secGroupShare = 0;

            try
            {
                LogManager.WriteLog("Inside CalculatePostTermsPartnerSecSiteGroupSharesOnly...", LogManager.enumLogLevel.Info);

                //this only works out the shares
                //the rent has already been done..

                //if the Site Group is before the Output vat, then
                //  Take % from CashBoxVatable and decrement the remainder
                //if it is after, then
                //  Take % from remainder and decrement remainder

                _colDays = _termsInfo.CollectionDays;
                _supShare = _termsInfo.SupplierShare;
                _locShare = _termsInfo.SiteShare;
                _groupShare = _termsInfo.GroupShare;
                _secGroupShare = _termsInfo.SecGroupShare;

                //if shares are used...
                //to work out the true %, we need to find the sum of all remaining shares
                //and work out the % of that, not 100.
                //this is because other charges may be made between calculating each
                //individual share, thus adding up to a value above the cash box value

                _sharesToGo = _secGroupShare;
                if (_termsInfo.SupplierUse > 0 && (_termsInfo.SupplierType == TERMS_SHARE_SCHEDULE || _termsInfo.SupplierType == TERMS_MIN || _termsInfo.SupplierType == TERMS_MAX || _termsInfo.SupplierType == TERMS_FRONT_MONEY) && _termsInfo.SupplierUse > _termsInfo.SecGroupUse)
                    _sharesToGo = _sharesToGo + _supShare;
                if (_termsInfo.SiteUse > 0 && (_termsInfo.SiteType == TERMS_MIN || _termsInfo.SiteType == TERMS_MAX || _termsInfo.SiteType == TERMS_FRONT_MONEY) && _termsInfo.SiteUse > _termsInfo.SecGroupUse)
                    _sharesToGo = _sharesToGo + _locShare;
                if (_termsInfo.GroupUse > 0 && (_termsInfo.GroupType == TERMS_MIN || _termsInfo.GroupType == TERMS_MAX || _termsInfo.GroupType == TERMS_FRONT_MONEY) && _termsInfo.GroupUse > _termsInfo.SecGroupUse)
                    _sharesToGo = _sharesToGo + _groupShare;
                if (_sharesToGo == 0) return;

                _termsResults.SecSiteGroupPerc = _secGroupShare;

                //calculate required value
                switch (_termsInfo.SecGroupType)
                {
                    case TERMS_MIN:
                        if (_termsInfo.VATOutputUse > 0 && _termsInfo.SecGroupUse < _termsInfo.VATOutputUse)
                        {
                            _tempShares = _secGroupShare;
                            _tempShares = _tempShares / _sharesToGo;
                            _tempShares = _tempShares * _termsResults.CollectionValueOutputVatable.GetValueOrDefault();
                            _tempShares = Max<float>(_tempShares.GetValueOrDefault(), ((_termsInfo.SecGroupValue.GetValueOrDefault() / 7) * (float)_colDays));
                        }
                        else
                        {
                            _tempShares = _secGroupShare;
                            _tempShares = _tempShares / _sharesToGo;
                            _tempShares = _tempShares * _termsResults.Remainder.GetValueOrDefault();
                            _tempShares = Max<float>(_tempShares.GetValueOrDefault(), ((_termsInfo.SecGroupValue.GetValueOrDefault() / 7) * (float)_colDays));
                        }
                        break;
                    case TERMS_MAX:
                        if (_termsInfo.VATOutputUse > 0 && _termsInfo.SecGroupUse < _termsInfo.VATOutputUse)
                        {
                            _tempShares = _secGroupShare;
                            _tempShares = _tempShares / _sharesToGo;
                            _tempShares = _tempShares * _termsResults.CollectionValueOutputVatable.GetValueOrDefault();
                            _tempShares = Min<float>(_tempShares.GetValueOrDefault(), ((_termsInfo.SecGroupValue.GetValueOrDefault() / 7) * (float)_colDays));
                        }
                        else
                        {
                            _tempShares = _secGroupShare;
                            _tempShares = _tempShares / _sharesToGo;
                            _tempShares = _tempShares * _termsResults.Remainder.GetValueOrDefault();
                            _tempShares = Min<float>(_tempShares.GetValueOrDefault(), ((_termsInfo.SecGroupValue.GetValueOrDefault() / 7) * (float)_colDays));
                        }
                        break;
                    case TERMS_FRONT_MONEY:
                        if (_termsInfo.VATOutputUse > 0 && _termsInfo.SecGroupUse < _termsInfo.VATOutputUse)
                        {
                            _tempShares = _secGroupShare;
                            _tempShares = _tempShares / _sharesToGo;
                            _tempShares = _tempShares * _termsResults.CollectionValueOutputVatable.GetValueOrDefault();
                        }
                        else
                        {
                            _tempShares = _secGroupShare;
                            _tempShares = _tempShares / 100;
                            _tempShares = _tempShares * _termsResults.CollectionValue.GetValueOrDefault();
                        }
                        break;
                    default:
                        break;
                }

                _termsResults.SecSiteGroupShareShouldGet = Format2DPs(_tempShares.GetValueOrDefault());
                _termsResults.SecSiteGroupShare = Format2DPs(Min<float>(_termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault(), Max<float>(_termsResults.CollectionValue.GetValueOrDefault(), 0)));
                _termsResults.Remainder = _termsResults.Remainder.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void CalculatePostTermsGPT()
        {
            try
            {
                LogManager.WriteLog("Inside CalculatePostTermsGPT...", LogManager.enumLogLevel.Info);

                if (_termsInfo.GPTUse > 0)
                {
                    _termsResults.GPT = Format2DPs((_termsResults.CollectionValue.GetValueOrDefault() * 0.15f));
                    _termsResults.Remainder = _termsResults.Remainder.GetValueOrDefault() - _termsResults.GPT.GetValueOrDefault();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void CalculatePostTermsVatOutput()
        {
            try
            {
                LogManager.WriteLog("Inside CalculatePostTermsVatOutput...", LogManager.enumLogLevel.Info);

                //NB, Cash Box is VAT inclusive
                //VAT = CB - (CB / 1.175)
                if (_termsInfo.VATOutputUse > 0)
                {
                    _termsResults.OutputVAT = Format2DPs(_termsResults.CollectionValueOutputVatable.GetValueOrDefault() - (Format2DPs(_termsResults.CollectionValueOutputVatable.GetValueOrDefault() / 1.175f)));
                    _termsResults.Remainder = _termsResults.Remainder.GetValueOrDefault() - _termsResults.OutputVAT.GetValueOrDefault();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void CalculatePostTermsVatSupplier()
        {
            try
            {
                LogManager.WriteLog("Inside CalculatePostTermsVatSupplier...", LogManager.enumLogLevel.Info);

                if (_termsInfo.VATSupplierUse.GetValueOrDefault() > 0)
                {
                    if (_termsInfo.GPTUse.GetValueOrDefault() > 0)
                    {
                        //GPT is used, it is therefore 17.5% of rent + 17.5% of share
                        _termsResults.SupplierVatShouldGet = Format2DPs((_termsResults.SupplierRent.GetValueOrDefault() + _termsResults.SupplierShare.GetValueOrDefault()) * SystemVATRate);
                    }
                    else
                    {
                        //this is calculated by the supplier's rent * 0.175

                        //it should be done after the shortfalls...

                        //shares are not taken into account

                        //If myTermsSet!Terms_Profile_Partners_Supplier_Type = TERMS_FRONT_MONEY And myTermsSet!Terms_Profile_Partners_Supplier_Value_Guaranteed Then
                        //VAT should only be chargeable if there is a rent
                        //Guaranteed frontMoney counts as rent...

                        //if it is the last VAT portion to be calculated, use the remainder, else calculate it
                        if (((_termsInfo.VATGroupUse > 0 && (_termsInfo.VATGroupUse < _termsInfo.VATSupplierUse)) || _termsInfo.VATGroupUse == 0) &&
                            ((_termsInfo.VATSiteUse > 0 && (_termsInfo.VATSiteUse < _termsInfo.VATSupplierUse)) || (_termsInfo.VATSiteUse == 0) &&
                            ((_termsInfo.VatSecGroupUse > 0 && (_termsInfo.VatSecGroupUse < _termsInfo.VATSupplierUse)) || _termsInfo.VatSecGroupUse == 0)))
                        {
                            _termsResults.SupplierVatShouldGet = _termsResults.OutputVAT.GetValueOrDefault() - _termsResults.SiteVatShouldGet.GetValueOrDefault() - _termsResults.SiteGroupVatShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupVatShouldGet.GetValueOrDefault();
                        }
                        else
                        {
                            _termsResults.SupplierVatShouldGet = Format2DPs((_termsResults.SupplierRent.GetValueOrDefault() + _termsResults.SupplierShare.GetValueOrDefault()) * SystemVATRate);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void CalculatePostTermsVatSite()
        {
            try
            {
                LogManager.WriteLog("Inside CalculatePostTermsVatSite...", LogManager.enumLogLevel.Info);

                //this is the Site Group's share * 0.175
                if (_termsInfo.VATSiteUse.GetValueOrDefault() > 0)
                {
                    //if it is the last Vat portion to be calculated, take the remainder
                    if (((_termsInfo.VATGroupUse > 0 && (_termsInfo.VATGroupUse < _termsInfo.VATSiteUse)) || _termsInfo.VATGroupUse == 0) &&
                        ((_termsInfo.VATSupplierUse > 0 && (_termsInfo.VATSupplierUse < _termsInfo.VATSiteUse)) || (_termsInfo.VATSupplierUse == 0) &&
                        ((_termsInfo.VatSecGroupUse > 0 && (_termsInfo.VatSecGroupUse < _termsInfo.VATSiteUse)) || _termsInfo.VatSecGroupUse == 0)))
                    {
                        //use the remainder
                        _termsResults.SiteVatShouldGet = Format2DPs(_termsResults.OutputVAT.GetValueOrDefault() - _termsResults.SiteGroupVatShouldGet.GetValueOrDefault() - _termsResults.SupplierVatShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupVatShouldGet.GetValueOrDefault());
                    }
                    else
                    {
                        _termsResults.SiteVatShouldGet = Format2DPs(_termsResults.SiteShare.GetValueOrDefault() * SystemVATRate);
                    }

                    //If the vat is going to the supplier then to C&E, allocate all vat to supplier
                    if (_termsInfo.VATSiteDest == TERMS_SUPPLIER && _termsInfo.VatSiteDeferredDest == TERMS_CUSTOMS_AND_EXCISE)
                    {
                        _termsResults.SupplierVatShouldGet = _termsResults.SupplierVatShouldGet.GetValueOrDefault() + _termsResults.SiteVatShouldGet.GetValueOrDefault();
                        _termsResults.SiteVatShouldGet = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void CalculatePostTermsVatSiteGroup()
        {
            try
            {
                LogManager.WriteLog("Inside CalculatePostTermsVatSiteGroup...", LogManager.enumLogLevel.Info);

                //this is the Site Group's share * 0.175
                if (_termsInfo.VATGroupUse.GetValueOrDefault() > 0)
                {
                    //if it is the last Vat portion to be calculated, take the remainder
                    if (((_termsInfo.VATSiteUse > 0 && (_termsInfo.VATSiteUse < _termsInfo.VATGroupUse)) || _termsInfo.VATSiteUse == 0) &&
                        ((_termsInfo.VATSupplierUse > 0 && (_termsInfo.VATSupplierUse < _termsInfo.VATGroupUse)) || (_termsInfo.VATSupplierUse == 0) &&
                        ((_termsInfo.VatSecGroupUse > 0 && (_termsInfo.VatSecGroupUse < _termsInfo.VATGroupUse)) || _termsInfo.VatSecGroupUse == 0)))
                    {
                        //use the remainder
                        _termsResults.SiteGroupVatShouldGet = Format2DPs(_termsResults.OutputVAT.GetValueOrDefault() - _termsResults.SiteVatShouldGet.GetValueOrDefault() - _termsResults.SupplierVatShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupVatShouldGet.GetValueOrDefault());
                    }
                    else
                    {
                        _termsResults.SiteGroupVatShouldGet = Format2DPs(_termsResults.SiteGroupShare.GetValueOrDefault() * SystemVATRate);

                        //we have to include the rent if the Site Group is using front money...
                        if (_termsInfo.GroupType == TERMS_FRONT_MONEY)
                            _termsResults.SiteGroupVatShouldGet = Format2DPs(_termsResults.SiteGroupVatShouldGet.GetValueOrDefault() + (_termsResults.SiteGroupRent.GetValueOrDefault() * SystemVATRate));
                    }

                    if (_termsResults.SiteGroupVatShouldGet.GetValueOrDefault() < 0.0)
                        _termsResults.SiteGroupVatShouldGet = 0;

                    //If the vat is going to the supplier then to C&E, allocate all vat to supplier
                    if (_termsInfo.VATGroupDest == TERMS_SUPPLIER && _termsInfo.VATGroupDeferredDest == TERMS_CUSTOMS_AND_EXCISE)
                    {
                        _termsResults.SupplierVatShouldGet = _termsResults.SupplierVatShouldGet.GetValueOrDefault() + _termsResults.SiteGroupVatShouldGet.GetValueOrDefault();
                        _termsResults.SiteGroupVatShouldGet = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void CalculatePostTermsVatSecSiteGroup()
        {
            try
            {
                LogManager.WriteLog("Inside CalculatePostTermsVatSecSiteGroup...", LogManager.enumLogLevel.Info);

                //this is the Sec Site Group's share * 0.175
                if (_termsInfo.VatSecGroupUse.GetValueOrDefault() > 0)
                {
                    //if it is the last Vat portion to be calculated, take the remainder
                    if (((_termsInfo.VATSiteUse > 0 && (_termsInfo.VATSiteUse < _termsInfo.VatSecGroupUse)) || _termsInfo.VATSiteUse == 0) &&
                        ((_termsInfo.VATSupplierUse > 0 && (_termsInfo.VATSupplierUse < _termsInfo.VatSecGroupUse)) || (_termsInfo.VATSupplierUse == 0) &&
                        ((_termsInfo.VatSecGroupUse > 0 && (_termsInfo.VatSecGroupUse < _termsInfo.VatSecGroupUse)) || _termsInfo.VATGroupUse == 0)))
                    {
                        //use the remainder
                        _termsResults.SecSiteGroupVatShouldGet = Format2DPs(_termsResults.OutputVAT.GetValueOrDefault() - _termsResults.SiteVatShouldGet.GetValueOrDefault() - _termsResults.SupplierVatShouldGet.GetValueOrDefault() - _termsResults.SiteGroupVatShouldGet.GetValueOrDefault());
                    }
                    else
                    {
                        _termsResults.SecSiteGroupVatShouldGet = Format2DPs(_termsResults.SecSiteGroupShare.GetValueOrDefault() * SystemVATRate);

                        //we have to include the rent if the Site Group is using front money...
                        if (_termsInfo.SecGroupType == TERMS_FRONT_MONEY)
                            _termsResults.SecSiteGroupVatShouldGet = Format2DPs(_termsResults.SecSiteGroupVatShouldGet.GetValueOrDefault() + (_termsResults.SecSiteGroupRent.GetValueOrDefault() * SystemVATRate));
                    }

                    //If the vat is going to the supplier then to C&E, allocate all vat to supplier
                    if (_termsInfo.VATSecGroupDest == TERMS_SUPPLIER && _termsInfo.VATSecGroupDeferredDest == TERMS_CUSTOMS_AND_EXCISE)
                    {
                        _termsResults.SupplierVatShouldGet = _termsResults.SupplierVatShouldGet.GetValueOrDefault() + _termsResults.SecSiteGroupVatShouldGet.GetValueOrDefault();
                        _termsResults.SecSiteGroupVatShouldGet = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void CalculatePostTermsVatSupplierShortfall()
        {
            try
            {
                LogManager.WriteLog("Inside CalculatePostTermsVatSupplierShortfall...", LogManager.enumLogLevel.Info);

                if (_termsInfo.GPTUse > 0)
                {
                    _termsResults.SupplierVat = _termsResults.SupplierVatShouldGet.GetValueOrDefault();
                }
                else
                {
                    _termsResults.SupplierVat = Max<float>(0, Min<float>(_termsResults.SupplierVatShouldGet.GetValueOrDefault(), _termsResults.OutputVAT.GetValueOrDefault()));
                    _termsResults.OutputVAT = _termsResults.OutputVAT.GetValueOrDefault() - _termsResults.SupplierVat.GetValueOrDefault();

                    //This code uses the VAT guarantors - RIGHT!
                    if (_termsResults.SupplierVat.GetValueOrDefault() < _termsResults.SupplierVatShouldGet.GetValueOrDefault())
                    {
                        switch (_termsInfo.VATSupplierShortfallGuarantor)
                        {
                            case TERMS_SITE:
                                _termsResults.SiteGuarantorForVATShortfall = _termsResults.SiteGuarantorForVATShortfall.GetValueOrDefault() + (_termsResults.SupplierVat.GetValueOrDefault() - _termsResults.SupplierVatShouldGet.GetValueOrDefault());
                                break;
                            case TERMS_SITE_GROUP:
                                if (_termsInfo.SiteUse > 0)
                                    _termsResults.SiteGuarantorForVATShortfall = _termsResults.SiteGroupGuarantorForVATShortfall.GetValueOrDefault() + (_termsResults.SupplierVat.GetValueOrDefault() - _termsResults.SupplierVatShouldGet.GetValueOrDefault());
                                else
                                    _termsResults.SiteGroupGuarantorForVATShortfall = _termsResults.SiteGroupGuarantorForVATShortfall.GetValueOrDefault() + (_termsResults.SupplierVat.GetValueOrDefault() - _termsResults.SupplierVatShouldGet.GetValueOrDefault());
                                break;
                            case TERMS_SECONDARY_GROUP:
                                _termsResults.SecSiteGroupGuarantorForVATShortfall = _termsResults.SecSiteGroupGuarantorForVATShortfall.GetValueOrDefault() + (_termsResults.SupplierVat.GetValueOrDefault() - _termsResults.SupplierVatShouldGet.GetValueOrDefault());
                                break;
                            case TERMS_SUPPLIER:
                                _termsResults.SupplierGuarantorForVATShortfall = _termsResults.SupplierGuarantorForVATShortfall.GetValueOrDefault() + (_termsResults.SupplierVat.GetValueOrDefault() - _termsResults.SupplierVatShouldGet.GetValueOrDefault());
                                break;
                            default:
                                break;
                        }

                        _termsResults.SupplierVat = _termsResults.SupplierVatShouldGet.GetValueOrDefault();
                    }
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void CalculatePostTermsVatSiteShortfall()
        {
            try
            {
                LogManager.WriteLog("Inside CalculatePostTermsVatSiteShortfall...", LogManager.enumLogLevel.Info);

                _termsResults.SiteVat = Max<float>(0, Min<float>(_termsResults.SiteVatShouldGet.GetValueOrDefault(), _termsResults.OutputVAT.GetValueOrDefault()));
                _termsResults.OutputVAT = _termsResults.OutputVAT.GetValueOrDefault() - _termsResults.SiteVat.GetValueOrDefault();

                if (_termsResults.SiteVat.GetValueOrDefault() < _termsResults.SiteVatShouldGet.GetValueOrDefault())
                {
                    switch (_termsInfo.VATSiteShortfallGuarantor)
                    {
                        case TERMS_SITE:
                            _termsResults.SiteGuarantorForVATShortfall = _termsResults.SiteGuarantorForVATShortfall.GetValueOrDefault() + (_termsResults.SiteVatShouldGet.GetValueOrDefault() - _termsResults.SiteVat.GetValueOrDefault());
                            break;
                        case TERMS_SITE_GROUP:
                            _termsResults.SiteGroupGuarantorForVATShortfall = _termsResults.SiteGroupGuarantorForVATShortfall.GetValueOrDefault() + (_termsResults.SiteVatShouldGet.GetValueOrDefault() - _termsResults.SiteVat.GetValueOrDefault());
                            break;
                        case TERMS_SECONDARY_GROUP:
                            _termsResults.SecSiteGroupGuarantorForVATShortfall = _termsResults.SecSiteGroupGuarantorForVATShortfall.GetValueOrDefault() + (_termsResults.SiteVatShouldGet.GetValueOrDefault() - _termsResults.SiteVat.GetValueOrDefault());
                            break;
                        case TERMS_SUPPLIER:
                            _termsResults.SupplierGuarantorForVATShortfall = _termsResults.SupplierGuarantorForVATShortfall.GetValueOrDefault() + (_termsResults.SiteVatShouldGet.GetValueOrDefault() - _termsResults.SiteVat.GetValueOrDefault());
                            break;
                        default:
                            break;
                    }

                    _termsResults.SiteVat = _termsResults.SiteVatShouldGet.GetValueOrDefault();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void CalculatePostTermsVatSiteGroupShortfall()
        {
            try
            {
                LogManager.WriteLog("Inside CalculatePostTermsVatSiteGroupShortfall...", LogManager.enumLogLevel.Info);

                _termsResults.SiteGroupVat = Max<float>(0, Min<float>(_termsResults.SiteGroupVatShouldGet.GetValueOrDefault(), _termsResults.OutputVAT.GetValueOrDefault()));
                _termsResults.OutputVAT = _termsResults.OutputVAT.GetValueOrDefault() - _termsResults.SiteGroupVat.GetValueOrDefault();

                if (_termsResults.SiteGroupVat.GetValueOrDefault() < _termsResults.SiteGroupVatShouldGet.GetValueOrDefault())
                {
                    switch (_termsInfo.VATGroupShortfallGuarantor)
                    {
                        case TERMS_SITE:
                            _termsResults.SiteGuarantorForVATShortfall = _termsResults.SiteGuarantorForVATShortfall.GetValueOrDefault() + (_termsResults.SiteGroupVatShouldGet.GetValueOrDefault() - _termsResults.SiteGroupVat.GetValueOrDefault());
                            break;
                        case TERMS_SITE_GROUP:
                            _termsResults.SiteGroupGuarantorForVATShortfall = _termsResults.SiteGroupGuarantorForVATShortfall.GetValueOrDefault() + (_termsResults.SiteGroupVatShouldGet.GetValueOrDefault() - _termsResults.SiteGroupVat.GetValueOrDefault());
                            break;
                        case TERMS_SECONDARY_GROUP:
                            _termsResults.SecSiteGroupGuarantorForVATShortfall = _termsResults.SecSiteGroupGuarantorForVATShortfall.GetValueOrDefault() + (_termsResults.SiteGroupVatShouldGet.GetValueOrDefault() - _termsResults.SiteGroupVat.GetValueOrDefault());
                            break;
                        case TERMS_SUPPLIER:
                            _termsResults.SupplierGuarantorForVATShortfall = _termsResults.SupplierGuarantorForVATShortfall.GetValueOrDefault() + (_termsResults.SiteGroupVatShouldGet.GetValueOrDefault() - _termsResults.SiteGroupVat.GetValueOrDefault());
                            break;
                        default:
                            break;
                    }

                    _termsResults.SiteGroupVat = _termsResults.SiteGroupVatShouldGet.GetValueOrDefault();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void CalculatePostTermsVatSecSiteGroupShortfall()
        {
            try
            {
                LogManager.WriteLog("Inside CalculatePostTermsVatSecSiteGroupShortfall...", LogManager.enumLogLevel.Info);

                _termsResults.SecSiteGroupVat = Max<float>(0, Min<float>(_termsResults.SecSiteGroupVatShouldGet.GetValueOrDefault(), _termsResults.OutputVAT.GetValueOrDefault()));
                _termsResults.OutputVAT = _termsResults.OutputVAT - _termsResults.SecSiteGroupVat.GetValueOrDefault();

                if (_termsResults.SecSiteGroupVat.GetValueOrDefault() < _termsResults.SecSiteGroupVatShouldGet.GetValueOrDefault())
                {
                    switch (_termsInfo.VATSecGroupShortfallGuarantor)
                    {
                        case TERMS_SITE:
                            _termsResults.SiteGuarantorForVATShortfall = _termsResults.SiteGuarantorForVATShortfall.GetValueOrDefault() + (_termsResults.SecSiteGroupVatShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupVat.GetValueOrDefault());
                            break;
                        case TERMS_SITE_GROUP:
                            _termsResults.SiteGroupGuarantorForVATShortfall = _termsResults.SiteGroupGuarantorForVATShortfall.GetValueOrDefault() + (_termsResults.SecSiteGroupVatShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupVat.GetValueOrDefault());
                            break;
                        case TERMS_SECONDARY_GROUP:
                            _termsResults.SecSiteGroupGuarantorForVATShortfall = _termsResults.SecSiteGroupGuarantorForVATShortfall.GetValueOrDefault() + (_termsResults.SecSiteGroupVatShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupVat.GetValueOrDefault());
                            break;
                        case TERMS_SUPPLIER:
                            _termsResults.SupplierGuarantorForVATShortfall = _termsResults.SupplierGuarantorForVATShortfall.GetValueOrDefault() + (_termsResults.SecSiteGroupVatShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupVat.GetValueOrDefault());
                            break;
                        default:
                            break;
                    }

                    _termsResults.SecSiteGroupVat = _termsResults.SecSiteGroupVatShouldGet.GetValueOrDefault();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void CalculatePostTermsOtherLicenceShortfall()
        {
            float? _totalPercentage = 0;
            float? _supplierPerc = 0;
            float? _sitePerc = 0;
            float? _groupPerc = 0;
            float? _secGroupPerc = 0;
            float? _shortfallDifference = 0;
            float? _shortfallDifferenceLeft = 0;
            float? _percSoFar = 0;

            try
            {
                LogManager.WriteLog("Inside CalculatePostTermsOtherLicenceShortfall...", LogManager.enumLogLevel.Info);

                if (_termsResults.LicenceChargeShouldGet.GetValueOrDefault() > _termsResults.LicenceCharge.GetValueOrDefault())
                {
                    switch (_termsInfo.OtherLicenceGuarantor)
                    {
                        case TERMS_SITE:
                            _termsResults.SiteShare = _termsResults.SiteShare.GetValueOrDefault() - (_termsResults.LicenceChargeShouldGet.GetValueOrDefault() - _termsResults.LicenceCharge.GetValueOrDefault());
                            _termsResults.LicenceCharge = _termsResults.LicenceChargeShouldGet.GetValueOrDefault();
                            break;
                        case TERMS_SUPPLIER:
                            _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() - (_termsResults.LicenceChargeShouldGet.GetValueOrDefault() - _termsResults.LicenceCharge.GetValueOrDefault());
                            _termsResults.LicenceCharge = _termsResults.LicenceChargeShouldGet.GetValueOrDefault();
                            break;
                        case TERMS_SITE_GROUP:
                            _termsResults.SiteGroupShare = _termsResults.SiteGroupShare.GetValueOrDefault() - (_termsResults.LicenceChargeShouldGet.GetValueOrDefault() - _termsResults.LicenceCharge.GetValueOrDefault());
                            _termsResults.LicenceCharge = _termsResults.LicenceChargeShouldGet.GetValueOrDefault();
                            break;
                        case TERMS_SECONDARY_GROUP:
                            _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() - (_termsResults.LicenceChargeShouldGet.GetValueOrDefault() - _termsResults.LicenceCharge.GetValueOrDefault());
                            _termsResults.LicenceCharge = _termsResults.LicenceChargeShouldGet.GetValueOrDefault();
                            break;
                        case TERMS_PERCENTAGE:
                            _totalPercentage = -1;
                            if (_termsInfo.SupplierUse > 0 && (!(_termsInfo.SupplierValueGuaranteed.GetValueOrDefault() || _termsInfo.SupplierShareGuaranteed.GetValueOrDefault())))
                            {
                                if (_totalPercentage == -1)
                                    _totalPercentage = 0;

                                _supplierPerc = _termsInfo.SupplierGuarantorPercentage;
                                _totalPercentage = _totalPercentage + _termsInfo.SupplierGuarantorPercentage;
                            }

                            if (_termsInfo.SiteUse > 0 && (!(_termsInfo.SiteValueGuaranteed.GetValueOrDefault() || _termsInfo.SiteShareGuaranteed.GetValueOrDefault())))
                            {
                                if (_totalPercentage == -1)
                                    _totalPercentage = 0;

                                _sitePerc = _termsInfo.SiteGuarantorPercentage;
                                _totalPercentage = _totalPercentage + _termsInfo.SiteGuarantorPercentage;
                            }

                            if (_termsInfo.GroupUse > 0 && (!(_termsInfo.GroupValueGuaranteed.GetValueOrDefault() || _termsInfo.GroupShareGuaranteed.GetValueOrDefault())))
                            {
                                if (_totalPercentage == -1)
                                    _totalPercentage = 0;

                                _groupPerc = _termsInfo.GroupGuarantorPercentage;
                                _totalPercentage = _totalPercentage + _termsInfo.GroupGuarantorPercentage;
                            }

                            if (_termsInfo.SecGroupUse > 0 && (!(_termsInfo.GroupValueGuaranteed.GetValueOrDefault() || _termsInfo.GroupShareGuaranteed.GetValueOrDefault())))
                            {
                                if (_totalPercentage == -1)
                                    _totalPercentage = 0;

                                _secGroupPerc = _termsInfo.GroupGuarantorPercentage;
                                _totalPercentage = _totalPercentage + _termsInfo.GroupGuarantorPercentage;
                            }

                            _shortfallDifference = _termsResults.LicenceChargeShouldGet.GetValueOrDefault() - _termsResults.LicenceCharge.GetValueOrDefault();
                            _shortfallDifferenceLeft = _shortfallDifference;

                            //if TotalPercentage = -1 then do not use perc, otherwise use perc
                            _percSoFar = 0;
                            if (_totalPercentage >= 0)
                            {
                                if (_supplierPerc > 0)
                                {
                                    if ((_totalPercentage - _percSoFar) == _supplierPerc)
                                    {
                                        _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() - _shortfallDifferenceLeft.GetValueOrDefault();
                                        _termsResults.LicenceCharge = _termsResults.LicenceCharge.GetValueOrDefault() + _shortfallDifferenceLeft.GetValueOrDefault();
                                        _shortfallDifferenceLeft = 0;
                                    }
                                    else
                                    {
                                        _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() - Format2DPs((_shortfallDifference.GetValueOrDefault()) * (_supplierPerc.GetValueOrDefault() / _totalPercentage.GetValueOrDefault()));
                                        _termsResults.LicenceCharge = _termsResults.LicenceCharge.GetValueOrDefault() + Format2DPs((_shortfallDifference.GetValueOrDefault()) * (_supplierPerc.GetValueOrDefault() / _totalPercentage.GetValueOrDefault()));
                                        _shortfallDifferenceLeft = _shortfallDifferenceLeft.GetValueOrDefault() - Format2DPs((_shortfallDifference.GetValueOrDefault()) * (_supplierPerc.GetValueOrDefault() / _totalPercentage.GetValueOrDefault()));
                                    }
                                    _percSoFar = _percSoFar + _supplierPerc;
                                }

                                if (_sitePerc > 0)
                                {
                                    if ((_totalPercentage - _percSoFar) == _sitePerc)
                                    {
                                        _termsResults.SiteShare = _termsResults.SiteShare.GetValueOrDefault() - _shortfallDifferenceLeft.GetValueOrDefault();
                                        _termsResults.LicenceCharge = _termsResults.LicenceCharge.GetValueOrDefault() + _shortfallDifferenceLeft.GetValueOrDefault();
                                        _shortfallDifferenceLeft = 0;
                                    }
                                    else
                                    {
                                        _termsResults.SiteShare = _termsResults.SiteShare.GetValueOrDefault() - Format2DPs((_shortfallDifference.GetValueOrDefault()) * (_sitePerc.GetValueOrDefault() / _totalPercentage.GetValueOrDefault()));
                                        _termsResults.LicenceCharge = _termsResults.LicenceCharge.GetValueOrDefault() + Format2DPs((_shortfallDifference.GetValueOrDefault()) * (_sitePerc.GetValueOrDefault() / _totalPercentage.GetValueOrDefault()));
                                        _shortfallDifferenceLeft = _shortfallDifferenceLeft.GetValueOrDefault() - Format2DPs((_shortfallDifference.GetValueOrDefault()) * (_sitePerc.GetValueOrDefault() / _totalPercentage.GetValueOrDefault()));
                                    }
                                    _percSoFar = _percSoFar + _sitePerc;
                                }

                                if (_groupPerc > 0)
                                {
                                    if ((_totalPercentage - _percSoFar) == _groupPerc)
                                    {
                                        _termsResults.SiteGroupShare = _termsResults.SiteGroupShare.GetValueOrDefault() - _shortfallDifferenceLeft.GetValueOrDefault();
                                        _termsResults.LicenceCharge = _termsResults.LicenceCharge.GetValueOrDefault() + _shortfallDifferenceLeft.GetValueOrDefault();
                                        _shortfallDifferenceLeft = 0;
                                    }
                                    else
                                    {
                                        _termsResults.SiteGroupShare = _termsResults.SiteGroupShare.GetValueOrDefault() - Format2DPs((_shortfallDifference.GetValueOrDefault()) * (_groupPerc.GetValueOrDefault() / _totalPercentage.GetValueOrDefault()));
                                        _termsResults.LicenceCharge = _termsResults.LicenceCharge.GetValueOrDefault() + Format2DPs((_shortfallDifference.GetValueOrDefault()) * (_groupPerc.GetValueOrDefault() / _totalPercentage.GetValueOrDefault()));
                                        _shortfallDifferenceLeft = _shortfallDifferenceLeft.GetValueOrDefault() - Format2DPs((_shortfallDifference.GetValueOrDefault()) * (_groupPerc.GetValueOrDefault() / _totalPercentage.GetValueOrDefault()));
                                    }
                                    _percSoFar = _percSoFar + _groupPerc;
                                }

                                if (_secGroupPerc > 0)
                                {
                                    if ((_totalPercentage - _percSoFar) == _secGroupPerc)
                                    {
                                        _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() - _shortfallDifferenceLeft.GetValueOrDefault();
                                        _termsResults.LicenceCharge = _termsResults.LicenceCharge.GetValueOrDefault() + _shortfallDifferenceLeft.GetValueOrDefault();
                                        _shortfallDifferenceLeft = 0;
                                    }
                                    else
                                    {
                                        _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() - Format2DPs((_shortfallDifference.GetValueOrDefault()) * (_secGroupPerc.GetValueOrDefault() / _totalPercentage.GetValueOrDefault()));
                                        _termsResults.LicenceCharge = _termsResults.LicenceCharge.GetValueOrDefault() + Format2DPs((_shortfallDifference.GetValueOrDefault()) * (_secGroupPerc.GetValueOrDefault() / _totalPercentage.GetValueOrDefault()));
                                        _shortfallDifferenceLeft = _shortfallDifferenceLeft.GetValueOrDefault() - Format2DPs((_shortfallDifference.GetValueOrDefault()) * (_secGroupPerc.GetValueOrDefault() / _totalPercentage.GetValueOrDefault()));
                                    }
                                    _percSoFar = _percSoFar + _secGroupPerc;
                                }
                            }

                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void CalculatePostTermsSiteShortfall()
        {
            try
            {
                LogManager.WriteLog("Inside CalculatePostTermsSiteShortfall...", LogManager.enumLogLevel.Info);

                if (_termsInfo.SiteValueGuaranteed.GetValueOrDefault() || _termsInfo.SiteShareGuaranteed.GetValueOrDefault())
                {
                    //If it is primary & secondary, then they have to be in the correct order
                    //otherwise, do 1 to 15...
                    if (((_termsInfo.SupplierUse > 0 && (!(_termsInfo.SupplierValueGuaranteed.GetValueOrDefault() || _termsInfo.SupplierShareGuaranteed.GetValueOrDefault())) && (_termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PRIMARY_TO_ZERO || _termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER || _termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_SECONDARY_TO_ZERO))) ||
                        ((_termsInfo.SiteUse > 0 && (!(_termsInfo.SiteValueGuaranteed.GetValueOrDefault() || _termsInfo.SiteShareGuaranteed.GetValueOrDefault())) && (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PRIMARY_TO_ZERO || _termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER || _termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_SECONDARY_TO_ZERO))) ||
                        ((_termsInfo.GroupUse > 0 && (!(_termsInfo.GroupValueGuaranteed.GetValueOrDefault() || _termsInfo.GroupShareGuaranteed.GetValueOrDefault())) && (_termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_PRIMARY_TO_ZERO || _termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER || _termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_SECONDARY_TO_ZERO))) ||
                        ((_termsInfo.SecGroupUse > 0 && (!(_termsInfo.SecGroupValueGuaranteed.GetValueOrDefault() || _termsInfo.SecGroupShareGuaranteed.GetValueOrDefault())) && (_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PRIMARY_TO_ZERO || _termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER || _termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_SECONDARY_TO_ZERO))))
                    {
                        //Find Primary
                        if (_termsInfo.SupplierUse > 0 && (!(_termsInfo.SupplierValueGuaranteed.GetValueOrDefault() || _termsInfo.SupplierShareGuaranteed.GetValueOrDefault())) && (_termsInfo.SupplierGuarantor == TERMS_PRIMARY_FULL || _termsInfo.SupplierGuarantor == TERMS_PRIMARY_TO_ZERO))
                            UseSupplierForSiteShortfall();
                        else if (_termsInfo.GroupUse > 0 && (!(_termsInfo.GroupValueGuaranteed.GetValueOrDefault() || _termsInfo.GroupShareGuaranteed.GetValueOrDefault())) && (_termsInfo.GroupGuarantor == TERMS_PRIMARY_FULL || _termsInfo.GroupGuarantor == TERMS_PRIMARY_TO_ZERO))
                            UseSiteGroupForSiteShortfall();
                        else if (_termsInfo.SecGroupUse > 0 && (!(_termsInfo.SecGroupValueGuaranteed.GetValueOrDefault() || _termsInfo.SecGroupShareGuaranteed.GetValueOrDefault())) && (_termsInfo.SecGroupGuarantor == TERMS_PRIMARY_FULL || _termsInfo.SecGroupGuarantor == TERMS_PRIMARY_TO_ZERO))
                            UseSecSiteGroupForSiteShortfall();

                        //Find Secondary
                        if (_termsInfo.SupplierUse > 0 && (!(_termsInfo.SupplierValueGuaranteed.GetValueOrDefault() || _termsInfo.SupplierShareGuaranteed.GetValueOrDefault())) && (_termsInfo.SupplierGuarantor == TERMS_SECONDARY_REMAINDER || _termsInfo.SupplierGuarantor == TERMS_SECONDARY_TO_ZERO))
                            UseSupplierForSiteShortfall();
                        else if (_termsInfo.GroupUse > 0 && (!(_termsInfo.GroupValueGuaranteed.GetValueOrDefault() || _termsInfo.GroupShareGuaranteed.GetValueOrDefault())) && (_termsInfo.GroupGuarantor == TERMS_SECONDARY_REMAINDER || _termsInfo.GroupGuarantor == TERMS_SECONDARY_TO_ZERO))
                            UseSiteGroupForSiteShortfall();
                        else if (_termsInfo.SecGroupUse > 0 && (!(_termsInfo.SecGroupValueGuaranteed.GetValueOrDefault() || _termsInfo.SecGroupShareGuaranteed.GetValueOrDefault())) && (_termsInfo.SecGroupGuarantor == TERMS_SECONDARY_REMAINDER || _termsInfo.SecGroupGuarantor == TERMS_SECONDARY_TO_ZERO))
                            UseSecSiteGroupForSiteShortfall();
                    }
                    else
                    {
                        //so the guarantors are worked out in order
                        for (int i = 1; i == 15; i++)
                        {
                            if (_termsInfo.SupplierUse == i)
                            {
                                if (_termsInfo.SupplierGuarantor != TERMS_NONE)
                                    UseSupplierForSiteShortfall();
                            }
                            else if (_termsInfo.GroupUse == i)
                            {
                                if (_termsInfo.GroupGuarantor != TERMS_NONE)
                                    UseSiteGroupForSiteShortfall();
                            }
                            else if (_termsInfo.SecGroupUse == i)
                            {
                                if (_termsInfo.SecGroupGuarantor != TERMS_NONE)
                                    UseSecSiteGroupForSiteShortfall();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void CalculatePostTermsSiteGroupShortfall()
        {
            try
            {
                LogManager.WriteLog("Inside CalculatePostTermsSiteGroupShortfall...", LogManager.enumLogLevel.Info);

                if (_termsInfo.GroupValueGuaranteed.GetValueOrDefault() || _termsInfo.GroupShareGuaranteed.GetValueOrDefault())
                {
                    //If it is primary & secondary, then they have to be in the correct order
                    //otherwise, do 1 to 15...
                    if (((_termsInfo.SiteUse > 0 && (!(_termsInfo.SiteValueGuaranteed.GetValueOrDefault() || _termsInfo.SiteShareGuaranteed.GetValueOrDefault())) && (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PRIMARY_TO_ZERO || _termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER || _termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_SECONDARY_TO_ZERO))) ||
                        ((_termsInfo.SupplierUse > 0 && (!(_termsInfo.SupplierValueGuaranteed.GetValueOrDefault() || _termsInfo.SupplierShareGuaranteed.GetValueOrDefault())) && (_termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PRIMARY_TO_ZERO || _termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER || _termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_SECONDARY_TO_ZERO))) ||
                        ((_termsInfo.GroupUse > 0 && (!(_termsInfo.GroupValueGuaranteed.GetValueOrDefault() || _termsInfo.GroupShareGuaranteed.GetValueOrDefault())) && (_termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_PRIMARY_TO_ZERO || _termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER || _termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_SECONDARY_TO_ZERO))) ||
                        ((_termsInfo.SecGroupUse > 0 && (!(_termsInfo.SecGroupValueGuaranteed.GetValueOrDefault() || _termsInfo.SecGroupShareGuaranteed.GetValueOrDefault())) && (_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PRIMARY_TO_ZERO || _termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER || _termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_SECONDARY_TO_ZERO))))
                    {
                        //Find Primary
                        if (_termsInfo.SiteUse > 0 && (!(_termsInfo.SiteValueGuaranteed.GetValueOrDefault() || _termsInfo.SiteShareGuaranteed.GetValueOrDefault())) && (_termsInfo.SiteGuarantor == TERMS_PRIMARY_FULL || _termsInfo.SiteGuarantor == TERMS_PRIMARY_TO_ZERO))
                            UseSiteForSiteGroupShortfall();
                        if (_termsInfo.SupplierUse > 0 && (!(_termsInfo.SupplierValueGuaranteed.GetValueOrDefault() || _termsInfo.SupplierShareGuaranteed.GetValueOrDefault())) && (_termsInfo.SupplierGuarantor == TERMS_PRIMARY_FULL || _termsInfo.SupplierGuarantor == TERMS_PRIMARY_TO_ZERO))
                            UseSupplierForSiteGroupShortfall();
                        else if (_termsInfo.SecGroupUse > 0 && (!(_termsInfo.SecGroupValueGuaranteed.GetValueOrDefault() || _termsInfo.SecGroupShareGuaranteed.GetValueOrDefault())) && (_termsInfo.SecGroupGuarantor == TERMS_PRIMARY_FULL || _termsInfo.SecGroupGuarantor == TERMS_PRIMARY_TO_ZERO))
                            UseSecSiteGroupForSiteGroupShortfall();

                        //Find Secondary
                        if (_termsInfo.SiteUse > 0 && (!(_termsInfo.SiteValueGuaranteed.GetValueOrDefault() || _termsInfo.SiteShareGuaranteed.GetValueOrDefault())) && (_termsInfo.SiteGuarantor == TERMS_SECONDARY_REMAINDER || _termsInfo.SiteGuarantor == TERMS_SECONDARY_TO_ZERO))
                            UseSiteForSiteGroupShortfall();
                        if (_termsInfo.SupplierUse > 0 && (!(_termsInfo.SupplierValueGuaranteed.GetValueOrDefault() || _termsInfo.SupplierShareGuaranteed.GetValueOrDefault())) && (_termsInfo.SupplierGuarantor == TERMS_SECONDARY_REMAINDER || _termsInfo.SupplierGuarantor == TERMS_SECONDARY_TO_ZERO))
                            UseSupplierForSiteGroupShortfall();
                        else if (_termsInfo.SecGroupUse > 0 && (!(_termsInfo.SecGroupValueGuaranteed.GetValueOrDefault() || _termsInfo.SecGroupShareGuaranteed.GetValueOrDefault())) && (_termsInfo.SecGroupGuarantor == TERMS_SECONDARY_REMAINDER || _termsInfo.SecGroupGuarantor == TERMS_SECONDARY_TO_ZERO))
                            UseSecSiteGroupForSiteGroupShortfall();
                    }
                    else
                    {
                        //so the guarantors are worked out in order
                        for (int i = 1; i == 15; i++)
                        {
                            if (_termsInfo.SiteUse == i)
                            {
                                if (_termsInfo.SiteGuarantor != TERMS_NONE)
                                    UseSiteForSiteGroupShortfall();
                            }
                            else if (_termsInfo.SupplierUse == i)
                            {
                                if (_termsInfo.SupplierGuarantor != TERMS_NONE)
                                    UseSupplierForSiteGroupShortfall();
                            }
                            else if (_termsInfo.SecGroupUse == i)
                            {
                                if (_termsInfo.SecGroupGuarantor != TERMS_NONE)
                                    UseSecSiteGroupForSiteGroupShortfall();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void CalculatePostTermsSecSiteGroupShortfall()
        {
            try
            {
                LogManager.WriteLog("Inside CalculatePostTermsSecSiteGroupShortfall...", LogManager.enumLogLevel.Info);

                if (_termsInfo.SecGroupValueGuaranteed.GetValueOrDefault() || _termsInfo.SecGroupShareGuaranteed.GetValueOrDefault())
                {
                    //If it is primary & secondary, then they have to be in the correct order
                    //otherwise, do 1 to 15...
                    if (((_termsInfo.SiteUse > 0 && (!(_termsInfo.SiteValueGuaranteed.GetValueOrDefault() || _termsInfo.SiteShareGuaranteed.GetValueOrDefault())) && (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PRIMARY_TO_ZERO || _termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER || _termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_SECONDARY_TO_ZERO))) ||
                        ((_termsInfo.SecGroupUse > 0 && (!(_termsInfo.SecGroupValueGuaranteed.GetValueOrDefault() || _termsInfo.SecGroupShareGuaranteed.GetValueOrDefault())) && (_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PRIMARY_TO_ZERO || _termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER || _termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_SECONDARY_TO_ZERO))) ||
                        ((_termsInfo.GroupUse > 0 && (!(_termsInfo.GroupValueGuaranteed.GetValueOrDefault() || _termsInfo.GroupShareGuaranteed.GetValueOrDefault())) && (_termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_PRIMARY_TO_ZERO || _termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER || _termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_SECONDARY_TO_ZERO))) ||
                        ((_termsInfo.SupplierUse > 0 && (!(_termsInfo.SupplierValueGuaranteed.GetValueOrDefault() || _termsInfo.SupplierShareGuaranteed.GetValueOrDefault())) && (_termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PRIMARY_TO_ZERO || _termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER || _termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_SECONDARY_TO_ZERO))))
                    {
                        //Find Primary
                        if (_termsInfo.SiteUse > 0 && (!(_termsInfo.SiteValueGuaranteed.GetValueOrDefault() || _termsInfo.SiteShareGuaranteed.GetValueOrDefault())) && (_termsInfo.SiteGuarantor == TERMS_PRIMARY_FULL || _termsInfo.SiteGuarantor == TERMS_PRIMARY_TO_ZERO))
                            UseSiteForSecSiteGroupShortfall();
                        else if (_termsInfo.GroupUse > 0 && (!(_termsInfo.GroupValueGuaranteed.GetValueOrDefault() || _termsInfo.GroupShareGuaranteed.GetValueOrDefault())) && (_termsInfo.GroupGuarantor == TERMS_PRIMARY_FULL || _termsInfo.GroupGuarantor == TERMS_PRIMARY_TO_ZERO))
                            UseSiteGroupForSecSiteGroupShortfall();
                        if (_termsInfo.SupplierUse > 0 && (!(_termsInfo.SupplierValueGuaranteed.GetValueOrDefault() || _termsInfo.SupplierShareGuaranteed.GetValueOrDefault())) && (_termsInfo.SupplierGuarantor == TERMS_PRIMARY_FULL || _termsInfo.SupplierGuarantor == TERMS_PRIMARY_TO_ZERO))
                            UseSupplierForSecSiteGroupShortfall();

                        //Find Secondary
                        if (_termsInfo.SiteUse > 0 && (!(_termsInfo.SiteValueGuaranteed.GetValueOrDefault() || _termsInfo.SiteShareGuaranteed.GetValueOrDefault())) && (_termsInfo.SiteGuarantor == TERMS_SECONDARY_REMAINDER || _termsInfo.SiteGuarantor == TERMS_SECONDARY_TO_ZERO))
                            UseSiteForSecSiteGroupShortfall();
                        else if (_termsInfo.GroupUse > 0 && (!(_termsInfo.GroupValueGuaranteed.GetValueOrDefault() || _termsInfo.GroupShareGuaranteed.GetValueOrDefault())) && (_termsInfo.GroupGuarantor == TERMS_SECONDARY_REMAINDER || _termsInfo.GroupGuarantor == TERMS_SECONDARY_TO_ZERO))
                            UseSiteGroupForSecSiteGroupShortfall();
                        if (_termsInfo.SupplierUse > 0 && (!(_termsInfo.SupplierValueGuaranteed.GetValueOrDefault() || _termsInfo.SupplierShareGuaranteed.GetValueOrDefault())) && (_termsInfo.SupplierGuarantor == TERMS_SECONDARY_REMAINDER || _termsInfo.SupplierGuarantor == TERMS_SECONDARY_TO_ZERO))
                            UseSupplierForSecSiteGroupShortfall();
                    }
                    else
                    {
                        //so the guarantors are worked out in order
                        for (int i = 1; i == 15; i++)
                        {
                            if (_termsInfo.SiteUse == i)
                            {
                                if (_termsInfo.SiteGuarantor != TERMS_NONE)
                                    UseSiteForSecSiteGroupShortfall();
                            }
                            else if (_termsInfo.GroupUse == i)
                            {
                                if (_termsInfo.GroupGuarantor != TERMS_NONE)
                                    UseSiteGroupForSecSiteGroupShortfall();
                            }
                            else if (_termsInfo.SupplierUse == i)
                            {
                                if (_termsInfo.SupplierGuarantor != TERMS_NONE)
                                    UseSupplierForSecSiteGroupShortfall();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void CalculatePostTermsOtherPrizeShortfall()
        {
            float? _totalPercentage = 0;
            float? _supplierPerc = 0;
            float? _sitePerc = 0;
            float? _groupPerc = 0;
            float? _secGroupPerc = 0;
            float? _shortfallDifference = 0;

            try
            {
                LogManager.WriteLog("Inside CalculatePostTermsOtherPrizeShortfall...", LogManager.enumLogLevel.Info);

                if (_termsResults.PrizeChargeShouldGet.GetValueOrDefault() > _termsResults.PrizeCharge.GetValueOrDefault())
                {
                    switch (_termsInfo.OtherPrizeGuarantor.GetValueOrDefault())
                    {
                        case TERMS_SITE:
                            _termsResults.SiteShare = _termsResults.SiteShare.GetValueOrDefault() - (_termsResults.PrizeChargeShouldGet.GetValueOrDefault() - _termsResults.PrizeCharge.GetValueOrDefault());
                            _termsResults.PrizeCharge = _termsResults.PrizeChargeShouldGet.GetValueOrDefault();
                            break;
                        case TERMS_SUPPLIER:
                            _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() - (_termsResults.PrizeChargeShouldGet.GetValueOrDefault() - _termsResults.PrizeCharge.GetValueOrDefault());
                            _termsResults.PrizeCharge = _termsResults.PrizeChargeShouldGet.GetValueOrDefault();
                            break;
                        case TERMS_SITE_GROUP:
                            _termsResults.SiteGroupShare = _termsResults.SiteGroupShare.GetValueOrDefault() - (_termsResults.PrizeChargeShouldGet.GetValueOrDefault() - _termsResults.PrizeCharge.GetValueOrDefault());
                            _termsResults.PrizeCharge = _termsResults.PrizeChargeShouldGet.GetValueOrDefault();
                            break;
                        case TERMS_SECONDARY_GROUP:
                            _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() - (_termsResults.PrizeChargeShouldGet.GetValueOrDefault() - _termsResults.PrizeCharge.GetValueOrDefault());
                            _termsResults.PrizeCharge = _termsResults.PrizeChargeShouldGet.GetValueOrDefault();
                            break;
                        case TERMS_PERCENTAGE:
                            _totalPercentage = -1;
                            if (_termsInfo.SupplierUse.GetValueOrDefault() > 0 && (!(_termsInfo.SupplierValueGuaranteed.GetValueOrDefault() || _termsInfo.SupplierShareGuaranteed.GetValueOrDefault())) && _termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE)
                            {
                                if (_totalPercentage == -1)
                                    _totalPercentage = 0;

                                _supplierPerc = _termsInfo.SupplierGuarantorPercentage.GetValueOrDefault();
                                _totalPercentage = _totalPercentage.GetValueOrDefault() + _termsInfo.SupplierGuarantorPercentage.GetValueOrDefault();
                            }

                            if (_termsInfo.SiteUse.GetValueOrDefault() > 0 && (!(_termsInfo.SiteValueGuaranteed.GetValueOrDefault() || _termsInfo.SiteShareGuaranteed.GetValueOrDefault())) && _termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE)
                            {
                                if (_totalPercentage == -1)
                                    _totalPercentage = 0;

                                _sitePerc = _termsInfo.SiteGuarantorPercentage.GetValueOrDefault();
                                _totalPercentage = _totalPercentage.GetValueOrDefault() + _termsInfo.SiteGuarantorPercentage.GetValueOrDefault();
                            }

                            if (_termsInfo.GroupUse.GetValueOrDefault() > 0 && (!(_termsInfo.GroupValueGuaranteed.GetValueOrDefault() || _termsInfo.GroupShareGuaranteed.GetValueOrDefault())) && _termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE)
                            {
                                if (_totalPercentage == -1)
                                    _totalPercentage = 0;

                                _groupPerc = _termsInfo.GroupGuarantorPercentage.GetValueOrDefault();
                                _totalPercentage = _totalPercentage.GetValueOrDefault() + _termsInfo.GroupGuarantorPercentage.GetValueOrDefault();
                            }

                            if (_termsInfo.SecGroupUse.GetValueOrDefault() > 0 && (!(_termsInfo.SecGroupValueGuaranteed.GetValueOrDefault() || _termsInfo.SecGroupShareGuaranteed.GetValueOrDefault())) && _termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE)
                            {
                                if (_totalPercentage == -1)
                                    _totalPercentage = 0;

                                _secGroupPerc = _termsInfo.SecGroupGuarantorPercentage.GetValueOrDefault();
                                _totalPercentage = _totalPercentage.GetValueOrDefault() + _termsInfo.SecGroupGuarantorPercentage.GetValueOrDefault();
                            }

                            _shortfallDifference = _termsResults.PrizeChargeShouldGet.GetValueOrDefault() - _termsResults.PrizeCharge.GetValueOrDefault();

                            //if TotalPercentage = -1 then do not use perc, otherwise use perc                            
                            if (_totalPercentage >= 0)
                            {
                                if (_supplierPerc > 0)
                                {
                                    _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() - ((_shortfallDifference.GetValueOrDefault()) * (_supplierPerc.GetValueOrDefault() / _totalPercentage.GetValueOrDefault()));
                                    _termsResults.PrizeCharge = _termsResults.PrizeChargeShouldGet.GetValueOrDefault() + ((_shortfallDifference.GetValueOrDefault()) * (_supplierPerc.GetValueOrDefault() / _totalPercentage.GetValueOrDefault()));
                                }

                                if (_sitePerc > 0)
                                {
                                    _termsResults.SiteShare = _termsResults.SiteShare.GetValueOrDefault() - ((_shortfallDifference.GetValueOrDefault()) * (_sitePerc.GetValueOrDefault() / _totalPercentage.GetValueOrDefault()));
                                    _termsResults.PrizeCharge = _termsResults.PrizeChargeShouldGet.GetValueOrDefault() + ((_shortfallDifference.GetValueOrDefault()) * (_sitePerc.GetValueOrDefault() / _totalPercentage.GetValueOrDefault()));
                                }

                                if (_groupPerc > 0)
                                {
                                    _termsResults.SiteGroupShare = _termsResults.SiteGroupShare.GetValueOrDefault() - ((_shortfallDifference.GetValueOrDefault()) * (_groupPerc.GetValueOrDefault() / _totalPercentage.GetValueOrDefault()));
                                    _termsResults.PrizeCharge = _termsResults.PrizeChargeShouldGet.GetValueOrDefault() + ((_shortfallDifference.GetValueOrDefault()) * (_groupPerc.GetValueOrDefault() / _totalPercentage.GetValueOrDefault()));
                                }

                                if (_secGroupPerc > 0)
                                {
                                    _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() - ((_shortfallDifference.GetValueOrDefault()) * (_secGroupPerc.GetValueOrDefault() / _totalPercentage.GetValueOrDefault()));
                                    _termsResults.PrizeCharge = _termsResults.PrizeChargeShouldGet.GetValueOrDefault() + ((_shortfallDifference.GetValueOrDefault()) * (_secGroupPerc.GetValueOrDefault() / _totalPercentage.GetValueOrDefault()));
                                }
                            }

                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void CalculatePostTermsOtherConsultancyShortfall()
        {
            float? _totalPercentage = 0;
            float? _supplierPerc = 0;
            float? _sitePerc = 0;
            float? _groupPerc = 0;
            float? _secGroupPerc = 0;
            float? _shortfallDifference = 0;

            try
            {
                LogManager.WriteLog("Inside CalculatePostTermsOtherConsultancyShortfall...", LogManager.enumLogLevel.Info);

                if (_termsResults.ConsultancyChargeShouldGet.GetValueOrDefault() > _termsResults.ConsultancyCharge.GetValueOrDefault())
                {
                    switch (_termsInfo.OtherConsultancyGuarantor.GetValueOrDefault())
                    {
                        case TERMS_SITE:
                            _termsResults.SiteShare = _termsResults.SiteShare.GetValueOrDefault() - (_termsResults.ConsultancyChargeShouldGet.GetValueOrDefault() - _termsResults.ConsultancyCharge.GetValueOrDefault());
                            _termsResults.ConsultancyCharge = _termsResults.ConsultancyChargeShouldGet.GetValueOrDefault();
                            break;
                        case TERMS_SUPPLIER:
                            _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() - (_termsResults.ConsultancyChargeShouldGet.GetValueOrDefault() - _termsResults.ConsultancyCharge.GetValueOrDefault());
                            _termsResults.ConsultancyCharge = _termsResults.ConsultancyChargeShouldGet.GetValueOrDefault();
                            break;
                        case TERMS_SITE_GROUP:
                            _termsResults.SiteGroupShare = _termsResults.SiteGroupShare.GetValueOrDefault() - (_termsResults.ConsultancyChargeShouldGet.GetValueOrDefault() - _termsResults.ConsultancyCharge.GetValueOrDefault());
                            _termsResults.ConsultancyCharge = _termsResults.ConsultancyChargeShouldGet.GetValueOrDefault();
                            break;
                        case TERMS_SECONDARY_GROUP:
                            _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() - (_termsResults.ConsultancyChargeShouldGet.GetValueOrDefault() - _termsResults.ConsultancyCharge.GetValueOrDefault());
                            _termsResults.ConsultancyCharge = _termsResults.ConsultancyChargeShouldGet.GetValueOrDefault();
                            break;
                        case TERMS_PERCENTAGE:
                            _totalPercentage = -1;
                            if (_termsInfo.SupplierUse.GetValueOrDefault() > 0 && (!(_termsInfo.SupplierValueGuaranteed.GetValueOrDefault() || _termsInfo.SupplierShareGuaranteed.GetValueOrDefault())) && _termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE)
                            {
                                if (_totalPercentage == -1)
                                    _totalPercentage = 0;

                                _supplierPerc = _termsInfo.SupplierGuarantorPercentage.GetValueOrDefault();
                                _totalPercentage = _totalPercentage.GetValueOrDefault() + _termsInfo.SupplierGuarantorPercentage.GetValueOrDefault();
                            }

                            if (_termsInfo.SiteUse.GetValueOrDefault() > 0 && (!(_termsInfo.SiteValueGuaranteed.GetValueOrDefault() || _termsInfo.SiteShareGuaranteed.GetValueOrDefault())) && _termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE)
                            {
                                if (_totalPercentage == -1)
                                    _totalPercentage = 0;

                                _sitePerc = _termsInfo.SiteGuarantorPercentage.GetValueOrDefault();
                                _totalPercentage = _totalPercentage.GetValueOrDefault() + _termsInfo.SiteGuarantorPercentage.GetValueOrDefault();
                            }

                            if (_termsInfo.GroupUse.GetValueOrDefault() > 0 && (!(_termsInfo.GroupValueGuaranteed.GetValueOrDefault() || _termsInfo.GroupShareGuaranteed.GetValueOrDefault())) && _termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE)
                            {
                                if (_totalPercentage == -1)
                                    _totalPercentage = 0;

                                _groupPerc = _termsInfo.GroupGuarantorPercentage.GetValueOrDefault();
                                _totalPercentage = _totalPercentage.GetValueOrDefault() + _termsInfo.GroupGuarantorPercentage.GetValueOrDefault();
                            }

                            if (_termsInfo.SecGroupUse.GetValueOrDefault() > 0 && (!(_termsInfo.SecGroupValueGuaranteed.GetValueOrDefault() || _termsInfo.SecGroupShareGuaranteed.GetValueOrDefault())) && _termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE)
                            {
                                if (_totalPercentage == -1)
                                    _totalPercentage = 0;

                                _secGroupPerc = _termsInfo.SecGroupGuarantorPercentage.GetValueOrDefault();
                                _totalPercentage = _totalPercentage.GetValueOrDefault() + _termsInfo.SecGroupGuarantorPercentage.GetValueOrDefault();
                            }

                            _shortfallDifference = _termsResults.ConsultancyChargeShouldGet.GetValueOrDefault() - _termsResults.ConsultancyCharge.GetValueOrDefault();

                            //if TotalPercentage = -1 then do not use perc, otherwise use perc                            
                            if (_totalPercentage >= 0)
                            {
                                if (_supplierPerc > 0)
                                {
                                    _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() - ((_shortfallDifference.GetValueOrDefault()) * (_supplierPerc.GetValueOrDefault() / _totalPercentage.GetValueOrDefault()));
                                    _termsResults.ConsultancyCharge = _termsResults.ConsultancyChargeShouldGet.GetValueOrDefault() + ((_shortfallDifference.GetValueOrDefault()) * (_supplierPerc.GetValueOrDefault() / _totalPercentage.GetValueOrDefault()));
                                }

                                if (_sitePerc > 0)
                                {
                                    _termsResults.SiteShare = _termsResults.SiteShare.GetValueOrDefault() - ((_shortfallDifference.GetValueOrDefault()) * (_sitePerc.GetValueOrDefault() / _totalPercentage.GetValueOrDefault()));
                                    _termsResults.ConsultancyCharge = _termsResults.ConsultancyChargeShouldGet.GetValueOrDefault() + ((_shortfallDifference.GetValueOrDefault()) * (_sitePerc.GetValueOrDefault() / _totalPercentage.GetValueOrDefault()));
                                }

                                if (_groupPerc > 0)
                                {
                                    _termsResults.SiteGroupShare = _termsResults.SiteGroupShare.GetValueOrDefault() - ((_shortfallDifference.GetValueOrDefault()) * (_groupPerc.GetValueOrDefault() / _totalPercentage.GetValueOrDefault()));
                                    _termsResults.ConsultancyCharge = _termsResults.ConsultancyChargeShouldGet.GetValueOrDefault() + ((_shortfallDifference.GetValueOrDefault()) * (_groupPerc.GetValueOrDefault() / _totalPercentage.GetValueOrDefault()));
                                }

                                if (_secGroupPerc > 0)
                                {
                                    _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() - ((_shortfallDifference.GetValueOrDefault()) * (_secGroupPerc.GetValueOrDefault() / _totalPercentage.GetValueOrDefault()));
                                    _termsResults.ConsultancyCharge = _termsResults.ConsultancyChargeShouldGet.GetValueOrDefault() + ((_shortfallDifference.GetValueOrDefault()) * (_secGroupPerc.GetValueOrDefault() / _totalPercentage.GetValueOrDefault()));
                                }
                            }

                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void CalculatePostTermsOtherRoyaltyShortfall()
        {
            float? _totalPercentage = 0;
            float? _supplierPerc = 0;
            float? _sitePerc = 0;
            float? _groupPerc = 0;
            float? _secGroupPerc = 0;
            float? _shortfallDifference = 0;

            try
            {
                LogManager.WriteLog("Inside CalculatePostTermsOtherRoyaltyShortfall...", LogManager.enumLogLevel.Info);

                if (_termsResults.RoyaltyChargeShouldGet.GetValueOrDefault() > _termsResults.RoyaltyCharge.GetValueOrDefault())
                {
                    switch (_termsInfo.OtherRoyaltyGuarantor.GetValueOrDefault())
                    {
                        case TERMS_SITE:
                            _termsResults.SiteShare = _termsResults.SiteShare.GetValueOrDefault() - (_termsResults.RoyaltyChargeShouldGet.GetValueOrDefault() - _termsResults.RoyaltyCharge.GetValueOrDefault());
                            _termsResults.RoyaltyCharge = _termsResults.RoyaltyChargeShouldGet.GetValueOrDefault();
                            break;
                        case TERMS_SUPPLIER:
                            _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() - (_termsResults.RoyaltyChargeShouldGet.GetValueOrDefault() - _termsResults.RoyaltyCharge.GetValueOrDefault());
                            _termsResults.RoyaltyCharge = _termsResults.RoyaltyChargeShouldGet.GetValueOrDefault();
                            break;
                        case TERMS_SITE_GROUP:
                            _termsResults.SiteGroupShare = _termsResults.SiteGroupShare.GetValueOrDefault() - (_termsResults.RoyaltyChargeShouldGet.GetValueOrDefault() - _termsResults.RoyaltyCharge.GetValueOrDefault());
                            _termsResults.RoyaltyCharge = _termsResults.RoyaltyChargeShouldGet.GetValueOrDefault();
                            break;
                        case TERMS_SECONDARY_GROUP:
                            _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() - (_termsResults.RoyaltyChargeShouldGet.GetValueOrDefault() - _termsResults.RoyaltyCharge.GetValueOrDefault());
                            _termsResults.RoyaltyCharge = _termsResults.RoyaltyChargeShouldGet.GetValueOrDefault();
                            break;
                        case TERMS_PERCENTAGE:
                            _totalPercentage = -1;
                            if (_termsInfo.SupplierUse.GetValueOrDefault() > 0 && (!(_termsInfo.SupplierValueGuaranteed.GetValueOrDefault() || _termsInfo.SupplierShareGuaranteed.GetValueOrDefault())) && _termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE)
                            {
                                if (_totalPercentage == -1)
                                    _totalPercentage = 0;

                                _supplierPerc = _termsInfo.SupplierGuarantorPercentage.GetValueOrDefault();
                                _totalPercentage = _totalPercentage.GetValueOrDefault() + _termsInfo.SupplierGuarantorPercentage.GetValueOrDefault();
                            }

                            if (_termsInfo.SiteUse.GetValueOrDefault() > 0 && (!(_termsInfo.SiteValueGuaranteed.GetValueOrDefault() || _termsInfo.SiteShareGuaranteed.GetValueOrDefault())) && _termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE)
                            {
                                if (_totalPercentage == -1)
                                    _totalPercentage = 0;

                                _sitePerc = _termsInfo.SiteGuarantorPercentage.GetValueOrDefault();
                                _totalPercentage = _totalPercentage.GetValueOrDefault() + _termsInfo.SiteGuarantorPercentage.GetValueOrDefault();
                            }

                            if (_termsInfo.GroupUse.GetValueOrDefault() > 0 && (!(_termsInfo.GroupValueGuaranteed.GetValueOrDefault() || _termsInfo.GroupShareGuaranteed.GetValueOrDefault())) && _termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE)
                            {
                                if (_totalPercentage == -1)
                                    _totalPercentage = 0;

                                _groupPerc = _termsInfo.GroupGuarantorPercentage.GetValueOrDefault();
                                _totalPercentage = _totalPercentage.GetValueOrDefault() + _termsInfo.GroupGuarantorPercentage.GetValueOrDefault();
                            }

                            if (_termsInfo.SecGroupUse.GetValueOrDefault() > 0 && (!(_termsInfo.SecGroupValueGuaranteed.GetValueOrDefault() || _termsInfo.SecGroupShareGuaranteed.GetValueOrDefault())) && _termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE)
                            {
                                if (_totalPercentage == -1)
                                    _totalPercentage = 0;

                                _secGroupPerc = _termsInfo.SecGroupGuarantorPercentage.GetValueOrDefault();
                                _totalPercentage = _totalPercentage.GetValueOrDefault() + _termsInfo.SecGroupGuarantorPercentage.GetValueOrDefault();
                            }

                            _shortfallDifference = _termsResults.RoyaltyChargeShouldGet.GetValueOrDefault() - _termsResults.RoyaltyCharge.GetValueOrDefault();

                            //if TotalPercentage = -1 then do not use perc, otherwise use perc                            
                            if (_totalPercentage >= 0)
                            {
                                if (_supplierPerc > 0)
                                {
                                    _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() - ((_shortfallDifference.GetValueOrDefault()) * (_supplierPerc.GetValueOrDefault() / _totalPercentage.GetValueOrDefault()));
                                    _termsResults.RoyaltyCharge = _termsResults.RoyaltyChargeShouldGet.GetValueOrDefault() + ((_shortfallDifference.GetValueOrDefault()) * (_supplierPerc.GetValueOrDefault() / _totalPercentage.GetValueOrDefault()));
                                }

                                if (_sitePerc > 0)
                                {
                                    _termsResults.SiteShare = _termsResults.SiteShare.GetValueOrDefault() - ((_shortfallDifference.GetValueOrDefault()) * (_sitePerc.GetValueOrDefault() / _totalPercentage.GetValueOrDefault()));
                                    _termsResults.RoyaltyCharge = _termsResults.RoyaltyChargeShouldGet.GetValueOrDefault() + ((_shortfallDifference.GetValueOrDefault()) * (_sitePerc.GetValueOrDefault() / _totalPercentage.GetValueOrDefault()));
                                }

                                if (_groupPerc > 0)
                                {
                                    _termsResults.SiteGroupShare = _termsResults.SiteGroupShare.GetValueOrDefault() - ((_shortfallDifference.GetValueOrDefault()) * (_groupPerc.GetValueOrDefault() / _totalPercentage.GetValueOrDefault()));
                                    _termsResults.RoyaltyCharge = _termsResults.RoyaltyChargeShouldGet.GetValueOrDefault() + ((_shortfallDifference.GetValueOrDefault()) * (_groupPerc.GetValueOrDefault() / _totalPercentage.GetValueOrDefault()));
                                }

                                if (_secGroupPerc > 0)
                                {
                                    _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() - ((_shortfallDifference.GetValueOrDefault()) * (_secGroupPerc.GetValueOrDefault() / _totalPercentage.GetValueOrDefault()));
                                    _termsResults.RoyaltyCharge = _termsResults.RoyaltyChargeShouldGet.GetValueOrDefault() + ((_shortfallDifference.GetValueOrDefault()) * (_secGroupPerc.GetValueOrDefault() / _totalPercentage.GetValueOrDefault()));
                                }
                            }

                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void CalculatePostTermsOtherOther1Shortfall()
        {
            float? _totalPercentage = 0;
            float? _supplierPerc = 0;
            float? _sitePerc = 0;
            float? _groupPerc = 0;
            float? _secGroupPerc = 0;
            float? _shortfallDifference = 0;

            try
            {
                LogManager.WriteLog("Inside CalculatePostTermsOtherOther1Shortfall...", LogManager.enumLogLevel.Info);

                if (_termsResults.Other1ChargeShouldGet.GetValueOrDefault() > _termsResults.Other1Charge.GetValueOrDefault())
                {
                    switch (_termsInfo.OtherOther1Guarantor.GetValueOrDefault())
                    {
                        case TERMS_SITE:
                            _termsResults.SiteShare = _termsResults.SiteShare.GetValueOrDefault() - (_termsResults.Other1ChargeShouldGet.GetValueOrDefault() - _termsResults.Other1Charge.GetValueOrDefault());
                            _termsResults.Other1Charge = _termsResults.Other1ChargeShouldGet.GetValueOrDefault();
                            break;
                        case TERMS_SUPPLIER:
                            _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() - (_termsResults.Other1ChargeShouldGet.GetValueOrDefault() - _termsResults.Other1Charge.GetValueOrDefault());
                            _termsResults.Other1Charge = _termsResults.Other1ChargeShouldGet.GetValueOrDefault();
                            break;
                        case TERMS_SITE_GROUP:
                            _termsResults.SiteGroupShare = _termsResults.SiteGroupShare.GetValueOrDefault() - (_termsResults.Other1ChargeShouldGet.GetValueOrDefault() - _termsResults.Other1Charge.GetValueOrDefault());
                            _termsResults.Other1Charge = _termsResults.Other1ChargeShouldGet.GetValueOrDefault();
                            break;
                        case TERMS_SECONDARY_GROUP:
                            _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() - (_termsResults.Other1ChargeShouldGet.GetValueOrDefault() - _termsResults.Other1Charge.GetValueOrDefault());
                            _termsResults.Other1Charge = _termsResults.Other1ChargeShouldGet.GetValueOrDefault();
                            break;
                        case TERMS_PERCENTAGE:
                            _totalPercentage = -1;
                            if (_termsInfo.SupplierUse.GetValueOrDefault() > 0 && (!(_termsInfo.SupplierValueGuaranteed.GetValueOrDefault() || _termsInfo.SupplierShareGuaranteed.GetValueOrDefault())) && _termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE)
                            {
                                if (_totalPercentage == -1)
                                    _totalPercentage = 0;

                                _supplierPerc = _termsInfo.SupplierGuarantorPercentage.GetValueOrDefault();
                                _totalPercentage = _totalPercentage.GetValueOrDefault() + _termsInfo.SupplierGuarantorPercentage.GetValueOrDefault();
                            }

                            if (_termsInfo.SiteUse.GetValueOrDefault() > 0 && (!(_termsInfo.SiteValueGuaranteed.GetValueOrDefault() || _termsInfo.SiteShareGuaranteed.GetValueOrDefault())) && _termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE)
                            {
                                if (_totalPercentage == -1)
                                    _totalPercentage = 0;

                                _sitePerc = _termsInfo.SiteGuarantorPercentage.GetValueOrDefault();
                                _totalPercentage = _totalPercentage.GetValueOrDefault() + _termsInfo.SiteGuarantorPercentage.GetValueOrDefault();
                            }

                            if (_termsInfo.GroupUse.GetValueOrDefault() > 0 && (!(_termsInfo.GroupValueGuaranteed.GetValueOrDefault() || _termsInfo.GroupShareGuaranteed.GetValueOrDefault())) && _termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE)
                            {
                                if (_totalPercentage == -1)
                                    _totalPercentage = 0;

                                _groupPerc = _termsInfo.GroupGuarantorPercentage.GetValueOrDefault();
                                _totalPercentage = _totalPercentage.GetValueOrDefault() + _termsInfo.GroupGuarantorPercentage.GetValueOrDefault();
                            }

                            if (_termsInfo.SecGroupUse.GetValueOrDefault() > 0 && (!(_termsInfo.SecGroupValueGuaranteed.GetValueOrDefault() || _termsInfo.SecGroupShareGuaranteed.GetValueOrDefault())) && _termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE)
                            {
                                if (_totalPercentage == -1)
                                    _totalPercentage = 0;

                                _secGroupPerc = _termsInfo.SecGroupGuarantorPercentage.GetValueOrDefault();
                                _totalPercentage = _totalPercentage.GetValueOrDefault() + _termsInfo.SecGroupGuarantorPercentage.GetValueOrDefault();
                            }

                            _shortfallDifference = _termsResults.Other1ChargeShouldGet.GetValueOrDefault() - _termsResults.Other1Charge.GetValueOrDefault();

                            //if TotalPercentage = -1 then do not use perc, otherwise use perc                            
                            if (_totalPercentage >= 0)
                            {
                                if (_supplierPerc > 0)
                                {
                                    _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() - ((_shortfallDifference.GetValueOrDefault()) * (_supplierPerc.GetValueOrDefault() / _totalPercentage.GetValueOrDefault()));
                                    _termsResults.Other1Charge = _termsResults.Other1ChargeShouldGet.GetValueOrDefault() + ((_shortfallDifference.GetValueOrDefault()) * (_supplierPerc.GetValueOrDefault() / _totalPercentage.GetValueOrDefault()));
                                }

                                if (_sitePerc > 0)
                                {
                                    _termsResults.SiteShare = _termsResults.SiteShare.GetValueOrDefault() - ((_shortfallDifference.GetValueOrDefault()) * (_sitePerc.GetValueOrDefault() / _totalPercentage.GetValueOrDefault()));
                                    _termsResults.Other1Charge = _termsResults.Other1ChargeShouldGet.GetValueOrDefault() + ((_shortfallDifference.GetValueOrDefault()) * (_sitePerc.GetValueOrDefault() / _totalPercentage.GetValueOrDefault()));
                                }

                                if (_groupPerc > 0)
                                {
                                    _termsResults.SiteGroupShare = _termsResults.SiteGroupShare.GetValueOrDefault() - ((_shortfallDifference.GetValueOrDefault()) * (_groupPerc.GetValueOrDefault() / _totalPercentage.GetValueOrDefault()));
                                    _termsResults.Other1Charge = _termsResults.Other1ChargeShouldGet.GetValueOrDefault() + ((_shortfallDifference.GetValueOrDefault()) * (_groupPerc.GetValueOrDefault() / _totalPercentage.GetValueOrDefault()));
                                }

                                if (_secGroupPerc > 0)
                                {
                                    _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() - ((_shortfallDifference.GetValueOrDefault()) * (_secGroupPerc.GetValueOrDefault() / _totalPercentage.GetValueOrDefault()));
                                    _termsResults.Other1Charge = _termsResults.Other1ChargeShouldGet.GetValueOrDefault() + ((_shortfallDifference.GetValueOrDefault()) * (_secGroupPerc.GetValueOrDefault() / _totalPercentage.GetValueOrDefault()));
                                }
                            }

                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void CalculatePostTermsOtherOther2Shortfall()
        {
            float? _totalPercentage = 0;
            float? _supplierPerc = 0;
            float? _sitePerc = 0;
            float? _groupPerc = 0;
            float? _secGroupPerc = 0;
            float? _shortfallDifference = 0;

            try
            {
                LogManager.WriteLog("Inside CalculatePostTermsOtherOther2Shortfall...", LogManager.enumLogLevel.Info);

                if (_termsResults.Other2ChargeShouldGet.GetValueOrDefault() > _termsResults.Other2Charge.GetValueOrDefault())
                {
                    switch (_termsInfo.OtherOther2Guarantor.GetValueOrDefault())
                    {
                        case TERMS_SITE:
                            _termsResults.SiteShare = _termsResults.SiteShare.GetValueOrDefault() - (_termsResults.Other2ChargeShouldGet.GetValueOrDefault() - _termsResults.Other2Charge.GetValueOrDefault());
                            _termsResults.Other2Charge = _termsResults.Other2ChargeShouldGet.GetValueOrDefault();
                            break;
                        case TERMS_SUPPLIER:
                            _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() - (_termsResults.Other2ChargeShouldGet.GetValueOrDefault() - _termsResults.Other2Charge.GetValueOrDefault());
                            _termsResults.Other2Charge = _termsResults.Other2ChargeShouldGet.GetValueOrDefault();
                            break;
                        case TERMS_SITE_GROUP:
                            _termsResults.SiteGroupShare = _termsResults.SiteGroupShare.GetValueOrDefault() - (_termsResults.Other2ChargeShouldGet.GetValueOrDefault() - _termsResults.Other2Charge.GetValueOrDefault());
                            _termsResults.Other2Charge = _termsResults.Other2ChargeShouldGet.GetValueOrDefault();
                            break;
                        case TERMS_SECONDARY_GROUP:
                            _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() - (_termsResults.Other2ChargeShouldGet.GetValueOrDefault() - _termsResults.Other2Charge.GetValueOrDefault());
                            _termsResults.Other2Charge = _termsResults.Other2ChargeShouldGet.GetValueOrDefault();
                            break;
                        case TERMS_PERCENTAGE:
                            _totalPercentage = -1;
                            if (_termsInfo.SupplierUse.GetValueOrDefault() > 0 && (!(_termsInfo.SupplierValueGuaranteed.GetValueOrDefault() || _termsInfo.SupplierShareGuaranteed.GetValueOrDefault())) && _termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE)
                            {
                                if (_totalPercentage == -1)
                                    _totalPercentage = 0;

                                _supplierPerc = _termsInfo.SupplierGuarantorPercentage.GetValueOrDefault();
                                _totalPercentage = _totalPercentage.GetValueOrDefault() + _termsInfo.SupplierGuarantorPercentage.GetValueOrDefault();
                            }

                            if (_termsInfo.SiteUse.GetValueOrDefault() > 0 && (!(_termsInfo.SiteValueGuaranteed.GetValueOrDefault() || _termsInfo.SiteShareGuaranteed.GetValueOrDefault())) && _termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE)
                            {
                                if (_totalPercentage == -1)
                                    _totalPercentage = 0;

                                _sitePerc = _termsInfo.SiteGuarantorPercentage.GetValueOrDefault();
                                _totalPercentage = _totalPercentage.GetValueOrDefault() + _termsInfo.SiteGuarantorPercentage.GetValueOrDefault();
                            }

                            if (_termsInfo.GroupUse.GetValueOrDefault() > 0 && (!(_termsInfo.GroupValueGuaranteed.GetValueOrDefault() || _termsInfo.GroupShareGuaranteed.GetValueOrDefault())) && _termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE)
                            {
                                if (_totalPercentage == -1)
                                    _totalPercentage = 0;

                                _groupPerc = _termsInfo.GroupGuarantorPercentage.GetValueOrDefault();
                                _totalPercentage = _totalPercentage.GetValueOrDefault() + _termsInfo.GroupGuarantorPercentage.GetValueOrDefault();
                            }

                            if (_termsInfo.SecGroupUse.GetValueOrDefault() > 0 && (!(_termsInfo.SecGroupValueGuaranteed.GetValueOrDefault() || _termsInfo.SecGroupShareGuaranteed.GetValueOrDefault())) && _termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE)
                            {
                                if (_totalPercentage == -1)
                                    _totalPercentage = 0;

                                _secGroupPerc = _termsInfo.SecGroupGuarantorPercentage.GetValueOrDefault();
                                _totalPercentage = _totalPercentage.GetValueOrDefault() + _termsInfo.SecGroupGuarantorPercentage.GetValueOrDefault();
                            }

                            _shortfallDifference = _termsResults.Other2ChargeShouldGet.GetValueOrDefault() - _termsResults.Other2Charge.GetValueOrDefault();

                            //if TotalPercentage = -1 then do not use perc, otherwise use perc                            
                            if (_totalPercentage >= 0)
                            {
                                if (_supplierPerc > 0)
                                {
                                    _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() - ((_shortfallDifference.GetValueOrDefault()) * (_supplierPerc.GetValueOrDefault() / _totalPercentage.GetValueOrDefault()));
                                    _termsResults.Other2Charge = _termsResults.Other2ChargeShouldGet.GetValueOrDefault() + ((_shortfallDifference.GetValueOrDefault()) * (_supplierPerc.GetValueOrDefault() / _totalPercentage.GetValueOrDefault()));
                                }

                                if (_sitePerc > 0)
                                {
                                    _termsResults.SiteShare = _termsResults.SiteShare.GetValueOrDefault() - ((_shortfallDifference.GetValueOrDefault()) * (_sitePerc.GetValueOrDefault() / _totalPercentage.GetValueOrDefault()));
                                    _termsResults.Other2Charge = _termsResults.Other2ChargeShouldGet.GetValueOrDefault() + ((_shortfallDifference.GetValueOrDefault()) * (_sitePerc.GetValueOrDefault() / _totalPercentage.GetValueOrDefault()));
                                }

                                if (_groupPerc > 0)
                                {
                                    _termsResults.SiteGroupShare = _termsResults.SiteGroupShare.GetValueOrDefault() - ((_shortfallDifference.GetValueOrDefault()) * (_groupPerc.GetValueOrDefault() / _totalPercentage.GetValueOrDefault()));
                                    _termsResults.Other2Charge = _termsResults.Other2ChargeShouldGet.GetValueOrDefault() + ((_shortfallDifference.GetValueOrDefault()) * (_groupPerc.GetValueOrDefault() / _totalPercentage.GetValueOrDefault()));
                                }

                                if (_secGroupPerc > 0)
                                {
                                    _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() - ((_shortfallDifference.GetValueOrDefault()) * (_secGroupPerc.GetValueOrDefault() / _totalPercentage.GetValueOrDefault()));
                                    _termsResults.Other2Charge = _termsResults.Other2ChargeShouldGet.GetValueOrDefault() + ((_shortfallDifference.GetValueOrDefault()) * (_secGroupPerc.GetValueOrDefault() / _totalPercentage.GetValueOrDefault()));
                                }
                            }

                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void CalculatePostTermsOtherLicence()
        {
            try
            {
                LogManager.WriteLog("Inside CalculatePostTermsOtherLicence...", LogManager.enumLogLevel.Info);

                if (_termsInfo.OtherLicenceFrequency.GetValueOrDefault() == TERMS_FREQUENCY_PER_COLLECTION)
                {
                    if (_termsInfo.CollectionDays.GetValueOrDefault() > 0)
                        _termsResults.LicenceChargeShouldGet = _termsInfo.OtherLicenceCharge.GetValueOrDefault() * (_termsInfo.CollectionDays.GetValueOrDefault() / _termsInfo.CollectionDays.GetValueOrDefault()); //Downdays put in by request of Crown, then taken out by request of Crown!
                    else
                        _termsResults.LicenceChargeShouldGet = _termsInfo.OtherLicenceCharge.GetValueOrDefault();
                }
                else //TERMS_FREQUENCY_PER_WEEK
                {
                    _termsResults.LicenceChargeShouldGet = (_termsInfo.OtherLicenceCharge.GetValueOrDefault() / 7) * _termsInfo.CollectionDays.GetValueOrDefault(); //Downdays put in by request of Crown, then taken out by request of Crown!
                }

                _termsResults.LicenceChargeWeekly = _termsInfo.OtherLicenceCharge.GetValueOrDefault();

                if (_termsInfo.OtherLicenceVAT.GetValueOrDefault() == true)
                    _termsResults.LicenceChargeShouldGet = _termsResults.LicenceChargeShouldGet.GetValueOrDefault() * (1 + SystemVATRate);

                switch (_termsInfo.OtherLicencePaidBy.GetValueOrDefault())
                {
                    case TERMS_CASH_BOX:
                        // if this is set to before the Output VAT, then
                        // decrement the CashBoxVatable and the remainder
                        // if it is after, then
                        // decrement the remainder
                        if (_termsInfo.OtherLicenceUse.GetValueOrDefault() < _termsInfo.VATOutputUse.GetValueOrDefault())
                            _termsResults.CollectionValueOutputVatable = Max<float>((_termsResults.CollectionValueOutputVatable.GetValueOrDefault() - _termsResults.LicenceChargeShouldGet.GetValueOrDefault()), 0);

                        if (_termsResults.Remainder.GetValueOrDefault() >= _termsResults.LicenceChargeShouldGet.GetValueOrDefault())
                        {
                            _termsResults.LicenceCharge = _termsResults.LicenceChargeShouldGet.GetValueOrDefault();
                            _termsResults.Remainder = _termsResults.Remainder.GetValueOrDefault() - _termsResults.LicenceChargeShouldGet.GetValueOrDefault();
                        }
                        else
                        {
                            _termsResults.LicenceCharge = _termsResults.Remainder.GetValueOrDefault();
                            _termsResults.Remainder = 0;
                        }
                        break;
                    case TERMS_SUPPLIER:
                        _termsResults.LicenceCharge = _termsResults.LicenceChargeShouldGet.GetValueOrDefault();
                        _termsResults.SupplierRent = _termsResults.SupplierRent.GetValueOrDefault() - _termsResults.LicenceChargeShouldGet.GetValueOrDefault();
                        break;
                    case TERMS_SITE:
                        _termsResults.LicenceCharge = _termsResults.LicenceChargeShouldGet.GetValueOrDefault();
                        _termsResults.SiteRent = _termsResults.SiteRent.GetValueOrDefault() - _termsResults.LicenceChargeShouldGet.GetValueOrDefault();
                        break;
                    case TERMS_SITE_GROUP:
                        _termsResults.LicenceCharge = _termsResults.LicenceChargeShouldGet.GetValueOrDefault();
                        _termsResults.SiteGroupRent = _termsResults.SiteGroupRent.GetValueOrDefault() - _termsResults.LicenceChargeShouldGet.GetValueOrDefault();
                        break;
                    case TERMS_SECONDARY_GROUP:
                        _termsResults.LicenceCharge = _termsResults.LicenceChargeShouldGet.GetValueOrDefault();
                        _termsResults.SecSiteGroupRent = _termsResults.SecSiteGroupRent.GetValueOrDefault() - _termsResults.LicenceChargeShouldGet.GetValueOrDefault();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void CalculatePostTermsOtherPrizeCash()
        {
            try
            {
                LogManager.WriteLog("Inside CalculatePostTermsOtherPrizeCash...", LogManager.enumLogLevel.Info);

                if (_termsInfo.OtherPrizeFrequency.GetValueOrDefault() == TERMS_FREQUENCY_PER_COLLECTION)
                    _termsResults.PrizeChargeShouldGet = _termsResults.InputPrizeValue.GetValueOrDefault();
                else //TERMS_FREQUENCY_PER_WEEK
                    _termsResults.PrizeChargeShouldGet = (_termsResults.InputPrizeValue.GetValueOrDefault() / 7) * _termsInfo.CollectionDays.GetValueOrDefault();

                if (_termsInfo.OtherPrizeVAT.GetValueOrDefault())
                    _termsResults.PrizeChargeShouldGet = Format2DPs(_termsResults.PrizeChargeShouldGet.GetValueOrDefault() * (1 + SystemVATRate));

                switch (_termsInfo.OtherPrizePaidBy.GetValueOrDefault())
                {
                    case TERMS_CASH_BOX:
                        //if this is set to before the Output VAT, then
                        //   decrement the CashBoxVatable and the remainder
                        //if it is after, then
                        //   decrement the remainder
                        if (_termsInfo.OtherPrizeUse.GetValueOrDefault() < _termsInfo.VATOutputUse.GetValueOrDefault())
                            _termsResults.CollectionValueOutputVatable = Max<float>(_termsResults.CollectionValueOutputVatable.GetValueOrDefault() - _termsResults.PrizeChargeShouldGet.GetValueOrDefault(), 0);

                        if (_termsResults.Remainder >= _termsResults.PrizeChargeShouldGet)
                        {
                            _termsResults.PrizeCharge = _termsResults.PrizeChargeShouldGet.GetValueOrDefault();
                            _termsResults.Remainder = _termsResults.Remainder.GetValueOrDefault() - _termsResults.PrizeChargeShouldGet.GetValueOrDefault();
                        }
                        else
                        {
                            _termsResults.PrizeCharge = _termsResults.Remainder.GetValueOrDefault();
                            _termsResults.Remainder = 0;
                        }
                        break;
                    case TERMS_SUPPLIER:
                        _termsResults.PrizeCharge = _termsResults.PrizeChargeShouldGet.GetValueOrDefault();
                        _termsResults.SupplierRent = _termsResults.SupplierRent.GetValueOrDefault() - _termsResults.PrizeChargeShouldGet.GetValueOrDefault();
                        break;
                    case TERMS_SITE:
                        _termsResults.PrizeCharge = _termsResults.PrizeChargeShouldGet.GetValueOrDefault();
                        _termsResults.SiteRent = _termsResults.SiteRent.GetValueOrDefault() - _termsResults.PrizeChargeShouldGet.GetValueOrDefault();
                        break;
                    case TERMS_SITE_GROUP:
                        _termsResults.PrizeCharge = _termsResults.PrizeChargeShouldGet.GetValueOrDefault();
                        _termsResults.SiteGroupRent = _termsResults.SiteGroupRent.GetValueOrDefault() - _termsResults.PrizeChargeShouldGet.GetValueOrDefault();
                        break;
                    case TERMS_SECONDARY_GROUP:
                        _termsResults.PrizeCharge = _termsResults.PrizeChargeShouldGet.GetValueOrDefault();
                        _termsResults.SecSiteGroupRent = _termsResults.SecSiteGroupRent.GetValueOrDefault() - _termsResults.PrizeChargeShouldGet.GetValueOrDefault();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void CalculatePostTermsOtherPrizeGoods()
        {
            //to be done after all other calculations
            try
            {
                LogManager.WriteLog("Inside CalculatePostTermsOtherPrizeGoods...", LogManager.enumLogLevel.Info);

                if (_termsInfo.OtherPrizeFrequency.GetValueOrDefault() == TERMS_FREQUENCY_PER_COLLECTION)
                    _termsResults.PrizeChargeShouldGet = _termsInfo.OtherPrizeCharge.GetValueOrDefault();
                else //TERMS_FREQUENCY_PER_WEEK
                    _termsResults.PrizeChargeShouldGet = (_termsInfo.OtherPrizeCharge.GetValueOrDefault() / 7) * _termsInfo.CollectionDays.GetValueOrDefault();

                if (_termsInfo.OtherPrizeVAT.GetValueOrDefault())
                    _termsResults.PrizeChargeShouldGet = Format2DPs(_termsResults.PrizeChargeShouldGet.GetValueOrDefault() * (1 + SystemVATRate));

                switch (_termsInfo.OtherPrizePaidBy.GetValueOrDefault())
                {
                    case TERMS_CASH_BOX:
                        if (_termsResults.Remainder.GetValueOrDefault() >= _termsResults.PrizeChargeShouldGet.GetValueOrDefault())
                        {
                            _termsResults.PrizeCharge = _termsResults.PrizeChargeShouldGet.GetValueOrDefault();
                            _termsResults.Remainder = _termsResults.Remainder.GetValueOrDefault() - _termsResults.PrizeChargeShouldGet.GetValueOrDefault();
                        }
                        else
                        {
                            _termsResults.PrizeCharge = _termsResults.Remainder.GetValueOrDefault();
                            _termsResults.Remainder = 0;
                        }
                        break;
                    case TERMS_SUPPLIER:
                        _termsResults.PrizeCharge = _termsResults.PrizeChargeShouldGet.GetValueOrDefault();
                        _termsResults.SupplierRent = _termsResults.SupplierRent.GetValueOrDefault() - _termsResults.PrizeChargeShouldGet.GetValueOrDefault();
                        break;
                    case TERMS_SITE:
                        _termsResults.PrizeCharge = _termsResults.PrizeChargeShouldGet.GetValueOrDefault();
                        _termsResults.SiteRent = _termsResults.SiteRent.GetValueOrDefault() - _termsResults.PrizeChargeShouldGet.GetValueOrDefault();
                        break;
                    case TERMS_SITE_GROUP:
                        _termsResults.PrizeCharge = _termsResults.PrizeChargeShouldGet.GetValueOrDefault();
                        _termsResults.SiteGroupRent = _termsResults.SiteGroupRent.GetValueOrDefault() - _termsResults.PrizeChargeShouldGet.GetValueOrDefault();
                        break;
                    case TERMS_SECONDARY_GROUP:
                        _termsResults.PrizeCharge = _termsResults.PrizeChargeShouldGet.GetValueOrDefault();
                        _termsResults.SecSiteGroupRent = _termsResults.SecSiteGroupRent.GetValueOrDefault() - _termsResults.PrizeChargeShouldGet.GetValueOrDefault();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void CalculatePostTermsOtherConsultancy()
        {
            try
            {
                LogManager.WriteLog("Inside CalculatePostTermsOtherConsultancy...", LogManager.enumLogLevel.Info);

                if (_termsInfo.OtherConsultancyFrequency == TERMS_FREQUENCY_PER_COLLECTION)
                    _termsResults.ConsultancyChargeShouldGet = _termsInfo.OtherConsultancyCharge.GetValueOrDefault();
                else //TERMS_FREQUENCY_PER_WEEK
                    _termsResults.ConsultancyChargeShouldGet = (_termsInfo.OtherConsultancyCharge.GetValueOrDefault() / 7) * _termsInfo.CollectionDays.GetValueOrDefault();

                switch (_termsInfo.OtherConsultancyPaidBy.GetValueOrDefault())
                {
                    case TERMS_CASH_BOX:
                        //if this is set to before the Output VAT, then
                        //   decrement the CashBoxVatable and the remainder
                        //if it is after, then
                        //   decrement the remainder
                        if (_termsInfo.OtherConsultancyVAT.GetValueOrDefault())
                            _termsResults.ConsultancyChargeShouldGet = Format2DPs(_termsResults.ConsultancyChargeShouldGet.GetValueOrDefault() * (1 + SystemVATRate));

                        if (_termsResults.Remainder.GetValueOrDefault() >= _termsResults.ConsultancyChargeShouldGet.GetValueOrDefault())
                        {
                            _termsResults.ConsultancyCharge = _termsResults.ConsultancyChargeShouldGet.GetValueOrDefault();
                            _termsResults.Remainder = _termsResults.Remainder.GetValueOrDefault() - _termsResults.ConsultancyChargeShouldGet.GetValueOrDefault();
                        }
                        else
                        {
                            _termsResults.ConsultancyCharge = _termsResults.Remainder.GetValueOrDefault();
                            _termsResults.Remainder = 0;
                        }
                        break;
                    case TERMS_SUPPLIER:
                        _termsResults.ConsultancyCharge = _termsResults.ConsultancyChargeShouldGet.GetValueOrDefault();
                        _termsResults.SupplierRent = _termsResults.SupplierRent.GetValueOrDefault() - _termsResults.ConsultancyChargeShouldGet.GetValueOrDefault();
                        break;
                    case TERMS_SITE:
                        _termsResults.ConsultancyCharge = _termsResults.ConsultancyChargeShouldGet.GetValueOrDefault();
                        _termsResults.SiteRent = _termsResults.SiteRent.GetValueOrDefault() - _termsResults.ConsultancyChargeShouldGet.GetValueOrDefault();
                        break;
                    case TERMS_SITE_GROUP:
                        _termsResults.ConsultancyCharge = _termsResults.ConsultancyChargeShouldGet.GetValueOrDefault();
                        _termsResults.SiteGroupRent = _termsResults.SiteGroupRent.GetValueOrDefault() - _termsResults.ConsultancyChargeShouldGet.GetValueOrDefault();
                        break;
                    case TERMS_SECONDARY_GROUP:
                        _termsResults.ConsultancyCharge = _termsResults.ConsultancyChargeShouldGet.GetValueOrDefault();
                        _termsResults.SecSiteGroupRent = _termsResults.SecSiteGroupRent.GetValueOrDefault() - _termsResults.ConsultancyChargeShouldGet.GetValueOrDefault();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void CalculatePostTermsOtherRoyalty()
        {
            try
            {
                LogManager.WriteLog("Inside CalculatePostTermsOtherRoyalty...", LogManager.enumLogLevel.Info);

                if (_termsInfo.OtherRoyaltyFrequency == TERMS_FREQUENCY_PER_COLLECTION)
                    _termsResults.RoyaltyChargeShouldGet = _termsInfo.OtherRoyaltyCharge.GetValueOrDefault();
                else //TERMS_FREQUENCY_PER_WEEK
                    _termsResults.RoyaltyChargeShouldGet = (_termsInfo.OtherRoyaltyCharge.GetValueOrDefault() / 7) * _termsInfo.CollectionDays.GetValueOrDefault();

                switch (_termsInfo.OtherRoyaltyPaidBy.GetValueOrDefault())
                {
                    case TERMS_CASH_BOX:
                        //if this is set to before the Output VAT, then
                        //   decrement the CashBoxVatable and the remainder
                        //if it is after, then
                        //   decrement the remainder
                        if (_termsInfo.OtherRoyaltyVAT.GetValueOrDefault())
                            _termsResults.RoyaltyChargeShouldGet = Format2DPs(_termsResults.RoyaltyChargeShouldGet.GetValueOrDefault() * (1 + SystemVATRate));

                        if (_termsResults.Remainder.GetValueOrDefault() >= _termsResults.RoyaltyChargeShouldGet.GetValueOrDefault())
                        {
                            _termsResults.RoyaltyCharge = _termsResults.RoyaltyChargeShouldGet.GetValueOrDefault();
                            _termsResults.Remainder = _termsResults.Remainder.GetValueOrDefault() - _termsResults.RoyaltyChargeShouldGet.GetValueOrDefault();
                        }
                        else
                        {
                            _termsResults.RoyaltyCharge = _termsResults.Remainder.GetValueOrDefault();
                            _termsResults.Remainder = 0;
                        }
                        break;
                    case TERMS_SUPPLIER:
                        _termsResults.RoyaltyCharge = _termsResults.RoyaltyChargeShouldGet.GetValueOrDefault();
                        _termsResults.SupplierRent = _termsResults.SupplierRent.GetValueOrDefault() - _termsResults.RoyaltyChargeShouldGet.GetValueOrDefault();
                        break;
                    case TERMS_SITE:
                        _termsResults.RoyaltyCharge = _termsResults.RoyaltyChargeShouldGet.GetValueOrDefault();
                        _termsResults.SiteRent = _termsResults.SiteRent.GetValueOrDefault() - _termsResults.RoyaltyChargeShouldGet.GetValueOrDefault();
                        break;
                    case TERMS_SITE_GROUP:
                        _termsResults.RoyaltyCharge = _termsResults.RoyaltyChargeShouldGet.GetValueOrDefault();
                        _termsResults.SiteGroupRent = _termsResults.SiteGroupRent.GetValueOrDefault() - _termsResults.RoyaltyChargeShouldGet.GetValueOrDefault();
                        break;
                    case TERMS_SECONDARY_GROUP:
                        _termsResults.RoyaltyCharge = _termsResults.RoyaltyChargeShouldGet.GetValueOrDefault();
                        _termsResults.SecSiteGroupRent = _termsResults.SecSiteGroupRent.GetValueOrDefault() - _termsResults.RoyaltyChargeShouldGet.GetValueOrDefault();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void CalculatePostTermsOtherOther1()
        {
            try
            {
                LogManager.WriteLog("Inside CalculatePostTermsOtherOther1...", LogManager.enumLogLevel.Info);

                if (_termsInfo.OtherOther1Frequency == TERMS_FREQUENCY_PER_COLLECTION)
                    _termsResults.Other1ChargeShouldGet = _termsInfo.OtherOther1Charge.GetValueOrDefault();
                else //TERMS_FREQUENCY_PER_WEEK
                    _termsResults.Other1ChargeShouldGet = (_termsInfo.OtherOther1Charge.GetValueOrDefault() / 7) * _termsInfo.CollectionDays.GetValueOrDefault();

                switch (_termsInfo.OtherOther1PaidBy.GetValueOrDefault())
                {
                    case TERMS_CASH_BOX:
                        //if this is set to before the Output VAT, then
                        //   decrement the CashBoxVatable and the remainder
                        //if it is after, then
                        //   decrement the remainder
                        if (_termsInfo.OtherOther1VAT.GetValueOrDefault())
                            _termsResults.Other1ChargeShouldGet = Format2DPs(_termsResults.Other1ChargeShouldGet.GetValueOrDefault() * (1 + SystemVATRate));

                        if (_termsResults.Remainder >= _termsResults.Other1ChargeShouldGet.GetValueOrDefault())
                        {
                            _termsResults.Other1Charge = _termsResults.Other1ChargeShouldGet.GetValueOrDefault();
                            _termsResults.Remainder = _termsResults.Remainder.GetValueOrDefault() - _termsResults.Other1ChargeShouldGet.GetValueOrDefault();
                        }
                        else
                        {
                            _termsResults.Other1Charge = _termsResults.Remainder.GetValueOrDefault();
                            _termsResults.Remainder = 0;
                        }
                        break;
                    case TERMS_SUPPLIER:
                        _termsResults.Other1Charge = _termsResults.Other1ChargeShouldGet.GetValueOrDefault();
                        _termsResults.SupplierRent = _termsResults.SupplierRent.GetValueOrDefault() - _termsResults.Other1ChargeShouldGet.GetValueOrDefault();
                        break;
                    case TERMS_SITE:
                        _termsResults.Other1Charge = _termsResults.Other1ChargeShouldGet.GetValueOrDefault();
                        _termsResults.SiteRent = _termsResults.SiteRent.GetValueOrDefault() - _termsResults.Other1ChargeShouldGet.GetValueOrDefault();
                        break;
                    case TERMS_SITE_GROUP:
                        _termsResults.Other1Charge = _termsResults.Other1ChargeShouldGet.GetValueOrDefault();
                        _termsResults.SiteGroupRent = _termsResults.SiteGroupRent.GetValueOrDefault() - _termsResults.Other1ChargeShouldGet.GetValueOrDefault();
                        break;
                    case TERMS_SECONDARY_GROUP:
                        _termsResults.Other1Charge = _termsResults.Other1ChargeShouldGet.GetValueOrDefault();
                        _termsResults.SecSiteGroupRent = _termsResults.SecSiteGroupRent.GetValueOrDefault() - _termsResults.Other1ChargeShouldGet.GetValueOrDefault();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void CalculatePostTermsOtherOther2()
        {
            try
            {
                LogManager.WriteLog("Inside CalculatePostTermsOtherOther2...", LogManager.enumLogLevel.Info);

                if (_termsInfo.OtherOther2Frequency == TERMS_FREQUENCY_PER_COLLECTION)
                    _termsResults.Other2ChargeShouldGet = _termsInfo.OtherOther2Charge.GetValueOrDefault();
                else //TERMS_FREQUENCY_PER_WEEK
                    _termsResults.Other2ChargeShouldGet = (_termsInfo.OtherOther2Charge.GetValueOrDefault() / 7) * _termsInfo.CollectionDays.GetValueOrDefault();

                switch (_termsInfo.OtherOther2PaidBy)
                {
                    case TERMS_CASH_BOX:
                        //if this is set to before the Output VAT, then
                        //   decrement the CashBoxVatable and the remainder
                        //if it is after, then
                        //   decrement the remainder
                        if (_termsInfo.OtherOther2VAT.GetValueOrDefault())
                            _termsResults.Other2ChargeShouldGet = Format2DPs(_termsResults.Other2ChargeShouldGet.GetValueOrDefault() * (1 + SystemVATRate));

                        if (_termsResults.Remainder.GetValueOrDefault() >= _termsResults.Other2ChargeShouldGet.GetValueOrDefault())
                        {
                            _termsResults.Other2Charge = _termsResults.Other2ChargeShouldGet.GetValueOrDefault();
                            _termsResults.Remainder = _termsResults.Remainder.GetValueOrDefault() - _termsResults.Other2ChargeShouldGet.GetValueOrDefault();
                        }
                        else
                        {
                            _termsResults.Other2Charge = _termsResults.Remainder.GetValueOrDefault();
                            _termsResults.Remainder = 0;
                        }
                        break;
                    case TERMS_SUPPLIER:
                        _termsResults.Other2Charge = _termsResults.Other2ChargeShouldGet.GetValueOrDefault();
                        _termsResults.SupplierRent = _termsResults.SupplierRent.GetValueOrDefault() - _termsResults.Other2ChargeShouldGet.GetValueOrDefault();
                        break;
                    case TERMS_SITE:
                        _termsResults.Other2Charge = _termsResults.Other2ChargeShouldGet.GetValueOrDefault();
                        _termsResults.SiteRent = _termsResults.SiteRent.GetValueOrDefault() - _termsResults.Other2ChargeShouldGet.GetValueOrDefault();
                        break;
                    case TERMS_SITE_GROUP:
                        _termsResults.Other2Charge = _termsResults.Other2ChargeShouldGet.GetValueOrDefault();
                        _termsResults.SiteGroupRent = _termsResults.SiteGroupRent.GetValueOrDefault() - _termsResults.Other2ChargeShouldGet.GetValueOrDefault();
                        break;
                    case TERMS_SECONDARY_GROUP:
                        _termsResults.Other2Charge = _termsResults.Other2ChargeShouldGet.GetValueOrDefault();
                        _termsResults.SecSiteGroupRent = _termsResults.SecSiteGroupRent.GetValueOrDefault() - _termsResults.Other2ChargeShouldGet.GetValueOrDefault();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void CalculatePostTermsSupplierShortfall()
        {
            try
            {
                LogManager.WriteLog("Inside CalculatePostTermsSupplierShortfall...", LogManager.enumLogLevel.Info);

                if (_termsInfo.SupplierValueGuaranteed.GetValueOrDefault() || _termsInfo.SupplierShareGuaranteed.GetValueOrDefault())
                {
                    //If it is primary & secondary, then they have to be in the correct order
                    //otherwise, do 1 to 15...
                    if ((_termsInfo.SiteUse.GetValueOrDefault() > 0 && (!(_termsInfo.SiteValueGuaranteed.GetValueOrDefault() || _termsInfo.SiteShareGuaranteed.GetValueOrDefault())) && (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PRIMARY_TO_ZERO || _termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER || _termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_SECONDARY_TO_ZERO)) ||
                        (_termsInfo.SupplierUse.GetValueOrDefault() > 0 && (!(_termsInfo.SupplierValueGuaranteed.GetValueOrDefault() || _termsInfo.SupplierShareGuaranteed.GetValueOrDefault())) && (_termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PRIMARY_TO_ZERO || _termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER || _termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_SECONDARY_TO_ZERO)) ||
                        (_termsInfo.GroupUse.GetValueOrDefault() > 0 && (!(_termsInfo.GroupValueGuaranteed.GetValueOrDefault() || _termsInfo.GroupShareGuaranteed.GetValueOrDefault())) && (_termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_PRIMARY_TO_ZERO || _termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER || _termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_SECONDARY_TO_ZERO)) ||
                        (_termsInfo.SecGroupUse.GetValueOrDefault() > 0 && (!(_termsInfo.SecGroupValueGuaranteed.GetValueOrDefault() || _termsInfo.SecGroupShareGuaranteed.GetValueOrDefault())) && (_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PRIMARY_TO_ZERO || _termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER || _termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_SECONDARY_TO_ZERO)))
                    {
                        //Find Primary
                        if (_termsInfo.SiteUse.GetValueOrDefault() > 0 && (!(_termsInfo.SiteValueGuaranteed.GetValueOrDefault() || _termsInfo.SiteShareGuaranteed.GetValueOrDefault())) && (_termsInfo.SiteGuarantor == TERMS_PRIMARY_FULL || _termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PRIMARY_TO_ZERO))
                            UseSiteForSupplierShortfall();
                        else if (_termsInfo.GroupUse.GetValueOrDefault() > 0 && (!(_termsInfo.GroupValueGuaranteed.GetValueOrDefault() || _termsInfo.GroupShareGuaranteed.GetValueOrDefault())) && (_termsInfo.GroupGuarantor == TERMS_PRIMARY_FULL || _termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_PRIMARY_TO_ZERO))
                            UseSiteGroupForSupplierShortfall();
                        else if (_termsInfo.SecGroupUse.GetValueOrDefault() > 0 && (!(_termsInfo.SecGroupValueGuaranteed.GetValueOrDefault() || _termsInfo.SecGroupShareGuaranteed.GetValueOrDefault())) && (_termsInfo.SecGroupGuarantor == TERMS_PRIMARY_FULL || _termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PRIMARY_TO_ZERO))
                            UseSecSiteGroupForSupplierShortfall();

                        //Find Secondary
                        if (_termsInfo.SiteUse.GetValueOrDefault() > 0 && (!(_termsInfo.SiteValueGuaranteed.GetValueOrDefault() || _termsInfo.SiteShareGuaranteed.GetValueOrDefault())) && (_termsInfo.SiteGuarantor == TERMS_SECONDARY_REMAINDER || _termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_SECONDARY_TO_ZERO))
                            UseSiteForSupplierShortfall();
                        else if (_termsInfo.GroupUse.GetValueOrDefault() > 0 && (!(_termsInfo.GroupValueGuaranteed.GetValueOrDefault() || _termsInfo.GroupShareGuaranteed.GetValueOrDefault())) && (_termsInfo.GroupGuarantor == TERMS_SECONDARY_REMAINDER || _termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_SECONDARY_TO_ZERO))
                            UseSiteGroupForSupplierShortfall();
                        else if (_termsInfo.SecGroupUse.GetValueOrDefault() > 0 && (!(_termsInfo.SecGroupValueGuaranteed.GetValueOrDefault() || _termsInfo.SecGroupShareGuaranteed.GetValueOrDefault())) && (_termsInfo.SecGroupGuarantor == TERMS_SECONDARY_REMAINDER || _termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_SECONDARY_TO_ZERO))
                            UseSecSiteGroupForSupplierShortfall();
                    }
                    else
                    {
                        //so the guarantors are worked out in order (don't think it makes much difference, but...)
                        for (int i = 1; i <= 15; i++)
                        {
                            if (_termsInfo.SiteUse.GetValueOrDefault() == i)
                            {
                                if (_termsInfo.SiteGuarantor != TERMS_NONE)
                                    UseSiteForSupplierShortfall();
                            }
                            else if (_termsInfo.GroupUse.GetValueOrDefault() == i)
                            {
                                if (_termsInfo.GroupGuarantor != TERMS_NONE)
                                    UseSiteGroupForSupplierShortfall();
                            }
                            else if (_termsInfo.SecGroupUse.GetValueOrDefault() == i)
                            {
                                if (_termsInfo.SecGroupGuarantor != TERMS_NONE)
                                    UseSecSiteGroupForSupplierShortfall();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void UseSiteForSupplierShortfall()
        {
            float? temp = 0;

            try
            {
                LogManager.WriteLog("Inside UseSiteForSupplierShortfall...", LogManager.enumLogLevel.Info);

                switch (_termsInfo.SiteGuarantor.GetValueOrDefault())
                {
                    case TERMS_PRIMARY_TO_ZERO:
                    case TERMS_PRIMARY_FULL:
                    case TERMS_SECONDARY_TO_ZERO:
                    case TERMS_SECONDARY_REMAINDER:
                    case TERMS_PERCENTAGE:
                        switch (_termsInfo.SupplierType.GetValueOrDefault())
                        {
                            case TERMS_RENT:
                            case TERMS_RENT_FIXED:
                            case TERMS_RENT_FULL:
                            case TERMS_RENT_ONLY:
                            case TERMS_RENT_SCHEDULE:
                                //if the rent is guaranteed
                                if ((_termsInfo.SupplierValueGuaranteed.GetValueOrDefault() && _termsInfo.SupplierType != TERMS_RENT_SCHEDULE) || (_termsInfo.SupplierShareGuaranteed.GetValueOrDefault() && _termsInfo.SupplierType == TERMS_RENT_SCHEDULE))
                                {
                                    //if the supplier needs a shortfall guarantor...
                                    if (_termsResults.SupplierRentShouldGet.GetValueOrDefault() - _termsResults.SupplierRent.GetValueOrDefault() > 0)
                                    {
                                        //take value off site shares
                                        if (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE)
                                        {
                                            //take a % of the shortfall from the site shares

                                            //find out if the site is the only percentage guarantor left
                                            if ((_termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.GroupUse.GetValueOrDefault() > _termsInfo.SiteUse)
                                                || (_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SecGroupUse.GetValueOrDefault() > _termsInfo.SiteUse))
                                            {
                                                temp = _termsInfo.SiteGuarantorPercentage.GetValueOrDefault() / 100;
                                                temp = temp * (_termsResults.SupplierRentShouldGet.GetValueOrDefault() - _termsResults.SupplierRent.GetValueOrDefault());
                                                _termsResults.SiteShare = _termsResults.SiteShare.GetValueOrDefault() - Format2DPs(temp.GetValueOrDefault());
                                                _termsResults.SupplierRent = _termsResults.SupplierRent.GetValueOrDefault() + Format2DPs(temp.GetValueOrDefault());
                                                temp = 0;
                                            }
                                            else //there is no other shortfall guarantor, therefore the remainder is to be paid
                                            {
                                                _termsResults.SiteShare = _termsResults.SiteShare.GetValueOrDefault() - (_termsResults.SupplierRentShouldGet.GetValueOrDefault() - _termsResults.SupplierRent.GetValueOrDefault());
                                                _termsResults.SupplierRent = _termsResults.SupplierRentShouldGet.GetValueOrDefault();
                                            }
                                        }
                                    }
                                    else if (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER || _termsInfo.SiteShare.GetValueOrDefault() > (_termsResults.SupplierRentShouldGet.GetValueOrDefault() - _termsResults.SupplierRent.GetValueOrDefault()))
                                    {
                                        _termsResults.SiteShare = _termsResults.SiteShare.GetValueOrDefault() - (_termsResults.SupplierRentShouldGet.GetValueOrDefault() - _termsResults.SupplierRent.GetValueOrDefault());
                                        _termsResults.SupplierRent = _termsResults.SupplierRentShouldGet.GetValueOrDefault();
                                    }
                                    else //to zero
                                    {
                                        _termsResults.SupplierRent = _termsResults.SupplierRent.GetValueOrDefault() + _termsResults.SiteShare.GetValueOrDefault();
                                        _termsResults.SiteShare = 0;
                                    }

                                    //if the supplier still needs a shortfall guarantor...
                                    if (_termsResults.SupplierRentShouldGet.GetValueOrDefault() - _termsResults.SupplierRent.GetValueOrDefault() > 0)
                                    {
                                        //take remaining value off site rent
                                        if (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //do nothing - already got the shortfall
                                        { }
                                        else if (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER)   //do nothing - already got the shortfall
                                        { }
                                        else if (_termsResults.SiteRent.GetValueOrDefault() > (_termsResults.SupplierRentShouldGet.GetValueOrDefault() - _termsResults.SupplierRent.GetValueOrDefault()))   //Has to be to zero
                                        {
                                            _termsResults.SiteRent = _termsResults.SiteRent.GetValueOrDefault() - (_termsResults.SupplierRentShouldGet.GetValueOrDefault() - _termsResults.SupplierRent.GetValueOrDefault());
                                            _termsResults.SupplierRent = _termsResults.SupplierRentShouldGet.GetValueOrDefault();
                                        }
                                        else //to zero
                                        {
                                            _termsResults.SupplierRent = _termsResults.SupplierRent.GetValueOrDefault() + _termsResults.SiteRent.GetValueOrDefault();
                                            _termsResults.SiteRent = 0;
                                        }
                                    }
                                }
                                break;
                            case TERMS_MIN:
                                if (_termsInfo.SupplierValueGuaranteed.GetValueOrDefault())
                                {
                                    if (_termsInfo.SupplierValue.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault() > 0) //if the supplier needs a shortfall guarantor for the min
                                    {
                                        if (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //if the site can cover the shortfall out of shares then
                                        {
                                            //take a % of shortfall from Site
                                            //find out if the site is the only percentage guarantor left
                                            if ((_termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.GroupUse.GetValueOrDefault() > _termsInfo.SiteUse.GetValueOrDefault())
                                                || (_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SecGroupUse.GetValueOrDefault() > _termsInfo.SiteUse.GetValueOrDefault()))
                                            {
                                                temp = _termsInfo.SiteGuarantorPercentage.GetValueOrDefault() / 100;
                                                temp = temp * (_termsInfo.SupplierValue.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault());
                                                _termsResults.SiteShare = _termsResults.SiteShare.GetValueOrDefault() - Format2DPs(temp.GetValueOrDefault());
                                                _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() + Format2DPs(temp.GetValueOrDefault());
                                                temp = 0;
                                            }
                                            else //there is no other shortfall guarantor, therefore the remainder is to be paid to bring up to the min
                                            {
                                                _termsResults.SiteShare = _termsResults.SiteShare.GetValueOrDefault() - (_termsInfo.SupplierValue.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault());
                                                _termsResults.SupplierShare = _termsInfo.SupplierValue.GetValueOrDefault();
                                            }
                                        }
                                        else if (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER ||
                                                    (_termsResults.SiteShare.GetValueOrDefault() > (_termsInfo.SupplierValue.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault())))
                                        {
                                            _termsResults.SiteShare = _termsResults.SiteShare.GetValueOrDefault() - (_termsInfo.SupplierValue.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault());
                                            _termsResults.SupplierShare = _termsInfo.SupplierValue.GetValueOrDefault();
                                        }
                                        else
                                        {
                                            //site shares cover min until they get to zero
                                            _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() + _termsResults.SiteShare.GetValueOrDefault();
                                            _termsResults.SiteShare = 0;
                                        }
                                    }

                                    //if there is still a shortfall on the minimum
                                    if (_termsInfo.SupplierValue.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault() > 0)
                                    {
                                        //now look at the site's rent
                                        if (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //shortfall is already taken care of
                                        { }
                                        else if (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER)  //do nothing - already got the shortfall
                                        { }
                                        else if (_termsResults.SiteRent.GetValueOrDefault() > (_termsInfo.SupplierValue.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault())) //to zero
                                        {
                                            _termsResults.SiteRent = _termsResults.SiteRent.GetValueOrDefault() - (_termsInfo.SupplierValue.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault());
                                            _termsResults.SupplierShare = _termsInfo.SupplierValue.GetValueOrDefault();
                                        }
                                        else //site shares cover min until they get to zero
                                        {
                                            _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() + _termsResults.SiteRent.GetValueOrDefault();
                                            _termsResults.SiteRent = 0;
                                        }
                                    }
                                }

                                //Now check if the share is guaranteed
                                if (_termsInfo.SupplierShareGuaranteed.GetValueOrDefault())
                                {
                                    if (_termsResults.SupplierShareShouldGet.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault() > 0) //if the supplier needs a shortfall guarantor for the min
                                    {
                                        if (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //if the site can cover the shortfall out of shares then
                                        {
                                            //take a % of shortfall from Site
                                            //find out if the site is the only percentage guarantor left
                                            if ((_termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.GroupUse.GetValueOrDefault() > _termsInfo.SiteUse.GetValueOrDefault())
                                                || (_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SecGroupUse.GetValueOrDefault() > _termsInfo.SiteUse.GetValueOrDefault()))
                                            {
                                                temp = _termsInfo.SiteGuarantorPercentage.GetValueOrDefault() / 100;
                                                temp = temp * (_termsResults.SupplierShareShouldGet.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault());
                                                _termsResults.SiteShare = _termsResults.SiteShare.GetValueOrDefault() - Format2DPs(temp.GetValueOrDefault());
                                                _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() + Format2DPs(temp.GetValueOrDefault());
                                                temp = 0;
                                            }
                                            else //there is no other shortfall guarantor, therefore the remainder is to be paid to bring up to the min
                                            {
                                                _termsResults.SiteShare = _termsResults.SiteShare.GetValueOrDefault() - (_termsResults.SupplierShareShouldGet.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault());
                                                _termsResults.SupplierShare = _termsResults.SupplierShareShouldGet.GetValueOrDefault();
                                            }
                                        }
                                        else if (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER ||
                                                    (_termsResults.SiteShare.GetValueOrDefault() > (_termsResults.SupplierShareShouldGet.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault())))
                                        {
                                            _termsResults.SiteShare = _termsResults.SiteShare.GetValueOrDefault() - (_termsResults.SupplierShareShouldGet.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault());
                                            _termsResults.SupplierShare = _termsResults.SupplierShareShouldGet.GetValueOrDefault();
                                        }
                                        else//site shares cover min until they get to zero
                                        {
                                            _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() + _termsResults.SiteShare.GetValueOrDefault();
                                            _termsResults.SiteShare = 0;
                                        }
                                    }

                                    //if there is still a shortfall on the minimum
                                    if (_termsResults.SupplierShareShouldGet.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault() > 0)
                                    {
                                        //now look at the site's rent
                                        if (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //shortfall is already taken care of
                                        { }
                                        else if (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SiteGuarantor == TERMS_SECONDARY_REMAINDER)  //do nothing - already got the shortfall
                                        { }
                                        else if (_termsResults.SiteRent.GetValueOrDefault() > (_termsResults.SupplierShareShouldGet.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault())) //to zero
                                        {
                                            _termsResults.SiteRent = _termsResults.SiteRent.GetValueOrDefault() - (_termsResults.SupplierShareShouldGet.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault());
                                            _termsResults.SupplierShare = _termsResults.SupplierShareShouldGet.GetValueOrDefault();
                                        }
                                        else //site shares cover min until they get to zero
                                        {
                                            _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() + _termsResults.SiteRent.GetValueOrDefault();
                                            _termsResults.SiteRent = 0;
                                        }
                                    }
                                }
                                break;
                            case TERMS_MAX:
                                //the max value can't be guaranteed
                                //so we only have to guarantee the shares
                                if (_termsInfo.SupplierShareGuaranteed.GetValueOrDefault())
                                {
                                    if (_termsResults.SupplierShareShouldGet.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault() > 0) //if the supplier needs a shortfall guarantor for the share
                                    {
                                        if (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //if the site can cover the shortfall out of shares then
                                        {
                                            //take a % of shortfall from Site
                                            //find out if the site is the only percentage guarantor left
                                            if ((_termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.GroupUse.GetValueOrDefault() > _termsInfo.SiteUse.GetValueOrDefault())
                                                || (_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SecGroupUse.GetValueOrDefault() > _termsInfo.SiteUse.GetValueOrDefault()))
                                            {
                                                temp = _termsInfo.SiteGuarantorPercentage.GetValueOrDefault() / 100;
                                                temp = temp * (_termsResults.SupplierShareShouldGet.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault());
                                                _termsResults.SiteShare = _termsResults.SiteShare.GetValueOrDefault() - Format2DPs(temp.GetValueOrDefault());
                                                _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() + Format2DPs(temp.GetValueOrDefault());
                                                temp = 0;
                                            }
                                            else //there is no other shortfall guarantor, therefore the remainder is to be paid to bring up to the min
                                            {
                                                _termsResults.SiteShare = _termsResults.SiteShare.GetValueOrDefault() - (_termsResults.SupplierShareShouldGet.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault());
                                                _termsResults.SupplierShare = _termsResults.SupplierShareShouldGet.GetValueOrDefault();
                                            }
                                        }
                                        else if (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER ||
                                                    (_termsResults.SiteShare.GetValueOrDefault() > (_termsResults.SupplierShareShouldGet.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault())))
                                        {
                                            _termsResults.SiteShare = _termsResults.SiteShare.GetValueOrDefault() - (_termsResults.SupplierShareShouldGet.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault());
                                            _termsResults.SupplierShare = _termsResults.SupplierShareShouldGet.GetValueOrDefault();
                                        }
                                        else
                                        {
                                            //site shares cover min until they get to zero
                                            _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() + _termsResults.SiteShare.GetValueOrDefault();
                                            _termsResults.SiteShare = 0;
                                        }
                                    }

                                    //if there is still a shortfall on the minimum
                                    if (_termsResults.SupplierShareShouldGet.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault() > 0)
                                    {
                                        //now look at the site's rent
                                        if (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //shortfall is already taken care of
                                        { }
                                        else if (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER)  //do nothing - already got the shortfall
                                        { }
                                        else if (_termsResults.SiteRent.GetValueOrDefault() > (_termsResults.SupplierShareShouldGet.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault())) //to zero
                                        {
                                            _termsResults.SiteRent = _termsResults.SiteRent.GetValueOrDefault() - (_termsResults.SupplierShareShouldGet.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault());
                                            _termsResults.SupplierShare = _termsResults.SupplierShareShouldGet.GetValueOrDefault();
                                        }
                                        else //site shares cover min until they get to zero
                                        {
                                            _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() + _termsResults.SiteRent.GetValueOrDefault();
                                            _termsResults.SiteRent = 0;
                                        }
                                    }
                                }
                                break;
                            case TERMS_FRONT_MONEY:
                                if (_termsInfo.SupplierValueGuaranteed.GetValueOrDefault())
                                {
                                    if (_termsResults.SupplierRentShouldGet.GetValueOrDefault() - _termsResults.SupplierRent.GetValueOrDefault() > 0) //if the supplier needs a shortfall guarantor for the rent
                                    {
                                        if (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //if the site can cover the shortfall out of shares then
                                        {
                                            //take a % of shortfall from Site
                                            //find out if the site is the only percentage guarantor left
                                            if ((_termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.GroupUse.GetValueOrDefault() > _termsInfo.SiteUse)
                                                || (_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SecGroupUse.GetValueOrDefault() > _termsInfo.SiteUse))
                                            {
                                                temp = _termsInfo.SiteGuarantorPercentage.GetValueOrDefault() / 100;
                                                temp = temp * (_termsResults.SupplierRentShouldGet.GetValueOrDefault() - _termsResults.SupplierRent.GetValueOrDefault());
                                                _termsResults.SiteShare = _termsResults.SiteShare.GetValueOrDefault() - Format2DPs(temp.GetValueOrDefault());
                                                _termsResults.SupplierRent = _termsResults.SupplierRent.GetValueOrDefault() + Format2DPs(temp.GetValueOrDefault());
                                                temp = 0;
                                            }
                                            else //there is no other shortfall guarantor, therefore the remainder is to be paid to bring up to the min
                                            {
                                                _termsResults.SiteShare = _termsResults.SiteShare.GetValueOrDefault() - (_termsResults.SupplierRentShouldGet.GetValueOrDefault() - _termsResults.SupplierRent.GetValueOrDefault());
                                                _termsResults.SupplierRent = _termsResults.SupplierRentShouldGet.GetValueOrDefault();
                                            }
                                        }
                                        else if (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER ||
                                                    (_termsResults.SiteShare.GetValueOrDefault() > (_termsResults.SupplierShareShouldGet.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault())))
                                        {
                                            _termsResults.SiteShare = _termsResults.SiteShare.GetValueOrDefault() - (_termsResults.SupplierShareShouldGet.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault());
                                            _termsResults.SupplierRent = _termsResults.SupplierRentShouldGet.GetValueOrDefault();
                                        }
                                        else
                                        {
                                            //site shares cover min until they get to zero
                                            _termsResults.SupplierRent = _termsResults.SupplierRent.GetValueOrDefault() + _termsResults.SiteShare.GetValueOrDefault();
                                            _termsResults.SiteShare = 0;
                                        }
                                    }

                                    //if there is still a shortfall on the minimum
                                    if (_termsResults.SupplierRentShouldGet.GetValueOrDefault() - _termsResults.SupplierRent.GetValueOrDefault() > 0)
                                    {
                                        //now look at the site's rent
                                        if (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //shortfall is already taken care of
                                        { }
                                        else if (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER)  //do nothing - already got the shortfall
                                        { }
                                        else if (_termsResults.SiteRent.GetValueOrDefault() > (_termsResults.SupplierRentShouldGet.GetValueOrDefault() - _termsResults.SupplierRent.GetValueOrDefault())) //to zero
                                        {
                                            _termsResults.SiteRent = _termsResults.SiteRent.GetValueOrDefault() - (_termsResults.SupplierRentShouldGet.GetValueOrDefault() - _termsResults.SupplierRent.GetValueOrDefault());
                                            _termsResults.SupplierRent = _termsResults.SupplierRentShouldGet.GetValueOrDefault();
                                        }
                                        else //site shares cover min until they get to zero
                                        {
                                            _termsResults.SupplierRent = _termsResults.SupplierRent.GetValueOrDefault() + _termsResults.SiteRent.GetValueOrDefault();
                                            _termsResults.SiteRent = 0;
                                        }
                                    }
                                }

                                //Now check if the share is guaranteed
                                if (_termsInfo.SupplierShareGuaranteed.GetValueOrDefault())
                                {
                                    if (_termsResults.SupplierShareShouldGet.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault() > 0) //if the supplier needs a shortfall guarantor for the min
                                    {
                                        if (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //if the site can cover the shortfall out of shares then
                                        {
                                            //take a % of shortfall from Site
                                            //find out if the site is the only percentage guarantor left
                                            if ((_termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.GroupUse.GetValueOrDefault() > _termsInfo.SiteUse.GetValueOrDefault())
                                                || (_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SecGroupUse.GetValueOrDefault() > _termsInfo.SiteUse.GetValueOrDefault()))
                                            {
                                                temp = _termsInfo.SiteGuarantorPercentage.GetValueOrDefault() / 100;
                                                temp = temp * (_termsResults.SupplierShareShouldGet.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault());
                                                _termsResults.SiteShare = _termsResults.SiteShare.GetValueOrDefault() - Format2DPs(temp.GetValueOrDefault());
                                                _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() + Format2DPs(temp.GetValueOrDefault());
                                                temp = 0;
                                            }
                                            else //there is no other shortfall guarantor, therefore the remainder is to be paid to bring up to the min
                                            {
                                                _termsResults.SiteShare = _termsResults.SiteShare.GetValueOrDefault() - (_termsResults.SupplierShareShouldGet.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault());
                                                _termsResults.SupplierShare = _termsResults.SupplierShareShouldGet.GetValueOrDefault();
                                            }
                                        }
                                        else if (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER ||
                                                    (_termsResults.SiteShare.GetValueOrDefault() > (_termsResults.SupplierShareShouldGet.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault())))
                                        {
                                            _termsResults.SiteShare = _termsResults.SiteShare.GetValueOrDefault() - (_termsResults.SupplierShareShouldGet.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault());
                                            _termsResults.SupplierShare = _termsResults.SupplierShareShouldGet.GetValueOrDefault();
                                        }
                                        else
                                        {
                                            //site shares cover min until they get to zero
                                            _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() + _termsResults.SiteShare.GetValueOrDefault();
                                            _termsResults.SiteShare = 0;
                                        }
                                    }

                                    //if there is still a shortfall on the minimum
                                    //take it out of the site's rent
                                    if (_termsResults.SupplierShareShouldGet.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault() > 0)
                                    {
                                        //now look at the site's rent
                                        if (_termsInfo.SiteGuarantor == TERMS_PERCENTAGE) //shortfall is already taken care of
                                        { }
                                        else if (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER)  //do nothing - already got the shortfall
                                        { }
                                        else if (_termsResults.SiteRent.GetValueOrDefault() > (_termsResults.SupplierShareShouldGet.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault())) //to zero
                                        {
                                            _termsResults.SiteRent = _termsResults.SiteRent.GetValueOrDefault() - (_termsResults.SupplierShareShouldGet.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault());
                                            _termsResults.SupplierShare = _termsResults.SupplierShareShouldGet.GetValueOrDefault();
                                        }
                                        else //site shares cover min until they get to zero
                                        {
                                            _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() + _termsResults.SiteRent.GetValueOrDefault();
                                            _termsResults.SiteRent = 0;
                                        }
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void UseSiteGroupForSupplierShortfall()
        {
            float? temp = 0;

            try
            {
                LogManager.WriteLog("Inside UseSiteGroupForSupplierShortfall...", LogManager.enumLogLevel.Info);

                switch (_termsInfo.GroupGuarantor.GetValueOrDefault())
                {
                    case TERMS_PRIMARY_TO_ZERO:
                    case TERMS_PRIMARY_FULL:
                    case TERMS_SECONDARY_TO_ZERO:
                    case TERMS_SECONDARY_REMAINDER:
                    case TERMS_PERCENTAGE:
                        switch (_termsInfo.SupplierType.GetValueOrDefault())
                        {
                            case TERMS_RENT:
                            case TERMS_RENT_FIXED:
                            case TERMS_RENT_FULL:
                            case TERMS_RENT_ONLY:
                            case TERMS_RENT_SCHEDULE:
                                //if the rent is guaranteed
                                if ((_termsInfo.SupplierValueGuaranteed.GetValueOrDefault() && _termsInfo.SupplierType != TERMS_RENT_SCHEDULE) || (_termsInfo.SupplierShareGuaranteed.GetValueOrDefault() && _termsInfo.SupplierType == TERMS_RENT_SCHEDULE))
                                {
                                    //if the supplier needs a shortfall guarantor...
                                    if (_termsResults.SupplierRentShouldGet.GetValueOrDefault() - _termsResults.SupplierRent.GetValueOrDefault() > 0)
                                    {
                                        //take value off site shares
                                        if (_termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE)
                                        {
                                            //take a % of the shortfall from the site shares

                                            //find out if the site is the only percentage guarantor left
                                            if ((_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SiteUse.GetValueOrDefault() > _termsInfo.GroupUse)
                                                || (_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SecGroupUse.GetValueOrDefault() > _termsInfo.GroupUse))
                                            {
                                                temp = _termsInfo.GroupGuarantorPercentage.GetValueOrDefault() / 100;
                                                temp = temp * (_termsResults.SupplierRentShouldGet.GetValueOrDefault() - _termsResults.SupplierRent.GetValueOrDefault());
                                                _termsResults.SiteGroupShare = _termsResults.SiteGroupShare.GetValueOrDefault() - Format2DPs(temp.GetValueOrDefault());
                                                _termsResults.SupplierRent = _termsResults.SupplierRent.GetValueOrDefault() + Format2DPs(temp.GetValueOrDefault());
                                                temp = 0;
                                            }
                                            else //there is no other shortfall guarantor, therefore the remainder is to be paid
                                            {
                                                _termsResults.SiteGroupShare = _termsResults.SiteGroupShare.GetValueOrDefault() - (_termsResults.SupplierRentShouldGet.GetValueOrDefault() - _termsResults.SupplierRent.GetValueOrDefault());
                                                _termsResults.SupplierRent = _termsResults.SupplierRentShouldGet.GetValueOrDefault();
                                            }
                                        }
                                    }
                                    else if (_termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER || _termsResults.SiteGroupShare.GetValueOrDefault() > (_termsResults.SupplierRentShouldGet.GetValueOrDefault() - _termsResults.SupplierRent.GetValueOrDefault()))
                                    {
                                        _termsResults.SiteGroupShare = _termsResults.SiteGroupShare.GetValueOrDefault() - (_termsResults.SupplierRentShouldGet.GetValueOrDefault() - _termsResults.SupplierRent.GetValueOrDefault());
                                        _termsResults.SupplierRent = _termsResults.SupplierRentShouldGet.GetValueOrDefault();
                                    }
                                    else //to zero
                                    {
                                        _termsResults.SupplierRent = _termsResults.SupplierRent.GetValueOrDefault() + _termsResults.SiteGroupShare.GetValueOrDefault();
                                        _termsResults.SiteGroupShare = 0;
                                    }

                                    //if the supplier still needs a shortfall guarantor...
                                    if (_termsResults.SupplierRentShouldGet.GetValueOrDefault() - _termsResults.SupplierRent.GetValueOrDefault() > 0)
                                    {
                                        //take remaining value off site rent
                                        if (_termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //do nothing - already got the shortfall
                                        { }
                                        else if (_termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER)   //do nothing - already got the shortfall
                                        { }
                                        else if (_termsResults.SiteGroupRent.GetValueOrDefault() > (_termsResults.SupplierRentShouldGet.GetValueOrDefault() - _termsResults.SupplierRent.GetValueOrDefault()))   //Has to be to zero
                                        {
                                            _termsResults.SiteGroupRent = _termsResults.SiteGroupRent.GetValueOrDefault() - (_termsResults.SupplierRentShouldGet.GetValueOrDefault() - _termsResults.SupplierRent.GetValueOrDefault());
                                            _termsResults.SupplierRent = _termsResults.SupplierRentShouldGet.GetValueOrDefault();
                                        }
                                        else //to zero
                                        {
                                            _termsResults.SupplierRent = _termsResults.SupplierRent.GetValueOrDefault() + _termsResults.SiteGroupRent.GetValueOrDefault();
                                            _termsResults.SiteGroupRent = 0;
                                        }
                                    }
                                }
                                break;
                            case TERMS_MIN:
                                if (_termsInfo.SupplierValueGuaranteed.GetValueOrDefault())
                                {
                                    if (_termsInfo.SupplierValue.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault() > 0) //if the supplier needs a shortfall guarantor for the min
                                    {
                                        if (_termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //if the site can cover the shortfall out of shares then
                                        {
                                            //take a % of shortfall from Site
                                            //find out if the site is the only percentage guarantor left
                                            if ((_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SiteUse.GetValueOrDefault() > _termsInfo.GroupUse.GetValueOrDefault())
                                                || (_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SecGroupUse.GetValueOrDefault() > _termsInfo.GroupUse.GetValueOrDefault()))
                                            {
                                                temp = _termsInfo.GroupGuarantorPercentage.GetValueOrDefault() / 100;
                                                temp = temp * (_termsInfo.SupplierValue.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault());
                                                _termsResults.SiteGroupShare = _termsResults.SiteGroupShare.GetValueOrDefault() - Format2DPs(temp.GetValueOrDefault());
                                                _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() + Format2DPs(temp.GetValueOrDefault());
                                                temp = 0;
                                            }
                                            else //there is no other shortfall guarantor, therefore the remainder is to be paid to bring up to the min
                                            {
                                                _termsResults.SiteGroupShare = _termsResults.SiteGroupShare.GetValueOrDefault() - (_termsInfo.SupplierValue.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault());
                                                _termsResults.SupplierShare = _termsInfo.SupplierValue.GetValueOrDefault();
                                            }
                                        }
                                        else if (_termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER ||
                                                    (_termsResults.SiteGroupShare.GetValueOrDefault() > (_termsInfo.SupplierValue.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault())))
                                        {
                                            _termsResults.SiteGroupShare = _termsResults.SiteGroupShare.GetValueOrDefault() - (_termsInfo.SupplierValue.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault());
                                            _termsResults.SupplierShare = _termsInfo.SupplierValue.GetValueOrDefault();
                                        }
                                        else
                                        {
                                            //site shares cover min until they get to zero
                                            _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() + _termsResults.SiteGroupShare.GetValueOrDefault();
                                            _termsResults.SiteGroupShare = 0;
                                        }
                                    }

                                    //if there is still a shortfall on the minimum
                                    if (_termsInfo.SupplierValue.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault() > 0)
                                    {
                                        //now look at the site's rent
                                        if (_termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //shortfall is already taken care of
                                        { }
                                        else if (_termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER)  //do nothing - already got the shortfall
                                        { }
                                        else if (_termsResults.SiteGroupRent.GetValueOrDefault() > (_termsInfo.SupplierValue.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault())) //to zero
                                        {
                                            _termsResults.SiteGroupRent = _termsResults.SiteGroupRent.GetValueOrDefault() - (_termsInfo.SupplierValue.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault());
                                            _termsResults.SupplierShare = _termsInfo.SupplierValue.GetValueOrDefault();
                                        }
                                        else //site shares cover min until they get to zero
                                        {
                                            _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() + _termsResults.SiteGroupRent.GetValueOrDefault();
                                            _termsResults.SiteGroupRent = 0;
                                        }
                                    }
                                }

                                //Now check if the share is guaranteed
                                if (_termsInfo.SupplierShareGuaranteed.GetValueOrDefault())
                                {
                                    if (_termsResults.SupplierShareShouldGet.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault() > 0) //if the supplier needs a shortfall guarantor for the min
                                    {
                                        if (_termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //if the site can cover the shortfall out of shares then
                                        {
                                            //take a % of shortfall from Site
                                            //find out if the site is the only percentage guarantor left
                                            if ((_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SiteUse.GetValueOrDefault() > _termsInfo.GroupUse.GetValueOrDefault())
                                                || (_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SecGroupUse.GetValueOrDefault() > _termsInfo.GroupUse.GetValueOrDefault()))
                                            {
                                                temp = _termsInfo.GroupGuarantorPercentage.GetValueOrDefault() / 100;
                                                temp = temp * (_termsResults.SupplierShareShouldGet.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault());
                                                _termsResults.SiteGroupShare = _termsResults.SiteGroupShare.GetValueOrDefault() - Format2DPs(temp.GetValueOrDefault());
                                                _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() + Format2DPs(temp.GetValueOrDefault());
                                                temp = 0;
                                            }
                                            else //there is no other shortfall guarantor, therefore the remainder is to be paid to bring up to the min
                                            {
                                                _termsResults.SiteGroupShare = _termsResults.SiteGroupShare.GetValueOrDefault() - (_termsResults.SupplierShareShouldGet.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault());
                                                _termsResults.SupplierShare = _termsResults.SupplierShareShouldGet.GetValueOrDefault();
                                            }
                                        }
                                        else if (_termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER ||
                                                    (_termsResults.SiteGroupShare.GetValueOrDefault() > (_termsResults.SupplierShareShouldGet.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault())))
                                        {
                                            _termsResults.SiteGroupShare = _termsResults.SiteGroupShare.GetValueOrDefault() - (_termsResults.SupplierShareShouldGet.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault());
                                            _termsResults.SupplierShare = _termsResults.SupplierShareShouldGet.GetValueOrDefault();
                                        }
                                        else//site shares cover min until they get to zero
                                        {
                                            _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() + _termsResults.SiteGroupShare.GetValueOrDefault();
                                            _termsResults.SiteGroupShare = 0;
                                        }
                                    }

                                    //if there is still a shortfall on the minimum
                                    if (_termsResults.SupplierShareShouldGet.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault() > 0)
                                    {
                                        //now look at the site's rent
                                        if (_termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //shortfall is already taken care of
                                        { }
                                        else if (_termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER)  //do nothing - already got the shortfall
                                        { }
                                        else if (_termsResults.SiteGroupRent.GetValueOrDefault() > (_termsResults.SupplierShareShouldGet.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault())) //to zero
                                        {
                                            _termsResults.SiteRent = _termsResults.SiteGroupRent.GetValueOrDefault() - (_termsResults.SupplierShareShouldGet.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault());
                                            _termsResults.SupplierShare = _termsResults.SupplierShareShouldGet.GetValueOrDefault();
                                        }
                                        else //site shares cover min until they get to zero
                                        {
                                            _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() + _termsResults.SiteGroupRent.GetValueOrDefault();
                                            _termsResults.SiteGroupRent = 0;
                                        }
                                    }
                                }
                                break;
                            case TERMS_MAX:
                                //the max value can't be guaranteed
                                //so we only have to guarantee the shares
                                if (_termsInfo.SupplierShareGuaranteed.GetValueOrDefault())
                                {
                                    if (_termsResults.SupplierShareShouldGet.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault() > 0) //if the supplier needs a shortfall guarantor for the share
                                    {
                                        if (_termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //if the site can cover the shortfall out of shares then
                                        {
                                            //take a % of shortfall from Site
                                            //find out if the site is the only percentage guarantor left
                                            if ((_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SiteUse.GetValueOrDefault() > _termsInfo.GroupUse.GetValueOrDefault())
                                                || (_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SecGroupUse.GetValueOrDefault() > _termsInfo.GroupUse.GetValueOrDefault()))
                                            {
                                                temp = _termsInfo.GroupGuarantorPercentage.GetValueOrDefault() / 100;
                                                temp = temp * (_termsResults.SupplierShareShouldGet.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault());
                                                _termsResults.SiteGroupShare = _termsResults.SiteGroupShare.GetValueOrDefault() - Format2DPs(temp.GetValueOrDefault());
                                                _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() + Format2DPs(temp.GetValueOrDefault());
                                                temp = 0;
                                            }
                                            else //there is no other shortfall guarantor, therefore the remainder is to be paid to bring up to the min
                                            {
                                                _termsResults.SiteGroupShare = _termsResults.SiteGroupShare.GetValueOrDefault() - (_termsResults.SupplierShareShouldGet.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault());
                                                _termsResults.SupplierShare = _termsResults.SupplierShareShouldGet.GetValueOrDefault();
                                            }
                                        }
                                        else if (_termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER ||
                                                    (_termsResults.SiteGroupShare.GetValueOrDefault() > (_termsResults.SupplierShareShouldGet.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault())))
                                        {
                                            _termsResults.SiteGroupShare = _termsResults.SiteGroupShare.GetValueOrDefault() - (_termsResults.SupplierShareShouldGet.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault());
                                            _termsResults.SupplierShare = _termsResults.SupplierShareShouldGet.GetValueOrDefault();
                                        }
                                        else
                                        {
                                            //site shares cover min until they get to zero
                                            _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() + _termsResults.SiteGroupShare.GetValueOrDefault();
                                            _termsResults.SiteGroupShare = 0;
                                        }
                                    }

                                    //if there is still a shortfall on the minimum
                                    if (_termsResults.SupplierShareShouldGet.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault() > 0)
                                    {
                                        //now look at the site's rent
                                        if (_termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //shortfall is already taken care of
                                        { }
                                        else if (_termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER)  //do nothing - already got the shortfall
                                        { }
                                        else if (_termsResults.SiteGroupRent.GetValueOrDefault() > (_termsResults.SupplierShareShouldGet.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault())) //to zero
                                        {
                                            _termsResults.SiteGroupRent = _termsResults.SiteGroupRent.GetValueOrDefault() - (_termsResults.SupplierShareShouldGet.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault());
                                            _termsResults.SupplierShare = _termsResults.SupplierShareShouldGet.GetValueOrDefault();
                                        }
                                        else //site shares cover min until they get to zero
                                        {
                                            _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() + _termsResults.SiteGroupRent.GetValueOrDefault();
                                            _termsResults.SiteGroupRent = 0;
                                        }
                                    }
                                }
                                break;
                            case TERMS_FRONT_MONEY:
                                if (_termsInfo.SupplierValueGuaranteed.GetValueOrDefault())
                                {
                                    if (_termsResults.SupplierRentShouldGet.GetValueOrDefault() - _termsResults.SupplierRent.GetValueOrDefault() > 0) //if the supplier needs a shortfall guarantor for the rent
                                    {
                                        if (_termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //if the site can cover the shortfall out of shares then
                                        {
                                            //take a % of shortfall from Site
                                            //find out if the site is the only percentage guarantor left
                                            if ((_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SiteUse.GetValueOrDefault() > _termsInfo.GroupUse.GetValueOrDefault())
                                                || (_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SecGroupUse.GetValueOrDefault() > _termsInfo.GroupUse.GetValueOrDefault()))
                                            {
                                                temp = _termsInfo.GroupGuarantorPercentage.GetValueOrDefault() / 100;
                                                temp = temp * (_termsResults.SupplierRentShouldGet.GetValueOrDefault() - _termsResults.SupplierRent.GetValueOrDefault());
                                                _termsResults.SiteGroupShare = _termsResults.SiteGroupShare.GetValueOrDefault() - Format2DPs(temp.GetValueOrDefault());
                                                _termsResults.SupplierRent = _termsResults.SupplierRent.GetValueOrDefault() + Format2DPs(temp.GetValueOrDefault());
                                                temp = 0;
                                            }
                                            else //there is no other shortfall guarantor, therefore the remainder is to be paid to bring up to the min
                                            {
                                                _termsResults.SiteGroupShare = _termsResults.SiteGroupShare.GetValueOrDefault() - (_termsResults.SupplierRentShouldGet.GetValueOrDefault() - _termsResults.SupplierRent.GetValueOrDefault());
                                                _termsResults.SupplierRent = _termsResults.SupplierRentShouldGet.GetValueOrDefault();
                                            }
                                        }
                                        else if (_termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER ||
                                                    (_termsResults.SiteGroupShare.GetValueOrDefault() > (_termsResults.SupplierRentShouldGet.GetValueOrDefault() - _termsResults.SupplierRent.GetValueOrDefault())))
                                        {
                                            _termsResults.SiteGroupShare = _termsResults.SiteGroupShare.GetValueOrDefault() - (_termsResults.SupplierRentShouldGet.GetValueOrDefault() - _termsResults.SupplierRent.GetValueOrDefault());
                                            _termsResults.SupplierRent = _termsResults.SupplierRentShouldGet.GetValueOrDefault();
                                        }
                                        else
                                        {
                                            //site shares cover min until they get to zero
                                            _termsResults.SupplierRent = _termsResults.SupplierRent.GetValueOrDefault() + _termsResults.SiteGroupShare.GetValueOrDefault();
                                            _termsResults.SiteGroupShare = 0;
                                        }
                                    }

                                    //if there is still a shortfall on the minimum
                                    if (_termsResults.SupplierRentShouldGet.GetValueOrDefault() - _termsResults.SupplierRent.GetValueOrDefault() > 0)
                                    {
                                        //now look at the site's rent
                                        if (_termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //shortfall is already taken care of
                                        { }
                                        else if (_termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER)  //do nothing - already got the shortfall
                                        { }
                                        else if (_termsResults.SiteGroupRent.GetValueOrDefault() > (_termsResults.SupplierRentShouldGet.GetValueOrDefault() - _termsResults.SupplierRent.GetValueOrDefault())) //to zero
                                        {
                                            _termsResults.SiteGroupRent = _termsResults.SiteGroupRent.GetValueOrDefault() - (_termsResults.SupplierRentShouldGet.GetValueOrDefault() - _termsResults.SupplierRent.GetValueOrDefault());
                                            _termsResults.SupplierRent = _termsResults.SupplierRentShouldGet.GetValueOrDefault();
                                        }
                                        else //site shares cover min until they get to zero
                                        {
                                            _termsResults.SupplierRent = _termsResults.SupplierRent.GetValueOrDefault() + _termsResults.SiteGroupRent.GetValueOrDefault();
                                            _termsResults.SiteGroupRent = 0;
                                        }
                                    }
                                }

                                //Now check if the share is guaranteed
                                if (_termsInfo.SupplierShareGuaranteed.GetValueOrDefault())
                                {
                                    if (_termsResults.SupplierShareShouldGet.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault() > 0) //if the supplier needs a shortfall guarantor for the min
                                    {
                                        if (_termsInfo.GroupGuarantor == TERMS_PERCENTAGE) //if the site can cover the shortfall out of shares then
                                        {
                                            //take a % of shortfall from Site
                                            //find out if the site is the only percentage guarantor left
                                            if ((_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SiteUse.GetValueOrDefault() > _termsInfo.GroupUse.GetValueOrDefault())
                                                || (_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SecGroupUse.GetValueOrDefault() > _termsInfo.GroupUse.GetValueOrDefault()))
                                            {
                                                temp = _termsInfo.GroupGuarantorPercentage.GetValueOrDefault() / 100;
                                                temp = temp * (_termsResults.SupplierShareShouldGet.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault());
                                                _termsResults.SiteGroupShare = _termsResults.SiteGroupShare.GetValueOrDefault() - Format2DPs(temp.GetValueOrDefault());
                                                _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() + Format2DPs(temp.GetValueOrDefault());
                                                temp = 0;
                                            }
                                            else //there is no other shortfall guarantor, therefore the remainder is to be paid to bring up to the min
                                            {
                                                _termsResults.SiteGroupShare = _termsResults.SiteGroupShare.GetValueOrDefault() - (_termsResults.SupplierShareShouldGet.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault());
                                                _termsResults.SupplierShare = _termsResults.SupplierShareShouldGet.GetValueOrDefault();
                                            }
                                        }
                                        else if (_termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER ||
                                                    (_termsResults.SiteGroupShare.GetValueOrDefault() > (_termsResults.SupplierShareShouldGet.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault())))
                                        {
                                            _termsResults.SiteGroupShare = _termsResults.SiteGroupShare.GetValueOrDefault() - (_termsResults.SupplierShareShouldGet.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault());
                                            _termsResults.SupplierShare = _termsResults.SupplierShareShouldGet.GetValueOrDefault();
                                        }
                                        else
                                        {
                                            //site shares cover min until they get to zero
                                            _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() + _termsResults.SiteGroupShare.GetValueOrDefault();
                                            _termsResults.SiteGroupShare = 0;
                                        }
                                    }

                                    //if there is still a shortfall on the minimum
                                    //take it out of the site's rent
                                    if (_termsResults.SupplierShareShouldGet.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault() > 0)
                                    {
                                        //now look at the site's rent
                                        if (_termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //shortfall is already taken care of
                                        { }
                                        else if (_termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER)  //do nothing - already got the shortfall
                                        { }
                                        else if (_termsResults.SiteGroupRent.GetValueOrDefault() > (_termsResults.SupplierShareShouldGet.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault())) //to zero
                                        {
                                            _termsResults.SiteGroupRent = _termsResults.SiteGroupRent.GetValueOrDefault() - (_termsResults.SupplierShareShouldGet.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault());
                                            _termsResults.SupplierShare = _termsResults.SupplierShareShouldGet.GetValueOrDefault();
                                        }
                                        else //site shares cover min until they get to zero
                                        {
                                            _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() + _termsResults.SiteGroupRent.GetValueOrDefault();
                                            _termsResults.SiteGroupRent = 0;
                                        }
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void UseSecSiteGroupForSupplierShortfall()
        {
            float? temp = 0;

            try
            {
                LogManager.WriteLog("Inside UseSecSiteGroupForSupplierShortfall...", LogManager.enumLogLevel.Info);

                switch (_termsInfo.SecGroupGuarantor.GetValueOrDefault())
                {
                    case TERMS_PRIMARY_TO_ZERO:
                    case TERMS_PRIMARY_FULL:
                    case TERMS_SECONDARY_TO_ZERO:
                    case TERMS_SECONDARY_REMAINDER:
                    case TERMS_PERCENTAGE:
                        switch (_termsInfo.SupplierType.GetValueOrDefault())
                        {
                            case TERMS_RENT:
                            case TERMS_RENT_FIXED:
                            case TERMS_RENT_FULL:
                            case TERMS_RENT_ONLY:
                            case TERMS_RENT_SCHEDULE:
                                //if the rent is guaranteed
                                if ((_termsInfo.SupplierValueGuaranteed.GetValueOrDefault() && _termsInfo.SupplierType != TERMS_RENT_SCHEDULE) || (_termsInfo.SupplierShareGuaranteed.GetValueOrDefault() && _termsInfo.SupplierType == TERMS_RENT_SCHEDULE))
                                {
                                    //if the supplier needs a shortfall guarantor...
                                    if (_termsResults.SupplierRentShouldGet.GetValueOrDefault() - _termsResults.SupplierRent.GetValueOrDefault() > 0)
                                    {
                                        //take value off site shares
                                        if (_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE)
                                        {
                                            //take a % of the shortfall from the site shares

                                            //find out if the site is the only percentage guarantor left
                                            if ((_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SiteUse.GetValueOrDefault() > _termsInfo.SecGroupUse.GetValueOrDefault())
                                                || (_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SecGroupUse.GetValueOrDefault() > _termsInfo.SecGroupUse.GetValueOrDefault()))
                                            {
                                                temp = _termsInfo.SecGroupGuarantorPercentage.GetValueOrDefault() / 100;
                                                temp = temp * (_termsResults.SupplierRentShouldGet.GetValueOrDefault() - _termsResults.SupplierRent.GetValueOrDefault());
                                                _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() - Format2DPs(temp.GetValueOrDefault());
                                                _termsResults.SupplierRent = _termsResults.SupplierRent.GetValueOrDefault() + Format2DPs(temp.GetValueOrDefault());
                                                temp = 0;
                                            }
                                            else //there is no other shortfall guarantor, therefore the remainder is to be paid
                                            {
                                                _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() - (_termsResults.SupplierRentShouldGet.GetValueOrDefault() - _termsResults.SupplierRent.GetValueOrDefault());
                                                _termsResults.SupplierRent = _termsResults.SupplierRentShouldGet.GetValueOrDefault();
                                            }
                                        }
                                    }
                                    else if (_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER || _termsResults.SecSiteGroupShare.GetValueOrDefault() > (_termsResults.SupplierRentShouldGet.GetValueOrDefault() - _termsResults.SupplierRent.GetValueOrDefault()))
                                    {
                                        _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() - (_termsResults.SupplierRentShouldGet.GetValueOrDefault() - _termsResults.SupplierRent.GetValueOrDefault());
                                        _termsResults.SupplierRent = _termsResults.SupplierRentShouldGet.GetValueOrDefault();
                                    }
                                    else //to zero
                                    {
                                        _termsResults.SupplierRent = _termsResults.SupplierRent.GetValueOrDefault() + _termsResults.SecSiteGroupShare.GetValueOrDefault();
                                        _termsResults.SecSiteGroupShare = 0;
                                    }

                                    //if the supplier still needs a shortfall guarantor...
                                    if (_termsResults.SupplierRentShouldGet.GetValueOrDefault() - _termsResults.SupplierRent.GetValueOrDefault() > 0)
                                    {
                                        //take remaining value off site rent
                                        if (_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //do nothing - already got the shortfall
                                        { }
                                        else if (_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER)   //do nothing - already got the shortfall
                                        { }
                                        else if (_termsResults.SecSiteGroupRent.GetValueOrDefault() > (_termsResults.SupplierRentShouldGet.GetValueOrDefault() - _termsResults.SupplierRent.GetValueOrDefault()))   //Has to be to zero
                                        {
                                            _termsResults.SecSiteGroupRent = _termsResults.SecSiteGroupRent.GetValueOrDefault() - (_termsResults.SupplierRentShouldGet.GetValueOrDefault() - _termsResults.SupplierRent.GetValueOrDefault());
                                            _termsResults.SupplierRent = _termsResults.SupplierRentShouldGet.GetValueOrDefault();
                                        }
                                        else //to zero
                                        {
                                            _termsResults.SupplierRent = _termsResults.SupplierRent.GetValueOrDefault() + _termsResults.SecSiteGroupRent.GetValueOrDefault();
                                            _termsResults.SecSiteGroupRent = 0;
                                        }
                                    }
                                }
                                break;
                            case TERMS_MIN:
                                if (_termsInfo.SupplierValueGuaranteed.GetValueOrDefault())
                                {
                                    if (_termsInfo.SupplierValue.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault() > 0) //if the supplier needs a shortfall guarantor for the min
                                    {
                                        if (_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //if the site can cover the shortfall out of shares then
                                        {
                                            //take a % of shortfall from Site
                                            //find out if the site is the only percentage guarantor left
                                            if ((_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SiteUse.GetValueOrDefault() > _termsInfo.SecGroupUse.GetValueOrDefault())
                                                || (_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SecGroupUse.GetValueOrDefault() > _termsInfo.SecGroupUse.GetValueOrDefault()))
                                            {
                                                temp = _termsInfo.SecGroupGuarantorPercentage.GetValueOrDefault() / 100;
                                                temp = temp * (_termsInfo.SupplierValue.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault());
                                                _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() - Format2DPs(temp.GetValueOrDefault());
                                                _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() + Format2DPs(temp.GetValueOrDefault());
                                                temp = 0;
                                            }
                                            else //there is no other shortfall guarantor, therefore the remainder is to be paid to bring up to the min
                                            {
                                                _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() - (_termsInfo.SupplierValue.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault());
                                                _termsResults.SupplierShare = _termsInfo.SupplierValue.GetValueOrDefault();
                                            }
                                        }
                                        else if (_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER ||
                                                    (_termsResults.SecSiteGroupShare.GetValueOrDefault() > (_termsInfo.SupplierValue.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault())))
                                        {
                                            _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() - (_termsInfo.SupplierValue.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault());
                                            _termsResults.SupplierShare = _termsInfo.SupplierValue.GetValueOrDefault();
                                        }
                                        else
                                        {
                                            //site shares cover min until they get to zero
                                            _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() + _termsResults.SecSiteGroupShare.GetValueOrDefault();
                                            _termsResults.SecSiteGroupShare = 0;
                                        }
                                    }

                                    //if there is still a shortfall on the minimum
                                    if (_termsInfo.SupplierValue.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault() > 0)
                                    {
                                        //now look at the site's rent
                                        if (_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //shortfall is already taken care of
                                        { }
                                        else if (_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER)  //do nothing - already got the shortfall
                                        { }
                                        else if (_termsResults.SecSiteGroupRent.GetValueOrDefault() > (_termsInfo.SupplierValue.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault())) //to zero
                                        {
                                            _termsResults.SecSiteGroupRent = _termsResults.SecSiteGroupRent.GetValueOrDefault() - (_termsInfo.SupplierValue.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault());
                                            _termsResults.SupplierShare = _termsInfo.SupplierValue.GetValueOrDefault();
                                        }
                                        else //site shares cover min until they get to zero
                                        {
                                            _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() + _termsResults.SecSiteGroupRent.GetValueOrDefault();
                                            _termsResults.SecSiteGroupRent = 0;
                                        }
                                    }
                                }

                                //Now check if the share is guaranteed
                                if (_termsInfo.SupplierShareGuaranteed.GetValueOrDefault())
                                {
                                    if (_termsResults.SupplierShareShouldGet.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault() > 0) //if the supplier needs a shortfall guarantor for the min
                                    {
                                        if (_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //if the site can cover the shortfall out of shares then
                                        {
                                            //take a % of shortfall from Site
                                            //find out if the site is the only percentage guarantor left
                                            if ((_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SiteUse.GetValueOrDefault() > _termsInfo.SecGroupUse.GetValueOrDefault())
                                                || (_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SecGroupUse.GetValueOrDefault() > _termsInfo.SecGroupUse.GetValueOrDefault()))
                                            {
                                                temp = _termsInfo.SecGroupGuarantorPercentage.GetValueOrDefault() / 100;
                                                temp = temp * (_termsResults.SupplierShareShouldGet.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault());
                                                _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() - Format2DPs(temp.GetValueOrDefault());
                                                _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() + Format2DPs(temp.GetValueOrDefault());
                                                temp = 0;
                                            }
                                            else //there is no other shortfall guarantor, therefore the remainder is to be paid to bring up to the min
                                            {
                                                _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() - (_termsResults.SupplierShareShouldGet.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault());
                                                _termsResults.SupplierShare = _termsResults.SupplierShareShouldGet.GetValueOrDefault();
                                            }
                                        }
                                        else if (_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER ||
                                                    (_termsResults.SecSiteGroupShare.GetValueOrDefault() > (_termsResults.SupplierShareShouldGet.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault())))
                                        {
                                            _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() - (_termsResults.SupplierShareShouldGet.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault());
                                            _termsResults.SupplierShare = _termsResults.SupplierShareShouldGet.GetValueOrDefault();
                                        }
                                        else//site shares cover min until they get to zero
                                        {
                                            _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() + _termsResults.SecSiteGroupShare.GetValueOrDefault();
                                            _termsResults.SecSiteGroupShare = 0;
                                        }
                                    }

                                    //if there is still a shortfall on the minimum
                                    if (_termsResults.SupplierShareShouldGet.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault() > 0)
                                    {
                                        //now look at the site's rent
                                        if (_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //shortfall is already taken care of
                                        { }
                                        else if (_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER)  //do nothing - already got the shortfall
                                        { }
                                        else if (_termsResults.SecSiteGroupRent.GetValueOrDefault() > (_termsResults.SupplierShareShouldGet.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault())) //to zero
                                        {
                                            _termsResults.SecSiteGroupRent = _termsResults.SecSiteGroupRent.GetValueOrDefault() - (_termsResults.SupplierShareShouldGet.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault());
                                            _termsResults.SupplierShare = _termsResults.SupplierShareShouldGet.GetValueOrDefault();
                                        }
                                        else //site shares cover min until they get to zero
                                        {
                                            _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() + _termsResults.SecSiteGroupRent.GetValueOrDefault();
                                            _termsResults.SecSiteGroupRent = 0;
                                        }
                                    }
                                }
                                break;
                            case TERMS_MAX:
                                //the max value can't be guaranteed
                                //so we only have to guarantee the shares
                                if (_termsInfo.SupplierShareGuaranteed.GetValueOrDefault())
                                {
                                    if (_termsResults.SupplierShareShouldGet.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault() > 0) //if the supplier needs a shortfall guarantor for the share
                                    {
                                        if (_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //if the site can cover the shortfall out of shares then
                                        {
                                            //take a % of shortfall from Site
                                            //find out if the site is the only percentage guarantor left
                                            if ((_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SiteUse.GetValueOrDefault() > _termsInfo.SecGroupUse.GetValueOrDefault())
                                                || (_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SecGroupUse.GetValueOrDefault() > _termsInfo.SecGroupUse.GetValueOrDefault()))
                                            {
                                                temp = _termsInfo.SecGroupGuarantorPercentage.GetValueOrDefault() / 100;
                                                temp = temp * (_termsResults.SupplierShareShouldGet.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault());
                                                _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() - Format2DPs(temp.GetValueOrDefault());
                                                _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() + Format2DPs(temp.GetValueOrDefault());
                                                temp = 0;
                                            }
                                            else //there is no other shortfall guarantor, therefore the remainder is to be paid to bring up to the min
                                            {
                                                _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() - (_termsResults.SupplierShareShouldGet.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault());
                                                _termsResults.SupplierShare = _termsResults.SupplierShareShouldGet.GetValueOrDefault();
                                            }
                                        }
                                        else if (_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER ||
                                                    (_termsResults.SecSiteGroupShare.GetValueOrDefault() > (_termsResults.SupplierShareShouldGet.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault())))
                                        {
                                            _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() - (_termsResults.SupplierShareShouldGet.GetValueOrDefault() - _termsResults.SupplierShare);
                                            _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() - (_termsResults.SupplierShareShouldGet.GetValueOrDefault() - _termsResults.SupplierShare);
                                            _termsResults.SupplierShare = _termsResults.SupplierShareShouldGet.GetValueOrDefault();
                                        }
                                        else
                                        {
                                            //site shares cover min until they get to zero
                                            _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() + _termsResults.SecSiteGroupShare.GetValueOrDefault();
                                            _termsResults.SecSiteGroupShare = 0;
                                        }
                                    }

                                    //if there is still a shortfall on the minimum
                                    if (_termsResults.SupplierShareShouldGet.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault() > 0)
                                    {
                                        //now look at the site's rent
                                        if (_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //shortfall is already taken care of
                                        { }
                                        else if (_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER)  //do nothing - already got the shortfall
                                        { }
                                        else if (_termsResults.SecSiteGroupRent.GetValueOrDefault() > (_termsResults.SupplierShareShouldGet.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault())) //to zero
                                        {
                                            _termsResults.SecSiteGroupRent = _termsResults.SecSiteGroupRent.GetValueOrDefault() - (_termsResults.SupplierShareShouldGet.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault());
                                            _termsResults.SupplierShare = _termsResults.SupplierShareShouldGet.GetValueOrDefault();
                                        }
                                        else //site shares cover min until they get to zero
                                        {
                                            _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() + _termsResults.SecSiteGroupRent.GetValueOrDefault();
                                            _termsResults.SecSiteGroupRent = 0;
                                        }
                                    }
                                }
                                break;
                            case TERMS_FRONT_MONEY:
                                if (_termsInfo.SupplierValueGuaranteed.GetValueOrDefault())
                                {
                                    if (_termsResults.SupplierRentShouldGet.GetValueOrDefault() - _termsResults.SupplierRent.GetValueOrDefault() > 0) //if the supplier needs a shortfall guarantor for the rent
                                    {
                                        if (_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //if the site can cover the shortfall out of shares then
                                        {
                                            //take a % of shortfall from Site
                                            //find out if the site is the only percentage guarantor left
                                            if ((_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SiteUse.GetValueOrDefault() > _termsInfo.SecGroupUse.GetValueOrDefault())
                                                || (_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SecGroupUse.GetValueOrDefault() > _termsInfo.SecGroupUse.GetValueOrDefault()))
                                            {
                                                temp = _termsInfo.SecGroupGuarantorPercentage.GetValueOrDefault() / 100;
                                                temp = temp * (_termsResults.SupplierRentShouldGet.GetValueOrDefault() - _termsResults.SupplierRent.GetValueOrDefault());
                                                _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() - Format2DPs(temp.GetValueOrDefault());
                                                _termsResults.SupplierRent = _termsResults.SupplierRent.GetValueOrDefault() + Format2DPs(temp.GetValueOrDefault());
                                                temp = 0;
                                            }
                                            else //there is no other shortfall guarantor, therefore the remainder is to be paid to bring up to the min
                                            {
                                                _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() - (_termsResults.SupplierRentShouldGet.GetValueOrDefault() - _termsResults.SupplierRent.GetValueOrDefault());
                                                _termsResults.SupplierRent = _termsResults.SupplierRentShouldGet.GetValueOrDefault();
                                            }
                                        }
                                        else if (_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER ||
                                                    (_termsResults.SecSiteGroupShare.GetValueOrDefault() > (_termsResults.SupplierRentShouldGet.GetValueOrDefault() - _termsResults.SupplierRent.GetValueOrDefault())))
                                        {
                                            _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() - (_termsResults.SupplierShareShouldGet.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault());
                                            _termsResults.SupplierRent = _termsResults.SupplierRentShouldGet.GetValueOrDefault();
                                        }
                                        else
                                        {
                                            //site shares cover min until they get to zero
                                            _termsResults.SupplierRent = _termsResults.SupplierRent.GetValueOrDefault() + _termsResults.SecSiteGroupShare.GetValueOrDefault();
                                            _termsResults.SecSiteGroupShare = 0;
                                        }
                                    }

                                    //if there is still a shortfall on the minimum
                                    if (_termsResults.SupplierRentShouldGet.GetValueOrDefault() - _termsResults.SupplierRent.GetValueOrDefault() > 0)
                                    {
                                        //now look at the site's rent
                                        if (_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //shortfall is already taken care of
                                        { }
                                        else if (_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER)  //do nothing - already got the shortfall
                                        { }
                                        else if (_termsResults.SecSiteGroupRent.GetValueOrDefault() > (_termsResults.SupplierRentShouldGet.GetValueOrDefault() - _termsResults.SupplierRent.GetValueOrDefault())) //to zero
                                        {
                                            _termsResults.SecSiteGroupRent = _termsResults.SecSiteGroupRent.GetValueOrDefault() - (_termsResults.SupplierRentShouldGet.GetValueOrDefault() - _termsResults.SupplierRent.GetValueOrDefault());
                                            _termsResults.SupplierRent = _termsResults.SupplierRentShouldGet.GetValueOrDefault();
                                        }
                                        else //site shares cover min until they get to zero
                                        {
                                            _termsResults.SupplierRent = _termsResults.SupplierRent.GetValueOrDefault() + _termsResults.SecSiteGroupRent.GetValueOrDefault();
                                            _termsResults.SecSiteGroupRent = 0;
                                        }
                                    }
                                }

                                //Now check if the share is guaranteed
                                if (_termsInfo.SupplierShareGuaranteed.GetValueOrDefault())
                                {
                                    if (_termsResults.SupplierShareShouldGet.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault() > 0) //if the supplier needs a shortfall guarantor for the min
                                    {
                                        if (_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //if the site can cover the shortfall out of shares then
                                        {
                                            //take a % of shortfall from Site
                                            //find out if the site is the only percentage guarantor left
                                            if ((_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SiteUse.GetValueOrDefault() > _termsInfo.SecGroupUse.GetValueOrDefault())
                                                || (_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SecGroupUse.GetValueOrDefault() > _termsInfo.SecGroupUse.GetValueOrDefault()))
                                            {
                                                temp = _termsInfo.SecGroupGuarantorPercentage.GetValueOrDefault() / 100;
                                                temp = temp * (_termsResults.SupplierShareShouldGet.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault());
                                                _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() - Format2DPs(temp.GetValueOrDefault());
                                                _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() + Format2DPs(temp.GetValueOrDefault());
                                                temp = 0;
                                            }
                                            else //there is no other shortfall guarantor, therefore the remainder is to be paid to bring up to the min
                                            {
                                                _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() - (_termsResults.SupplierShareShouldGet.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault());
                                                _termsResults.SupplierShare = _termsResults.SupplierShareShouldGet.GetValueOrDefault();
                                            }
                                        }
                                        else if (_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER ||
                                                    (_termsResults.SecSiteGroupShare.GetValueOrDefault() > (_termsResults.SupplierShareShouldGet.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault())))
                                        {
                                            _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() - (_termsResults.SupplierShareShouldGet.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault());
                                            _termsResults.SupplierShare = _termsResults.SupplierShareShouldGet.GetValueOrDefault();
                                        }
                                        else
                                        {
                                            //site shares cover min until they get to zero
                                            _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() + _termsResults.SecSiteGroupShare.GetValueOrDefault();
                                            _termsResults.SecSiteGroupShare = 0;
                                        }
                                    }

                                    //if there is still a shortfall on the minimum
                                    //take it out of the site's rent
                                    if (_termsResults.SupplierShareShouldGet.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault() > 0)
                                    {
                                        //now look at the site's rent
                                        if (_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //shortfall is already taken care of
                                        { }
                                        else if (_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER)  //do nothing - already got the shortfall
                                        { }
                                        else if (_termsResults.SecSiteGroupRent.GetValueOrDefault() > (_termsResults.SupplierShareShouldGet.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault())) //to zero
                                        {
                                            _termsResults.SecSiteGroupRent = _termsResults.SecSiteGroupRent.GetValueOrDefault() - (_termsResults.SupplierShareShouldGet.GetValueOrDefault() - _termsResults.SupplierShare.GetValueOrDefault());
                                            _termsResults.SupplierShare = _termsResults.SupplierShareShouldGet.GetValueOrDefault();
                                        }
                                        else //site shares cover min until they get to zero
                                        {
                                            _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() + _termsResults.SecSiteGroupRent.GetValueOrDefault();
                                            _termsResults.SecSiteGroupRent = 0;
                                        }
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void UseSiteForSiteGroupShortfall()
        {
            float? temp = 0;

            try
            {
                LogManager.WriteLog("Inside UseSiteForSiteGroupShortfall...", LogManager.enumLogLevel.Info);

                switch (_termsInfo.SiteGuarantor.GetValueOrDefault())
                {
                    case TERMS_PRIMARY_TO_ZERO:
                    case TERMS_PRIMARY_FULL:
                    case TERMS_SECONDARY_TO_ZERO:
                    case TERMS_SECONDARY_REMAINDER:
                    case TERMS_PERCENTAGE:
                        switch (_termsInfo.GroupType.GetValueOrDefault())
                        {
                            case TERMS_RENT:
                                //if the rent is guaranteed
                                if (_termsInfo.GroupValueGuaranteed.GetValueOrDefault())
                                {
                                    //if the SiteGroup needs a shortfall guarantor...
                                    if (_termsResults.SiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SiteGroupRent.GetValueOrDefault() > 0)
                                    {
                                        //take value off site shares
                                        if (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE)
                                        {
                                            //take a % of the shortfall from the site shares

                                            //find out if the site is the only percentage guarantor left
                                            if ((_termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SupplierUse.GetValueOrDefault() > _termsInfo.SiteUse.GetValueOrDefault())
                                                || (_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SecGroupUse.GetValueOrDefault() > _termsInfo.SiteUse.GetValueOrDefault()))
                                            {
                                                temp = _termsInfo.SiteGuarantorPercentage.GetValueOrDefault() / 100;
                                                temp = temp * (_termsResults.SiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SiteGroupRent.GetValueOrDefault());
                                                _termsResults.SiteShare = _termsResults.SiteShare.GetValueOrDefault() - Format2DPs(temp.GetValueOrDefault());
                                                _termsResults.SiteGroupRent = _termsResults.SiteGroupRent.GetValueOrDefault() + Format2DPs(temp.GetValueOrDefault());
                                                temp = 0;
                                            }
                                            else //there is no other shortfall guarantor, therefore the remainder is to be paid
                                            {
                                                _termsResults.SiteShare = _termsResults.SiteShare.GetValueOrDefault() - (_termsResults.SiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SiteGroupRent.GetValueOrDefault());
                                                _termsResults.SiteGroupRent = _termsResults.SiteGroupRentShouldGet.GetValueOrDefault();
                                            }
                                        }
                                    }
                                    else if (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER || _termsResults.SiteShare.GetValueOrDefault() > (_termsResults.SiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SiteGroupRent.GetValueOrDefault()))
                                    {
                                        _termsResults.SiteShare = _termsResults.SiteShare.GetValueOrDefault() - (_termsResults.SiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SiteGroupRent.GetValueOrDefault());
                                        _termsResults.SiteGroupRent = _termsResults.SiteGroupRentShouldGet.GetValueOrDefault();
                                    }
                                    else //to zero
                                    {
                                        _termsResults.SiteGroupRent = _termsResults.SiteGroupRent.GetValueOrDefault() + _termsResults.SiteShare.GetValueOrDefault();
                                        _termsResults.SiteShare = 0;
                                    }

                                    //if the SiteGroup still needs a shortfall guarantor...
                                    if (_termsResults.SiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SiteGroupRent.GetValueOrDefault() > 0)
                                    {
                                        //take remaining value off site rent
                                        if (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //do nothing - already got the shortfall
                                        { }
                                        else if (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER)   //do nothing - already got the shortfall
                                        { }
                                        else if (_termsResults.SiteRent.GetValueOrDefault() > (_termsResults.SiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SiteGroupRent.GetValueOrDefault()))   //Has to be to zero
                                        {
                                            _termsResults.SiteRent = _termsResults.SiteRent.GetValueOrDefault() - (_termsResults.SiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SiteGroupRent.GetValueOrDefault());
                                            _termsResults.SiteGroupRent = _termsResults.SiteGroupRentShouldGet.GetValueOrDefault();
                                        }
                                        else //to zero
                                        {
                                            _termsResults.SiteGroupRent = _termsResults.SiteGroupRent.GetValueOrDefault() + _termsResults.SiteRent.GetValueOrDefault();
                                            _termsResults.SiteRent = 0;
                                        }
                                    }
                                }
                                break;
                            case TERMS_MIN:
                                if (_termsInfo.GroupValueGuaranteed.GetValueOrDefault())
                                {
                                    if (_termsInfo.GroupValue.GetValueOrDefault() - _termsResults.SiteGroupShare.GetValueOrDefault() > 0) //if the SiteGroup needs a shortfall guarantor for the min
                                    {
                                        if (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //if the site can cover the shortfall out of shares then
                                        {
                                            //take a % of shortfall from Site
                                            //find out if the site is the only percentage guarantor left
                                            if ((_termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SupplierUse.GetValueOrDefault() > _termsInfo.SiteUse.GetValueOrDefault())
                                                || (_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SecGroupUse.GetValueOrDefault() > _termsInfo.SiteUse.GetValueOrDefault()))
                                            {
                                                temp = _termsInfo.SiteGuarantorPercentage.GetValueOrDefault() / 100;
                                                temp = temp * (_termsInfo.GroupValue.GetValueOrDefault() - _termsResults.SiteGroupShare.GetValueOrDefault());
                                                _termsResults.SiteShare = _termsResults.SiteShare.GetValueOrDefault() - Format2DPs(temp.GetValueOrDefault());
                                                _termsResults.SiteGroupShare = _termsResults.SiteGroupShare.GetValueOrDefault() + Format2DPs(temp.GetValueOrDefault());
                                                temp = 0;
                                            }
                                            else //there is no other shortfall guarantor, therefore the remainder is to be paid to bring up to the min
                                            {
                                                _termsResults.SiteShare = _termsResults.SiteShare.GetValueOrDefault() - (_termsInfo.GroupValue.GetValueOrDefault() - _termsResults.SiteGroupShare.GetValueOrDefault());
                                                _termsResults.SiteGroupShare = _termsInfo.GroupValue.GetValueOrDefault();
                                            }
                                        }
                                        else if (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER ||
                                                    (_termsResults.SiteShare.GetValueOrDefault() > (_termsInfo.GroupValue.GetValueOrDefault() - _termsResults.SiteGroupShare.GetValueOrDefault())))
                                        {
                                            _termsResults.SiteShare = _termsResults.SiteShare.GetValueOrDefault() - (_termsInfo.GroupValue.GetValueOrDefault() - _termsResults.SiteGroupShare.GetValueOrDefault());
                                            _termsResults.SiteGroupShare = _termsInfo.GroupValue.GetValueOrDefault();
                                        }
                                        else
                                        {
                                            //site shares cover min until they get to zero
                                            _termsResults.SiteGroupShare = _termsResults.SiteGroupShare.GetValueOrDefault() + _termsResults.SiteShare.GetValueOrDefault();
                                            _termsResults.SiteShare = 0;
                                        }
                                    }

                                    //if there is still a shortfall on the minimum
                                    if (_termsInfo.GroupValue.GetValueOrDefault() - _termsResults.SiteGroupShare.GetValueOrDefault() > 0)
                                    {
                                        //now look at the site's rent
                                        if (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //shortfall is already taken care of
                                        { }
                                        else if (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER)  //do nothing - already got the shortfall
                                        { }
                                        else if (_termsResults.SiteRent.GetValueOrDefault() > (_termsInfo.GroupValue.GetValueOrDefault() - _termsResults.SiteGroupShare.GetValueOrDefault())) //to zero
                                        {
                                            _termsResults.SiteRent = _termsResults.SiteRent.GetValueOrDefault() - (_termsInfo.GroupValue.GetValueOrDefault() - _termsResults.SiteGroupShare.GetValueOrDefault());
                                            _termsResults.SiteGroupShare = _termsInfo.GroupValue.GetValueOrDefault();
                                        }
                                        else //site shares cover min until they get to zero
                                        {
                                            _termsResults.SiteGroupShare = _termsResults.SiteGroupShare.GetValueOrDefault() + _termsResults.SiteRent.GetValueOrDefault();
                                            _termsResults.SiteRent = 0;
                                        }
                                    }
                                }

                                //Now check if the share is guaranteed
                                if (_termsInfo.GroupShareGuaranteed.GetValueOrDefault())
                                {
                                    if (_termsResults.SiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SiteGroupShare.GetValueOrDefault() > 0) //if the SiteGroup needs a shortfall guarantor for the min
                                    {
                                        if (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //if the site can cover the shortfall out of shares then
                                        {
                                            //take a % of shortfall from Site
                                            //find out if the site is the only percentage guarantor left
                                            if ((_termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SupplierUse.GetValueOrDefault() > _termsInfo.SiteUse.GetValueOrDefault())
                                                || (_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SecGroupUse.GetValueOrDefault() > _termsInfo.SiteUse.GetValueOrDefault()))
                                            {
                                                temp = _termsInfo.SiteGuarantorPercentage.GetValueOrDefault() / 100;
                                                temp = temp * (_termsResults.SiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SiteGroupShare.GetValueOrDefault());
                                                _termsResults.SiteShare = _termsResults.SiteShare.GetValueOrDefault() - Format2DPs(temp.GetValueOrDefault());
                                                _termsResults.SiteGroupShare = _termsResults.SiteGroupShare.GetValueOrDefault() + Format2DPs(temp.GetValueOrDefault());
                                                temp = 0;
                                            }
                                            else //there is no other shortfall guarantor, therefore the remainder is to be paid to bring up to the min
                                            {
                                                _termsResults.SiteShare = _termsResults.SiteShare.GetValueOrDefault() - (_termsResults.SiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SiteGroupShare.GetValueOrDefault());
                                                _termsResults.SiteGroupShare = _termsResults.SiteGroupShareShouldGet.GetValueOrDefault();
                                            }
                                        }
                                        else if (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER ||
                                                    (_termsResults.SiteShare.GetValueOrDefault() > (_termsResults.SiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SiteGroupShare.GetValueOrDefault())))
                                        {
                                            _termsResults.SiteShare = _termsResults.SiteShare.GetValueOrDefault() - (_termsResults.SiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SiteGroupShare.GetValueOrDefault());
                                            _termsResults.SiteGroupShare = _termsResults.SiteGroupShareShouldGet.GetValueOrDefault();
                                        }
                                        else//site shares cover min until they get to zero
                                        {
                                            _termsResults.SiteGroupShare = _termsResults.SiteGroupShare.GetValueOrDefault() + _termsResults.SiteShare.GetValueOrDefault();
                                            _termsResults.SiteShare = 0;
                                        }
                                    }

                                    //if there is still a shortfall on the minimum
                                    if (_termsResults.SiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SiteGroupShare.GetValueOrDefault() > 0)
                                    {
                                        //now look at the site's rent
                                        if (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //shortfall is already taken care of
                                        { }
                                        else if (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER)  //do nothing - already got the shortfall
                                        { }
                                        else if (_termsResults.SiteRent.GetValueOrDefault() > (_termsResults.SiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SiteGroupShare.GetValueOrDefault())) //to zero
                                        {
                                            _termsResults.SiteRent = _termsResults.SiteRent.GetValueOrDefault() - (_termsResults.SiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SiteGroupShare.GetValueOrDefault());
                                            _termsResults.SiteGroupShare = _termsResults.SiteGroupShareShouldGet.GetValueOrDefault();
                                        }
                                        else //site shares cover min until they get to zero
                                        {
                                            _termsResults.SiteGroupShare = _termsResults.SiteGroupShare.GetValueOrDefault() + _termsResults.SiteRent.GetValueOrDefault();
                                            _termsResults.SiteRent = 0;
                                        }
                                    }
                                }
                                break;
                            case TERMS_MAX:
                                //the max value can't be guaranteed
                                //so we only have to guarantee the shares
                                if (_termsInfo.GroupShareGuaranteed.GetValueOrDefault())
                                {
                                    if (_termsResults.SiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SiteGroupShare.GetValueOrDefault() > 0) //if the SiteGroup needs a shortfall guarantor for the share
                                    {
                                        if (_termsInfo.SiteGuarantor == TERMS_PERCENTAGE) //if the site can cover the shortfall out of shares then
                                        {
                                            //take a % of shortfall from Site
                                            //find out if the site is the only percentage guarantor left
                                            if ((_termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SupplierUse.GetValueOrDefault() > _termsInfo.SiteUse.GetValueOrDefault())
                                                || (_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SecGroupUse.GetValueOrDefault() > _termsInfo.SiteUse.GetValueOrDefault()))
                                            {
                                                temp = _termsInfo.SiteGuarantorPercentage.GetValueOrDefault() / 100;
                                                temp = temp * (_termsResults.SiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SiteGroupShare.GetValueOrDefault());
                                                _termsResults.SiteShare = _termsResults.SiteShare.GetValueOrDefault() - Format2DPs(temp.GetValueOrDefault());
                                                _termsResults.SiteGroupShare = _termsResults.SiteGroupShare.GetValueOrDefault() + Format2DPs(temp.GetValueOrDefault());
                                                temp = 0;
                                            }
                                            else //there is no other shortfall guarantor, therefore the remainder is to be paid to bring up to the min
                                            {
                                                _termsResults.SiteShare = _termsResults.SiteShare.GetValueOrDefault() - (_termsResults.SiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SiteGroupShare.GetValueOrDefault());
                                                _termsResults.SiteGroupShare = _termsResults.SiteGroupShareShouldGet.GetValueOrDefault();
                                            }
                                        }
                                        else if (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER ||
                                                    (_termsResults.SiteShare.GetValueOrDefault() > (_termsResults.SiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SiteGroupShare.GetValueOrDefault())))
                                        {
                                            _termsResults.SiteShare = _termsResults.SiteShare.GetValueOrDefault() - (_termsResults.SiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SiteGroupShare.GetValueOrDefault());
                                            _termsResults.SiteGroupShare = _termsResults.SiteGroupShareShouldGet.GetValueOrDefault();
                                        }
                                        else
                                        {
                                            //site shares cover min until they get to zero
                                            _termsResults.SiteGroupShare = _termsResults.SiteGroupShare.GetValueOrDefault() + _termsResults.SiteShare.GetValueOrDefault();
                                            _termsResults.SiteShare = 0;
                                        }
                                    }

                                    //if there is still a shortfall on the minimum
                                    if (_termsResults.SiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SiteGroupShare.GetValueOrDefault() > 0)
                                    {
                                        //now look at the site's rent
                                        if (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //shortfall is already taken care of
                                        { }
                                        else if (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER)  //do nothing - already got the shortfall
                                        { }
                                        else if (_termsResults.SiteRent.GetValueOrDefault() > (_termsResults.SiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SiteGroupShare.GetValueOrDefault())) //to zero
                                        {
                                            _termsResults.SiteRent = _termsResults.SiteRent.GetValueOrDefault() - (_termsResults.SiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SiteGroupShare.GetValueOrDefault());
                                            _termsResults.SiteGroupShare = _termsResults.SiteGroupShareShouldGet.GetValueOrDefault();
                                        }
                                        else //site shares cover min until they get to zero
                                        {
                                            _termsResults.SiteGroupShare = _termsResults.SiteGroupShare.GetValueOrDefault() + _termsResults.SiteRent.GetValueOrDefault();
                                            _termsResults.SiteRent = 0;
                                        }
                                    }
                                }
                                break;
                            case TERMS_FRONT_MONEY:
                                if (_termsInfo.GroupValueGuaranteed.GetValueOrDefault())
                                {
                                    if (_termsResults.SiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SiteGroupRent.GetValueOrDefault() > 0) //if the SiteGroup needs a shortfall guarantor for the rent
                                    {
                                        if (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //if the site can cover the shortfall out of shares then
                                        {
                                            //take a % of shortfall from Site
                                            //find out if the site is the only percentage guarantor left
                                            if ((_termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SupplierUse.GetValueOrDefault() > _termsInfo.SiteUse.GetValueOrDefault())
                                                || (_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SecGroupUse.GetValueOrDefault() > _termsInfo.SiteUse.GetValueOrDefault()))
                                            {
                                                temp = _termsInfo.SiteGuarantorPercentage.GetValueOrDefault() / 100;
                                                temp = temp * (_termsResults.SiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SiteGroupRent.GetValueOrDefault());
                                                _termsResults.SiteShare = _termsResults.SiteShare.GetValueOrDefault() - Format2DPs(temp.GetValueOrDefault());
                                                _termsResults.SiteGroupRent = _termsResults.SiteGroupRent.GetValueOrDefault() + Format2DPs(temp.GetValueOrDefault());
                                                temp = 0;
                                            }
                                            else //there is no other shortfall guarantor, therefore the remainder is to be paid to bring up to the min
                                            {
                                                _termsResults.SiteShare = _termsResults.SiteShare.GetValueOrDefault() - (_termsResults.SiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SiteGroupRent.GetValueOrDefault());
                                                _termsResults.SiteGroupRent = _termsResults.SiteGroupRentShouldGet.GetValueOrDefault();
                                            }
                                        }
                                        else if (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER ||
                                                    (_termsResults.SiteShare.GetValueOrDefault() > (_termsResults.SiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SiteGroupRent.GetValueOrDefault())))
                                        {
                                            _termsResults.SiteShare = _termsResults.SiteShare.GetValueOrDefault() - (_termsResults.SiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SiteGroupRent.GetValueOrDefault());
                                            _termsResults.SiteGroupRent = _termsResults.SiteGroupRentShouldGet.GetValueOrDefault();
                                        }
                                        else
                                        {
                                            //site shares cover min until they get to zero
                                            _termsResults.SiteGroupRent = _termsResults.SiteGroupRent.GetValueOrDefault() + _termsResults.SiteShare.GetValueOrDefault();
                                            _termsResults.SiteShare = 0;
                                        }
                                    }

                                    //if there is still a shortfall on the minimum
                                    if (_termsResults.SiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SiteGroupRent.GetValueOrDefault() > 0)
                                    {
                                        //now look at the site's rent
                                        if (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //shortfall is already taken care of
                                        { }
                                        else if (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER)  //do nothing - already got the shortfall
                                        { }
                                        else if (_termsResults.SiteRent.GetValueOrDefault() > (_termsResults.SiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SiteGroupRent.GetValueOrDefault())) //to zero
                                        {
                                            _termsResults.SiteRent = _termsResults.SiteRent.GetValueOrDefault() - (_termsResults.SiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SiteGroupRent.GetValueOrDefault());
                                            _termsResults.SiteGroupRent = _termsResults.SiteGroupRentShouldGet.GetValueOrDefault();
                                        }
                                        else //site shares cover min until they get to zero
                                        {
                                            _termsResults.SiteGroupRent = _termsResults.SiteGroupRent.GetValueOrDefault() + _termsResults.SiteRent.GetValueOrDefault();
                                            _termsResults.SiteRent = 0;
                                        }
                                    }
                                }

                                //Now check if the share is guaranteed
                                if (_termsInfo.GroupShareGuaranteed.GetValueOrDefault())
                                {
                                    if (_termsResults.SiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SiteGroupShare.GetValueOrDefault() > 0) //if the SiteGroup needs a shortfall guarantor for the min
                                    {
                                        if (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //if the site can cover the shortfall out of shares then
                                        {
                                            //take a % of shortfall from Site
                                            //find out if the site is the only percentage guarantor left
                                            if ((_termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SupplierUse.GetValueOrDefault() > _termsInfo.SiteUse.GetValueOrDefault())
                                                || (_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SecGroupUse.GetValueOrDefault() > _termsInfo.SiteUse.GetValueOrDefault()))
                                            {
                                                temp = _termsInfo.SiteGuarantorPercentage.GetValueOrDefault() / 100;
                                                temp = temp * (_termsResults.SiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SiteGroupShare.GetValueOrDefault());
                                                _termsResults.SiteShare = _termsResults.SiteShare.GetValueOrDefault() - Format2DPs(temp.GetValueOrDefault());
                                                _termsResults.SiteGroupShare = _termsResults.SiteGroupShare.GetValueOrDefault() + Format2DPs(temp.GetValueOrDefault());
                                                temp = 0;
                                            }
                                            else //there is no other shortfall guarantor, therefore the remainder is to be paid to bring up to the min
                                            {
                                                _termsResults.SiteShare = _termsResults.SiteShare.GetValueOrDefault() - (_termsResults.SiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SiteGroupShare.GetValueOrDefault());
                                                _termsResults.SiteGroupShare = _termsResults.SiteGroupShareShouldGet.GetValueOrDefault();
                                            }
                                        }
                                        else if (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER ||
                                                    (_termsResults.SiteShare.GetValueOrDefault() > (_termsResults.SiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SiteGroupShare.GetValueOrDefault())))
                                        {
                                            _termsResults.SiteShare = _termsResults.SiteShare.GetValueOrDefault() - (_termsResults.SiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SiteGroupShare.GetValueOrDefault());
                                            _termsResults.SiteGroupShare = _termsResults.SiteGroupShareShouldGet.GetValueOrDefault();
                                        }
                                        else
                                        {
                                            //site shares cover min until they get to zero
                                            _termsResults.SiteGroupShare = _termsResults.SiteGroupShare.GetValueOrDefault() + _termsResults.SiteShare.GetValueOrDefault();
                                            _termsResults.SiteShare = 0;
                                        }
                                    }

                                    //if there is still a shortfall on the minimum
                                    //take it out of the site's rent
                                    if (_termsResults.SiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SiteGroupShare.GetValueOrDefault() > 0)
                                    {
                                        //now look at the site's rent
                                        if (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //shortfall is already taken care of
                                        { }
                                        else if (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER)  //do nothing - already got the shortfall
                                        { }
                                        else if (_termsResults.SiteRent.GetValueOrDefault() > (_termsResults.SiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SiteGroupShare.GetValueOrDefault())) //to zero
                                        {
                                            _termsResults.SiteRent = _termsResults.SiteRent.GetValueOrDefault() - (_termsResults.SiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SiteGroupShare.GetValueOrDefault());
                                            _termsResults.SiteGroupShare = _termsResults.SiteGroupShareShouldGet.GetValueOrDefault();
                                        }
                                        else //site shares cover min until they get to zero
                                        {
                                            _termsResults.SiteGroupShare = _termsResults.SiteGroupShare.GetValueOrDefault() + _termsResults.SiteRent.GetValueOrDefault();
                                            _termsResults.SiteRent = 0;
                                        }
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void UseSiteForSecSiteGroupShortfall()
        {
            float? temp = 0;

            try
            {
                LogManager.WriteLog("Inside UseSiteForSecSiteGroupShortfall...", LogManager.enumLogLevel.Info);

                switch (_termsInfo.SiteGuarantor.GetValueOrDefault())
                {
                    case TERMS_PRIMARY_TO_ZERO:
                    case TERMS_PRIMARY_FULL:
                    case TERMS_SECONDARY_TO_ZERO:
                    case TERMS_SECONDARY_REMAINDER:
                    case TERMS_PERCENTAGE:
                        switch (_termsInfo.SecGroupType.GetValueOrDefault())
                        {
                            case TERMS_RENT:
                                //if the rent is guaranteed
                                if (_termsInfo.SecGroupValueGuaranteed.GetValueOrDefault())
                                {
                                    //if the SiteGroup needs a shortfall guarantor...
                                    if (_termsResults.SecSiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupRent.GetValueOrDefault() > 0)
                                    {
                                        //take value off site shares
                                        if (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE)
                                        {
                                            //take a % of the shortfall from the site shares

                                            //find out if the site is the only percentage guarantor left
                                            if ((_termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SupplierUse.GetValueOrDefault() > _termsInfo.SiteUse.GetValueOrDefault())
                                                || (_termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.GroupUse.GetValueOrDefault() > _termsInfo.SiteUse.GetValueOrDefault()))
                                            {
                                                temp = _termsInfo.SiteGuarantorPercentage.GetValueOrDefault() / 100;
                                                temp = temp * (_termsResults.SecSiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupRent.GetValueOrDefault());
                                                _termsResults.SiteShare = _termsResults.SiteShare.GetValueOrDefault() - Format2DPs(temp.GetValueOrDefault());
                                                _termsResults.SecSiteGroupRent = _termsResults.SecSiteGroupRent.GetValueOrDefault() + Format2DPs(temp.GetValueOrDefault());
                                                temp = 0;
                                            }
                                            else //there is no other shortfall guarantor, therefore the remainder is to be paid
                                            {
                                                _termsResults.SiteShare = _termsResults.SiteShare.GetValueOrDefault() - (_termsResults.SecSiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupRent.GetValueOrDefault());
                                                _termsResults.SecSiteGroupRent = _termsResults.SecSiteGroupRentShouldGet.GetValueOrDefault();
                                            }
                                        }
                                    }
                                    else if (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER || _termsResults.SiteShare.GetValueOrDefault() > (_termsResults.SecSiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupRent.GetValueOrDefault()))
                                    {
                                        _termsResults.SiteShare = _termsResults.SiteShare.GetValueOrDefault() - (_termsResults.SecSiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupRent.GetValueOrDefault());
                                        _termsResults.SecSiteGroupRent = _termsResults.SecSiteGroupRentShouldGet.GetValueOrDefault();
                                    }
                                    else //to zero
                                    {
                                        _termsResults.SecSiteGroupRent = _termsResults.SecSiteGroupRent.GetValueOrDefault() + _termsResults.SiteShare.GetValueOrDefault();
                                        _termsResults.SiteShare = 0;
                                    }

                                    //if the SiteGroup still needs a shortfall guarantor...
                                    if (_termsResults.SecSiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupRent.GetValueOrDefault() > 0)
                                    {
                                        //take remaining value off site rent
                                        if (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //do nothing - already got the shortfall
                                        { }
                                        else if (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER)   //do nothing - already got the shortfall
                                        { }
                                        else if (_termsResults.SiteRent.GetValueOrDefault() > (_termsResults.SecSiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupRent.GetValueOrDefault()))   //Has to be to zero
                                        {
                                            _termsResults.SiteRent = _termsResults.SiteRent.GetValueOrDefault() - (_termsResults.SecSiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupRent.GetValueOrDefault());
                                            _termsResults.SecSiteGroupRent = _termsResults.SecSiteGroupRentShouldGet.GetValueOrDefault();
                                        }
                                        else //to zero
                                        {
                                            _termsResults.SecSiteGroupRent = _termsResults.SecSiteGroupRent.GetValueOrDefault() + _termsResults.SiteRent.GetValueOrDefault();
                                            _termsResults.SiteRent = 0;
                                        }
                                    }
                                }
                                break;
                            case TERMS_MIN:
                                if (_termsInfo.SecGroupValueGuaranteed.GetValueOrDefault())
                                {
                                    if (_termsInfo.SecGroupValue.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault() > 0) //if the SiteGroup needs a shortfall guarantor for the min
                                    {
                                        if (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //if the site can cover the shortfall out of shares then
                                        {
                                            //take a % of shortfall from Site
                                            //find out if the site is the only percentage guarantor left
                                            if ((_termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SupplierUse.GetValueOrDefault() > _termsInfo.SiteUse.GetValueOrDefault())
                                                || (_termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.GroupUse.GetValueOrDefault() > _termsInfo.SiteUse.GetValueOrDefault()))
                                            {
                                                temp = _termsInfo.SiteGuarantorPercentage.GetValueOrDefault() / 100;
                                                temp = temp * (_termsInfo.SecGroupValue.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault());
                                                _termsResults.SiteShare = _termsResults.SiteShare.GetValueOrDefault() - Format2DPs(temp.GetValueOrDefault());
                                                _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare + Format2DPs(temp.GetValueOrDefault());
                                                temp = 0;
                                            }
                                            else //there is no other shortfall guarantor, therefore the remainder is to be paid to bring up to the min
                                            {
                                                _termsResults.SiteShare = _termsResults.SiteShare.GetValueOrDefault() - (_termsInfo.SecGroupValue.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault());
                                                _termsResults.SecSiteGroupShare = _termsInfo.SecGroupValue.GetValueOrDefault();
                                            }
                                        }
                                        else if (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER ||
                                                    (_termsResults.SiteShare.GetValueOrDefault() > (_termsInfo.SecGroupValue.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault())))
                                        {
                                            _termsResults.SiteShare = _termsResults.SiteShare.GetValueOrDefault() - (_termsInfo.SecGroupValue.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault());
                                            _termsResults.SecSiteGroupShare = _termsInfo.SecGroupValue.GetValueOrDefault();
                                        }
                                        else
                                        {
                                            //site shares cover min until they get to zero
                                            _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() + _termsResults.SiteShare.GetValueOrDefault();
                                            _termsResults.SiteShare = 0;
                                        }
                                    }

                                    //if there is still a shortfall on the minimum
                                    if (_termsInfo.SecGroupValue.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault() > 0)
                                    {
                                        //now look at the site's rent
                                        if (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //shortfall is already taken care of
                                        { }
                                        else if (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER)  //do nothing - already got the shortfall
                                        { }
                                        else if (_termsResults.SiteRent.GetValueOrDefault() > (_termsInfo.SecGroupValue.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault())) //to zero
                                        {
                                            _termsResults.SiteRent = _termsResults.SiteRent.GetValueOrDefault() - (_termsInfo.SecGroupValue.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault());
                                            _termsResults.SecSiteGroupShare = _termsInfo.SecGroupValue.GetValueOrDefault();
                                        }
                                        else //site shares cover min until they get to zero
                                        {
                                            _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() + _termsResults.SiteRent.GetValueOrDefault();
                                            _termsResults.SiteRent = 0;
                                        }
                                    }
                                }

                                //Now check if the share is guaranteed
                                if (_termsInfo.SecGroupShareGuaranteed.GetValueOrDefault())
                                {
                                    if (_termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault() > 0) //if the SiteGroup needs a shortfall guarantor for the min
                                    {
                                        if (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //if the site can cover the shortfall out of shares then
                                        {
                                            //take a % of shortfall from Site
                                            //find out if the site is the only percentage guarantor left
                                            if ((_termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SupplierUse.GetValueOrDefault() > _termsInfo.SiteUse.GetValueOrDefault())
                                                || (_termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.GroupUse.GetValueOrDefault() > _termsInfo.SiteUse.GetValueOrDefault()))
                                            {
                                                temp = _termsInfo.SiteGuarantorPercentage.GetValueOrDefault() / 100;
                                                temp = temp * (_termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault());
                                                _termsResults.SiteShare = _termsResults.SiteShare.GetValueOrDefault() - Format2DPs(temp.GetValueOrDefault());
                                                _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() + Format2DPs(temp.GetValueOrDefault());
                                                temp = 0;
                                            }
                                            else //there is no other shortfall guarantor, therefore the remainder is to be paid to bring up to the min
                                            {
                                                _termsResults.SiteShare = _termsResults.SiteShare.GetValueOrDefault() - (_termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault());
                                                _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault();
                                            }
                                        }
                                        else if (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER ||
                                                    (_termsResults.SiteShare.GetValueOrDefault() > (_termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault())))
                                        {
                                            _termsResults.SiteShare = _termsResults.SiteShare.GetValueOrDefault() - (_termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault());
                                            _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault();
                                        }
                                        else//site shares cover min until they get to zero
                                        {
                                            _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() + _termsResults.SiteShare.GetValueOrDefault();
                                            _termsResults.SiteShare = 0;
                                        }
                                    }

                                    //if there is still a shortfall on the minimum
                                    if (_termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault() > 0)
                                    {
                                        //now look at the site's rent
                                        if (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //shortfall is already taken care of
                                        { }
                                        else if (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER)  //do nothing - already got the shortfall
                                        { }
                                        else if (_termsResults.SiteRent.GetValueOrDefault() > (_termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault())) //to zero
                                        {
                                            _termsResults.SiteRent = _termsResults.SiteRent.GetValueOrDefault() - (_termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault());
                                            _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault();
                                        }
                                        else //site shares cover min until they get to zero
                                        {
                                            _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() + _termsResults.SiteRent.GetValueOrDefault();
                                            _termsResults.SiteRent = 0;
                                        }
                                    }
                                }
                                break;
                            case TERMS_MAX:
                                //the max value can't be guaranteed
                                //so we only have to guarantee the shares
                                if (_termsInfo.SecGroupShareGuaranteed.GetValueOrDefault())
                                {
                                    if (_termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault() > 0) //if the SiteGroup needs a shortfall guarantor for the share
                                    {
                                        if (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //if the site can cover the shortfall out of shares then
                                        {
                                            //take a % of shortfall from Site
                                            //find out if the site is the only percentage guarantor left
                                            if ((_termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SupplierUse.GetValueOrDefault() > _termsInfo.SiteUse.GetValueOrDefault())
                                                || (_termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.GroupUse.GetValueOrDefault() > _termsInfo.SiteUse.GetValueOrDefault()))
                                            {
                                                temp = _termsInfo.SiteGuarantorPercentage.GetValueOrDefault() / 100;
                                                temp = temp * (_termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault());
                                                _termsResults.SiteShare = _termsResults.SiteShare.GetValueOrDefault() - Format2DPs(temp.GetValueOrDefault());
                                                _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() + Format2DPs(temp.GetValueOrDefault());
                                                temp = 0;
                                            }
                                            else //there is no other shortfall guarantor, therefore the remainder is to be paid to bring up to the min
                                            {
                                                _termsResults.SiteShare = _termsResults.SiteShare.GetValueOrDefault() - (_termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault());
                                                _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault();
                                            }
                                        }
                                        else if (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER ||
                                                    (_termsResults.SiteShare.GetValueOrDefault() > (_termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault())))
                                        {
                                            _termsResults.SiteShare = _termsResults.SiteShare.GetValueOrDefault() - (_termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault());
                                            _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault();
                                        }
                                        else
                                        {
                                            //site shares cover min until they get to zero
                                            _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() + _termsResults.SiteShare.GetValueOrDefault();
                                            _termsResults.SiteShare = 0;
                                        }
                                    }

                                    //if there is still a shortfall on the minimum
                                    if (_termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault() > 0)
                                    {
                                        //now look at the site's rent
                                        if (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //shortfall is already taken care of
                                        { }
                                        else if (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER)  //do nothing - already got the shortfall
                                        { }
                                        else if (_termsResults.SiteRent.GetValueOrDefault() > (_termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault())) //to zero
                                        {
                                            _termsResults.SiteRent = _termsResults.SiteRent.GetValueOrDefault() - (_termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault());
                                            _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault();
                                        }
                                        else //site shares cover min until they get to zero
                                        {
                                            _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() + _termsResults.SiteRent.GetValueOrDefault();
                                            _termsResults.SiteRent = 0;
                                        }
                                    }
                                }
                                break;
                            case TERMS_FRONT_MONEY:
                                if (_termsInfo.SecGroupValueGuaranteed.GetValueOrDefault())
                                {
                                    if (_termsResults.SecSiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupRent.GetValueOrDefault() > 0) //if the SiteGroup needs a shortfall guarantor for the rent
                                    {
                                        if (_termsInfo.SiteGuarantor == TERMS_PERCENTAGE) //if the site can cover the shortfall out of shares then
                                        {
                                            //take a % of shortfall from Site
                                            //find out if the site is the only percentage guarantor left
                                            if ((_termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SupplierUse.GetValueOrDefault() > _termsInfo.SiteUse.GetValueOrDefault())
                                                || (_termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.GroupUse.GetValueOrDefault() > _termsInfo.SiteUse.GetValueOrDefault()))
                                            {
                                                temp = _termsInfo.SiteGuarantorPercentage.GetValueOrDefault() / 100;
                                                temp = temp * (_termsResults.SecSiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupRent.GetValueOrDefault());
                                                _termsResults.SiteShare = _termsResults.SiteShare.GetValueOrDefault() - Format2DPs(temp.GetValueOrDefault());
                                                _termsResults.SecSiteGroupRent = _termsResults.SecSiteGroupRent.GetValueOrDefault() + Format2DPs(temp.GetValueOrDefault());
                                                temp = 0;
                                            }
                                            else //there is no other shortfall guarantor, therefore the remainder is to be paid to bring up to the min
                                            {
                                                _termsResults.SiteShare = _termsResults.SiteShare.GetValueOrDefault() - (_termsResults.SecSiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupRent.GetValueOrDefault());
                                                _termsResults.SecSiteGroupRent = _termsResults.SecSiteGroupRentShouldGet.GetValueOrDefault();
                                            }
                                        }
                                        else if (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER ||
                                                    (_termsResults.SiteShare.GetValueOrDefault() > (_termsResults.SecSiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupRent.GetValueOrDefault())))
                                        {
                                            _termsResults.SiteShare = _termsResults.SiteShare.GetValueOrDefault() - (_termsResults.SecSiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupRent.GetValueOrDefault());
                                            _termsResults.SecSiteGroupRent = _termsResults.SecSiteGroupRentShouldGet.GetValueOrDefault();
                                        }
                                        else
                                        {
                                            //site shares cover min until they get to zero
                                            _termsResults.SecSiteGroupRent = _termsResults.SecSiteGroupRent.GetValueOrDefault() + _termsResults.SiteShare.GetValueOrDefault();
                                            _termsResults.SiteShare = 0;
                                        }
                                    }

                                    //if there is still a shortfall on the minimum
                                    if (_termsResults.SecSiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupRent.GetValueOrDefault() > 0)
                                    {
                                        //now look at the site's rent
                                        if (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //shortfall is already taken care of
                                        { }
                                        else if (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER)  //do nothing - already got the shortfall
                                        { }
                                        else if (_termsResults.SiteRent.GetValueOrDefault() > (_termsResults.SecSiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupRent.GetValueOrDefault())) //to zero
                                        {
                                            _termsResults.SiteRent = _termsResults.SiteRent.GetValueOrDefault() - (_termsResults.SecSiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupRent.GetValueOrDefault());
                                            _termsResults.SecSiteGroupRent = _termsResults.SecSiteGroupRentShouldGet.GetValueOrDefault();
                                        }
                                        else //site shares cover min until they get to zero
                                        {
                                            _termsResults.SecSiteGroupRent = _termsResults.SecSiteGroupRent.GetValueOrDefault() + _termsResults.SiteRent.GetValueOrDefault();
                                            _termsResults.SiteRent = 0;
                                        }
                                    }
                                }

                                //Now check if the share is guaranteed
                                if (_termsInfo.SecGroupShareGuaranteed.GetValueOrDefault())
                                {
                                    if (_termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault() > 0) //if the SiteGroup needs a shortfall guarantor for the min
                                    {
                                        if (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //if the site can cover the shortfall out of shares then
                                        {
                                            //take a % of shortfall from Site
                                            //find out if the site is the only percentage guarantor left
                                            if ((_termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SupplierUse.GetValueOrDefault() > _termsInfo.SiteUse.GetValueOrDefault())
                                                || (_termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.GroupUse.GetValueOrDefault() > _termsInfo.SiteUse.GetValueOrDefault()))
                                            {
                                                temp = _termsInfo.SiteGuarantorPercentage.GetValueOrDefault() / 100;
                                                temp = temp * (_termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault());
                                                _termsResults.SiteShare = _termsResults.SiteShare.GetValueOrDefault() - Format2DPs(temp.GetValueOrDefault());
                                                _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() + Format2DPs(temp.GetValueOrDefault());
                                                temp = 0;
                                            }
                                            else //there is no other shortfall guarantor, therefore the remainder is to be paid to bring up to the min
                                            {
                                                _termsResults.SiteShare = _termsResults.SiteShare.GetValueOrDefault() - (_termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault());
                                                _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault();
                                            }
                                        }
                                        else if (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER ||
                                                    (_termsResults.SiteShare.GetValueOrDefault() > (_termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault())))
                                        {
                                            _termsResults.SiteShare = _termsResults.SiteShare.GetValueOrDefault() - (_termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault());
                                            _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault();
                                        }
                                        else
                                        {
                                            //site shares cover min until they get to zero
                                            _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() + _termsResults.SiteShare.GetValueOrDefault();
                                            _termsResults.SiteShare = 0;
                                        }
                                    }

                                    //if there is still a shortfall on the minimum
                                    //take it out of the site's rent
                                    if (_termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault() > 0)
                                    {
                                        //now look at the site's rent
                                        if (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //shortfall is already taken care of
                                        { }
                                        else if (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER)  //do nothing - already got the shortfall
                                        { }
                                        else if (_termsResults.SiteRent.GetValueOrDefault() > (_termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault())) //to zero
                                        {
                                            _termsResults.SiteRent = _termsResults.SiteRent.GetValueOrDefault() - (_termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault());
                                            _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault();
                                        }
                                        else //site shares cover min until they get to zero
                                        {
                                            _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() + _termsResults.SiteRent.GetValueOrDefault();
                                            _termsResults.SiteRent = 0;
                                        }
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void UseSecSiteGroupForSiteGroupShortfall()
        {
            float? temp = 0;

            try
            {
                LogManager.WriteLog("Inside UseSecSiteGroupForSiteGroupShortfall...", LogManager.enumLogLevel.Info);

                switch (_termsInfo.SecGroupGuarantor.GetValueOrDefault())
                {
                    case TERMS_PRIMARY_TO_ZERO:
                    case TERMS_PRIMARY_FULL:
                    case TERMS_SECONDARY_TO_ZERO:
                    case TERMS_SECONDARY_REMAINDER:
                    case TERMS_PERCENTAGE:
                        switch (_termsInfo.GroupType.GetValueOrDefault())
                        {
                            case TERMS_RENT:
                                //if the rent is guaranteed
                                if (_termsInfo.GroupValueGuaranteed.GetValueOrDefault())
                                {
                                    //if the SiteGroup needs a shortfall guarantor...
                                    if (_termsResults.SecSiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupRent.GetValueOrDefault() > 0)
                                    {
                                        //take value off site shares
                                        if (_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE)
                                        {
                                            //take a % of the shortfall from the site shares

                                            //find out if the site is the only percentage guarantor left
                                            if ((_termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SupplierUse.GetValueOrDefault() > _termsInfo.SecGroupUse)
                                                || (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SiteUse.GetValueOrDefault() > _termsInfo.SecGroupUse))
                                            {
                                                temp = _termsInfo.SecGroupGuarantorPercentage.GetValueOrDefault() / 100;
                                                temp = temp * (_termsResults.SecSiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupRent.GetValueOrDefault());
                                                _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() - Format2DPs(temp.GetValueOrDefault());
                                                _termsResults.SecSiteGroupRent = _termsResults.SecSiteGroupRent.GetValueOrDefault() + Format2DPs(temp.GetValueOrDefault());
                                                temp = 0;
                                            }
                                            else //there is no other shortfall guarantor, therefore the remainder is to be paid
                                            {
                                                _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() - (_termsResults.SecSiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupRent.GetValueOrDefault());
                                                _termsResults.SecSiteGroupRent = _termsResults.SecSiteGroupRentShouldGet.GetValueOrDefault();
                                            }
                                        }
                                    }
                                    else if (_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER || _termsResults.SecSiteGroupShare.GetValueOrDefault() > (_termsResults.SecSiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupRent.GetValueOrDefault()))
                                    {
                                        _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() - (_termsResults.SecSiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupRent.GetValueOrDefault());
                                        _termsResults.SecSiteGroupRent = _termsResults.SecSiteGroupRentShouldGet.GetValueOrDefault();
                                    }
                                    else //to zero
                                    {
                                        _termsResults.SecSiteGroupRent = _termsResults.SecSiteGroupRent.GetValueOrDefault() + _termsResults.SecSiteGroupShare.GetValueOrDefault();
                                        _termsResults.SecSiteGroupShare = 0;
                                    }

                                    //if the SiteGroup still needs a shortfall guarantor...
                                    if (_termsResults.SecSiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupRent.GetValueOrDefault() > 0)
                                    {
                                        //take remaining value off site rent
                                        if (_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //do nothing - already got the shortfall
                                        { }
                                        else if (_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER)   //do nothing - already got the shortfall
                                        { }
                                        else if (_termsResults.SecSiteGroupRent.GetValueOrDefault() > (_termsResults.SecSiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupRent.GetValueOrDefault()))   //Has to be to zero
                                        {
                                            _termsResults.SecSiteGroupRent = _termsResults.SecSiteGroupRent.GetValueOrDefault() - (_termsResults.SecSiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupRent.GetValueOrDefault());
                                            _termsResults.SecSiteGroupRent = _termsResults.SecSiteGroupRentShouldGet.GetValueOrDefault();
                                        }
                                        else //to zero
                                        {
                                            _termsResults.SecSiteGroupRent = _termsResults.SecSiteGroupRent.GetValueOrDefault() + _termsResults.SecSiteGroupRent.GetValueOrDefault();
                                            _termsResults.SecSiteGroupRent = 0;
                                        }
                                    }
                                }
                                break;
                            case TERMS_MIN:
                                if (_termsInfo.GroupValueGuaranteed.GetValueOrDefault())
                                {
                                    if (_termsInfo.GroupValue.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault() > 0) //if the SiteGroup needs a shortfall guarantor for the min
                                    {
                                        if (_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //if the site can cover the shortfall out of shares then
                                        {
                                            //take a % of shortfall from Site
                                            //find out if the site is the only percentage guarantor left
                                            if ((_termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SupplierUse.GetValueOrDefault() > _termsInfo.SecGroupUse.GetValueOrDefault())
                                                || (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SiteUse.GetValueOrDefault() > _termsInfo.SecGroupUse.GetValueOrDefault()))
                                            {
                                                temp = _termsInfo.SecGroupGuarantorPercentage.GetValueOrDefault() / 100;
                                                temp = temp * (_termsInfo.SecGroupValue.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault());
                                                _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() - Format2DPs(temp.GetValueOrDefault());
                                                _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() + Format2DPs(temp.GetValueOrDefault());
                                                temp = 0;
                                            }
                                            else //there is no other shortfall guarantor, therefore the remainder is to be paid to bring up to the min
                                            {
                                                _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() - (_termsInfo.GroupValue.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault());
                                                _termsResults.SecSiteGroupShare = _termsInfo.GroupValue.GetValueOrDefault();
                                            }
                                        }
                                        else if (_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER ||
                                                    (_termsResults.SecSiteGroupShare.GetValueOrDefault() > (_termsInfo.GroupValue.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault())))
                                        {
                                            _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() - (_termsInfo.GroupValue.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault());
                                            _termsResults.SecSiteGroupShare = _termsInfo.GroupValue.GetValueOrDefault();
                                        }
                                        else
                                        {
                                            //site shares cover min until they get to zero
                                            _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() + _termsResults.SecSiteGroupShare.GetValueOrDefault();
                                            _termsResults.SecSiteGroupShare = 0;
                                        }
                                    }

                                    //if there is still a shortfall on the minimum
                                    if (_termsInfo.GroupValue.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault() > 0)
                                    {
                                        //now look at the site's rent
                                        if (_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //shortfall is already taken care of
                                        { }
                                        else if (_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER)  //do nothing - already got the shortfall
                                        { }
                                        else if (_termsResults.SecSiteGroupRent.GetValueOrDefault() > (_termsInfo.GroupValue.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault())) //to zero
                                        {
                                            _termsResults.SecSiteGroupRent = _termsResults.SecSiteGroupRent.GetValueOrDefault() - (_termsInfo.GroupValue.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault());
                                            _termsResults.SecSiteGroupShare = _termsInfo.GroupValue.GetValueOrDefault();
                                        }
                                        else //site shares cover min until they get to zero
                                        {
                                            _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() + _termsResults.SecSiteGroupRent.GetValueOrDefault();
                                            _termsResults.SecSiteGroupRent = 0;
                                        }
                                    }
                                }

                                //Now check if the share is guaranteed
                                if (_termsInfo.GroupShareGuaranteed.GetValueOrDefault())
                                {
                                    if (_termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault() > 0) //if the SiteGroup needs a shortfall guarantor for the min
                                    {
                                        if (_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //if the site can cover the shortfall out of shares then
                                        {
                                            //take a % of shortfall from Site
                                            //find out if the site is the only percentage guarantor left
                                            if ((_termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SupplierUse.GetValueOrDefault() > _termsInfo.SecGroupUse.GetValueOrDefault())
                                                || (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SiteUse.GetValueOrDefault() > _termsInfo.SecGroupUse.GetValueOrDefault()))
                                            {
                                                temp = _termsInfo.SecGroupGuarantorPercentage.GetValueOrDefault() / 100;
                                                temp = temp * (_termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault());
                                                _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() - Format2DPs(temp.GetValueOrDefault());
                                                _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() + Format2DPs(temp.GetValueOrDefault());
                                                temp = 0;
                                            }
                                            else //there is no other shortfall guarantor, therefore the remainder is to be paid to bring up to the min
                                            {
                                                _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() - (_termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault());
                                                _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault();
                                            }
                                        }
                                        else if (_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER ||
                                                    (_termsResults.SecSiteGroupShare.GetValueOrDefault() > (_termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault())))
                                        {
                                            _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() - (_termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault());
                                            _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault();
                                        }
                                        else//site shares cover min until they get to zero
                                        {
                                            _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() + _termsResults.SecSiteGroupShare.GetValueOrDefault();
                                            _termsResults.SecSiteGroupShare = 0;
                                        }
                                    }

                                    //if there is still a shortfall on the minimum
                                    if (_termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault() > 0)
                                    {
                                        //now look at the site's rent
                                        if (_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //shortfall is already taken care of
                                        { }
                                        else if (_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER)  //do nothing - already got the shortfall
                                        { }
                                        else if (_termsResults.SecSiteGroupRent.GetValueOrDefault() > (_termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault())) //to zero
                                        {
                                            _termsResults.SecSiteGroupRent = _termsResults.SecSiteGroupRent.GetValueOrDefault() - (_termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault());
                                            _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault();
                                        }
                                        else //site shares cover min until they get to zero
                                        {
                                            _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() + _termsResults.SecSiteGroupRent.GetValueOrDefault();
                                            _termsResults.SecSiteGroupRent = 0;
                                        }
                                    }
                                }
                                break;
                            case TERMS_MAX:
                                //the max value can't be guaranteed
                                //so we only have to guarantee the shares
                                if (_termsInfo.GroupShareGuaranteed.GetValueOrDefault())
                                {
                                    if (_termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault() > 0) //if the SiteGroup needs a shortfall guarantor for the share
                                    {
                                        if (_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //if the site can cover the shortfall out of shares then
                                        {
                                            //take a % of shortfall from Site
                                            //find out if the site is the only percentage guarantor left
                                            if ((_termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SupplierUse.GetValueOrDefault() > _termsInfo.SecGroupUse.GetValueOrDefault())
                                                || (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SiteUse.GetValueOrDefault() > _termsInfo.SecGroupUse.GetValueOrDefault()))
                                            {
                                                temp = _termsInfo.SecGroupGuarantorPercentage.GetValueOrDefault() / 100;
                                                temp = temp * (_termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault());
                                                _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() - Format2DPs(temp.GetValueOrDefault());
                                                _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() + Format2DPs(temp.GetValueOrDefault());
                                                temp = 0;
                                            }
                                            else //there is no other shortfall guarantor, therefore the remainder is to be paid to bring up to the min
                                            {
                                                _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() - (_termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault());
                                                _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault();
                                            }
                                        }
                                        else if (_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER ||
                                                    (_termsResults.SecSiteGroupShare.GetValueOrDefault() > (_termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault())))
                                        {
                                            _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() - (_termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault());
                                            _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault();
                                        }
                                        else
                                        {
                                            //site shares cover min until they get to zero
                                            _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() + _termsResults.SecSiteGroupShare.GetValueOrDefault();
                                            _termsResults.SecSiteGroupShare = 0;
                                        }
                                    }

                                    //if there is still a shortfall on the minimum
                                    if (_termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault() > 0)
                                    {
                                        //now look at the site's rent
                                        if (_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //shortfall is already taken care of
                                        { }
                                        else if (_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER)  //do nothing - already got the shortfall
                                        { }
                                        else if (_termsResults.SecSiteGroupRent.GetValueOrDefault() > (_termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault())) //to zero
                                        {
                                            _termsResults.SecSiteGroupRent = _termsResults.SecSiteGroupRent.GetValueOrDefault() - (_termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault());
                                            _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault();
                                        }
                                        else //site shares cover min until they get to zero
                                        {
                                            _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() + _termsResults.SecSiteGroupRent.GetValueOrDefault();
                                            _termsResults.SecSiteGroupRent = 0;
                                        }
                                    }
                                }
                                break;
                            case TERMS_FRONT_MONEY:
                                if (_termsInfo.GroupValueGuaranteed.GetValueOrDefault())
                                {
                                    if (_termsResults.SecSiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupRent.GetValueOrDefault() > 0) //if the SiteGroup needs a shortfall guarantor for the rent
                                    {
                                        if (_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //if the site can cover the shortfall out of shares then
                                        {
                                            //take a % of shortfall from Site
                                            //find out if the site is the only percentage guarantor left
                                            if ((_termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SupplierUse.GetValueOrDefault() > _termsInfo.SecGroupUse.GetValueOrDefault())
                                                || (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SiteUse.GetValueOrDefault() > _termsInfo.SecGroupUse.GetValueOrDefault()))
                                            {
                                                temp = _termsInfo.SecGroupGuarantorPercentage.GetValueOrDefault() / 100;
                                                temp = temp * (_termsResults.SecSiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupRent.GetValueOrDefault());
                                                _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() - Format2DPs(temp.GetValueOrDefault());
                                                _termsResults.SecSiteGroupRent = _termsResults.SecSiteGroupRent.GetValueOrDefault() + Format2DPs(temp.GetValueOrDefault());
                                                temp = 0;
                                            }
                                            else //there is no other shortfall guarantor, therefore the remainder is to be paid to bring up to the min
                                            {
                                                _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() - (_termsResults.SecSiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupRent.GetValueOrDefault());
                                                _termsResults.SecSiteGroupRent = _termsResults.SecSiteGroupRentShouldGet.GetValueOrDefault();
                                            }
                                        }
                                        else if (_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER ||
                                                    (_termsResults.SecSiteGroupShare.GetValueOrDefault() > (_termsResults.SecSiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupRent.GetValueOrDefault())))
                                        {
                                            _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() - (_termsResults.SecSiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupRent.GetValueOrDefault());
                                            _termsResults.SecSiteGroupRent = _termsResults.SecSiteGroupRentShouldGet.GetValueOrDefault();
                                        }
                                        else
                                        {
                                            //site shares cover min until they get to zero
                                            _termsResults.SecSiteGroupRent = _termsResults.SecSiteGroupRent.GetValueOrDefault() + _termsResults.SecSiteGroupShare.GetValueOrDefault();
                                            _termsResults.SecSiteGroupShare = 0;
                                        }
                                    }

                                    //if there is still a shortfall on the minimum
                                    if (_termsResults.SecSiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupRent.GetValueOrDefault() > 0)
                                    {
                                        //now look at the site's rent
                                        if (_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //shortfall is already taken care of
                                        { }
                                        else if (_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER)  //do nothing - already got the shortfall
                                        { }
                                        else if (_termsResults.SecSiteGroupRent.GetValueOrDefault() > (_termsResults.SecSiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupRent.GetValueOrDefault())) //to zero
                                        {
                                            _termsResults.SecSiteGroupRent = _termsResults.SecSiteGroupRent.GetValueOrDefault() - (_termsResults.SecSiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupRent.GetValueOrDefault());
                                            _termsResults.SecSiteGroupRent = _termsResults.SecSiteGroupRentShouldGet.GetValueOrDefault();
                                        }
                                        else //site shares cover min until they get to zero
                                        {
                                            _termsResults.SecSiteGroupRent = _termsResults.SecSiteGroupRent.GetValueOrDefault() + _termsResults.SecSiteGroupRent.GetValueOrDefault();
                                            _termsResults.SecSiteGroupRent = 0;
                                        }
                                    }
                                }

                                //Now check if the share is guaranteed
                                if (_termsInfo.GroupShareGuaranteed.GetValueOrDefault())
                                {
                                    if (_termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault() > 0) //if the SiteGroup needs a shortfall guarantor for the min
                                    {
                                        if (_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //if the site can cover the shortfall out of shares then
                                        {
                                            //take a % of shortfall from Site
                                            //find out if the site is the only percentage guarantor left
                                            if ((_termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SupplierUse.GetValueOrDefault() > _termsInfo.SecGroupUse.GetValueOrDefault())
                                                || (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SiteUse.GetValueOrDefault() > _termsInfo.SecGroupUse.GetValueOrDefault()))
                                            {
                                                temp = _termsInfo.SecGroupGuarantorPercentage.GetValueOrDefault() / 100;
                                                temp = temp * (_termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault());
                                                _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() - Format2DPs(temp.GetValueOrDefault());
                                                _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() + Format2DPs(temp.GetValueOrDefault());
                                                temp = 0;
                                            }
                                            else //there is no other shortfall guarantor, therefore the remainder is to be paid to bring up to the min
                                            {
                                                _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() - (_termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault());
                                                _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault();
                                            }
                                        }
                                        else if (_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER ||
                                                    (_termsResults.SecSiteGroupShare.GetValueOrDefault() > (_termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault())))
                                        {
                                            _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() - (_termsResults.SecSiteGroupShareShouldGet - _termsResults.SecSiteGroupShare.GetValueOrDefault());
                                            _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault();
                                        }
                                        else
                                        {
                                            //site shares cover min until they get to zero
                                            _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() + _termsResults.SecSiteGroupShare.GetValueOrDefault();
                                            _termsResults.SecSiteGroupShare = 0;
                                        }
                                    }

                                    //if there is still a shortfall on the minimum
                                    //take it out of the site's rent
                                    if (_termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault() > 0)
                                    {
                                        //now look at the site's rent
                                        if (_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //shortfall is already taken care of
                                        { }
                                        else if (_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER)  //do nothing - already got the shortfall
                                        { }
                                        else if (_termsResults.SecSiteGroupRent.GetValueOrDefault() > (_termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault())) //to zero
                                        {
                                            _termsResults.SecSiteGroupRent = _termsResults.SecSiteGroupRent.GetValueOrDefault() - (_termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault());
                                            _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault();
                                        }
                                        else //site shares cover min until they get to zero
                                        {
                                            _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() + _termsResults.SecSiteGroupRent.GetValueOrDefault();
                                            _termsResults.SecSiteGroupRent = 0;
                                        }
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
            }
            catch(Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void UseSupplierForSiteGroupShortfall()
        {
            float? temp = 0;

            try
            {
                LogManager.WriteLog("Inside UseSupplierForSiteGroupShortfall...", LogManager.enumLogLevel.Info);

                switch (_termsInfo.SupplierGuarantor.GetValueOrDefault())
                {
                    case TERMS_PRIMARY_TO_ZERO:
                    case TERMS_PRIMARY_FULL:
                    case TERMS_SECONDARY_TO_ZERO:
                    case TERMS_SECONDARY_REMAINDER:
                    case TERMS_PERCENTAGE:
                        switch (_termsInfo.GroupType.GetValueOrDefault())
                        {
                            case TERMS_RENT:
                                //if the rent is guaranteed
                                if (_termsInfo.GroupValueGuaranteed.GetValueOrDefault())
                                {
                                    //if the SiteGroup needs a shortfall guarantor...
                                    if (_termsResults.SiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SiteGroupRent.GetValueOrDefault() > 0)
                                    {
                                        //take value off site shares
                                        if (_termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE)
                                        {
                                            //take a % of the shortfall from the site shares

                                            //find out if the site is the only percentage guarantor left
                                            if ((_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SecGroupUse.GetValueOrDefault() > _termsInfo.SupplierUse.GetValueOrDefault())
                                                || (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SiteUse.GetValueOrDefault() > _termsInfo.SupplierUse.GetValueOrDefault()))
                                            {
                                                temp = _termsInfo.SupplierGuarantorPercentage.GetValueOrDefault() / 100;
                                                temp = temp * (_termsResults.SiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SiteGroupRent.GetValueOrDefault());
                                                _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() - Format2DPs(temp.GetValueOrDefault());
                                                _termsResults.SiteGroupRent = _termsResults.SiteGroupRent.GetValueOrDefault() + Format2DPs(temp.GetValueOrDefault());
                                                temp = 0;
                                            }
                                            else //there is no other shortfall guarantor, therefore the remainder is to be paid
                                            {
                                                _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() - (_termsResults.SiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SiteGroupRent.GetValueOrDefault());
                                                _termsResults.SiteGroupRent = _termsResults.SiteGroupRentShouldGet.GetValueOrDefault();
                                            }
                                        }
                                    }
                                    else if (_termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER || _termsResults.SupplierShare.GetValueOrDefault() > (_termsResults.SiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SiteGroupRent.GetValueOrDefault()))
                                    {
                                        _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() - (_termsResults.SiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SiteGroupRent.GetValueOrDefault());
                                        _termsResults.SiteGroupRent = _termsResults.SiteGroupRentShouldGet.GetValueOrDefault();
                                    }
                                    else //to zero
                                    {
                                        _termsResults.SiteGroupRent = _termsResults.SiteGroupRent.GetValueOrDefault() + _termsResults.SupplierShare.GetValueOrDefault();
                                        _termsResults.SupplierShare = 0;
                                    }

                                    //if the SiteGroup still needs a shortfall guarantor...
                                    if (_termsResults.SiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SiteGroupRent.GetValueOrDefault() > 0)
                                    {
                                        //take remaining value off site rent
                                        if (_termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //do nothing - already got the shortfall
                                        { }
                                        else if (_termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER)   //do nothing - already got the shortfall
                                        { }
                                        else if (_termsResults.SupplierRent.GetValueOrDefault() > (_termsResults.SiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SiteGroupRent.GetValueOrDefault()))   //Has to be to zero
                                        {
                                            _termsResults.SupplierRent = _termsResults.SupplierRent.GetValueOrDefault() - (_termsResults.SiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SiteGroupRent.GetValueOrDefault());
                                            _termsResults.SiteGroupRent = _termsResults.SiteGroupRentShouldGet.GetValueOrDefault();
                                        }
                                        else //to zero
                                        {
                                            _termsResults.SiteGroupRent = _termsResults.SiteGroupRent.GetValueOrDefault() + _termsResults.SupplierRent.GetValueOrDefault();
                                            _termsResults.SupplierRent = 0;
                                        }
                                    }
                                }
                                break;
                            case TERMS_MIN:
                                if (_termsInfo.GroupValueGuaranteed.GetValueOrDefault())
                                {
                                    if (_termsInfo.GroupValue.GetValueOrDefault() - _termsResults.SiteGroupShare.GetValueOrDefault() > 0) //if the SiteGroup needs a shortfall guarantor for the min
                                    {
                                        if (_termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //if the site can cover the shortfall out of shares then
                                        {
                                            //take a % of shortfall from Site
                                            //find out if the site is the only percentage guarantor left
                                            if ((_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SecGroupUse.GetValueOrDefault() > _termsInfo.SupplierUse.GetValueOrDefault())
                                                || (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SiteUse.GetValueOrDefault() > _termsInfo.SupplierUse.GetValueOrDefault()))
                                            {
                                                temp = _termsInfo.SupplierGuarantorPercentage.GetValueOrDefault() / 100;
                                                temp = temp * (_termsInfo.GroupValue.GetValueOrDefault() - _termsResults.SiteGroupShare.GetValueOrDefault());
                                                _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() - Format2DPs(temp.GetValueOrDefault());
                                                _termsResults.SiteGroupShare = _termsResults.SiteGroupShare.GetValueOrDefault() + Format2DPs(temp.GetValueOrDefault());
                                                temp = 0;
                                            }
                                            else //there is no other shortfall guarantor, therefore the remainder is to be paid to bring up to the min
                                            {
                                                _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() - (_termsInfo.GroupValue.GetValueOrDefault() - _termsResults.SiteGroupShare.GetValueOrDefault());
                                                _termsResults.SiteGroupShare = _termsInfo.GroupValue.GetValueOrDefault();
                                            }
                                        }
                                        else if (_termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER ||
                                                    (_termsResults.SupplierShare.GetValueOrDefault() > (_termsInfo.GroupValue.GetValueOrDefault() - _termsResults.SiteGroupShare.GetValueOrDefault())))
                                        {
                                            _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() - (_termsInfo.GroupValue.GetValueOrDefault() - _termsResults.SiteGroupShare.GetValueOrDefault());
                                            _termsResults.SiteGroupShare = _termsInfo.GroupValue.GetValueOrDefault();
                                        }
                                        else
                                        {
                                            //site shares cover min until they get to zero
                                            _termsResults.SiteGroupShare = _termsResults.SiteGroupShare.GetValueOrDefault() + _termsResults.SupplierShare.GetValueOrDefault();
                                            _termsResults.SupplierShare = 0;
                                        }
                                    }

                                    //if there is still a shortfall on the minimum
                                    if (_termsInfo.GroupValue.GetValueOrDefault() - _termsResults.SiteGroupShare.GetValueOrDefault() > 0)
                                    {
                                        //now look at the site's rent
                                        if (_termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //shortfall is already taken care of
                                        { }
                                        else if (_termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER)  //do nothing - already got the shortfall
                                        { }
                                        else if (_termsResults.SupplierRent.GetValueOrDefault() > (_termsInfo.GroupValue.GetValueOrDefault() - _termsResults.SiteGroupShare.GetValueOrDefault())) //to zero
                                        {
                                            _termsResults.SupplierRent = _termsResults.SupplierRent.GetValueOrDefault() - (_termsInfo.GroupValue.GetValueOrDefault() - _termsResults.SiteGroupShare.GetValueOrDefault());
                                            _termsResults.SiteGroupShare = _termsInfo.GroupValue.GetValueOrDefault();
                                        }
                                        else //site shares cover min until they get to zero
                                        {
                                            _termsResults.SiteGroupShare = _termsResults.SiteGroupShare.GetValueOrDefault() + _termsResults.SupplierRent.GetValueOrDefault();
                                            _termsResults.SupplierRent = 0;
                                        }
                                    }
                                }

                                //Now check if the share is guaranteed
                                if (_termsInfo.GroupShareGuaranteed.GetValueOrDefault())
                                {
                                    if (_termsResults.SiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SiteGroupShare.GetValueOrDefault() > 0) //if the SiteGroup needs a shortfall guarantor for the min
                                    {
                                        if (_termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //if the site can cover the shortfall out of shares then
                                        {
                                            //take a % of shortfall from Site
                                            //find out if the site is the only percentage guarantor left
                                            if ((_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SecGroupUse.GetValueOrDefault() > _termsInfo.SupplierUse.GetValueOrDefault())
                                                || (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SiteUse.GetValueOrDefault() > _termsInfo.SupplierUse.GetValueOrDefault()))
                                            {
                                                temp = _termsInfo.SupplierGuarantorPercentage.GetValueOrDefault() / 100;
                                                temp = temp * (_termsResults.SiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SiteGroupShare.GetValueOrDefault());
                                                _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() - Format2DPs(temp.GetValueOrDefault());
                                                _termsResults.SiteGroupShare = _termsResults.SiteGroupShare.GetValueOrDefault() + Format2DPs(temp.GetValueOrDefault());
                                                temp = 0;
                                            }
                                            else //there is no other shortfall guarantor, therefore the remainder is to be paid to bring up to the min
                                            {
                                                _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() - (_termsResults.SiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SiteGroupShare.GetValueOrDefault());
                                                _termsResults.SiteGroupShare = _termsResults.SiteGroupShareShouldGet.GetValueOrDefault();
                                            }
                                        }
                                        else if (_termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER ||
                                                    (_termsResults.SupplierShare.GetValueOrDefault() > (_termsResults.SiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SiteGroupShare.GetValueOrDefault())))
                                        {
                                            _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() - (_termsResults.SiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SiteGroupShare.GetValueOrDefault());
                                            _termsResults.SiteGroupShare = _termsResults.SiteGroupShareShouldGet.GetValueOrDefault();
                                        }
                                        else//site shares cover min until they get to zero
                                        {
                                            _termsResults.SiteGroupShare = _termsResults.SiteGroupShare.GetValueOrDefault() + _termsResults.SupplierShare.GetValueOrDefault();
                                            _termsResults.SupplierShare = 0;
                                        }
                                    }

                                    //if there is still a shortfall on the minimum
                                    if (_termsResults.SiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SiteGroupShare.GetValueOrDefault() > 0)
                                    {
                                        //now look at the site's rent
                                        if (_termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //shortfall is already taken care of
                                        { }
                                        else if (_termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER)  //do nothing - already got the shortfall
                                        { }
                                        else if (_termsResults.SupplierRent.GetValueOrDefault() > (_termsResults.SiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SiteGroupShare.GetValueOrDefault())) //to zero
                                        {
                                            _termsResults.SupplierRent = _termsResults.SupplierRent.GetValueOrDefault() - (_termsResults.SiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SiteGroupShare.GetValueOrDefault());
                                            _termsResults.SiteGroupShare = _termsResults.SiteGroupShareShouldGet.GetValueOrDefault();
                                        }
                                        else //site shares cover min until they get to zero
                                        {
                                            _termsResults.SiteGroupShare = _termsResults.SiteGroupShare.GetValueOrDefault() + _termsResults.SupplierRent.GetValueOrDefault();
                                            _termsResults.SupplierRent = 0;
                                        }
                                    }
                                }
                                break;
                            case TERMS_MAX:
                                //the max value can't be guaranteed
                                //so we only have to guarantee the shares
                                if (_termsInfo.GroupShareGuaranteed.GetValueOrDefault())
                                {
                                    if (_termsResults.SiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SiteGroupShare.GetValueOrDefault() > 0) //if the SiteGroup needs a shortfall guarantor for the share
                                    {
                                        if (_termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //if the site can cover the shortfall out of shares then
                                        {
                                            //take a % of shortfall from Site
                                            //find out if the site is the only percentage guarantor left
                                            if ((_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SecGroupUse.GetValueOrDefault() > _termsInfo.SupplierUse.GetValueOrDefault())
                                                || (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SiteUse.GetValueOrDefault() > _termsInfo.SupplierUse.GetValueOrDefault()))
                                            {
                                                temp = _termsInfo.SupplierGuarantorPercentage.GetValueOrDefault() / 100;
                                                temp = temp * (_termsResults.SiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SiteGroupShare.GetValueOrDefault());
                                                _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() - Format2DPs(temp.GetValueOrDefault());
                                                _termsResults.SiteGroupShare = _termsResults.SiteGroupShare.GetValueOrDefault() + Format2DPs(temp.GetValueOrDefault());
                                                temp = 0;
                                            }
                                            else //there is no other shortfall guarantor, therefore the remainder is to be paid to bring up to the min
                                            {
                                                _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() - (_termsResults.SiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SiteGroupShare.GetValueOrDefault());
                                                _termsResults.SiteGroupShare = _termsResults.SiteGroupShareShouldGet.GetValueOrDefault();
                                            }
                                        }
                                        else if (_termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER ||
                                                    (_termsResults.SupplierShare.GetValueOrDefault() > (_termsResults.SiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SiteGroupShare.GetValueOrDefault())))
                                        {
                                            _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() - (_termsResults.SiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SiteGroupShare.GetValueOrDefault());
                                            _termsResults.SiteGroupShare = _termsResults.SiteGroupShareShouldGet.GetValueOrDefault();
                                        }
                                        else
                                        {
                                            //site shares cover min until they get to zero
                                            _termsResults.SiteGroupShare = _termsResults.SiteGroupShare.GetValueOrDefault() + _termsResults.SupplierShare.GetValueOrDefault();
                                            _termsResults.SupplierShare = 0;
                                        }
                                    }

                                    //if there is still a shortfall on the minimum
                                    if (_termsResults.SiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SiteGroupShare.GetValueOrDefault() > 0)
                                    {
                                        //now look at the site's rent
                                        if (_termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //shortfall is already taken care of
                                        { }
                                        else if (_termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER)  //do nothing - already got the shortfall
                                        { }
                                        else if (_termsResults.SupplierRent.GetValueOrDefault() > (_termsResults.SiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SiteGroupShare.GetValueOrDefault())) //to zero
                                        {
                                            _termsResults.SupplierRent = _termsResults.SupplierRent.GetValueOrDefault() - (_termsResults.SiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SiteGroupShare.GetValueOrDefault());
                                            _termsResults.SiteGroupShare = _termsResults.SiteGroupShareShouldGet.GetValueOrDefault();
                                        }
                                        else //site shares cover min until they get to zero
                                        {
                                            _termsResults.SiteGroupShare = _termsResults.SiteGroupShare.GetValueOrDefault() + _termsResults.SupplierRent.GetValueOrDefault();
                                            _termsResults.SupplierRent = 0;
                                        }
                                    }
                                }
                                break;
                            case TERMS_FRONT_MONEY:
                                if (_termsInfo.GroupValueGuaranteed.GetValueOrDefault())
                                {
                                    if (_termsResults.SiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SiteGroupRent.GetValueOrDefault() > 0) //if the SiteGroup needs a shortfall guarantor for the rent
                                    {
                                        if (_termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //if the site can cover the shortfall out of shares then
                                        {
                                            //take a % of shortfall from Site
                                            //find out if the site is the only percentage guarantor left
                                            if ((_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SecGroupUse.GetValueOrDefault() > _termsInfo.SupplierUse.GetValueOrDefault())
                                                || (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SiteUse.GetValueOrDefault() > _termsInfo.SupplierUse.GetValueOrDefault()))
                                            {
                                                temp = _termsInfo.SupplierGuarantorPercentage.GetValueOrDefault() / 100;
                                                temp = temp * (_termsResults.SiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SiteGroupRent.GetValueOrDefault());
                                                _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() - Format2DPs(temp.GetValueOrDefault());
                                                _termsResults.SiteGroupRent = _termsResults.SiteGroupRent.GetValueOrDefault() + Format2DPs(temp.GetValueOrDefault());
                                                temp = 0;
                                            }
                                            else //there is no other shortfall guarantor, therefore the remainder is to be paid to bring up to the min
                                            {
                                                _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() - (_termsResults.SiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SiteGroupRent.GetValueOrDefault());
                                                _termsResults.SiteGroupRent = _termsResults.SiteGroupRentShouldGet.GetValueOrDefault();
                                            }
                                        }
                                        else if (_termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER ||
                                                    (_termsResults.SupplierShare.GetValueOrDefault() > (_termsResults.SiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SiteGroupRent.GetValueOrDefault())))
                                        {
                                            _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() - (_termsResults.SiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SiteGroupRent.GetValueOrDefault());
                                            _termsResults.SiteGroupRent = _termsResults.SiteGroupRentShouldGet.GetValueOrDefault();
                                        }
                                        else
                                        {
                                            //site shares cover min until they get to zero
                                            _termsResults.SiteGroupRent = _termsResults.SiteGroupRent.GetValueOrDefault() + _termsResults.SupplierShare.GetValueOrDefault();
                                            _termsResults.SupplierShare = 0;
                                        }
                                    }

                                    //if there is still a shortfall on the minimum
                                    if (_termsResults.SiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SiteGroupRent.GetValueOrDefault() > 0)
                                    {
                                        //now look at the site's rent
                                        if (_termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //shortfall is already taken care of
                                        { }
                                        else if (_termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SupplierGuarantor == TERMS_SECONDARY_REMAINDER)  //do nothing - already got the shortfall
                                        { }
                                        else if (_termsResults.SupplierRent.GetValueOrDefault() > (_termsResults.SiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SiteGroupRent.GetValueOrDefault())) //to zero
                                        {
                                            _termsResults.SupplierRent = _termsResults.SupplierRent.GetValueOrDefault() - (_termsResults.SiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SiteGroupRent.GetValueOrDefault());
                                            _termsResults.SiteGroupRent = _termsResults.SiteGroupRentShouldGet.GetValueOrDefault();
                                        }
                                        else //site shares cover min until they get to zero
                                        {
                                            _termsResults.SiteGroupRent = _termsResults.SiteGroupRent.GetValueOrDefault() + _termsResults.SupplierRent.GetValueOrDefault();
                                            _termsResults.SupplierRent = 0;
                                        }
                                    }
                                }

                                //Now check if the share is guaranteed
                                if (_termsInfo.GroupShareGuaranteed.GetValueOrDefault())
                                {
                                    if (_termsResults.SiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SiteGroupShare.GetValueOrDefault() > 0) //if the SiteGroup needs a shortfall guarantor for the min
                                    {
                                        if (_termsInfo.SupplierGuarantor == TERMS_PERCENTAGE) //if the site can cover the shortfall out of shares then
                                        {
                                            //take a % of shortfall from Site
                                            //find out if the site is the only percentage guarantor left
                                            if ((_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SecGroupUse.GetValueOrDefault() > _termsInfo.SupplierUse.GetValueOrDefault())
                                                || (_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SiteUse.GetValueOrDefault() > _termsInfo.SupplierUse.GetValueOrDefault()))
                                            {
                                                temp = _termsInfo.SupplierGuarantorPercentage.GetValueOrDefault() / 100;
                                                temp = temp * (_termsResults.SiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SiteGroupShare.GetValueOrDefault());
                                                _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() - Format2DPs(temp.GetValueOrDefault());
                                                _termsResults.SiteGroupShare = _termsResults.SiteGroupShare.GetValueOrDefault() + Format2DPs(temp.GetValueOrDefault());
                                                temp = 0;
                                            }
                                            else //there is no other shortfall guarantor, therefore the remainder is to be paid to bring up to the min
                                            {
                                                _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() - (_termsResults.SiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SiteGroupShare.GetValueOrDefault());
                                                _termsResults.SiteGroupShare = _termsResults.SiteGroupShareShouldGet.GetValueOrDefault();
                                            }
                                        }
                                        else if (_termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER ||
                                                    (_termsResults.SupplierShare.GetValueOrDefault() > (_termsResults.SiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SiteGroupShare.GetValueOrDefault())))
                                        {
                                            _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() - (_termsResults.SiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SiteGroupShare.GetValueOrDefault());
                                            _termsResults.SiteGroupShare = _termsResults.SiteGroupShareShouldGet.GetValueOrDefault();
                                        }
                                        else
                                        {
                                            //site shares cover min until they get to zero
                                            _termsResults.SiteGroupShare = _termsResults.SiteGroupShare.GetValueOrDefault() + _termsResults.SupplierShare.GetValueOrDefault();
                                            _termsResults.SupplierShare = 0;
                                        }
                                    }

                                    //if there is still a shortfall on the minimum
                                    //take it out of the site's rent
                                    if (_termsResults.SiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SiteGroupShare.GetValueOrDefault() > 0)
                                    {
                                        //now look at the site's rent
                                        if (_termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //shortfall is already taken care of
                                        { }
                                        else if (_termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER)  //do nothing - already got the shortfall
                                        { }
                                        else if (_termsResults.SupplierRent.GetValueOrDefault() > (_termsResults.SiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SiteGroupShare.GetValueOrDefault())) //to zero
                                        {
                                            _termsResults.SupplierRent = _termsResults.SupplierRent.GetValueOrDefault() - (_termsResults.SiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SiteGroupShare.GetValueOrDefault());
                                            _termsResults.SiteGroupShare = _termsResults.SiteGroupShareShouldGet.GetValueOrDefault();
                                        }
                                        else //site shares cover min until they get to zero
                                        {
                                            _termsResults.SiteGroupShare = _termsResults.SiteGroupShare.GetValueOrDefault() + _termsResults.SupplierRent.GetValueOrDefault();
                                            _termsResults.SupplierRent = 0;
                                        }
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void UseSupplierForSecSiteGroupShortfall()
        {
            float? temp = 0;

            try
            {
                LogManager.WriteLog("Inside UseSupplierForSecSiteGroupShortfall...", LogManager.enumLogLevel.Info);

                switch (_termsInfo.SupplierGuarantor.GetValueOrDefault())
                {
                    case TERMS_PRIMARY_TO_ZERO:
                    case TERMS_PRIMARY_FULL:
                    case TERMS_SECONDARY_TO_ZERO:
                    case TERMS_SECONDARY_REMAINDER:
                    case TERMS_PERCENTAGE:
                        switch (_termsInfo.SecGroupType.GetValueOrDefault())
                        {
                            case TERMS_RENT:
                                //if the rent is guaranteed
                                if (_termsInfo.SecGroupValueGuaranteed.GetValueOrDefault())
                                {
                                    //if the SiteGroup needs a shortfall guarantor...
                                    if (_termsResults.SecSiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupRent.GetValueOrDefault() > 0)
                                    {
                                        //take value off site shares
                                        if (_termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE)
                                        {
                                            //take a % of the shortfall from the site shares

                                            //find out if the site is the only percentage guarantor left
                                            if ((_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SiteUse.GetValueOrDefault() > _termsInfo.SupplierUse.GetValueOrDefault())
                                                || (_termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.GroupUse.GetValueOrDefault() > _termsInfo.SupplierUse.GetValueOrDefault()))
                                            {
                                                temp = _termsInfo.SupplierGuarantorPercentage.GetValueOrDefault() / 100;
                                                temp = temp * (_termsResults.SecSiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupRent.GetValueOrDefault());
                                                _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() - Format2DPs(temp.GetValueOrDefault());
                                                _termsResults.SecSiteGroupRent = _termsResults.SecSiteGroupRent.GetValueOrDefault() + Format2DPs(temp.GetValueOrDefault());
                                                temp = 0;
                                            }
                                            else //there is no other shortfall guarantor, therefore the remainder is to be paid
                                            {
                                                _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() - (_termsResults.SecSiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupRent.GetValueOrDefault());
                                                _termsResults.SecSiteGroupRent = _termsResults.SecSiteGroupRentShouldGet.GetValueOrDefault();
                                            }
                                        }
                                    }
                                    else if (_termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER || _termsResults.SupplierShare.GetValueOrDefault() > (_termsResults.SecSiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupRent.GetValueOrDefault()))
                                    {
                                        _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() - (_termsResults.SecSiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupRent.GetValueOrDefault());
                                        _termsResults.SecSiteGroupRent = _termsResults.SecSiteGroupRentShouldGet.GetValueOrDefault();
                                    }
                                    else //to zero
                                    {
                                        _termsResults.SecSiteGroupRent = _termsResults.SecSiteGroupRent.GetValueOrDefault() + _termsResults.SupplierShare.GetValueOrDefault();
                                        _termsResults.SupplierShare = 0;
                                    }

                                    //if the SiteGroup still needs a shortfall guarantor...
                                    if (_termsResults.SecSiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupRent.GetValueOrDefault() > 0)
                                    {
                                        //take remaining value off site rent
                                        if (_termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //do nothing - already got the shortfall
                                        { }
                                        else if (_termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER)   //do nothing - already got the shortfall
                                        { }
                                        else if (_termsResults.SupplierRent.GetValueOrDefault() > (_termsResults.SecSiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupRent.GetValueOrDefault()))   //Has to be to zero
                                        {
                                            _termsResults.SupplierRent = _termsResults.SupplierRent.GetValueOrDefault() - (_termsResults.SecSiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupRent.GetValueOrDefault());
                                            _termsResults.SecSiteGroupRent = _termsResults.SecSiteGroupRentShouldGet.GetValueOrDefault();
                                        }
                                        else //to zero
                                        {
                                            _termsResults.SecSiteGroupRent = _termsResults.SecSiteGroupRent.GetValueOrDefault() + _termsResults.SupplierRent.GetValueOrDefault();
                                            _termsResults.SupplierRent = 0;
                                        }
                                    }
                                }
                                break;
                            case TERMS_MIN:
                                if (_termsInfo.SecGroupValueGuaranteed.GetValueOrDefault())
                                {
                                    if (_termsInfo.SecGroupValue.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault() > 0) //if the SiteGroup needs a shortfall guarantor for the min
                                    {
                                        if (_termsInfo.SupplierGuarantor == TERMS_PERCENTAGE) //if the site can cover the shortfall out of shares then
                                        {
                                            //take a % of shortfall from Site
                                            //find out if the site is the only percentage guarantor left
                                            if ((_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SiteUse.GetValueOrDefault() > _termsInfo.SupplierUse.GetValueOrDefault())
                                                || (_termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.GroupUse.GetValueOrDefault() > _termsInfo.SupplierUse.GetValueOrDefault()))
                                            {
                                                temp = _termsInfo.SupplierGuarantorPercentage.GetValueOrDefault() / 100;
                                                temp = temp * (_termsInfo.SecGroupValue.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault());
                                                _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() - Format2DPs(temp.GetValueOrDefault());
                                                _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() + Format2DPs(temp.GetValueOrDefault());
                                                temp = 0;
                                            }
                                            else //there is no other shortfall guarantor, therefore the remainder is to be paid to bring up to the min
                                            {
                                                _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() - (_termsInfo.SecGroupValue.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault());
                                                _termsResults.SecSiteGroupShare = _termsInfo.SecGroupValue.GetValueOrDefault();
                                            }
                                        }
                                        else if (_termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER ||
                                                    (_termsResults.SupplierShare.GetValueOrDefault() > (_termsInfo.SecGroupValue.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault())))
                                        {
                                            _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() - (_termsInfo.SecGroupValue.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault());
                                            _termsResults.SecSiteGroupShare = _termsInfo.SecGroupValue.GetValueOrDefault();
                                        }
                                        else
                                        {
                                            //site shares cover min until they get to zero
                                            _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() + _termsResults.SupplierShare.GetValueOrDefault();
                                            _termsResults.SupplierShare = 0;
                                        }
                                    }

                                    //if there is still a shortfall on the minimum
                                    if (_termsInfo.SecGroupValue.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault() > 0)
                                    {
                                        //now look at the site's rent
                                        if (_termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //shortfall is already taken care of
                                        { }
                                        else if (_termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER)  //do nothing - already got the shortfall
                                        { }
                                        else if (_termsResults.SupplierRent.GetValueOrDefault() > (_termsInfo.SecGroupValue.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault())) //to zero
                                        {
                                            _termsResults.SupplierRent = _termsResults.SupplierRent.GetValueOrDefault() - (_termsInfo.SecGroupValue.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault());
                                            _termsResults.SecSiteGroupShare = _termsInfo.SecGroupValue.GetValueOrDefault();
                                        }
                                        else //site shares cover min until they get to zero
                                        {
                                            _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() + _termsResults.SupplierRent.GetValueOrDefault();
                                            _termsResults.SupplierRent = 0;
                                        }
                                    }
                                }

                                //Now check if the share is guaranteed
                                if (_termsInfo.SecGroupShareGuaranteed.GetValueOrDefault())
                                {
                                    if (_termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault() > 0) //if the SiteGroup needs a shortfall guarantor for the min
                                    {
                                        if (_termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //if the site can cover the shortfall out of shares then
                                        {
                                            //take a % of shortfall from Site
                                            //find out if the site is the only percentage guarantor left
                                            if ((_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SiteUse.GetValueOrDefault() > _termsInfo.SupplierUse.GetValueOrDefault())
                                                || (_termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.GroupUse.GetValueOrDefault() > _termsInfo.SupplierUse.GetValueOrDefault()))
                                            {
                                                temp = _termsInfo.SupplierGuarantorPercentage.GetValueOrDefault() / 100;
                                                temp = temp * (_termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault());
                                                _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() - Format2DPs(temp.GetValueOrDefault());
                                                _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() + Format2DPs(temp.GetValueOrDefault());
                                                temp = 0;
                                            }
                                            else //there is no other shortfall guarantor, therefore the remainder is to be paid to bring up to the min
                                            {
                                                _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() - (_termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault());
                                                _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault();
                                            }
                                        }
                                        else if (_termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER ||
                                                    (_termsResults.SupplierShare.GetValueOrDefault() > (_termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault())))
                                        {
                                            _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() - (_termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault());
                                            _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault();
                                        }
                                        else//site shares cover min until they get to zero
                                        {
                                            _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() + _termsResults.SupplierShare.GetValueOrDefault();
                                            _termsResults.SupplierShare = 0;
                                        }
                                    }

                                    //if there is still a shortfall on the minimum
                                    if (_termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault() > 0)
                                    {
                                        //now look at the site's rent
                                        if (_termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //shortfall is already taken care of
                                        { }
                                        else if (_termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER)  //do nothing - already got the shortfall
                                        { }
                                        else if (_termsResults.SupplierRent.GetValueOrDefault() > (_termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault())) //to zero
                                        {
                                            _termsResults.SupplierRent = _termsResults.SupplierRent.GetValueOrDefault() - (_termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault());
                                            _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault();
                                        }
                                        else //site shares cover min until they get to zero
                                        {
                                            _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() + _termsResults.SupplierRent.GetValueOrDefault();
                                            _termsResults.SupplierRent = 0;
                                        }
                                    }
                                }
                                break;
                            case TERMS_MAX:
                                //the max value can't be guaranteed
                                //so we only have to guarantee the shares
                                if (_termsInfo.SecGroupShareGuaranteed.GetValueOrDefault())
                                {
                                    if (_termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault() > 0) //if the SiteGroup needs a shortfall guarantor for the share
                                    {
                                        if (_termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //if the site can cover the shortfall out of shares then
                                        {
                                            //take a % of shortfall from Site
                                            //find out if the site is the only percentage guarantor left
                                            if ((_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SiteUse.GetValueOrDefault() > _termsInfo.SupplierUse.GetValueOrDefault())
                                                || (_termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.GroupUse.GetValueOrDefault() > _termsInfo.SupplierUse.GetValueOrDefault()))
                                            {
                                                temp = _termsInfo.SupplierGuarantorPercentage.GetValueOrDefault() / 100;
                                                temp = temp * (_termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault());
                                                _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() - Format2DPs(temp.GetValueOrDefault());
                                                _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() + Format2DPs(temp.GetValueOrDefault());
                                                temp = 0;
                                            }
                                            else //there is no other shortfall guarantor, therefore the remainder is to be paid to bring up to the min
                                            {
                                                _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() - (_termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault());
                                                _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault();
                                            }
                                        }
                                        else if (_termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER ||
                                                    (_termsResults.SupplierShare.GetValueOrDefault() > (_termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault())))
                                        {
                                            _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() - (_termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault());
                                            _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault();
                                        }
                                        else
                                        {
                                            //site shares cover min until they get to zero
                                            _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() + _termsResults.SupplierShare.GetValueOrDefault();
                                            _termsResults.SupplierShare = 0;
                                        }
                                    }

                                    //if there is still a shortfall on the minimum
                                    if (_termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault() > 0)
                                    {
                                        //now look at the site's rent
                                        if (_termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //shortfall is already taken care of
                                        { }
                                        else if (_termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER)  //do nothing - already got the shortfall
                                        { }
                                        else if (_termsResults.SupplierRent.GetValueOrDefault() > (_termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault())) //to zero
                                        {
                                            _termsResults.SupplierRent = _termsResults.SupplierRent.GetValueOrDefault() - (_termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault());
                                            _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault();
                                        }
                                        else //site shares cover min until they get to zero
                                        {
                                            _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() + _termsResults.SupplierRent.GetValueOrDefault();
                                            _termsResults.SupplierRent = 0;
                                        }
                                    }
                                }
                                break;
                            case TERMS_FRONT_MONEY:
                                if (_termsInfo.SecGroupValueGuaranteed.GetValueOrDefault())
                                {
                                    if (_termsResults.SecSiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupRent.GetValueOrDefault() > 0) //if the SiteGroup needs a shortfall guarantor for the rent
                                    {
                                        if (_termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //if the site can cover the shortfall out of shares then
                                        {
                                            //take a % of shortfall from Site
                                            //find out if the site is the only percentage guarantor left
                                            if ((_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SiteUse.GetValueOrDefault() > _termsInfo.SupplierUse.GetValueOrDefault())
                                                || (_termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.GroupUse.GetValueOrDefault() > _termsInfo.SupplierUse.GetValueOrDefault()))
                                            {
                                                temp = _termsInfo.SupplierGuarantorPercentage.GetValueOrDefault() / 100;
                                                temp = temp * (_termsResults.SecSiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupRent.GetValueOrDefault());
                                                _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() - Format2DPs(temp.GetValueOrDefault());
                                                _termsResults.SecSiteGroupRent = _termsResults.SecSiteGroupRent.GetValueOrDefault() + Format2DPs(temp.GetValueOrDefault());
                                                temp = 0;
                                            }
                                            else //there is no other shortfall guarantor, therefore the remainder is to be paid to bring up to the min
                                            {
                                                _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() - (_termsResults.SecSiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupRent.GetValueOrDefault());
                                                _termsResults.SecSiteGroupRent = _termsResults.SecSiteGroupRentShouldGet.GetValueOrDefault();
                                            }
                                        }
                                        else if (_termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER ||
                                                    (_termsResults.SupplierShare.GetValueOrDefault() > (_termsResults.SecSiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupRent.GetValueOrDefault())))
                                        {
                                            _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() - (_termsResults.SecSiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupRent.GetValueOrDefault());
                                            _termsResults.SecSiteGroupRent = _termsResults.SecSiteGroupRentShouldGet.GetValueOrDefault();
                                        }
                                        else
                                        {
                                            //site shares cover min until they get to zero
                                            _termsResults.SecSiteGroupRent = _termsResults.SecSiteGroupRent.GetValueOrDefault() + _termsResults.SupplierShare.GetValueOrDefault();
                                            _termsResults.SupplierShare = 0;
                                        }
                                    }

                                    //if there is still a shortfall on the minimum
                                    if (_termsResults.SecSiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupRent.GetValueOrDefault() > 0)
                                    {
                                        //now look at the site's rent
                                        if (_termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //shortfall is already taken care of
                                        { }
                                        else if (_termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER)  //do nothing - already got the shortfall
                                        { }
                                        else if (_termsResults.SupplierRent.GetValueOrDefault() > (_termsResults.SecSiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupRent.GetValueOrDefault())) //to zero
                                        {
                                            _termsResults.SupplierRent = _termsResults.SupplierRent.GetValueOrDefault() - (_termsResults.SecSiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupRent.GetValueOrDefault());
                                            _termsResults.SecSiteGroupRent = _termsResults.SecSiteGroupRentShouldGet.GetValueOrDefault();
                                        }
                                        else //site shares cover min until they get to zero
                                        {
                                            _termsResults.SecSiteGroupRent = _termsResults.SecSiteGroupRent.GetValueOrDefault() + _termsResults.SupplierRent.GetValueOrDefault();
                                            _termsResults.SupplierRent = 0;
                                        }
                                    }
                                }

                                //Now check if the share is guaranteed
                                if (_termsInfo.SecGroupShareGuaranteed.GetValueOrDefault())
                                {
                                    if (_termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault() > 0) //if the SiteGroup needs a shortfall guarantor for the min
                                    {
                                        if (_termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //if the site can cover the shortfall out of shares then
                                        {
                                            //take a % of shortfall from Site
                                            //find out if the site is the only percentage guarantor left
                                            if ((_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SiteUse.GetValueOrDefault() > _termsInfo.SupplierUse.GetValueOrDefault())
                                                || (_termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.GroupUse.GetValueOrDefault() > _termsInfo.SupplierUse.GetValueOrDefault()))
                                            {
                                                temp = _termsInfo.SupplierGuarantorPercentage.GetValueOrDefault() / 100;
                                                temp = temp * (_termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault());
                                                _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() - Format2DPs(temp.GetValueOrDefault());
                                                _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() + Format2DPs(temp.GetValueOrDefault());
                                                temp = 0;
                                            }
                                            else //there is no other shortfall guarantor, therefore the remainder is to be paid to bring up to the min
                                            {
                                                _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() - (_termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault());
                                                _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault();
                                            }
                                        }
                                        else if (_termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER ||
                                                    (_termsResults.SupplierShare.GetValueOrDefault() > (_termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault())))
                                        {
                                            _termsResults.SupplierShare = _termsResults.SupplierShare.GetValueOrDefault() - (_termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault());
                                            _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault();
                                        }
                                        else
                                        {
                                            //site shares cover min until they get to zero
                                            _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() + _termsResults.SupplierShare.GetValueOrDefault();
                                            _termsResults.SupplierShare = 0;
                                        }
                                    }

                                    //if there is still a shortfall on the minimum
                                    //take it out of the site's rent
                                    if (_termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault() > 0)
                                    {
                                        //now look at the site's rent
                                        if (_termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //shortfall is already taken care of
                                        { }
                                        else if (_termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER)  //do nothing - already got the shortfall
                                        { }
                                        else if (_termsResults.SupplierRent.GetValueOrDefault() > (_termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault())) //to zero
                                        {
                                            _termsResults.SupplierRent = _termsResults.SupplierRent.GetValueOrDefault() - (_termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault());
                                            _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault();
                                        }
                                        else //site shares cover min until they get to zero
                                        {
                                            _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() + _termsResults.SupplierRent.GetValueOrDefault();
                                            _termsResults.SupplierRent = 0;
                                        }
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void UseSiteGroupForSecSiteGroupShortfall()
        {
            float? temp = 0;

            try
            {
                LogManager.WriteLog("Inside UseSiteGroupForSecSiteGroupShortfall...", LogManager.enumLogLevel.Info);

                switch (_termsInfo.GroupGuarantor.GetValueOrDefault())
                {
                    case TERMS_PRIMARY_TO_ZERO:
                    case TERMS_PRIMARY_FULL:
                    case TERMS_SECONDARY_TO_ZERO:
                    case TERMS_SECONDARY_REMAINDER:
                    case TERMS_PERCENTAGE:
                        switch (_termsInfo.SecGroupType)
                        {
                            case TERMS_RENT:
                                //if the rent is guaranteed
                                if (_termsInfo.SecGroupValueGuaranteed.GetValueOrDefault())
                                {
                                    //if the SiteGroup needs a shortfall guarantor...
                                    if (_termsResults.SecSiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupRent.GetValueOrDefault() > 0)
                                    {
                                        //take value off site shares
                                        if (_termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE)
                                        {
                                            //take a % of the shortfall from the site shares

                                            //find out if the site is the only percentage guarantor left
                                            if ((_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SiteUse.GetValueOrDefault() > _termsInfo.GroupUse.GetValueOrDefault())
                                                || (_termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SupplierUse.GetValueOrDefault() > _termsInfo.GroupUse.GetValueOrDefault()))
                                            {
                                                temp = _termsInfo.GroupGuarantorPercentage.GetValueOrDefault() / 100;
                                                temp = temp * (_termsResults.SecSiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupRent.GetValueOrDefault());
                                                _termsResults.SiteGroupShare = _termsResults.SiteGroupShare.GetValueOrDefault() - Format2DPs(temp.GetValueOrDefault());
                                                _termsResults.SecSiteGroupRent = _termsResults.SecSiteGroupRent.GetValueOrDefault() + Format2DPs(temp.GetValueOrDefault());
                                                temp = 0;
                                            }
                                            else //there is no other shortfall guarantor, therefore the remainder is to be paid
                                            {
                                                _termsResults.SiteGroupShare = _termsResults.SiteGroupShare.GetValueOrDefault() - (_termsResults.SecSiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupRent.GetValueOrDefault());
                                                _termsResults.SecSiteGroupRent = _termsResults.SecSiteGroupRentShouldGet.GetValueOrDefault();
                                            }
                                        }
                                    }
                                    else if (_termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER || _termsResults.SiteGroupShare.GetValueOrDefault() > (_termsResults.SecSiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupRent.GetValueOrDefault()))
                                    {
                                        _termsResults.SiteGroupShare = _termsResults.SiteGroupShare.GetValueOrDefault() - (_termsResults.SecSiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupRent.GetValueOrDefault());
                                        _termsResults.SecSiteGroupRent = _termsResults.SecSiteGroupRentShouldGet.GetValueOrDefault();
                                    }
                                    else //to zero
                                    {
                                        _termsResults.SecSiteGroupRent = _termsResults.SecSiteGroupRent.GetValueOrDefault() + _termsResults.SiteGroupShare.GetValueOrDefault();
                                        _termsResults.SiteGroupShare = 0;
                                    }

                                    //if the SiteGroup still needs a shortfall guarantor...
                                    if (_termsResults.SecSiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupRent.GetValueOrDefault() > 0)
                                    {
                                        //take remaining value off site rent
                                        if (_termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //do nothing - already got the shortfall
                                        { }
                                        else if (_termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER)   //do nothing - already got the shortfall
                                        { }
                                        else if (_termsResults.SiteGroupRent.GetValueOrDefault() > (_termsResults.SecSiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupRent.GetValueOrDefault()))   //Has to be to zero
                                        {
                                            _termsResults.SiteGroupRent = _termsResults.SiteGroupRent.GetValueOrDefault() - (_termsResults.SecSiteGroupRentShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupRent.GetValueOrDefault());
                                            _termsResults.SecSiteGroupRent = _termsResults.SecSiteGroupRentShouldGet.GetValueOrDefault();
                                        }
                                        else //to zero
                                        {
                                            _termsResults.SecSiteGroupRent = _termsResults.SecSiteGroupRent.GetValueOrDefault() + _termsResults.SiteGroupRent.GetValueOrDefault();
                                            _termsResults.SiteGroupRent = 0;
                                        }
                                    }
                                }
                                break;
                            case TERMS_MIN:
                                if (_termsInfo.SecGroupValueGuaranteed.GetValueOrDefault())
                                {
                                    if (_termsInfo.SecGroupValue.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault() > 0) //if the SiteGroup needs a shortfall guarantor for the min
                                    {
                                        if (_termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //if the site can cover the shortfall out of shares then
                                        {
                                            //take a % of shortfall from Site
                                            //find out if the site is the only percentage guarantor left
                                            if ((_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SiteUse.GetValueOrDefault() > _termsInfo.GroupUse.GetValueOrDefault())
                                                || (_termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SupplierUse.GetValueOrDefault() > _termsInfo.GroupUse.GetValueOrDefault()))
                                            {
                                                temp = _termsInfo.GroupGuarantorPercentage.GetValueOrDefault() / 100;
                                                temp = temp * (_termsInfo.SecGroupValue.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault());
                                                _termsResults.SiteGroupShare = _termsResults.SiteGroupShare.GetValueOrDefault() - Format2DPs(temp.GetValueOrDefault());
                                                _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() + Format2DPs(temp.GetValueOrDefault());
                                                temp = 0;
                                            }
                                            else //there is no other shortfall guarantor, therefore the remainder is to be paid to bring up to the min
                                            {
                                                _termsResults.SiteGroupShare = _termsResults.SiteGroupShare.GetValueOrDefault() - (_termsInfo.SecGroupValue.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault());
                                                _termsResults.SecSiteGroupShare = _termsInfo.SecGroupValue.GetValueOrDefault();
                                            }
                                        }
                                        else if (_termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER ||
                                                    (_termsResults.SiteGroupShare.GetValueOrDefault() > (_termsInfo.SecGroupValue.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault())))
                                        {
                                            _termsResults.SiteGroupShare = _termsResults.SiteGroupShare.GetValueOrDefault() - (_termsInfo.SecGroupValue.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault());
                                            _termsResults.SecSiteGroupShare = _termsInfo.SecGroupValue.GetValueOrDefault();
                                        }
                                        else
                                        {
                                            //site shares cover min until they get to zero
                                            _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() + _termsResults.SiteGroupShare.GetValueOrDefault();
                                            _termsResults.SiteGroupShare = 0;
                                        }
                                    }

                                    //if there is still a shortfall on the minimum
                                    if (_termsInfo.SecGroupValue.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault() > 0)
                                    {
                                        //now look at the site's rent
                                        if (_termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //shortfall is already taken care of
                                        { }
                                        else if (_termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER)  //do nothing - already got the shortfall
                                        { }
                                        else if (_termsResults.SiteGroupRent.GetValueOrDefault() > (_termsInfo.SecGroupValue.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault())) //to zero
                                        {
                                            _termsResults.SiteGroupRent = _termsResults.SiteGroupRent.GetValueOrDefault() - (_termsInfo.SecGroupValue.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault());
                                            _termsResults.SecSiteGroupShare = _termsInfo.SecGroupValue.GetValueOrDefault();
                                        }
                                        else //site shares cover min until they get to zero
                                        {
                                            _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare.GetValueOrDefault() + _termsResults.SiteGroupRent.GetValueOrDefault();
                                            _termsResults.SiteGroupRent = 0;
                                        }
                                    }
                                }

                                //Now check if the share is guaranteed
                                if (_termsInfo.SecGroupShareGuaranteed.GetValueOrDefault())
                                {
                                    if (_termsResults.SecSiteGroupShareShouldGet.GetValueOrDefault() - _termsResults.SecSiteGroupShare.GetValueOrDefault() > 0) //if the SiteGroup needs a shortfall guarantor for the min
                                    {
                                        if (_termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //if the site can cover the shortfall out of shares then
                                        {
                                            //take a % of shortfall from Site
                                            //find out if the site is the only percentage guarantor left
                                            if ((_termsInfo.SiteGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SiteUse > _termsInfo.GroupUse)
                                                || (_termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SupplierUse > _termsInfo.GroupUse))
                                            {
                                                temp = _termsInfo.GroupGuarantorPercentage / 100;
                                                temp = temp * (_termsResults.SecSiteGroupShareShouldGet - _termsResults.SecSiteGroupShare);
                                                _termsResults.SiteGroupShare = _termsResults.SiteGroupShare - Format2DPs(temp.GetValueOrDefault());
                                                _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare + Format2DPs(temp.GetValueOrDefault());
                                                temp = 0;
                                            }
                                            else //there is no other shortfall guarantor, therefore the remainder is to be paid to bring up to the min
                                            {
                                                _termsResults.SiteGroupShare = _termsResults.SiteGroupShare - (_termsResults.SecSiteGroupShareShouldGet - _termsResults.SecSiteGroupShare);
                                                _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShareShouldGet;
                                            }
                                        }
                                        else if (_termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER ||
                                                    (_termsResults.SiteGroupShare > (_termsResults.SecSiteGroupShareShouldGet - _termsResults.SecSiteGroupShare)))
                                        {
                                            _termsResults.SiteGroupShare = _termsResults.SiteGroupShare - (_termsResults.SecSiteGroupShareShouldGet - _termsResults.SecSiteGroupShare);
                                            _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShareShouldGet;
                                        }
                                        else//site shares cover min until they get to zero
                                        {
                                            _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare + _termsResults.SiteGroupShare;
                                            _termsResults.SiteGroupShare = 0;
                                        }
                                    }

                                    //if there is still a shortfall on the minimum
                                    if (_termsResults.SecSiteGroupShareShouldGet - _termsResults.SecSiteGroupShare > 0)
                                    {
                                        //now look at the site's rent
                                        if (_termsInfo.GroupGuarantor == TERMS_PERCENTAGE) //shortfall is already taken care of
                                        { }
                                        else if (_termsInfo.GroupGuarantor == TERMS_PRIMARY_FULL || _termsInfo.GroupGuarantor == TERMS_SECONDARY_REMAINDER)  //do nothing - already got the shortfall
                                        { }
                                        else if (_termsResults.SiteGroupRent > (_termsResults.SecSiteGroupShareShouldGet - _termsResults.SecSiteGroupShare)) //to zero
                                        {
                                            _termsResults.SiteGroupRent = _termsResults.SiteGroupRent - (_termsResults.SecSiteGroupShareShouldGet - _termsResults.SecSiteGroupShare);
                                            _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShareShouldGet;
                                        }
                                        else //site shares cover min until they get to zero
                                        {
                                            _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare + _termsResults.SiteGroupRent;
                                            _termsResults.SiteGroupRent = 0;
                                        }
                                    }
                                }
                                break;
                            case TERMS_MAX:
                                //the max value can't be guaranteed
                                //so we only have to guarantee the shares
                                if (_termsInfo.SecGroupShareGuaranteed.GetValueOrDefault())
                                {
                                    if (_termsResults.SecSiteGroupShareShouldGet - _termsResults.SecSiteGroupShare > 0) //if the SiteGroup needs a shortfall guarantor for the share
                                    {
                                        if (_termsInfo.GroupGuarantor == TERMS_PERCENTAGE) //if the site can cover the shortfall out of shares then
                                        {
                                            //take a % of shortfall from Site
                                            //find out if the site is the only percentage guarantor left
                                            if ((_termsInfo.SiteGuarantor == TERMS_PERCENTAGE && _termsInfo.SiteUse > _termsInfo.GroupUse)
                                                || (_termsInfo.SupplierGuarantor == TERMS_PERCENTAGE && _termsInfo.SupplierUse > _termsInfo.GroupUse))
                                            {
                                                temp = _termsInfo.GroupGuarantorPercentage / 100;
                                                temp = temp * (_termsResults.SecSiteGroupShareShouldGet - _termsResults.SecSiteGroupShare);
                                                _termsResults.SiteGroupShare = _termsResults.SiteGroupShare - Format2DPs(temp.GetValueOrDefault());
                                                _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare + Format2DPs(temp.GetValueOrDefault());
                                                temp = 0;
                                            }
                                            else //there is no other shortfall guarantor, therefore the remainder is to be paid to bring up to the min
                                            {
                                                _termsResults.SiteGroupShare = _termsResults.SiteGroupShare - (_termsResults.SecSiteGroupShareShouldGet - _termsResults.SecSiteGroupShare);
                                                _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShareShouldGet;
                                            }
                                        }
                                        else if (_termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER ||
                                                    (_termsResults.SiteGroupShare > (_termsResults.SecSiteGroupShareShouldGet - _termsResults.SecSiteGroupShare)))
                                        {
                                            _termsResults.SiteGroupShare = _termsResults.SiteGroupShare - (_termsResults.SecSiteGroupShareShouldGet - _termsResults.SecSiteGroupShare);
                                            _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShareShouldGet;
                                        }
                                        else
                                        {
                                            //site shares cover min until they get to zero
                                            _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare + _termsResults.SiteGroupShare;
                                            _termsResults.SiteGroupShare = 0;
                                        }
                                    }

                                    //if there is still a shortfall on the minimum
                                    if (_termsResults.SecSiteGroupShareShouldGet - _termsResults.SecSiteGroupShare > 0)
                                    {
                                        //now look at the site's rent
                                        if (_termsInfo.GroupGuarantor == TERMS_PERCENTAGE) //shortfall is already taken care of
                                        { }
                                        else if (_termsInfo.GroupGuarantor == TERMS_PRIMARY_FULL || _termsInfo.GroupGuarantor == TERMS_SECONDARY_REMAINDER)  //do nothing - already got the shortfall
                                        { }
                                        else if (_termsResults.SiteGroupRent > (_termsResults.SecSiteGroupShareShouldGet - _termsResults.SecSiteGroupShare)) //to zero
                                        {
                                            _termsResults.SiteGroupRent = _termsResults.SiteGroupRent - (_termsResults.SecSiteGroupShareShouldGet - _termsResults.SecSiteGroupShare);
                                            _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShareShouldGet;
                                        }
                                        else //site shares cover min until they get to zero
                                        {
                                            _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare + _termsResults.SiteGroupRent;
                                            _termsResults.SiteGroupRent = 0;
                                        }
                                    }
                                }
                                break;
                            case TERMS_FRONT_MONEY:
                                if (_termsInfo.SecGroupValueGuaranteed.GetValueOrDefault())
                                {
                                    if (_termsResults.SecSiteGroupRentShouldGet - _termsResults.SecSiteGroupRent > 0) //if the SiteGroup needs a shortfall guarantor for the rent
                                    {
                                        if (_termsInfo.GroupGuarantor == TERMS_PERCENTAGE) //if the site can cover the shortfall out of shares then
                                        {
                                            //take a % of shortfall from Site
                                            //find out if the site is the only percentage guarantor left
                                            if ((_termsInfo.SiteGuarantor == TERMS_PERCENTAGE && _termsInfo.SiteUse > _termsInfo.GroupUse)
                                                || (_termsInfo.SupplierGuarantor == TERMS_PERCENTAGE && _termsInfo.SupplierUse > _termsInfo.GroupUse))
                                            {
                                                temp = _termsInfo.GroupGuarantorPercentage / 100;
                                                temp = temp * (_termsResults.SecSiteGroupRentShouldGet - _termsResults.SecSiteGroupRent);
                                                _termsResults.SiteGroupShare = _termsResults.SiteGroupShare - Format2DPs(temp.GetValueOrDefault());
                                                _termsResults.SecSiteGroupRent = _termsResults.SecSiteGroupRent + Format2DPs(temp.GetValueOrDefault());
                                                temp = 0;
                                            }
                                            else //there is no other shortfall guarantor, therefore the remainder is to be paid to bring up to the min
                                            {
                                                _termsResults.SiteGroupShare = _termsResults.SiteGroupShare - (_termsResults.SecSiteGroupRentShouldGet - _termsResults.SecSiteGroupRent);
                                                _termsResults.SecSiteGroupRent = _termsResults.SecSiteGroupRentShouldGet;
                                            }
                                        }
                                        else if (_termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER ||
                                                    (_termsResults.SiteGroupShare > (_termsResults.SecSiteGroupRentShouldGet - _termsResults.SecSiteGroupRent)))
                                        {
                                            _termsResults.SiteGroupShare = _termsResults.SiteGroupShare - (_termsResults.SecSiteGroupRentShouldGet - _termsResults.SecSiteGroupRent);
                                            _termsResults.SecSiteGroupRent = _termsResults.SecSiteGroupRentShouldGet;
                                        }
                                        else
                                        {
                                            //site shares cover min until they get to zero
                                            _termsResults.SecSiteGroupRent = _termsResults.SecSiteGroupRent + _termsResults.SiteGroupShare;
                                            _termsResults.SiteGroupShare = 0;
                                        }
                                    }

                                    //if there is still a shortfall on the minimum
                                    if (_termsResults.SecSiteGroupRentShouldGet - _termsResults.SecSiteGroupRent > 0)
                                    {
                                        //now look at the site's rent
                                        if (_termsInfo.GroupGuarantor == TERMS_PERCENTAGE) //shortfall is already taken care of
                                        { }
                                        else if (_termsInfo.GroupGuarantor == TERMS_PRIMARY_FULL || _termsInfo.GroupGuarantor == TERMS_SECONDARY_REMAINDER)  //do nothing - already got the shortfall
                                        { }
                                        else if (_termsResults.SiteGroupRent > (_termsResults.SecSiteGroupRentShouldGet - _termsResults.SecSiteGroupRent)) //to zero
                                        {
                                            _termsResults.SiteGroupRent = _termsResults.SiteGroupRent - (_termsResults.SecSiteGroupRentShouldGet - _termsResults.SecSiteGroupRent);
                                            _termsResults.SecSiteGroupRent = _termsResults.SecSiteGroupRentShouldGet;
                                        }
                                        else //site shares cover min until they get to zero
                                        {
                                            _termsResults.SecSiteGroupRent = _termsResults.SecSiteGroupRent + _termsResults.SiteGroupRent;
                                            _termsResults.SiteGroupRent = 0;
                                        }
                                    }
                                }

                                //Now check if the share is guaranteed
                                if (_termsInfo.SecGroupShareGuaranteed.GetValueOrDefault())
                                {
                                    if (_termsResults.SecSiteGroupShareShouldGet - _termsResults.SecSiteGroupShare > 0) //if the SiteGroup needs a shortfall guarantor for the min
                                    {
                                        if (_termsInfo.GroupGuarantor == TERMS_PERCENTAGE) //if the site can cover the shortfall out of shares then
                                        {
                                            //take a % of shortfall from Site
                                            //find out if the site is the only percentage guarantor left
                                            if ((_termsInfo.SiteGuarantor == TERMS_PERCENTAGE && _termsInfo.SiteUse > _termsInfo.GroupUse)
                                                || (_termsInfo.SupplierGuarantor == TERMS_PERCENTAGE && _termsInfo.SupplierUse > _termsInfo.GroupUse))
                                            {
                                                temp = _termsInfo.GroupGuarantorPercentage / 100;
                                                temp = temp * (_termsResults.SecSiteGroupShareShouldGet - _termsResults.SecSiteGroupShare);
                                                _termsResults.SiteGroupShare = _termsResults.SiteGroupShare - Format2DPs(temp.GetValueOrDefault());
                                                _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare + Format2DPs(temp.GetValueOrDefault());
                                                temp = 0;
                                            }
                                            else //there is no other shortfall guarantor, therefore the remainder is to be paid to bring up to the min
                                            {
                                                _termsResults.SiteGroupShare = _termsResults.SiteGroupShare - (_termsResults.SecSiteGroupShareShouldGet - _termsResults.SecSiteGroupShare);
                                                _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShareShouldGet;
                                            }
                                        }
                                        else if (_termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER ||
                                                    (_termsResults.SiteGroupShare > (_termsResults.SecSiteGroupShareShouldGet - _termsResults.SecSiteGroupShare)))
                                        {
                                            _termsResults.SiteGroupShare = _termsResults.SiteGroupShare - (_termsResults.SecSiteGroupShareShouldGet - _termsResults.SecSiteGroupShare);
                                            _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShareShouldGet;
                                        }
                                        else
                                        {
                                            //site shares cover min until they get to zero
                                            _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare + _termsResults.SiteGroupShare;
                                            _termsResults.SiteGroupShare = 0;
                                        }
                                    }

                                    //if there is still a shortfall on the minimum
                                    //take it out of the site's rent
                                    if (_termsResults.SecSiteGroupShareShouldGet - _termsResults.SecSiteGroupShare > 0)
                                    {
                                        //now look at the site's rent
                                        if (_termsInfo.GroupGuarantor == TERMS_PERCENTAGE) //shortfall is already taken care of
                                        { }
                                        else if (_termsInfo.GroupGuarantor == TERMS_PRIMARY_FULL || _termsInfo.GroupGuarantor == TERMS_SECONDARY_REMAINDER)  //do nothing - already got the shortfall
                                        { }
                                        else if (_termsResults.SiteGroupRent > (_termsResults.SecSiteGroupShareShouldGet - _termsResults.SecSiteGroupShare)) //to zero
                                        {
                                            _termsResults.SiteGroupRent = _termsResults.SiteGroupRent - (_termsResults.SecSiteGroupShareShouldGet - _termsResults.SecSiteGroupShare);
                                            _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShareShouldGet;
                                        }
                                        else //site shares cover min until they get to zero
                                        {
                                            _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare + _termsResults.SiteGroupRent;
                                            _termsResults.SiteGroupRent = 0;
                                        }
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void UseSupplierForSiteShortfall()
        {
            float? temp = 0;

            try
            {
                LogManager.WriteLog("Inside UseSupplierForSiteShortfall...", LogManager.enumLogLevel.Info);

                switch (_termsInfo.SupplierGuarantor.GetValueOrDefault())
                {
                    case TERMS_PRIMARY_TO_ZERO:
                    case TERMS_PRIMARY_FULL:
                    case TERMS_SECONDARY_TO_ZERO:
                    case TERMS_SECONDARY_REMAINDER:
                    case TERMS_PERCENTAGE:
                        switch (_termsInfo.SiteType)
                        {
                            case TERMS_RENT:
                                //if the rent is guaranteed
                                if (_termsInfo.SiteValueGuaranteed.GetValueOrDefault())
                                {
                                    //if the SiteGroup needs a shortfall guarantor...
                                    if (_termsResults.SiteRentShouldGet - _termsResults.SiteRent > 0)
                                    {
                                        //take value off site shares
                                        if (_termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE)
                                        {
                                            //take a % of the shortfall from the site shares

                                            //find out if the site is the only percentage guarantor left
                                            if ((_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SecGroupUse.GetValueOrDefault() > _termsInfo.SupplierUse)
                                                || (_termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.GroupUse.GetValueOrDefault() > _termsInfo.SupplierUse))
                                            {
                                                temp = _termsInfo.SupplierGuarantorPercentage / 100;
                                                temp = temp * (_termsResults.SiteRentShouldGet - _termsResults.SiteRent);
                                                _termsResults.SupplierShare = _termsResults.SupplierShare - Format2DPs(temp.GetValueOrDefault());
                                                _termsResults.SiteRent = _termsResults.SiteRent + Format2DPs(temp.GetValueOrDefault());
                                                temp = 0;
                                            }
                                            else //there is no other shortfall guarantor, therefore the remainder is to be paid
                                            {
                                                _termsResults.SupplierShare = _termsResults.SupplierShare - (_termsResults.SiteRentShouldGet - _termsResults.SiteRent);
                                                _termsResults.SiteRent = _termsResults.SiteRentShouldGet;
                                            }
                                        }
                                    }
                                    else if (_termsInfo.SupplierGuarantor == TERMS_PRIMARY_FULL || _termsInfo.SupplierGuarantor == TERMS_SECONDARY_REMAINDER || _termsResults.SupplierShare > (_termsResults.SiteRentShouldGet - _termsResults.SiteRent))
                                    {
                                        _termsResults.SupplierShare = _termsResults.SupplierShare - (_termsResults.SiteRentShouldGet - _termsResults.SiteRent);
                                        _termsResults.SiteRent = _termsResults.SiteRentShouldGet;
                                    }
                                    else //to zero
                                    {
                                        _termsResults.SiteRent = _termsResults.SiteRent + _termsResults.SupplierShare;
                                        _termsResults.SupplierShare = 0;
                                    }

                                    //if the SiteGroup still needs a shortfall guarantor...
                                    if (_termsResults.SiteRentShouldGet - _termsResults.SiteRent > 0)
                                    {
                                        //take remaining value off site rent
                                        if (_termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //do nothing - already got the shortfall
                                        { }
                                        else if (_termsInfo.SupplierGuarantor == TERMS_PRIMARY_FULL || _termsInfo.SupplierGuarantor == TERMS_SECONDARY_REMAINDER)   //do nothing - already got the shortfall
                                        { }
                                        else if (_termsResults.SupplierRent > (_termsResults.SiteRentShouldGet - _termsResults.SiteRent))   //Has to be to zero
                                        {
                                            _termsResults.SupplierRent = _termsResults.SupplierRent - (_termsResults.SiteRentShouldGet - _termsResults.SiteRent);
                                            _termsResults.SiteRent = _termsResults.SiteRentShouldGet;
                                        }
                                        else //to zero
                                        {
                                            _termsResults.SiteRent = _termsResults.SiteRent + _termsResults.SupplierRent;
                                            _termsResults.SupplierRent = 0;
                                        }
                                    }
                                }
                                break;
                            case TERMS_MIN:
                                if (_termsInfo.SiteValueGuaranteed.GetValueOrDefault())
                                {
                                    if (_termsInfo.SiteValue - _termsResults.SiteShare > 0) //if the SiteGroup needs a shortfall guarantor for the min
                                    {
                                        if (_termsInfo.SupplierGuarantor == TERMS_PERCENTAGE) //if the site can cover the shortfall out of shares then
                                        {
                                            //take a % of shortfall from Site
                                            //find out if the site is the only percentage guarantor left
                                            if ((_termsInfo.SecGroupGuarantor == TERMS_PERCENTAGE && _termsInfo.SecGroupUse > _termsInfo.SupplierUse)
                                                || (_termsInfo.GroupGuarantor == TERMS_PERCENTAGE && _termsInfo.GroupUse > _termsInfo.SupplierUse))
                                            {
                                                temp = _termsInfo.SupplierGuarantorPercentage / 100;
                                                temp = temp * (_termsInfo.SiteValue - _termsResults.SiteShare);
                                                _termsResults.SupplierShare = _termsResults.SupplierShare - Format2DPs(temp.GetValueOrDefault());
                                                _termsResults.SiteShare = _termsResults.SiteShare + Format2DPs(temp.GetValueOrDefault());
                                                temp = 0;
                                            }
                                            else //there is no other shortfall guarantor, therefore the remainder is to be paid to bring up to the min
                                            {
                                                _termsResults.SupplierShare = _termsResults.SupplierShare - (_termsInfo.SiteValue - _termsResults.SiteShare);
                                                _termsResults.SiteShare = _termsInfo.SiteValue;
                                            }
                                        }
                                        else if (_termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER ||
                                                    (_termsResults.SupplierShare > (_termsInfo.SiteValue - _termsResults.SiteShare)))
                                        {
                                            _termsResults.SupplierShare = _termsResults.SupplierShare - (_termsInfo.SiteValue - _termsResults.SiteShare);
                                            _termsResults.SiteShare = _termsInfo.SiteValue;
                                        }
                                        else
                                        {
                                            //site shares cover min until they get to zero
                                            _termsResults.SiteShare = _termsResults.SiteShare + _termsResults.SupplierShare;
                                            _termsResults.SupplierShare = 0;
                                        }
                                    }

                                    //if there is still a shortfall on the minimum
                                    if (_termsInfo.SiteValue - _termsResults.SiteShare > 0)
                                    {
                                        //now look at the site's rent
                                        if (_termsInfo.SupplierGuarantor == TERMS_PERCENTAGE) //shortfall is already taken care of
                                        { }
                                        else if (_termsInfo.SupplierGuarantor == TERMS_PRIMARY_FULL || _termsInfo.SupplierGuarantor == TERMS_SECONDARY_REMAINDER)  //do nothing - already got the shortfall
                                        { }
                                        else if (_termsResults.SupplierRent > (_termsInfo.SiteValue - _termsResults.SiteShare)) //to zero
                                        {
                                            _termsResults.SupplierRent = _termsResults.SupplierRent - (_termsInfo.SiteValue - _termsResults.SiteShare);
                                            _termsResults.SiteShare = _termsInfo.SiteValue;
                                        }
                                        else //site shares cover min until they get to zero
                                        {
                                            _termsResults.SiteShare = _termsResults.SiteShare + _termsResults.SupplierRent;
                                            _termsResults.SupplierRent = 0;
                                        }
                                    }
                                }

                                //Now check if the share is guaranteed
                                if (_termsInfo.SiteShareGuaranteed.GetValueOrDefault())
                                {
                                    if (_termsResults.SiteShareShouldGet - _termsResults.SiteShare > 0) //if the SiteGroup needs a shortfall guarantor for the min
                                    {
                                        if (_termsInfo.SupplierGuarantor == TERMS_PERCENTAGE) //if the site can cover the shortfall out of shares then
                                        {
                                            //take a % of shortfall from Site
                                            //find out if the site is the only percentage guarantor left
                                            if ((_termsInfo.SecGroupGuarantor == TERMS_PERCENTAGE && _termsInfo.SecGroupUse > _termsInfo.SupplierUse)
                                                || (_termsInfo.GroupGuarantor == TERMS_PERCENTAGE && _termsInfo.GroupUse > _termsInfo.SupplierUse))
                                            {
                                                temp = _termsInfo.SupplierGuarantorPercentage / 100;
                                                temp = temp * (_termsResults.SiteShareShouldGet - _termsResults.SiteShare);
                                                _termsResults.SupplierShare = _termsResults.SupplierShare - Format2DPs(temp.GetValueOrDefault());
                                                _termsResults.SiteShare = _termsResults.SiteShare + Format2DPs(temp.GetValueOrDefault());
                                                temp = 0;
                                            }
                                            else //there is no other shortfall guarantor, therefore the remainder is to be paid to bring up to the min
                                            {
                                                _termsResults.SupplierShare = _termsResults.SupplierShare - (_termsResults.SiteShareShouldGet - _termsResults.SiteShare);
                                                _termsResults.SiteShare = _termsResults.SiteShareShouldGet;
                                            }
                                        }
                                        else if (_termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER ||
                                                    (_termsResults.SupplierShare > (_termsResults.SiteShareShouldGet - _termsResults.SiteShare)))
                                        {
                                            _termsResults.SupplierShare = _termsResults.SupplierShare - (_termsResults.SiteShareShouldGet - _termsResults.SiteShare);
                                            _termsResults.SiteShare = _termsResults.SiteShareShouldGet;
                                        }
                                        else//site shares cover min until they get to zero
                                        {
                                            _termsResults.SiteShare = _termsResults.SiteShare + _termsResults.SupplierShare;
                                            _termsResults.SupplierShare = 0;
                                        }
                                    }

                                    //if there is still a shortfall on the minimum
                                    if (_termsResults.SiteShareShouldGet - _termsResults.SiteShare > 0)
                                    {
                                        //now look at the site's rent
                                        if (_termsInfo.SupplierGuarantor == TERMS_PERCENTAGE) //shortfall is already taken care of
                                        { }
                                        else if (_termsInfo.SupplierGuarantor == TERMS_PRIMARY_FULL || _termsInfo.SupplierGuarantor == TERMS_SECONDARY_REMAINDER)  //do nothing - already got the shortfall
                                        { }
                                        else if (_termsResults.SupplierRent > (_termsResults.SiteShareShouldGet - _termsResults.SiteShare)) //to zero
                                        {
                                            _termsResults.SupplierRent = _termsResults.SupplierRent - (_termsResults.SiteShareShouldGet - _termsResults.SiteShare);
                                            _termsResults.SiteShare = _termsResults.SiteShareShouldGet;
                                        }
                                        else //site shares cover min until they get to zero
                                        {
                                            _termsResults.SiteShare = _termsResults.SiteShare + _termsResults.SupplierRent;
                                            _termsResults.SupplierRent = 0;
                                        }
                                    }
                                }
                                break;
                            case TERMS_MAX:
                                //the max value can't be guaranteed
                                //so we only have to guarantee the shares
                                if (_termsInfo.SiteShareGuaranteed.GetValueOrDefault())
                                {
                                    if (_termsResults.SiteShareShouldGet - _termsResults.SiteShare > 0) //if the SiteGroup needs a shortfall guarantor for the share
                                    {
                                        if (_termsInfo.SupplierGuarantor == TERMS_PERCENTAGE) //if the site can cover the shortfall out of shares then
                                        {
                                            //take a % of shortfall from Site
                                            //find out if the site is the only percentage guarantor left
                                            if ((_termsInfo.SecGroupGuarantor == TERMS_PERCENTAGE && _termsInfo.SecGroupUse > _termsInfo.SupplierUse)
                                                || (_termsInfo.GroupGuarantor == TERMS_PERCENTAGE && _termsInfo.GroupUse > _termsInfo.SupplierUse))
                                            {
                                                temp = _termsInfo.SupplierGuarantorPercentage / 100;
                                                temp = temp * (_termsResults.SiteShareShouldGet - _termsResults.SiteShare);
                                                _termsResults.SupplierShare = _termsResults.SupplierShare - Format2DPs(temp.GetValueOrDefault());
                                                _termsResults.SiteShare = _termsResults.SiteShare + Format2DPs(temp.GetValueOrDefault());
                                                temp = 0;
                                            }
                                            else //there is no other shortfall guarantor, therefore the remainder is to be paid to bring up to the min
                                            {
                                                _termsResults.SupplierShare = _termsResults.SupplierShare - (_termsResults.SiteShareShouldGet - _termsResults.SiteShare);
                                                _termsResults.SiteShare = _termsResults.SiteShareShouldGet;
                                            }
                                        }
                                        else if (_termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER ||
                                                    (_termsResults.SupplierShare > (_termsResults.SiteShareShouldGet - _termsResults.SiteShare)))
                                        {
                                            _termsResults.SupplierShare = _termsResults.SupplierShare - (_termsResults.SiteShareShouldGet - _termsResults.SiteShare);
                                            _termsResults.SiteShare = _termsResults.SiteShareShouldGet;
                                        }
                                        else
                                        {
                                            //site shares cover min until they get to zero
                                            _termsResults.SiteShare = _termsResults.SiteShare + _termsResults.SupplierShare;
                                            _termsResults.SupplierShare = 0;
                                        }
                                    }

                                    //if there is still a shortfall on the minimum
                                    if (_termsResults.SiteShareShouldGet - _termsResults.SiteShare > 0)
                                    {
                                        //now look at the site's rent
                                        if (_termsInfo.SupplierGuarantor == TERMS_PERCENTAGE) //shortfall is already taken care of
                                        { }
                                        else if (_termsInfo.SupplierGuarantor == TERMS_PRIMARY_FULL || _termsInfo.SupplierGuarantor == TERMS_SECONDARY_REMAINDER)  //do nothing - already got the shortfall
                                        { }
                                        else if (_termsResults.SupplierRent > (_termsResults.SiteShareShouldGet - _termsResults.SiteShare)) //to zero
                                        {
                                            _termsResults.SupplierRent = _termsResults.SupplierRent - (_termsResults.SiteShareShouldGet - _termsResults.SiteShare);
                                            _termsResults.SiteShare = _termsResults.SiteShareShouldGet;
                                        }
                                        else //site shares cover min until they get to zero
                                        {
                                            _termsResults.SiteShare = _termsResults.SiteShare + _termsResults.SupplierRent;
                                            _termsResults.SupplierRent = 0;
                                        }
                                    }
                                }
                                break;
                            case TERMS_FRONT_MONEY:
                                if (_termsInfo.SiteValueGuaranteed.GetValueOrDefault())
                                {
                                    if (_termsResults.SiteRentShouldGet - _termsResults.SiteRent > 0) //if the SiteGroup needs a shortfall guarantor for the rent
                                    {
                                        if (_termsInfo.SupplierGuarantor == TERMS_PERCENTAGE) //if the site can cover the shortfall out of shares then
                                        {
                                            //take a % of shortfall from Site
                                            //find out if the site is the only percentage guarantor left
                                            if ((_termsInfo.SecGroupGuarantor == TERMS_PERCENTAGE && _termsInfo.SecGroupUse > _termsInfo.SupplierUse)
                                                || (_termsInfo.GroupGuarantor == TERMS_PERCENTAGE && _termsInfo.GroupUse > _termsInfo.SupplierUse))
                                            {
                                                temp = _termsInfo.SupplierGuarantorPercentage / 100;
                                                temp = temp * (_termsResults.SiteRentShouldGet - _termsResults.SiteRent);
                                                _termsResults.SupplierShare = _termsResults.SupplierShare - Format2DPs(temp.GetValueOrDefault());
                                                _termsResults.SiteRent = _termsResults.SiteRent + Format2DPs(temp.GetValueOrDefault());
                                                temp = 0;
                                            }
                                            else //there is no other shortfall guarantor, therefore the remainder is to be paid to bring up to the min
                                            {
                                                _termsResults.SupplierShare = _termsResults.SupplierShare - (_termsResults.SiteRentShouldGet - _termsResults.SiteRent);
                                                _termsResults.SiteRent = _termsResults.SiteRentShouldGet;
                                            }
                                        }
                                        else if (_termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER ||
                                                    (_termsResults.SupplierShare > (_termsResults.SiteRentShouldGet - _termsResults.SiteRent)))
                                        {
                                            _termsResults.SupplierShare = _termsResults.SupplierShare - (_termsResults.SiteRentShouldGet - _termsResults.SiteRent);
                                            _termsResults.SiteRent = _termsResults.SiteRentShouldGet;
                                        }
                                        else
                                        {
                                            //site shares cover min until they get to zero
                                            _termsResults.SiteRent = _termsResults.SiteRent + _termsResults.SupplierShare;
                                            _termsResults.SupplierShare = 0;
                                        }
                                    }

                                    //if there is still a shortfall on the minimum
                                    if (_termsResults.SiteRentShouldGet - _termsResults.SiteRent > 0)
                                    {
                                        //now look at the site's rent
                                        if (_termsInfo.SupplierGuarantor == TERMS_PERCENTAGE) //shortfall is already taken care of
                                        { }
                                        else if (_termsInfo.SupplierGuarantor == TERMS_PRIMARY_FULL || _termsInfo.SupplierGuarantor == TERMS_SECONDARY_REMAINDER)  //do nothing - already got the shortfall
                                        { }
                                        else if (_termsResults.SupplierRent > (_termsResults.SiteRentShouldGet - _termsResults.SiteRent)) //to zero
                                        {
                                            _termsResults.SupplierRent = _termsResults.SupplierRent - (_termsResults.SiteRentShouldGet - _termsResults.SiteRent);
                                            _termsResults.SiteRent = _termsResults.SiteRentShouldGet;
                                        }
                                        else //site shares cover min until they get to zero
                                        {
                                            _termsResults.SiteRent = _termsResults.SiteRent + _termsResults.SupplierRent;
                                            _termsResults.SupplierRent = 0;
                                        }
                                    }
                                }

                                //Now check if the share is guaranteed
                                if (_termsInfo.SiteShareGuaranteed.GetValueOrDefault())
                                {
                                    if (_termsResults.SiteShareShouldGet - _termsResults.SiteShare > 0) //if the SiteGroup needs a shortfall guarantor for the min
                                    {
                                        if (_termsInfo.SupplierGuarantor == TERMS_PERCENTAGE) //if the site can cover the shortfall out of shares then
                                        {
                                            //take a % of shortfall from Site
                                            //find out if the site is the only percentage guarantor left
                                            if ((_termsInfo.SecGroupGuarantor == TERMS_PERCENTAGE && _termsInfo.SecGroupUse > _termsInfo.SupplierUse)
                                                || (_termsInfo.GroupGuarantor == TERMS_PERCENTAGE && _termsInfo.GroupUse > _termsInfo.SupplierUse))
                                            {
                                                temp = _termsInfo.SupplierGuarantorPercentage / 100;
                                                temp = temp * (_termsResults.SiteShareShouldGet - _termsResults.SiteShare);
                                                _termsResults.SupplierShare = _termsResults.SupplierShare - Format2DPs(temp.GetValueOrDefault());
                                                _termsResults.SiteShare = _termsResults.SiteShare + Format2DPs(temp.GetValueOrDefault());
                                                temp = 0;
                                            }
                                            else //there is no other shortfall guarantor, therefore the remainder is to be paid to bring up to the min
                                            {
                                                _termsResults.SupplierShare = _termsResults.SupplierShare - (_termsResults.SiteShareShouldGet - _termsResults.SiteShare);
                                                _termsResults.SiteShare = _termsResults.SiteShareShouldGet;
                                            }
                                        }
                                        else if (_termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER ||
                                                    (_termsResults.SupplierShare > (_termsResults.SiteShareShouldGet - _termsResults.SiteShare)))
                                        {
                                            _termsResults.SupplierShare = _termsResults.SupplierShare - (_termsResults.SiteShareShouldGet - _termsResults.SiteShare);
                                            _termsResults.SiteShare = _termsResults.SiteShareShouldGet;
                                        }
                                        else
                                        {
                                            //site shares cover min until they get to zero
                                            _termsResults.SiteShare = _termsResults.SiteShare + _termsResults.SupplierShare;
                                            _termsResults.SupplierShare = 0;
                                        }
                                    }

                                    //if there is still a shortfall on the minimum
                                    //take it out of the site's rent
                                    if (_termsResults.SiteShareShouldGet - _termsResults.SiteShare > 0)
                                    {
                                        //now look at the site's rent
                                        if (_termsInfo.SupplierGuarantor == TERMS_PERCENTAGE) //shortfall is already taken care of
                                        { }
                                        else if (_termsInfo.SupplierGuarantor == TERMS_PRIMARY_FULL || _termsInfo.SupplierGuarantor == TERMS_SECONDARY_REMAINDER)  //do nothing - already got the shortfall
                                        { }
                                        else if (_termsResults.SupplierRent > (_termsResults.SiteShareShouldGet - _termsResults.SiteShare)) //to zero
                                        {
                                            _termsResults.SupplierRent = _termsResults.SupplierRent - (_termsResults.SiteShareShouldGet - _termsResults.SiteShare);
                                            _termsResults.SiteShare = _termsResults.SiteShareShouldGet;
                                        }
                                        else //site shares cover min until they get to zero
                                        {
                                            _termsResults.SiteShare = _termsResults.SiteShare + _termsResults.SupplierRent;
                                            _termsResults.SupplierRent = 0;
                                        }
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void UseSiteGroupForSiteShortfall()
        {
            float? temp = 0;

            try
            {
                LogManager.WriteLog("Inside UseSiteGroupForSiteShortfall...", LogManager.enumLogLevel.Info);

                switch (_termsInfo.GroupGuarantor.GetValueOrDefault())
                {
                    case TERMS_PRIMARY_TO_ZERO:
                    case TERMS_PRIMARY_FULL:
                    case TERMS_SECONDARY_TO_ZERO:
                    case TERMS_SECONDARY_REMAINDER:
                    case TERMS_PERCENTAGE:
                        switch (_termsInfo.SiteType)
                        {
                            case TERMS_RENT:
                                //if the rent is guaranteed
                                if (_termsInfo.SiteValueGuaranteed.GetValueOrDefault())
                                {
                                    //if the SiteGroup needs a shortfall guarantor...
                                    if (_termsResults.SiteRentShouldGet - _termsResults.SiteRent > 0)
                                    {
                                        //take value off site shares
                                        if (_termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE)
                                        {
                                            //take a % of the shortfall from the site shares

                                            //find out if the site is the only percentage guarantor left
                                            if ((_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SecGroupUse.GetValueOrDefault() > _termsInfo.GroupUse)
                                                || (_termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SupplierUse.GetValueOrDefault() > _termsInfo.GroupUse))
                                            {
                                                temp = _termsInfo.GroupGuarantorPercentage / 100;
                                                temp = temp * (_termsResults.SiteRentShouldGet - _termsResults.SiteRent);
                                                _termsResults.SiteGroupShare = _termsResults.SiteGroupShare - Format2DPs(temp.GetValueOrDefault());
                                                _termsResults.SiteRent = _termsResults.SiteRent + Format2DPs(temp.GetValueOrDefault());
                                                temp = 0;
                                            }
                                            else //there is no other shortfall guarantor, therefore the remainder is to be paid
                                            {
                                                _termsResults.SiteGroupShare = _termsResults.SiteGroupShare - (_termsResults.SiteRentShouldGet - _termsResults.SiteRent);
                                                _termsResults.SiteRent = _termsResults.SiteRentShouldGet;
                                            }
                                        }
                                    }
                                    else if (_termsInfo.GroupGuarantor == TERMS_PRIMARY_FULL || _termsInfo.GroupGuarantor == TERMS_SECONDARY_REMAINDER || _termsResults.SiteGroupShare > (_termsResults.SiteRentShouldGet - _termsResults.SiteRent))
                                    {
                                        _termsResults.SiteGroupShare = _termsResults.SiteGroupShare - (_termsResults.SiteRentShouldGet - _termsResults.SiteRent);
                                        _termsResults.SiteRent = _termsResults.SiteRentShouldGet;
                                    }
                                    else //to zero
                                    {
                                        _termsResults.SiteRent = _termsResults.SiteRent + _termsResults.SiteGroupShare;
                                        _termsResults.SiteGroupShare = 0;
                                    }

                                    //if the SiteGroup still needs a shortfall guarantor...
                                    if (_termsResults.SiteRentShouldGet - _termsResults.SiteRent > 0)
                                    {
                                        //take remaining value off site rent
                                        if (_termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //do nothing - already got the shortfall
                                        { }
                                        else if (_termsInfo.GroupGuarantor == TERMS_PRIMARY_FULL || _termsInfo.GroupGuarantor == TERMS_SECONDARY_REMAINDER)   //do nothing - already got the shortfall
                                        { }
                                        else if (_termsResults.SiteGroupRent > (_termsResults.SiteRentShouldGet - _termsResults.SiteRent))   //Has to be to zero
                                        {
                                            _termsResults.SiteGroupRent = _termsResults.SiteGroupRent - (_termsResults.SiteRentShouldGet - _termsResults.SiteRent);
                                            _termsResults.SiteRent = _termsResults.SiteRentShouldGet;
                                        }
                                        else //to zero
                                        {
                                            _termsResults.SiteRent = _termsResults.SiteRent + _termsResults.SiteGroupRent;
                                            _termsResults.SiteGroupRent = 0;
                                        }
                                    }
                                }
                                break;
                            case TERMS_MIN:
                                if (_termsInfo.SiteValueGuaranteed.GetValueOrDefault())
                                {
                                    if (_termsInfo.SiteValue - _termsResults.SiteShare > 0) //if the SiteGroup needs a shortfall guarantor for the min
                                    {
                                        if (_termsInfo.GroupGuarantor == TERMS_PERCENTAGE) //if the site can cover the shortfall out of shares then
                                        {
                                            //take a % of shortfall from Site
                                            //find out if the site is the only percentage guarantor left
                                            if ((_termsInfo.SecGroupGuarantor == TERMS_PERCENTAGE && _termsInfo.SecGroupUse > _termsInfo.GroupUse)
                                                || (_termsInfo.SupplierGuarantor == TERMS_PERCENTAGE && _termsInfo.SupplierUse > _termsInfo.GroupUse))
                                            {
                                                temp = _termsInfo.GroupGuarantorPercentage / 100;
                                                temp = temp * (_termsInfo.SiteValue - _termsResults.SiteShare);
                                                _termsResults.SiteGroupShare = _termsResults.SiteGroupShare - Format2DPs(temp.GetValueOrDefault());
                                                _termsResults.SiteShare = _termsResults.SiteShare + Format2DPs(temp.GetValueOrDefault());
                                                temp = 0;
                                            }
                                            else //there is no other shortfall guarantor, therefore the remainder is to be paid to bring up to the min
                                            {
                                                _termsResults.SiteGroupShare = _termsResults.SiteGroupShare - (_termsInfo.SiteValue - _termsResults.SiteShare);
                                                _termsResults.SiteShare = _termsInfo.SiteValue;
                                            }
                                        }
                                        else if (_termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER ||
                                                    (_termsResults.SiteGroupShare > (_termsInfo.SiteValue - _termsResults.SiteShare)))
                                        {
                                            _termsResults.SiteGroupShare = _termsResults.SiteGroupShare - (_termsInfo.SiteValue - _termsResults.SiteShare);
                                            _termsResults.SiteShare = _termsInfo.SiteValue;
                                        }
                                        else
                                        {
                                            //site shares cover min until they get to zero
                                            _termsResults.SiteShare = _termsResults.SiteShare + _termsResults.SiteGroupShare;
                                            _termsResults.SiteGroupShare = 0;
                                        }
                                    }

                                    //if there is still a shortfall on the minimum
                                    if (_termsInfo.SiteValue - _termsResults.SiteShare > 0)
                                    {
                                        //now look at the site's rent
                                        if (_termsInfo.GroupGuarantor == TERMS_PERCENTAGE) //shortfall is already taken care of
                                        { }
                                        else if (_termsInfo.GroupGuarantor == TERMS_PRIMARY_FULL || _termsInfo.GroupGuarantor == TERMS_SECONDARY_REMAINDER)  //do nothing - already got the shortfall
                                        { }
                                        else if (_termsResults.SiteGroupRent > (_termsInfo.SiteValue - _termsResults.SiteShare)) //to zero
                                        {
                                            _termsResults.SiteGroupRent = _termsResults.SiteGroupRent - (_termsInfo.SiteValue - _termsResults.SiteShare);
                                            _termsResults.SiteShare = _termsInfo.SiteValue;
                                        }
                                        else //site shares cover min until they get to zero
                                        {
                                            _termsResults.SiteShare = _termsResults.SiteShare + _termsResults.SiteGroupRent;
                                            _termsResults.SiteGroupRent = 0;
                                        }
                                    }
                                }

                                //Now check if the share is guaranteed
                                if (_termsInfo.SiteShareGuaranteed.GetValueOrDefault())
                                {
                                    if (_termsResults.SiteShareShouldGet - _termsResults.SiteShare > 0) //if the SiteGroup needs a shortfall guarantor for the min
                                    {
                                        if (_termsInfo.GroupGuarantor == TERMS_PERCENTAGE) //if the site can cover the shortfall out of shares then
                                        {
                                            //take a % of shortfall from Site
                                            //find out if the site is the only percentage guarantor left
                                            if ((_termsInfo.SecGroupGuarantor == TERMS_PERCENTAGE && _termsInfo.SecGroupUse > _termsInfo.GroupUse)
                                                || (_termsInfo.SupplierGuarantor == TERMS_PERCENTAGE && _termsInfo.SupplierUse > _termsInfo.GroupUse))
                                            {
                                                temp = _termsInfo.GroupGuarantorPercentage / 100;
                                                temp = temp * (_termsResults.SiteShareShouldGet - _termsResults.SiteShare);
                                                _termsResults.SiteGroupShare = _termsResults.SiteGroupShare - Format2DPs(temp.GetValueOrDefault());
                                                _termsResults.SiteShare = _termsResults.SiteShare + Format2DPs(temp.GetValueOrDefault());
                                                temp = 0;
                                            }
                                            else //there is no other shortfall guarantor, therefore the remainder is to be paid to bring up to the min
                                            {
                                                _termsResults.SiteGroupShare = _termsResults.SiteGroupShare - (_termsResults.SiteShareShouldGet - _termsResults.SiteShare);
                                                _termsResults.SiteShare = _termsResults.SiteShareShouldGet;
                                            }
                                        }
                                        else if (_termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER ||
                                                    (_termsResults.SiteGroupShare > (_termsResults.SiteShareShouldGet - _termsResults.SiteShare)))
                                        {
                                            _termsResults.SiteGroupShare = _termsResults.SiteGroupShare - (_termsResults.SiteShareShouldGet - _termsResults.SiteShare);
                                            _termsResults.SiteShare = _termsResults.SiteShareShouldGet;
                                        }
                                        else//site shares cover min until they get to zero
                                        {
                                            _termsResults.SiteShare = _termsResults.SiteShare + _termsResults.SiteGroupShare;
                                            _termsResults.SiteGroupShare = 0;
                                        }
                                    }

                                    //if there is still a shortfall on the minimum
                                    if (_termsResults.SiteShareShouldGet - _termsResults.SiteShare > 0)
                                    {
                                        //now look at the site's rent
                                        if (_termsInfo.GroupGuarantor == TERMS_PERCENTAGE) //shortfall is already taken care of
                                        { }
                                        else if (_termsInfo.GroupGuarantor == TERMS_PRIMARY_FULL || _termsInfo.GroupGuarantor == TERMS_SECONDARY_REMAINDER)  //do nothing - already got the shortfall
                                        { }
                                        else if (_termsResults.SiteGroupRent > (_termsResults.SiteShareShouldGet - _termsResults.SiteShare)) //to zero
                                        {
                                            _termsResults.SiteGroupRent = _termsResults.SiteGroupRent - (_termsResults.SiteShareShouldGet - _termsResults.SiteShare);
                                            _termsResults.SiteShare = _termsResults.SiteShareShouldGet;
                                        }
                                        else //site shares cover min until they get to zero
                                        {
                                            _termsResults.SiteShare = _termsResults.SiteShare + _termsResults.SiteGroupRent;
                                            _termsResults.SiteGroupRent = 0;
                                        }
                                    }
                                }
                                break;
                            case TERMS_MAX:
                                //the max value can't be guaranteed
                                //so we only have to guarantee the shares
                                if (_termsInfo.SiteShareGuaranteed.GetValueOrDefault())
                                {
                                    if (_termsResults.SiteShareShouldGet - _termsResults.SiteShare > 0) //if the SiteGroup needs a shortfall guarantor for the share
                                    {
                                        if (_termsInfo.GroupGuarantor == TERMS_PERCENTAGE) //if the site can cover the shortfall out of shares then
                                        {
                                            //take a % of shortfall from Site
                                            //find out if the site is the only percentage guarantor left
                                            if ((_termsInfo.SecGroupGuarantor == TERMS_PERCENTAGE && _termsInfo.SecGroupUse > _termsInfo.GroupUse)
                                                || (_termsInfo.SupplierGuarantor == TERMS_PERCENTAGE && _termsInfo.SupplierUse > _termsInfo.GroupUse))
                                            {
                                                temp = _termsInfo.GroupGuarantorPercentage / 100;
                                                temp = temp * (_termsResults.SiteShareShouldGet - _termsResults.SiteShare);
                                                _termsResults.SiteGroupShare = _termsResults.SiteGroupShare - Format2DPs(temp.GetValueOrDefault());
                                                _termsResults.SiteShare = _termsResults.SiteShare + Format2DPs(temp.GetValueOrDefault());
                                                temp = 0;
                                            }
                                            else //there is no other shortfall guarantor, therefore the remainder is to be paid to bring up to the min
                                            {
                                                _termsResults.SiteGroupShare = _termsResults.SiteGroupShare - (_termsResults.SiteShareShouldGet - _termsResults.SiteShare);
                                                _termsResults.SiteShare = _termsResults.SiteShareShouldGet;
                                            }
                                        }
                                        else if (_termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER ||
                                                    (_termsResults.SiteGroupShare > (_termsResults.SiteShareShouldGet - _termsResults.SiteShare)))
                                        {
                                            _termsResults.SiteGroupShare = _termsResults.SiteGroupShare - (_termsResults.SiteShareShouldGet - _termsResults.SiteShare);
                                            _termsResults.SiteShare = _termsResults.SiteShareShouldGet;
                                        }
                                        else
                                        {
                                            //site shares cover min until they get to zero
                                            _termsResults.SiteShare = _termsResults.SiteShare + _termsResults.SiteGroupShare;
                                            _termsResults.SiteGroupShare = 0;
                                        }
                                    }

                                    //if there is still a shortfall on the minimum
                                    if (_termsResults.SiteShareShouldGet - _termsResults.SiteShare > 0)
                                    {
                                        //now look at the site's rent
                                        if (_termsInfo.GroupGuarantor == TERMS_PERCENTAGE) //shortfall is already taken care of
                                        { }
                                        else if (_termsInfo.GroupGuarantor == TERMS_PRIMARY_FULL || _termsInfo.GroupGuarantor == TERMS_SECONDARY_REMAINDER)  //do nothing - already got the shortfall
                                        { }
                                        else if (_termsResults.SiteGroupRent > (_termsResults.SiteShareShouldGet - _termsResults.SiteShare)) //to zero
                                        {
                                            _termsResults.SiteGroupRent = _termsResults.SiteGroupRent - (_termsResults.SiteShareShouldGet - _termsResults.SiteShare);
                                            _termsResults.SiteShare = _termsResults.SiteShareShouldGet;
                                        }
                                        else //site shares cover min until they get to zero
                                        {
                                            _termsResults.SiteShare = _termsResults.SiteShare + _termsResults.SiteGroupRent;
                                            _termsResults.SiteGroupRent = 0;
                                        }
                                    }
                                }
                                break;
                            case TERMS_FRONT_MONEY:
                                if (_termsInfo.SiteValueGuaranteed.GetValueOrDefault())
                                {
                                    if (_termsResults.SiteRentShouldGet - _termsResults.SiteRent > 0) //if the SiteGroup needs a shortfall guarantor for the rent
                                    {
                                        if (_termsInfo.GroupGuarantor == TERMS_PERCENTAGE) //if the site can cover the shortfall out of shares then
                                        {
                                            //take a % of shortfall from Site
                                            //find out if the site is the only percentage guarantor left
                                            if ((_termsInfo.SecGroupGuarantor == TERMS_PERCENTAGE && _termsInfo.SecGroupUse > _termsInfo.GroupUse)
                                                || (_termsInfo.SupplierGuarantor == TERMS_PERCENTAGE && _termsInfo.SupplierUse > _termsInfo.GroupUse))
                                            {
                                                temp = _termsInfo.GroupGuarantorPercentage / 100;
                                                temp = temp * (_termsResults.SiteRentShouldGet - _termsResults.SiteRent);
                                                _termsResults.SiteGroupShare = _termsResults.SiteGroupShare - Format2DPs(temp.GetValueOrDefault());
                                                _termsResults.SiteRent = _termsResults.SiteRent + Format2DPs(temp.GetValueOrDefault());
                                                temp = 0;
                                            }
                                            else //there is no other shortfall guarantor, therefore the remainder is to be paid to bring up to the min
                                            {
                                                _termsResults.SiteGroupShare = _termsResults.SiteGroupShare - (_termsResults.SiteRentShouldGet - _termsResults.SiteRent);
                                                _termsResults.SiteRent = _termsResults.SiteRentShouldGet;
                                            }
                                        }
                                        else if (_termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER ||
                                                    (_termsResults.SiteGroupShare > (_termsResults.SiteRentShouldGet - _termsResults.SiteRent)))
                                        {
                                            _termsResults.SiteGroupShare = _termsResults.SiteGroupShare - (_termsResults.SiteRentShouldGet - _termsResults.SiteRent);
                                            _termsResults.SiteRent = _termsResults.SiteRentShouldGet;
                                        }
                                        else
                                        {
                                            //site shares cover min until they get to zero
                                            _termsResults.SiteRent = _termsResults.SiteRent + _termsResults.SiteGroupShare;
                                            _termsResults.SiteGroupShare = 0;
                                        }
                                    }

                                    //if there is still a shortfall on the minimum
                                    if (_termsResults.SiteRentShouldGet - _termsResults.SiteRent > 0)
                                    {
                                        //now look at the site's rent
                                        if (_termsInfo.GroupGuarantor == TERMS_PERCENTAGE) //shortfall is already taken care of
                                        { }
                                        else if (_termsInfo.GroupGuarantor == TERMS_PRIMARY_FULL || _termsInfo.GroupGuarantor == TERMS_SECONDARY_REMAINDER)  //do nothing - already got the shortfall
                                        { }
                                        else if (_termsResults.SiteGroupRent > (_termsResults.SiteRentShouldGet - _termsResults.SiteRent)) //to zero
                                        {
                                            _termsResults.SiteGroupRent = _termsResults.SiteGroupRent - (_termsResults.SiteRentShouldGet - _termsResults.SiteRent);
                                            _termsResults.SiteRent = _termsResults.SiteRentShouldGet;
                                        }
                                        else //site shares cover min until they get to zero
                                        {
                                            _termsResults.SiteRent = _termsResults.SiteRent + _termsResults.SiteGroupRent;
                                            _termsResults.SiteGroupRent = 0;
                                        }
                                    }
                                }

                                //Now check if the share is guaranteed
                                if (_termsInfo.SiteShareGuaranteed.GetValueOrDefault())
                                {
                                    if (_termsResults.SiteShareShouldGet - _termsResults.SiteShare > 0) //if the SiteGroup needs a shortfall guarantor for the min
                                    {
                                        if (_termsInfo.GroupGuarantor == TERMS_PERCENTAGE) //if the site can cover the shortfall out of shares then
                                        {
                                            //take a % of shortfall from Site
                                            //find out if the site is the only percentage guarantor left
                                            if ((_termsInfo.SecGroupGuarantor == TERMS_PERCENTAGE && _termsInfo.SecGroupUse > _termsInfo.GroupUse)
                                                || (_termsInfo.SupplierGuarantor == TERMS_PERCENTAGE && _termsInfo.SupplierUse > _termsInfo.GroupUse))
                                            {
                                                temp = _termsInfo.GroupGuarantorPercentage / 100;
                                                temp = temp * (_termsResults.SiteShareShouldGet - _termsResults.SiteShare);
                                                _termsResults.SiteGroupShare = _termsResults.SiteGroupShare - Format2DPs(temp.GetValueOrDefault());
                                                _termsResults.SiteShare = _termsResults.SiteShare + Format2DPs(temp.GetValueOrDefault());
                                                temp = 0;
                                            }
                                            else //there is no other shortfall guarantor, therefore the remainder is to be paid to bring up to the min
                                            {
                                                _termsResults.SiteGroupShare = _termsResults.SiteGroupShare - (_termsResults.SiteShareShouldGet - _termsResults.SiteShare);
                                                _termsResults.SiteShare = _termsResults.SiteShareShouldGet;
                                            }
                                        }
                                        else if (_termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER ||
                                                    (_termsResults.SiteGroupShare > (_termsResults.SiteShareShouldGet - _termsResults.SiteShare)))
                                        {
                                            _termsResults.SiteGroupShare = _termsResults.SiteGroupShare - (_termsResults.SiteShareShouldGet - _termsResults.SiteShare);
                                            _termsResults.SiteShare = _termsResults.SiteShareShouldGet;
                                        }
                                        else
                                        {
                                            //site shares cover min until they get to zero
                                            _termsResults.SiteShare = _termsResults.SiteShare + _termsResults.SiteGroupShare;
                                            _termsResults.SiteGroupShare = 0;
                                        }
                                    }

                                    //if there is still a shortfall on the minimum
                                    //take it out of the site's rent
                                    if (_termsResults.SiteShareShouldGet - _termsResults.SiteShare > 0)
                                    {
                                        //now look at the site's rent
                                        if (_termsInfo.GroupGuarantor == TERMS_PERCENTAGE) //shortfall is already taken care of
                                        { }
                                        else if (_termsInfo.GroupGuarantor == TERMS_PRIMARY_FULL || _termsInfo.GroupGuarantor == TERMS_SECONDARY_REMAINDER)  //do nothing - already got the shortfall
                                        { }
                                        else if (_termsResults.SiteGroupRent > (_termsResults.SiteShareShouldGet - _termsResults.SiteShare)) //to zero
                                        {
                                            _termsResults.SiteGroupRent = _termsResults.SiteGroupRent - (_termsResults.SiteShareShouldGet - _termsResults.SiteShare);
                                            _termsResults.SiteShare = _termsResults.SiteShareShouldGet;
                                        }
                                        else //site shares cover min until they get to zero
                                        {
                                            _termsResults.SiteShare = _termsResults.SiteShare + _termsResults.SiteGroupRent;
                                            _termsResults.SiteGroupRent = 0;
                                        }
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void UseSecSiteGroupForSiteShortfall()
        {
            float? temp = 0;

            try
            {
                LogManager.WriteLog("Inside UseSecSiteGroupForSiteShortfall...", LogManager.enumLogLevel.Info);

                switch (_termsInfo.SecGroupGuarantor.GetValueOrDefault())
                {
                    case TERMS_PRIMARY_TO_ZERO:
                    case TERMS_PRIMARY_FULL:
                    case TERMS_SECONDARY_TO_ZERO:
                    case TERMS_SECONDARY_REMAINDER:
                    case TERMS_PERCENTAGE:
                        switch (_termsInfo.SiteType)
                        {
                            case TERMS_RENT:
                                //if the rent is guaranteed
                                if (_termsInfo.SiteValueGuaranteed.GetValueOrDefault())
                                {
                                    //if the SiteGroup needs a shortfall guarantor...
                                    if (_termsResults.SiteRentShouldGet - _termsResults.SiteRent > 0)
                                    {
                                        //take value off site shares
                                        if (_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE)
                                        {
                                            //take a % of the shortfall from the site shares

                                            //find out if the site is the only percentage guarantor left
                                            if ((_termsInfo.SupplierGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.SupplierUse.GetValueOrDefault() > _termsInfo.SecGroupUse)
                                                || (_termsInfo.GroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE && _termsInfo.GroupUse.GetValueOrDefault() > _termsInfo.SecGroupUse))
                                            {
                                                temp = _termsInfo.SecGroupGuarantorPercentage / 100;
                                                temp = temp * (_termsResults.SiteRentShouldGet - _termsResults.SiteRent);
                                                _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare - Format2DPs(temp.GetValueOrDefault());
                                                _termsResults.SiteRent = _termsResults.SiteRent + Format2DPs(temp.GetValueOrDefault());
                                                temp = 0;
                                            }
                                            else //there is no other shortfall guarantor, therefore the remainder is to be paid
                                            {
                                                _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare - (_termsResults.SiteRentShouldGet - _termsResults.SiteRent);
                                                _termsResults.SiteRent = _termsResults.SiteRentShouldGet;
                                            }
                                        }
                                    }
                                    else if (_termsInfo.SecGroupGuarantor == TERMS_PRIMARY_FULL || _termsInfo.SecGroupGuarantor == TERMS_SECONDARY_REMAINDER || _termsResults.SecSiteGroupShare > (_termsResults.SiteRentShouldGet - _termsResults.SiteRent))
                                    {
                                        _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare - (_termsResults.SiteRentShouldGet - _termsResults.SiteRent);
                                        _termsResults.SiteRent = _termsResults.SiteRentShouldGet;
                                    }
                                    else //to zero
                                    {
                                        _termsResults.SiteRent = _termsResults.SiteRent + _termsResults.SecSiteGroupShare;
                                        _termsResults.SecSiteGroupShare = 0;
                                    }

                                    //if the SiteGroup still needs a shortfall guarantor...
                                    if (_termsResults.SiteRentShouldGet - _termsResults.SiteRent > 0)
                                    {
                                        //take remaining value off site rent
                                        if (_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PERCENTAGE) //do nothing - already got the shortfall
                                        { }
                                        else if (_termsInfo.SecGroupGuarantor == TERMS_PRIMARY_FULL || _termsInfo.SecGroupGuarantor == TERMS_SECONDARY_REMAINDER)   //do nothing - already got the shortfall
                                        { }
                                        else if (_termsResults.SecSiteGroupRent > (_termsResults.SiteRentShouldGet - _termsResults.SiteRent))   //Has to be to zero
                                        {
                                            _termsResults.SecSiteGroupRent = _termsResults.SecSiteGroupRent - (_termsResults.SiteRentShouldGet - _termsResults.SiteRent);
                                            _termsResults.SiteRent = _termsResults.SiteRentShouldGet;
                                        }
                                        else //to zero
                                        {
                                            _termsResults.SiteRent = _termsResults.SiteRent + _termsResults.SecSiteGroupRent;
                                            _termsResults.SecSiteGroupRent = 0;
                                        }
                                    }
                                }
                                break;
                            case TERMS_MIN:
                                if (_termsInfo.SiteValueGuaranteed.GetValueOrDefault())
                                {
                                    if (_termsInfo.SiteValue - _termsResults.SiteShare > 0) //if the SiteGroup needs a shortfall guarantor for the min
                                    {
                                        if (_termsInfo.SecGroupGuarantor == TERMS_PERCENTAGE) //if the site can cover the shortfall out of shares then
                                        {
                                            //take a % of shortfall from Site
                                            //find out if the site is the only percentage guarantor left
                                            if ((_termsInfo.SupplierGuarantor == TERMS_PERCENTAGE && _termsInfo.SupplierUse > _termsInfo.SecGroupUse)
                                                || (_termsInfo.GroupGuarantor == TERMS_PERCENTAGE && _termsInfo.GroupUse > _termsInfo.SecGroupUse))
                                            {
                                                temp = _termsInfo.SecGroupGuarantorPercentage / 100;
                                                temp = temp * (_termsInfo.SiteValue - _termsResults.SiteShare);
                                                _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare - Format2DPs(temp.GetValueOrDefault());
                                                _termsResults.SiteShare = _termsResults.SiteShare + Format2DPs(temp.GetValueOrDefault());
                                                temp = 0;
                                            }
                                            else //there is no other shortfall guarantor, therefore the remainder is to be paid to bring up to the min
                                            {
                                                _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare - (_termsInfo.SiteValue - _termsResults.SiteShare);
                                                _termsResults.SiteShare = _termsInfo.SiteValue;
                                            }
                                        }
                                        else if (_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER ||
                                                    (_termsResults.SecSiteGroupShare > (_termsInfo.SiteValue - _termsResults.SiteShare)))
                                        {
                                            _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare - (_termsInfo.SiteValue - _termsResults.SiteShare);
                                            _termsResults.SiteShare = _termsInfo.SiteValue;
                                        }
                                        else
                                        {
                                            //site shares cover min until they get to zero
                                            _termsResults.SiteShare = _termsResults.SiteShare + _termsResults.SecSiteGroupShare;
                                            _termsResults.SecSiteGroupShare = 0;
                                        }
                                    }

                                    //if there is still a shortfall on the minimum
                                    if (_termsInfo.SiteValue - _termsResults.SiteShare > 0)
                                    {
                                        //now look at the site's rent
                                        if (_termsInfo.SecGroupGuarantor == TERMS_PERCENTAGE) //shortfall is already taken care of
                                        { }
                                        else if (_termsInfo.SecGroupGuarantor == TERMS_PRIMARY_FULL || _termsInfo.SecGroupGuarantor == TERMS_SECONDARY_REMAINDER)  //do nothing - already got the shortfall
                                        { }
                                        else if (_termsResults.SecSiteGroupRent > (_termsInfo.SiteValue - _termsResults.SiteShare)) //to zero
                                        {
                                            _termsResults.SecSiteGroupRent = _termsResults.SecSiteGroupRent - (_termsInfo.SiteValue - _termsResults.SiteShare);
                                            _termsResults.SiteShare = _termsInfo.SiteValue;
                                        }
                                        else //site shares cover min until they get to zero
                                        {
                                            _termsResults.SiteShare = _termsResults.SiteShare + _termsResults.SecSiteGroupRent;
                                            _termsResults.SecSiteGroupRent = 0;
                                        }
                                    }
                                }

                                //Now check if the share is guaranteed
                                if (_termsInfo.SiteShareGuaranteed.GetValueOrDefault())
                                {
                                    if (_termsResults.SiteShareShouldGet - _termsResults.SiteShare > 0) //if the SiteGroup needs a shortfall guarantor for the min
                                    {
                                        if (_termsInfo.SecGroupGuarantor == TERMS_PERCENTAGE) //if the site can cover the shortfall out of shares then
                                        {
                                            //take a % of shortfall from Site
                                            //find out if the site is the only percentage guarantor left
                                            if ((_termsInfo.SupplierGuarantor == TERMS_PERCENTAGE && _termsInfo.SupplierUse > _termsInfo.SecGroupUse)
                                                || (_termsInfo.GroupGuarantor == TERMS_PERCENTAGE && _termsInfo.GroupUse > _termsInfo.SecGroupUse))
                                            {
                                                temp = _termsInfo.SecGroupGuarantorPercentage / 100;
                                                temp = temp * (_termsResults.SiteShareShouldGet - _termsResults.SiteShare);
                                                _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare - Format2DPs(temp.GetValueOrDefault());
                                                _termsResults.SiteShare = _termsResults.SiteShare + Format2DPs(temp.GetValueOrDefault());
                                                temp = 0;
                                            }
                                            else //there is no other shortfall guarantor, therefore the remainder is to be paid to bring up to the min
                                            {
                                                _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare - (_termsResults.SiteShareShouldGet - _termsResults.SiteShare);
                                                _termsResults.SiteShare = _termsResults.SiteShareShouldGet;
                                            }
                                        }
                                        else if (_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER ||
                                                    (_termsResults.SecSiteGroupShare > (_termsResults.SiteShareShouldGet - _termsResults.SiteShare)))
                                        {
                                            _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare - (_termsResults.SiteShareShouldGet - _termsResults.SiteShare);
                                            _termsResults.SiteShare = _termsResults.SiteShareShouldGet;
                                        }
                                        else//site shares cover min until they get to zero
                                        {
                                            _termsResults.SiteShare = _termsResults.SiteShare + _termsResults.SecSiteGroupShare;
                                            _termsResults.SecSiteGroupShare = 0;
                                        }
                                    }

                                    //if there is still a shortfall on the minimum
                                    if (_termsResults.SiteShareShouldGet - _termsResults.SiteShare > 0)
                                    {
                                        //now look at the site's rent
                                        if (_termsInfo.SecGroupGuarantor == TERMS_PERCENTAGE) //shortfall is already taken care of
                                        { }
                                        else if (_termsInfo.SecGroupGuarantor == TERMS_PRIMARY_FULL || _termsInfo.SecGroupGuarantor == TERMS_SECONDARY_REMAINDER)  //do nothing - already got the shortfall
                                        { }
                                        else if (_termsResults.SecSiteGroupRent > (_termsResults.SiteShareShouldGet - _termsResults.SiteShare)) //to zero
                                        {
                                            _termsResults.SecSiteGroupRent = _termsResults.SecSiteGroupRent - (_termsResults.SiteShareShouldGet - _termsResults.SiteShare);
                                            _termsResults.SiteShare = _termsResults.SiteShareShouldGet;
                                        }
                                        else //site shares cover min until they get to zero
                                        {
                                            _termsResults.SiteShare = _termsResults.SiteShare + _termsResults.SecSiteGroupRent;
                                            _termsResults.SecSiteGroupRent = 0;
                                        }
                                    }
                                }
                                break;
                            case TERMS_MAX:
                                //the max value can't be guaranteed
                                //so we only have to guarantee the shares
                                if (_termsInfo.SiteShareGuaranteed.GetValueOrDefault())
                                {
                                    if (_termsResults.SiteShareShouldGet - _termsResults.SiteShare > 0) //if the SiteGroup needs a shortfall guarantor for the share
                                    {
                                        if (_termsInfo.SecGroupGuarantor == TERMS_PERCENTAGE) //if the site can cover the shortfall out of shares then
                                        {
                                            //take a % of shortfall from Site
                                            //find out if the site is the only percentage guarantor left
                                            if ((_termsInfo.SupplierGuarantor == TERMS_PERCENTAGE && _termsInfo.SupplierUse > _termsInfo.SecGroupUse)
                                                || (_termsInfo.GroupGuarantor == TERMS_PERCENTAGE && _termsInfo.GroupUse > _termsInfo.SecGroupUse))
                                            {
                                                temp = _termsInfo.SecGroupGuarantorPercentage / 100;
                                                temp = temp * (_termsResults.SiteShareShouldGet - _termsResults.SiteShare);
                                                _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare - Format2DPs(temp.GetValueOrDefault());
                                                _termsResults.SiteShare = _termsResults.SiteShare + Format2DPs(temp.GetValueOrDefault());
                                                temp = 0;
                                            }
                                            else //there is no other shortfall guarantor, therefore the remainder is to be paid to bring up to the min
                                            {
                                                _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare - (_termsResults.SiteShareShouldGet - _termsResults.SiteShare);
                                                _termsResults.SiteShare = _termsResults.SiteShareShouldGet;
                                            }
                                        }
                                        else if (_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER ||
                                                    (_termsResults.SecSiteGroupShare > (_termsResults.SiteShareShouldGet - _termsResults.SiteShare)))
                                        {
                                            _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare - (_termsResults.SiteShareShouldGet - _termsResults.SiteShare);
                                            _termsResults.SiteShare = _termsResults.SiteShareShouldGet;
                                        }
                                        else
                                        {
                                            //site shares cover min until they get to zero
                                            _termsResults.SiteShare = _termsResults.SiteShare + _termsResults.SecSiteGroupShare;
                                            _termsResults.SecSiteGroupShare = 0;
                                        }
                                    }

                                    //if there is still a shortfall on the minimum
                                    if (_termsResults.SiteShareShouldGet - _termsResults.SiteShare > 0)
                                    {
                                        //now look at the site's rent
                                        if (_termsInfo.SecGroupGuarantor == TERMS_PERCENTAGE) //shortfall is already taken care of
                                        { }
                                        else if (_termsInfo.SecGroupGuarantor == TERMS_PRIMARY_FULL || _termsInfo.SecGroupGuarantor == TERMS_SECONDARY_REMAINDER)  //do nothing - already got the shortfall
                                        { }
                                        else if (_termsResults.SecSiteGroupRent > (_termsResults.SiteShareShouldGet - _termsResults.SiteShare)) //to zero
                                        {
                                            _termsResults.SecSiteGroupRent = _termsResults.SecSiteGroupRent - (_termsResults.SiteShareShouldGet - _termsResults.SiteShare);
                                            _termsResults.SiteShare = _termsResults.SiteShareShouldGet;
                                        }
                                        else //site shares cover min until they get to zero
                                        {
                                            _termsResults.SiteShare = _termsResults.SiteShare + _termsResults.SecSiteGroupRent;
                                            _termsResults.SecSiteGroupRent = 0;
                                        }
                                    }
                                }
                                break;
                            case TERMS_FRONT_MONEY:
                                if (_termsInfo.SiteValueGuaranteed.GetValueOrDefault())
                                {
                                    if (_termsResults.SiteRentShouldGet - _termsResults.SiteRent > 0) //if the SiteGroup needs a shortfall guarantor for the rent
                                    {
                                        if (_termsInfo.SecGroupGuarantor == TERMS_PERCENTAGE) //if the site can cover the shortfall out of shares then
                                        {
                                            //take a % of shortfall from Site
                                            //find out if the site is the only percentage guarantor left
                                            if ((_termsInfo.SupplierGuarantor == TERMS_PERCENTAGE && _termsInfo.SupplierUse > _termsInfo.SecGroupUse)
                                                || (_termsInfo.GroupGuarantor == TERMS_PERCENTAGE && _termsInfo.GroupUse > _termsInfo.SecGroupUse))
                                            {
                                                temp = _termsInfo.SecGroupGuarantorPercentage / 100;
                                                temp = temp * (_termsResults.SiteRentShouldGet - _termsResults.SiteRent);
                                                _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare - Format2DPs(temp.GetValueOrDefault());
                                                _termsResults.SiteRent = _termsResults.SiteRent + Format2DPs(temp.GetValueOrDefault());
                                                temp = 0;
                                            }
                                            else //there is no other shortfall guarantor, therefore the remainder is to be paid to bring up to the min
                                            {
                                                _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare - (_termsResults.SiteRentShouldGet - _termsResults.SiteRent);
                                                _termsResults.SiteRent = _termsResults.SiteRentShouldGet;
                                            }
                                        }
                                        else if (_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER ||
                                                    (_termsResults.SecSiteGroupShare > (_termsResults.SiteRentShouldGet - _termsResults.SiteRent)))
                                        {
                                            _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare - (_termsResults.SiteRentShouldGet - _termsResults.SiteRent);
                                            _termsResults.SiteRent = _termsResults.SiteRentShouldGet;
                                        }
                                        else
                                        {
                                            //site shares cover min until they get to zero
                                            _termsResults.SiteRent = _termsResults.SiteRent + _termsResults.SecSiteGroupShare;
                                            _termsResults.SecSiteGroupShare = 0;
                                        }
                                    }

                                    //if there is still a shortfall on the rent
                                    if (_termsResults.SiteRentShouldGet - _termsResults.SiteRent > 0)
                                    {
                                        //now look at the site's rent
                                        if (_termsInfo.SecGroupGuarantor == TERMS_PERCENTAGE) //shortfall is already taken care of
                                        { }
                                        else if (_termsInfo.SecGroupGuarantor == TERMS_PRIMARY_FULL || _termsInfo.SecGroupGuarantor == TERMS_SECONDARY_REMAINDER)  //do nothing - already got the shortfall
                                        { }
                                        else if (_termsResults.SecSiteGroupRent > (_termsResults.SiteRentShouldGet - _termsResults.SiteRent)) //to zero
                                        {
                                            _termsResults.SecSiteGroupRent = _termsResults.SecSiteGroupRent - (_termsResults.SiteRentShouldGet - _termsResults.SiteRent);
                                            _termsResults.SiteRent = _termsResults.SiteRentShouldGet;
                                        }
                                        else //site shares cover min until they get to zero
                                        {
                                            _termsResults.SiteRent = _termsResults.SiteRent + _termsResults.SecSiteGroupRent;
                                            _termsResults.SecSiteGroupRent = 0;
                                        }
                                    }
                                }

                                //Now check if the share is guaranteed
                                if (_termsInfo.SiteShareGuaranteed.GetValueOrDefault())
                                {
                                    if (_termsResults.SiteShareShouldGet - _termsResults.SiteShare > 0) //if the SiteGroup needs a shortfall guarantor for the min
                                    {
                                        if (_termsInfo.SecGroupGuarantor == TERMS_PERCENTAGE) //if the site can cover the shortfall out of shares then
                                        {
                                            //take a % of shortfall from Site
                                            //find out if the site is the only percentage guarantor left
                                            if ((_termsInfo.SupplierGuarantor == TERMS_PERCENTAGE && _termsInfo.SupplierUse > _termsInfo.SecGroupUse)
                                                || (_termsInfo.GroupGuarantor == TERMS_PERCENTAGE && _termsInfo.GroupUse > _termsInfo.SecGroupUse))
                                            {
                                                temp = _termsInfo.SecGroupGuarantorPercentage / 100;
                                                temp = temp * (_termsResults.SiteShareShouldGet - _termsResults.SiteShare);
                                                _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare - Format2DPs(temp.GetValueOrDefault());
                                                _termsResults.SiteShare = _termsResults.SiteShare + Format2DPs(temp.GetValueOrDefault());
                                                temp = 0;
                                            }
                                            else //there is no other shortfall guarantor, therefore the remainder is to be paid to bring up to the min
                                            {
                                                _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare - (_termsResults.SiteShareShouldGet - _termsResults.SiteShare);
                                                _termsResults.SiteShare = _termsResults.SiteShareShouldGet;
                                            }
                                        }
                                        else if (_termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_PRIMARY_FULL || _termsInfo.SecGroupGuarantor.GetValueOrDefault() == TERMS_SECONDARY_REMAINDER ||
                                                    (_termsResults.SecSiteGroupShare > (_termsResults.SiteShareShouldGet - _termsResults.SiteShare)))
                                        {
                                            _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare - (_termsResults.SiteShareShouldGet - _termsResults.SiteShare);
                                            _termsResults.SiteShare = _termsResults.SiteShareShouldGet;
                                        }
                                        else
                                        {
                                            //site shares cover min until they get to zero
                                            _termsResults.SiteShare = _termsResults.SiteShare + _termsResults.SecSiteGroupShare;
                                            _termsResults.SecSiteGroupShare = 0;
                                        }
                                    }

                                    //if there is still a shortfall on the minimum
                                    //take it out of the site's rent
                                    if (_termsResults.SiteShareShouldGet - _termsResults.SiteShare > 0)
                                    {
                                        //now look at the site's rent
                                        if (_termsInfo.SecGroupGuarantor == TERMS_PERCENTAGE) //shortfall is already taken care of
                                        { }
                                        else if (_termsInfo.SecGroupGuarantor == TERMS_PRIMARY_FULL || _termsInfo.SecGroupGuarantor == TERMS_SECONDARY_REMAINDER)  //do nothing - already got the shortfall
                                        { }
                                        else if (_termsResults.SecSiteGroupRent > (_termsResults.SiteShareShouldGet - _termsResults.SiteShare)) //to zero
                                        {
                                            _termsResults.SecSiteGroupRent = _termsResults.SecSiteGroupRent - (_termsResults.SiteShareShouldGet - _termsResults.SiteShare);
                                            _termsResults.SiteShare = _termsResults.SiteShareShouldGet;
                                        }
                                        else //site shares cover min until they get to zero
                                        {
                                            _termsResults.SiteShare = _termsResults.SiteShare + _termsResults.SecSiteGroupRent;
                                            _termsResults.SecSiteGroupRent = 0;
                                        }
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void UseSiteForSupplierVATShortfall()
        {
            float? _temp = 0;
            bool? _isLastPercentage = false;

            try
            {
                LogManager.WriteLog("Inside UseSiteForSupplierVATShortfall...", LogManager.enumLogLevel.Info);

                switch (_termsInfo.SiteGuarantor)
                {
                    case TERMS_PRIMARY_TO_ZERO:
                    case TERMS_SECONDARY_TO_ZERO:
                        //try shares first
                        if ((_termsResults.SupplierVatShouldGet - _termsResults.SupplierVat) > _termsResults.SiteShare)
                        {
                            _termsResults.SupplierVat = _termsResults.SupplierVat + _termsResults.SiteShare;
                            _termsResults.SiteShare = 0;
                        }
                        else
                        {
                            _termsResults.SiteShare = _termsResults.SiteShare - (_termsResults.SupplierVatShouldGet - _termsResults.SupplierVat);
                            _termsResults.SupplierVat = _termsResults.SupplierVatShouldGet;
                        }

                        //then rent
                        if ((_termsResults.SupplierVatShouldGet - _termsResults.SupplierVat) > _termsResults.SiteRent)
                        {
                            _termsResults.SupplierVat = _termsResults.SupplierVat + _termsResults.SiteRent;
                            _termsResults.SiteShare = 0;
                        }
                        else
                        {
                            _termsResults.SiteRent = _termsResults.SiteRent - (_termsResults.SupplierVatShouldGet - _termsResults.SupplierVat);
                            _termsResults.SupplierVat = _termsResults.SupplierVatShouldGet;
                        }
                        break;
                    case TERMS_PRIMARY_FULL:
                    case TERMS_SECONDARY_REMAINDER:
                        _termsResults.SiteShare = _termsResults.SiteShare - (_termsResults.SupplierVatShouldGet - _termsResults.SupplierVat);
                        _termsResults.SupplierVat = _termsResults.SupplierVatShouldGet;
                        break;
                    case TERMS_PERCENTAGE:
                        _temp = 0;
                        _isLastPercentage = true;

                        if (_termsInfo.SiteUse > 0 && (!(_termsInfo.SiteShareGuaranteed.GetValueOrDefault() || _termsInfo.SiteValueGuaranteed.GetValueOrDefault())) && _termsInfo.SiteGuarantor == TERMS_PERCENTAGE)
                            _temp = _temp + _termsInfo.SiteGuarantorPercentage;

                        if (_termsInfo.SupplierUse > 0 && (!(_termsInfo.SupplierShareGuaranteed.GetValueOrDefault() || _termsInfo.SupplierValueGuaranteed.GetValueOrDefault())) && _termsInfo.SupplierGuarantor == TERMS_PERCENTAGE)
                        {
                            _temp = _temp + _termsInfo.SupplierGuarantorPercentage;
                            if (_termsInfo.SupplierUse > _termsInfo.SiteUse)
                                _isLastPercentage = false;
                        }

                        if (_termsInfo.GroupUse > 0 && (!(_termsInfo.GroupShareGuaranteed.GetValueOrDefault() || _termsInfo.GroupValueGuaranteed.GetValueOrDefault())) && _termsInfo.GroupGuarantor == TERMS_PERCENTAGE)
                        {
                            _temp = _temp + _termsInfo.GroupGuarantorPercentage;
                            if (_termsInfo.GroupUse > _termsInfo.SiteUse)
                                _isLastPercentage = false;
                        }

                        if (_termsInfo.SecGroupUse > 0 && (!(_termsInfo.SecGroupShareGuaranteed.GetValueOrDefault() || _termsInfo.SecGroupValueGuaranteed.GetValueOrDefault())) && _termsInfo.SecGroupGuarantor == TERMS_PERCENTAGE)
                        {
                            _temp = _temp + _termsInfo.SecGroupGuarantorPercentage;
                            if (_termsInfo.SecGroupUse > _termsInfo.SiteUse)
                                _isLastPercentage = false;
                        }

                        if (_isLastPercentage.GetValueOrDefault())
                        {
                            _termsResults.SiteRent = _termsResults.SiteRent - (_termsResults.SupplierVatShouldGet - _termsResults.SupplierVat);
                            _termsResults.SupplierVat = _termsResults.SupplierVatShouldGet;
                        }
                        else
                        {
                            _termsResults.SupplierVat = _termsResults.SupplierVat + ((_termsResults.SupplierVatShouldGet - _termsResults.SupplierVat) * (_termsInfo.SiteGuarantorPercentage / _temp));
                            _termsResults.SiteRent = _termsResults.SiteRent - ((_termsResults.SupplierVatShouldGet - _termsResults.SupplierVat) * (_termsInfo.SiteGuarantorPercentage / _temp));
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void UseSiteGroupForSupplierVATShortfall()
        {
            float? _temp = 0;
            bool? _isLastPercentage = false;

            try
            {
                LogManager.WriteLog("Inside UseSiteGroupForSupplierVATShortfall...", LogManager.enumLogLevel.Info);

                switch (_termsInfo.GroupGuarantor)
                {
                    case TERMS_PRIMARY_TO_ZERO:
                    case TERMS_SECONDARY_TO_ZERO:
                        //try shares first
                        if ((_termsResults.SupplierVatShouldGet - _termsResults.SupplierVat) > _termsResults.SiteGroupShare)
                        {
                            _termsResults.SupplierVat = _termsResults.SupplierVat + _termsResults.SiteGroupShare;
                            _termsResults.SiteGroupShare = 0;
                        }
                        else
                        {
                            _termsResults.SiteGroupShare = _termsResults.SiteGroupShare - (_termsResults.SupplierVatShouldGet - _termsResults.SupplierVat);
                            _termsResults.SupplierVat = _termsResults.SupplierVatShouldGet;
                        }

                        //then rent
                        if ((_termsResults.SupplierVatShouldGet - _termsResults.SupplierVat) > _termsResults.SiteGroupRent)
                        {
                            _termsResults.SupplierVat = _termsResults.SupplierVat + _termsResults.SiteGroupRent;
                            _termsResults.SiteGroupShare = 0;
                        }
                        else
                        {
                            _termsResults.SiteGroupRent = _termsResults.SiteGroupRent - (_termsResults.SupplierVatShouldGet - _termsResults.SupplierVat);
                            _termsResults.SupplierVat = _termsResults.SupplierVatShouldGet;
                        }
                        break;
                    case TERMS_PRIMARY_FULL:
                    case TERMS_SECONDARY_REMAINDER:
                        _termsResults.SiteGroupShare = _termsResults.SiteGroupShare - (_termsResults.SupplierVatShouldGet - _termsResults.SupplierVat);
                        _termsResults.SupplierVat = _termsResults.SupplierVatShouldGet;
                        break;
                    case TERMS_PERCENTAGE:
                        _temp = 0;
                        _isLastPercentage = true;

                        if (_termsInfo.SiteUse > 0 && (!(_termsInfo.SiteShareGuaranteed.GetValueOrDefault() || _termsInfo.SiteValueGuaranteed.GetValueOrDefault())) && _termsInfo.SiteGuarantor == TERMS_PERCENTAGE)
                        {
                            _temp = _temp + _termsInfo.SiteGuarantorPercentage;
                            if (_termsInfo.SiteUse > _termsInfo.GroupUse)
                                _isLastPercentage = false;
                        }

                        if (_termsInfo.SupplierUse > 0 && (!(_termsInfo.SupplierShareGuaranteed.GetValueOrDefault() || _termsInfo.SupplierValueGuaranteed.GetValueOrDefault())) && _termsInfo.SupplierGuarantor == TERMS_PERCENTAGE)
                        {
                            _temp = _temp + _termsInfo.SupplierGuarantorPercentage;
                            if (_termsInfo.SupplierUse > _termsInfo.GroupUse)
                                _isLastPercentage = false;
                        }

                        if (_termsInfo.GroupUse > 0 && (!(_termsInfo.GroupShareGuaranteed.GetValueOrDefault() || _termsInfo.GroupValueGuaranteed.GetValueOrDefault())) && _termsInfo.GroupGuarantor == TERMS_PERCENTAGE)
                            _temp = _temp + _termsInfo.GroupGuarantorPercentage;

                        if (_termsInfo.SecGroupUse > 0 && (!(_termsInfo.SecGroupShareGuaranteed.GetValueOrDefault() || _termsInfo.SecGroupValueGuaranteed.GetValueOrDefault())) && _termsInfo.SecGroupGuarantor == TERMS_PERCENTAGE)
                        {
                            _temp = _temp + _termsInfo.SecGroupGuarantorPercentage;
                            if (_termsInfo.SecGroupUse > _termsInfo.GroupUse)
                                _isLastPercentage = false;
                        }

                        if (_isLastPercentage.GetValueOrDefault())
                        {
                            _termsResults.SiteGroupRent = _termsResults.SiteGroupRent - (_termsResults.SupplierVatShouldGet - _termsResults.SupplierVat);
                            _termsResults.SupplierVat = _termsResults.SupplierVatShouldGet;
                        }
                        else
                        {
                            _termsResults.SupplierVat = _termsResults.SupplierVat + ((_termsResults.SupplierVatShouldGet - _termsResults.SupplierVat) * (_termsInfo.GroupGuarantorPercentage / _temp));
                            _termsResults.SiteGroupRent = _termsResults.SiteGroupRent - ((_termsResults.SupplierVatShouldGet - _termsResults.SupplierVat) * (_termsInfo.GroupGuarantorPercentage / _temp));
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void UseSecSiteGroupForSupplierVATShortfall()
        {
            float? _temp = 0;
            bool? _isLastPercentage = false;

            try
            {
                LogManager.WriteLog("Inside UseSecSiteGroupForSupplierVATShortfall...", LogManager.enumLogLevel.Info);

                switch (_termsInfo.SecGroupGuarantor)
                {
                    case TERMS_PRIMARY_TO_ZERO:
                    case TERMS_SECONDARY_TO_ZERO:
                        //try shares first
                        if ((_termsResults.SupplierVatShouldGet - _termsResults.SupplierVat) > _termsResults.SecSiteGroupShare)
                        {
                            _termsResults.SupplierVat = _termsResults.SupplierVat + _termsResults.SecSiteGroupShare;
                            _termsResults.SecSiteGroupShare = 0;
                        }
                        else
                        {
                            _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare - (_termsResults.SupplierVatShouldGet - _termsResults.SupplierVat);
                            _termsResults.SupplierVat = _termsResults.SupplierVatShouldGet;
                        }

                        //then rent
                        if ((_termsResults.SupplierVatShouldGet - _termsResults.SupplierVat) > _termsResults.SecSiteGroupRent)
                        {
                            _termsResults.SupplierVat = _termsResults.SupplierVat + _termsResults.SecSiteGroupRent;
                            _termsResults.SecSiteGroupShare = 0;
                        }
                        else
                        {
                            _termsResults.SecSiteGroupRent = _termsResults.SecSiteGroupRent - (_termsResults.SupplierVatShouldGet - _termsResults.SupplierVat);
                            _termsResults.SupplierVat = _termsResults.SupplierVatShouldGet;
                        }
                        break;
                    case TERMS_PRIMARY_FULL:
                    case TERMS_SECONDARY_REMAINDER:
                        _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare - (_termsResults.SupplierVatShouldGet - _termsResults.SupplierVat);
                        _termsResults.SupplierVat = _termsResults.SupplierVatShouldGet;
                        break;
                    case TERMS_PERCENTAGE:
                        _temp = 0;
                        _isLastPercentage = true;

                        if (_termsInfo.SiteUse > 0 && (!(_termsInfo.SiteShareGuaranteed.GetValueOrDefault() || _termsInfo.SiteValueGuaranteed.GetValueOrDefault())) && _termsInfo.SiteGuarantor == TERMS_PERCENTAGE)
                        {
                            _temp = _temp + _termsInfo.SiteGuarantorPercentage;
                            if (_termsInfo.SiteUse > _termsInfo.SecGroupUse)
                                _isLastPercentage = false;
                        }

                        if (_termsInfo.SupplierUse > 0 && (!(_termsInfo.SupplierShareGuaranteed.GetValueOrDefault() || _termsInfo.SupplierValueGuaranteed.GetValueOrDefault())) && _termsInfo.SupplierGuarantor == TERMS_PERCENTAGE)
                        {
                            _temp = _temp + _termsInfo.SupplierGuarantorPercentage;
                            if (_termsInfo.SupplierUse > _termsInfo.SecGroupUse)
                                _isLastPercentage = false;
                        }

                        if (_termsInfo.GroupUse > 0 && (!(_termsInfo.GroupShareGuaranteed.GetValueOrDefault() || _termsInfo.GroupValueGuaranteed.GetValueOrDefault())) && _termsInfo.GroupGuarantor == TERMS_PERCENTAGE)
                        {
                            _temp = _temp + _termsInfo.GroupGuarantorPercentage;
                            if (_termsInfo.GroupUse > _termsInfo.SecGroupUse)
                                _isLastPercentage = false;
                        }

                        if (_termsInfo.SecGroupUse > 0 && (!(_termsInfo.SecGroupShareGuaranteed.GetValueOrDefault() || _termsInfo.SecGroupValueGuaranteed.GetValueOrDefault())) && _termsInfo.SecGroupGuarantor == TERMS_PERCENTAGE)
                            _temp = _temp + _termsInfo.SecGroupGuarantorPercentage;

                        if (_isLastPercentage.GetValueOrDefault())
                        {
                            _termsResults.SecSiteGroupRent = _termsResults.SecSiteGroupRent - (_termsResults.SupplierVatShouldGet - _termsResults.SupplierVat);
                            _termsResults.SupplierVat = _termsResults.SupplierVatShouldGet;
                        }
                        else
                        {
                            _termsResults.SupplierVat = _termsResults.SupplierVat + ((_termsResults.SupplierVatShouldGet - _termsResults.SupplierVat) * (_termsInfo.GroupGuarantorPercentage / _temp));
                            _termsResults.SecSiteGroupRent = _termsResults.SecSiteGroupRent - ((_termsResults.SupplierVatShouldGet - _termsResults.SupplierVat) * (_termsInfo.GroupGuarantorPercentage / _temp));
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void UseSupplierForSiteVATShortfall()
        {
            float? _temp = 0;
            bool? _isLastPercentage = false;

            try
            {
                LogManager.WriteLog("Inside UseSupplierForSiteVATShortfall...", LogManager.enumLogLevel.Info);

                switch (_termsInfo.SupplierGuarantor)
                {
                    case TERMS_PRIMARY_TO_ZERO:
                    case TERMS_SECONDARY_TO_ZERO:
                        //try shares first
                        if ((_termsResults.SiteVatShouldGet - _termsResults.SiteVat) > _termsResults.SupplierShare)
                        {
                            _termsResults.SiteVat = _termsResults.SiteVat + _termsResults.SupplierShare;
                            _termsResults.SupplierShare = 0;
                        }
                        else
                        {
                            _termsResults.SupplierShare = _termsResults.SupplierShare - (_termsResults.SiteVatShouldGet - _termsResults.SiteVat);
                            _termsResults.SiteVat = _termsResults.SiteVatShouldGet;
                        }

                        //then rent
                        if ((_termsResults.SiteVatShouldGet - _termsResults.SiteVat) > _termsResults.SupplierRent)
                        {
                            _termsResults.SiteVat = _termsResults.SiteVat + _termsResults.SupplierRent;
                            _termsResults.SupplierShare = 0;
                        }
                        else
                        {
                            _termsResults.SupplierRent = _termsResults.SupplierRent - (_termsResults.SiteVatShouldGet - _termsResults.SiteVat);
                            _termsResults.SiteVat = _termsResults.SiteVatShouldGet;
                        }
                        break;
                    case TERMS_PRIMARY_FULL:
                    case TERMS_SECONDARY_REMAINDER:
                        _termsResults.SupplierShare = _termsResults.SupplierShare - (_termsResults.SiteVatShouldGet - _termsResults.SiteVat);
                        _termsResults.SiteVat = _termsResults.SiteVatShouldGet;
                        break;
                    case TERMS_PERCENTAGE:
                        _temp = 0;
                        _isLastPercentage = true;

                        if (_termsInfo.SupplierUse > 0 && (!(_termsInfo.SupplierShareGuaranteed.GetValueOrDefault() || _termsInfo.SupplierValueGuaranteed.GetValueOrDefault())) && _termsInfo.SupplierGuarantor == TERMS_PERCENTAGE)
                            _temp = _temp + _termsInfo.SupplierGuarantorPercentage;

                        if (_termsInfo.SiteUse > 0 && (!(_termsInfo.SiteShareGuaranteed.GetValueOrDefault() || _termsInfo.SiteValueGuaranteed.GetValueOrDefault())) && _termsInfo.SiteGuarantor == TERMS_PERCENTAGE)
                        {
                            _temp = _temp + _termsInfo.SiteGuarantorPercentage;
                            if (_termsInfo.SiteUse > _termsInfo.SupplierUse)
                                _isLastPercentage = false;
                        }

                        if (_termsInfo.GroupUse > 0 && (!(_termsInfo.GroupShareGuaranteed.GetValueOrDefault() || _termsInfo.GroupValueGuaranteed.GetValueOrDefault())) && _termsInfo.GroupGuarantor == TERMS_PERCENTAGE)
                        {
                            _temp = _temp + _termsInfo.GroupGuarantorPercentage;
                            if (_termsInfo.GroupUse > _termsInfo.SupplierUse)
                                _isLastPercentage = false;
                        }

                        if (_termsInfo.SecGroupUse > 0 && (!(_termsInfo.SecGroupShareGuaranteed.GetValueOrDefault() || _termsInfo.SecGroupValueGuaranteed.GetValueOrDefault())) && _termsInfo.SecGroupGuarantor == TERMS_PERCENTAGE)
                        {
                            _temp = _temp + _termsInfo.SecGroupGuarantorPercentage;
                            if (_termsInfo.SecGroupUse > _termsInfo.SupplierUse)
                                _isLastPercentage = false;
                        }

                        if (_isLastPercentage.GetValueOrDefault())
                        {
                            _termsResults.SupplierRent = _termsResults.SupplierRent - (_termsResults.SiteVatShouldGet - _termsResults.SiteVat);
                            _termsResults.SiteVat = _termsResults.SiteVatShouldGet;
                        }
                        else
                        {
                            _termsResults.SiteVat = _termsResults.SiteVat + ((_termsResults.SiteVatShouldGet - _termsResults.SiteVat) * (_termsInfo.SupplierGuarantorPercentage / _temp));
                            _termsResults.SupplierRent = _termsResults.SupplierRent - ((_termsResults.SiteVatShouldGet - _termsResults.SiteVat) * (_termsInfo.SupplierGuarantorPercentage / _temp));
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void UseSiteGroupForSiteVATShortfall()
        {
            float? _temp = 0;
            bool? _isLastPercentage = false;

            try
            {
                LogManager.WriteLog("Inside UseSiteGroupForSiteVATShortfall...", LogManager.enumLogLevel.Info);

                switch (_termsInfo.GroupGuarantor)
                {
                    case TERMS_PRIMARY_TO_ZERO:
                    case TERMS_SECONDARY_TO_ZERO:
                        //try shares first
                        if ((_termsResults.SiteVatShouldGet - _termsResults.SiteVat) > _termsResults.SiteGroupShare)
                        {
                            _termsResults.SiteVat = _termsResults.SiteVat + _termsResults.SiteGroupShare;
                            _termsResults.SiteGroupShare = 0;
                        }
                        else
                        {
                            _termsResults.SiteGroupShare = _termsResults.SiteGroupShare - (_termsResults.SiteVatShouldGet - _termsResults.SiteVat);
                            _termsResults.SiteVat = _termsResults.SiteVatShouldGet;
                        }

                        //then rent
                        if ((_termsResults.SiteVatShouldGet - _termsResults.SiteVat) > _termsResults.SiteGroupRent)
                        {
                            _termsResults.SiteVat = _termsResults.SiteVat + _termsResults.SiteGroupRent;
                            _termsResults.SiteGroupShare = 0;
                        }
                        else
                        {
                            _termsResults.SiteGroupRent = _termsResults.SiteGroupRent - (_termsResults.SiteVatShouldGet - _termsResults.SiteVat);
                            _termsResults.SiteVat = _termsResults.SiteVatShouldGet;
                        }
                        break;
                    case TERMS_PRIMARY_FULL:
                    case TERMS_SECONDARY_REMAINDER:
                        _termsResults.SiteGroupShare = _termsResults.SiteGroupShare - (_termsResults.SiteVatShouldGet - _termsResults.SiteVat);
                        _termsResults.SiteVat = _termsResults.SiteVatShouldGet;
                        break;
                    case TERMS_PERCENTAGE:
                        _temp = 0;
                        _isLastPercentage = true;

                        if (_termsInfo.SupplierUse > 0 && (!(_termsInfo.SupplierShareGuaranteed.GetValueOrDefault() || _termsInfo.SupplierValueGuaranteed.GetValueOrDefault())) && _termsInfo.SupplierGuarantor == TERMS_PERCENTAGE)
                        {
                            _temp = _temp + _termsInfo.SupplierGuarantorPercentage;
                            if (_termsInfo.SupplierUse > _termsInfo.GroupUse)
                                _isLastPercentage = false;
                        }

                        if (_termsInfo.SiteUse > 0 && (!(_termsInfo.SiteShareGuaranteed.GetValueOrDefault() || _termsInfo.SiteValueGuaranteed.GetValueOrDefault())) && _termsInfo.SiteGuarantor == TERMS_PERCENTAGE)
                        {
                            _temp = _temp + _termsInfo.SiteGuarantorPercentage;
                            if (_termsInfo.SiteUse > _termsInfo.GroupUse)
                                _isLastPercentage = false;
                        }

                        if (_termsInfo.GroupUse > 0 && (!(_termsInfo.GroupShareGuaranteed.GetValueOrDefault() || _termsInfo.GroupValueGuaranteed.GetValueOrDefault())) && _termsInfo.GroupGuarantor == TERMS_PERCENTAGE)
                            _temp = _temp + _termsInfo.GroupGuarantorPercentage;

                        if (_termsInfo.SecGroupUse > 0 && (!(_termsInfo.SecGroupShareGuaranteed.GetValueOrDefault() || _termsInfo.SecGroupValueGuaranteed.GetValueOrDefault())) && _termsInfo.SecGroupGuarantor == TERMS_PERCENTAGE)
                        {
                            _temp = _temp + _termsInfo.SecGroupGuarantorPercentage;
                            if (_termsInfo.SecGroupUse > _termsInfo.GroupUse)
                                _isLastPercentage = false;
                        }

                        if (_isLastPercentage.GetValueOrDefault())
                        {
                            _termsResults.SiteGroupRent = _termsResults.SiteGroupRent - (_termsResults.SiteVatShouldGet - _termsResults.SiteVat);
                            _termsResults.SiteVat = _termsResults.SiteVatShouldGet;
                        }
                        else
                        {
                            _termsResults.SiteVat = _termsResults.SiteVat + ((_termsResults.SiteVatShouldGet - _termsResults.SiteVat) * (_termsInfo.GroupGuarantorPercentage / _temp));
                            _termsResults.SiteGroupRent = _termsResults.SiteGroupRent - ((_termsResults.SiteVatShouldGet - _termsResults.SiteVat) * (_termsInfo.GroupGuarantorPercentage / _temp));
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void UseSecSiteGroupForSiteVATShortfall()
        {
            float? _temp = 0;
            bool? _isLastPercentage = false;

            try
            {
                LogManager.WriteLog("Inside UseSecSiteGroupForSiteVATShortfall...", LogManager.enumLogLevel.Info);

                switch (_termsInfo.SecGroupGuarantor)
                {
                    case TERMS_PRIMARY_TO_ZERO:
                    case TERMS_SECONDARY_TO_ZERO:
                        //try shares first
                        if ((_termsResults.SiteVatShouldGet - _termsResults.SiteVat) > _termsResults.SecSiteGroupShare)
                        {
                            _termsResults.SiteVat = _termsResults.SiteVat + _termsResults.SecSiteGroupShare;
                            _termsResults.SecSiteGroupShare = 0;
                        }
                        else
                        {
                            _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare - (_termsResults.SiteVatShouldGet - _termsResults.SiteVat);
                            _termsResults.SiteVat = _termsResults.SiteVatShouldGet;
                        }

                        //then rent
                        if ((_termsResults.SiteVatShouldGet - _termsResults.SiteVat) > _termsResults.SecSiteGroupRent)
                        {
                            _termsResults.SiteVat = _termsResults.SiteVat + _termsResults.SecSiteGroupRent;
                            _termsResults.SecSiteGroupShare = 0;
                        }
                        else
                        {
                            _termsResults.SecSiteGroupRent = _termsResults.SecSiteGroupRent - (_termsResults.SiteVatShouldGet - _termsResults.SiteVat);
                            _termsResults.SiteVat = _termsResults.SiteVatShouldGet;
                        }
                        break;
                    case TERMS_PRIMARY_FULL:
                    case TERMS_SECONDARY_REMAINDER:
                        _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare - (_termsResults.SiteVatShouldGet - _termsResults.SiteVat);
                        _termsResults.SiteVat = _termsResults.SiteVatShouldGet;
                        break;
                    case TERMS_PERCENTAGE:
                        _temp = 0;
                        _isLastPercentage = true;

                        if (_termsInfo.SupplierUse > 0 && (!(_termsInfo.SupplierShareGuaranteed.GetValueOrDefault() || _termsInfo.SupplierValueGuaranteed.GetValueOrDefault())) && _termsInfo.SupplierGuarantor == TERMS_PERCENTAGE)
                        {
                            _temp = _temp + _termsInfo.SupplierGuarantorPercentage;
                            if (_termsInfo.SupplierUse > _termsInfo.SecGroupUse)
                                _isLastPercentage = false;
                        }

                        if (_termsInfo.SiteUse > 0 && (!(_termsInfo.SiteShareGuaranteed.GetValueOrDefault() || _termsInfo.SiteValueGuaranteed.GetValueOrDefault())) && _termsInfo.SiteGuarantor == TERMS_PERCENTAGE)
                        {
                            _temp = _temp + _termsInfo.SiteGuarantorPercentage;
                            if (_termsInfo.SiteUse > _termsInfo.SecGroupUse)
                                _isLastPercentage = false;
                        }

                        if (_termsInfo.GroupUse > 0 && (!(_termsInfo.GroupShareGuaranteed.GetValueOrDefault() || _termsInfo.GroupValueGuaranteed.GetValueOrDefault())) && _termsInfo.GroupGuarantor == TERMS_PERCENTAGE)
                        {
                            _temp = _temp + _termsInfo.GroupGuarantorPercentage;
                            if (_termsInfo.GroupUse > _termsInfo.SecGroupUse)
                                _isLastPercentage = false;
                        }

                        if (_termsInfo.SecGroupUse > 0 && (!(_termsInfo.SecGroupShareGuaranteed.GetValueOrDefault() || _termsInfo.SecGroupValueGuaranteed.GetValueOrDefault())) && _termsInfo.SecGroupGuarantor == TERMS_PERCENTAGE)
                            _temp = _temp + _termsInfo.SecGroupGuarantorPercentage;

                        if (_isLastPercentage.GetValueOrDefault())
                        {
                            _termsResults.SecSiteGroupRent = _termsResults.SecSiteGroupRent - (_termsResults.SiteVatShouldGet - _termsResults.SiteVat);
                            _termsResults.SiteVat = _termsResults.SiteVatShouldGet;
                        }
                        else
                        {
                            _termsResults.SiteVat = _termsResults.SiteVat + ((_termsResults.SiteVatShouldGet - _termsResults.SiteVat) * (_termsInfo.GroupGuarantorPercentage / _temp));
                            _termsResults.SecSiteGroupRent = _termsResults.SecSiteGroupRent - ((_termsResults.SiteVatShouldGet - _termsResults.SiteVat) * (_termsInfo.GroupGuarantorPercentage / _temp));
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void UseSiteForSiteGroupVATShortfall()
        {
            float? _temp = 0;
            bool? _isLastPercentage = false;

            try
            {
                LogManager.WriteLog("Inside UseSiteForSiteGroupVATShortfall...", LogManager.enumLogLevel.Info);

                switch (_termsInfo.SiteGuarantor)
                {
                    case TERMS_PRIMARY_TO_ZERO:
                    case TERMS_SECONDARY_TO_ZERO:
                        //try shares first
                        if ((_termsResults.SiteGroupVatShouldGet - _termsResults.SiteGroupVat) > _termsResults.SiteShare)
                        {
                            _termsResults.SiteGroupVat = _termsResults.SiteGroupVat + _termsResults.SiteShare;
                            _termsResults.SiteShare = 0;
                        }
                        else
                        {
                            _termsResults.SiteShare = _termsResults.SiteShare - (_termsResults.SiteGroupVatShouldGet - _termsResults.SiteGroupVat);
                            _termsResults.SiteGroupVat = _termsResults.SiteGroupVatShouldGet;
                        }

                        //then rent
                        if ((_termsResults.SiteGroupVatShouldGet - _termsResults.SiteGroupVat) > _termsResults.SiteRent)
                        {
                            _termsResults.SiteGroupVat = _termsResults.SiteGroupVat + _termsResults.SiteRent;
                            _termsResults.SiteShare = 0;
                        }
                        else
                        {
                            _termsResults.SiteRent = _termsResults.SiteRent - (_termsResults.SiteGroupVatShouldGet - _termsResults.SiteGroupVat);
                            _termsResults.SiteGroupVat = _termsResults.SiteGroupVatShouldGet;
                        }
                        break;
                    case TERMS_PRIMARY_FULL:
                    case TERMS_SECONDARY_REMAINDER:
                        _termsResults.SiteShare = _termsResults.SiteShare - (_termsResults.SiteGroupVatShouldGet - _termsResults.SiteGroupVat);
                        _termsResults.SiteGroupVat = _termsResults.SiteGroupVatShouldGet;
                        break;
                    case TERMS_PERCENTAGE:
                        _temp = 0;
                        _isLastPercentage = true;

                        if (_termsInfo.SiteUse > 0 && (!(_termsInfo.SiteShareGuaranteed.GetValueOrDefault() || _termsInfo.SiteValueGuaranteed.GetValueOrDefault())) && _termsInfo.SiteGuarantor == TERMS_PERCENTAGE)
                            _temp = _temp + _termsInfo.SiteGuarantorPercentage;

                        if (_termsInfo.GroupUse > 0 && (!(_termsInfo.GroupShareGuaranteed.GetValueOrDefault() || _termsInfo.GroupValueGuaranteed.GetValueOrDefault())) && _termsInfo.GroupGuarantor == TERMS_PERCENTAGE)
                        {
                            _temp = _temp + _termsInfo.GroupGuarantor;
                            if (_termsInfo.GroupUse > _termsInfo.SiteUse)
                                _isLastPercentage = false;
                        }

                        if (_termsInfo.SupplierUse > 0 && (!(_termsInfo.SupplierShareGuaranteed.GetValueOrDefault() || _termsInfo.SupplierValueGuaranteed.GetValueOrDefault())) && _termsInfo.SupplierGuarantor == TERMS_PERCENTAGE)
                        {
                            _temp = _temp + _termsInfo.SupplierGuarantorPercentage;
                            if (_termsInfo.SupplierUse > _termsInfo.SiteUse)
                                _isLastPercentage = false;
                        }

                        if (_termsInfo.SecGroupUse > 0 && (!(_termsInfo.SecGroupShareGuaranteed.GetValueOrDefault() || _termsInfo.SecGroupValueGuaranteed.GetValueOrDefault())) && _termsInfo.SecGroupGuarantor == TERMS_PERCENTAGE)
                        {
                            _temp = _temp + _termsInfo.SecGroupGuarantorPercentage;
                            if (_termsInfo.SecGroupUse > _termsInfo.SiteUse)
                                _isLastPercentage = false;
                        }

                        if (_isLastPercentage.GetValueOrDefault())
                        {
                            _termsResults.SiteRent = _termsResults.SiteRent - (_termsResults.SiteGroupVatShouldGet - _termsResults.SiteGroupVat);
                            _termsResults.SiteGroupVat = _termsResults.SiteGroupVatShouldGet;
                        }
                        else
                        {
                            _termsResults.SiteGroupVat = _termsResults.SiteGroupVat + ((_termsResults.SiteGroupVatShouldGet - _termsResults.SiteGroupVat) * (_termsInfo.SiteGuarantorPercentage / _temp));
                            _termsResults.SiteRent = _termsResults.SiteRent - ((_termsResults.SiteGroupVatShouldGet - _termsResults.SiteGroupVat) * (_termsInfo.SiteGuarantorPercentage / _temp));
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void UseSupplierForSiteGroupVATShortfall()
        {
            float? _temp = 0;
            bool? _isLastPercentage = false;

            try
            {
                LogManager.WriteLog("Inside UseSupplierForSiteGroupVATShortfall...", LogManager.enumLogLevel.Info);

                switch (_termsInfo.SupplierGuarantor)
                {
                    case TERMS_PRIMARY_TO_ZERO:
                    case TERMS_SECONDARY_TO_ZERO:
                        //try shares first
                        if ((_termsResults.SiteGroupVatShouldGet - _termsResults.SiteGroupVat) > _termsResults.SupplierShare)
                        {
                            _termsResults.SiteGroupVat = _termsResults.SiteGroupVat + _termsResults.SupplierShare;
                            _termsResults.SupplierShare = 0;
                        }
                        else
                        {
                            _termsResults.SupplierShare = _termsResults.SupplierShare - (_termsResults.SiteGroupVatShouldGet - _termsResults.SiteGroupVat);
                            _termsResults.SiteGroupVat = _termsResults.SiteGroupVatShouldGet;
                        }

                        //then rent
                        if ((_termsResults.SiteGroupVatShouldGet - _termsResults.SiteGroupVat) > _termsResults.SupplierRent)
                        {
                            _termsResults.SiteGroupVat = _termsResults.SiteGroupVat + _termsResults.SupplierRent;
                            _termsResults.SupplierShare = 0;
                        }
                        else
                        {
                            _termsResults.SupplierRent = _termsResults.SupplierRent - (_termsResults.SiteGroupVatShouldGet - _termsResults.SiteGroupVat);
                            _termsResults.SiteGroupVat = _termsResults.SiteGroupVatShouldGet;
                        }
                        break;
                    case TERMS_PRIMARY_FULL:
                    case TERMS_SECONDARY_REMAINDER:
                        _termsResults.SupplierShare = _termsResults.SupplierShare - (_termsResults.SiteGroupVatShouldGet - _termsResults.SiteGroupVat);
                        _termsResults.SiteGroupVat = _termsResults.SiteGroupVatShouldGet;
                        break;
                    case TERMS_PERCENTAGE:
                        _temp = 0;
                        _isLastPercentage = true;

                        if (_termsInfo.SiteUse > 0 && (!(_termsInfo.SiteShareGuaranteed.GetValueOrDefault() || _termsInfo.SiteValueGuaranteed.GetValueOrDefault())) && _termsInfo.SiteGuarantor == TERMS_PERCENTAGE)
                        {
                            _temp = _temp + _termsInfo.SiteGuarantorPercentage;
                            if (_termsInfo.SiteUse > _termsInfo.SupplierUse)
                                _isLastPercentage = false;
                        }

                        if (_termsInfo.GroupUse > 0 && (!(_termsInfo.GroupShareGuaranteed.GetValueOrDefault() || _termsInfo.GroupValueGuaranteed.GetValueOrDefault())) && _termsInfo.GroupGuarantor == TERMS_PERCENTAGE)
                        {
                            _temp = _temp + _termsInfo.GroupGuarantor;
                            if (_termsInfo.GroupUse > _termsInfo.SupplierUse)
                                _isLastPercentage = false;
                        }

                        if (_termsInfo.SupplierUse > 0 && (!(_termsInfo.SupplierShareGuaranteed.GetValueOrDefault() || _termsInfo.SupplierValueGuaranteed.GetValueOrDefault())) && _termsInfo.SupplierGuarantor == TERMS_PERCENTAGE)
                            _temp = _temp + _termsInfo.SupplierGuarantorPercentage;

                        if (_termsInfo.SecGroupUse > 0 && (!(_termsInfo.SecGroupShareGuaranteed.GetValueOrDefault() || _termsInfo.SecGroupValueGuaranteed.GetValueOrDefault())) && _termsInfo.SecGroupGuarantor == TERMS_PERCENTAGE)
                        {
                            _temp = _temp + _termsInfo.SecGroupGuarantorPercentage;
                            if (_termsInfo.SecGroupUse > _termsInfo.SupplierUse)
                                _isLastPercentage = false;
                        }

                        if (_isLastPercentage.GetValueOrDefault())
                        {
                            _termsResults.SupplierRent = _termsResults.SupplierRent - (_termsResults.SiteGroupVatShouldGet - _termsResults.SiteGroupVat);
                            _termsResults.SiteGroupVat = _termsResults.SiteGroupVatShouldGet;
                        }
                        else
                        {
                            _termsResults.SiteGroupVat = _termsResults.SiteGroupVat + ((_termsResults.SiteGroupVatShouldGet - _termsResults.SiteGroupVat) * (_termsInfo.SupplierGuarantorPercentage / _temp));
                            _termsResults.SupplierRent = _termsResults.SupplierRent - ((_termsResults.SiteGroupVatShouldGet - _termsResults.SiteGroupVat) * (_termsInfo.SupplierGuarantorPercentage / _temp));
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void UseSecSiteGroupForSiteGroupVATShortfall()
        {
            float? _temp = 0;
            bool? _isLastPercentage = false;

            try
            {
                LogManager.WriteLog("Inside UseSecSiteGroupForSiteGroupVATShortfall...", LogManager.enumLogLevel.Info);

                switch (_termsInfo.SecGroupGuarantor)
                {
                    case TERMS_PRIMARY_TO_ZERO:
                    case TERMS_SECONDARY_TO_ZERO:
                        //try shares first
                        if ((_termsResults.SiteGroupVatShouldGet - _termsResults.SiteGroupVat) > _termsResults.SecSiteGroupShare)
                        {
                            _termsResults.SiteGroupVat = _termsResults.SiteGroupVat + _termsResults.SecSiteGroupShare;
                            _termsResults.SecSiteGroupShare = 0;
                        }
                        else
                        {
                            _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare - (_termsResults.SiteGroupVatShouldGet - _termsResults.SiteGroupVat);
                            _termsResults.SiteGroupVat = _termsResults.SiteGroupVatShouldGet;
                        }

                        //then rent
                        if ((_termsResults.SiteGroupVatShouldGet - _termsResults.SiteGroupVat) > _termsResults.SecSiteGroupRent)
                        {
                            _termsResults.SiteGroupVat = _termsResults.SiteGroupVat + _termsResults.SecSiteGroupRent;
                            _termsResults.SecSiteGroupShare = 0;
                        }
                        else
                        {
                            _termsResults.SecSiteGroupRent = _termsResults.SecSiteGroupRent - (_termsResults.SiteGroupVatShouldGet - _termsResults.SiteGroupVat);
                            _termsResults.SiteGroupVat = _termsResults.SiteGroupVatShouldGet;
                        }
                        break;
                    case TERMS_PRIMARY_FULL:
                    case TERMS_SECONDARY_REMAINDER:
                        _termsResults.SecSiteGroupShare = _termsResults.SecSiteGroupShare - (_termsResults.SiteGroupVatShouldGet - _termsResults.SiteGroupVat);
                        _termsResults.SiteGroupVat = _termsResults.SiteGroupVatShouldGet;
                        break;
                    case TERMS_PERCENTAGE:
                        _temp = 0;
                        _isLastPercentage = true;

                        if (_termsInfo.SiteUse > 0 && (!(_termsInfo.SiteShareGuaranteed.GetValueOrDefault() || _termsInfo.SiteValueGuaranteed.GetValueOrDefault())) && _termsInfo.SiteGuarantor == TERMS_PERCENTAGE)
                        {
                            _temp = _temp + _termsInfo.SiteGuarantorPercentage;
                            if (_termsInfo.SiteUse > _termsInfo.SecGroupUse)
                                _isLastPercentage = false;
                        }

                        if (_termsInfo.GroupUse > 0 && (!(_termsInfo.GroupShareGuaranteed.GetValueOrDefault() || _termsInfo.GroupValueGuaranteed.GetValueOrDefault())) && _termsInfo.GroupGuarantor == TERMS_PERCENTAGE)
                        {
                            _temp = _temp + _termsInfo.GroupGuarantor;
                            if (_termsInfo.GroupUse > _termsInfo.SecGroupUse)
                                _isLastPercentage = false;
                        }

                        if (_termsInfo.SupplierUse > 0 && (!(_termsInfo.SupplierShareGuaranteed.GetValueOrDefault() || _termsInfo.SupplierValueGuaranteed.GetValueOrDefault())) && _termsInfo.SupplierGuarantor == TERMS_PERCENTAGE)
                        {
                            _temp = _temp + _termsInfo.SupplierGuarantorPercentage;
                            if (_termsInfo.SupplierUse > _termsInfo.SecGroupUse)
                                _isLastPercentage = false;
                        }

                        if (_termsInfo.SecGroupUse > 0 && (!(_termsInfo.SecGroupShareGuaranteed.GetValueOrDefault() || _termsInfo.SecGroupValueGuaranteed.GetValueOrDefault())) && _termsInfo.SecGroupGuarantor == TERMS_PERCENTAGE)
                            _temp = _temp + _termsInfo.SecGroupGuarantorPercentage;

                        if (_isLastPercentage.GetValueOrDefault())
                        {
                            _termsResults.SecSiteGroupRent = _termsResults.SecSiteGroupRent - (_termsResults.SiteGroupVatShouldGet - _termsResults.SiteGroupVat);
                            _termsResults.SiteGroupVat = _termsResults.SiteGroupVatShouldGet;
                        }
                        else
                        {
                            _termsResults.SiteGroupVat = _termsResults.SiteGroupVat + ((_termsResults.SiteGroupVatShouldGet - _termsResults.SiteGroupVat) * (_termsInfo.SupplierGuarantorPercentage / _temp));
                            _termsResults.SecSiteGroupRent = _termsResults.SecSiteGroupRent - ((_termsResults.SiteGroupVatShouldGet - _termsResults.SiteGroupVat) * (_termsInfo.SupplierGuarantorPercentage / _temp));
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void UseSiteForSecSiteGroupVATShortfall()
        {
            float? _temp = 0;
            bool? _isLastPercentage = false;

            try
            {
                LogManager.WriteLog("Inside UseSiteForSecSiteGroupVATShortfall...", LogManager.enumLogLevel.Info);

                switch (_termsInfo.SiteGuarantor)
                {
                    case TERMS_PRIMARY_TO_ZERO:
                    case TERMS_SECONDARY_TO_ZERO:
                        //try shares first
                        if ((_termsResults.SecSiteGroupVatShouldGet - _termsResults.SecSiteGroupVat) > _termsResults.SiteShare)
                        {
                            _termsResults.SecSiteGroupVat = _termsResults.SecSiteGroupVat + _termsResults.SiteShare;
                            _termsResults.SiteShare = 0;
                        }
                        else
                        {
                            _termsResults.SiteShare = _termsResults.SiteShare - (_termsResults.SecSiteGroupVatShouldGet - _termsResults.SecSiteGroupVat);
                            _termsResults.SecSiteGroupVat = _termsResults.SecSiteGroupVatShouldGet;
                        }

                        //then rent
                        if ((_termsResults.SecSiteGroupVatShouldGet - _termsResults.SecSiteGroupVat) > _termsResults.SiteRent)
                        {
                            _termsResults.SecSiteGroupVat = _termsResults.SecSiteGroupVat + _termsResults.SiteRent;
                            _termsResults.SiteShare = 0;
                        }
                        else
                        {
                            _termsResults.SiteRent = _termsResults.SiteRent - (_termsResults.SecSiteGroupVatShouldGet - _termsResults.SecSiteGroupVat);
                            _termsResults.SecSiteGroupVat = _termsResults.SecSiteGroupVatShouldGet;
                        }
                        break;
                    case TERMS_PRIMARY_FULL:
                    case TERMS_SECONDARY_REMAINDER:
                        _termsResults.SiteShare = _termsResults.SiteShare - (_termsResults.SecSiteGroupVatShouldGet - _termsResults.SecSiteGroupVat);
                        _termsResults.SecSiteGroupVat = _termsResults.SiteGroupVatShouldGet;
                        break;
                    case TERMS_PERCENTAGE:
                        _temp = 0;
                        _isLastPercentage = true;

                        if (_termsInfo.SiteUse > 0 && (!(_termsInfo.SiteShareGuaranteed.GetValueOrDefault() || _termsInfo.SiteValueGuaranteed.GetValueOrDefault())) && _termsInfo.SiteGuarantor == TERMS_PERCENTAGE)
                            _temp = _temp + _termsInfo.SiteGuarantorPercentage;

                        if (_termsInfo.SecGroupUse > 0 && (!(_termsInfo.SecGroupShareGuaranteed.GetValueOrDefault() || _termsInfo.SecGroupValueGuaranteed.GetValueOrDefault())) && _termsInfo.SecGroupGuarantor == TERMS_PERCENTAGE)
                        {
                            _temp = _temp + _termsInfo.SecGroupGuarantorPercentage;
                            if (_termsInfo.SecGroupUse > _termsInfo.SiteUse)
                                _isLastPercentage = false;
                        }

                        if (_termsInfo.GroupUse > 0 && (!(_termsInfo.GroupShareGuaranteed.GetValueOrDefault() || _termsInfo.GroupValueGuaranteed.GetValueOrDefault())) && _termsInfo.GroupGuarantor == TERMS_PERCENTAGE)
                        {
                            _temp = _temp + _termsInfo.GroupGuarantorPercentage;
                            if (_termsInfo.GroupUse > _termsInfo.SiteUse)
                                _isLastPercentage = false;
                        }

                        if (_termsInfo.SupplierUse > 0 && (!(_termsInfo.SupplierShareGuaranteed.GetValueOrDefault() || _termsInfo.SupplierValueGuaranteed.GetValueOrDefault())) && _termsInfo.SupplierGuarantor == TERMS_PERCENTAGE)
                        {
                            _temp = _temp + _termsInfo.SupplierGuarantorPercentage;
                            if (_termsInfo.SupplierUse > _termsInfo.SiteUse)
                                _isLastPercentage = false;
                        }

                        if (_isLastPercentage.GetValueOrDefault())
                        {
                            _termsResults.SiteRent = _termsResults.SiteRent - (_termsResults.SecSiteGroupVatShouldGet - _termsResults.SecSiteGroupVat);
                            _termsResults.SecSiteGroupVat = _termsResults.SecSiteGroupVatShouldGet;
                        }
                        else
                        {
                            _termsResults.SecSiteGroupVat = _termsResults.SecSiteGroupVat + ((_termsResults.SecSiteGroupVatShouldGet - _termsResults.SecSiteGroupVat) * (_termsInfo.SiteGuarantorPercentage / _temp));
                            _termsResults.SiteRent = _termsResults.SiteRent - ((_termsResults.SecSiteGroupVatShouldGet - _termsResults.SecSiteGroupVat) * (_termsInfo.SiteGuarantorPercentage / _temp));
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void UseSiteGroupForSecSiteGroupVATShortfall()
        {
            float? _temp = 0;
            bool? _isLastPercentage = false;

            try
            {
                LogManager.WriteLog("Inside UseSiteGroupForSecSiteGroupVATShortfall...", LogManager.enumLogLevel.Info);

                switch (_termsInfo.GroupGuarantor)
                {
                    case TERMS_PRIMARY_TO_ZERO:
                    case TERMS_SECONDARY_TO_ZERO:
                        //try shares first
                        if ((_termsResults.SecSiteGroupVatShouldGet - _termsResults.SecSiteGroupVat) > _termsResults.SiteGroupShare)
                        {
                            _termsResults.SecSiteGroupVat = _termsResults.SecSiteGroupVat + _termsResults.SiteGroupShare;
                            _termsResults.SiteGroupShare = 0;
                        }
                        else
                        {
                            _termsResults.SiteGroupShare = _termsResults.SiteGroupShare - (_termsResults.SecSiteGroupVatShouldGet - _termsResults.SecSiteGroupVat);
                            _termsResults.SecSiteGroupVat = _termsResults.SecSiteGroupVatShouldGet;
                        }

                        //then rent
                        if ((_termsResults.SecSiteGroupVatShouldGet - _termsResults.SecSiteGroupVat) > _termsResults.SiteGroupRent)
                        {
                            _termsResults.SecSiteGroupVat = _termsResults.SecSiteGroupVat + _termsResults.SiteGroupRent;
                            _termsResults.SiteGroupShare = 0;
                        }
                        else
                        {
                            _termsResults.SiteGroupRent = _termsResults.SiteGroupRent - (_termsResults.SecSiteGroupVatShouldGet - _termsResults.SecSiteGroupVat);
                            _termsResults.SecSiteGroupVat = _termsResults.SecSiteGroupVatShouldGet;
                        }
                        break;
                    case TERMS_PRIMARY_FULL:
                    case TERMS_SECONDARY_REMAINDER:
                        _termsResults.SiteGroupShare = _termsResults.SiteGroupShare - (_termsResults.SecSiteGroupVatShouldGet - _termsResults.SecSiteGroupVat);
                        _termsResults.SecSiteGroupVat = _termsResults.SiteGroupVatShouldGet;
                        break;
                    case TERMS_PERCENTAGE:
                        _temp = 0;
                        _isLastPercentage = true;

                        if (_termsInfo.SiteUse > 0 && (!(_termsInfo.SiteShareGuaranteed.GetValueOrDefault() || _termsInfo.SiteValueGuaranteed.GetValueOrDefault())) && _termsInfo.SiteGuarantor == TERMS_PERCENTAGE)
                        {
                            _temp = _temp + _termsInfo.SiteGuarantorPercentage;
                            if (_termsInfo.SiteUse > _termsInfo.GroupUse)
                                _isLastPercentage = false;
                        }

                        if (_termsInfo.SecGroupUse > 0 && (!(_termsInfo.SecGroupShareGuaranteed.GetValueOrDefault() || _termsInfo.SecGroupValueGuaranteed.GetValueOrDefault())) && _termsInfo.SecGroupGuarantor == TERMS_PERCENTAGE)
                        {
                            _temp = _temp + _termsInfo.SecGroupGuarantorPercentage;
                            if (_termsInfo.SecGroupUse > _termsInfo.GroupUse)
                                _isLastPercentage = false;
                        }

                        if (_termsInfo.GroupUse > 0 && (!(_termsInfo.GroupShareGuaranteed.GetValueOrDefault() || _termsInfo.GroupValueGuaranteed.GetValueOrDefault())) && _termsInfo.GroupGuarantor == TERMS_PERCENTAGE)
                            _temp = _temp + _termsInfo.GroupGuarantorPercentage;

                        if (_termsInfo.SupplierUse > 0 && (!(_termsInfo.SupplierShareGuaranteed.GetValueOrDefault() || _termsInfo.SupplierValueGuaranteed.GetValueOrDefault())) && _termsInfo.SupplierGuarantor == TERMS_PERCENTAGE)
                        {
                            _temp = _temp + _termsInfo.SupplierGuarantorPercentage;
                            if (_termsInfo.SupplierUse > _termsInfo.GroupUse)
                                _isLastPercentage = false;
                        }

                        if (_isLastPercentage.GetValueOrDefault())
                        {
                            _termsResults.SiteGroupRent = _termsResults.SiteGroupRent - (_termsResults.SecSiteGroupVatShouldGet - _termsResults.SecSiteGroupVat);
                            _termsResults.SecSiteGroupVat = _termsResults.SecSiteGroupVatShouldGet;
                        }
                        else
                        {
                            _termsResults.SecSiteGroupVat = _termsResults.SecSiteGroupVat + ((_termsResults.SecSiteGroupVatShouldGet - _termsResults.SecSiteGroupVat) * (_termsInfo.GroupGuarantorPercentage / _temp));
                            _termsResults.SiteGroupRent = _termsResults.SiteGroupRent - ((_termsResults.SecSiteGroupVatShouldGet - _termsResults.SecSiteGroupVat) * (_termsInfo.GroupGuarantorPercentage / _temp));
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void UseSupplierForSecSiteGroupVATShortfall()
        {
            float? _temp = 0;
            bool? _isLastPercentage = false;

            try
            {
                LogManager.WriteLog("Inside UseSupplierForSecSiteGroupVATShortfall...", LogManager.enumLogLevel.Info);

                switch (_termsInfo.SupplierGuarantor)
                {
                    case TERMS_PRIMARY_TO_ZERO:
                    case TERMS_SECONDARY_TO_ZERO:
                        //try shares first
                        if ((_termsResults.SecSiteGroupVatShouldGet - _termsResults.SecSiteGroupVat) > _termsResults.SupplierShare)
                        {
                            _termsResults.SecSiteGroupVat = _termsResults.SecSiteGroupVat + _termsResults.SupplierShare;
                            _termsResults.SupplierShare = 0;
                        }
                        else
                        {
                            _termsResults.SupplierShare = _termsResults.SupplierShare - (_termsResults.SecSiteGroupVatShouldGet - _termsResults.SecSiteGroupVat);
                            _termsResults.SecSiteGroupVat = _termsResults.SecSiteGroupVatShouldGet;
                        }

                        //then rent
                        if ((_termsResults.SecSiteGroupVatShouldGet - _termsResults.SecSiteGroupVat) > _termsResults.SupplierRent)
                        {
                            _termsResults.SecSiteGroupVat = _termsResults.SecSiteGroupVat + _termsResults.SupplierRent;
                            _termsResults.SupplierShare = 0;
                        }
                        else
                        {
                            _termsResults.SupplierRent = _termsResults.SupplierRent - (_termsResults.SecSiteGroupVatShouldGet - _termsResults.SecSiteGroupVat);
                            _termsResults.SecSiteGroupVat = _termsResults.SecSiteGroupVatShouldGet;
                        }
                        break;
                    case TERMS_PRIMARY_FULL:
                    case TERMS_SECONDARY_REMAINDER:
                        _termsResults.SupplierShare = _termsResults.SupplierShare - (_termsResults.SecSiteGroupVatShouldGet - _termsResults.SecSiteGroupVat);
                        _termsResults.SecSiteGroupVat = _termsResults.SecSiteGroupVatShouldGet;
                        break;
                    case TERMS_PERCENTAGE:
                        _temp = 0;
                        _isLastPercentage = true;

                        if (_termsInfo.SiteUse > 0 && (!(_termsInfo.SiteShareGuaranteed.GetValueOrDefault() || _termsInfo.SiteValueGuaranteed.GetValueOrDefault())) && _termsInfo.SiteGuarantor == TERMS_PERCENTAGE)
                        {
                            _temp = _temp + _termsInfo.SiteGuarantorPercentage;
                            if (_termsInfo.SiteUse > _termsInfo.SupplierUse)
                                _isLastPercentage = false;
                        }

                        if (_termsInfo.SecGroupUse > 0 && (!(_termsInfo.SecGroupShareGuaranteed.GetValueOrDefault() || _termsInfo.SecGroupValueGuaranteed.GetValueOrDefault())) && _termsInfo.SecGroupGuarantor == TERMS_PERCENTAGE)
                        {
                            _temp = _temp + _termsInfo.SecGroupGuarantorPercentage;
                            if (_termsInfo.SecGroupUse > _termsInfo.SupplierUse)
                                _isLastPercentage = false;
                        }

                        if (_termsInfo.GroupUse > 0 && (!(_termsInfo.GroupShareGuaranteed.GetValueOrDefault() || _termsInfo.GroupValueGuaranteed.GetValueOrDefault())) && _termsInfo.GroupGuarantor == TERMS_PERCENTAGE)
                        {
                            _temp = _temp + _termsInfo.GroupGuarantorPercentage;
                            if (_termsInfo.GroupUse > _termsInfo.SupplierUse)
                                _isLastPercentage = false;
                        }

                        if (_termsInfo.SupplierUse > 0 && (!(_termsInfo.SupplierShareGuaranteed.GetValueOrDefault() || _termsInfo.SupplierValueGuaranteed.GetValueOrDefault())) && _termsInfo.SupplierGuarantor == TERMS_PERCENTAGE)
                            _temp = _temp + _termsInfo.SupplierGuarantorPercentage;

                        if (_isLastPercentage.GetValueOrDefault())
                        {
                            _termsResults.SupplierRent = _termsResults.SupplierRent - (_termsResults.SecSiteGroupVatShouldGet - _termsResults.SecSiteGroupVat);
                            _termsResults.SecSiteGroupVat = _termsResults.SecSiteGroupVatShouldGet;
                        }
                        else
                        {
                            _termsResults.SecSiteGroupVat = _termsResults.SecSiteGroupVat + ((_termsResults.SecSiteGroupVatShouldGet - _termsResults.SecSiteGroupVat) * (_termsInfo.SupplierGuarantorPercentage / _temp));
                            _termsResults.SupplierRent = _termsResults.SupplierRent - ((_termsResults.SecSiteGroupVatShouldGet - _termsResults.SecSiteGroupVat) * (_termsInfo.SupplierGuarantorPercentage / _temp));
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void AllocateSupplierMoneyToDestination()
        {
            try
            {
                LogManager.WriteLog("Inside AllocateSupplierMoneyToDestination...", LogManager.enumLogLevel.Info);

                if (_termsInfo.SupplierUse > 0)
                {
                    switch (_termsInfo.SupplierCashDestination)
                    {
                        case TERMS_SUPPLIER:
                            _termsResults.SupplierShareBankedToSupplier = _termsResults.SupplierShareBankedToSupplier.GetValueOrDefault() + _termsResults.SupplierRent.GetValueOrDefault() + _termsResults.SupplierShare.GetValueOrDefault();
                            _termsResults.BankedToSupplier = _termsResults.BankedToSupplier.GetValueOrDefault() + _termsResults.SupplierRent.GetValueOrDefault() + _termsResults.SupplierShare.GetValueOrDefault();
                            //No deferred cash
                            break;
                        case TERMS_SITE:
                        case TERMS_LEFT_ON_SITE:
                            //deferred remittance has to be supplier
                            _termsResults.SupplierShareLOS = _termsResults.SupplierShareLOS.GetValueOrDefault() + _termsResults.SupplierRent.GetValueOrDefault() + _termsResults.SupplierShare.GetValueOrDefault();
                            _termsResults.LOS = _termsResults.LOS + _termsResults.SupplierRent.GetValueOrDefault() + _termsResults.SupplierShare.GetValueOrDefault();

                            switch (_termsInfo.SupplierDeferredDest)
                            {
                                case TERMS_SUPPLIER:
                                    _termsResults.SupplierShareLOS_toSupplier = _termsResults.SupplierShareLOS_toSupplier.GetValueOrDefault() + _termsResults.SupplierRent.GetValueOrDefault() + _termsResults.SupplierShare.GetValueOrDefault();
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case TERMS_SITE_GROUP:
                            //deferred remittance has to be supplier
                            _termsResults.SupplierShareBankedToCompany = _termsResults.SupplierShareBankedToCompany.GetValueOrDefault() + _termsResults.SupplierRent.GetValueOrDefault() + _termsResults.SupplierShare.GetValueOrDefault();
                            _termsResults.BankedToCompany = _termsResults.BankedToCompany.GetValueOrDefault() + _termsResults.SupplierRent.GetValueOrDefault() + _termsResults.SupplierShare.GetValueOrDefault();

                            switch (_termsInfo.SupplierDeferredDest)
                            {
                                case TERMS_SUPPLIER:
                                    _termsResults.SupplierShareBankedToGroup_toSupplier = _termsResults.SupplierShareBankedToGroup_toSupplier.GetValueOrDefault() + _termsResults.SupplierRent.GetValueOrDefault() + _termsResults.SupplierShare.GetValueOrDefault();
                                    break;
                                default:
                                    break;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void AllocateSiteMoneyToDestination()
        {
            try
            {
                LogManager.WriteLog("Inside AllocateSiteMoneyToDestination...", LogManager.enumLogLevel.Info);

                if (_termsInfo.SiteUse > 0)
                {
                    switch (_termsInfo.SiteCashDestination)
                    {
                        case TERMS_SUPPLIER:
                            //deferred remittance has to be site
                            _termsResults.SiteShareBankedToSupplier = _termsResults.SiteShareBankedToSupplier.GetValueOrDefault() + _termsResults.SiteRent.GetValueOrDefault() + _termsResults.SiteShare.GetValueOrDefault();
                            _termsResults.BankedToSupplier = _termsResults.BankedToSupplier.GetValueOrDefault() + _termsResults.SiteRent.GetValueOrDefault() + _termsResults.SiteShare.GetValueOrDefault();

                            switch (_termsInfo.SiteDeferredDest)
                            {
                                case TERMS_SITE_GROUP:
                                    _termsResults.SiteShareBankedToSupplier_toGroup = _termsResults.SiteShareBankedToSupplier_toGroup.GetValueOrDefault() + _termsResults.SiteRent.GetValueOrDefault() + _termsResults.SiteShare.GetValueOrDefault();
                                    break;
                                case TERMS_SITE:
                                    _termsResults.SiteShareBankedToSupplier_toSite = _termsResults.SiteShareBankedToSupplier_toSite.GetValueOrDefault() + _termsResults.SiteRent.GetValueOrDefault() + _termsResults.SiteShare.GetValueOrDefault();
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case TERMS_SITE:
                        case TERMS_LEFT_ON_SITE:
                            _termsResults.SiteShareLOS = _termsResults.SiteShareLOS.GetValueOrDefault() + _termsResults.SiteRent.GetValueOrDefault() + _termsResults.SiteShare.GetValueOrDefault();
                            _termsResults.LOS = _termsResults.LOS.GetValueOrDefault() + _termsResults.SiteRent.GetValueOrDefault() + _termsResults.SiteShare.GetValueOrDefault();
                            break;
                        case TERMS_SITE_GROUP:
                            //deferred remittance has to be supplier
                            _termsResults.SiteShareBankedToCompany = _termsResults.SiteShareBankedToCompany.GetValueOrDefault() + _termsResults.SiteRent.GetValueOrDefault() + _termsResults.SiteShare.GetValueOrDefault();
                            _termsResults.BankedToCompany = _termsResults.BankedToCompany.GetValueOrDefault() + _termsResults.SiteRent.GetValueOrDefault() + _termsResults.SiteShare.GetValueOrDefault();

                            switch (_termsInfo.SiteDeferredDest)
                            {
                                case TERMS_SITE:
                                    _termsResults.SiteShareBankedToGroup_toSite = _termsResults.SiteShareBankedToGroup_toSite.GetValueOrDefault() + _termsResults.SiteRent + _termsResults.SiteShare.GetValueOrDefault();
                                    break;
                                default:
                                    break;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void AllocateSiteGroupMoneyToDestination()
        {
            try
            {
                LogManager.WriteLog("Inside AllocateSiteGroupMoneyToDestination...", LogManager.enumLogLevel.Info);

                if (_termsInfo.GroupUse > 0)
                {
                    switch (_termsInfo.GroupCashDestination)
                    {
                        case TERMS_SUPPLIER:
                            //deferred remittance has to be site group
                            _termsResults.SiteGroupShareBankedToSupplier = _termsResults.SiteGroupShareBankedToSupplier.GetValueOrDefault() + _termsResults.SiteGroupRent.GetValueOrDefault() + _termsResults.SiteGroupShare.GetValueOrDefault();
                            _termsResults.BankedToSupplier = _termsResults.BankedToSupplier.GetValueOrDefault() + _termsResults.SiteGroupRent.GetValueOrDefault() + _termsResults.SiteGroupShare.GetValueOrDefault();

                            switch (_termsInfo.GroupDeferredDest)
                            {
                                case TERMS_SITE_GROUP:
                                    _termsResults.SiteGroupShareBankedToSupplier_toGroup = _termsResults.SiteGroupShareBankedToSupplier_toGroup.GetValueOrDefault() + _termsResults.SiteGroupRent.GetValueOrDefault() + _termsResults.SiteGroupShare.GetValueOrDefault();
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case TERMS_SITE:
                        case TERMS_LEFT_ON_SITE:
                            //deferred remittance has to be site group
                            _termsResults.SiteGroupShareLOS = _termsResults.SiteGroupShareLOS.GetValueOrDefault() + _termsResults.SiteGroupRent.GetValueOrDefault() + _termsResults.SiteGroupShare.GetValueOrDefault();
                            _termsResults.LOS = _termsResults.LOS.GetValueOrDefault() + _termsResults.SiteGroupRent.GetValueOrDefault() + _termsResults.SiteGroupShare.GetValueOrDefault();
                            break;
                        case TERMS_SITE_GROUP:
                            //deferred remittance has to be supplier
                            _termsResults.SiteGroupShareBankedToCompany = _termsResults.SiteGroupShareBankedToCompany.GetValueOrDefault() + _termsResults.SiteGroupRent.GetValueOrDefault() + _termsResults.SiteGroupShare.GetValueOrDefault();
                            _termsResults.BankedToCompany = _termsResults.BankedToCompany.GetValueOrDefault() + _termsResults.SiteGroupRent.GetValueOrDefault() + _termsResults.SiteGroupShare.GetValueOrDefault();
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void AllocateSecSiteGroupMoneyToDestination()
        {
            try
            {
                LogManager.WriteLog("Inside AllocateSecSiteGroupMoneyToDestination...", LogManager.enumLogLevel.Info);

                if (_termsInfo.SecGroupUse > 0)
                {
                    switch (_termsInfo.SecGroupCashDestination)
                    {
                        case TERMS_SUPPLIER:
                            _termsResults.SecSiteGroupShareBankedToSupplier = _termsResults.SecSiteGroupShareBankedToSupplier.GetValueOrDefault() + _termsResults.SecSiteGroupRent.GetValueOrDefault() + _termsResults.SecSiteGroupShare.GetValueOrDefault();
                            _termsResults.BankedToSupplier = _termsResults.BankedToSupplier.GetValueOrDefault() + _termsResults.SecSiteGroupRent.GetValueOrDefault() + _termsResults.SecSiteGroupShare.GetValueOrDefault();

                            switch (_termsInfo.SecGroupDeferredDest)
                            {
                                case TERMS_SECONDARY_GROUP:
                                    _termsResults.SecSiteGroupShareBankedToSupplier_toSecGroup = _termsResults.SecSiteGroupShareBankedToSupplier_toSecGroup.GetValueOrDefault() + _termsResults.SecSiteGroupRent.GetValueOrDefault() + _termsResults.SecSiteGroupShare.GetValueOrDefault();
                                    break;
                                case TERMS_SITE_GROUP:
                                    _termsResults.SecSiteGroupShareBankedToSupplier_toGroup = _termsResults.SecSiteGroupShareBankedToSupplier_toGroup.GetValueOrDefault() + _termsResults.SecSiteGroupRent.GetValueOrDefault() + _termsResults.SecSiteGroupShare.GetValueOrDefault();
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case TERMS_SITE:
                        case TERMS_LEFT_ON_SITE:
                            //deferred remittance has to be site group
                            _termsResults.SecSiteGroupShareLOS = _termsResults.SecSiteGroupShareLOS.GetValueOrDefault() + _termsResults.SecSiteGroupRent.GetValueOrDefault() + _termsResults.SecSiteGroupShare.GetValueOrDefault();
                            _termsResults.LOS = _termsResults.LOS + _termsResults.SecSiteGroupRent.GetValueOrDefault() + _termsResults.SecSiteGroupShare.GetValueOrDefault();
                            switch (_termsInfo.SecGroupDeferredDest)
                            {
                                case TERMS_SECONDARY_GROUP:
                                    _termsResults.SecSiteGroupShareLOS_toSecGroup = _termsResults.SecSiteGroupShareLOS_toSecGroup.GetValueOrDefault() + _termsResults.SecSiteGroupRent.GetValueOrDefault() + _termsResults.SecSiteGroupShare.GetValueOrDefault();
                                    break;
                                case TERMS_SITE_GROUP:
                                    _termsResults.SecSiteGroupShareLOS_toGroup = _termsResults.SecSiteGroupShareLOS_toGroup.GetValueOrDefault() + _termsResults.SecSiteGroupRent.GetValueOrDefault() + _termsResults.SecSiteGroupShare.GetValueOrDefault();
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case TERMS_SITE_GROUP:
                            _termsResults.SecSiteGroupShareBankedToCompany = _termsResults.SecSiteGroupShareBankedToCompany.GetValueOrDefault() + _termsResults.SecSiteGroupRent.GetValueOrDefault() + _termsResults.SecSiteGroupShare.GetValueOrDefault();
                            _termsResults.BankedToCompany = _termsResults.BankedToCompany.GetValueOrDefault() + _termsResults.SecSiteGroupRent.GetValueOrDefault() + _termsResults.SecSiteGroupShare.GetValueOrDefault();
                            switch (_termsInfo.SecGroupDeferredDest)
                            {
                                case TERMS_SECONDARY_GROUP:
                                    _termsResults.SecSiteGroupShareBankedToGroup_toSecGroup = _termsResults.SecSiteGroupShareBankedToGroup_toSecGroup.GetValueOrDefault() + _termsResults.SecSiteGroupRent.GetValueOrDefault() + _termsResults.SecSiteGroupShare.GetValueOrDefault();
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case TERMS_SECONDARY_GROUP:
                            //This can't really happen!
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void AllocateOutputVatMoneyToDestination()
        {
            try
            {
                LogManager.WriteLog("Inside AllocateOutputVatMoneyToDestination...", LogManager.enumLogLevel.Info);

                //needs some looking at!
                if (_termsInfo.VATOutputUse > 0)
                {
                    switch (-1000)      //doesn't exist! TermsInfo.vatOutputCashDestination
                    {
                        case TERMS_SUPPLIER:
                            _termsResults.VATBankedToSupplier = _termsResults.VATBankedToSupplier.GetValueOrDefault() + _termsResults.OutputVAT.GetValueOrDefault();
                            break;
                        case TERMS_SITE:
                        case TERMS_LEFT_ON_SITE:
                            _termsResults.VATLOS = _termsResults.VATLOS.GetValueOrDefault() + _termsResults.OutputVAT.GetValueOrDefault();
                            break;
                        case TERMS_SITE_GROUP:
                            _termsResults.VATBankedToCompany = _termsResults.VATBankedToCompany.GetValueOrDefault() + _termsResults.OutputVAT.GetValueOrDefault();
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void AllocateSupplierVatMoneyToDestination()
        {
            try
            {
                LogManager.WriteLog("Inside AllocateSupplierVatMoneyToDestination...", LogManager.enumLogLevel.Info);

                if (_termsInfo.VATSupplierUse > 0)
                {
                    _termsResults.SupplierVat = _termsResults.SupplierVat.GetValueOrDefault() + _termsResults.SupplierGuarantorForVATShortfall.GetValueOrDefault();
                    _termsResults.SupplierGuarantorForVATShortfall = 0;

                    switch (_termsInfo.VATSupplierDest)
                    {
                        case TERMS_SUPPLIER:
                            _termsResults.SupplierVATBankedToSupplier = _termsResults.SupplierVat.GetValueOrDefault() + _termsResults.SupplierGuarantorForVATShortfall.GetValueOrDefault();
                            _termsResults.VATBankedToSupplier = _termsResults.VATBankedToSupplier.GetValueOrDefault() + _termsResults.SupplierVat.GetValueOrDefault() + _termsResults.SupplierGuarantorForVATShortfall.GetValueOrDefault();
                            _termsResults.BankedToSupplier = _termsResults.BankedToSupplier.GetValueOrDefault() + _termsResults.SupplierVat.GetValueOrDefault() + _termsResults.SupplierGuarantorForVATShortfall.GetValueOrDefault();

                            switch (_termsInfo.VATSupplierDeferredDest)
                            {
                                case TERMS_SITE_GROUP:
                                    _termsResults.VATSupplierBankedToSupplier_toGroup = _termsResults.VATSupplierBankedToSupplier_toGroup.GetValueOrDefault() + _termsResults.SupplierVat.GetValueOrDefault() + _termsResults.SupplierGuarantorForVATShortfall.GetValueOrDefault();
                                    break;
                                case TERMS_SECONDARY_GROUP:
                                    _termsResults.VATSupplierBankedToSupplier_toSecGroup = _termsResults.VATSupplierBankedToSupplier_toSecGroup.GetValueOrDefault() + _termsResults.SupplierVat.GetValueOrDefault() + _termsResults.SupplierGuarantorForVATShortfall.GetValueOrDefault();
                                    break;
                                case TERMS_CUSTOMS_AND_EXCISE:
                                    _termsResults.VATSupplierBankedToSupplier_toCustoms = _termsResults.VATSupplierBankedToSupplier_toCustoms.GetValueOrDefault() + _termsResults.SupplierVat.GetValueOrDefault() + _termsResults.SupplierGuarantorForVATShortfall.GetValueOrDefault();
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case TERMS_SITE:
                        case TERMS_LEFT_ON_SITE:
                            //deferred remittance has to be site group
                            _termsResults.SupplierVATLOS = _termsResults.SupplierVat.GetValueOrDefault() + _termsResults.SupplierGuarantorForVATShortfall.GetValueOrDefault();
                            _termsResults.VATLOS = _termsResults.VATLOS + _termsResults.SupplierVat.GetValueOrDefault() + _termsResults.SupplierGuarantorForVATShortfall.GetValueOrDefault();
                            _termsResults.LOS = _termsResults.LOS + _termsResults.SupplierVat.GetValueOrDefault() + _termsResults.SupplierGuarantorForVATShortfall.GetValueOrDefault();

                            switch (_termsInfo.VATSupplierDeferredDest)
                            {
                                case TERMS_SITE_GROUP:
                                    _termsResults.VATSupplierLos_toGroup = _termsResults.VATSupplierLos_toGroup.GetValueOrDefault() + _termsResults.SupplierVat.GetValueOrDefault() + _termsResults.SupplierGuarantorForVATShortfall.GetValueOrDefault();
                                    break;
                                case TERMS_SUPPLIER:
                                    _termsResults.VATSupplierLos_toSupplier = _termsResults.VATSupplierLos_toSupplier.GetValueOrDefault() + _termsResults.SupplierVat.GetValueOrDefault() + _termsResults.SupplierGuarantorForVATShortfall.GetValueOrDefault();
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case TERMS_SITE_GROUP:
                            _termsResults.SupplierVATBankedToCompany = _termsResults.SupplierVat.GetValueOrDefault() + _termsResults.SupplierGuarantorForVATShortfall.GetValueOrDefault();
                            _termsResults.VATBankedToCompany = _termsResults.VATBankedToCompany.GetValueOrDefault() + _termsResults.SupplierVat.GetValueOrDefault() + _termsResults.SupplierGuarantorForVATShortfall.GetValueOrDefault();
                            _termsResults.BankedToCompany = _termsResults.BankedToCompany.GetValueOrDefault() + _termsResults.SupplierVat.GetValueOrDefault() + _termsResults.SupplierGuarantorForVATShortfall.GetValueOrDefault();

                            switch (_termsInfo.VATSupplierDeferredDest)
                            {
                                case TERMS_SUPPLIER:
                                    _termsResults.VATSupplierBankedToGroup_toSupplier = _termsResults.VATSupplierBankedToGroup_toSupplier.GetValueOrDefault() + _termsResults.SupplierVat.GetValueOrDefault() + _termsResults.SupplierGuarantorForVATShortfall.GetValueOrDefault();
                                    break;
                                case TERMS_CUSTOMS_AND_EXCISE:
                                    _termsResults.VATSupplierBankedToGroup_toCustoms = _termsResults.VATSupplierBankedToGroup_toCustoms.GetValueOrDefault() + _termsResults.SupplierVat.GetValueOrDefault() + _termsResults.SupplierGuarantorForVATShortfall.GetValueOrDefault();
                                    break;
                                default:
                                    break;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void AllocateSiteVatMoneyToDestination()
        {
            try
            {
                LogManager.WriteLog("Inside AllocateSiteVatMoneyToDestination...", LogManager.enumLogLevel.Info);

                if (_termsInfo.VATSiteUse > 0)
                {
                    _termsResults.SiteVat = _termsResults.SiteVat.GetValueOrDefault() + _termsResults.SiteGuarantorForVATShortfall.GetValueOrDefault();
                    _termsResults.SiteGuarantorForVATShortfall = 0;

                    switch (_termsInfo.VATSiteDest)
                    {
                        case TERMS_SUPPLIER:
                            _termsResults.SiteVATBankedToSupplier = _termsResults.SiteVat.GetValueOrDefault() + _termsResults.SiteGuarantorForVATShortfall.GetValueOrDefault();
                            _termsResults.VATBankedToSupplier = _termsResults.VATBankedToSupplier.GetValueOrDefault() + _termsResults.SiteVat.GetValueOrDefault() + _termsResults.SiteGuarantorForVATShortfall.GetValueOrDefault();
                            _termsResults.BankedToSupplier = _termsResults.BankedToSupplier.GetValueOrDefault() + _termsResults.SiteVat.GetValueOrDefault() + _termsResults.SiteGuarantorForVATShortfall.GetValueOrDefault();

                            switch (_termsInfo.VatSiteDeferredDest)
                            {
                                case TERMS_SITE:
                                    _termsResults.VATSiteBankedToSupplier_toSite = _termsResults.VATSiteBankedToSupplier_toSite.GetValueOrDefault() + _termsResults.SiteVat.GetValueOrDefault() + _termsResults.SiteGuarantorForVATShortfall.GetValueOrDefault();
                                    break;
                                case TERMS_SITE_GROUP:
                                    _termsResults.VATSiteBankedToSupplier_toGroup = _termsResults.VATSiteBankedToSupplier_toGroup.GetValueOrDefault() + _termsResults.SiteVat.GetValueOrDefault() + _termsResults.SiteGuarantorForVATShortfall.GetValueOrDefault();
                                    break;
                                case TERMS_CUSTOMS_AND_EXCISE:
                                    _termsResults.VATSiteBankedToSupplier_toCustoms = _termsResults.VATSiteBankedToSupplier_toCustoms.GetValueOrDefault() + _termsResults.SiteVat.GetValueOrDefault() + _termsResults.SiteGuarantorForVATShortfall.GetValueOrDefault();
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case TERMS_SITE:
                        case TERMS_LEFT_ON_SITE:
                            _termsResults.SiteVATLos = _termsResults.SiteVat.GetValueOrDefault() + _termsResults.SiteGuarantorForVATShortfall.GetValueOrDefault();
                            _termsResults.VATLOS = _termsResults.VATLOS.GetValueOrDefault() + _termsResults.SiteVat.GetValueOrDefault() + _termsResults.SiteGuarantorForVATShortfall.GetValueOrDefault();
                            _termsResults.LOS = _termsResults.LOS.GetValueOrDefault() + _termsResults.SiteVat.GetValueOrDefault() + _termsResults.SiteGuarantorForVATShortfall.GetValueOrDefault();

                            switch (_termsInfo.VatSiteDeferredDest)
                            {
                                case TERMS_SUPPLIER:
                                    _termsResults.VATSiteLos_toSupplier = _termsResults.VATSiteLos_toSupplier.GetValueOrDefault() + _termsResults.SiteVat.GetValueOrDefault() + _termsResults.SiteGuarantorForVATShortfall.GetValueOrDefault();
                                    break;
                                case TERMS_SITE_GROUP:
                                    _termsResults.VATSiteLos_toGroup = _termsResults.VATSiteLos_toGroup.GetValueOrDefault() + _termsResults.SiteVat.GetValueOrDefault() + _termsResults.SiteGuarantorForVATShortfall.GetValueOrDefault();
                                    break;
                                case TERMS_CUSTOMS_AND_EXCISE:
                                    _termsResults.VATSiteLos_toCustoms = _termsResults.VATSiteLos_toCustoms.GetValueOrDefault() + _termsResults.SiteVat.GetValueOrDefault() + _termsResults.SiteGuarantorForVATShortfall.GetValueOrDefault();
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case TERMS_SITE_GROUP:
                            _termsResults.SiteVATBankedToCompany = _termsResults.SiteVat.GetValueOrDefault() + _termsResults.SiteGuarantorForVATShortfall.GetValueOrDefault();
                            _termsResults.VATBankedToCompany = _termsResults.VATBankedToCompany.GetValueOrDefault() + _termsResults.SiteVat.GetValueOrDefault() + _termsResults.SiteGuarantorForVATShortfall.GetValueOrDefault();
                            _termsResults.BankedToCompany = _termsResults.BankedToCompany.GetValueOrDefault() + _termsResults.SiteVat.GetValueOrDefault() + _termsResults.SiteGuarantorForVATShortfall.GetValueOrDefault();

                            switch (_termsInfo.VatSiteDeferredDest)
                            {
                                case TERMS_SUPPLIER:
                                    _termsResults.VATSiteBankedToGroup_toSupplier = _termsResults.VATSiteBankedToGroup_toSupplier.GetValueOrDefault() + _termsResults.SiteVat.GetValueOrDefault() + _termsResults.SiteGuarantorForVATShortfall.GetValueOrDefault();
                                    break;
                                case TERMS_CUSTOMS_AND_EXCISE:
                                    _termsResults.VATSiteBankedToGroup_toCustoms = _termsResults.VATSiteBankedToGroup_toCustoms.GetValueOrDefault() + _termsResults.SiteVat.GetValueOrDefault() + _termsResults.SiteGuarantorForVATShortfall.GetValueOrDefault();
                                    break;
                                default:
                                    break;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void AllocateSiteGroupVatMoneyToDestination()
        {
            try
            {
                LogManager.WriteLog("Inside AllocateSiteGroupVatMoneyToDestination...", LogManager.enumLogLevel.Info);

                if (_termsInfo.VATGroupUse > 0)
                {
                    _termsResults.SiteGroupVat = _termsResults.SiteGroupVat.GetValueOrDefault() + _termsResults.SiteGuarantorForVATShortfall.GetValueOrDefault();
                    _termsResults.SiteGroupGuarantorForVATShortfall = 0;

                    switch (_termsInfo.VATGroupDest)
                    {
                        case TERMS_SUPPLIER:
                            _termsResults.SiteGroupVATBankedToSupplier = _termsResults.SiteGroupVat.GetValueOrDefault() + _termsResults.SiteGroupGuarantorForVATShortfall.GetValueOrDefault();
                            _termsResults.VATBankedToSupplier = _termsResults.VATBankedToSupplier.GetValueOrDefault() + _termsResults.SiteGroupVat.GetValueOrDefault() + _termsResults.SiteGroupGuarantorForVATShortfall.GetValueOrDefault();
                            _termsResults.BankedToSupplier = _termsResults.BankedToSupplier.GetValueOrDefault() + _termsResults.SiteGroupVat.GetValueOrDefault() + _termsResults.SiteGroupGuarantorForVATShortfall.GetValueOrDefault();

                            switch (_termsInfo.VATGroupDeferredDest)
                            {
                                case TERMS_SITE_GROUP:
                                    _termsResults.VATGroupBankedToSupplier_toGroup = _termsResults.VATGroupBankedToSupplier_toGroup.GetValueOrDefault() + _termsResults.SiteGroupVat.GetValueOrDefault() + _termsResults.SiteGroupGuarantorForVATShortfall.GetValueOrDefault();
                                    break;
                                case TERMS_CUSTOMS_AND_EXCISE:
                                    _termsResults.VATGroupBankedToSupplier_toCustoms = _termsResults.VATGroupBankedToSupplier_toCustoms.GetValueOrDefault() + _termsResults.SiteGroupVat.GetValueOrDefault() + _termsResults.SiteGroupGuarantorForVATShortfall.GetValueOrDefault();
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case TERMS_SITE:
                        case TERMS_LEFT_ON_SITE:
                            _termsResults.SiteGroupVATLos = _termsResults.SiteGroupVat.GetValueOrDefault() + _termsResults.SiteGroupGuarantorForVATShortfall.GetValueOrDefault();
                            _termsResults.VATLOS = _termsResults.VATLOS.GetValueOrDefault() + _termsResults.SiteGroupVat.GetValueOrDefault() + _termsResults.SiteGroupGuarantorForVATShortfall.GetValueOrDefault();
                            _termsResults.LOS = _termsResults.LOS.GetValueOrDefault() + _termsResults.SiteGroupVat.GetValueOrDefault() + _termsResults.SiteGroupGuarantorForVATShortfall.GetValueOrDefault();

                            switch (_termsInfo.VATGroupDeferredDest)
                            {
                                case TERMS_SITE_GROUP:
                                    _termsResults.VATGroupLos_toGroup = _termsResults.VATGroupLos_toGroup.GetValueOrDefault() + _termsResults.SiteGroupVat.GetValueOrDefault() + _termsResults.SiteGroupGuarantorForVATShortfall.GetValueOrDefault();
                                    break;
                                case TERMS_CUSTOMS_AND_EXCISE:
                                    _termsResults.VATGroupLos_toSupplier = _termsResults.VATGroupLos_toSupplier.GetValueOrDefault() + _termsResults.SiteGroupVat.GetValueOrDefault() + _termsResults.SiteGroupGuarantorForVATShortfall.GetValueOrDefault();
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case TERMS_SITE_GROUP:
                            _termsResults.SiteGroupVATBankedToCompany = _termsResults.SiteGroupVat.GetValueOrDefault() + _termsResults.SiteGroupGuarantorForVATShortfall.GetValueOrDefault();
                            _termsResults.VATBankedToCompany = _termsResults.VATBankedToCompany.GetValueOrDefault() + _termsResults.SiteGroupVat.GetValueOrDefault() + _termsResults.SiteGroupGuarantorForVATShortfall.GetValueOrDefault();
                            _termsResults.BankedToCompany = _termsResults.BankedToCompany.GetValueOrDefault() + _termsResults.SiteGroupVat.GetValueOrDefault() + _termsResults.SiteGroupGuarantorForVATShortfall.GetValueOrDefault();

                            switch (_termsInfo.VATGroupDeferredDest)
                            {
                                case TERMS_CUSTOMS_AND_EXCISE:
                                    _termsResults.VATGroupBankedToGroup_toCustoms = _termsResults.VATGroupBankedToGroup_toCustoms.GetValueOrDefault() + _termsResults.SiteGroupVat.GetValueOrDefault() + _termsResults.SiteGroupGuarantorForVATShortfall.GetValueOrDefault();
                                    break;
                                case TERMS_SUPPLIER:
                                    _termsResults.VATGroupBankedToGroup_toSupplier = _termsResults.VATGroupBankedToGroup_toSupplier.GetValueOrDefault() + _termsResults.SiteGroupVat.GetValueOrDefault() + _termsResults.SiteGroupGuarantorForVATShortfall.GetValueOrDefault();
                                    break;
                                default:
                                    break;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void AllocateSecSiteGroupVatMoneyToDestination()
        {
            try
            {
                LogManager.WriteLog("Inside AllocateSecSiteGroupVatMoneyToDestination...", LogManager.enumLogLevel.Info);

                if (_termsInfo.VatSecGroupUse > 0)
                {
                    _termsResults.SecSiteGroupVat = _termsResults.SecSiteGroupVat.GetValueOrDefault() + _termsResults.SecSiteGroupGuarantorForVATShortfall.GetValueOrDefault();
                    _termsResults.SecSiteGroupGuarantorForVATShortfall = 0;

                    switch (_termsInfo.VATSecGroupDest)
                    {
                        case TERMS_SUPPLIER:
                            _termsResults.SecSiteGroupVATBankedToSupplier = _termsResults.SecSiteGroupVat.GetValueOrDefault() + _termsResults.SecSiteGroupGuarantorForVATShortfall.GetValueOrDefault();
                            _termsResults.VATBankedToSupplier = _termsResults.VATBankedToSupplier.GetValueOrDefault() + _termsResults.SecSiteGroupVat.GetValueOrDefault() + _termsResults.SecSiteGroupGuarantorForVATShortfall.GetValueOrDefault();
                            _termsResults.BankedToSupplier = _termsResults.BankedToSupplier.GetValueOrDefault() + _termsResults.SecSiteGroupVat.GetValueOrDefault() + _termsResults.SecSiteGroupGuarantorForVATShortfall.GetValueOrDefault();

                            switch (_termsInfo.VATSecGroupDeferredDest)
                            {
                                case TERMS_CUSTOMS_AND_EXCISE:
                                    _termsResults.VATSecGroupBankedToSupplier_toCustoms = _termsResults.VATSecGroupBankedToSupplier_toCustoms.GetValueOrDefault() + _termsResults.SecSiteGroupVat.GetValueOrDefault() + _termsResults.SecSiteGroupGuarantorForVATShortfall.GetValueOrDefault();
                                    break;
                                case TERMS_SITE_GROUP:
                                    _termsResults.VATSecGroupBankedToSupplier_toGroup = _termsResults.VATSecGroupBankedToSupplier_toGroup.GetValueOrDefault() + _termsResults.SecSiteGroupVat.GetValueOrDefault() + _termsResults.SecSiteGroupGuarantorForVATShortfall.GetValueOrDefault();
                                    break;
                                case TERMS_SECONDARY_GROUP:
                                    _termsResults.VATSecGroupBankedToSupplier_toSecGroup = _termsResults.VATSecGroupBankedToSupplier_toSecGroup.GetValueOrDefault() + _termsResults.SecSiteGroupVat.GetValueOrDefault() + _termsResults.SecSiteGroupGuarantorForVATShortfall.GetValueOrDefault();
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case TERMS_SITE:
                        case TERMS_LEFT_ON_SITE:
                            _termsResults.SecSiteGroupVATLos = _termsResults.SecSiteGroupVat.GetValueOrDefault() + _termsResults.SecSiteGroupGuarantorForVATShortfall.GetValueOrDefault();
                            _termsResults.VATLOS = _termsResults.VATLOS.GetValueOrDefault() + _termsResults.SecSiteGroupVat.GetValueOrDefault() + _termsResults.SecSiteGroupGuarantorForVATShortfall.GetValueOrDefault();
                            _termsResults.LOS = _termsResults.LOS.GetValueOrDefault() + _termsResults.SecSiteGroupVat.GetValueOrDefault() + _termsResults.SecSiteGroupGuarantorForVATShortfall.GetValueOrDefault();

                            switch (_termsInfo.VATSecGroupDeferredDest)
                            {
                                case TERMS_SUPPLIER:
                                    _termsResults.VATSecGroupLos_toSupplier = _termsResults.VATSecGroupLos_toSupplier.GetValueOrDefault() + _termsResults.SecSiteGroupVat.GetValueOrDefault() + _termsResults.SecSiteGroupGuarantorForVATShortfall.GetValueOrDefault();
                                    break;
                                case TERMS_SITE_GROUP:
                                    _termsResults.VATSecGroupLos_toGroup = _termsResults.VATSecGroupLos_toGroup.GetValueOrDefault() + _termsResults.SecSiteGroupVat.GetValueOrDefault() + _termsResults.SecSiteGroupGuarantorForVATShortfall.GetValueOrDefault();
                                    break;
                                case TERMS_SECONDARY_GROUP:
                                    _termsResults.VATSecGroupLos_toSecGroup = _termsResults.VATSecGroupLos_toSecGroup.GetValueOrDefault() + _termsResults.SecSiteGroupVat.GetValueOrDefault() + _termsResults.SecSiteGroupGuarantorForVATShortfall.GetValueOrDefault();
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case TERMS_SITE_GROUP:
                            _termsResults.SecSiteGroupVATBankedToCompany = _termsResults.SecSiteGroupVat.GetValueOrDefault() + _termsResults.SecSiteGroupGuarantorForVATShortfall.GetValueOrDefault();
                            _termsResults.VATBankedToCompany = _termsResults.VATBankedToCompany.GetValueOrDefault() + _termsResults.SecSiteGroupVat.GetValueOrDefault() + _termsResults.SecSiteGroupGuarantorForVATShortfall.GetValueOrDefault();
                            _termsResults.BankedToCompany = _termsResults.BankedToCompany.GetValueOrDefault() + _termsResults.SecSiteGroupVat.GetValueOrDefault() + _termsResults.SecSiteGroupGuarantorForVATShortfall.GetValueOrDefault();

                            switch (_termsInfo.VATSecGroupDeferredDest)
                            {
                                case TERMS_SUPPLIER:
                                    _termsResults.VATSecGroupBankedToGroup_toSupplier = _termsResults.VATSecGroupBankedToGroup_toSupplier.GetValueOrDefault() + _termsResults.SecSiteGroupVat.GetValueOrDefault() + _termsResults.SecSiteGroupGuarantorForVATShortfall.GetValueOrDefault();
                                    break;
                                case TERMS_CUSTOMS_AND_EXCISE:
                                    _termsResults.VATSecGroupBankedToGroup_toCustoms = _termsResults.VATSecGroupBankedToGroup_toCustoms.GetValueOrDefault() + _termsResults.SecSiteGroupVat.GetValueOrDefault() + _termsResults.SecSiteGroupGuarantorForVATShortfall.GetValueOrDefault();
                                    break;
                                case TERMS_SECONDARY_GROUP:
                                    _termsResults.VATSecGroupBankedToGroup_toSecGroup = _termsResults.VATSecGroupBankedToGroup_toSecGroup.GetValueOrDefault() + _termsResults.SecSiteGroupVat.GetValueOrDefault() + _termsResults.SecSiteGroupGuarantorForVATShortfall.GetValueOrDefault();
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case TERMS_SECONDARY_GROUP:
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void AllocateGPTMoneyToDestination()
        {
            try
            {
                LogManager.WriteLog("Inside AllocateGPTMoneyToDestination...", LogManager.enumLogLevel.Info);

                if (_termsInfo.GPTUse > 0)
                {
                    switch (_termsInfo.GPTCashDestination)
                    {
                        case TERMS_SUPPLIER:
                            _termsResults.GPTBankedToSupplier = _termsResults.GPTBankedToSupplier.GetValueOrDefault() + _termsResults.GPT.GetValueOrDefault();
                            _termsResults.BankedToSupplier = _termsResults.BankedToSupplier.GetValueOrDefault() + _termsResults.GPT.GetValueOrDefault();

                            switch (_termsInfo.GPTDeferredDestination)
                            {
                                case TERMS_SITE_GROUP:
                                    _termsResults.GPTBankedToSupplier_ToGroup = _termsResults.GPTBankedToSupplier_ToGroup.GetValueOrDefault() + _termsResults.GPT.GetValueOrDefault();
                                    break;
                                case TERMS_CUSTOMS_AND_EXCISE:
                                    _termsResults.GPTBankedToSupplier_ToCustoms = _termsResults.GPTBankedToSupplier_ToCustoms.GetValueOrDefault() + _termsResults.GPT.GetValueOrDefault();
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case TERMS_SITE:
                        case TERMS_LEFT_ON_SITE:
                            _termsResults.GPTLOS = _termsResults.GPTLOS.GetValueOrDefault() + _termsResults.GPT.GetValueOrDefault();
                            _termsResults.LOS = _termsResults.LOS.GetValueOrDefault() + _termsResults.GPT.GetValueOrDefault();

                            switch (_termsInfo.GPTDeferredDestination)
                            {
                                case TERMS_SITE_GROUP:
                                    _termsResults.GPTLOS_ToGroup = _termsResults.GPTLOS_ToGroup.GetValueOrDefault() + _termsResults.GPT.GetValueOrDefault();
                                    break;
                                case TERMS_SUPPLIER:
                                    _termsResults.GPTLOS_ToSupplier = _termsResults.GPTLOS_ToSupplier.GetValueOrDefault() + _termsResults.GPT.GetValueOrDefault();
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case TERMS_SITE_GROUP:
                            _termsResults.GPTBankedToCompany = _termsResults.GPTBankedToCompany.GetValueOrDefault() + _termsResults.GPT.GetValueOrDefault();
                            _termsResults.BankedToCompany = _termsResults.BankedToCompany.GetValueOrDefault() + _termsResults.GPT.GetValueOrDefault();

                            switch (_termsInfo.OtherLicenceDeferredDest)
                            {
                                case TERMS_SUPPLIER:
                                    _termsResults.GPTBankedToGroup_ToSupplier = _termsResults.GPTBankedToGroup_ToSupplier.GetValueOrDefault() + _termsResults.GPT.GetValueOrDefault();
                                    break;
                            }
                            break;
                        case TERMS_SECONDARY_GROUP:
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void AllocateLicenceMoneyToDestination()
        {
            try
            {
                LogManager.WriteLog("Inside AllocateLicenceMoneyToDestination...", LogManager.enumLogLevel.Info);

                if (_termsInfo.OtherLicenceUse > 0)
                {
                    switch (_termsInfo.OtherLicenceCashDest)
                    {
                        case TERMS_SUPPLIER:
                            _termsResults.LicenceChargeBankedToSupplier = _termsResults.LicenceChargeBankedToSupplier.GetValueOrDefault() + _termsResults.LicenceCharge.GetValueOrDefault();
                            _termsResults.BankedToSupplier = _termsResults.BankedToSupplier.GetValueOrDefault() + _termsResults.LicenceCharge.GetValueOrDefault();

                            switch (_termsInfo.OtherLicenceDeferredDest)
                            {
                                case TERMS_SITE_GROUP:
                                    _termsResults.LicenceChargeBankedToSupplier_toGroup = _termsResults.LicenceChargeBankedToSupplier_toGroup.GetValueOrDefault() + _termsResults.LicenceCharge.GetValueOrDefault();
                                    break;
                            }
                            break;
                        case TERMS_SITE:
                        case TERMS_LEFT_ON_SITE:
                            _termsResults.LicenceChargeLOS = _termsResults.LicenceChargeLOS.GetValueOrDefault() + _termsResults.LicenceCharge.GetValueOrDefault();
                            _termsResults.LOS = _termsResults.LOS.GetValueOrDefault() + _termsResults.LicenceCharge.GetValueOrDefault();

                            switch (_termsInfo.OtherLicenceDeferredDest)
                            {
                                case TERMS_SITE_GROUP:
                                    _termsResults.LicenceChargeLOS_toGroup = _termsResults.LicenceChargeLOS_toGroup.GetValueOrDefault() + _termsResults.LicenceCharge.GetValueOrDefault();
                                    break;
                                case TERMS_SUPPLIER:
                                    _termsResults.LicenceChargeLOS_toSupplier = _termsResults.LicenceChargeLOS_toSupplier.GetValueOrDefault() + _termsResults.LicenceCharge.GetValueOrDefault();
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case TERMS_SITE_GROUP:
                            _termsResults.LicenceChargeBankedToCompany = _termsResults.LicenceChargeBankedToCompany.GetValueOrDefault() + _termsResults.LicenceCharge.GetValueOrDefault();
                            _termsResults.BankedToCompany = _termsResults.BankedToCompany.GetValueOrDefault() + _termsResults.LicenceCharge.GetValueOrDefault();

                            switch (_termsInfo.OtherLicenceDeferredDest)
                            {
                                case TERMS_SUPPLIER:
                                    _termsResults.LicenceChargeBankedToGroup_toSupplier = _termsResults.LicenceChargeBankedToGroup_toSupplier.GetValueOrDefault() + _termsResults.LicenceCharge.GetValueOrDefault();
                                    break;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void AllocatePrizeMoneyToDestination()
        {
            try
            {
                LogManager.WriteLog("Inside AllocatePrizeMoneyToDestination...", LogManager.enumLogLevel.Info);

                if (_termsInfo.OtherPrizeUse > 0)
                {
                    switch (_termsInfo.OtherPrizeCashDest)
                    {
                        case TERMS_SUPPLIER:
                            _termsResults.PrizeChargeBankedToSupplier = _termsResults.PrizeChargeBankedToSupplier.GetValueOrDefault() + _termsResults.PrizeCharge.GetValueOrDefault();
                            _termsResults.BankedToSupplier = _termsResults.BankedToSupplier.GetValueOrDefault() + _termsResults.PrizeCharge.GetValueOrDefault();

                            switch (_termsInfo.OtherPrizeDeferredDest)
                            {
                                case TERMS_SITE_GROUP:
                                    _termsResults.PrizeChargeBankedToSupplier_toGroup = _termsResults.PrizeChargeBankedToSupplier_toGroup.GetValueOrDefault() + _termsResults.PrizeCharge.GetValueOrDefault();
                                    break;
                            }
                            break;
                        case TERMS_SITE:
                        case TERMS_LEFT_ON_SITE:
                            _termsResults.PrizeChargeLOS = _termsResults.PrizeChargeLOS.GetValueOrDefault() + _termsResults.PrizeCharge.GetValueOrDefault();
                            _termsResults.LOS = _termsResults.LOS.GetValueOrDefault() + _termsResults.PrizeCharge.GetValueOrDefault();

                            switch (_termsInfo.OtherPrizeDeferredDest)
                            {
                                case TERMS_SITE_GROUP:
                                    _termsResults.PrizeChargeLOS_toGroup = _termsResults.PrizeChargeLOS_toGroup.GetValueOrDefault() + _termsResults.PrizeCharge.GetValueOrDefault();
                                    break;
                                case TERMS_SUPPLIER:
                                    _termsResults.PrizeChargeLOS_toSupplier = _termsResults.PrizeChargeLOS_toSupplier.GetValueOrDefault() + _termsResults.PrizeCharge.GetValueOrDefault();
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case TERMS_SITE_GROUP:
                            _termsResults.PrizeChargeBankedToCompany = _termsResults.PrizeChargeBankedToCompany.GetValueOrDefault() + _termsResults.PrizeCharge.GetValueOrDefault();
                            _termsResults.BankedToCompany = _termsResults.BankedToCompany.GetValueOrDefault() + _termsResults.PrizeCharge.GetValueOrDefault();

                            switch (_termsInfo.OtherPrizeDeferredDest)
                            {
                                case TERMS_SUPPLIER:
                                    _termsResults.PrizeChargeBankedToGroup_toSupplier = _termsResults.PrizeChargeBankedToGroup_toSupplier.GetValueOrDefault() + _termsResults.PrizeCharge.GetValueOrDefault();
                                    break;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void AllocateConsultancyMoneyToDestination()
        {
            try
            {
                LogManager.WriteLog("Inside AllocateConsultancyMoneyToDestination...", LogManager.enumLogLevel.Info);

                if (_termsInfo.OtherConsultancyUse > 0)
                {
                    switch (_termsInfo.OtherConsultancyCashDest)
                    {
                        case TERMS_SUPPLIER:
                            _termsResults.ConsultancyChargeBankedToSupplier = _termsResults.ConsultancyChargeBankedToSupplier.GetValueOrDefault() + _termsResults.ConsultancyCharge.GetValueOrDefault();
                            _termsResults.BankedToSupplier = _termsResults.BankedToSupplier.GetValueOrDefault() + _termsResults.ConsultancyCharge.GetValueOrDefault();

                            switch (_termsInfo.OtherConsultancyDeferredDest)
                            {
                                case TERMS_SITE_GROUP:
                                    _termsResults.ConsultancyChargeBankedToSupplier_toGroup = _termsResults.ConsultancyChargeBankedToSupplier_toGroup.GetValueOrDefault() + _termsResults.ConsultancyCharge.GetValueOrDefault();
                                    break;
                            }
                            break;
                        case TERMS_SITE:
                        case TERMS_LEFT_ON_SITE:
                            _termsResults.ConsultancyChargeLOS = _termsResults.ConsultancyChargeLOS.GetValueOrDefault() + _termsResults.ConsultancyCharge.GetValueOrDefault();
                            _termsResults.LOS = _termsResults.LOS.GetValueOrDefault() + _termsResults.ConsultancyCharge.GetValueOrDefault();

                            switch (_termsInfo.OtherConsultancyDeferredDest)
                            {
                                case TERMS_SITE_GROUP:
                                    _termsResults.ConsultancyChargeLOS_toGroup = _termsResults.ConsultancyChargeLOS_toGroup.GetValueOrDefault() + _termsResults.ConsultancyCharge.GetValueOrDefault();
                                    break;
                                case TERMS_SUPPLIER:
                                    _termsResults.ConsultancyChargeLOS_toSupplier = _termsResults.ConsultancyChargeLOS_toSupplier.GetValueOrDefault() + _termsResults.ConsultancyCharge.GetValueOrDefault();
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case TERMS_SITE_GROUP:
                            _termsResults.ConsultancyChargeBankedToCompany = _termsResults.ConsultancyChargeBankedToCompany.GetValueOrDefault() + _termsResults.ConsultancyCharge.GetValueOrDefault();
                            _termsResults.BankedToCompany = _termsResults.BankedToCompany.GetValueOrDefault() + _termsResults.ConsultancyCharge.GetValueOrDefault();

                            switch (_termsInfo.OtherConsultancyDeferredDest)
                            {
                                case TERMS_SUPPLIER:
                                    _termsResults.ConsultancyChargeBankedToGroup_toSupplier = _termsResults.ConsultancyChargeBankedToGroup_toSupplier.GetValueOrDefault() + _termsResults.ConsultancyCharge.GetValueOrDefault();
                                    break;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void AllocateRoyaltyMoneyToDestination()
        {
            try
            {
                LogManager.WriteLog("Inside AllocateRoyaltyMoneyToDestination...", LogManager.enumLogLevel.Info);

                if (_termsInfo.OtherRoyaltyUse > 0)
                {
                    switch (_termsInfo.OtherRoyaltyCashDest)
                    {
                        case TERMS_SUPPLIER:
                            _termsResults.RoyaltyChargeBankedToSupplier = _termsResults.RoyaltyChargeBankedToSupplier.GetValueOrDefault() + _termsResults.RoyaltyCharge.GetValueOrDefault();
                            _termsResults.BankedToSupplier = _termsResults.BankedToSupplier.GetValueOrDefault() + _termsResults.RoyaltyCharge.GetValueOrDefault();

                            switch (_termsInfo.OtherRoyaltyDeferredDest)
                            {
                                case TERMS_SITE_GROUP:
                                    _termsResults.RoyaltyChargeBankedToSupplier_toGroup = _termsResults.RoyaltyChargeBankedToSupplier_toGroup.GetValueOrDefault() + _termsResults.RoyaltyCharge.GetValueOrDefault();
                                    break;
                            }
                            break;
                        case TERMS_SITE:
                        case TERMS_LEFT_ON_SITE:
                            _termsResults.RoyaltyChargeLOS = _termsResults.RoyaltyChargeLOS.GetValueOrDefault() + _termsResults.RoyaltyCharge.GetValueOrDefault();
                            _termsResults.LOS = _termsResults.LOS.GetValueOrDefault() + _termsResults.RoyaltyCharge.GetValueOrDefault();

                            switch (_termsInfo.OtherRoyaltyDeferredDest)
                            {
                                case TERMS_SITE_GROUP:
                                    _termsResults.RoyaltyChargeLOS_toGroup = _termsResults.RoyaltyChargeLOS_toGroup.GetValueOrDefault() + _termsResults.RoyaltyCharge.GetValueOrDefault();
                                    break;
                                case TERMS_SUPPLIER:
                                    _termsResults.RoyaltyChargeLOS_toSupplier = _termsResults.RoyaltyChargeLOS_toSupplier.GetValueOrDefault() + _termsResults.RoyaltyCharge.GetValueOrDefault();
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case TERMS_SITE_GROUP:
                            _termsResults.RoyaltyChargeBankedToCompany = _termsResults.RoyaltyChargeBankedToCompany.GetValueOrDefault() + _termsResults.RoyaltyCharge.GetValueOrDefault();
                            _termsResults.BankedToCompany = _termsResults.BankedToCompany.GetValueOrDefault() + _termsResults.RoyaltyCharge.GetValueOrDefault();

                            switch (_termsInfo.OtherRoyaltyDeferredDest)
                            {
                                case TERMS_SUPPLIER:
                                    _termsResults.RoyaltyChargeBankedToGroup_toSupplier = _termsResults.RoyaltyChargeBankedToGroup_toSupplier.GetValueOrDefault() + _termsResults.RoyaltyCharge.GetValueOrDefault();
                                    break;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void AllocateOther1MoneyToDestination()
        {
            try
            {
                LogManager.WriteLog("Inside AllocateOther1MoneyToDestination...", LogManager.enumLogLevel.Info);

                if (_termsInfo.OtherOther1Use > 0)
                {
                    switch (_termsInfo.OtherOther1CashDest)
                    {
                        case TERMS_SUPPLIER:
                            _termsResults.Other1ChargeBankedToSupplier = _termsResults.Other1ChargeBankedToSupplier.GetValueOrDefault() + _termsResults.Other1Charge.GetValueOrDefault();
                            _termsResults.BankedToSupplier = _termsResults.BankedToSupplier.GetValueOrDefault() + _termsResults.Other1Charge.GetValueOrDefault();

                            switch (_termsInfo.OtherOther1DeferredDest)
                            {
                                case TERMS_SITE_GROUP:
                                    _termsResults.Other1ChargeBankedToSupplier_toGroup = _termsResults.Other1ChargeBankedToSupplier_toGroup.GetValueOrDefault() + _termsResults.Other1Charge.GetValueOrDefault();
                                    break;
                            }
                            break;
                        case TERMS_SITE:
                        case TERMS_LEFT_ON_SITE:
                            _termsResults.Other1ChargeLOS = _termsResults.Other1ChargeLOS.GetValueOrDefault() + _termsResults.Other1Charge.GetValueOrDefault();
                            _termsResults.LOS = _termsResults.LOS + _termsResults.Other1Charge.GetValueOrDefault();

                            switch (_termsInfo.OtherOther1DeferredDest)
                            {
                                case TERMS_SITE_GROUP:
                                    _termsResults.Other1ChargeLOS_toGroup = _termsResults.Other1ChargeLOS_toGroup.GetValueOrDefault() + _termsResults.Other1Charge.GetValueOrDefault();
                                    break;
                                case TERMS_SUPPLIER:
                                    _termsResults.Other1ChargeLOS_toSupplier = _termsResults.Other1ChargeLOS_toSupplier.GetValueOrDefault() + _termsResults.Other1Charge.GetValueOrDefault();
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case TERMS_SITE_GROUP:
                            _termsResults.Other1ChargeBankedToCompany = _termsResults.Other1ChargeBankedToCompany.GetValueOrDefault() + _termsResults.Other1Charge.GetValueOrDefault();
                            _termsResults.BankedToCompany = _termsResults.BankedToCompany.GetValueOrDefault() + _termsResults.Other1Charge.GetValueOrDefault();

                            switch (_termsInfo.OtherOther1DeferredDest)
                            {
                                case TERMS_SUPPLIER:
                                    _termsResults.Other1ChargeBankedToGroup_toSupplier = _termsResults.Other1ChargeBankedToGroup_toSupplier.GetValueOrDefault() + _termsResults.Other1Charge.GetValueOrDefault();
                                    break;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void AllocateOther2MoneyToDestination()
        {
            try
            {
                LogManager.WriteLog("Inside AllocateOther2MoneyToDestination...", LogManager.enumLogLevel.Info);

                if (_termsInfo.OtherOther2Use > 0)
                {
                    switch (_termsInfo.OtherOther2CashDest)
                    {
                        case TERMS_SUPPLIER:
                            _termsResults.Other2ChargeBankedToSupplier = _termsResults.Other2ChargeBankedToSupplier.GetValueOrDefault() + _termsResults.Other2Charge.GetValueOrDefault();
                            _termsResults.BankedToSupplier = _termsResults.BankedToSupplier.GetValueOrDefault() + _termsResults.Other2Charge.GetValueOrDefault();

                            switch (_termsInfo.OtherOther2DeferredDest)
                            {
                                case TERMS_SITE_GROUP:
                                    _termsResults.Other2ChargeBankedToSupplier_toGroup = _termsResults.Other2ChargeBankedToSupplier_toGroup.GetValueOrDefault() + _termsResults.Other2Charge.GetValueOrDefault();
                                    break;
                            }
                            break;
                        case TERMS_SITE:
                        case TERMS_LEFT_ON_SITE:
                            _termsResults.Other2ChargeLOS = _termsResults.Other2ChargeLOS.GetValueOrDefault() + _termsResults.Other2Charge.GetValueOrDefault();
                            _termsResults.LOS = _termsResults.LOS.GetValueOrDefault() + _termsResults.Other2Charge.GetValueOrDefault();

                            switch (_termsInfo.OtherOther2DeferredDest)
                            {
                                case TERMS_SITE_GROUP:
                                    _termsResults.Other2ChargeLOS_toGroup = _termsResults.Other2ChargeLOS_toGroup.GetValueOrDefault() + _termsResults.Other2Charge.GetValueOrDefault();
                                    break;
                                case TERMS_SUPPLIER:
                                    _termsResults.Other2ChargeLOS_toSupplier = _termsResults.Other2ChargeLOS_toSupplier.GetValueOrDefault() + _termsResults.Other2Charge.GetValueOrDefault();
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case TERMS_SITE_GROUP:
                            _termsResults.Other2ChargeBankedToCompany = _termsResults.Other2ChargeBankedToCompany.GetValueOrDefault() + _termsResults.Other2Charge.GetValueOrDefault();
                            _termsResults.BankedToCompany = _termsResults.BankedToCompany.GetValueOrDefault() + _termsResults.Other2Charge.GetValueOrDefault();

                            switch (_termsInfo.OtherOther2DeferredDest)
                            {
                                case TERMS_SUPPLIER:
                                    _termsResults.Other2ChargeBankedToGroup_toSupplier = _termsResults.Other2ChargeBankedToGroup_toSupplier.GetValueOrDefault() + _termsResults.Other2Charge.GetValueOrDefault();
                                    break;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void CalculatePostTermsSupplierSupplementalCharge()
        {
            int? _tempDays = 0;
            float? _dailyRate = 0;
            int? _numOfDays = 0;
            float? _tempCharge = 0;
            DateTime _previousDate;
            try
            {
                LogManager.WriteLog("Inside CalculatePostTermsSupplierSupplementalCharge...", LogManager.enumLogLevel.Info);

                if (_termsInfo.SupplierSupplementalCharge == 0 && _termsInfo.SupplierSupplementalChargeAfter == 0 && _termsInfo.SupplierSupplementalChargeBefore == 0)
                {
                    _numOfDays = _termsInfo.CollectionDays;

                    _previousDate = IsValidDateTime(_termsInfo.SupplierValueBeforeChangeDate) ? Convert.ToDateTime(_termsInfo.SupplierValueBeforeChangeDate) : Convert.ToDateTime(_termsInfo.CollectionDate).AddYears(-10);
                    _previousDate = IsValidDateTime(_termsInfo.SupplierValueAfterChangeDate) ? Convert.ToDateTime(_termsInfo.SupplierValueAfterChangeDate) : Convert.ToDateTime(_termsInfo.CollectionDate).AddYears(10);

                    //Previous Rent Band
                    if (IsValidDateTime(_termsInfo.SupplierValueBeforeChangeDate))
                    {
                        try
                        {
                            _tempDays = Convert.ToInt32(
                                    Min<double>(
                                        Convert.ToDouble(_numOfDays),
                                        Max<double>(
                                            0, (Convert.ToDateTime(_termsInfo.CollectionDate).AddDays(0.0 - Convert.ToDouble(_termsInfo.CollectionDays)) - Convert.ToDateTime(_termsInfo.SupplierValueBeforeChangeDate)).TotalDays)
                                    ));
                        }
                        catch (Exception ex) { }

                        if (_tempDays > 0)
                        {
                            _dailyRate = _termsInfo.SupplierSupplementalChargeBefore / 7;
                            _tempCharge = (_dailyRate * _tempDays);
                            _numOfDays = _numOfDays - _tempDays;
                        }
                    }

                    //Current rent band
                    try
                    {
                        _tempDays = Convert.ToInt32(
                                            Min<double>(
                                                Convert.ToDouble(_numOfDays),
                                                Max<double>(
                                                    0, (Max<DateTime>(_previousDate, Convert.ToDateTime(_termsInfo.CollectionDate).AddDays(0.0 - Convert.ToDouble(_termsInfo.CollectionDays)))
                                                            - Min<DateTime>(Convert.ToDateTime(_termsInfo.SupplierValueAfterChangeDate), Convert.ToDateTime(_termsInfo.CollectionDate))).TotalDays)
                                       ));
                    }
                    catch (Exception ex) { }

                    if (_tempDays > 0)
                    {
                        _dailyRate = _termsInfo.SupplierSupplementalCharge / 7;
                        _tempCharge = _tempCharge + (_dailyRate * _tempDays);
                        _numOfDays = _numOfDays - _tempDays;
                    }

                    //Future Rent Band
                    if (_numOfDays > 0)
                    {
                        _dailyRate = _termsInfo.SupplierSupplementalChargeAfter / 7;
                        _tempCharge = _tempCharge + (_dailyRate * _numOfDays);
                    }

                    _termsResults.SupplierSupplementalCharge = _tempCharge;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void CalculatePostTermsVATSupplierWhenGPT()
        {
            try
            {
                LogManager.WriteLog("Inside CalculatePostTermsVATSupplierWhenGPT...", LogManager.enumLogLevel.Info);

                _termsResults.SupplierVatShouldGet = _termsResults.SupplierShare - (_termsResults.SupplierShare / (1 + SystemVATRate));
                _termsResults.SupplierShare = _termsResults.SupplierShare / (1 + SystemVATRate);

                _termsResults.SupplierVatShouldGet = _termsResults.SupplierVatShouldGet + (_termsResults.SupplierRent - (_termsResults.SupplierRent / (1 + SystemVATRate)));
                _termsResults.SupplierRent = _termsResults.SupplierRent / (1 + SystemVATRate);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        #endregion CalculatePostTerms

        #region UpdateCollectionWithTermsResults
        private bool UpdateTermsResultsForCollectionID()
        {
            try
            {
                LogManager.WriteLog("Inside UpdateTermsResultsForCollectionID...", LogManager.enumLogLevel.Info);

                DataContext.UpdateCollectionDetailsWithTermsResults(_collectionId, _collectionGross, _termsResults.CollectionDays, _prize, 0, _termsResults.SiteGroupShare + _termsResults.SiteGroupRent, _termsResults.SupplierRent + _termsResults.SupplierShare,
                    _termsResults.SiteShare + _termsResults.SiteRent, _termsResults.LicenceCharge, _termsResults.OutputVAT, _termsResults.SiteGroupVat, _termsResults.SupplierVat, _termsResults.SiteVat, _termsResults.VATLOS, _termsResults.VATBankedToCompany,
                    _termsResults.VATBankedToSupplier, _termsResults.SupplierVATLOS, _termsResults.SiteGroupVATLos, _termsResults.SecSiteGroupVATLos, _termsResults.SiteVATLos, _termsResults.SupplierVATBankedToCompany, _termsResults.SiteGroupVATBankedToCompany,
                    _termsResults.SecSiteGroupVATBankedToCompany, _termsResults.SiteVATBankedToCompany, _termsResults.SupplierVATBankedToSupplier, _termsResults.SiteGroupVATBankedToSupplier, _termsResults.SecSiteGroupVATBankedToSupplier,
                    _termsResults.SiteVATBankedToSupplier, _termsResults.SiteGroupShareLOS, _termsResults.SupplierShareLOS, _termsResults.SiteShareLOS, _termsResults.BankedToCompany, _termsResults.BankedToSupplier, _termsResults.SecSiteGroupVat,
                    _termsResults.SecSiteGroupShareLOS, _termsResults.SiteGroupShareBankedToCompany, _termsResults.SiteGroupShareBankedToSupplier, _termsResults.SecSiteGroupShareBankedToCompany, _termsResults.SecSiteGroupShareBankedToSupplier,
                    _termsResults.SupplierShareBankedToCompany, _termsResults.SupplierShareBankedToSupplier, _termsResults.SiteShareBankedToCompany, _termsResults.SiteShareBankedToSupplier, _termsResults.LicenceChargeBankedToCompany,
                    _termsResults.LicenceChargeBankedToSupplier, _termsResults.LicenceChargeLOS, _termsResults.LOS, _termsResults.SiteGroupShareLOS_toGroup, _termsResults.SiteGroupShareBankedToSupplier_toGroup, _termsResults.SecSiteGroupShareLOS_toGroup,
                    _termsResults.SecSiteGroupShareLOS_toSecGroup, _termsResults.SecSiteGroupShareBankedToSupplier_toGroup, _termsResults.SecSiteGroupShareBankedToSupplier_toSecGroup, _termsResults.SecSiteGroupShareBankedToGroup_toSecGroup,
                    _termsResults.SupplierShareLOS_toSupplier, _termsResults.SupplierShareBankedToGroup_toSupplier, _termsResults.SiteShareBankedToSupplier_toSite, _termsResults.SiteShareBankedToSupplier_toGroup, _termsResults.SiteShareBankedToGroup_toSite,
                    _termsResults.VATOutputLos_toSupplier, _termsResults.VATOutputLos_toGroup, _termsResults.VATOutputBankedToSupplier_toGroup, _termsResults.VATOutputBankedToSupplier_toSecGroup, _termsResults.VATOutputBankedToSupplier_toCustoms,
                    _termsResults.VATOutputBankedToGroup_toSupplier, _termsResults.VATOutputBankedToGroup_toCustoms, _termsResults.VATSupplierLos_toSupplier, _termsResults.VATSupplierLos_toGroup, _termsResults.VATSupplierBankedToSupplier_toGroup,
                    _termsResults.VATSupplierBankedToSupplier_toSecGroup, _termsResults.VATSupplierBankedToSupplier_toCustoms, _termsResults.VATSupplierBankedToGroup_toSupplier, _termsResults.VATSupplierBankedToGroup_toCustoms, _termsResults.VATSiteLos_toSupplier,
                    _termsResults.VATSiteLos_toGroup, _termsResults.VATSiteLos_toCustoms, _termsResults.VATSiteBankedToSupplier_toGroup, _termsResults.VATSiteBankedToSupplier_toCustoms, _termsResults.VATSiteBankedToSupplier_toSite,
                    _termsResults.VATSiteBankedToGroup_toSupplier, _termsResults.VATSiteBankedToGroup_toCustoms, _termsResults.VATGroupLos_toSupplier, _termsResults.VATGroupLos_toGroup, _termsResults.VATGroupBankedToSupplier_toGroup,
                    _termsResults.VATGroupBankedToSupplier_toCustoms, _termsResults.VATGroupBankedToGroup_toSupplier, _termsResults.VATGroupBankedToGroup_toCustoms, _termsResults.VATSecGroupLos_toSupplier, _termsResults.VATSecGroupLos_toGroup,
                    _termsResults.VATSecGroupLos_toSecGroup, _termsResults.VATSecGroupBankedToSupplier_toGroup, _termsResults.VATSecGroupBankedToSupplier_toSecGroup, _termsResults.VATSecGroupBankedToSupplier_toCustoms, _termsResults.VATSecGroupBankedToGroup_toSupplier,
                    _termsResults.VATSecGroupBankedToGroup_toSecGroup, _termsResults.VATSecGroupBankedToGroup_toCustoms, _termsResults.LicenceChargeLOS_toSupplier, _termsResults.LicenceChargeLOS_toGroup, _termsResults.LicenceChargeBankedToSupplier_toGroup,
                    _termsResults.LicenceChargeBankedToGroup_toSupplier, _termsResults.PrizeChargeLOS_toSupplier, _termsResults.PrizeChargeLOS_toGroup, _termsResults.PrizeChargeBankedToSupplier_toGroup, _termsResults.PrizeChargeBankedToGroup_toSupplier,
                    _termsResults.ConsultancyChargeLOS_toSupplier, _termsResults.ConsultancyChargeLOS_toGroup, _termsResults.ConsultancyChargeBankedToSupplier_toGroup, _termsResults.ConsultancyChargeBankedToGroup_toSupplier, _termsResults.RoyaltyChargeLOS_toSupplier,
                    _termsResults.RoyaltyChargeLOS_toGroup, _termsResults.RoyaltyChargeBankedToSupplier_toGroup, _termsResults.RoyaltyChargeBankedToGroup_toSupplier, _termsResults.Other1ChargeLOS_toSupplier, _termsResults.Other1ChargeLOS_toGroup,
                    _termsResults.Other1ChargeBankedToSupplier_toGroup, _termsResults.Other1ChargeBankedToGroup_toSupplier, _termsResults.Other2ChargeLOS_toSupplier, _termsResults.Other2ChargeLOS_toGroup, _termsResults.Other2ChargeBankedToSupplier_toGroup,
                    _termsResults.Other2ChargeBankedToGroup_toSupplier);

                DataContext.UpdateCollectionTermsWithTermsResults(_collectionId, _termsResults.SiteGroupShare + _termsResults.SiteGroupRent, _termsResults.SupplierRent + _termsResults.SupplierShare, _termsResults.SiteShare + _termsResults.SiteRent,
                    _termsResults.LicenceCharge, _termsResults.OutputVAT, _termsResults.SiteGroupVat, _termsResults.SupplierVat, _termsResults.SiteVat, _termsResults.VATLOS, _termsResults.VATBankedToCompany, _termsResults.VATBankedToSupplier,
                    _termsResults.SupplierVATLOS, _termsResults.SiteGroupVATLos, _termsResults.SecSiteGroupVATLos, _termsResults.SiteVATLos, _termsResults.SupplierVATBankedToCompany, _termsResults.SiteGroupVATBankedToCompany,
                    _termsResults.SecSiteGroupVATBankedToCompany, _termsResults.SiteVATBankedToCompany, _termsResults.SupplierVATBankedToSupplier, _termsResults.SiteGroupVATBankedToSupplier, _termsResults.SecSiteGroupVATBankedToSupplier,
                    _termsResults.SiteVATBankedToSupplier, _termsResults.SiteGroupShareLOS, _termsResults.SupplierShareLOS, _termsResults.SiteShareLOS, _termsResults.BankedToCompany, _termsResults.BankedToSupplier, _termsResults.SecSiteGroupVat,
                    _termsResults.SecSiteGroupShareLOS, _termsResults.SiteGroupShareBankedToCompany, _termsResults.SiteGroupShareBankedToSupplier, _termsResults.SecSiteGroupShareBankedToCompany, _termsResults.SecSiteGroupShareBankedToSupplier,
                    _termsResults.SupplierShareBankedToCompany, _termsResults.SupplierShareBankedToSupplier, _termsResults.SiteShareBankedToCompany, _termsResults.SiteShareBankedToSupplier, _termsResults.LicenceChargeBankedToCompany,
                    _termsResults.LicenceChargeBankedToSupplier, _termsResults.LicenceChargeLOS, _termsResults.LOS);

                return true;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
        }
        #endregion UpdateCollectionWithTermsResults

        #region Common Methods
        private bool IsValidDateTime(string _dateTime)
        {
            try
            {
                Convert.ToDateTime(_dateTime);
                return true;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return false;
        }

        private T Max<T>(T value1, T value2) where T : IComparable
        {
            return value1.CompareTo(value2) > 0 ? value1 : value2;
        }

        private T Min<T>(T value1, T value2) where T : IComparable
        {
            return value1.CompareTo(value2) < 0 ? value1 : value2;
        }

        private float Format2DPs(float floatValue)
        {
            try
            {
                return (float)Math.Round((decimal)floatValue, 2);
            }
            catch
            {
                return -1;
            }
        }
        #endregion Common Methods

        #endregion Private Methods
    }
    #endregion Class
}
