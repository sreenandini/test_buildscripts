USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetBarPositionDetailsBySiteID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetBarPositionDetailsBySiteID]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 CREATE PROCEDURE rsp_GetBarPositionDetailsBySiteID
 @SiteID INT
 AS
 BEGIN

  -- Deleting Unassociated BarPositions 
  IF @SiteID = 0 
  BEGIN
	DELETE FROM Bar_Position WHERE Site_ID = 0
  END
		
  SELECT Bar_Position.Bar_Position_ID, 
		 RIGHT(REPLICATE('0',3) + Bar_Position.Bar_Position_Name,3) Bar_Position_Name, 
		 Bar_Position.Bar_Position_Location, 
		 Machine_Type.Machine_Type_Code, 
		 Machine_Class.Machine_Name, 
		 Machine_Class.Machine_BACTA_Code, 
		 Machine.Machine_Stock_No, 
		 Installation.Installation_ID, 
		 Installation.Installation_End_Date, 
		 [Zone].Zone_ID, 
		 [Zone].Zone_Name,
		 dbo.udf_GetRouteNames(Bar_Position.Bar_Position_ID) as Route_Name
   FROM 
   Bar_Position 
   LEFT JOIN Installation ON Bar_Position.Bar_Position_ID = Installation.Bar_Position_ID
   LEFT JOIN Machine ON Installation.Machine_ID = Machine.Machine_ID
   LEFT JOIN Machine_Class ON Machine.Machine_Class_ID = Machine_Class.Machine_Class_ID
   LEFT JOIN Machine_Type ON Machine_Class.Machine_Type_ID = Machine_Type.Machine_Type_ID
   LEFT JOIN [Zone] ON Bar_Position.Zone_ID = [Zone].Zone_ID AND [Zone].Site_ID=@SiteID
   WHERE Bar_Position.Site_ID = @SiteID
   
   ORDER BY 
   Bar_Position.Bar_Position_Name, 
   Bar_Position.Bar_Position_ID, 
   Installation.Installation_End_Date
   
 END


GO

