USE Enterprise
GO 
-- Drop stored procedure if it already exists
IF EXISTS (
       SELECT *
       FROM   INFORMATION_SCHEMA.ROUTINES
       WHERE  SPECIFIC_SCHEMA = N'dbo'
              AND SPECIFIC_NAME = N'usp_Vault_ImportEvent'
   )
    DROP PROCEDURE dbo.usp_Vault_ImportEvent
GO
 --
CREATE PROCEDURE dbo.usp_Vault_ImportEvent    
 @xml XML,    
 @IsSuccess INT OUTPUT    
AS    
 /*****************************************************************************************************    
DESCRIPTION : PROC Description      
CREATED DATE: PROC CreateDate    
MODULE  : PROC used in Modules     
CHANGE HISTORY :    
------------------------------------------------------------------------------------------------------    
AUTHOR     DESCRIPTON          MODIFIED DATE    
------------------------------------------------------------------------------------------------------    
usp_Vault_ImportEvent 1    
*****************************************************************************************************/    
    
    
BEGIN    
     
 SET NOCOUNT ON     
     
 DECLARE @docHandle INT   
 SET @IsSuccess = 0        
 EXEC sp_xml_preparedocument @docHandle OUTPUT,    
      @Xml       
     
     
     
 DECLARE @tempEvent             TABLE    
         (    
             [EventID] [int],    
             [EventDescription] [varchar](200),    
             [DeviceID] [varchar](50),    
             [SiteID] [varchar](50),    
             [EventDateTime] [datetime],    
             CreateDate [datetime],    
             Site_Drop_ref BIGINT,    
             Site_Event_ID BIGINT,
             Vault_Id INT     
         )    
     
 DECLARE @tempCassette          TABLE    
         (    
             Site_Cassette_id BIGINT,    
             CassetteNumber VARCHAR(100),    
             CassettLevel INT,    
             CassettDenom INT,    
             [Description] VARCHAR(200),    
             VaultEventid BIGINT    
         )    
     
 DECLARE @tempExistingCassette  TABLE    
         (Cassette_id BIGINT, Site_Cassette_id BIGINT)    
     
 --Parse event     
 INSERT INTO @tempEvent    
   (    
     [EventID],    
     [EventDescription],    
     [DeviceID],    
     [EventDateTime],    
     [CreateDate],    
     Site_Drop_ref,    
     Site_Event_ID,    
     [SiteID],
     Vault_Id     
   )    
 SELECT temp.EventID,    
        temp.EventDescription,    
        temp.HQ_Vault_ID,    
        temp.EventDateTime,    
        temp.CreateDate,    
        temp.Site_Drop_ref,    
        temp.Site_Event_ID,    
        s.Site_ID,
        temp.Vault_Id    
 FROM   OPENXML(@dochandle, 'VaultEvent/Event', 2)     
        WITH     
        (    
            EventID [int] 'EventID',    
            EventDescription [varchar](200) 'EventDescription',    
            HQ_Vault_ID [varchar](50) 'HQ_Vault_ID',    
            EventDateTime [datetime] 'EventDateTime',    
            CreateDate [datetime] 'CreateDate',    
            Site_Drop_ref BIGINT 'Drop_Id',    
            Site_Event_ID BIGINT 'VaultEventid',    
            SiteCode VARCHAR(10) 'SiteCode',   
            Vault_Id [varchar](50) 'Vault_Id'
        ) temp    
        INNER JOIN dbo.[Site] s    
             ON  s.site_code = temp.SiteCode    
     
 --Parse cassette into a temp table     
     
 INSERT INTO @tempCassette    
   (    
     Site_Cassette_id,    
     CassetteNumber,    
     CassettLevel,    
     CassettDenom,    
     [Description]    
   )    
 SELECT Site_Cassette_id,    
        CassetteNumber,    
        CassettLevel,    
        CassettDenom,    
        [Description]    
 FROM   OPENXML(@dochandle, 'VaultEvent/Cassettes/Cassette', 2)     
        WITH     
        (    
            Site_Cassette_id BIGINT 'Cassette_id',    
            CassetteNumber VARCHAR(100) 'CassetteNumber',    
            CassettLevel INT 'CassettLevel',    
            CassettDenom INT 'CassettDenom',    
            [Description] VARCHAR(200) 'Description'    
        ) temp    
     
     
     
     
     
 --tVault_TransactionEvents    
 EXEC sp_xml_removedocument @dochandle    
     
 DECLARE @Event_ID BIGINT    
     
 
 BEGIN TRAN     
    --==============INSERT OR UPDATE EVENT =======    
    
     INSERT INTO vaultevents
       (
         EventID,
         EventDescription,
         DeviceID,
         SiteID,
         EventDateTime,
         CreateDate,
         Site_Drop_ref,
         Site_Event_ID,
         Vault_Id
       )
     SELECT EventID,
            [EventDescription],
            [DeviceID],
            [SiteID],
            [EventDateTime],
            CreateDate,
            Site_Drop_ref,
            Site_Event_ID,
            isnull(Vault_Id,
             (  -- This is to handle EP4 vault events. 
				SELECT TOP 1 vault_id
				FROM   tVault_Devices tx   WITH(NOLOCK)
					   INNER JOIN tNGADevices td   WITH(NOLOCK)
							ON  tx.NGADevice_ID = td.NGADevice_ID
					   INNER JOIN tNGAInstallations tn   WITH(NOLOCK)
						ON  tn.NGADevice_ID=td.ngaDevice_ID 
				WHERE tn.Site_ID =t.[SiteID]
				ORDER BY tn.Start_Date DESC  ))
     FROM   @tempEvent t     
     
     SET @Event_ID = SCOPE_IDENTITY()    
     
     IF @@ERROR <> 0
         GOTO Err 
	--==============INSERT/UPDATE  Cassette details =======--    
     INSERT INTO cassettedetails
       (
         Site_Cassette_id,
         CassetteNumber,
         CassettLevel,
         CassettDenom,
         [Description],
         VaultEventid
       )
     SELECT Site_Cassette_id,
            CassetteNumber,
            CassettLevel,
            CassettDenom,
            [Description],
            @Event_ID
     FROM   @tempCassette    
     
     IF @@ERROR <> 0
         GOTO Err 
         
 COMMIT TRAN    
 RETURN    
     
 --ON ERROR ROLLBACK ALL      
 Err:   
 IF @@Error<>0    
    BEGIN    
        SET @IsSuccess = -1 --Failed to Update
    END    
 ROLLBACK    
     
END 
GO
