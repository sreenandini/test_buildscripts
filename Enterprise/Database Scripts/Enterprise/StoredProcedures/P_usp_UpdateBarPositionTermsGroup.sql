/************************************************************
 * Code formatted by SoftTree SQL Assistant © v4.8.29
 * Time: 8/13/2014 3:17:39 PM
 ************************************************************/

-- =============================================
-- Author:		Srinivasan
-- Create date:	12-Aug-2014
-- Description:	Procedure to update bar position with terms group details
-- =============================================

USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[usp_UpdateBarPositionTermsGroup]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[usp_UpdateBarPositionTermsGroup]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

   
CREATE PROCEDURE [dbo].[usp_UpdateBarPositionTermsGroup](
    @BarPositionID                   INT,
    @SiteID                          INT,
    @PricePerPlay                    BIT,
    @PricePerPlayValue               INT,
    @Jackpot                         BIT,
    @JackpotValue                    INT,
    @Payout                          BIT,
    @PayoutValue                     INT,
    @AccessKey                       BIT,
    @AccessKeyValue                  INT,
    @TermsGroup                      BIT,
    @TermsGroupId                    INT,
    @TermsGroupPastId                INT,
    @TermsGroupFutureId              INT,
    @TermsGroupChangeOverPastDate    DATETIME,
    @TermsGroupChangeOverFutureDate  DATETIME,
    @Audit_User_ID                   INT,
    @Audit_User_Name                 VARCHAR(50),
    @Audit_ModuleName                VARCHAR(50),
    @Audit_ModuleID                  INT
)
AS
BEGIN
	DECLARE @Audit TABLE(
	            ID INT,
	            FieldName VARCHAR(100),
	            TYPE VARCHAR(200),
	            Cascade_Level_AuditDesc VARCHAR(500),
	            NewValue VARCHAR(100),
	            OldValue VARCHAR(100)
	        )
	
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	IF (@PricePerPlay = 1)
	BEGIN
	    UPDATE BP
	    SET    bp.Bar_Position_Price_Per_Play = @PricePerPlayValue,
	           bp.Bar_Position_Price_Per_Play_Default = 1 
	           OUTPUT @BarPositionID,
	           'Bar_Position_Price_Per_Play',
	           'BARPOSITION',
	           'Position',
	           INSERTED.Bar_Position_Price_Per_Play,
	           DELETED.Bar_Position_Price_Per_Play 
	           INTO @Audit
	    FROM   BAR_POSITION BP
	    WHERE  BP.Bar_Position_ID = @BarPositionID
	           AND BP.Site_ID = @SiteID
	END
	
	IF (@Jackpot = 1)
	BEGIN
	    UPDATE BP
	    SET    bp.Bar_Position_Jackpot = @JackpotValue,
	           bp.Bar_Position_Jackpot_Default = 1 
	           OUTPUT @BarPositionID,
	           'Bar_Position_Jackpot',
	           'BARPOSITION',
	           'Position',
	           INSERTED.Bar_Position_Jackpot,
	           DELETED.Bar_Position_Jackpot 
	           INTO @Audit
	    FROM   BAR_POSITION BP
	    WHERE  BP.Bar_Position_ID = @BarPositionID
	           AND BP.Site_ID = @SiteID
	END
	
	IF (@Payout = 1)
	BEGIN
	    UPDATE BP
	    SET    bp.Bar_Position_Percentage_Payout = @PayoutValue,
	           bp.Bar_Position_Percentage_Payout_Default = 1 
	           OUTPUT @BarPositionID,
	           'Bar_Position_Percentage_Payout',
	           'BARPOSITION',
	           'Position',
	           INSERTED.Bar_Position_Percentage_Payout,
	           DELETED.Bar_Position_Percentage_Payout 
	           INTO @Audit
	    FROM   BAR_POSITION BP
	    WHERE  BP.Bar_Position_ID = @BarPositionID
	           AND BP.Site_ID = @SiteID
	END
	
	
	IF (@AccessKey = 1)
	BEGIN
	    UPDATE BP
	    SET    bp.Access_Key_ID = @AccessKeyValue,
	           bp.Access_Key_ID_Default = 1 
	           OUTPUT @BarPositionID,
	           'Access_Key_ID',
	           'BARPOSITION',
	           'Position',
	           INSERTED.Access_Key_ID,
	           DELETED.Access_Key_ID 
	           INTO @Audit
	    FROM   BAR_POSITION BP
	    WHERE  BP.Bar_Position_ID = @BarPositionID
	           AND BP.Site_ID = @SiteID
	END
	
	IF (@TermsGroup = 1)
	BEGIN
     UPDATE BP  
     SET    bp.Terms_Group_ID = CASE @TermsGroupId when -1 then @TermsGroupId else S.Terms_Group_ID end,  
            bp.Terms_Group_ID_Default = CASE @TermsGroupId when -1 then 0 else 1 end
            OUTPUT @BarPositionID,  
            'Terms_Group_ID',  
            'BARPOSITION',  
            'Position',  
            INSERTED.Terms_Group_ID,  
            DELETED.Terms_Group_ID   
            INTO @Audit  
     FROM   BAR_POSITION BP inner join [Site] S on BP.Site_ID = S.Site_ID
     WHERE  BP.Bar_Position_ID = @BarPositionID  
            AND BP.Site_ID = @SiteID   
            
     UPDATE BP  
     SET    bp.Terms_Group_Future_ID = CASE @TermsGroupFutureId when -1 then @TermsGroupFutureId else S.Terms_Group_ID end
            OUTPUT @BarPositionID,  
            'Terms_Group_Future_ID',  
            'BARPOSITION',  
            'Position',  
            INSERTED.Terms_Group_Future_ID,  
            DELETED.Terms_Group_Future_ID   
            INTO @Audit  
     FROM   BAR_POSITION BP inner join [Site] S on BP.Site_ID = S.Site_ID
     WHERE  BP.Bar_Position_ID = @BarPositionID  
            AND BP.Site_ID = @SiteID  
       
     UPDATE BP  
     SET    bp.Terms_Group_Past_ID = CASE @TermsGroupPastId when -1 then @TermsGroupPastId else S.Terms_Group_ID end 
            OUTPUT @BarPositionID,  
            'Terms_Group_Past_ID',  
            'BARPOSITION',  
            'Position',  
            INSERTED.Terms_Group_Past_ID,  
            DELETED.Terms_Group_Past_ID   
            INTO @Audit  
     FROM   BAR_POSITION BP inner join [Site] S on BP.Site_ID = S.Site_ID
     WHERE  BP.Bar_Position_ID = @BarPositionID  
            AND BP.Site_ID = @SiteID 
       
     UPDATE BP  
     SET    bp.Terms_Group_Past_Changeover_Date = @TermsGroupChangeOverPastDate,
		  bp.Terms_Group_ID_Default = 0
            OUTPUT @BarPositionID,  
            'Terms_Group_Past_Changeover_Date',  
            'BARPOSITION',  
            'Position',  
            INSERTED.Terms_Group_Past_Changeover_Date,  
            DELETED.Terms_Group_Past_Changeover_Date   
            INTO @Audit  
     FROM   BAR_POSITION BP  
     WHERE  BP.Bar_Position_ID = @BarPositionID  
            AND BP.Site_ID = @SiteID  
       
     UPDATE BP  
     SET    bp.Terms_Group_Changeover_Date = @TermsGroupChangeOverFutureDate,
		  bp.Terms_Group_ID_Default = 0  
            OUTPUT @BarPositionID,  
            'Terms_Group_Changeover_Date',  
            'BARPOSITION',  
            'Position',  
            INSERTED.Terms_Group_Changeover_Date,  
            DELETED.Terms_Group_Changeover_Date   
            INTO @Audit  
     FROM   BAR_POSITION BP  
     WHERE  BP.Bar_Position_ID = @BarPositionID  
            AND BP.Site_ID = @SiteID  
	END
	
	
	
	INSERT INTO Audit_History
	  (
	    Audit_Date,
	    Audit_User_ID,
	    Audit_User_Name,
	    Audit_Module_ID,
	    Audit_Module_Name,
	    Audit_Screen_Name,
	    Audit_Desc,
	    Audit_Field,
	    Audit_Old_Vl,
	    Audit_New_Vl,
	    Audit_Operation_Type,
	    Audit_Slot
	  )
	SELECT GETDATE(),
	       @Audit_User_ID,
	       @Audit_User_Name,
	       @Audit_ModuleID,
	       @Audit_ModuleName,
	       Cascade_Level_AuditDesc,
	       'Updated ' + [TYPE] + ' (' + CAST(id AS VARCHAR(20)) + ') ..[' +
	       [FieldName] 
	       + ']: ' + COALESCE(OldValue, '') + ' --> ' + COALESCE(NewValue, '') AS 
	       DESC1,
	       FieldName,
	       OldValue,
	       NewValue,
	       'MODIFY',
	       ''
	FROM   @Audit
END