USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fnFormatDate]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fnFormatDate]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- ===================================================================================================================================  
-- SELECT dbo.fnFormatDate(GETDATE(), 'DD/MM/YYYY')  
-- -----------------------------------------------------------------------------------------------------------------------------------  
--  
-- returns the date in the expected format  
--   
  
-- -----------------------------------------------------------------------------------------------------------------------------------  
-- Revision History   
--   
-- 20/08/2008 Sudarsan S Created  
-- ===================================================================================================================================  
  
CREATE FUNCTION [dbo].[fnFormatDate]  
(  
 @InputDate DATETIME,    
 @FormatString VARCHAR(25)  
)  
RETURNS VARCHAR(20) AS   
  
BEGIN  
  
DECLARE @Dateformat INT  
DECLARE @ReturnedDate VARCHAR(20)  
  
 SELECT @DateFormat = CASE @FormatString  
      WHEN 'mm/dd/yy' THEN 1  
      WHEN 'mm/dd/yyyy' THEN 101  
      WHEN 'yy.mm.dd' THEN 2  
      WHEN 'dd/mm/yy' THEN 3  
      WHEN 'dd.mm.yy' THEN 4  
      WHEN 'dd-mm-yy' THEN 5  
      WHEN 'dd Mmm yy' THEN 6  
      WHEN 'Mmm dd, yy' THEN 7  
      WHEN 'yyyy.mm.dd' THEN 102  
      WHEN 'dd/mm/yyyy' THEN 103  
      WHEN 'dd.mm.yyyy' THEN 104  
      WHEN 'dd-mm-yyyy' THEN 105  
      WHEN 'dd Mmm yyyy' THEN 106  
      WHEN 'Mmm dd, yyyy' THEN 107  
      WHEN 'mm-dd-yy' THEN 10  
      WHEN 'mm-dd-yyyy' THEN 110  
      WHEN 'yy/mm/dd' THEN 11  
      WHEN 'yyyy/mm/dd' THEN 111  
      WHEN 'yymmdd' THEN 12  
      WHEN 'yyyymmdd' THEN 112  
      ELSE 101 END  
  
  
 SELECT @ReturnedDate = CONVERT(VARCHAR, @InputDate, @DateFormat)  
  
RETURN @ReturnedDate  
  
END  
  

GO

