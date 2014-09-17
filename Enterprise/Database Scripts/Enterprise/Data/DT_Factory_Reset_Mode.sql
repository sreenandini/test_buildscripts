USE Enterprise
GO

IF NOT EXISTS(
       SELECT 1
       FROM   Factory_Reset_Mode
       WHERE  FR_Mode = 'Master Reset'
   )
    INSERT Factory_Reset_Mode
      (
        [FR_Mode],
        [FR_Description]
      )
    VALUES
      (
        'Master Reset',
        'Reset site controller to fresh setup'
      )

IF NOT EXISTS(
       SELECT 1
       FROM   Factory_Reset_Mode
       WHERE  FR_Mode = 'Reset Initial Setting'
   )
    INSERT Factory_Reset_Mode
      (
        [FR_Mode],
        [FR_Description]
      )
    VALUES
      (
        'Reset Initial Setting',
        'Rerverts all changes made to site controller'
      )
    
IF NOT EXISTS(
       SELECT 1
       FROM   Factory_Reset_Mode
       WHERE  FR_Mode = 'Delete Account Information'
   )
    INSERT Factory_Reset_Mode
      (
        [FR_Mode],
        [FR_Description]
      )
    VALUES
      (
        'Delete Account Information',
        'Deletes all account information in site controller'
      )
GO