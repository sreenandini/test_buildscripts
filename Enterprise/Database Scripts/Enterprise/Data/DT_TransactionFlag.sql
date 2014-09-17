/************************************************************
 * Created by Bally Technologies © 2011
 * Time: 26/12/12 5:43:30 PM
 ************************************************************/
USE [Enterprise]
GO

IF NOT EXISTS(SELECT 1 FROM [TransactionFlag]WHERE TransactionFlagName = 'Reset')
    INSERT [TransactionFlag] ( TransactionFlagName, TransactionFlagDescription )
    SELECT 'Reset', 'Flagged for FactoryReset'
ELSE
    UPDATE [TransactionFlag]
    SET    TransactionFlagDescription = 'Flagged for FactoryReset'
    WHERE  TransactionFlagName = 'Reset'

IF NOT EXISTS(SELECT 1 FROM [TransactionFlag]WHERE TransactionFlagName = 'Recovery')
    INSERT [TransactionFlag] ( TransactionFlagName, TransactionFlagDescription )
    SELECT 'Recovery', 'Flagged for Recovery'
ELSE
    UPDATE [TransactionFlag]
    SET    TransactionFlagDescription = 'Flagged for Recovery'
    WHERE  TransactionFlagName = 'Recovery'

IF NOT EXISTS(SELECT 1 FROM [TransactionFlag]WHERE TransactionFlagName = 'New')
    INSERT [TransactionFlag] ( TransactionFlagName, TransactionFlagDescription )
    SELECT 'New', 'Flagged for New Site'
ELSE
    UPDATE [TransactionFlag]
    SET    TransactionFlagDescription = 'Flagged for New Site'
    WHERE  TransactionFlagName = 'New'

IF NOT EXISTS(SELECT 1 FROM [TransactionFlag]WHERE TransactionFlagName = 'Update')
    INSERT [TransactionFlag] ( TransactionFlagName, TransactionFlagDescription )
    SELECT 'Update', 'Updated Site Details'
ELSE
    UPDATE [TransactionFlag]
    SET    TransactionFlagDescription = 'Updated Site Details'
    WHERE  TransactionFlagName = 'Update'

IF NOT EXISTS(SELECT 1 FROM [TransactionFlag]WHERE TransactionFlagName = 'Closed')
    INSERT [TransactionFlag] ( TransactionFlagName, TransactionFlagDescription )
    SELECT 'Closed', 'Flagged for Site closed'
ELSE
    UPDATE [TransactionFlag]
    SET    TransactionFlagDescription = 'Flagged for Site closed'
    WHERE  TransactionFlagName = 'Closed'

IF NOT EXISTS(SELECT 1 FROM [TransactionFlag]WHERE TransactionFlagName = 'Reopen')
    INSERT [TransactionFlag] ( TransactionFlagName, TransactionFlagDescription )
    SELECT 'Reopen', 'Flagged for site reopened'
ELSE
    UPDATE [TransactionFlag]
    SET    TransactionFlagDescription = 'Flagged for site reopened'
    WHERE  TransactionFlagName = 'Reopen'

GO