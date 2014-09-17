/************************************************************
 * Code formatted by SoftTree SQL Assistant © v4.8.29
 * Time: 10/04/13 7:42:20 PM
 ************************************************************/

USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[usp_ImportVaultBalanceDetailsFromXML]')
              AND TYPE IN (N'P' ,N'PC')
   )
    DROP PROCEDURE [dbo].[usp_ImportVaultBalanceDetailsFromXML]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*  

Exec usp_ImportVaultBalanceDetailsFromXML '<ROOT>
  <Site>
    <Type>VAULTBALANCE</Type>
    <EH_ID>9890</EH_ID>
    <SiteXML>
     <VaultBalance>
  <VaultBalance_Info>
    <HQ_Installation_No>15</HQ_Installation_No>
    <Site_Code>1005</Site_Code>
    <VaultID>Vault1</VaultID>
    <FillDate>2013-04-05T17:15:28.813</FillDate>
    <Fills>11.210000000000000e+002</Fills>
     <FillAmount>150</FillAmount>
    <TotalFundsIn>0.000000000000000e+000</TotalFundsIn>
    <TotalFundsAtSite>8.790000000000000e+002</TotalFundsAtSite>
    <VoucherOut>2.000000000000000e+000</VoucherOut>
    <AttendantPay>1.000000000000000e+001</AttendantPay>
    <Jackpot>1.000000000000000e+001</Jackpot>
  </VaultBalance_Info>
</VaultBalance>
    </SiteXML>
  </Site>
</ROOT>',null

Select * from TotalVaultBalance
*/  

CREATE PROCEDURE usp_ImportVaultBalanceDetailsFromXML
	@doc VARCHAR(MAX),
	@IsSuccess INT OUTPUT
AS
BEGIN
    DECLARE @idoc INT  
    
    SET @IsSuccess = 0  
    
    IF ISNULL(@doc ,'')=''
        RETURN 0  
    
    SET @doc = '<?xml version="1.0" encoding="ISO-8859-1"?>'+@doc  
    
    EXEC sp_xml_preparedocument @idoc OUTPUT
        ,@doc  
    
    
    DECLARE @HQ_Installation_No  INT
           ,@Site_Code           VARCHAR(50)
           ,@Site_ID             INT
           ,@Site_Address        NVARCHAR(200)
           ,@OldFills               FLOAT
    
    SELECT @HQ_Installation_No = HQ_Installation_No
          ,@Site_Code = Site_Code
    FROM   OPENXML(@idoc ,'.//VaultBalance/VaultBalance_Info' ,1) WITH 
           (
               HQ_Installation_No INT './HQ_Installation_No'
              ,Site_Code VARCHAR(50) './Site_Code'
           )
    
    SELECT @OldFills = Fills
    FROM   TotalVaultBalance
    WHERE  Installation_ID = @HQ_Installation_No 
    
    SELECT @Site_ID = Site_ID
          ,@Site_Address = Site_Address
    FROM   [SITE] WITH(NOLOCK)
    WHERE  Site_Code = @Site_Code
    
    
    IF NOT EXISTS(
           SELECT Installation_ID
           FROM   TotalVaultBalance
           WHERE  Installation_ID = @HQ_Installation_No
       )
    BEGIN
        INSERT INTO dbo.TotalVaultBalance
          (
            Site_ID
           ,Installation_ID
          )
        VALUES
          (
            @Site_ID
           ,@HQ_Installation_No
          )
    END
    
    UPDATE TF
    SET    TF.VaultID = A.VaultID
          ,TF.FillDate = A.FillDate
          ,TF.Fills = A.Fills
          ,TF.TotalFundsIn = A.TotalFundsIn
          ,TF.TotalFundsAtSite = A.TotalFundsAtSite
          ,TF.VoucherOut = A.VoucherOut
          ,TF.AttendantPay = A.AttendantPay
          ,TF.Jackpot = A.Jackpot
          ,TF.FillAmount= A.FillAmount
    FROM   TotalVaultBalance TF
           INNER JOIN OPENXML(@idoc ,'.//VaultBalance/VaultBalance_Info' ,2) 
                WITH 
                (
                    HQ_Installation_No INT './HQ_Installation_No'
                   ,VaultID VARCHAR(150) './VaultID'
                   ,FillDate DATETIME './FillDate'
                   ,Fills FLOAT './Fills'
                   ,FillAmount FLOAT './FillAmount'
                   ,TotalFundsIn FLOAT './TotalFundsIn'
                   ,TotalFundsAtSite FLOAT './TotalFundsAtSite'
                   ,VoucherOut FLOAT './VoucherOut'
                   ,AttendantPay FLOAT './AttendantPay'
                   ,Jackpot FLOAT './Jackpot'                 
                )A
                ON  TF.Installation_ID = A.HQ_Installation_No
    
    IF @@Error<>0
    BEGIN
        SET @IsSuccess = -1 -- failed while updating the records in the VaultBalance table
    END
END
GO

