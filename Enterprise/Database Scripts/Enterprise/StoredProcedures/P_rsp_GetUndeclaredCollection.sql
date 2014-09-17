USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetUndeclaredCollection]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetUndeclaredCollection]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[rsp_GetUndeclaredCollection] 
	(@Site_Id INT,
	@AddToExisting bit = 0)  
AS  
BEGIN  
	
	 DECLARE @IsMachineBasedDeclaration VARCHAR(50)
	  
	  DECLARE @McBasedDrop VARCHAR(50)

	  EXEC [dbo].[rsp_GetSiteSetting] @Site_ID, 'IsMachineBasedDropDeclaration', @IsMachineBasedDeclaration OUTPUT
	  IF @IsMachineBasedDeclaration IS NULL OR @IsMachineBasedDeclaration = ''
		SELECT @IsMachineBasedDeclaration = 'False'
	  
      If (@AddToExisting  = 0)  
      BEGIN  
            SELECT
                  DISTINCT B.Batch_ID,  
                  B.Batch_Name,  
                  DisplayName = Convert(Varchar(10),RIGHT(B.Batch_Ref, LEN(B.Batch_Ref) - LEN(LEFT(B.Batch_Ref, 5)))) + ' - ' + B.Batch_Name
                  ,BatchTime = Batch_Date
            FROM Collection  C
				INNER JOIN Batch B ON C.Batch_ID = B.Batch_ID
				INNER JOIN [Installation] I ON I.[Installation_ID] = C.[Installation_ID]
				INNER JOIN [Bar_Position] BP ON BP.[Bar_Position_ID] = I.[Bar_Position_ID]
				INNER JOIN [Site] S ON S.[Site_ID] = BP.[Site_ID]
            WHERE   
                  C.Declaration = 0  
                  --AND Batch.Collection_Batch_Machine = @MachineID
                 AND B.Batch_Import_Complete = 1
				 AND B.Batch_ID <> 0 -- Exclude Part Collection
                 AND S.[Site_ID] = @Site_Id 
                 AND ( ( @IsMachineBasedDeclaration <> 'False' ) --AND Batch.Batch_Name = @MachineID     )                
					  OR                      
					  @IsMachineBasedDeclaration = 'False'                
					)  
            ORDER BY Batch_Date
      END  
      ELSE  
      BEGIN  
            SELECT   
                  DISTINCT B.Batch_ID,  
                  B.Batch_Name,  
                  DisplayName = Convert(Varchar(10),RIGHT(B.Batch_Ref, LEN(B.Batch_Ref) - LEN(LEFT(B.Batch_Ref, 5)))) + ' - ' + B.Batch_Name  
                  ,BatchTime = Batch_Date
            FROM Collection C
				INNER JOIN Batch B ON C.Batch_ID = B.Batch_ID
				INNER JOIN [Installation] I ON I.[Installation_ID] = C.[Installation_ID]
				INNER JOIN [Bar_Position] BP ON BP.[Bar_Position_ID] = I.[Bar_Position_ID]
				INNER JOIN [Site] S ON S.[Site_ID] = BP.[Site_ID]
            WHERE   
                  C.Declaration = 0
                  AND B.Batch_Import_Complete = 1
				  AND B.Batch_ID <> 0 -- Exclude Part Collection
				  AND S.[Site_ID] = @Site_Id    
                  --AND Batch.Collection_Batch_Machine = @MachineID  
            ORDER BY Batch_Date
      END  
END

