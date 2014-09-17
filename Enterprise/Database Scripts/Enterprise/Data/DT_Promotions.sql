USE Enterprise
GO

IF NOT EXISTS(SELECT 1 FROM [Promotions] WHERE SourceName = 'TIS')
    INSERT [Promotions] ( PromotionalName, SourceName,dtExpire)
    SELECT 'TIS Promotional', 'TIS',GETDATE()


