USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_NTPZeroCollection]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_NTPZeroCollection]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE  PROCEDURE [dbo].[usp_NTPZeroCollection] 
(
	@SiteID int,
	@NTPFrom varchar(30),
	@NTPTo	varchar(30),	
	@CollDays int,
	@CollTime varchar(50),
	@PrevCollTime varchar(30),
	@CollType varchar(1),
	@Remarks varchar(max)
)
AS
BEGIN

DECLARE @Count INT  
DECLARE @Start INT 
DECLARE @InstID INT
DECLARE @CollId INT

SET @Start = 1 

CREATE TABLE #Inst_Temp  
 (  
	Sno  INT IDENTITY(1,1),
	InstallationID INT
 )

INSERT INTO #Inst_Temp 
SELECT 
Installation_ID 
FROM 
Installation 
INNER JOIN 
Bar_Position 
ON 
Installation.Bar_Position_ID = Bar_Position.Bar_Position_ID 
WHERE Site_ID = @SiteID 
AND 
Installation_End_Date IS NULL

SELECT @Count = COUNT(*) FROM #Inst_Temp

	WHILE @Start <= @Count
	BEGIN
	SELECT @InstID = InstallationID FROM #Inst_Temp WHERE Sno = @Start

	INSERT INTO COLLECTION 
	(Installation_ID, Collection_Date, Collection_Time, Previous_Collection_Date, Previous_Collection_Time, Remarks)
	VALUES
	(@InstID, @NTPTo, @CollTime, @NTPFrom, @PrevCollTime, @Remarks)

	SET @CollId = SCOPE_IDENTITY()
	
	INSERT INTO Collection_Details
	(Collection_ID, Collection_Days, Collection_Type)
	VALUES
	(@CollId, @CollDays, @CollType)


	UPDATE Bar_Position 
	SET 
	Bar_Position_Last_Collection_ID = @CollId,
	Bar_Position_Last_Collection_Date = @NTPFrom
	FROM Bar_Position 
	INNER JOIN 
	Installation 
	ON 
	Bar_Position.Bar_Position_ID = Installation.Bar_Position_ID 
	WHERE Installation.Installation_ID = @InstID

	SET @Start = @Start + 1
	END

DROP TABLE #Inst_Temp

END

GO

