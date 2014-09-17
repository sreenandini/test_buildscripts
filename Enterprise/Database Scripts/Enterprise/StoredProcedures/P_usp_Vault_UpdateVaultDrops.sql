/************************************************************
 * Code formatted by SoftTree SQL Assistant © v6.3.171
 * Time: 10/17/2013 3:06:19 PM
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
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[usp_Vault_UpdateVaultDrops]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[usp_Vault_UpdateVaultDrops]
GO
-- =============================================
-- Author:		<SriHari Jogaraj>
-- Create date: <9th July 2013>
-- Description:	<Get the Site Details in Enterprise - Vault Declaration Screen>
-- ============================================= 
CREATE PROCEDURE dbo.usp_Vault_UpdateVaultDrops
	@DeclaredBalance DECIMAL(18, 2),
	@Declared BIT,
	@DropID BIGINT,
	@SiteCode VARCHAR(50),
	@UserId INT,
	@CassetteXML XML = NULL
AS
	/*****************************************************************************************************  
DESCRIPTION  : Update declared drops in Vault  
CREATED DATE : 12th July 2013  
MODULE   : BMC Enterprise Client Vault Declaration  
CHANGE HISTORY :  
------------------------------------------------------------------------------------------------------  
AUTHOR                              DESCRIPTON                                                        MODIFIED DATE  
------------------------------------------------------------------------------------------------------  

*****************************************************************************************************/  

BEGIN
	SET NOCOUNT ON
	
	DECLARE @bStatus BIT
	DECLARE @iDocHandle INT 
	
	SELECT @SiteCode=S.site_code 
	FROM site s  WITH(NOLOCK)
	Where S.site_id=@SiteCode 
	
	IF (ISNULL(cast(@CassetteXML AS VARCHAR(MAX)), '') <> '')
	BEGIN
	    EXEC sp_xml_preparedocument @iDocHandle OUTPUT,
	         @CassetteXML
	    
	    UPDATE TC
	    SET    TC.DeclaredBalance = A.DeclaredBalance,
	           TC.dtUpdated = GETDATE()
	    FROM   OPENXML(@iDocHandle, './CassetteDetails/Cassette', 2) WITH 
	           (
	               Cassette_ID INT '@Cassette_ID',
	               Denom FLOAT '@Denom',
	               DeclaredBalance DECIMAL(18, 2) '@FillAmount'
	           ) AS A
	           INNER JOIN tVault_CassetteDrops TC
	                ON  TC.Cassette_ID = A.Cassette_ID
	                AND TC.Drop_ID = @DropID
	    
	    EXEC sp_xml_RemoveDocument @iDocHandle
	END  
	
	
	
	UPDATE tVault_Drops
	SET    Declared_Balance = @DeclaredBalance,
	       IsDeclared = @Declared,
	       ModifiedUser = @UserId,
	       ModifiedDate = GETDATE(),
	       @bStatus = 1
	WHERE  Drop_ID = @DropID
	       AND IsDeclared = 0 
	
	IF ISNULL(@bStatus, 0) = 0
	BEGIN
	    RETURN -2
	END 
	
	IF @@ERROR = 0
	BEGIN
	    INSERT INTO Export_History
	    VALUES
	      (
	        GETDATE(),
	        @DropID,
	        'VAULTDROP',
	        NULL,
	        NULL,
	        @SiteCode
	      )
	END
END
GO