USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fnGetCustomDateFormat]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
	DROP FUNCTION [dbo].[fnGetCustomDateFormat]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- ===================================================================================================================================  
-- SELECT dbo.fnGetCustomDateFormat(GETDATE(), 'DD-MM-YYYY HH:mm:ss')  
-- -----------------------------------------------------------------------------------------------------------------------------------  
--  
-- returns the date in the expected format  
--   
  
-- -----------------------------------------------------------------------------------------------------------------------------------  
-- Revision History   
--   
-- 
-- ===================================================================================================================================  

CREATE FUNCTION dbo.fnGetCustomDateFormat
(
	@Datetime DATETIME, 
	@FormatMask VARCHAR(50)
)
RETURNS VARCHAR(50)
AS
BEGIN
    DECLARE @StringDate VARCHAR(32)
    SET @StringDate = @FormatMask
    
	IF (CHARINDEX ('YYYY' COLLATE sql_latin1_General_CP1_cs_as,@StringDate) > 0)
       SET @StringDate = REPLACE(@StringDate, 'YYYY' COLLATE sql_latin1_General_CP1_cs_as,
                         DATENAME(YY, @Datetime))

    IF (CHARINDEX ('YY' COLLATE sql_latin1_General_CP1_cs_as,@StringDate) > 0)
       SET @StringDate = REPLACE(@StringDate, 'YY' COLLATE sql_latin1_General_CP1_cs_as,
                         RIGHT(DATENAME(YY, @Datetime),2))

    IF (CHARINDEX ('Month',@StringDate) > 0)
       SET @StringDate = REPLACE(@StringDate, 'Month',
                         DATENAME(MM, @Datetime))

    IF (CHARINDEX ('MON',@StringDate COLLATE SQL_Latin1_General_CP1_CS_AS)>0)
       SET @StringDate = REPLACE(@StringDate, 'MON',
                         LEFT(UPPER(DATENAME(MM, @Datetime)),3))

    IF (CHARINDEX ('Mon',@StringDate) > 0)
       SET @StringDate = REPLACE(@StringDate, 'Mon',
                                     LEFT(DATENAME(MM, @Datetime),3))

    IF (CHARINDEX ('MM' COLLATE sql_latin1_General_CP1_cs_as,@StringDate) > 0)
       SET @StringDate = REPLACE(@StringDate, 'MM' COLLATE sql_latin1_General_CP1_cs_as,
                  RIGHT('0'+CONVERT(VARCHAR,DATEPART(MM, @Datetime)),2))

    IF (CHARINDEX ('M' COLLATE sql_latin1_General_CP1_cs_as,@StringDate) > 0)
       SET @StringDate = REPLACE(@StringDate, 'M' COLLATE sql_latin1_General_CP1_cs_as,
                         CONVERT(VARCHAR,DATEPART(MM, @Datetime)))

    IF (CHARINDEX ('DD' COLLATE sql_latin1_General_CP1_cs_as,@StringDate) > 0)
       SET @StringDate = REPLACE(@StringDate, 'DD' COLLATE sql_latin1_General_CP1_cs_as,
                         RIGHT('0'+DATENAME(DD, @Datetime),2))

    IF (CHARINDEX ('D' COLLATE sql_latin1_General_CP1_cs_as,@StringDate) > 0)
       SET @StringDate = REPLACE(@StringDate, 'D' COLLATE sql_latin1_General_CP1_cs_as,
                                     DATENAME(DD, @Datetime))   

	IF (CHARINDEX ('HH' COLLATE sql_latin1_General_CP1_cs_as,@StringDate) > 0)
       SET @StringDate = REPLACE(@StringDate, 'HH' COLLATE sql_latin1_General_CP1_cs_as,
                                     RIGHT('0' + DATENAME(HH, @Datetime), 2))

	IF (CHARINDEX ('mm' COLLATE sql_latin1_General_CP1_cs_as,@StringDate) > 0)
       SET @StringDate = REPLACE(@StringDate, 'mm' COLLATE sql_latin1_General_CP1_cs_as,
                                     RIGHT('0' + DATENAME(MINUTE, @Datetime), 2))

	IF (CHARINDEX ('ss' COLLATE sql_latin1_General_CP1_cs_as,@StringDate) > 0)
       SET @StringDate = REPLACE(@StringDate, 'ss' COLLATE sql_latin1_General_CP1_cs_as,
                                     RIGHT('0' + DATENAME(SS, @Datetime), 2))
	
	RETURN @StringDate
END
GO