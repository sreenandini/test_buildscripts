GO
USE [master]
GO
/****** Object:  StoredProcedure [dbo].[RestoreDatabase]    Script Date: 01/02/2012 12:23:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RestoreDatabase]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[RestoreDatabase]
GO
CREATE PROC RestoreDatabase( @sBackUPPath VARCHAR(MAX), @sRestorePath VARCHAR(MAX), @sDatabaseName VARCHAR(MAX))  
AS  
BEGIN  
DECLARE @sData VARCHAR(MAX)  
DECLARE @sLog VARCHAR(MAX)  
DECLARE @mdf VARCHAR(MAX)  
DECLARE @ldf VARCHAR(MAX)  
DECLARE @sBackUPFile VARCHAR(MAX)  
  
SET @sData = @sDatabaseName + '_Data'  
SET @sLog = @sDatabaseName + '_Log'  
SET @mdf = @sRestorePath + '\' + @sDatabaseName + '.mdf'  
SET @ldf = @sRestorePath + '\' + @sDatabaseName + '.ldf'  
SET @sBackUPFile = @sBackUPPath + '\'+ @sDatabaseName + 'BlankDB.bak'  
RESTORE DATABASE @sDatabaseName   
FROM DISK = @sBackUPFile  
WITH MOVE @sData TO @mdf,  
MOVE @sLog TO @ldf  
END
GO
/****** Object:  StoredProcedure [dbo].[RestoreDatabase]    Script Date: 01/02/2012 12:23:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IsDBExists]') AND type in (N'FN'))
DROP FUNCTION [dbo].[IsDBExists]
GO
CREATE FUNCTION IsDBExists(@Database VARCHAR(MAX))
RETURNS BIT
BEGIN
	IF EXISTS(SELECT 1 from sys.databases where name = @Database)
	RETURN 1
RETURN 0
END
GO
/***#Separator#***/
USE [master]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CreateDatabase]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CreateDatabase]
GO
CREATE PROC CreateDatabase( @mdf VARCHAR(MAX), @ldf VARCHAR(MAX), @sDatabaseName VARCHAR(50))  
AS  
BEGIN  
DECLARE @sData VARCHAR(MAX)  
DECLARE @sLog VARCHAR(MAX)  
DECLARE @sBackUPFile VARCHAR(MAX)  
DECLARE @CreateDatabase NVARCHAR(MAX) 
DECLARE @SqlCollation  NVARCHAR(50)
SET @SqlCollation= CASE WHEN UPPER(@sDatabaseName)='EXCHANGE' THEN 'Latin1_General_CI_AS'
     ELSE 'SQL_Latin1_General_CP1_CI_AS'
END
SET @sData = @sDatabaseName + '_Data'  
SET @sLog = @sDatabaseName + '_Log'  
SET @mdf = @mdf + '\' + @sDatabaseName + '.mdf'  
SET @ldf = @ldf + '\' + @sDatabaseName + '.ldf' 
SET @CreateDatabase = 'CREATE DATABASE [' + @sDatabaseName + '] ON  PRIMARY ' 
SET @CreateDatabase = @CreateDatabase + '( NAME = N''' + @sData + ''', FILENAME = N''' + @mdf + ''' , SIZE = 35904KB , MAXSIZE = UNLIMITED, FILEGROWTH = 10%)'
SET @CreateDatabase = @CreateDatabase + ' LOG ON '
SET @CreateDatabase = @CreateDatabase + '( NAME = N''' + @sLog + ''', FILENAME = N''' + @ldf + ''' , SIZE = 1024KB , MAXSIZE = UNLIMITED, FILEGROWTH = 10%)'
SET @CreateDatabase = @CreateDatabase + ' COLLATE '+ @SqlCollation
exec sp_executesql @CreateDatabase
END
GO
USE [master]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DropDatabase]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[DropDatabase]
GO
CREATE PROC DropDatabase( @sDatabaseName VARCHAR(50))  
AS  
BEGIN  
DECLARE @DropDatabase NVARCHAR(MAX)  
SET @DropDatabase = 'IF EXISTS ( SELECT 1 FROM SYS.DATABASES WHERE NAME = ''' + @sDatabaseName + ''') 
                                        BEGIN
                                        ALTER DATABASE ' + @sDatabaseName + ' SET SINGLE_USER WITH ROLLBACK IMMEDIATE  
                                        DROP DATABASE [' + @sDatabaseName + ']
                                        END'


exec sp_executesql @DropDatabase
END
GO
