USE [Enterprise]
GO


UPDATE [Read] SET ReadDate = CONVERT(DATETIME, Read_Date, 105) WHERE ReadDate IS NULL
GO