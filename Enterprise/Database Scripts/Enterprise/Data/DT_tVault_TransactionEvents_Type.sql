USE Enterprise  
GO

IF NOT EXISTS (
       SELECT ''
       FROM   tVault_TransactionEvents_Type
       WHERE  TypeName = 'VOUCHER'
   )
    INSERT INTO tVault_TransactionEvents_Type
    VALUES
      (
        1,
        'VOUCHER',
        'Voucher'
      )

IF NOT EXISTS (
       SELECT ''
       FROM   tVault_TransactionEvents_Type
       WHERE  TypeName = 'HANDPAY'
   )
    INSERT INTO tVault_TransactionEvents_Type
    VALUES
      (
        2,
        'HANDPAY',
        'Handpay'
      )

IF NOT EXISTS (
       SELECT ''
       FROM   tVault_TransactionEvents_Type
       WHERE  TypeName = 'JACKPOT'
   )
    INSERT INTO tVault_TransactionEvents_Type
    VALUES
      (
        3,
        'JACKPOT',
        'Jackpot'
      )
	
IF NOT EXISTS (
       SELECT ''
       FROM   tVault_TransactionEvents_Type
       WHERE  TypeName = 'MYSTERY'
   )
    INSERT INTO tVault_TransactionEvents_Type
    VALUES
      (
        4,
        'MYSTERY',
        'Mystery'
      )

IF NOT EXISTS (
       SELECT ''
       FROM   tVault_TransactionEvents_Type
       WHERE  TypeName = 'PROG'
   )
    INSERT INTO tVault_TransactionEvents_Type
    VALUES
      (
        5,
        'PROG',
        'Progressive'
      )

GO