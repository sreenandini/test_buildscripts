SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_Vault_GetCassetteDropDetails]')
              AND TYPE IN (N'P', N'PC')
)
    DROP PROCEDURE [dbo].[rsp_Vault_GetCassetteDropDetails]
GO
-- =============================================
-- Author:		<SriHari Jogaraj>
-- Create date: <9th July 2013>
-- Description:	<Get the Vault Details in Enterprise - Vault Declaration Screen>
-- Example EXEC rsp_Vault_GetCassetteDropDetails 0,1
-- =============================================
CREATE PROCEDURE dbo.rsp_Vault_GetCassetteDropDetails
	@Drop_ID BIGINT
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
	SELECT  CASE WHEN tct.CassetteType_Name='Rejection' THEN  c.Cassette_Name + ' (R)'
				ELSE c.Cassette_Name
			END Cassette_Name,
	       c.[Type],
	       c.MaxFillAmount,
	       d.Drop_ID,
	       d.Cassette_ID,
	       d.Denom,
	       d.MeterBalance,
	       d.VaultBalance,
	       d.DeclaredBalance,
	       d.AuditBalance,
	       d.FillAmount,
	       d.BleedAmount,
	       d.AdjustmentAmount,
	       d.dtCreated,
	       d.dtUpdated,
	       d.AudtiDate
	FROM   tVault_CassetteDrops d
	       INNER JOIN tVault_Cassettes c
	            ON  c.Cassette_ID = d.Cassette_ID
	       INNER JOIN tVault_CassetteTypes     tct
	       ON c.Type =tct.CassetteType_ID     
	WHERE  Drop_ID = @Drop_ID 
	ORDER BY c.Type,c.Denom
	
END
GO
