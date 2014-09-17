USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_CreateDataLoadScript]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_CreateDataLoadScript]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/************************************************************          
 * Created by Bally Technologies © 2011          
 * Time: 24/12/12 7:17:42 PM          
 ************************************************************/          
          
CREATE PROCEDURE sp_CreateDataLoadScript          
 @TblName VARCHAR(128),          
 @TblColumnName VARCHAR(128),          
 @UpdateColumns VARCHAR(MAX),          
@OmitInsertColumns VARCHAR(MAX),      
@IS_identity INT =NULL,
@UpdateRequired BIT = 1         
          
AS          
 /*                    
 exec sp_CreateDataLoadScript 'SETTING', Setting_Name,'Setting_Value',''                   
 */                    
           
           
 CREATE TABLE #a          
 (          
  id INT IDENTITY (1, 1          
 ),           
 ColType INT,           
 ColName VARCHAR(128),          
 Date_Type NVARCHAR(256)          
 )                    
           
 INSERT #a ( ColType, ColName, Date_Type )          
 SELECT CASE           
             WHEN DATA_TYPE LIKE '%char%' THEN 1          
             ELSE 0          
        END, COLUMN_NAME, DATA_TYPE FROM information_schema.columns WHERE TABLE_NAME = @TblName AND COLUMN_NAME IN (SELECT NAME FROM sys.[columns] c WHERE c.[object_id] = OBJECT_ID(@TblName) AND IS_identity = COALESCE(@IS_identity,IS_identity))ORDER BY ORDINAL_POSITION                    
           
 IF NOT EXISTS (SELECT 1 FROM #a)          
 BEGIN          
     RAISERROR('No columns found for table %s', 16, -1, @TblName)           
     RETURN          
 END                    
          
SELECT * FROM #a       
 DECLARE @id INT, @maxid INT, @cmd1 VARCHAR(MAX), @cmd2 VARCHAR(MAX), @cmd3 VARCHAR(MAX), @cmd4 VARCHAR(MAX), @cmd5 VARCHAR(MAX)                    
           
 SELECT @id = 0, @maxid = MAX(id) FROM #a                    
           
 SELECT @cmd3 = 'if not exists(select 1 from ' + '['+@TblName  +']'                    
   PRINT @cmd3        
 SELECT @cmd5 = ' where ' + @TblColumnName + ' =  '           
        + ''''''' + case when ' + ColName + ' is null '           
        + ' then ''null'' '           
        + ' else '           
        + CASE           
               WHEN 0 = 1 THEN ''''''''' + ' + ColName + ' + '''''''''          
               ELSE 'convert(varchar(2000),' + ColName + ')'          
          END           
        + ' end + '''''')' FROM #a WHERE ColName = @TblColumnName                    
PRINT @cmd5      
 SELECT @cmd3 = @cmd3 + @cmd5           
 PRINT @cmd3                    
 SELECT @cmd1 = 'select ''' + @cmd3 + ' insert ' + '[' + @TblName + '] ( '                    
 SELECT @cmd2 = ' + '' select '' + '                    
           
 SELECT @id = 0, @maxid = MAX(id) FROM #a                    
           
 WHILE @id < @maxid          
 BEGIN          
     SELECT @id = MIN(id) FROM #a WHERE id > @id                    
               
     SELECT @cmd1 = @cmd1 + ColName + ',' FROM #a WHERE ColName COLLATE Latin1_General_CI_AS NOT IN (SELECT Ltrim(Rtrim(VALUE)) FROM dbo.[UDF_GetStringTable](@OmitInsertColumns, ',')) and id = @id                    
               
     SELECT @cmd2 = @cmd2           
            + ' case when ' + ColName + ' is null '           
            + ' then ''null'' '           
            + ' else '           
            + CASE           
                   WHEN ColType = 1 THEN ''''''''' + ' + ColName + ' + '''''''''          
                        ----                   WHEN Date_Type = 'datetime' THEN 'convert(DATETIME,' + ColName + ')'          
                        --                   WHEN Date_Type = 'int' THEN '' + ColName + ''          
                   ELSE 'convert(varchar(2000),' + ColName + ')'          
              END           
            + ' end + '','' + ' FROM #a WHERE ColName COLLATE Latin1_General_CI_AS NOT IN (SELECT Ltrim(Rtrim(VALUE)) FROM dbo.[UDF_GetStringTable](@OmitInsertColumns, ',')) and id = @id          
 END                    
           
 SELECT @cmd4 = ' + '' else update [' + @TblName + '] set '                     
           
 SELECT @id = 0, @maxid = MAX(id) FROM #a                    
             
          
 WHILE @id < @maxid          
 BEGIN         
        
     SELECT @id = MIN(id) FROM #a WHERE id > @id         
    PRINT User      
     SELECT @cmd4 = @cmd4           
            + ColName + ' = ''+ case when ' + ColName + ' is null '           
            + ' then ''null'' '           
            + ' else '           
            + CASE           
             WHEN ColType = 1 THEN ''''''''' + ' + ColName + ' + '''''''''          
                   ELSE 'convert(varchar(2000),' + ColName + ')'          
              END           
            + ' end + '','         
       FROM #a WHERE ColName COLLATE SQL_Latin1_General_CP1_CI_AS IN (SELECT Ltrim(Rtrim(VALUE)) FROM dbo.[UDF_GetStringTable](@UpdateColumns, ',')) AND id = @id        
        
 END                    
           
 SELECT @cmd1 = LEFT(@cmd1, LEN(@cmd1) -1) + ' ) '' '                    
 SELECT @cmd2 = LEFT(@cmd2, LEN(@cmd2) -8) + CASE WHEN (@UpdateRequired = 1 ) THEN LEFT(@cmd4, LEN(@cmd4) -1) + LEFT(@cmd5, LEN(@cmd5) -2) + '''''' ELSE '' END +  ' from dbo.[' + @tblName + ']'           
           
 -- select '/*' + @cmd1 + @cmd2 + '*/'  Latin1_General_CI_AS   SQL_Latin1_General_CP1_CI_AS               
 PRINT @cmd1 + @cmd2           
 PRINT @tblName                    
 EXEC (@cmd1 + @cmd2)           
 DROP TABLE #a 
GO

