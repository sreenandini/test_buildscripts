USE Enterprise
GO

IF NOT EXISTS (
       SELECT '1'
       FROM   [tNGATypes] WITH (NOLOCK)
       WHERE  NAME = 'Vault'
   )
    INSERT INTO [tNGATypes]
    VALUES
      (
        1,
        'Vault',
        'Vault device'
      )
GO 