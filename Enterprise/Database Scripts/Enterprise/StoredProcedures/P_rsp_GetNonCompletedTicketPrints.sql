USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetNonCompletedTicketPrints]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetNonCompletedTicketPrints]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/***
Version History
---------------------------------------
Kirubakar	Created		03 Jun 2010 20:39:48
---------------------------------------
***/
  
CREATE PROCEDURE rsp_GetNonCompletedTicketPrints      
   @StartDate datetime = '',    
   @EndDate   datetime = '',  
   @Site int    
      
AS      
    
    
 SELECT TE_TicketNumber,      
         TE_Installation_No,      
         TE_Date,      
         TE_Value,      
         Bar_position_name,      
         Machine_Stock_No,    
   TE_Status_Create_Expected as CreateExpected,    
   TE_Status_Create_Actual as CreateActual,    
   CASE WHEN ISNULL(Machine_Class.Validation_Length, 0) > 0 THEN    
    RIGHT(LTRIM(RTRIM(TE_TicketNumber)), ISNULL(Machine_Class.Validation_Length, 0))    
   ELSE    
   TE_TicketNumber END AS ActualBarCode    
      
    FROM ticket_Exception        
      
    JOIN Installation      
      ON Installation.Installation_ID = Ticket_Exception.TE_Installation_No      
      
   JOIN Bar_Position      
     ON Installation.Bar_Position_ID = Bar_Position.Bar_Position_ID  AND Bar_Position.Site_ID= @Site  
       
   JOIN Machine      
     ON Installation.Machine_ID = Machine.Machine_ID      
    
   JOIN Machine_Class    
  ON Machine.Machine_Class_ID = Machine_Class.Machine_Class_ID    
      
  WHERE TE_Status = 'N'      
  AND    
  (     
           ( @StartDate <> ''  -- claimed    
              and    
                te_date BETWEEN @StartDate AND @EndDate    
   )    
                 
   )    
      
 ORDER BY TE_Date DESC      

GO

