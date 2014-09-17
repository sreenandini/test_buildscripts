USE [Enterprise]
GO

IF NOT EXISTS(SELECT 1 FROM Call_Remedy WHERE Call_Remedy_Reference = 99)
    INSERT [Call_Remedy] ( Call_Remedy_Description, Call_Remedy_Reference ,Call_Remedy_Attract_Downtime)
    SELECT 'Other', 99 , 0
ELSE
    UPDATE [Call_Remedy]
    SET   Call_Remedy_Description = 'Other',
		  Call_Remedy_Attract_Downtime = 0
    WHERE  Call_Remedy_Reference = 99
GO

