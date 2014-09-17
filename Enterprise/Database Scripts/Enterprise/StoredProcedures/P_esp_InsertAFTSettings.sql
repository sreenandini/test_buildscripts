USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[esp_InsertAFTSettings]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[esp_InsertAFTSettings]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE esp_InsertAFTSettings      
(      
@AFTTransactionsAllowed bit,      
@AllowCashableDeposits bit,      
@AllowNonCashableDeposits bit,      
@AllowRedeemOffers bit,      
@AllowPointsWithdrawal bit,      
@AllowCashWithdrawal bit,      
@AllowPartialTransfers bit,      
@AutoDepositNonCashableCreditsonCardOut bit,      
@AutoDepositCashableCreditsonCardOut bit,      
@AllowOffers bit,      
@EFTTimeoutValue int,      
@Option1WithdrawalAmount int,      
@Option2WithdrawalAmount int,      
@Option3WithdrawalAmount int,      
@Option4WithdrawalAmount int,      
@Option5WithdrawalAmount int,      
@MaxDepositAmount int,      
@MaxWithdrawAmount int ,    
@SiteID int , 
@Denom INt   
)      
AS      
      
BEGIN TRAN  
  
  
if not exists (select 1 from AFTSetting where SiteCode = @SiteID and Denom = @Denom)  
begin  
  INSERT INTO  
    AFTSetting   
   (  
    AFTTransactionsAllowed,  
    AllowCashableDeposits,  
    AllowNonCashableDeposits,  
    AllowRedeemOffers,  
    AllowPointsWithdrawal,  
    AllowCashWithdrawal,  
    AllowPartialTransfers,  
    AutoDepositNonCashableCreditsonCardOut,  
    AutoDepositCashableCreditsonCardOut,  
    AllowOffers,  
    EFTTimeoutValue,  
    Option1WithdrawalAmount,  
    Option2WithdrawalAmount,  
    Option3WithdrawalAmount,  
    Option4WithdrawalAmount,  
    Option5WithdrawalAmount,  
    [MaxDepositAmount],  
    [MaxWithDrawAmount],  
    SiteCode ,
	Denom
   )  
  values  
    (  
   @AFTTransactionsAllowed,      
   @AllowCashableDeposits ,      
   @AllowNonCashableDeposits ,      
   @AllowRedeemOffers ,      
   @AllowPointsWithdrawal ,      
   @AllowCashWithdrawal ,      
   @AllowPartialTransfers ,      
   @AutoDepositNonCashableCreditsonCardOut ,      
   @AutoDepositCashableCreditsonCardOut ,      
   @AllowOffers ,      
   @EFTTimeoutValue ,      
   @Option1WithdrawalAmount ,      
   @Option2WithdrawalAmount ,      
   @Option3WithdrawalAmount ,      
   @Option4WithdrawalAmount ,      
   @Option5WithdrawalAmount ,      
   @MaxDepositAmount ,      
   @MaxWithdrawAmount  ,    
   @SiteID  ,
	@Denom
   )  
end  
else  
begin  
   Update AFTSetting set   
  AFTTransactionsAllowed= @AFTTransactionsAllowed,      
  AllowCashableDeposits=@AllowCashableDeposits ,      
  [AllowNonCashableDeposits] =@AllowNonCashableDeposits ,      
  AllowRedeemOffers= @AllowRedeemOffers ,      
  AllowPointsWithdrawal = @AllowPointsWithdrawal ,      
  AllowCashWithdrawal= @AllowCashWithdrawal ,      
  AllowPartialTransfers= @AllowPartialTransfers ,      
  [AutoDepositNonCashableCreditsonCardOut]= @AutoDepositNonCashableCreditsonCardOut ,      
  AutoDepositCashableCreditsonCardOut= @AutoDepositCashableCreditsonCardOut ,      
  AllowOffers=@AllowOffers ,      
  EFTTimeoutValue= @EFTTimeoutValue ,      
  Option1WithdrawalAmount= @Option1WithdrawalAmount ,      
  Option2WithdrawalAmount= @Option2WithdrawalAmount ,      
  Option3WithdrawalAmount=@Option3WithdrawalAmount ,      
  Option4WithdrawalAmount=@Option4WithdrawalAmount ,      
  Option5WithdrawalAmount=@Option5WithdrawalAmount ,      
  [MaxDepositAmount]= @MaxDepositAmount ,      
  [MaxWithDrawAmount]= @MaxWithdrawAmount      
  where SiteCode = @SiteID  and Denom = @Denom
     
end  
   
  
update site set AFT_Settings_Enabled = 1 where site_id = @SiteID  


DECLARE @Site_Code VARCHAR (50)

	SELECT @Site_Code = Site_Code FROM dbo.[Site] WHERE Site_ID = @SiteID 

	EXEC usp_Export_History @Denom, 'AFTSETTINGS', @SiteID


	INSERT INTO dbo.Export_History
		(EH_Date,
		EH_Reference1,
		EH_Type, 
		EH_Site_Code) 
	(SELECT 
			GETDATE(),
			I.Installation_ID,
			'AFTENABLEDISABLE', 
			@Site_Code
	FROM Installation I
		INNER JOIN Bar_Position BP ON BP.Bar_Position_ID = I.Bar_Position_ID
	WHERE 
		I.Installation_End_Date IS NULL
		AND BP.Site_ID =  @SiteID
	)

  
IF @@ERROR = 0  
 COMMIT TRAN  
ELSE  
ROLLBACK TRAN  

GO

