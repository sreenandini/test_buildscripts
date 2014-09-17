using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.EnterpriseBusiness.Entities
{
    public class RentInfo
    {
        public float? Rent {get; set;}
        public float? SuppCharge {get; set;}
        public float? SuppChargeEndPeriod {get; set;}         
    }

    public class TermsInfo
    {
        public bool? TermsValid {get; set;}
        public bool? BarPosSetForNoTerms {get; set;}
        public int? InstallationID {get; set;}
        public int? BarPosID {get; set;}
        public int? MachineID {get; set;}
        public int? MachineClassID {get; set;}        
        public string CollectionDate {get; set;}
        public string LastCollectionDate {get; set;}
        public int? CollectionDays {get; set;}
        public int? DownDays {get; set;}
        public string RentPaidUntil {get; set;}
        public int? CollectionPeriod {get; set;}
        public string PrizeType {get; set;}

        public bool? UseSplitRents;
        public float? CollectionValue {get; set;}
        public float? PrizeValue {get; set;}

        //Partners
        public int? SupplierUse {get; set;}
        public int? SupplierCashDestination {get; set;}
        public int? SupplierDeferredDest {get; set;}
        public int? SupplierType {get; set;}
        public float? SupplierValue {get; set;} 
        public float? SupplierSupplementalCharge {get; set;} 
        public float? SupplierValueBefore {get; set;} 
        public float? SupplierSupplementalChargeBefore {get; set;}         
        public string SupplierValueBeforeChangeDate {get; set;}
        public float? SupplierValueAfter {get; set;}
        public float? SupplierSupplementalChargeAfter {get; set;}
        public string SupplierValueAfterChangeDate {get; set;}
        public bool? SupplierValueGuaranteed {get; set;}
        public float? SupplierShare {get; set;}
        public bool? SupplierShareGuaranteed {get; set;}
        public int? SupplierGuarantor {get; set;}
        public float? SupplierGuarantorPercentage {get; set;}
    
        public int? SiteUse {get; set;}
        public int? SiteCashDestination {get; set;}
        public int? SiteDeferredDest {get; set;}
        public int? SiteType {get; set;}
        public float? SiteValue {get; set;}
        public bool? SiteValueGuaranteed {get; set;}
        public float? SiteShare {get; set;}
        public bool? SiteShareGuaranteed {get; set;}
        public int? SiteGuarantor {get; set;}
        public float? SiteGuarantorPercentage {get; set;}
    
        public int? GroupUse {get; set;}
        public int? GroupCashDestination {get; set;}
        public int? GroupDeferredDest {get; set;}
        public int? GroupType {get; set;}
        public float? GroupValue {get; set;}
        public bool? GroupValueGuaranteed {get; set;}
        public float? GroupShare {get; set;}
        public bool? GroupShareGuaranteed {get; set;}
        public int? GroupGuarantor {get; set;}
        public float? GroupGuarantorPercentage {get; set;}
    
        public int? SecGroupUse {get; set;}
        public int? SecGroupCashDestination {get; set;}
        public int? SecGroupDeferredDest {get; set;}
        public int? SecGroupType {get; set;}
        public float? SecGroupValue {get; set;}
        public bool? SecGroupValueGuaranteed {get; set;}
        public float? SecGroupShare {get; set;}
        public bool? SecGroupShareGuaranteed {get; set;}
        public int? SecGroupGuarantor {get; set;}
        public float? SecGroupGuarantorPercentage {get; set;}
    
        //VAT
        public int? VATOutputUse {get; set;}
        public int? VATCashDestination {get; set;}
        public int? VATDeferredDest {get; set;}
        public int? VATSupplierUse {get; set;}
        public int? VATSupplierDest {get; set;}
        public int? VATSupplierDeferredDest {get; set;}
        public int? VATSupplierShortfallGuarantor {get; set;}
        public int? VATSiteUse {get; set;}
        public int? VATSiteDest {get; set;}
        public int? VatSiteDeferredDest {get; set;}
        public int? VATSiteShortfallGuarantor {get; set;}
        public int? VATGroupUse {get; set;}
        public int? VATGroupDest {get; set;}
        public int? VATGroupDeferredDest {get; set;}
        public int? VATGroupShortfallGuarantor {get; set;}
        public int? VatSecGroupUse {get; set;}
        public int? VATSecGroupDest {get; set;}
        public int? VATSecGroupDeferredDest {get; set;}
        public int? VATSecGroupShortfallGuarantor {get; set;}
    
        //GPT
        public int? GPTUse {get; set;}
        public int? GPTCashDestination {get; set;}
        public int? GPTDeferredDestination {get; set;}
    
        //Other
        public int? OtherLicenceUse {get; set;}
        public bool? OtherLicenceVAT {get; set;}
        public int? OtherLicenceCashDest {get; set;}
        public int? OtherLicenceDeferredDest {get; set;}
        public float? OtherLicenceCharge {get; set;}
        public int? OtherLicencePaidBy {get; set;}
        public int? OtherLicenceGuarantor {get; set;}
        public int? OtherLicenceFrequency {get; set;}
    
        public int? OtherPrizeUse {get; set;}
        public bool? OtherPrizeVAT {get; set;}
        public int? OtherPrizeCashDest {get; set;}
        public int? OtherPrizeDeferredDest {get; set;}
        public float? OtherPrizeCharge {get; set;}
        public int? OtherPrizePaidBy {get; set;}
        public int? OtherPrizeGuarantor {get; set;}
        public int? OtherPrizeFrequency {get; set;}
    
        public int? OtherConsultancyUse {get; set;}
        public bool? OtherConsultancyVAT {get; set;}
        public int? OtherConsultancyCashDest {get; set;}
        public int? OtherConsultancyDeferredDest {get; set;}
        public float? OtherConsultancyCharge {get; set;}
        public int? OtherConsultancyPaidBy {get; set;}
        public int? OtherConsultancyGuarantor {get; set;}
        public int? OtherConsultancyFrequency {get; set;}
    
        public int? OtherRoyaltyUse {get; set;}
        public bool? OtherRoyaltyVAT {get; set;}
        public int? OtherRoyaltyCashDest {get; set;}
        public int? OtherRoyaltyDeferredDest {get; set;}
        public float? OtherRoyaltyCharge  {get; set;}
        public int? OtherRoyaltyPaidBy {get; set;}
        public int? OtherRoyaltyGuarantor {get; set;}
        public int? OtherRoyaltyFrequency {get; set;}
    
        public int? OtherOther1Use {get; set;}
        public bool? OtherOther1VAT {get; set;}
        public int? OtherOther1CashDest {get; set;}
        public int? OtherOther1DeferredDest {get; set;}
        public float? OtherOther1Charge {get; set;}
        public int? OtherOther1PaidBy {get; set;}
        public int? OtherOther1Guarantor {get; set;}
        public int? OtherOther1Frequency {get; set;}
    
        public int? OtherOther2Use {get; set;}
        public bool? OtherOther2VAT {get; set;}
        public int? OtherOther2CashDest {get; set;}
        public int? OtherOther2DeferredDest {get; set;}
        public float? OtherOther2Charge {get; set;}
        public int? OtherOther2PaidBy {get; set;}
        public int? OtherOther2Guarantor {get; set;}
        public int? OtherOther2Frequency {get; set;}
    
        public float? LondonRent {get; set;}
    
        //Recovery
        public float? RecoveryBarPosPreVat  {get; set;}
        public float? RecoveryBarPosPostVat  {get; set;}
        public float? RecoveryBarPosFromSupplier  {get; set;}
        public float? RecoveryBarPosFromSite  {get; set;}
        public float? RecoveryBarPosFromGroup  {get; set;}
        public float? RecoveryBarPosFromSecGroup  {get; set;}
    
        public float? RecoverySitePreVat  {get; set;}
        public float? RecoverySitePostVat  {get; set;}
        public float? RecoverySiteFromSupplier  {get; set;}
        public float? RecoverySiteFromSite  {get; set;}
        public float? RecoverySiteFromGroup  {get; set;}
        public float? RecoverySiteFromSecGroup  {get; set;}
    }

    public class TermsConfigurationInfo
    {
        public string TermsSet {get; set;}
        public string RentSchedule {get; set;}
        public string ShareSchedule {get; set;}        
        public bool? BarPosOverrideRent {get; set;}
        public bool? BarPosOverrideRentSchedule {get; set;}
        public bool? BarPosOverrideShares {get; set;}
        public bool? BarPosOverrideShareSchedule {get; set;}
        public bool? BarPosOverrideLicence {get; set;}
    }
    
    public class ShareScheduleInfo
    {
        public float? SupplierShare { get; set; }
        public float? SiteShare { get; set; }
        public float? CompanyShare { get; set; }
        public float? SecCompanyShare { get; set; }
        public float? SupplierRent { get; set; }
        public bool? SupplierShareGuaranteed { get; set; }
        public bool? SiteShareGuaranteed { get; set; }
        public bool? CompanyShareGuaranteed { get; set; }
        public bool? SecCompanyShareGuaranteed { get; set; }
        public bool? SupplierRentGuaranteed { get; set; }
    }

    public class TermsResults
    {
        public float? CollectionValue { get; set; }
        public float? CollectionValueOutputVatable { get; set; }
        public float? Remainder { get; set; }
        public float? InputPrizeValue { get; set; }

        public bool?  NoTermsProcessingSet { get; set; }
    
        public int? CollectionDays { get; set; }
        public string CollectionDate { get; set; }
    
        public bool?  UseSplitRent { get; set; }
        public float? SupplierRent { get; set; }
        public float? SupplierShare { get; set; }
        public float? SupplierWeeklyRent { get; set; }
        public float? SupplierPerc { get; set; }
        public float? SupplierVat { get; set; }
        public float? SupplierRentShouldGet { get; set; }
        public float? SupplierShareShouldGet { get; set; }
        public float? SupplierVatShouldGet { get; set; }
        public float? SupplierGuarantorForVATShortfall { get; set; }
        public float? SupplierSupplementalCharge { get; set; }
    
        public float? SiteRent { get; set; }
        public float? SitePerc { get; set; }
        public float? SiteShare { get; set; }
        public float? SiteVat { get; set; }
        public float? SiteRentShouldGet { get; set; }
        public float? SiteShareShouldGet { get; set; }
        public float? SiteVatShouldGet { get; set; }
        public float? SiteGuarantorForVATShortfall { get; set; }
    
        public float? SiteGroupRent { get; set; }
        public float? SiteGroupPerc { get; set; }
        public float? SiteGroupShare { get; set; }
        public float? SiteGroupVat { get; set; }
        public float? SiteGroupRentShouldGet { get; set; }
        public float? SiteGroupShareShouldGet { get; set; }
        public float? SiteGroupVatShouldGet { get; set; }
        public float? SiteGroupGuarantorForVATShortfall { get; set; }
    
        public float? SecSiteGroupRent { get; set; }
        public float? SecSiteGroupPerc { get; set; }
        public float? SecSiteGroupShare { get; set; }
        public float? SecSiteGroupVat { get; set; }
        public float? SecSiteGroupRentShouldGet { get; set; }
        public float? SecSiteGroupShareShouldGet { get; set; }
        public float? SecSiteGroupVatShouldGet { get; set; }
        public float? SecSiteGroupGuarantorForVATShortfall { get; set; }
    
        public float? OutputVAT { get; set; }
    
        public float? GPT { get; set; }

        public float? LicenceChargeWeekly { get; set; }
        public float? LicenceCharge { get; set; }
        public float? LicenceVat { get; set; }
        public float? PrizeCharge { get; set; }
        public float? PrizeVat { get; set; }
        public float? ConsultancyCharge { get; set; }
        public float? ConsultancyVat { get; set; }
        public float? RoyaltyCharge { get; set; }
        public float? RoyaltyVat { get; set; }
        public float? Other1Charge { get; set; }
        public float? Other1Vat { get; set; }
        public float? Other2Charge { get; set; }
        public float? Other2Vat { get; set; }
        public float? LicenceChargeShouldGet { get; set; }
        public float? LicenceVatShouldGet { get; set; }
        public float? PrizeChargeShouldGet { get; set; }
        public float? PrizeVatShouldGet { get; set; }
        public float? ConsultancyChargeShouldGet { get; set; }
        public float? ConsultancyVatShouldGet { get; set; }
        public float? RoyaltyChargeShouldGet { get; set; }
        public float? RoyaltyVatShouldGet { get; set; }
        public float? Other1ChargeShouldGet { get; set; }
        public float? Other1VatShouldGet { get; set; }
        public float? Other2ChargeShouldGet { get; set; }
        public float? Other2VatShouldGet { get; set; }

        //Banking
        public float? SiteGroupShareLOS { get; set; }
        public float? SecSiteGroupShareLOS { get; set; }
        public float? SupplierShareLOS { get; set; }
        public float? SiteShareLOS { get; set; }
        public float? LicenceChargeLOS { get; set; }
        public float? PrizeChargeLOS { get; set; }
        public float? ConsultancyChargeLOS { get; set; }
        public float? RoyaltyChargeLOS { get; set; }
        public float? Other1ChargeLOS { get; set; }
        public float? Other2ChargeLOS { get; set; }        
        public float? SupplierVATLOS { get; set; }
        public float? SiteGroupVATLos { get; set; }
        public float? SecSiteGroupVATLos { get; set; }
        public float? SiteVATLos { get; set; }
        public float? VATLOS { get; set; }
        public float? GPTLOS { get; set; }

        public float? SiteGroupShareBankedToCompany { get; set; }
        public float? SecSiteGroupShareBankedToCompany { get; set; }
        public float? SupplierShareBankedToCompany { get; set; }
        public float? SiteShareBankedToCompany { get; set; }
        public float? LicenceChargeBankedToCompany { get; set; }
        public float? PrizeChargeBankedToCompany { get; set; }
        public float? ConsultancyChargeBankedToCompany { get; set; }
        public float? RoyaltyChargeBankedToCompany { get; set; }
        public float? Other1ChargeBankedToCompany { get; set; }
        public float? Other2ChargeBankedToCompany { get; set; }    
        public float? SupplierVATBankedToCompany { get; set; }
        public float? SiteGroupVATBankedToCompany { get; set; }
        public float? SecSiteGroupVATBankedToCompany { get; set; }
        public float? SiteVATBankedToCompany { get; set; }
        public float? VATBankedToCompany { get; set; }
        public float? GPTBankedToCompany { get; set; }

        public float? SiteGroupShareBankedToSupplier { get; set; }
        public float? SecSiteGroupShareBankedToSupplier { get; set; }
        public float? SupplierShareBankedToSupplier { get; set; }
        public float? SiteShareBankedToSupplier { get; set; }
        public float? LicenceChargeBankedToSupplier { get; set; }
        public float? PrizeChargeBankedToSupplier { get; set; }
        public float? ConsultancyChargeBankedToSupplier { get; set; }
        public float? RoyaltyChargeBankedToSupplier { get; set; }
        public float? Other1ChargeBankedToSupplier { get; set; }
        public float? Other2ChargeBankedToSupplier { get; set; }    
        public float? SupplierVATBankedToSupplier { get; set; }
        public float? SiteGroupVATBankedToSupplier { get; set; }
        public float? SecSiteGroupVATBankedToSupplier { get; set; }
        public float? SiteVATBankedToSupplier { get; set; }
        public float? VATBankedToSupplier { get; set; }
        public float? GPTBankedToSupplier { get; set; }

        public float? BankedToCompany { get; set; }   //(total inc vat)
        public float? BankedToSupplier { get; set; }  //(total inc vat)
        public float? LOS { get; set; }               //(total inc vat)

        //Deferred
        public float? SiteGroupShareLOS_toGroup { get; set; }
        public float? SiteGroupShareBankedToSupplier_toGroup { get; set; }
    
        public float? SecSiteGroupShareLOS_toGroup { get; set; }
        public float? SecSiteGroupShareLOS_toSecGroup { get; set; }
        public float? SecSiteGroupShareBankedToSupplier_toGroup { get; set; }
        public float? SecSiteGroupShareBankedToSupplier_toSecGroup { get; set; }
        public float? SecSiteGroupShareBankedToGroup_toSecGroup { get; set; }
    
        public float? SupplierShareLOS_toSupplier { get; set; }
        public float? SupplierShareBankedToGroup_toSupplier { get; set; }
    
        public float? SiteShareBankedToSupplier_toSite { get; set; }
        public float? SiteShareBankedToSupplier_toGroup { get; set; }
        public float? SiteShareBankedToGroup_toSite { get; set; }
    
        public float? GPTLOS_ToSupplier { get; set; }
        public float? GPTLOS_ToGroup { get; set; }
    
        public float? GPTBankedToSupplier_ToGroup { get; set; }
        public float? GPTBankedToSupplier_ToSecGroup { get; set; }
        public float? GPTBankedToSupplier_ToCustoms { get; set; }
        public float? GPTBankedToGroup_ToSupplier { get; set; }
        public float? GPTBankedToGroup_ToCustomer { get; set; }
    
        public float? VATOutputLos_toSupplier { get; set; }
        public float? VATOutputLos_toGroup { get; set; }
        public float? VATOutputBankedToSupplier_toGroup { get; set; }
        public float? VATOutputBankedToSupplier_toSecGroup { get; set; }
        public float? VATOutputBankedToSupplier_toCustoms { get; set; }
        public float? VATOutputBankedToGroup_toSupplier { get; set; }
        public float? VATOutputBankedToGroup_toCustoms { get; set; }
    
        public float? VATSupplierLos_toSupplier { get; set; }
        public float? VATSupplierLos_toGroup { get; set; }
        public float? VATSupplierBankedToSupplier_toGroup { get; set; }
        public float? VATSupplierBankedToSupplier_toSecGroup { get; set; }
        public float? VATSupplierBankedToSupplier_toCustoms { get; set; }
        public float? VATSupplierBankedToGroup_toSupplier { get; set; }
        public float? VATSupplierBankedToGroup_toCustoms { get; set; }
    
        public float? VATSiteLos_toSupplier { get; set; }
        public float? VATSiteLos_toGroup { get; set; }
        public float? VATSiteLos_toCustoms { get; set; }
        public float? VATSiteBankedToSupplier_toGroup { get; set; }        
        public float? VATSiteBankedToSupplier_toCustoms { get; set; }
        public float? VATSiteBankedToSupplier_toSite { get; set; }        
        public float? VATSiteBankedToGroup_toSupplier { get; set; }
        public float? VATSiteBankedToGroup_toCustoms { get; set; }
    
        public float? VATGroupLos_toSupplier { get; set; }
        public float? VATGroupLos_toGroup { get; set; }
        public float? VATGroupBankedToSupplier_toGroup { get; set; }
        public float? VATGroupBankedToSupplier_toCustoms { get; set; }
        public float? VATGroupBankedToGroup_toSupplier { get; set; }
        public float? VATGroupBankedToGroup_toCustoms { get; set; }
    
        public float? VATSecGroupLos_toSupplier { get; set; }
        public float? VATSecGroupLos_toGroup { get; set; }
        public float? VATSecGroupLos_toSecGroup { get; set; }
        public float? VATSecGroupBankedToSupplier_toGroup { get; set; }
        public float? VATSecGroupBankedToSupplier_toSecGroup { get; set; }
        public float? VATSecGroupBankedToSupplier_toCustoms { get; set; }
        public float? VATSecGroupBankedToGroup_toSupplier { get; set; }
        public float? VATSecGroupBankedToGroup_toSecGroup { get; set; }
        public float? VATSecGroupBankedToGroup_toCustoms { get; set; }  
    
        public float? LicenceChargeLOS_toSupplier { get; set; }
        public float? LicenceChargeLOS_toGroup { get; set; }
        public float? LicenceChargeBankedToSupplier_toGroup { get; set; }
        public float? LicenceChargeBankedToGroup_toSupplier { get; set; }
    
        public float? PrizeChargeLOS_toSupplier { get; set; }
        public float? PrizeChargeLOS_toGroup { get; set; }
        public float? PrizeChargeBankedToSupplier_toGroup { get; set; }
        public float? PrizeChargeBankedToGroup_toSupplier { get; set; }
    
        public float? ConsultancyChargeLOS_toSupplier { get; set; }
        public float? ConsultancyChargeLOS_toGroup { get; set; }
        public float? ConsultancyChargeBankedToSupplier_toGroup { get; set; }
        public float? ConsultancyChargeBankedToGroup_toSupplier { get; set; }
    
        public float? RoyaltyChargeLOS_toSupplier { get; set; }
        public float? RoyaltyChargeLOS_toGroup { get; set; }
        public float? RoyaltyChargeBankedToSupplier_toGroup { get; set; }
        public float? RoyaltyChargeBankedToGroup_toSupplier { get; set; }
    
        public float? Other1ChargeLOS_toSupplier { get; set; }
        public float? Other1ChargeLOS_toGroup { get; set; }
        public float? Other1ChargeBankedToSupplier_toGroup { get; set; }
        public float? Other1ChargeBankedToGroup_toSupplier { get; set; }
    
        public float? Other2ChargeLOS_toSupplier { get; set; }
        public float? Other2ChargeLOS_toGroup { get; set; }
        public float? Other2ChargeBankedToSupplier_toGroup { get; set; }
        public float? Other2ChargeBankedToGroup_toSupplier { get; set; }
    }
}
