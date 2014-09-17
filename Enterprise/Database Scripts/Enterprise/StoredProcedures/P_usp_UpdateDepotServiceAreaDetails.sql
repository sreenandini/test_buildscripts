USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateDepotServiceAreaDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateDepotServiceAreaDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE usp_UpdateDepotServiceAreaDetails(@Depot_ID AS INT,  
             @Service_Area_Name AS VARCHAR(16),  
             @Service_Area_Description AS VARCHAR(16),  
             @Service_Area_Notes AS VARCHAR(16),
             @Service_Area_ID INT)
AS  
BEGIN  
	
  IF EXISTS(SELECT 1 FROM Service_Areas WITH(NOLOCK) WHERE Service_Area_ID =@Service_Area_ID)
	BEGIN
		UPDATE Service_Areas  
		SET	[Depot_ID] = @Depot_ID,  
        [Service_Area_Name] = @Service_Area_Name,  
        [Service_Area_Description] = @Service_Area_Description,  
        [Service_Area_Notes] = @Service_Area_Notes  
		WHERE Service_Area_ID=@Service_Area_ID 
	END  
  ELSE
	BEGIN

	INSERT INTO Service_Areas  
           ([Depot_ID]  
           ,[Service_Area_Name]  
           ,[Service_Area_Description]  
           ,[Service_Area_Notes])  
     VALUES  
           (@Depot_ID,  
            @Service_Area_Name,  
            @Service_Area_Description,  
            @Service_Area_Notes  
            )  
	END  
END


GO

