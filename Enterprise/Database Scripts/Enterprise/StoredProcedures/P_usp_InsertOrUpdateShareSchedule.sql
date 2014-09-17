USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_InsertOrUpdateShareSchedule]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_InsertOrUpdateShareSchedule]
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
        Kishore S             15-May-2014             Created          This SP is used to insert/update  Share_schedule table                                                                       based on Staff_ID.    
*/    
CREATE PROCEDURE usp_InsertOrUpdateShareSchedule
	@Share_Schedule_ID int,
	@Share_Schedule_Name VARCHAR(50) ,
	@Share_Schedule_Description VARCHAR(50),
	@Share_Schedule_No_Bands int,
	@Share_Schedule_Bands_Name_Type VARCHAR(10),
	@Share_Schedule_IDOut INT OUTPUT
AS
SET NOCOUNT OFF

	IF NOT EXISTS ( SELECT 1 FROM [dbo].[Share_Schedule] ss WHERE (ss.Share_Schedule_ID=@Share_Schedule_ID))
	BEGIN
	        INSERT INTO [dbo].[Share_Schedule]
	        (Share_Schedule_Name,Share_Schedule_Start_Date,Share_Schedule_Description ,Share_Schedule_No_Bands,Share_Schedule_Bands_Name_Type)
	        VALUES (@Share_Schedule_Name,CONVERT(VARCHAR(14), GETDATE(), 106) ,@Share_Schedule_Description,@Share_Schedule_No_Bands,@Share_Schedule_Bands_Name_Type);
	        SELECT @Share_Schedule_IDOut=(SELECT MAX(Share_Schedule_ID) FROM [dbo].[Share_Schedule]);
	 END   
	 ELSE
	 BEGIN
	        UPDATE [dbo].[Share_Schedule]
	        SET  Share_Schedule_Name=@Share_Schedule_Name,
	        Share_Schedule_Description=@Share_Schedule_Description,
	        Share_Schedule_No_Bands=@Share_Schedule_No_Bands,
	        Share_Schedule_Bands_Name_Type=@Share_Schedule_Bands_Name_Type
	        WHERE  Share_Schedule_ID=@Share_Schedule_ID;
	        SELECT @Share_Schedule_IDOut=@Share_Schedule_ID;
	 END
GO
