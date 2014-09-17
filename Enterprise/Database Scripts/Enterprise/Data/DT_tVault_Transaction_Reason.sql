USE Enterprise
GO

IF NOT EXISTS (
       SELECT ''
       FROM   tVault_Transaction_Reason
       WHERE  Reason_Description = 'FILL'
   )
    INSERT INTO tVault_Transaction_Reason
    VALUES
      (
       
        'FILL'
      )

IF NOT EXISTS (
       SELECT ''
       FROM   tVault_Transaction_Reason
       WHERE  Reason_Description = 'BLEED'
   )
    INSERT INTO tVault_Transaction_Reason
    VALUES
      (
       
        'BLEED'
      )

IF NOT EXISTS (
       SELECT ''
       FROM   tVault_Transaction_Reason
       WHERE  Reason_Description = 'ADJUSTMENT'
   )
    INSERT INTO tVault_Transaction_Reason
    VALUES
      (
      
        'ADJUSTMENT'
      )

IF NOT EXISTS (
       SELECT ''
       FROM   tVault_Transaction_Reason
       WHERE  Reason_Description = 'DROP'
   )
    INSERT INTO tVault_Transaction_Reason
    VALUES
      (
       
        'DROP'
      )
      
      
IF NOT EXISTS (
       SELECT ''
       FROM   tVault_Transaction_Reason
       WHERE  Reason_Description = 'STANDARD FILL'
   )
    INSERT INTO tVault_Transaction_Reason
    VALUES
      (
        
        'STANDARD FILL'
      )
 
 IF NOT EXISTS (
       SELECT ''
       FROM   tVault_Transaction_Reason
       WHERE  Reason_Description = 'INITIAL FILL'
   )
    INSERT INTO tVault_Transaction_Reason
    VALUES
      (
       
        'INITIAL FILL'
      )
  
  
 IF NOT EXISTS (
       SELECT ''
       FROM   tVault_Transaction_Reason
       WHERE  Reason_Description = 'EMERGENCY FILL'
   )
    INSERT INTO tVault_Transaction_Reason
    VALUES
      (
       
        'EMERGENCY FILL'
      )
      
 IF NOT EXISTS (
       SELECT ''
       FROM   tVault_Transaction_Reason
       WHERE  Reason_Description = 'FINAL DROP'
   )
    INSERT INTO tVault_Transaction_Reason
    VALUES
      (
        
        'FINAL DROP'
      )   

 IF NOT EXISTS (
       SELECT ''
       FROM   tVault_Transaction_Reason
       WHERE  Reason_Description = 'AUTO ADJUST'
   )
    INSERT INTO tVault_Transaction_Reason
    VALUES
      (
       
        'AUTO ADJUST'
      )  
      
GO