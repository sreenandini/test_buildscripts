USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_ImportReInstateData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_ImportReInstateData]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[usp_ImportReInstateData]   
@doc xml   
AS  
BEGIN  
	
	DECLARE @Installation_ID INT
	DECLARE @Collection_ID INT
	DECLARE @handle INT
	
	EXEC sp_xml_preparedocument @handle OUTPUT, @doc  

	SELECT @Installation_ID = Installation_ID  
	FROM OPENXML (@handle, './REINSTATE/tC/tI',2)    
	WITH ( Installation_ID INT './Installation_ID')  
  

	EXEC sp_xml_removedocument @handle

	SELECT 
		TOP (1) @Collection_ID  = Collection_ID
	FROM 
		[Collection] 
	WHERE 
		Installation_ID = @Installation_ID
	ORDER BY Collection_ID DESC

	UPDATE 
		[Collection]
	SET 
		Collection_Defloat_Collection = 0
	WHERE 
		Collection_ID = @Collection_ID
		
	UPDATE
		[Installation]
	SET 
		Installation_Float_Status = 0
	WHERE
		Installation_ID = @Installation_ID

	UPDATE BAR_Position SET 
		Bar_Position_Machine_Enabled = 1,
		Bar_Position_Machine_Enabled_Date = GETDATE() 
	WHERE Bar_Position_ID =  (SELECT TOP(1) Bar_Position_ID FROM INSTALLATION WITH(NOLOCK)
								WHERE Installation_ID = @Installation_ID)
 
END

GO

