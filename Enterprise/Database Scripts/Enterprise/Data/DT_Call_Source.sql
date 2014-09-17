USE [Enterprise]
GO


IF NOT EXISTS(SELECT 1 FROM [Call_Source] WHERE Call_Source_Reference = '99')
    INSERT [Call_Source] ( Call_Source_Description, Call_Source_Reference )
    SELECT 'GMU', '1'
ELSE
    UPDATE [Call_Source]
    SET   Call_Source_Description = 'GMU'
    WHERE  Call_Source_Reference = '1'
GO

IF NOT EXISTS(SELECT 1 FROM [Call_Source] WHERE Call_Source_Reference = '99')
    INSERT [Call_Source] ( Call_Source_Description, Call_Source_Reference )
    SELECT 'Door', '2'
ELSE
    UPDATE [Call_Source]
    SET   Call_Source_Description = 'Door'
    WHERE  Call_Source_Reference = '2'
GO

IF NOT EXISTS(SELECT 1 FROM [Call_Source] WHERE Call_Source_Reference = '99')
    INSERT [Call_Source] ( Call_Source_Description, Call_Source_Reference )
    SELECT 'Slot Machine', '3'
ELSE
    UPDATE [Call_Source]
    SET   Call_Source_Description = 'Slot Machine'
    WHERE  Call_Source_Reference = '3'
GO

IF NOT EXISTS(SELECT 1 FROM [Call_Source] WHERE Call_Source_Reference = '99')
    INSERT [Call_Source] ( Call_Source_Description, Call_Source_Reference )
    SELECT 'Network Problem', '4'
ELSE
    UPDATE [Call_Source]
    SET   Call_Source_Description = 'Network Problem'
    WHERE  Call_Source_Reference = '4'
GO


IF NOT EXISTS(SELECT 1 FROM [Call_Source] WHERE Call_Source_Reference = '99')
    INSERT [Call_Source] ( Call_Source_Description, Call_Source_Reference )
    SELECT 'Power', '5'
ELSE
    UPDATE [Call_Source]
    SET   Call_Source_Description = 'Power'
    WHERE  Call_Source_Reference = '5'
GO

IF NOT EXISTS(SELECT 1 FROM [Call_Source] WHERE Call_Source_Reference = '99')
    INSERT [Call_Source] ( Call_Source_Description, Call_Source_Reference )
    SELECT 'Other', '99'
ELSE
    UPDATE [Call_Source]
    SET   Call_Source_Description = 'Other'
    WHERE  Call_Source_Reference = '99'
GO
