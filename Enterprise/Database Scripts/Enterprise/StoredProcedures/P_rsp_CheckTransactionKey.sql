USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_CheckTransactionKey]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_CheckTransactionKey]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =======================================================================    
-- OUTPUT           To verify the authorization code and site code sent from Exchange with enterprise  
--Return Codes:@IsAuthenticated  
-- 0 : Not Authenticated  
-- 1 : Reset  
-- 2 : Recovery  
-- 3 : New  
-- 4 : Key Expired 
-- 6 : Site Code not found  
-- 7 : Site Closed  
-- 8 : Site AAMS Code missing 
-- 9 : Void Action
-- =======================================================================    
-- Revision History    
-- Poorna Chander 17/09/2009  
-- Vineetha Mathew 19/09/09  Modified Added code to check site code exists or not     
-- Vineetha Mathew 16/10/09  Modified to bring registry keys  
-- G Babu		13/09/10	  Removed the Transaction Key with prefix zero
---------------------------------------------------------------------------    
CREATE PROCEDURE [dbo].[rsp_CheckTransactionKey]    
    
(@Site_Code VARCHAR(10),   
 @TransactionKey varchar(100),
 @TransactionType varchar(100),
 @IsAuthenticated nvarchar(520) output)    

AS    
    
BEGIN    
  
 DECLARE @TranKeyID int    
 DECLARE @TransactionFlagId int    
 DECLARE @Site_Id int    
 DECLARE @SiteClosed int    
 DECLARE @SiteAAMSStatus varchar(50)    
 DECLARE @Date DATETIME  
 DECLARE @Void BIT
 --DECLARE @ExchangeKey nvarchar(500)    
 --DECLARE @EnterpriseKey nvarchar(500)  
  
 SET @TranKeyID = 0    
 SET @IsAuthenticated = '0'    
 SET @TransactionFlagID = 0    
 WHILE CHARINDEX('0',@TransactionKey,0) = 1
 BEGIN
 SET @TransactionKey = CASE	WHEN CHARINDEX('0',@TransactionKey,0) > 0 
				THEN SUBSTRING(@TransactionKey, 2, Len(@TransactionKey))
				ELSE @TransactionKey
				END
END 
 SELECT @Site_Id = ISNULL(SITE_ID,0), @SiteClosed = ISNULL(Site_Closed,0) FROM SITE WHERE SITE_CODE= @SITE_CODE    
 SELECT @SiteAAMSStatus = ISNULL(BAD_AAMS_Code,'') FROM BMC_AAMS_Details BAD INNER JOIN SITE S ON BAD.BAD_Reference_ID = S.Site_ID  
 WHERE S.Site_Code = @SITE_CODE AND BAD.BAD_AAMS_Entity_Type = 2  
  
 --EXEC master.dbo.xp_regread 'HKEY_LOCAL_MACHINE','SOFTWARE\HoneyFrame\CashmasterHQ\Settings\','EnterpriseKey', @EnterpriseKey OUTPUT  
 --select @EnterpriseKey  
  
 IF @Site_Id > 0    
 BEGIN  
  --Site Closed.  
	  IF @SiteClosed = 1  
	  BEGIN  
		   SET @IsAuthenticated = '7'  
		   RETURN  
	  END  
  --AAMS Code Missing.  
	  IF LEN(@SiteAAMSStatus) = 0  
	  BEGIN  
		   SET @IsAuthenticated = '8'  
		   RETURN  
	  END  
	  SELECT   
		  @TranKeyID = [TransactionKeyid] ,   
		  @TransactionFlagId = [TransactionKeys].[TransactionFlagid]  
		  --,@ExchangeKey=Site.ExchangeKey  
		  ,@Void= [TransactionKeys].Void
	  FROM [TransactionKeys]     
	  JOIN Site ON Site.Site_Id = [TransactionKeys].SiteId    
	  JOIN [TransactionFlag] ON [TransactionFlag].[TransactionFlagid] = [TransactionKeys].[TransactionFlagid]    
	  WHERE [TransactionKeys].[TransactionKey] = @TransactionKey AND Site.Site_Id = @SITE_ID
	  AND [TransactionFlag].TransactionFlagName = @TransactionType
	  
	  --To set expiry date for the transaction key after use of key   
	  SELECT @Date = ExpiryDate FROM [TransactionKeys] WHERE [TransactionKeyid] = @TranKeyID 
	 --Check the transaction voided
	  IF ISNULL(@Void,0)>0
		BEGIN
			SET @IsAuthenticated = '9'  
			RETURN 
		END
	  IF GetDate() > @Date  
		  BEGIN    
			SET @IsAuthenticated = '4'    
		  END   
	  ELSE  
		  BEGIN  
		   --To set expiry date for the transaction key      
		   UPDATE [TransactionKeys] SET ExpiryDate = GetDate() - 1 WHERE [TransactionKeyid] = @TranKeyID
		      		  
		   IF @@ROWCOUNT > 0    
			   BEGIN    
			  
				   --This is to notify the exchange what is happening on site for first time.later SiteStatus will be updated by site  
				   Update [Site] SET SiteStatus = TransactionFlagName     
				   FROM transactionflag WHERE transactionflag.[TransactionFlagid] = @TransactionFlagId    
				   AND Site_ID = @Site_Id   
				  
				   --Get Authentication Code with the enterprise and exchange keys  
				   --SET @IsAuthenticated = CAST(@TransactionFlagId as varchar(2)) + ','+ ISNULL(@ExchangeKey,'') +',' +ISNULL(@EnterpriseKey,'')  
				   SET @IsAuthenticated = CAST(@TransactionFlagId as varchar(2))   
			  
			   END   
		  END    
 END  
 ELSE    
  BEGIN    
   SET @IsAuthenticated = '6'    
  END   
END     


GO

