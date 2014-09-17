USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetShareSchedule]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetShareSchedule]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*    
* Revision History    
*     
* <Name of the owner>         DateCreated       Type(Created/Modified)  Description    
        Kishore S             23-jun-2014             Created          This SP is used to Read details from Share_Schedule table                                                                       based on Staff_ID.    
*/    
CREATE PROCEDURE [rsp_GetShareSchedule]
@Share_Schedule_ID int

AS
SET NOCOUNT ON
	IF @Share_Schedule_ID is null or @Share_Schedule_ID=0
	BEGIN
	SELECT ss.Share_Schedule_ID,
	ss.Share_Schedule_Name ,
	ss.Share_Schedule_Start_Date,
	ss.Share_Schedule_End_Date,
	ss.Share_Schedule_Description,
	bandcount as [Share_Schedule_No_Bands],
	ss.Share_Schedule_Bands_Name_Type,
	ss.Share_Machine_Change_Date
	from 
	(SELECT COUNT(*) as bandcount,ssa.Share_Schedule_ID
	 FROM share_schedule ssa LEFT JOIN share_band sb
	 ON ssa.Share_Schedule_ID=sb.Share_Schedule_ID
	 WHERE ssa.Share_Schedule_End_Date Is Null 
	 GROUP BY ssa.Share_Schedule_ID
	 ) as temp join share_schedule ss on temp.Share_Schedule_ID=ss.Share_Schedule_ID
	ORDER BY ss.Share_Schedule_Name asc
	END
		
	ELSE
	BEGIN
	SELECT Share_Schedule_ID,
	Share_Schedule_Name,
	Share_Schedule_Start_Date,
	Share_Schedule_End_Date,
	Share_Schedule_Description,
	Share_Schedule_No_Bands,
	Share_Schedule_Bands_Name_Type,
	Share_Machine_Change_Date
	FROM share_schedule 
	WHERE Share_Schedule_ID=@Share_Schedule_ID
	END
GO
