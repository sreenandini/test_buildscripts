USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_InsertTreasury]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_InsertTreasury]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[usp_InsertTreasury]                
                
  @Installation_No              INT,                
  @Collection_No                INT,                
  @User_ID                      INT,                
  @Treasury_Type                VARCHAR(30),                
  @Treasury_Reason              VARCHAR(200),                
  @Treasury_Amount              MONEY,                
  @Treasury_Allocated           BIT  ,              
  @Treasury_Membership_No       VARCHAR(50) = NULL,              
  @Treasury_Reason_Code			INT = 0,              
  @Treasury_Issuer_User_No		INT =0,              
  @Treasury_Temp				BIT = 0,              
  @Treasury_Float_Issued_By		INT =0,
  @Treasury_Actual_Date			DATETIME = NULL,
  @CustomerID					bigint = 0,
  @AuthorizedUser_No     bigint = 0,  
  @Authorized_Date   DATETIME = NULL,
  @IsManualAttendantPay INT = 0,
  @SiteId INT,
  @TreasuryNo INT OUTPUT            
                
 /**                
  @TreasurySoFar                MONEY,                
  @TreasuryRepayments           MONEY,                
  @TreasuryRefills              MONEY,                
  @TreasuryTokens               MONEY,                
  @TreasuryHandpay              MONEY                
**/                
AS                 
                
BEGIN TRANSACTION               
  SET DATEFORMAT DMY        
  
  DECLARE @currentDate DATETIME
	SET @currentDate = GETDATE()
	
	IF (@IsManualAttendantPay = 1)
	    SET @Treasury_Actual_Date = @currentDate        
                              
  -- Create treasury record ..                
  --                
  INSERT INTO Treasury_Entry                 
              (                 
                Installation_Id,                 
                Collection_Id,                 
                UserId,                 
                Treasury_Date,                
                Treasury_Time,                  
                Treasury_Amount,                
                Treasury_Reason,                
                Treasury_Allocated,                
                Treasury_Type,                
--                Treasury_Breakdown_2p,                
--                Treasury_Breakdown_5p,                
--                Treasury_Breakdown_10p,                
--                Treasury_Breakdown_20p,                
--                Treasury_Breakdown_50p,                
--                Treasury_Breakdown_100p,                
--                Treasury_Breakdown_200p ,              
    Treasury_Membership_No,              
    Treasury_Reason_Code,              
    Treasury_Issuer_User_No,              
    Treasury_Temp,              
    Treasury_Float_Issued_By,
	Treasury_Actual_Date,
	CustomerID,
	AuthorizedUser_No,
	Authorized_Date,
	IsManualAttendantPay)                
      VALUES                 
              (                
                @Installation_No,                
                @Collection_No,                
                @User_ID,                
                CONVERT(VARCHAR(20),Getdate(),106),
                CONVERT(VARCHAR(20),GetDate(),108),
                @Treasury_Amount,                
                @Treasury_Reason,                
                @Treasury_Allocated,                
                @Treasury_Type,                
--                @Treasury_Breakdown_2p,                
--    @Treasury_Breakdown_5p,                
--                @Treasury_Breakdown_10p,                
--                @Treasury_Breakdown_20p,                
--                @Treasury_Breakdown_50p,                
--                @Treasury_Breakdown_100p,                
--                @Treasury_Breakdown_200p,              
    @Treasury_Membership_No,              
    @Treasury_Reason_Code,              
    @Treasury_Issuer_User_No,              
    @Treasury_Temp,              
    @Treasury_Float_Issued_By,
	@Treasury_Actual_Date,
	@CustomerID,
	@AuthorizedUser_No,
	@Authorized_Date,
	@IsManualAttendantPay)                
                  
	SET @TreasuryNo = SCOPE_IDENTITY()
            
-- check error if any                
--              
IF (@@ERROR = 0)              
BEGIN            
  COMMIT TRANSACTION              
END            
ELSE              
 ROLLBACK TRANSACTION              
--RETURN @@Error     

GO

