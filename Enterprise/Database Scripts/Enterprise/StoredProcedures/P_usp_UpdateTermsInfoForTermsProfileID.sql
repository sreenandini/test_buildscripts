USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[usp_UpdateTermsInfoForTermsProfileID]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[usp_UpdateTermsInfoForTermsProfileID]
GO

CREATE PROCEDURE [dbo].[usp_UpdateTermsInfoForTermsProfileID](
    @Terms_Profile_ID                                       INT,
    @Terms_Profile_Name                                     VARCHAR(50),
    @Terms_Profile_Partners_Supplier_Index                  INT,
    @Terms_Profile_Partners_Supplier_Use                    BIT,
    @Terms_Profile_Partners_Supplier_Cash_Destination       INT,
    @Terms_Profile_Partners_Supplier_Deferred_Remittance    INT,
    @Terms_Profile_Partners_Supplier_Type                   INT,
    @Terms_Profile_Partners_Supplier_Value                  REAL,
    @Terms_Profile_Partners_Supplier_Value_Guaranteed       BIT,
    @Terms_Profile_Partners_Supplier_Share                  REAL,
    @Terms_Profile_Partners_Supplier_Share_Guaranteed       BIT,
    @Terms_Profile_Partners_Supplier_Share_Schedule         INT,
    @Terms_Profile_Partners_Supplier_Rent_Schedule          INT,
    @Terms_Profile_Partners_Supplier_Guarantor              INT,
    @Terms_Profile_Partners_Supplier_Guarantor_Percentage   INT,
    @Terms_Profile_Partners_Site_Index                      INT,
    @Terms_Profile_Partners_Site_Use                        BIT,
    @Terms_Profile_Partners_Site_Cash_Destination           INT,
    @Terms_Profile_Partners_Site_Deferred_Remittance        INT,
    @Terms_Profile_Partners_Site_Type                       INT,
    @Terms_Profile_Partners_Site_Value                      REAL,
    @Terms_Profile_Partners_Site_Value_Guaranteed           BIT,
    @Terms_Profile_Partners_Site_Share                      REAL,
    @Terms_Profile_Partners_Site_Share_Guaranteed           BIT,
    @Terms_Profile_Partners_Site_Guarantor                  INT,
    @Terms_Profile_Partners_Site_Guarantor_Percentage       INT,
    @Terms_Profile_Partners_Group_Index                     INT,
    @Terms_Profile_Partners_Group_Use                       BIT,
    @Terms_Profile_Partners_Group_Cash_Destination          INT,
    @Terms_Profile_Partners_Group_Deferred_Remittance       INT,
    @Terms_Profile_Partners_Group_Type                      INT,
    @Terms_Profile_Partners_Group_Value                     REAL,
    @Terms_Profile_Partners_Group_Value_Guaranteed          BIT,
    @Terms_Profile_Partners_Group_Share                     REAL,
    @Terms_Profile_Partners_Group_Share_Guaranteed          BIT,
    @Terms_Profile_Partners_Group_Guarantor                 INT,
    @Terms_Profile_Partners_Group_Guarantor_Percentage      INT,
    @Terms_Profile_Partners_Sec_Group_Index                 INT,
    @Terms_Profile_Partners_Sec_Group_Use                   BIT,
    @Terms_Profile_Partners_Sec_Group_Cash_Destination      INT,
    @Terms_Profile_Partners_Sec_Group_Deferred_Remittance   INT,
    @Terms_Profile_Partners_Sec_Group_Type                  INT,
    @Terms_Profile_Partners_Sec_Group_Value                 REAL,
    @Terms_Profile_Partners_Sec_Group_Value_Guaranteed      BIT,
    @Terms_Profile_Partners_Sec_Group_Share                 REAL,
    @Terms_Profile_Partners_Sec_Group_Share_Guaranteed      BIT,
    @Terms_Profile_Partners_Sec_Group_Guarantor             INT,
    @Terms_Profile_Partners_Sec_Group_Guarantor_Percentage  INT,
    @Terms_Profile_VAT_Output_Index                         INT,
    @Terms_Profile_VAT_Output_Use                           BIT,
    @Terms_Profile_VAT_Output_Cash_Destination              INT,
    @Terms_Profile_VAT_Output_Deferred_Remittance           INT,
    @Terms_Profile_VAT_Supplier_Index                       INT,
    @Terms_Profile_VAT_Supplier_Use                         BIT,
    @Terms_Profile_VAT_Supplier_Cash_Destination            INT,
    @Terms_Profile_VAT_Supplier_Deferred_Remittance         INT,
    @Terms_Profile_VAT_Supplier_Paid_By                     INT,
    @Terms_Profile_VAT_Supplier_Guarantor                   INT,
    @Terms_Profile_VAT_Site_Index                           INT,
    @Terms_Profile_VAT_Site_Use                             BIT,
    @Terms_Profile_VAT_Site_Cash_Destination                INT,
    @Terms_Profile_VAT_Site_Deferred_Remittance             INT,
    @Terms_Profile_VAT_Site_Paid_By                         INT,
    @Terms_Profile_VAT_Site_Guarantor                       INT,
    @Terms_Profile_VAT_Group_Index                          INT,
    @Terms_Profile_VAT_Group_Use                            BIT,
    @Terms_Profile_VAT_Group_Cash_Destination               INT,
    @Terms_Profile_VAT_Group_Deferred_Remittance            INT,
    @Terms_Profile_VAT_Group_Paid_By                        INT,
    @Terms_Profile_VAT_Group_Guarantor                      INT,
    @Terms_Profile_VAT_Sec_Group_Index                      INT,
    @Terms_Profile_VAT_Sec_Group_Use                        BIT,
    @Terms_Profile_VAT_Sec_Group_Cash_Destination           INT,
    @Terms_Profile_VAT_Sec_Group_Deferred_Remittance        INT,
    @Terms_Profile_VAT_Sec_Group_Paid_By                    INT,
    @Terms_Profile_VAT_Sec_Group_Guarantor                  INT,
    @Terms_Profile_GPT_Index                                INT,
    @Terms_Profile_GPT_Use                                  INT,
    @Terms_Profile_GPT_Cash_Destination                     INT,
    @Terms_Profile_GPT_Deferred_Remittance                  INT,
    @Terms_Profile_Other_Licence_Index                      INT,
    @Terms_Profile_Other_Licence_Use                        BIT,
    @Terms_Profile_Other_Licence_Vat                        BIT,
    @Terms_Profile_Other_Licence_Cash_Destination           INT,
    @Terms_Profile_Other_Licence_Deferred_Remittance        INT,
    @Terms_Profile_Other_Licence_Charge                     REAL,
    @Terms_Profile_Other_Licence_Paid_By                    INT,
    @Terms_Profile_Other_Licence_Guarantor                  INT,
    @Terms_Profile_Other_Licence_Frequency                  INT,
    @Terms_Profile_Other_Prize_Index                        INT,
    @Terms_Profile_Other_Prize_Use                          BIT,
    @Terms_Profile_Other_Prize_Vat                          BIT,
    @Terms_Profile_Other_Prize_Cash_Destination             INT,
    @Terms_Profile_Other_Prize_Deferred_Remittance          INT,
    @Terms_Profile_Other_Prize_Charge                       REAL,
    @Terms_Profile_Other_Prize_Paid_By                      INT,
    @Terms_Profile_Other_Prize_Guarantor                    INT,
    @Terms_Profile_Other_Prize_Frequency                    INT,
    @Terms_Profile_Other_Consultancy_Index                  INT,
    @Terms_Profile_Other_Consultancy_Use                    BIT,
    @Terms_Profile_Other_Consultancy_Vat                    BIT,
    @Terms_Profile_Other_Consultancy_Cash_Destination       INT,
    @Terms_Profile_Other_Consultancy_Deferred_Remittance    INT,
    @Terms_Profile_Other_Consultancy_Charge                 REAL,
    @Terms_Profile_Other_Consultancy_Paid_By                INT,
    @Terms_Profile_Other_Consultancy_Guarantor              INT,
    @Terms_Profile_Other_Consultancy_Frequency              INT,
    @Terms_Profile_Other_Royalty_Index                      INT,
    @Terms_Profile_Other_Royalty_Use                        BIT,
    @Terms_Profile_Other_Royalty_Vat                        BIT,
    @Terms_Profile_Other_Royalty_Cash_Destination           INT,
    @Terms_Profile_Other_Royalty_Deferred_Remittance        INT,
    @Terms_Profile_Other_Royalty_Charge                     REAL,
    @Terms_Profile_Other_Royalty_Paid_By                    INT,
    @Terms_Profile_Other_Royalty_Guarantor                  INT,
    @Terms_Profile_Other_Royalty_Frequency                  INT,
    @Terms_Profile_Other_Other1_Index                       INT,
    @Terms_Profile_Other_Other1_Name                        VARCHAR(50),
    @Terms_Profile_Other_Other1_Use                         BIT,
    @Terms_Profile_Other_Other1_Vat                         BIT,
    @Terms_Profile_Other_Other1_Cash_Destination            INT,
    @Terms_Profile_Other_Other1_Deferred_Remittance         INT,
    @Terms_Profile_Other_Other1_Charge                      REAL,
    @Terms_Profile_Other_Other1_Paid_By                     INT,
    @Terms_Profile_Other_Other1_Guarantor                   INT,
    @Terms_Profile_Other_Other1_Frequency                   INT,
    @Terms_Profile_Other_Other2_Index                       INT,
    @Terms_Profile_Other_Other2_Name                        VARCHAR(50),
    @Terms_Profile_Other_Other2_Use                         BIT,
    @Terms_Profile_Other_Other2_Vat                         BIT,
    @Terms_Profile_Other_Other2_Cash_Destination            INT,
    @Terms_Profile_Other_Other2_Deferred_Remittance         INT,
    @Terms_Profile_Other_Other2_Charge                      REAL,
    @Terms_Profile_Other_Other2_Paid_By                     INT,
    @Terms_Profile_Other_Other2_Guarantor                   INT,
    @Terms_Profile_Other_Other2_Frequency                   INT
)
AS
BEGIN
	INSERT INTO Terms_Profile
	  (
	    Terms_Profile_Name,
	    Terms_Profile_Partners_Supplier_Index,
	    Terms_Profile_Partners_Supplier_Use,
	    Terms_Profile_Partners_Supplier_Cash_Destination,
	    Terms_Profile_Partners_Supplier_Deferred_Remittance,
	    Terms_Profile_Partners_Supplier_Type,
	    Terms_Profile_Partners_Supplier_Value,
	    Terms_Profile_Partners_Supplier_Value_Guaranteed,
	    Terms_Profile_Partners_Supplier_Share,
	    Terms_Profile_Partners_Supplier_Share_Guaranteed,
	    Terms_Profile_Partners_Supplier_Share_Schedule,
	    Terms_Profile_Partners_Supplier_Rent_Schedule,
	    Terms_Profile_Partners_Supplier_Guarantor,
	    Terms_Profile_Partners_Supplier_Guarantor_Percentage,
	    Terms_Profile_Partners_Site_Index,
	    Terms_Profile_Partners_Site_Use,
	    Terms_Profile_Partners_Site_Cash_Destination,
	    Terms_Profile_Partners_Site_Deferred_Remittance,
	    Terms_Profile_Partners_Site_Type,
	    Terms_Profile_Partners_Site_Value,
	    Terms_Profile_Partners_Site_Value_Guaranteed,
	    Terms_Profile_Partners_Site_Share,
	    Terms_Profile_Partners_Site_Share_Guaranteed,
	    Terms_Profile_Partners_Site_Guarantor,
	    Terms_Profile_Partners_Site_Guarantor_Percentage,
	    Terms_Profile_Partners_Group_Index,
	    Terms_Profile_Partners_Group_Use,
	    Terms_Profile_Partners_Group_Cash_Destination,
	    Terms_Profile_Partners_Group_Deferred_Remittance,
	    Terms_Profile_Partners_Group_Type,
	    Terms_Profile_Partners_Group_Value,
	    Terms_Profile_Partners_Group_Value_Guaranteed,
	    Terms_Profile_Partners_Group_Share,
	    Terms_Profile_Partners_Group_Share_Guaranteed,
	    Terms_Profile_Partners_Group_Guarantor,
	    Terms_Profile_Partners_Group_Guarantor_Percentage,
	    Terms_Profile_Partners_Sec_Group_Index,
	    Terms_Profile_Partners_Sec_Group_Use,
	    Terms_Profile_Partners_Sec_Group_Cash_Destination,
	    Terms_Profile_Partners_Sec_Group_Deferred_Remittance,
	    Terms_Profile_Partners_Sec_Group_Type,
	    Terms_Profile_Partners_Sec_Group_Value,
	    Terms_Profile_Partners_Sec_Group_Value_Guaranteed,
	    Terms_Profile_Partners_Sec_Group_Share,
	    Terms_Profile_Partners_Sec_Group_Share_Guaranteed,
	    Terms_Profile_Partners_Sec_Group_Guarantor,
	    Terms_Profile_Partners_Sec_Group_Guarantor_Percentage,
	    Terms_Profile_VAT_Output_Index,
	    Terms_Profile_VAT_Output_Use,
	    Terms_Profile_VAT_Output_Cash_Destination,
	    Terms_Profile_VAT_Output_Deferred_Remittance,
	    Terms_Profile_VAT_Supplier_Index,
	    Terms_Profile_VAT_Supplier_Use,
	    Terms_Profile_VAT_Supplier_Cash_Destination,
	    Terms_Profile_VAT_Supplier_Deferred_Remittance,
	    Terms_Profile_VAT_Supplier_Paid_By,
	    Terms_Profile_VAT_Supplier_Guarantor,
	    Terms_Profile_VAT_Site_Index,
	    Terms_Profile_VAT_Site_Use,
	    Terms_Profile_VAT_Site_Cash_Destination,
	    Terms_Profile_VAT_Site_Deferred_Remittance,
	    Terms_Profile_VAT_Site_Paid_By,
	    Terms_Profile_VAT_Site_Guarantor,
	    Terms_Profile_VAT_Group_Index,
	    Terms_Profile_VAT_Group_Use,
	    Terms_Profile_VAT_Group_Cash_Destination,
	    Terms_Profile_VAT_Group_Deferred_Remittance,
	    Terms_Profile_VAT_Group_Paid_By,
	    Terms_Profile_VAT_Group_Guarantor,
	    Terms_Profile_VAT_Sec_Group_Index,
	    Terms_Profile_VAT_Sec_Group_Use,
	    Terms_Profile_VAT_Sec_Group_Cash_Destination,
	    Terms_Profile_VAT_Sec_Group_Deferred_Remittance,
	    Terms_Profile_VAT_Sec_Group_Paid_By,
	    Terms_Profile_VAT_Sec_Group_Guarantor,
	    Terms_Profile_GPT_Index,
	    Terms_Profile_GPT_Use,
	    Terms_Profile_GPT_Cash_Destination,
	    Terms_Profile_GPT_Deferred_Remittance,
	    Terms_Profile_Other_Licence_Index,
	    Terms_Profile_Other_Licence_Use,
	    Terms_Profile_Other_Licence_Vat,
	    Terms_Profile_Other_Licence_Cash_Destination,
	    Terms_Profile_Other_Licence_Deferred_Remittance,
	    Terms_Profile_Other_Licence_Charge,
	    Terms_Profile_Other_Licence_Paid_By,
	    Terms_Profile_Other_Licence_Guarantor,
	    Terms_Profile_Other_Licence_Frequency,
	    Terms_Profile_Other_Prize_Index,
	    Terms_Profile_Other_Prize_Use,
	    Terms_Profile_Other_Prize_Vat,
	    Terms_Profile_Other_Prize_Cash_Destination,
	    Terms_Profile_Other_Prize_Deferred_Remittance,
	    Terms_Profile_Other_Prize_Charge,
	    Terms_Profile_Other_Prize_Paid_By,
	    Terms_Profile_Other_Prize_Guarantor,
	    Terms_Profile_Other_Prize_Frequency,
	    Terms_Profile_Other_Consultancy_Index,
	    Terms_Profile_Other_Consultancy_Use,
	    Terms_Profile_Other_Consultancy_Vat,
	    Terms_Profile_Other_Consultancy_Cash_Destination,
	    Terms_Profile_Other_Consultancy_Deferred_Remittance,
	    Terms_Profile_Other_Consultancy_Charge,
	    Terms_Profile_Other_Consultancy_Paid_By,
	    Terms_Profile_Other_Consultancy_Guarantor,
	    Terms_Profile_Other_Consultancy_Frequency,
	    Terms_Profile_Other_Royalty_Index,
	    Terms_Profile_Other_Royalty_Use,
	    Terms_Profile_Other_Royalty_Vat,
	    Terms_Profile_Other_Royalty_Cash_Destination,
	    Terms_Profile_Other_Royalty_Deferred_Remittance,
	    Terms_Profile_Other_Royalty_Charge,
	    Terms_Profile_Other_Royalty_Paid_By,
	    Terms_Profile_Other_Royalty_Guarantor,
	    Terms_Profile_Other_Royalty_Frequency,
	    Terms_Profile_Other_Other1_Index,
	    Terms_Profile_Other_Other1_Name,
	    Terms_Profile_Other_Other1_Use,
	    Terms_Profile_Other_Other1_Vat,
	    Terms_Profile_Other_Other1_Cash_Destination,
	    Terms_Profile_Other_Other1_Deferred_Remittance,
	    Terms_Profile_Other_Other1_Charge,
	    Terms_Profile_Other_Other1_Paid_By,
	    Terms_Profile_Other_Other1_Guarantor,
	    Terms_Profile_Other_Other1_Frequency,
	    Terms_Profile_Other_Other2_Index,
	    Terms_Profile_Other_Other2_Name,
	    Terms_Profile_Other_Other2_Use,
	    Terms_Profile_Other_Other2_Vat,
	    Terms_Profile_Other_Other2_Cash_Destination,
	    Terms_Profile_Other_Other2_Deferred_Remittance,
	    Terms_Profile_Other_Other2_Charge,
	    Terms_Profile_Other_Other2_Paid_By,
	    Terms_Profile_Other_Other2_Guarantor,
	    Terms_Profile_Other_Other2_Frequency
	  )
	SELECT @Terms_Profile_Name,
	       @Terms_Profile_Partners_Supplier_Index,
	       @Terms_Profile_Partners_Supplier_Use,
	       @Terms_Profile_Partners_Supplier_Cash_Destination,
	       @Terms_Profile_Partners_Supplier_Deferred_Remittance,
	       @Terms_Profile_Partners_Supplier_Type,
	       @Terms_Profile_Partners_Supplier_Value,
	       @Terms_Profile_Partners_Supplier_Value_Guaranteed,
	       @Terms_Profile_Partners_Supplier_Share,
	       @Terms_Profile_Partners_Supplier_Share_Guaranteed,
	       @Terms_Profile_Partners_Supplier_Share_Schedule,
	       @Terms_Profile_Partners_Supplier_Rent_Schedule,
	       @Terms_Profile_Partners_Supplier_Guarantor,
	       @Terms_Profile_Partners_Supplier_Guarantor_Percentage,
	       @Terms_Profile_Partners_Site_Index,
	       @Terms_Profile_Partners_Site_Use,
	       @Terms_Profile_Partners_Site_Cash_Destination,
	       @Terms_Profile_Partners_Site_Deferred_Remittance,
	       @Terms_Profile_Partners_Site_Type,
	       @Terms_Profile_Partners_Site_Value,
	       @Terms_Profile_Partners_Site_Value_Guaranteed,
	       @Terms_Profile_Partners_Site_Share,
	       @Terms_Profile_Partners_Site_Share_Guaranteed,
	       @Terms_Profile_Partners_Site_Guarantor,
	       @Terms_Profile_Partners_Site_Guarantor_Percentage,
	       @Terms_Profile_Partners_Group_Index,
	       @Terms_Profile_Partners_Group_Use,
	       @Terms_Profile_Partners_Group_Cash_Destination,
	       @Terms_Profile_Partners_Group_Deferred_Remittance,
	       @Terms_Profile_Partners_Group_Type,
	       @Terms_Profile_Partners_Group_Value,
	       @Terms_Profile_Partners_Group_Value_Guaranteed,
	       @Terms_Profile_Partners_Group_Share,
	       @Terms_Profile_Partners_Group_Share_Guaranteed,
	       @Terms_Profile_Partners_Group_Guarantor,
	       @Terms_Profile_Partners_Group_Guarantor_Percentage,
	       @Terms_Profile_Partners_Sec_Group_Index,
	       @Terms_Profile_Partners_Sec_Group_Use,
	       @Terms_Profile_Partners_Sec_Group_Cash_Destination,
	       @Terms_Profile_Partners_Sec_Group_Deferred_Remittance,
	       @Terms_Profile_Partners_Sec_Group_Type,
	       @Terms_Profile_Partners_Sec_Group_Value,
	       @Terms_Profile_Partners_Sec_Group_Value_Guaranteed,
	       @Terms_Profile_Partners_Sec_Group_Share,
	       @Terms_Profile_Partners_Sec_Group_Share_Guaranteed,
	       @Terms_Profile_Partners_Sec_Group_Guarantor,
	       @Terms_Profile_Partners_Sec_Group_Guarantor_Percentage,
	       @Terms_Profile_VAT_Output_Index,
	       @Terms_Profile_VAT_Output_Use,
	       @Terms_Profile_VAT_Output_Cash_Destination,
	       @Terms_Profile_VAT_Output_Deferred_Remittance,
	       @Terms_Profile_VAT_Supplier_Index,
	       @Terms_Profile_VAT_Supplier_Use,
	       @Terms_Profile_VAT_Supplier_Cash_Destination,
	       @Terms_Profile_VAT_Supplier_Deferred_Remittance,
	       @Terms_Profile_VAT_Supplier_Paid_By,
	       @Terms_Profile_VAT_Supplier_Guarantor,
	       @Terms_Profile_VAT_Site_Index,
	       @Terms_Profile_VAT_Site_Use,
	       @Terms_Profile_VAT_Site_Cash_Destination,
	       @Terms_Profile_VAT_Site_Deferred_Remittance,
	       @Terms_Profile_VAT_Site_Paid_By,
	       @Terms_Profile_VAT_Site_Guarantor,
	       @Terms_Profile_VAT_Group_Index,
	       @Terms_Profile_VAT_Group_Use,
	       @Terms_Profile_VAT_Group_Cash_Destination,
	       @Terms_Profile_VAT_Group_Deferred_Remittance,
	       @Terms_Profile_VAT_Group_Paid_By,
	       @Terms_Profile_VAT_Group_Guarantor,
	       @Terms_Profile_VAT_Sec_Group_Index,
	       @Terms_Profile_VAT_Sec_Group_Use,
	       @Terms_Profile_VAT_Sec_Group_Cash_Destination,
	       @Terms_Profile_VAT_Sec_Group_Deferred_Remittance,
	       @Terms_Profile_VAT_Sec_Group_Paid_By,
	       @Terms_Profile_VAT_Sec_Group_Guarantor,
	       @Terms_Profile_GPT_Index,
	       @Terms_Profile_GPT_Use,
	       @Terms_Profile_GPT_Cash_Destination,
	       @Terms_Profile_GPT_Deferred_Remittance,
	       @Terms_Profile_Other_Licence_Index,
	       @Terms_Profile_Other_Licence_Use,
	       @Terms_Profile_Other_Licence_Vat,
	       @Terms_Profile_Other_Licence_Cash_Destination,
	       @Terms_Profile_Other_Licence_Deferred_Remittance,
	       @Terms_Profile_Other_Licence_Charge,
	       @Terms_Profile_Other_Licence_Paid_By,
	       @Terms_Profile_Other_Licence_Guarantor,
	       @Terms_Profile_Other_Licence_Frequency,
	       @Terms_Profile_Other_Prize_Index,
	       @Terms_Profile_Other_Prize_Use,
	       @Terms_Profile_Other_Prize_Vat,
	       @Terms_Profile_Other_Prize_Cash_Destination,
	       @Terms_Profile_Other_Prize_Deferred_Remittance,
	       @Terms_Profile_Other_Prize_Charge,
	       @Terms_Profile_Other_Prize_Paid_By,
	       @Terms_Profile_Other_Prize_Guarantor,
	       @Terms_Profile_Other_Prize_Frequency,
	       @Terms_Profile_Other_Consultancy_Index,
	       @Terms_Profile_Other_Consultancy_Use,
	       @Terms_Profile_Other_Consultancy_Vat,
	       @Terms_Profile_Other_Consultancy_Cash_Destination,
	       @Terms_Profile_Other_Consultancy_Deferred_Remittance,
	       @Terms_Profile_Other_Consultancy_Charge,
	       @Terms_Profile_Other_Consultancy_Paid_By,
	       @Terms_Profile_Other_Consultancy_Guarantor,
	       @Terms_Profile_Other_Consultancy_Frequency,
	       @Terms_Profile_Other_Royalty_Index,
	       @Terms_Profile_Other_Royalty_Use,
	       @Terms_Profile_Other_Royalty_Vat,
	       @Terms_Profile_Other_Royalty_Cash_Destination,
	       @Terms_Profile_Other_Royalty_Deferred_Remittance,
	       @Terms_Profile_Other_Royalty_Charge,
	       @Terms_Profile_Other_Royalty_Paid_By,
	       @Terms_Profile_Other_Royalty_Guarantor,
	       @Terms_Profile_Other_Royalty_Frequency,
	       @Terms_Profile_Other_Other1_Index,
	       @Terms_Profile_Other_Other1_Name,
	       @Terms_Profile_Other_Other1_Use,
	       @Terms_Profile_Other_Other1_Vat,
	       @Terms_Profile_Other_Other1_Cash_Destination,
	       @Terms_Profile_Other_Other1_Deferred_Remittance,
	       @Terms_Profile_Other_Other1_Charge,
	       @Terms_Profile_Other_Other1_Paid_By,
	       @Terms_Profile_Other_Other1_Guarantor,
	       @Terms_Profile_Other_Other1_Frequency,
	       @Terms_Profile_Other_Other2_Index,
	       @Terms_Profile_Other_Other2_Name,
	       @Terms_Profile_Other_Other2_Use,
	       @Terms_Profile_Other_Other2_Vat,
	       @Terms_Profile_Other_Other2_Cash_Destination,
	       @Terms_Profile_Other_Other2_Deferred_Remittance,
	       @Terms_Profile_Other_Other2_Charge,
	       @Terms_Profile_Other_Other2_Paid_By,
	       @Terms_Profile_Other_Other2_Guarantor,
	       @Terms_Profile_Other_Other2_Frequency
	FROM   Terms_Profile
	WHERE  @Terms_Profile_ID = 0  
	
	UPDATE Terms_Profile
	SET    Terms_Profile_Partners_Supplier_Index = @Terms_Profile_Partners_Supplier_Index,
	       Terms_Profile_Partners_Supplier_Use = @Terms_Profile_Partners_Supplier_Use,
	       Terms_Profile_Partners_Supplier_Cash_Destination = @Terms_Profile_Partners_Supplier_Cash_Destination,
	       Terms_Profile_Partners_Supplier_Deferred_Remittance = @Terms_Profile_Partners_Supplier_Deferred_Remittance,
	       Terms_Profile_Partners_Supplier_Type = @Terms_Profile_Partners_Supplier_Type,
	       Terms_Profile_Partners_Supplier_Value = CASE @Terms_Profile_Partners_Supplier_Value WHEN 0 THEN Terms_Profile_Partners_Supplier_Value ELSE @Terms_Profile_Partners_Supplier_Value END,
	       Terms_Profile_Partners_Supplier_Value_Guaranteed = @Terms_Profile_Partners_Supplier_Value_Guaranteed,
	       Terms_Profile_Partners_Supplier_Share = @Terms_Profile_Partners_Supplier_Share,
	       Terms_Profile_Partners_Supplier_Share_Guaranteed = @Terms_Profile_Partners_Supplier_Share_Guaranteed,
	       Terms_Profile_Partners_Supplier_Share_Schedule = CASE @Terms_Profile_Partners_Supplier_Share_Schedule WHEN 0 THEN Terms_Profile_Partners_Supplier_Share_Schedule ELSE @Terms_Profile_Partners_Supplier_Share_Schedule END,
	       Terms_Profile_Partners_Supplier_Rent_Schedule = CASE @Terms_Profile_Partners_Supplier_Rent_Schedule WHEN 0 THEN Terms_Profile_Partners_Supplier_Rent_Schedule ELSE @Terms_Profile_Partners_Supplier_Rent_Schedule END,
	       Terms_Profile_Partners_Supplier_Guarantor = @Terms_Profile_Partners_Supplier_Guarantor,
	       Terms_Profile_Partners_Supplier_Guarantor_Percentage = @Terms_Profile_Partners_Supplier_Guarantor_Percentage,
	       Terms_Profile_Partners_Site_Index = @Terms_Profile_Partners_Site_Index,
	       Terms_Profile_Partners_Site_Use = @Terms_Profile_Partners_Site_Use,
	       Terms_Profile_Partners_Site_Cash_Destination = @Terms_Profile_Partners_Site_Cash_Destination,
	       Terms_Profile_Partners_Site_Deferred_Remittance = @Terms_Profile_Partners_Site_Deferred_Remittance,
	       Terms_Profile_Partners_Site_Type = @Terms_Profile_Partners_Site_Type,
	       Terms_Profile_Partners_Site_Value = @Terms_Profile_Partners_Site_Value,
	       Terms_Profile_Partners_Site_Value_Guaranteed = @Terms_Profile_Partners_Site_Value_Guaranteed,
	       Terms_Profile_Partners_Site_Share = @Terms_Profile_Partners_Site_Share,
	       Terms_Profile_Partners_Site_Share_Guaranteed = @Terms_Profile_Partners_Site_Share_Guaranteed,
	       Terms_Profile_Partners_Site_Guarantor = @Terms_Profile_Partners_Site_Guarantor,
	       Terms_Profile_Partners_Site_Guarantor_Percentage = @Terms_Profile_Partners_Site_Guarantor_Percentage,
	       Terms_Profile_Partners_Group_Index = @Terms_Profile_Partners_Group_Index,
	       Terms_Profile_Partners_Group_Use = @Terms_Profile_Partners_Group_Use,
	       Terms_Profile_Partners_Group_Cash_Destination = @Terms_Profile_Partners_Group_Cash_Destination,
	       Terms_Profile_Partners_Group_Deferred_Remittance = @Terms_Profile_Partners_Group_Deferred_Remittance,
	       Terms_Profile_Partners_Group_Type = @Terms_Profile_Partners_Group_Type,
	       Terms_Profile_Partners_Group_Value = @Terms_Profile_Partners_Group_Value,
	       Terms_Profile_Partners_Group_Value_Guaranteed = @Terms_Profile_Partners_Group_Value_Guaranteed,
	       Terms_Profile_Partners_Group_Share = @Terms_Profile_Partners_Group_Share,
	       Terms_Profile_Partners_Group_Share_Guaranteed = @Terms_Profile_Partners_Group_Share_Guaranteed,
	       Terms_Profile_Partners_Group_Guarantor = @Terms_Profile_Partners_Group_Guarantor,
	       Terms_Profile_Partners_Group_Guarantor_Percentage = @Terms_Profile_Partners_Group_Guarantor_Percentage,
	       Terms_Profile_Partners_Sec_Group_Index = @Terms_Profile_Partners_Sec_Group_Index,
	       Terms_Profile_Partners_Sec_Group_Use = @Terms_Profile_Partners_Sec_Group_Use,
	       Terms_Profile_Partners_Sec_Group_Cash_Destination = @Terms_Profile_Partners_Sec_Group_Cash_Destination,
	       Terms_Profile_Partners_Sec_Group_Deferred_Remittance = @Terms_Profile_Partners_Sec_Group_Deferred_Remittance,
	       Terms_Profile_Partners_Sec_Group_Type = @Terms_Profile_Partners_Sec_Group_Type,
	       Terms_Profile_Partners_Sec_Group_Value = @Terms_Profile_Partners_Sec_Group_Value,
	       Terms_Profile_Partners_Sec_Group_Value_Guaranteed = @Terms_Profile_Partners_Sec_Group_Value_Guaranteed,
	       Terms_Profile_Partners_Sec_Group_Share = @Terms_Profile_Partners_Sec_Group_Share,
	       Terms_Profile_Partners_Sec_Group_Share_Guaranteed = @Terms_Profile_Partners_Sec_Group_Share_Guaranteed,
	       Terms_Profile_Partners_Sec_Group_Guarantor = @Terms_Profile_Partners_Sec_Group_Guarantor,
	       Terms_Profile_Partners_Sec_Group_Guarantor_Percentage = @Terms_Profile_Partners_Sec_Group_Guarantor_Percentage,
	       Terms_Profile_VAT_Output_Index = @Terms_Profile_VAT_Output_Index,
	       Terms_Profile_VAT_Output_Use = @Terms_Profile_VAT_Output_Use,
	       Terms_Profile_VAT_Output_Cash_Destination = @Terms_Profile_VAT_Output_Cash_Destination,
	       Terms_Profile_VAT_Output_Deferred_Remittance = @Terms_Profile_VAT_Output_Deferred_Remittance,
	       Terms_Profile_VAT_Supplier_Index = @Terms_Profile_VAT_Supplier_Index,
	       Terms_Profile_VAT_Supplier_Use = @Terms_Profile_VAT_Supplier_Use,
	       Terms_Profile_VAT_Supplier_Cash_Destination = @Terms_Profile_VAT_Supplier_Cash_Destination,
	       Terms_Profile_VAT_Supplier_Deferred_Remittance = @Terms_Profile_VAT_Supplier_Deferred_Remittance,
	       Terms_Profile_VAT_Supplier_Paid_By = @Terms_Profile_VAT_Supplier_Paid_By,
	       Terms_Profile_VAT_Supplier_Guarantor = @Terms_Profile_VAT_Supplier_Guarantor,
	       Terms_Profile_VAT_Site_Index = @Terms_Profile_VAT_Site_Index,
	       Terms_Profile_VAT_Site_Use = @Terms_Profile_VAT_Site_Use,
	       Terms_Profile_VAT_Site_Cash_Destination = @Terms_Profile_VAT_Site_Cash_Destination,
	       Terms_Profile_VAT_Site_Deferred_Remittance = @Terms_Profile_VAT_Site_Deferred_Remittance,
	       Terms_Profile_VAT_Site_Paid_By = @Terms_Profile_VAT_Site_Paid_By,
	       Terms_Profile_VAT_Site_Guarantor = @Terms_Profile_VAT_Site_Guarantor,
	       Terms_Profile_VAT_Group_Index = @Terms_Profile_VAT_Group_Index,
	       Terms_Profile_VAT_Group_Use = @Terms_Profile_VAT_Group_Use,
	       Terms_Profile_VAT_Group_Cash_Destination = @Terms_Profile_VAT_Group_Cash_Destination,
	       Terms_Profile_VAT_Group_Deferred_Remittance = @Terms_Profile_VAT_Group_Deferred_Remittance,
	       Terms_Profile_VAT_Group_Paid_By = @Terms_Profile_VAT_Group_Paid_By,
	       Terms_Profile_VAT_Group_Guarantor = @Terms_Profile_VAT_Group_Guarantor,
	       Terms_Profile_VAT_Sec_Group_Index = @Terms_Profile_VAT_Sec_Group_Index,
	       Terms_Profile_VAT_Sec_Group_Use = @Terms_Profile_VAT_Sec_Group_Use,
	       Terms_Profile_VAT_Sec_Group_Cash_Destination = @Terms_Profile_VAT_Sec_Group_Cash_Destination,
	       Terms_Profile_VAT_Sec_Group_Deferred_Remittance = @Terms_Profile_VAT_Sec_Group_Deferred_Remittance,
	       Terms_Profile_VAT_Sec_Group_Paid_By = @Terms_Profile_VAT_Sec_Group_Paid_By,
	       Terms_Profile_VAT_Sec_Group_Guarantor = @Terms_Profile_VAT_Sec_Group_Guarantor,
	       Terms_Profile_GPT_Index = @Terms_Profile_GPT_Index,
	       Terms_Profile_GPT_Use = @Terms_Profile_GPT_Use,
	       Terms_Profile_GPT_Cash_Destination = @Terms_Profile_GPT_Cash_Destination,
	       Terms_Profile_GPT_Deferred_Remittance = @Terms_Profile_GPT_Deferred_Remittance,
	       Terms_Profile_Other_Licence_Index = @Terms_Profile_Other_Licence_Index,
	       Terms_Profile_Other_Licence_Use = @Terms_Profile_Other_Licence_Use,
	       Terms_Profile_Other_Licence_Vat = @Terms_Profile_Other_Licence_Vat,
	       Terms_Profile_Other_Licence_Cash_Destination = @Terms_Profile_Other_Licence_Cash_Destination,
	       Terms_Profile_Other_Licence_Deferred_Remittance = @Terms_Profile_Other_Licence_Deferred_Remittance,
	       Terms_Profile_Other_Licence_Charge = @Terms_Profile_Other_Licence_Charge,
	       Terms_Profile_Other_Licence_Paid_By = @Terms_Profile_Other_Licence_Paid_By,
	       Terms_Profile_Other_Licence_Guarantor = @Terms_Profile_Other_Licence_Guarantor,
	       Terms_Profile_Other_Licence_Frequency = @Terms_Profile_Other_Licence_Frequency,
	       Terms_Profile_Other_Prize_Index = @Terms_Profile_Other_Prize_Index,
	       Terms_Profile_Other_Prize_Use = @Terms_Profile_Other_Prize_Use,
	       Terms_Profile_Other_Prize_Vat = @Terms_Profile_Other_Prize_Vat,
	       Terms_Profile_Other_Prize_Cash_Destination = @Terms_Profile_Other_Prize_Cash_Destination,
	       Terms_Profile_Other_Prize_Deferred_Remittance = @Terms_Profile_Other_Prize_Deferred_Remittance,
	       Terms_Profile_Other_Prize_Charge = @Terms_Profile_Other_Prize_Charge,
	       Terms_Profile_Other_Prize_Paid_By = @Terms_Profile_Other_Prize_Paid_By,
	       Terms_Profile_Other_Prize_Guarantor = @Terms_Profile_Other_Prize_Guarantor,
	       Terms_Profile_Other_Prize_Frequency = @Terms_Profile_Other_Prize_Frequency,
	       Terms_Profile_Other_Consultancy_Index = @Terms_Profile_Other_Consultancy_Index,
	       Terms_Profile_Other_Consultancy_Use = @Terms_Profile_Other_Consultancy_Use,
	       Terms_Profile_Other_Consultancy_Vat = @Terms_Profile_Other_Consultancy_Vat,
	       Terms_Profile_Other_Consultancy_Cash_Destination = @Terms_Profile_Other_Consultancy_Cash_Destination,
	       Terms_Profile_Other_Consultancy_Deferred_Remittance = @Terms_Profile_Other_Consultancy_Deferred_Remittance,
	       Terms_Profile_Other_Consultancy_Charge = @Terms_Profile_Other_Consultancy_Charge,
	       Terms_Profile_Other_Consultancy_Paid_By = @Terms_Profile_Other_Consultancy_Paid_By,
	       Terms_Profile_Other_Consultancy_Guarantor = @Terms_Profile_Other_Consultancy_Guarantor,
	       Terms_Profile_Other_Consultancy_Frequency = @Terms_Profile_Other_Consultancy_Frequency,
	       Terms_Profile_Other_Royalty_Index = @Terms_Profile_Other_Royalty_Index,
	       Terms_Profile_Other_Royalty_Use = @Terms_Profile_Other_Royalty_Use,
	       Terms_Profile_Other_Royalty_Vat = @Terms_Profile_Other_Royalty_Vat,
	       Terms_Profile_Other_Royalty_Cash_Destination = @Terms_Profile_Other_Royalty_Cash_Destination,
	       Terms_Profile_Other_Royalty_Deferred_Remittance = @Terms_Profile_Other_Royalty_Deferred_Remittance,
	       Terms_Profile_Other_Royalty_Charge = @Terms_Profile_Other_Royalty_Charge,
	       Terms_Profile_Other_Royalty_Paid_By = @Terms_Profile_Other_Royalty_Paid_By,
	       Terms_Profile_Other_Royalty_Guarantor = @Terms_Profile_Other_Royalty_Guarantor,
	       Terms_Profile_Other_Royalty_Frequency = @Terms_Profile_Other_Royalty_Frequency,
	       Terms_Profile_Other_Other1_Index = @Terms_Profile_Other_Other1_Index,
	       Terms_Profile_Other_Other1_Name = @Terms_Profile_Other_Other1_Name,
	       Terms_Profile_Other_Other1_Use = @Terms_Profile_Other_Other1_Use,
	       Terms_Profile_Other_Other1_Vat = @Terms_Profile_Other_Other1_Vat,
	       Terms_Profile_Other_Other1_Cash_Destination = @Terms_Profile_Other_Other1_Cash_Destination,
	       Terms_Profile_Other_Other1_Deferred_Remittance = @Terms_Profile_Other_Other1_Deferred_Remittance,
	       Terms_Profile_Other_Other1_Charge = @Terms_Profile_Other_Other1_Charge,
	       Terms_Profile_Other_Other1_Paid_By = @Terms_Profile_Other_Other1_Paid_By,
	       Terms_Profile_Other_Other1_Guarantor = @Terms_Profile_Other_Other1_Guarantor,
	       Terms_Profile_Other_Other1_Frequency = @Terms_Profile_Other_Other1_Frequency,
	       Terms_Profile_Other_Other2_Index = @Terms_Profile_Other_Other2_Index,
	       Terms_Profile_Other_Other2_Name = @Terms_Profile_Other_Other2_Name,
	       Terms_Profile_Other_Other2_Use = @Terms_Profile_Other_Other2_Use,
	       Terms_Profile_Other_Other2_Vat = @Terms_Profile_Other_Other2_Vat,
	       Terms_Profile_Other_Other2_Cash_Destination = @Terms_Profile_Other_Other2_Cash_Destination,
	       Terms_Profile_Other_Other2_Deferred_Remittance = @Terms_Profile_Other_Other2_Deferred_Remittance,
	       Terms_Profile_Other_Other2_Charge = @Terms_Profile_Other_Other2_Charge,
	       Terms_Profile_Other_Other2_Paid_By = @Terms_Profile_Other_Other2_Paid_By,
	       Terms_Profile_Other_Other2_Guarantor = @Terms_Profile_Other_Other2_Guarantor,
	       Terms_Profile_Other_Other2_Frequency = @Terms_Profile_Other_Other2_Frequency
	WHERE  Terms_Profile_ID = @Terms_Profile_ID
END 