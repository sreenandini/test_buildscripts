/************************************************************
 * Created by Bally Technologies © 2011
 * Time: 26/12/12 5:43:30 PM
 ************************************************************/
USE [Enterprise]
GO

SET IDENTITY_INSERT [ShareHolders] ON
GO

IF NOT EXISTS(SELECT 1 FROM [ShareHolders] WHERE ShareHolderId = 1)
    INSERT [ShareHolders] (ShareHolderId, ShareHolderName, ShareHolderDescription, DateCreated, DateModified, SysDelete, SHKey )
    SELECT 1, 'Operator', 'Operator', GETDATE(), NULL, 0, 0
ELSE
    UPDATE [ShareHolders]
    SET    ShareHolderDescription = 'Operator', DateModified = GETDATE(), SysDelete = 0, SHKey = 0
    WHERE  ShareHolderId = 1

IF NOT EXISTS(SELECT 1 FROM [ShareHolders] WHERE ShareHolderId = 2)
    INSERT [ShareHolders] (ShareHolderId, ShareHolderName, ShareHolderDescription, DateCreated, DateModified, SysDelete, SHKey )
    SELECT 2, 'Government', 'Government', GETDATE(), NULL, 0, 0
ELSE
    UPDATE [ShareHolders]
    SET    ShareHolderDescription = 'Government', DateModified = GETDATE(), SysDelete = 0, SHKey = 0
    WHERE  ShareHolderId = 2

IF NOT EXISTS(SELECT 1 FROM [ShareHolders] WHERE ShareHolderId = 3)
    INSERT [ShareHolders] (ShareHolderId, ShareHolderName, ShareHolderDescription, DateCreated, DateModified, SysDelete, SHKey )
    SELECT 3, 'Retailer', 'Retailer', GETDATE(), NULL, 0, 0
ELSE
    UPDATE [ShareHolders]
    SET    ShareHolderDescription = 'Retailer', DateModified = GETDATE(), SysDelete = 0, SHKey = 0
    WHERE  ShareHolderId = 3

SET IDENTITY_INSERT [ShareHolders] OFF
GO