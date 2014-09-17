USE Enterprise  
GO  

IF NOT EXISTS (
       SELECT ''
       FROM   tVault_Transaction_Type
       WHERE  Type_Description = 'FILL'
   )
    INSERT INTO tVault_Transaction_Type
    VALUES
      (
        1,
        'FILL'
      )

IF NOT EXISTS (
       SELECT ''
       FROM   tVault_Transaction_Type
       WHERE  Type_Description = 'BLEED'
   )
    INSERT INTO tVault_Transaction_Type
    VALUES
      (
        2,
        'BLEED'
      )

IF NOT EXISTS (
       SELECT ''
       FROM   tVault_Transaction_Type
       WHERE  Type_Description = 'ADJUSTMENT'
   )
    INSERT INTO tVault_Transaction_Type
    VALUES
      (
        3,
        'ADJUSTMENT'
      )

IF NOT EXISTS (
       SELECT ''
       FROM   tVault_Transaction_Type
       WHERE  Type_Description = 'DROP'
   )
    INSERT INTO tVault_Transaction_Type
    VALUES
      (
        4,
        'DROP'
      )
      
IF NOT EXISTS (
       SELECT ''
       FROM   tVault_Transaction_Type
       WHERE  Type_Description = 'STANDARDFILL'
   )
    INSERT INTO tVault_Transaction_Type
    VALUES
      (
        5,
        'STANDARDFILL'
      )

IF NOT EXISTS (
       SELECT ''
       FROM   tVault_Transaction_Type
       WHERE  Type_Description = 'FINALDROP'
   )
    INSERT INTO tVault_Transaction_Type
    VALUES
      (
        6,
        'FINALDROP'
      )
       
GO