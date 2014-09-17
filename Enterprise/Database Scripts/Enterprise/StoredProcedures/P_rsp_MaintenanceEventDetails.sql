USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_MaintenanceEventDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_MaintenanceEventDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE rsp_MaintenanceEventDetails (@SessionID INT, @SiteID INT)
AS
BEGIN
--      DECLARE @EventID INT  
--   DECLARE @TimeStamp DateTime  
--      DECLARE @ID INT     
--      DECLARE @AssociatedEvent INT  
--  
--      SELECT   
--            tMH.*,   
--            tD.AssociatedEvent AS AssociatedEvent  
--            INTO #MHTemp   
--      FROM MaintenanceSession tMS  
--      INNER JOIN MaintenanceHistory tMH   
--            ON tMH.SessionID = tMS.ID AND tMH.SITE_ID = @SiteID  
--      INNER JOIN DoorEvent_lkp tD   
--            ON tD.EventID = tMH.EventID  
--      WHERE  
--            tMS.ID = @SessionID  
--   AND tMS.Site_ID = @SiteID  
--            AND tMS.IsSessionOpen = 0   
--      ORDER BY [TimeStamp]  
--  
--  
--      -- SELECT *, 1 AS Duration, 0 AS COMPLETED INTO MaintenanceHistory_Temp FROM #MHTemp WHERE 1 <> 1 ORDER BY [TimeStamp]  
--  
-- IF EXISTS (SELECT * FROM SYS.Objects Where Name like 'MaintenanceHistory_Temp' And Type = 'u')  
-- BEGIN  
--  DROP TABLE MaintenanceHistory_Temp  
-- END  
--  
-- CREATE TABLE MaintenanceHistory_Temp  
-- (  
--  ID INT,  
--  SessionID INT ,  
--  EventID INT ,  
--  [TimeStamp] DateTime,  
--  CoinIn INT,  
--  CoinOut INT,  
--  Bill100 INT,  
--  Bill50 INT,  
--  Bill20 INT,  
--  Bill10 INT,  
--  Bill5 INT,  
--  Bill1 INT,  
--  TrueCoinIn INT,  
--  TrueCoinOut INT,  
--  [Drop] INT,  
--  Jackpot INT,  
--  CancelledCredits INT,  
--  HandPaidCancelledCredits INT,  
--  CashableTicketIn INT,  
--  CashableTicketOut INT,  
--  CashableTicketInQty INT,  
--  CashableTicketOutQty INT,  
--  ProgressiveHandPay INT,  
--  Site_ID INT,  
--  AssociatedEvent INT,  
--  Duration INT Default 1,  
--  COMPLETED Bit Default 0  
-- )  
--  
--      DECLARE MH_cursor CURSOR   
--      FOR SELECT ID, EventID, AssociatedEvent, [TimeStamp] FROM #MHTemp ORDER BY [TimeStamp]  
--      OPEN MH_Cursor  
--  
--      FETCH NEXT FROM MH_Cursor INTO @ID, @EventID, @AssociatedEvent, @TimeStamp  
--      WHILE @@FETCH_STATUS = 0  
--      BEGIN  
--  
--            IF (@EventID <> @AssociatedEvent)   -- NON POWER OFF Events  
--            BEGIN  
--                  IF EXISTS (SELECT * FROM MaintenanceHistory_Temp WHERE EventID = @AssociatedEvent)  
--                  BEGIN   
--      UPDATE MaintenanceHistory_Temp SET Duration = DATEDIFF(ss, [TimeStamp], @TimeStamp), COMPLETED = 1 WHERE EventID = @AssociatedEvent AND COMPLETED = 0  
--                  END  
--                  ELSE  
--                  BEGIN  
--                        IF (@EventID IN (17,11,4,15) AND (EXISTS (SELECT * FROM MaintenanceHistory_Temp WHERE EventID IN (200, 201, 202, 203))))  
--                        BEGIN  
--                              IF (@EventID = 17)                                      
--         UPDATE MaintenanceHistory_Temp SET Duration = DATEDIFF(ss, [TimeStamp], @TimeStamp), COMPLETED = 1 WHERE EventID = 200 AND COMPLETED = 0  
--                              IF (@EventID = 11)  
--         UPDATE MaintenanceHistory_Temp SET Duration = DATEDIFF(ss, [TimeStamp], @TimeStamp), COMPLETED = 1 WHERE EventID = 201 AND COMPLETED = 0  
--                              IF (@EventID = 4)  
--         UPDATE MaintenanceHistory_Temp SET Duration = DATEDIFF(ss, [TimeStamp], @TimeStamp), COMPLETED = 1 WHERE EventID = 202 AND COMPLETED = 0                                      
--                              IF (@EventID = 15)  
--         UPDATE MaintenanceHistory_Temp SET Duration = DATEDIFF(ss, [TimeStamp], @TimeStamp), COMPLETED = 1 WHERE EventID = 203 AND COMPLETED = 0                                      
--                        END  
--                        ELSE  
--                        BEGIN  
--  
--                              INSERT INTO MaintenanceHistory_Temp SELECT *, 0 ,0 FROM #MHTemp WHERE ID = @ID  
--                        END  
--                  END  
--   
--            END  
--            ELSE                                            -- POWER OFF Events  
--            BEGIN  
--   
--                  IF (@EventID IN (200,201,202,203) AND (EXISTS (SELECT * FROM MaintenanceHistory_Temp WHERE EventID IN (16, 10, 3, 14))))  
--                  BEGIN  
--                        IF (@EventID = 200)  
--       UPDATE MaintenanceHistory_Temp SET Duration = DATEDIFF(ss, [TimeStamp], @TimeStamp), COMPLETED = 1 WHERE EventID = 16 AND COMPLETED = 0                              
--                        IF (@EventID = 201)                                                       
--       UPDATE MaintenanceHistory_Temp SET Duration = DATEDIFF(ss, [TimeStamp], @TimeStamp), COMPLETED = 1 WHERE EventID = 10 AND COMPLETED = 0  
--                        IF (@EventID = 202)  
--       UPDATE MaintenanceHistory_Temp SET Duration = DATEDIFF(ss, [TimeStamp], @TimeStamp), COMPLETED = 1 WHERE EventID = 3 AND COMPLETED = 0                              
--                        IF (@EventID = 203)  
--       UPDATE MaintenanceHistory_Temp SET Duration = DATEDIFF(ss, [TimeStamp], @TimeStamp), COMPLETED = 1 WHERE EventID = 14 AND COMPLETED = 0  
--                              
--                  END  
--                  ELSE  
--                  BEGIN  
--                        INSERT INTO MaintenanceHistory_Temp SELECT *, 0, 0 FROM #MHTemp WHERE ID = @ID  
--                  END  
--            END  
--            FETCH NEXT FROM MH_Cursor INTO @ID, @EventID, @AssociatedEvent, @TimeStamp  
--      END  
--  
      SELECT tD.EventDesc, tMH.[TimeStamp],0 AS  Duration, 'Details'     = Convert (Varchar(50),tD.EventDesc + CASE OpenStatus WHEN NULL THEN '' ELSE CASE OpenStatus WHEN 1 THEN ' Open' ELSE ' Closed' END END ) FROM MaintenanceHistory tMH  
   INNER JOIN DoorEvent_lkp tD On (tD.EventID = tMH.EventID and tD.EventID <> -1)  WHERE tMH.SessionID = @SessionID AND tMH.Site_ID = @SiteID 
     

   

--
--   IF EXISTS (SELECT * FROM SYS.Objects Where Name like 'MaintenanceHistory_Temp' And Type = 'u')  
--   BEGIN  
--    DROP TABLE MaintenanceHistory_Temp  
--   END   
--  
--      CLOSE MH_Cursor  
--      DEALLOCATE MH_Cursor  
END


GO

