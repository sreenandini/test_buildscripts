/************************************************************
 * Code formatted by SoftTree SQL Assistant © v4.8.29
 * Time: 26/07/13 11:49:28 AM
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
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[usp_Vault_UpdateAuditData]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[usp_Vault_UpdateAuditData]
GO
-- =============================================
-- Author:		<SriHari Jogaraj>
-- Create date: <22nd July 2013>
-- Description:	<Get the Site Details in Enterprise - Vault Declaration Screen>
-- ============================================= 
CREATE PROCEDURE dbo.usp_Vault_UpdateAuditData
	@DropID BIGINT,
	@BmcTotal DECIMAL(18, 2),
	@VaultTotal DECIMAL(18, 2),
	@DeclaredBalance DECIMAL(18, 2),
	@AuditNotes VARCHAR(500),
	@Freezed BIT,
	@UserId INT,
	@SiteCode VARCHAR(10),
	@FreezePrevious BIT,
	@CassetteDetails VARCHAR(MAX) = NULL
AS
	/*****************************************************************************************************  
DESCRIPTION  : Update declared drops in Vault  
CREATED DATE : 12th July 2013  
MODULE   : BMC Enterprise Client Vault Declaration  
CHANGE HISTORY :  
------------------------------------------------------------------------------------------------------  
AUTHOR                              DESCRIPTON                                                        MODIFIED DATE  
------------------------------------------------------------------------------------------------------  
<CasssetteDetails>
<Cassette Id="2" Amount="100.00"/>
<Cassette Id="2" Amount="100.00"/>
</CasssetteDetails>
*****************************************************************************************************/  

BEGIN
	SET NOCOUNT ON
	DECLARE @bStatus     BIT
	DECLARE @Site_ID     INT
	DECLARE @iDocHandle  INT 
	DECLARE @Result      INT 
	
	SET @Result = -1
	
	SELECT @Site_ID = site_id
	FROM   SITE WITH(NOLOCK)
	WHERE  site_code = @SiteCode
	
	
	
	BEGIN TRANSACTION	
	IF (@Freezed = 1 AND @FreezePrevious = 0)
	BEGIN
	    UPDATE tVault_Drops
	    SET    IsFrozen = @Freezed,
	           FrozenDate = GETDATE(),
	           FrozeUser = @UserId,
	           AuditNote = ISNULL(AuditNote, @AuditNotes),
	           AuditDate = ISNULL(AuditDate, GETDATE()),
	           AuditUser = ISNULL(AuditUser, @UserId)
	    WHERE  Drop_ID < @DropID
	           AND IsFrozen <> 1
	           AND site_id = @Site_ID
	    
	    IF @@ERROR <> 0
	        GOTO ERR_HANDLE
	END
	
	
	IF ISNULL(@CassetteDetails, '') <> ''
	BEGIN
	    EXEC sp_xml_preparedocument @iDocHandle OUTPUT,
	         @CassetteDetails
	    
	    UPDATE td
	    SET    td.DeclaredBalance = A.amount
	    FROM   OPENXML(@iDocHandle, './CasssetteDetails/Cassette', 2) WITH 
	           (Cassette_ID INT '@Id', Amount DECIMAL(18, 2) '@Amount') AS A
	           INNER JOIN tvault_cassettedrops td
	                ON  td.Cassette_ID = A.Cassette_ID
	                AND td.Drop_ID = @DropID
	    
	    EXEC sp_xml_RemoveDocument @iDocHandle
	    
	    IF @@ERROR <> 0
	        GOTO ERR_HANDLE
	END
	
	UPDATE tVault_Drops
	SET    Meter_Balance = @BmcTotal,
	       Vault_Balance = @VaultTotal,
	       Declared_Balance = @DeclaredBalance,
	       AuditNote = @AuditNotes,
	       IsFrozen = @Freezed,
	       @bStatus = 1,
	       AuditDate = GETDATE(),
	       AuditUser = @UserId,
	       FrozenDate = CASE 
	                         WHEN @Freezed = 1 THEN GETDATE()
	                         ELSE NULL
	                    END,
	       FrozeUser = CASE 
	                        WHEN @Freezed = 1 THEN @UserId
	                        ELSE NULL
	                   END	
	WHERE  Drop_ID = @DropID
	       AND IsFrozen = 0
	
	IF ISNULL(@bStatus, 0) = 0
	BEGIN
	    SET @Result = -2
	    GOTO ERR_HANDLE
	END 
	
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
	
	COMMIT TRANSACTION
	RETURN 0
	
	ERR_HANDLE:
	
	ROLLBACK TRANSACTION
	RETURN @Result
END
GO