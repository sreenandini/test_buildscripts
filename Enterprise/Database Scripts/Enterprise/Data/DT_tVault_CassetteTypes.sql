USE Enterprise
GO
DELETE FROM tVault_CassetteTypes
IF NOT EXISTS (
       SELECT ''
       FROM   tVault_CassetteTypes
       WHERE  CassetteType_Name = 'Cassette'
   )
BEGIN
    INSERT INTO tVault_CassetteTypes
    VALUES
      (
        1,
        'Cassette',
        'Cassette'
      )
END
GO

IF NOT EXISTS (
       SELECT ''
       FROM   tVault_CassetteTypes
       WHERE  CassetteType_Name = 'Rejection'
   )
BEGIN
    INSERT INTO tVault_CassetteTypes
    VALUES
      (
        2,
        'Rejection',
        'Rejection'
      )
END
  
GO    
IF NOT EXISTS (
       SELECT ''
       FROM   tVault_CassetteTypes
       WHERE  CassetteType_Name = 'Hopper'
   )
BEGIN
    INSERT INTO tVault_CassetteTypes
    VALUES
      (
        50,
        'Hopper',
        'Hopper'
      )
END
GO