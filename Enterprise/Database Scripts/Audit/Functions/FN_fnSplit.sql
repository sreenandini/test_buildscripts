USE [Audit]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fnSplit]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fnSplit]
GO

USE [Audit]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- ===================================================================================================================================    
-- SELECT * from dbo.fnSplit('1,2,3,4,5',',')   -- returns 1,2,3,4,5 in table format  
-- -----------------------------------------------------------------------------------------------------------------------------------    
--    
-- returns the passed list as a table, using ',' as a seperator  
--     
-- -----------------------------------------------------------------------------------------------------------------------------------    
-- Revision History     
--     
-- 24/03/2010 C.Taylor ( contractor ) Created    
-- ===================================================================================================================================    
CREATE FUNCTION dbo.fnSplit  
(  
 @RowData nvarchar(2000),  
 @SplitOn nvarchar(5)  
)    
RETURNS @RtnValue table   
(  
 Id int identity(1,1),  
 Data nvarchar(100)  
)   
AS    
BEGIN   
 Declare @Cnt int  
 Set @Cnt = 1  
  
 While (Charindex(@SplitOn,@RowData)>0)  
 Begin  
  Insert Into @RtnValue (data)  
  Select   
   Data = ltrim(rtrim(Substring(@RowData,1,Charindex(@SplitOn,@RowData)-1)))  
  
  Set @RowData = Substring(@RowData,Charindex(@SplitOn,@RowData)+1,len(@RowData))  
  Set @Cnt = @Cnt + 1  
 End  
   
 Insert Into @RtnValue (data)  
 Select Data = ltrim(rtrim(@RowData))  
  
 Return  
END  
GO

