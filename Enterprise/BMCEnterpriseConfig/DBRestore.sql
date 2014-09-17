USE [master]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CreateDatabase]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CreateDatabase]
GO

CREATE PROC CreateDatabase( @mdf VARCHAR(MAX), @ldf VARCHAR(MAX), @sDatabaseName VARCHAR(MAX))  
AS  
BEGIN  
DECLARE @sData VARCHAR(MAX)  
DECLARE @sLog VARCHAR(MAX)   
DECLARE @sBackUPFile VARCHAR(MAX)  
DECLARE @CreateDatabase NVARCHAR(MAX)
  
SET @sData = @sDatabaseName + '_Data'  
SET @sLog = @sDatabaseName + '_Log'  
SET @mdf = @mdf + '\' + @sDatabaseName + '.mdf'  
SET @ldf = @ldf + '\' + @sDatabaseName + '.ldf'  
SET @CreateDatabase = 'CREATE DATABASE [' + @sDatabaseName + '] ON  PRIMARY ' 
SET @CreateDatabase = @CreateDatabase + '( NAME = N''' + @sData + ''', FILENAME = N''' + @mdf + ''' , SIZE = 35904KB , MAXSIZE = UNLIMITED, FILEGROWTH = 10%)'
SET @CreateDatabase = @CreateDatabase + ' LOG ON '
SET @CreateDatabase = @CreateDatabase + '( NAME = N''' + @sLog + ''', FILENAME = N''' + @ldf + ''' , SIZE = 1024KB , MAXSIZE = UNLIMITED, FILEGROWTH = 10%)'
SET @CreateDatabase = @CreateDatabase + ' COLLATE SQL_Latin1_General_CP1_CI_AS'
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
