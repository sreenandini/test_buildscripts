SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_Report_GetDeclaredVaultCassettes]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_Report_GetDeclaredVaultCassettes]
GO
--EXEC rsp_Report_GetDeclaredVaultCassettes 0,2,'2013-12-01 19:55:01','2014-01-30 16:12:26.767',0
CREATE PROCEDURE rsp_Report_GetDeclaredVaultCassettes
	@Vault_ID INT,
	@Site_ID INT,
	@StartDate DATETIME,
	@EndDate DATETIME,
	@VarianceType INT = 0 /*if @VarianceType=0 then all data,if '1' then with varaince,if '2' then zero varaince */
AS
BEGIN
	DECLARE @temp TABLE
	        (
	            [Drop_ID] [bigint] NOT NULL,
	            [Vault_ID][int] NULL,
	            [Name] [varchar] (150) NULL,
	            [Type_Prefix][varchar] (10) NULL,
	            [CreatedDate] [datetime] NULL,
	            [ModifiedDate] [datetime] NULL,
	            [CreatedUser] [int] NULL,
	            [ModifiedUser] [int] NULL,
	            [CassetteType] [int] NULL,
	            [Cassette_Name] [varchar] (150),
	            [Denom] [float] NULL,
	            [FillAmount] [decimal](18, 2) NULL,
	            [BleedAmount] [decimal](18, 2) NULL,
	            [AdjustmentAmount] [decimal](18, 2) NULL,
	            [VaultBalance] [decimal](18, 2) NULL,
	            [DeclaredBalance] [decimal](18, 2) NULL,
	            [VaultVariance] [decimal](18, 2) NULL
	        )
	
	INSERT INTO @temp
	  (
	    [Drop_ID],
	    [Vault_ID],
	    [Name],
	    [Type_Prefix],
	    [CreatedDate],
	    [ModifiedDate],
	    [CreatedUser],
	    [ModifiedUser],
	    [CassetteType],
	    [Cassette_Name],
	    [Denom],
	    [FillAmount],
	    [BleedAmount],
	    [AdjustmentAmount],
	    [VaultBalance],
	    [DeclaredBalance],
	    [VaultVariance]
	  )
	SELECT drp.Drop_ID,
	       td.Vault_ID,
	       td.Name,
	       td.Type_Prefix,
	       drp.CreatedDate,
	       drp.ModifiedDate,
	       drp.DropCompleteUser,
	       drp.ModifiedUser,
	       tvc.[Type],
	       CASE 
				WHEN  tct.CassetteType_Name ='Rejection' THEN tvc.Cassette_Name + ' (R)'
				ELSE  tvc.Cassette_Name 
		   END AS Cassette_Name,
	       tvcd.Denom,
	       tvcd.FillAmount,
	       tvcd.BleedAmount,
	       tvcd.AdjustmentAmount,
	       tvcd.VaultBalance,
	       tvcd.DeclaredBalance,
	       (tvcd.DeclaredBalance - tvcd.VaultBalance) AS VaultVariance
	FROM   Manufacturer m
	       INNER JOIN tVault_Devices td WITH (NOLOCK)
	            ON  m.Manufacturer_ID = td.Manufacturer_ID
	       INNER JOIN tVault_Drops drp WITH (NOLOCK)
	            ON  td.Vault_ID = drp.Device_ID
	       INNER JOIN tVault_CassetteDrops tvcd WITH (NOLOCK)
	            ON  tvcd.Drop_ID = drp.Drop_ID
	       INNER JOIN tVault_Cassettes tvc
	            ON  tvc.Cassette_ID = tvcd.Cassette_ID
	       INNER JOIN [staff] usr WITH (NOLOCK)
	            ON  drp.CreateUser = usr.UserTableID
		   INNER JOIN tVault_CassetteTypes tct WITH (NOLOCK)
				ON tvc.[Type]=tct.CassetteType_ID	
	WHERE  drp.Site_ID = @Site_ID
	       AND IsDeclared = 1
	       AND drp.ModifiedDate BETWEEN @StartDate AND @EndDate
	       AND COALESCE(drp.IsVaultWebServiceEnabled, td.IsWebServiceEnabled, 0) = 1
	
	
	IF (@VarianceType = 0)
	BEGIN
	    SELECT [Drop_ID],
	           [Vault_ID],
	           [Name],
	           [Type_Prefix],
	           [CreatedDate],
	           [ModifiedDate],
	           [CreatedUser],
	           [ModifiedUser],
	           [CassetteType],
	           [Cassette_Name],
	           [FillAmount],
	           [BleedAmount],
	           [AdjustmentAmount],
	           [VaultBalance],
	           [DeclaredBalance],
	           [VaultVariance]
	    FROM   @temp t
	    ORDER BY
	           t.[Drop_ID] DESC,
	           t.[CassetteType] ASC,
	           t.Denom
	END
	ELSE 
	IF (@VarianceType = 1)
	BEGIN
	    SELECT [Drop_ID],
	           [Vault_ID],
	           [Name],
	           [Type_Prefix],
	           [CreatedDate],
	           [ModifiedDate],
	           [CreatedUser],
	           [ModifiedUser],
	           [CassetteType],
	           [Cassette_Name],
	           [FillAmount],
	           [BleedAmount],
	           [AdjustmentAmount],
	           [VaultBalance],
	           [DeclaredBalance],
	           [VaultVariance]
	    FROM   @temp t
	    WHERE  [VaultVariance] <> 0
	    ORDER BY
	           t.[Drop_ID] DESC,
	           t.[CassetteType] ASC,
	           t.Denom
	END
	ELSE
	    --[Without Variance] Final Condition of @Variance = 2 not checked since fields to show in report on design
	BEGIN
	    SELECT [Drop_ID],
	           [Vault_ID],
	           [Name],
	           [Type_Prefix],
	           [CreatedDate],
	           [ModifiedDate],
	           [CreatedUser],
	           [ModifiedUser],
	           [CassetteType],
	           [Cassette_Name],
	           [FillAmount],
	           [BleedAmount],
	           [AdjustmentAmount],
	           [VaultBalance],
	           [DeclaredBalance],
	           [VaultVariance]
	    FROM   @temp t
	    WHERE  [VaultVariance] = 0
	    ORDER BY
	           t.[Drop_ID] DESC,
	           t.[CassetteType] ASC,
	           t.Denom
	END
END