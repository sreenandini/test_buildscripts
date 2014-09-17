USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[usp_UpdateCollectionTermsWithTermsResults]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[usp_UpdateCollectionTermsWithTermsResults] 
GO

CREATE PROCEDURE [dbo].[usp_UpdateCollectionTermsWithTermsResults] 
(
    @CollectionID                                  INT,
    @HQTerms_Company_Share                         REAL,
    @HQTerms_Supplier_Share                        REAL,
    @HQTerms_Location_Share                        REAL,
    @HQTerms_AMLD                                  REAL,
    @HQTerms_VAT_Output                            REAL,
    @HQTerms_VAT_Company                           REAL,
    @HQTerms_VAT_Supplier                          REAL,
    @HQTerms_VAT_Location                          REAL,
    @HQTerms_VAT_LOS                               REAL,
    @HQTerms_VAT_Banked_To_Company                 REAL,
    @HQTerms_VAT_Banked_To_Supplier                REAL,
    @HQTerms_Supplier_VAT_LOS                      REAL,
    @HQTerms_Company_VAT_LOS                       REAL,
    @HQTerms_Sec_Brewery_VAT_LOS                   REAL,
    @HQTerms_Location_VAT_LOS                      REAL,
    @HQTerms_Supplier_VAT_Banked_To_Company        REAL,
    @HQTerms_Company_VAT_Banked_To_Company         REAL,
    @HQTerms_Sec_Brewery_VAT_Banked_To_Company     REAL,
    @HQTerms_Location_VAT_Banked_To_Company        REAL,
    @HQTerms_Supplier_VAT_Banked_To_Supplier       REAL,
    @HQTerms_Company_VAT_Banked_To_Supplier        REAL,
    @HQTerms_Sec_Brewery_VAT_Banked_To_Supplier    REAL,
    @HQTerms_Location_VAT_Banked_To_Supplier       REAL,
    @HQTerms_Company_Share_LOS                     REAL,
    @HQTerms_Supplier_Share_LOS                    REAL,
    @HQTerms_Location_Share_LOS                    REAL,
    @HQTerms_Banked_To_Company                     REAL,
    @HQTerms_Banked_To_Supplier                    REAL,
    @HQTerms_VAT_Sec_Brewery                       REAL,
    @HQTerms_Sec_Brewery_Share_LOS                 REAL,
    @HQTerms_Company_Share_Banked_To_Company       REAL,
    @HQTerms_Company_Share_Banked_To_Supplier      REAL,
    @HQTerms_Sec_Brewery_Share_Banked_To_Company   REAL,
    @HQTerms_Sec_Brewery_Share_Banked_To_Supplier  REAL,
    @HQTerms_Supplier_Share_Banked_To_Company      REAL,
    @HQTerms_Supplier_Share_Banked_To_Supplier     REAL,
    @HQTerms_Site_Share_Banked_To_Company          REAL,
    @HQTerms_Site_Share_Banked_To_Supplier         REAL,
    @HQTerms_Licence_Banked_To_Company             REAL,
    @HQTerms_Licence_Banked_To_Supplier            REAL,
    @HQTerms_Licence_LOS                           REAL,
    @HQTerms_LOS                                   REAL
)
AS
BEGIN
	IF NOT EXISTS(
	       SELECT 1
	       FROM   Collection_Terms
	       WHERE  Collection_ID = @CollectionID
	   )
	BEGIN
	    INSERT INTO Collection_Terms
	      (
	        Collection_ID,
	        HQTerms_Company_Share,
	        HQTerms_Supplier_Share,
	        HQTerms_Location_Share,
	        HQTerms_AMLD,
	        HQTerms_VAT_Output,
	        HQTerms_VAT_Company,
	        HQTerms_VAT_Sec_Brewery,
	        HQTerms_VAT_Supplier,
	        HQTerms_VAT_Location,
	        HQTerms_Company_Share_LOS,
	        HQTerms_Sec_Brewery_Share_LOS,
	        HQTerms_Supplier_Share_LOS,
	        HQTerms_Location_Share_LOS,
	        HQTerms_Company_Share_Banked_To_Company,
	        HQTerms_Company_Share_Banked_To_Supplier,
	        HQTerms_Sec_Brewery_Share_Banked_To_Company,
	        HQTerms_Sec_Brewery_Share_Banked_To_Supplier,
	        HQTerms_Supplier_Share_Banked_To_Company,
	        HQTerms_Supplier_Share_Banked_To_Supplier,
	        HQTerms_Site_Share_Banked_To_Company,
	        HQTerms_Site_Share_Banked_To_Supplier,
	        HQTerms_Licence_Banked_To_Company,
	        HQTerms_Licence_Banked_To_Supplier,
	        HQTerms_Licence_LOS,
	        HQTerms_VAT_LOS,
	        HQTerms_VAT_Banked_To_Company,
	        HQTerms_VAT_Banked_To_Supplier,
	        HQTerms_Banked_To_Company,
	        HQTerms_Banked_To_Supplier,
	        HQTerms_LOS,
	        HQTerms_Supplier_VAT_LOS,
	        HQTerms_Company_VAT_LOS,
	        HQTerms_Sec_Brewery_VAT_LOS,
	        HQTerms_Location_VAT_LOS,
	        HQTerms_Supplier_VAT_Banked_To_Company,
	        HQTerms_Company_VAT_Banked_To_Company,
	        HQTerms_Sec_Brewery_VAT_Banked_To_Company,
	        HQTerms_Location_VAT_Banked_To_Company,
	        HQTerms_Supplier_VAT_Banked_To_Supplier,
	        HQTerms_Company_VAT_Banked_To_Supplier,
	        HQTerms_Sec_Brewery_VAT_Banked_To_Supplier,
	        HQTerms_Location_VAT_Banked_To_Supplier
	      )
	    VALUES
	      (
	        @CollectionID,
	        @HQTerms_Company_Share,
	        @HQTerms_Supplier_Share,
	        @HQTerms_Location_Share,
	        @HQTerms_AMLD,
	        @HQTerms_VAT_Output,
	        @HQTerms_VAT_Company,
	        @HQTerms_VAT_Sec_Brewery,
	        @HQTerms_VAT_Supplier,
	        @HQTerms_VAT_Location,
	        @HQTerms_Company_Share_LOS,
	        @HQTerms_Sec_Brewery_Share_LOS,
	        @HQTerms_Supplier_Share_LOS,
	        @HQTerms_Location_Share_LOS,
	        @HQTerms_Company_Share_Banked_To_Company,
	        @HQTerms_Company_Share_Banked_To_Supplier,
	        @HQTerms_Sec_Brewery_Share_Banked_To_Company,
	        @HQTerms_Sec_Brewery_Share_Banked_To_Supplier,
	        @HQTerms_Supplier_Share_Banked_To_Company,
	        @HQTerms_Supplier_Share_Banked_To_Supplier,
	        @HQTerms_Site_Share_Banked_To_Company,
	        @HQTerms_Site_Share_Banked_To_Supplier,
	        @HQTerms_Licence_Banked_To_Company,
	        @HQTerms_Licence_Banked_To_Supplier,
	        @HQTerms_Licence_LOS,
	        @HQTerms_VAT_LOS,
	        @HQTerms_VAT_Banked_To_Company,
	        @HQTerms_VAT_Banked_To_Supplier,
	        @HQTerms_Banked_To_Company,
	        @HQTerms_Banked_To_Supplier,
	        @HQTerms_LOS,
	        @HQTerms_Supplier_VAT_LOS,
	        @HQTerms_Company_VAT_LOS,
	        @HQTerms_Sec_Brewery_VAT_LOS,
	        @HQTerms_Location_VAT_LOS,
	        @HQTerms_Supplier_VAT_Banked_To_Company,
	        @HQTerms_Company_VAT_Banked_To_Company,
	        @HQTerms_Sec_Brewery_VAT_Banked_To_Company,
	        @HQTerms_Location_VAT_Banked_To_Company,
	        @HQTerms_Supplier_VAT_Banked_To_Supplier,
	        @HQTerms_Company_VAT_Banked_To_Supplier,
	        @HQTerms_Sec_Brewery_VAT_Banked_To_Supplier,
	        @HQTerms_Location_VAT_Banked_To_Supplier
	      )
	END
	ELSE
	BEGIN
	    UPDATE CT
	    SET    CT.HQTerms_Company_Share = @HQTerms_Company_Share,
	           CT.HQTerms_Supplier_Share = @HQTerms_Supplier_Share,
	           CT.HQTerms_Location_Share = @HQTerms_Location_Share,
	           CT.HQTerms_AMLD = @HQTerms_AMLD,
	           CT.HQTerms_VAT_Output = @HQTerms_VAT_Output,
	           CT.HQTerms_VAT_Company = @HQTerms_VAT_Company,
	           CT.HQTerms_VAT_Supplier = @HQTerms_VAT_Supplier,
	           CT.HQTerms_VAT_Location = @HQTerms_VAT_Location,
	           CT.HQTerms_VAT_LOS = @HQTerms_VAT_LOS,
	           CT.HQTerms_VAT_Banked_To_Company = @HQTerms_VAT_Banked_To_Company,
	           CT.HQTerms_VAT_Banked_To_Supplier = @HQTerms_VAT_Banked_To_Supplier,
	           CT.HQTerms_FloatSundries_LOS = CD.Collection_Sundries + CD.Collection_Sundries_Unsupported 
	           + CD.Collection_Non_Supplier_Float_Recovered,
	           CT.HQTerms_FloatSundries_Banked_To_Supplier = CD.Collection_Supplier_Float_Recovered,
	           CT.HQTerms_Supplier_VAT_LOS = @HQTerms_Supplier_VAT_LOS,
	           CT.HQTerms_Company_VAT_LOS = @HQTerms_Company_VAT_LOS,
	           CT.HQTerms_Sec_Brewery_VAT_LOS = @HQTerms_Sec_Brewery_VAT_LOS,
	           CT.HQTerms_Location_VAT_LOS = @HQTerms_Location_VAT_LOS,
	           CT.HQTerms_Supplier_VAT_Banked_To_Company = @HQTerms_Supplier_VAT_Banked_To_Company,
	           CT.HQTerms_Company_VAT_Banked_To_Company = @HQTerms_Company_VAT_Banked_To_Company,
	           CT.HQTerms_Sec_Brewery_VAT_Banked_To_Company = @HQTerms_Sec_Brewery_VAT_Banked_To_Company,
	           CT.HQTerms_Location_VAT_Banked_To_Company = @HQTerms_Location_VAT_Banked_To_Company,
	           CT.HQTerms_Supplier_VAT_Banked_To_Supplier = @HQTerms_Supplier_VAT_Banked_To_Supplier,
	           CT.HQTerms_Company_VAT_Banked_To_Supplier = @HQTerms_Company_VAT_Banked_To_Supplier,
	           CT.HQTerms_Sec_Brewery_VAT_Banked_To_Supplier = @HQTerms_Sec_Brewery_VAT_Banked_To_Supplier,
	           CT.HQTerms_Location_VAT_Banked_To_Supplier = @HQTerms_Location_VAT_Banked_To_Supplier,
	           CT.HQTerms_Company_Share_LOS = @HQTerms_Company_Share_LOS,
	           CT.HQTerms_Supplier_Share_LOS = @HQTerms_Supplier_Share_LOS,
	           CT.HQTerms_Location_Share_LOS = @HQTerms_Location_Share_LOS,
	           CT.HQTerms_Banked_To_Company = @HQTerms_Banked_To_Company,
	           CT.HQTerms_Banked_To_Supplier = @HQTerms_Banked_To_Supplier,
	           CT.HQTerms_VAT_Sec_Brewery = @HQTerms_VAT_Sec_Brewery,
	           CT.HQTerms_Sec_Brewery_Share_LOS = @HQTerms_Sec_Brewery_Share_LOS,
	           CT.HQTerms_Company_Share_Banked_To_Company = @HQTerms_Company_Share_Banked_To_Company,
	           CT.HQTerms_Company_Share_Banked_To_Supplier = @HQTerms_Company_Share_Banked_To_Supplier,
	           CT.HQTerms_Sec_Brewery_Share_Banked_To_Company = @HQTerms_Sec_Brewery_Share_Banked_To_Company,
	           CT.HQTerms_Sec_Brewery_Share_Banked_To_Supplier = @HQTerms_Sec_Brewery_Share_Banked_To_Supplier,
	           CT.HQTerms_Supplier_Share_Banked_To_Company = @HQTerms_Supplier_Share_Banked_To_Company,
	           CT.HQTerms_Supplier_Share_Banked_To_Supplier = @HQTerms_Supplier_Share_Banked_To_Supplier,
	           CT.HQTerms_Site_Share_Banked_To_Company = @HQTerms_Site_Share_Banked_To_Company,
	           CT.HQTerms_Site_Share_Banked_To_Supplier = @HQTerms_Site_Share_Banked_To_Supplier,
	           CT.HQTerms_Licence_Banked_To_Company = @HQTerms_Licence_Banked_To_Company,
	           CT.HQTerms_Licence_Banked_To_Supplier = @HQTerms_Licence_Banked_To_Supplier,
	           CT.HQTerms_Licence_LOS = @HQTerms_Licence_LOS,
	           CT.HQTerms_LOS = @HQTerms_LOS
	    FROM   Collection_Terms CT
	           INNER JOIN Collection_Details CD
	                ON  CT.Collection_ID = CD.Collection_ID
	    WHERE  CT.Collection_ID = @CollectionID
	END
END
GO
