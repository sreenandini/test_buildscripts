USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[fnGetZoneOperationalHours]')
              AND TYPE IN (N'FN', N'IF', N'TF', N'FS', N'FT')
   )
    DROP FUNCTION [dbo].[fnGetZoneOperationalHours]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[fnGetZoneOperationalHours]
(
	@ReadDate       DATETIME,
	@SiteID         INT,
	@BarPositionID  INT
)
RETURNS INT
AS
BEGIN
	DECLARE @OperationalHours  AS INT    
	DECLARE @Day               AS VARCHAR(10)    
	SET @Day = (
	        SELECT DATENAME(dw, DATEADD(d, -1, @ReadDate))
	    )    
	
	IF @Day = 'Monday'
	    SET @OperationalHours = (
	            SELECT LEN(REPLACE(Standard_Opening_Hours_Open_Monday, '0', ''))
	                   / 4
	            FROM   Enterprise.dbo.Standard_Opening_Hours so
	                   JOIN Enterprise.dbo.Zone Z
	                        ON  Z.Standard_Opening_hours_ID = so.Standard_Opening_hours_ID
	                   JOIN Enterprise.dbo.Bar_Position bp
	                        ON  bp.Zone_ID = Z.Zone_ID
	                        AND Z.Site_ID = @SiteID
	                        AND bp.Bar_Position_ID = @BarPositionID
	        )
	ELSE 
	IF @Day = 'Tuesday'
	    SET @OperationalHours = (
	            SELECT LEN(REPLACE(Standard_Opening_Hours_Open_Tuesday, '0', ''))
	                   / 4
	            FROM   Enterprise.dbo.Standard_Opening_Hours so
	                   JOIN Enterprise.dbo.Zone Z
	                        ON  Z.Standard_Opening_hours_ID = so.Standard_Opening_hours_ID
	                   JOIN Enterprise.dbo.Bar_Position bp
	                        ON  bp.Zone_ID = Z.Zone_ID
	                        AND Z.Site_ID = @SiteID
	                        AND bp.Bar_position_ID = @BarPositionID
	        )
	ELSE 
	IF @Day = 'Wednesday'
	    SET @OperationalHours = (
	            SELECT LEN(REPLACE(Standard_Opening_Hours_Open_Wednesday, '0', ''))
	                   / 4
	            FROM   Enterprise.dbo.Standard_Opening_Hours so
	                   JOIN Enterprise.dbo.Zone Z
	                        ON  Z.Standard_Opening_hours_ID = so.Standard_Opening_hours_ID
	                   JOIN Enterprise.dbo.Bar_Position bp
	                        ON  bp.Zone_ID = Z.Zone_ID
	                        AND Z.Site_ID = @SiteID
	                        AND bp.Bar_position_ID = @BarPositionID
	        )
	ELSE 
	IF @Day = 'Thursday'
	    SET @OperationalHours = (
	            SELECT LEN(REPLACE(Standard_Opening_Hours_Open_Thursday, '0', ''))
	                   / 4
	            FROM   Enterprise.dbo.Standard_Opening_Hours so
	                   JOIN Enterprise.dbo.Zone Z
	                        ON  Z.Standard_Opening_hours_ID = so.Standard_Opening_hours_ID
	                   JOIN Enterprise.dbo.Bar_Position bp
	                        ON  bp.Zone_ID = Z.Zone_ID
	                        AND Z.Site_ID = @SiteID
	                        AND bp.Bar_position_ID = @BarPositionID
	        )
	ELSE 
	IF @Day = 'Friday'
	    SET @OperationalHours = (
	            SELECT LEN(REPLACE(Standard_Opening_Hours_Open_Friday, '0', ''))
	                   / 4
	            FROM   Enterprise.dbo.Standard_Opening_Hours so
	                   JOIN Enterprise.dbo.Zone Z
	                        ON  Z.Standard_Opening_hours_ID = so.Standard_Opening_hours_ID
	                   JOIN Enterprise.dbo.Bar_Position bp
	                        ON  bp.Zone_ID = Z.Zone_ID
	                        AND Z.Site_ID = @SiteID
	                        AND bp.Bar_position_ID = @BarPositionID
	        )
	ELSE 
	IF @Day = 'Saturday'
	    SET @OperationalHours = (
	            SELECT LEN(REPLACE(Standard_Opening_Hours_Open_Saturday, '0', ''))
	                   / 4
	            FROM   Enterprise.dbo.Standard_Opening_Hours so
	                   JOIN Enterprise.dbo.Zone Z
	                        ON  Z.Standard_Opening_hours_ID = so.Standard_Opening_hours_ID
	                   JOIN Enterprise.dbo.Bar_Position bp
	                        ON  bp.Zone_ID = Z.Zone_ID
	                        AND Z.Site_ID = @SiteID
	                        AND bp.Bar_position_ID = @BarPositionID
	        )
	ELSE 
	IF @Day = 'Sunday'
	    SET @OperationalHours = (
	            SELECT LEN(REPLACE(Standard_Opening_Hours_Open_Sunday, '0', ''))
	                   / 4
	            FROM   Enterprise.dbo.Standard_Opening_Hours so
	                   JOIN Enterprise.dbo.Zone Z
	                        ON  Z.Standard_Opening_hours_ID = so.Standard_Opening_hours_ID
	                   JOIN Enterprise.dbo.Bar_Position bp
	                        ON  bp.Zone_ID = Z.Zone_ID
	                        AND Z.Site_ID = @SiteID
	                        AND bp.Bar_position_ID = @BarPositionID
	        ) 
	
	
	RETURN @OperationalHours
END
GO

