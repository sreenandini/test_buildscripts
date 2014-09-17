USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_GetBarpositions]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_GetBarpositions]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



--Bar Positions
Create Proc [dbo].[usp_GetBarpositions](@Site_Id int)
--With ENCRYPTION
As
Select DISTINCT
Bar_Position_ID as HQ_Bar_Position_ID,
Bar_Position_Name as Bar_Pos_Name ,
Bar_Position_Location as Location,
Bar_Position.Site_Id as Site_No,
Zone_Id as Zone_No,
Bar_Position.Depot_Id,
CONVERT(DATETIME, Bar_Position_Start_Date, 101) as Start_Date,
CONVERT(DATETIME, Bar_Position_End_Date, 101) as End_Date	,
Bar_Position_Collection_Day as Collection_Day,
Bar_Position_Machine_Enabled as Bar_Position_Machine_Enabled,
Bar_Position_Note_Acceptor_Enabled as Bar_Position_Note_Acceptor_Enabled,
'0' as FLOORTOP,
'0' as FLOORLEFT
from Bar_Position 
Join Site on Bar_Position.Site_Id = Site.Site_Id
	where Site_Code=@Site_Id and coalesce(Bar_Position_End_Date,'')=''
	For XML Path('Bar_Position'),ROOT('Bar_Positions'),TYPE, ELEMENTS XSINIL


GO

