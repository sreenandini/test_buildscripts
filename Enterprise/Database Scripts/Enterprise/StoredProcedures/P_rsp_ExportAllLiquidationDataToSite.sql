USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_ExportAllLiquidationDataToSite]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_ExportAllLiquidationDataToSite]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[rsp_ExportAllLiquidationDataToSite]
	@Site_Code VARCHAR(50)
AS
BEGIN
	
	IF ISNULL(@Site_Code, '') = ''
		RETURN
		
	IF NOT EXISTS(SELECT 1 FROM [Site] S WHERE LTRIM(RTRIM(S.Site_Code)) = LTRIM(RTRIM(@Site_Code)))
		RETURN
	
	INSERT INTO dbo.Export_History
	  (
	    EH_Date,
	    EH_Reference1,
	    EH_Type,
	    EH_Site_Code
	  )
	SELECT GETDATE(),
	       SH.ShareHolderId,
	       'SHAREHOLDER',
	       @Site_Code
	FROM   ShareHolders SH WITH(NOLOCK)
	
	
	INSERT INTO dbo.Export_History
	  (
	    EH_Date,
	    EH_Reference1,
	    EH_Type,
	    EH_Site_Code
	  )
	SELECT GETDATE(),
	       PSG.ProfitShareGroupId,
	       'PROFITSHAREGROUP',
	       @Site_Code
	FROM   ProfitShareGroup PSG
	       
	       
	INSERT INTO dbo.Export_History
	      (
	        EH_Date,
	        EH_Reference1,
	        EH_Type,
	        EH_Site_Code
	      )
	    SELECT GETDATE(),
	           PS.ProfitShareId,
	           'PROFITSHARE',
	           @Site_Code
	    FROM   ProfitShare PS
	       
	
	INSERT INTO dbo.Export_History
	  (
	    EH_Date,
	    EH_Reference1,
	    EH_Type,
	    EH_Site_Code
	  )
	SELECT GETDATE(),
	       ESG.ExpenseShareGroupId,
	       'EXPENSESHAREGROUP',
	       @Site_Code
	FROM   ExpenseShareGroup ESG WITH(NOLOCK)
	
	INSERT INTO dbo.Export_History
	  (
	    EH_Date,
	    EH_Reference1,
	    EH_Type,
	    EH_Site_Code
	  )
	SELECT GETDATE(),
	       ES.ExpenseShareId,
	       'EXPENSESHARE',
	       @Site_Code
	FROM   [SITE] S WITH(NOLOCK),
	       ExpenseShare ES WITH(NOLOCK)

END
GO