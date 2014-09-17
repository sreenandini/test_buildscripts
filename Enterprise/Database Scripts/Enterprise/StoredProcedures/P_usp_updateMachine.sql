USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_updateMachine]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_updateMachine]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*
Inserted Column For Collection Functionality in Enterprise
*/

 CREATE PROCEDURE [dbo].[usp_updateMachine]        
 @doc XML      
AS        
        
BEGIN        
        
DECLARE @idoc INT        
DECLARE @Type_ID INT        
DECLARE @Man_ID INT        
         
CREATE TABLE #Temp        
 (        
  [Stock_No] VARCHAR(100),        
  IsAFTEnabled BIT,
  Machine_Uses_Meters BIT     
 )        
        
 EXEC SP_XML_PREPAREDOCUMENT @idoc OUTPUT, @doc        
        
 INSERT INTO #Temp        
 SELECT * FROM OPENXML(@idoc, './MACHINE/Machine', 2) WITH #Temp        
        
 EXEC SP_XML_REMOVEDOCUMENT @idoc        
        
UPDATE M SET M.IsAFTEnabled = T.IsAftEnabled      
FROM dbo.Machine M        
INNER JOIN #Temp T ON T.Stock_No = M.Machine_Stock_No        
        
END   

GO

