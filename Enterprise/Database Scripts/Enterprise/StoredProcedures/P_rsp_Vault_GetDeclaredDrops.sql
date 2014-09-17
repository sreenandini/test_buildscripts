
/************************************************************
 * Code formatted by SoftTree SQL Assistant © v4.8.29
 * Time: 01/08/13 8:04:51 PM
 ************************************************************/

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_Vault_GetDeclaredDrops]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_Vault_GetDeclaredDrops]
  GO
  -- =============================================
  -- Author:		<SriHari Jogaraj>
  -- Create date: <9th July 2013>
  -- Description:	<Get the Vault Details in Enterprise - Vault Declaration Screen>
  -- Example EXEC rsp_Vault_GetDeclaredDrops 1,1,'2013-06-14 19:48 pm','2013-12-30 19:48 pm',4
  -- =============================================
CREATE PROCEDURE dbo.rsp_Vault_GetDeclaredDrops
	@Vault_ID INT,
	@Site_ID INT,
	@StartDate DATETIME,
	@EndDate DATETIME,
	@VarianceType INT = 0 /*if @VarianceType=0 then all data,if '1' then with varaince,if '2' then zero varaince */
AS
	/*****************************************************************************************************      
DESCRIPTION : Get undeclared records for declaration       
CREATED DATE: 10-july-2013      
MODULE  : Enterprise Vault Declaration screen
CHANGE HISTORY :      
------------------------------------------------------------------------------------------------------      
AUTHOR     DESCRIPTON          MODIFIED DATE      
------------------------------------------------------------------------------------------------------      

*****************************************************************************************************/
BEGIN
	SET NOCOUNT ON 
	DECLARE @Freeze_Drop_ID            BIGINT
	
	SELECT TOP 1 @Freeze_Drop_ID = drop_id
	FROM   tVault_drops WITH(NOLOCK)
	WHERE  Site_ID = @Site_ID
	       AND isfrozen = 0
	ORDER BY
	       drop_id ASC
		
	DECLARE @temp TABLE(
	            [Drop_ID] [bigint] NOT NULL,
	            [OpeningBalance] [decimal](18, 2) NULL,
	            [FillAmount] [decimal](18, 2) NULL,
	            [BleedAmount] [decimal](18, 2) NULL,
	            [AdjustmentAmount] [decimal](18, 2) NULL,
	            [Meter_Balance] [decimal](18, 2) NULL,
	            [Vault_Balance] [decimal](18, 2) NULL,
	            [Declared_Balance] [decimal](18, 2) NULL,
	            [Declared] [bit] NULL,
	            [Freezed] [bit] NULL,
	            [CreatedDate] [datetime] NULL,
	            [CreateUser] [int] NULL,
	            [ModifiedDate] [datetime] NULL,
	            [ModifiedUser] [int] NULL,
	            [FreezedDate] [datetime] NULL,
	            [FreezeUser] [int] NULL,
	            [AuditDate] [datetime] NULL,
	            [AuditUser] [int] NULL,
	            [Site_Drop_Ref] [bigint] NULL,
	            [Site_ID] [int] NULL,
	            [AuditNote] [varchar](500) NULL,
	            [UserName] [varchar] (100) NULL,
	            [Vault_ID][int] NULL,
	            [Name] [varchar] (150) NULL,
	            [Type_Prefix][varchar] (10) NULL,
	            [BMCVariance] [decimal](18, 2) NULL,
	            [VaultVariance] [decimal](18, 2) NULL,
	            [Manufacturer_Name][varchar] (100) NULL,
	            [CanFreeze] [BIT] NULL,
	            IsWebServiceEnabled [BIT],
	            [Capacity] [decimal](18, 2) NULL
	        )
	
	INSERT INTO @temp
	  (
	    [Drop_ID],
	    [OpeningBalance],
	    [FillAmount],
	    [BleedAmount],
	    [AdjustmentAmount],
	    [Meter_Balance],
	    [Vault_Balance],
	    [Declared_Balance],
	    [Declared],
	    [Freezed],
	    [CreatedDate],
	    [CreateUser],
	    [ModifiedDate],
	    [ModifiedUser],
	    [FreezedDate],
	    [FreezeUser],
	    [AuditDate],
	    [AuditUser],
	    [Site_Drop_Ref],
	    [Site_ID],
	    [AuditNote],
	    [UserName],
	    [Vault_ID],
	    [Name],
	    [Type_Prefix],
	    [BMCVariance],
	    [VaultVariance],
	    [Manufacturer_Name],
	    [CanFreeze],
	    IsWebServiceEnabled,
	    [Capacity]
	  )
	SELECT drp.Drop_ID,
	       --drp.Device_ID,
	       drp.OpeningBalance,
	       drp.FillAmount,	       
	       drp.BleedAmount,
	       drp.AdjustmentAmount,
	       drp.Meter_Balance,
	       drp.Vault_Balance,
	       drp.Declared_Balance,
	       drp.IsDeclared AS Declared,
	       ISNULL(drp.IsFrozen, 0) AS Freezed,
	       drp.DropCompleteDate CreatedDate,
	       drp.DropCompleteUser CreateUser,
	       drp.ModifiedDate,
	       drp.ModifiedUser,
	       drp.FrozenDate AS FreezedDate,
	       drp.FrozeUser AS FreezeUser,
	       drp.AuditDate,
	       drp.AuditUser,
	       drp.Site_Drop_Ref,
	       drp.Site_ID,
	       ISNULL(drp.AuditNote, '') AuditNote,
	       ISNULL(usr.Staff_Last_Name, '') + ', ' + ISNULL(usr.Staff_First_Name, '') 
	       UserName,
	       td.Vault_ID,
	       td.[Name],
	       td.Type_Prefix,
	       (drp.Declared_Balance - drp.Meter_Balance) AS BMCVariance,
	       (drp.Declared_Balance - drp.Vault_Balance) AS VaultVariance,
	       m.Manufacturer_Name,
	       CAST(
	           CASE 
	                WHEN @Freeze_Drop_ID = drp.Drop_ID THEN 1
	                ELSE 0
	           END
	           AS BIT
	       )
	       CanFreeze,
	       COALESCE(drp.IsVaultWebServiceEnabled,td.IsWebServiceEnabled,0) IsWebServiceEnabled,
	       td.[Capacity]
	FROM   Manufacturer m
	       INNER JOIN tVault_Devices td WITH (NOLOCK)
	            ON  m.Manufacturer_ID = td.Manufacturer_ID
	       INNER JOIN tVault_Drops drp WITH (NOLOCK)
	            ON  td.Vault_ID = drp.Device_ID
	       INNER JOIN [staff] usr WITH (NOLOCK)
	            ON  drp.CreateUser = usr.UserTableID
	WHERE   drp.Site_ID = @Site_ID
	       AND IsDeclared = 1
	       AND drp.ModifiedDate BETWEEN @StartDate AND @EndDate
	
	IF (@VarianceType = 0)
	BEGIN
	    SELECT [Drop_ID],
	           [OpeningBalance],
	           [FillAmount],
	           [BleedAmount],
	           [AdjustmentAmount],
	           [Meter_Balance],
	           [Vault_Balance],
	           [Declared_Balance],
	           [Declared],
	           [Freezed],
	           [CreatedDate],
	           [CreateUser],
	           [ModifiedDate],
	           [ModifiedUser],
	           [FreezedDate],
	           [FreezeUser],
	           [AuditDate],
	           [AuditUser],
	           [Site_Drop_Ref],
	           [Site_ID],
	           [AuditNote],
	           [UserName],
	           [Vault_ID],
	           [Name],
	           [Type_Prefix],
	           [BMCVariance],
	           [VaultVariance],
	           [Manufacturer_Name],
	           [CanFreeze],
	           IsWebServiceEnabled,
	           [Capacity]
	    FROM   @temp t
	       ORDER BY t.Drop_ID DESC
	END
	ELSE 
	IF (@VarianceType = 1)
	BEGIN
	        SELECT [Drop_ID],
	               [OpeningBalance],
	               [FillAmount],
	               [BleedAmount],
	               [AdjustmentAmount],
	               [Meter_Balance],
	               [Vault_Balance],
	               [Declared_Balance],
	               [Declared],
	               [Freezed],
	               [CreatedDate],
	               [CreateUser],
	               [ModifiedDate],
	               [ModifiedUser],
	               [FreezedDate],
	               [FreezeUser],
	               [AuditDate],
	               [AuditUser],
	               [Site_Drop_Ref],
	               [Site_ID],
	               [AuditNote],
	               [UserName],
	               [Vault_ID],
	               [Name],
	               [Type_Prefix],
	               [BMCVariance],
	               [VaultVariance],
	               [Manufacturer_Name],
	               [CanFreeze],
	               IsWebServiceEnabled,
	               [Capacity]
	        FROM   @temp t
	        WHERE 0<>  --DECIDE VARIANCE BASED ON WEBSERVICE ENABLED
				 CASE WHEN IsWebServiceEnabled=1 THEN VaultVariance
				 ELSE  BMCVariance 
				 END 
			ORDER BY t.Drop_ID DESC
	END
	ELSE 
	IF (@VarianceType = 2)
	BEGIN
	    SELECT [Drop_ID],
	               [OpeningBalance],
	               [FillAmount],
	               [BleedAmount],
	               [AdjustmentAmount],
	               [Meter_Balance],
	               [Vault_Balance],
	               [Declared_Balance],
	               [Declared],
	               [Freezed],
	               [CreatedDate],
	               [CreateUser],
	               [ModifiedDate],
	               [ModifiedUser],
	               [FreezedDate],
	               [FreezeUser],
	               [AuditDate],
	               [AuditUser],
	               [Site_Drop_Ref],
	               [Site_ID],
	               [AuditNote],
	               [UserName],
	               [Vault_ID],
	               [Name],
	               [Type_Prefix],
	               [BMCVariance],
	               [VaultVariance],
	               [Manufacturer_Name],
	               [CanFreeze],
	               IsWebServiceEnabled,
	               [Capacity]
	        FROM   @temp t
	        WHERE 0=  --DECIDE VARIANCE BASED ON WEBSERVICE ENABLED
				 CASE WHEN IsWebServiceEnabled=1 THEN VaultVariance
				 ELSE  BMCVariance 
				 END 
				 --(IsWebServiceEnabled=1 AND VaultVariance=0) OR	 (IsWebServiceEnabled=1 AND BMCVariance=0)
				 ORDER BY t.Drop_ID DESC
	END
	ELSE
	BEGIN
	    SELECT [Drop_ID],
	           [OpeningBalance],
	           [FillAmount],
	           [BleedAmount],
	           [AdjustmentAmount],
	           [Meter_Balance],
	           [Vault_Balance],
	           [Declared_Balance],
	           [Declared],
	           [Freezed],
	           [CreatedDate],
	           [CreateUser],
	           [ModifiedDate],
	           [ModifiedUser],
	           [FreezedDate],
	           [FreezeUser],
	           [AuditDate],
	           [AuditUser],
	           [Site_Drop_Ref],
	           [Site_ID],
	           [AuditNote],
	           [UserName],
	           [Vault_ID],
	           [Name],
	           [Type_Prefix],
	           [BMCVariance],
	           [VaultVariance],
	           [Manufacturer_Name],
	           [CanFreeze],
	           IsWebServiceEnabled,
	           [Capacity]
	    FROM   @temp t
	    ORDER BY t.Drop_ID DESC
	END
END 
GO