USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_ecUpdateSubCompany]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_ecUpdateSubCompany]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE usp_ecUpdateSubCompany
	@Sub_Company_ID INT OUTPUT,
	@Sub_Company_Name VARCHAR(50),
	@Company_ID INT,
	@Sub_Company_Switchboard_Phone_No VARCHAR(15),
	@Sub_Company_Address_1 VARCHAR(50),
	@Sub_Company_Address_2 VARCHAR(50),
	@Sub_Company_Address_3 VARCHAR(50),
	@Sub_Company_Address_4 VARCHAR(50),
	@Sub_Company_Address_5 VARCHAR(50),
	@Sub_Company_Postcode VARCHAR(15),
	@Sub_Company_ANA_Number VARCHAR(20),
	@Sub_Company_Income_Ledger_Code VARCHAR(20),
	@Sage_Account_Ref VARCHAR(50),
	@Company_Model_Set_ID INT,
	@Sub_Company_Trade_Type VARCHAR(50),
	@Sub_Company_Contact_Name VARCHAR(50),
	@Sub_Company_Contact_Phone_No VARCHAR(15),
	@Sub_Company_Contact_Email_Address VARCHAR(100),
	@Sub_Company_Use_Split_Rents BIT,
	@Sub_Company_Price_Per_Play_Default BIT,
	@Sub_Company_Price_Per_Play VARCHAR(50),
	@Sub_Company_Jackpot_Default BIT,
	@Sub_Company_Jackpot VARCHAR(50),
	@Sub_Company_Percentage_Payout_Default BIT,
	@Sub_Company_Percentage_Payout VARCHAR(50),
	@Terms_Group_ID_Default BIT,
	@Terms_Group_ID INT,
	@Access_Key_ID_Default BIT,
	@Access_Key_ID INT,
	@Staff_ID_Default BIT,
	@Staff_ID INT,
	@Sub_Company_Default_Opening_Hours_ID INT,
	@Sub_Company_Invoice_Name VARCHAR(50),
	@Sub_Company_Invoice_Address NTEXT,
	@Sub_Company_Invoice_Postcode VARCHAR(15),
	@Sub_Company_Account_Name VARCHAR(32),
	@Sub_Company_Account_No VARCHAR(12),
	@Sub_Company_Sort_Code VARCHAR(8),
	@UpdateAllSites BIT = FALSE
AS
BEGIN
	IF EXISTS (
	       SELECT 1
	       FROM   Sub_Company sc
	       WHERE  sc.Sub_Company_ID = @Sub_Company_ID
	   )
	BEGIN
	    UPDATE Sub_Company
	    SET    Sub_Company_Name = @Sub_Company_Name,
	           Company_ID = @Company_ID,
	           Sub_Company_Switchboard_Phone_No = @Sub_Company_Switchboard_Phone_No,
	           Sub_Company_Address_1 = @Sub_Company_Address_1,
	           Sub_Company_Address_2 = @Sub_Company_Address_2,
	           Sub_Company_Address_3 = @Sub_Company_Address_3,
	           Sub_Company_Address_4 = @Sub_Company_Address_4,
	           Sub_Company_Address_5 = @Sub_Company_Address_5,
	           Sub_Company_Postcode = @Sub_Company_Postcode,
	           Sub_Company_ANA_Number = @Sub_Company_ANA_Number,
	           Sub_Company_Income_Ledger_Code = @Sub_Company_Income_Ledger_Code,
	           Sage_Account_Ref = @Sage_Account_Ref,
	           Company_Model_Set_ID = @Company_Model_Set_ID,
	           Sub_Company_Trade_Type = @Sub_Company_Trade_Type,
	           Sub_Company_Contact_Name = @Sub_Company_Contact_Name,
	           Sub_Company_Contact_Phone_No = @Sub_Company_Contact_Phone_No,
	           Sub_Company_Contact_Email_Address = @Sub_Company_Contact_Email_Address,
	           Sub_Company_Use_Split_Rents = @Sub_Company_Use_Split_Rents,
	           Sub_Company_Price_Per_Play_Default = @Sub_Company_Price_Per_Play_Default,
	           Sub_Company_Price_Per_Play = @Sub_Company_Price_Per_Play,
	           Sub_Company_Jackpot_Default = @Sub_Company_Jackpot_Default,
	           Sub_Company_Jackpot = @Sub_Company_Jackpot,
	           Sub_Company_Percentage_Payout_Default = @Sub_Company_Percentage_Payout_Default,
	           Sub_Company_Percentage_Payout = @Sub_Company_Percentage_Payout,
	           Terms_Group_ID_Default = @Terms_Group_ID_Default,
	           Terms_Group_ID = @Terms_Group_ID,
	           Access_Key_ID_Default = @Access_Key_ID_Default,
	           Access_Key_ID = @Access_Key_ID,
	           Staff_ID_Default = @Staff_ID_Default,
	           Staff_ID = @Staff_ID,
	           Sub_Company_Default_Opening_Hours_ID = @Sub_Company_Default_Opening_Hours_ID,
	           Sub_Company_Invoice_Name = @Sub_Company_Invoice_Name,
	           Sub_Company_Invoice_Address = @Sub_Company_Invoice_Address,
	           Sub_Company_Invoice_Postcode = @Sub_Company_Invoice_Postcode,
	           Sub_Company_Account_Name = @Sub_Company_Account_Name,
	           Sub_Company_Account_No = @Sub_Company_Account_No,
	           Sub_Company_Sort_Code = @Sub_Company_Sort_Code
	    WHERE  Sub_Company_ID = @Sub_Company_ID
	    
	     UPDATE MeterAnalysis.dbo.Sub_Company
	    SET    Sub_Company_Name = @Sub_Company_Name,
	           Company_ID = @Company_ID,
	           Sub_Company_Switchboard_Phone_No = @Sub_Company_Switchboard_Phone_No,
	           Sub_Company_Address_1 = @Sub_Company_Address_1,
	           Sub_Company_Address_2 = @Sub_Company_Address_2,
	           Sub_Company_Address_3 = @Sub_Company_Address_3,
	           Sub_Company_Address_4 = @Sub_Company_Address_4,
	           Sub_Company_Address_5 = @Sub_Company_Address_5,
	           Sub_Company_Postcode = @Sub_Company_Postcode,
	           Sub_Company_ANA_Number = @Sub_Company_ANA_Number,
	           Sub_Company_Income_Ledger_Code = @Sub_Company_Income_Ledger_Code,
	           Sage_Account_Ref = @Sage_Account_Ref,
	           Company_Model_Set_ID = @Company_Model_Set_ID,
	           Sub_Company_Trade_Type = @Sub_Company_Trade_Type,
	           Sub_Company_Contact_Name = @Sub_Company_Contact_Name,
	           Sub_Company_Contact_Phone_No = @Sub_Company_Contact_Phone_No,
	           Sub_Company_Contact_Email_Address = @Sub_Company_Contact_Email_Address,
	           Sub_Company_Use_Split_Rents = @Sub_Company_Use_Split_Rents,
	           Sub_Company_Price_Per_Play_Default = @Sub_Company_Price_Per_Play_Default,
	           Sub_Company_Price_Per_Play = @Sub_Company_Price_Per_Play,
	           Sub_Company_Jackpot_Default = @Sub_Company_Jackpot_Default,
	           Sub_Company_Jackpot = @Sub_Company_Jackpot,
	           Sub_Company_Percentage_Payout_Default = @Sub_Company_Percentage_Payout_Default,
	           Sub_Company_Percentage_Payout = @Sub_Company_Percentage_Payout,
	           Terms_Group_ID_Default = @Terms_Group_ID_Default,
	           Terms_Group_ID = @Terms_Group_ID,
	           Access_Key_ID_Default = @Access_Key_ID_Default,
	           Access_Key_ID = @Access_Key_ID,
	           Staff_ID_Default = @Staff_ID_Default,
	           Staff_ID = @Staff_ID,
	           Sub_Company_Default_Opening_Hours_ID = @Sub_Company_Default_Opening_Hours_ID,
	           Sub_Company_Invoice_Name = @Sub_Company_Invoice_Name,
	           Sub_Company_Invoice_Address = @Sub_Company_Invoice_Address,
	           Sub_Company_Invoice_Postcode = @Sub_Company_Invoice_Postcode,
	           Sub_Company_Account_Name = @Sub_Company_Account_Name,
	           Sub_Company_Account_No = @Sub_Company_Account_No,
	           Sub_Company_Sort_Code = @Sub_Company_Sort_Code
	    WHERE  Sub_Company_ID = @Sub_Company_ID
	END
	ELSE
	BEGIN
	    INSERT INTO Sub_Company
	      (
	        Sub_Company_Name,
	        Company_ID,
	        Sub_Company_Switchboard_Phone_No,
	        Sub_Company_Address_1,
	        Sub_Company_Address_2,
	        Sub_Company_Address_3,
	        Sub_Company_Address_4,
	        Sub_Company_Address_5,
	        Sub_Company_Postcode,
	        Sub_Company_ANA_Number,
	        Sub_Company_Income_Ledger_Code,
	        Sage_Account_Ref,
	        Company_Model_Set_ID,
	        Sub_Company_Trade_Type,
	        Sub_Company_Contact_Name,
	        Sub_Company_Contact_Phone_No,
	        Sub_Company_Contact_Email_Address,
	        Sub_Company_Use_Split_Rents,
	        Sub_Company_Price_Per_Play_Default,
	        Sub_Company_Price_Per_Play,
	        Sub_Company_Jackpot_Default,
	        Sub_Company_Jackpot,
	        Sub_Company_Percentage_Payout_Default,
	        Sub_Company_Percentage_Payout,
	        Terms_Group_ID_Default,
	        Terms_Group_ID,
	        Access_Key_ID_Default,
	        Access_Key_ID,
	        Staff_ID_Default,
	        Staff_ID,
	        Sub_Company_Default_Opening_Hours_ID,
	        Sub_Company_Invoice_Name,
	        Sub_Company_Invoice_Address,
	        Sub_Company_Invoice_Postcode,
	        Sub_Company_Account_Name,
	        Sub_Company_Account_No,
	        Sub_Company_Sort_Code
	      )
	    VALUES
	      (
	        @Sub_Company_Name,
	        @Company_ID,
	        @Sub_Company_Switchboard_Phone_No,
	        @Sub_Company_Address_1,
	        @Sub_Company_Address_2,
	        @Sub_Company_Address_3,
	        @Sub_Company_Address_4,
	        @Sub_Company_Address_5,
	        @Sub_Company_Postcode,
	        @Sub_Company_ANA_Number,
	        @Sub_Company_Income_Ledger_Code,
	        @Sage_Account_Ref,
	        @Company_Model_Set_ID,
	        @Sub_Company_Trade_Type,
	        @Sub_Company_Contact_Name,
	        @Sub_Company_Contact_Phone_No,
	        @Sub_Company_Contact_Email_Address,
	        @Sub_Company_Use_Split_Rents,
	        @Sub_Company_Price_Per_Play_Default,
	        @Sub_Company_Price_Per_Play,
	        @Sub_Company_Jackpot_Default,
	        @Sub_Company_Jackpot,
	        @Sub_Company_Percentage_Payout_Default,
	        @Sub_Company_Percentage_Payout,
	        @Terms_Group_ID_Default,
	        @Terms_Group_ID,
	        @Access_Key_ID_Default,
	        @Access_Key_ID,
	        @Staff_ID_Default,
	        @Staff_ID,
	        @Sub_Company_Default_Opening_Hours_ID,
	        @Sub_Company_Invoice_Name,
	        @Sub_Company_Invoice_Address,
	        @Sub_Company_Invoice_Postcode,
	        @Sub_Company_Account_Name,
	        @Sub_Company_Account_No,
	        @Sub_Company_Sort_Code
	      )
	      
	      INSERT INTO MeterAnalysis.dbo.Sub_Company
	      (
	        Sub_Company_Name,
	        Company_ID,
	        Sub_Company_Switchboard_Phone_No,
	        Sub_Company_Address_1,
	        Sub_Company_Address_2,
	        Sub_Company_Address_3,
	        Sub_Company_Address_4,
	        Sub_Company_Address_5,
	        Sub_Company_Postcode,
	        Sub_Company_ANA_Number,
	        Sub_Company_Income_Ledger_Code,
	        Sage_Account_Ref,
	        Company_Model_Set_ID,
	        Sub_Company_Trade_Type,
	        Sub_Company_Contact_Name,
	        Sub_Company_Contact_Phone_No,
	        Sub_Company_Contact_Email_Address,
	        Sub_Company_Use_Split_Rents,
	        Sub_Company_Price_Per_Play_Default,
	        Sub_Company_Price_Per_Play,
	        Sub_Company_Jackpot_Default,
	        Sub_Company_Jackpot,
	        Sub_Company_Percentage_Payout_Default,
	        Sub_Company_Percentage_Payout,
	        Terms_Group_ID_Default,
	        Terms_Group_ID,
	        Access_Key_ID_Default,
	        Access_Key_ID,
	        Staff_ID_Default,
	        Staff_ID,
	        Sub_Company_Default_Opening_Hours_ID,
	        Sub_Company_Invoice_Name,
	        Sub_Company_Invoice_Address,
	        Sub_Company_Invoice_Postcode,
	        Sub_Company_Account_Name,
	        Sub_Company_Account_No,
	        Sub_Company_Sort_Code
	      )
	    VALUES
	      (
	        @Sub_Company_Name,
	        @Company_ID,
	        @Sub_Company_Switchboard_Phone_No,
	        @Sub_Company_Address_1,
	        @Sub_Company_Address_2,
	        @Sub_Company_Address_3,
	        @Sub_Company_Address_4,
	        @Sub_Company_Address_5,
	        @Sub_Company_Postcode,
	        @Sub_Company_ANA_Number,
	        @Sub_Company_Income_Ledger_Code,
	        @Sage_Account_Ref,
	        @Company_Model_Set_ID,
	        @Sub_Company_Trade_Type,
	        @Sub_Company_Contact_Name,
	        @Sub_Company_Contact_Phone_No,
	        @Sub_Company_Contact_Email_Address,
	        @Sub_Company_Use_Split_Rents,
	        @Sub_Company_Price_Per_Play_Default,
	        @Sub_Company_Price_Per_Play,
	        @Sub_Company_Jackpot_Default,
	        @Sub_Company_Jackpot,
	        @Sub_Company_Percentage_Payout_Default,
	        @Sub_Company_Percentage_Payout,
	        @Terms_Group_ID_Default,
	        @Terms_Group_ID,
	        @Access_Key_ID_Default,
	        @Access_Key_ID,
	        @Staff_ID_Default,
	        @Staff_ID,
	        @Sub_Company_Default_Opening_Hours_ID,
	        @Sub_Company_Invoice_Name,
	        @Sub_Company_Invoice_Address,
	        @Sub_Company_Invoice_Postcode,
	        @Sub_Company_Account_Name,
	        @Sub_Company_Account_No,
	        @Sub_Company_Sort_Code
	      )
	      SET @Sub_Company_ID = SCOPE_IDENTITY()
	END
	UPDATE SITE
	SET    Site_Trade_Type = @Sub_Company_Trade_Type
	WHERE  Sub_Company_ID = @Sub_Company_ID
	       AND @UpdateAllSites = 1
	
	IF @@ERROR <= 0
	    RETURN 0
	ELSE
	    RETURN 1
END

GO

