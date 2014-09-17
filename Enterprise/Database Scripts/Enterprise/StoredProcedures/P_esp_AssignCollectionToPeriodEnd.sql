USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[esp_AssignCollectionToPeriodEnd]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[esp_AssignCollectionToPeriodEnd]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO

-------------------------------------------------------------------------- 
---
--- Description: Auto assign collections to a current period end
---  
---              If period end is not assigned we ALTER  one, using latest calendar period end
---
--- Inputs:      see inputs
---
--- Outputs:     (0)   - no error .. 
---              OTHER - SQL error 
--- 
--- =======================================================================
--- 
--- Revision History
--- 
--- C.Taylor   30/06/08     Created 
--- C.Taylor   30/07/08     not updating period end id in collection_details correctly
--- C.Taylor   31/07/08     not picking calendar id if on month boundry 31/jul/2008 
--- C.Taylor   06/08/08     system was not updating collection_details.period_end_id
--- C.Taylor   20/10/08     when assigning the period end, 
---                              firstly check for an open period for the date it was collected
---                              if this has been closed, then insert into the next available one.
---                              if non available, then create the next new one
--------------------------------------------------------------------------- 
CREATE PROCEDURE [dbo].[esp_AssignCollectionToPeriodEnd]

  @Collection_ID   INT,
  @Installation_ID INT
  
AS

  DECLARE @CollDate          DATETIME,
          @SubCompID         INT,
          @Period_End_Date   DATETIME,
          @Period_Start_Date DATETIME,
          @Period_ID         INT

  -- get date of collection
  SELECT @CollDate = Collection_Date_Of_Collection
    FROM Collection
   WHERE COllection_ID = @Collection_ID

  -- get sub company from installation.
  SELECT @SubCompID = Sub_Company_ID
    FROM Bar_Position bp
    JOIN Installation i
      ON bp.Bar_Position_ID = I.Bar_Position_ID
    JOIN Site s
      ON s.Site_ID = bp.Site_ID
   WHERE i.Installation_ID = @Installation_ID

  -- get a matching calendar_period_end_date, to mark as the end of period marker
  SELECT @Period_Start_Date = CONVERT(DATETIME, Calendar_Period_Start_Date, 103),
         @Period_End_Date = CONVERT(DATETIME, Calendar_Period_End_Date, 103)
    FROM Calendar_Period cp
    JOIN Calendar c
      ON c.Calendar_ID = cp.Calendar_ID
    JOIN Sub_Company_Calendar scc
      ON scc.Calendar_ID = c.Calendar_ID 
   WHERE Sub_Company_ID = @SubCompID
     AND Sub_Company_Calendar_Active = 1
     AND @CollDate BETWEEN CONVERT(DATETIME, Calendar_Period_Start_Date, 103) 
	               AND CONVERT(DATETIME, Calendar_Period_End_Date, 103)
     
  -- does period end exist for @CollDate
  declare @Statement_No int
  
  SELECT @Period_ID = Period_End_ID,
         @Statement_No = Statement_No
    FROM Period_End
   WHERE CAST(Period_End_Final_Date AS DATE) = CAST(@Period_End_Date AS DATE)
     AND Sub_Company_ID = @SubCompID
  

  IF COALESCE(@Statement_No,0) <> 0
  BEGIN
    -- period has already been confirmed, so get the date for current
  
    -- use today as the date to assign the period to and not the date of collection
    select @Period_Start_Date = null, 
           @Period_End_Date = null, 
           @Period_ID = null,
           @CollDate = GETDATE()  

    select @Period_ID = min(period_end_id) 
      from period_end
     where sub_company_id = @SubCompID
       and coalesce(statement_no,0) = 0 

    if ( @period_id is null ) 
    BEGIN
      -- create a period to house the collection   
      print 'need to get the next period available ..'
   
      -- get a matching calendar_period_end_date, to mark as the end of period marker
      SELECT @Period_Start_Date = Calendar_Period_Start_Date,
             @Period_End_Date = Calendar_Period_End_Date
        FROM Calendar_Period cp
        JOIN Calendar c
          ON c.Calendar_ID = cp.Calendar_ID
        JOIN Sub_Company_Calendar scc
          ON scc.Calendar_ID = c.Calendar_ID 
       WHERE Sub_Company_ID = @SubCompID
         AND Sub_Company_Calendar_Active = 1
         AND @CollDate BETWEEN CONVERT(DATETIME, Calendar_Period_Start_Date, 103) 
	               AND CONVERT(DATETIME, Calendar_Period_End_Date, 103)
	              
      -- does period end exist for @CollDate  
      SELECT @Period_ID = Period_End_ID
        FROM Period_End
       WHERE Period_End_Final_Date = @Period_End_Date
         AND Sub_Company_ID = @SubCompID
    END
  END


  -- debug.
  select @Period_ID, @Period_End_Date

  -- if period id is zero, and we have a period end date create one
  IF ( COALESCE ( @Period_ID, 0 ) = 0 ) AND ( @Period_End_Date IS NOT NULL ) 
  BEGIN
    print 'Creating period end for sub company'

    -- no, create one
    INSERT INTO Period_End 
          ( 
            Sub_Company_ID,
            Period_End_Setup_Date,
            Period_End_Final_Date           
          )
    VALUES
          (
            @SubCompID, 
            @Period_Start_Date,
            @Period_End_Date
          )

    SELECT @Period_ID = SCOPE_IDENTITY()
  END  

  -- if @Period_ID is not zero, then set the period id within the collection
  IF ( COALESCE ( @Period_ID, 0 ) <> 0 ) 
  BEGIN
    UPDATE Collection_Details
       SET Period_End_ID = @Period_ID
     WHERE Collection_ID = @Collection_ID
       AND COALESCE( Period_End_ID, 0 ) = 0
  END


GO

