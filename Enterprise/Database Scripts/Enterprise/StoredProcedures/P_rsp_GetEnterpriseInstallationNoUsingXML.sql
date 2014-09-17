USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetEnterpriseInstallationNoUsingXML]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetEnterpriseInstallationNoUsingXML]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO



/****** Object:  StoredProcedure [dbo].[rsp_GetEnterpriseInstallationDetailsUsingXML]    Script Date: 02/20/2008 20:22:26 ******/
/***********

Author:			Rakesh Marwaha
Dated:			23 May 2008
Description :	This will take Exchange Installation Details XML as input and return Enterprise Installation Number along with Exchange Installation Details in XML.
Modified	:	04 June 2008	Site ID added				
************/



CREATE PROCEDURE [dbo].[rsp_GetEnterpriseInstallationNoUsingXML] 

--@doc	varchar(8000)
@doc	varchar(MAX)
As
set dateformat dmy

Declare	@idoc				int

--Use Start Date, Start Time, End Date, End Time,Machine,BarPosNo to get Installation No
--then add it(Installation No) to existing Installation Details XML.

----Testing
--declare @doc		varchar(8000)
--set @doc='
--<Installation_Details>
--  <Installation_Detail>
--    <Exchange_Installation_No>1162</Exchange_Installation_No>
--    <start_date>31 Jul 2006</start_date>
--    <start_time>14:59</start_time>
--    <end_date>02 Aug 2006</end_date>
--    <end_time>08:49</end_time>
--    <bar_pos_name>002</bar_pos_name>
--    <Site_Code>1001</Site_Code>
--    <Machine_Name>Lobstermania (�4000/1p)</Machine_Name>
--  </Installation_Detail>
--  <Installation_Detail>
--    <Exchange_Installation_No>1163</Exchange_Installation_No>
--    <start_date>31 Jul 2006</start_date>
--    <start_time>15:04</start_time>
--    <end_date>02 Aug 2006</end_date>
--    <end_time>08:49</end_time>
--    <bar_pos_name>003</bar_pos_name>
--    <Site_Code>1001</Site_Code>
--    <Machine_Name>Cleopatra 5RV ( �2000/10p)</Machine_Name>
--  </Installation_Detail>
--</Installation_Details>
--'
--
----Till Here

SET @doc='<?xml version="1.0" encoding="ISO-8859-1"?>' + @doc

EXEC sp_xml_preparedocument @idoc OUTPUT, @doc

BEGIN

	select * 
	into #tempTable
	FROM    OPENXML (@idoc, '/Installation_Details/Installation_Detail',2)
		WITH 
		(	
			Exchange_Installation_No	varchar(30)		'./Exchange_Installation_No',
			start_date					varchar(30)		'./start_date',
			start_time					varchar(30)		'./start_time',
			end_date					varchar(30)		'./end_date',		
	-- Need not to worry, if end_date doesnot exist, as it will be considered Null in that case
			end_time					varchar(30)		'./end_time',
			bar_pos_name				varchar(30)		'./bar_pos_name',
			Site_Code					varchar(30)		'./Site_Code',
			Machine_Name				varchar(30)		'./Machine_Name'
		 )

	EXEC sp_xml_removedocument @idoc

	alter table #tempTable add Enterprise_Installation_No varchar(30)

	update #tempTable set Enterprise_Installation_No=( 
	select installation.Installation_ID from installation
	where #tempTable.start_date=installation.installation_start_date
	and #tempTable.start_time=installation.installation_start_time
	and #tempTable.end_date=installation.installation_end_date
	and #tempTable.end_time=installation.installation_end_time
	and #tempTable.bar_pos_name=(select bar_position.bar_position_name 
	from bar_position where bar_position.Bar_Position_ID=installation.Bar_Position_ID)
	and #tempTable.Site_Code=(select top 1 Site.Site_Code 
	from Site inner join Bar_Position on Site.Site_ID=Bar_Position.Site_ID
	inner join installation on Bar_Position.Bar_Position_ID=Installation.Bar_Position_ID
	where bar_position.Bar_Position_ID=installation.Bar_Position_ID)
	and #tempTable.Machine_Name=(select Machine_Class.Machine_Name from Machine_Class
	inner join Machine on Machine.Machine_Class_Id=Machine_Class.Machine_Class_Id
	where Machine.Machine_Id=installation.Machine_ID)
	)

	select * from #tempTable for XML Path('Installation_Detail'),Type,Root('Installation_Details')
	drop table #tempTable

END



GO

