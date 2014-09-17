USE Enterprise
GO 
-- Drop stored procedure if it already exists
IF EXISTS (
       SELECT *
       FROM   INFORMATION_SCHEMA.ROUTINES
       WHERE  SPECIFIC_SCHEMA = N'dbo'
              AND SPECIFIC_NAME = N'usp_Route_UpdateRouteFromXML'
   )
    DROP PROCEDURE dbo.usp_Route_UpdateRouteFromXML
GO

CREATE PROCEDURE usp_Route_UpdateRouteFromXML
	@Site_ID INT,
	@doc XML
AS
BEGIN
	/*****************************************************************************************************      
	DESCRIPTION :   For Updating Route Details in Enterprise from Exchange,will be updated at the time of Migration.       
	This SP will be executed from rsp_GetCommonData.sql      
	CREATED DATE:   30th-Jan-2014      
	MODULE  :   Route       
	SAMPLE  :  '<RM>      
					<row RN="MONDAY (50 - 80)" BP="667" sC="1555"/>      
					<row RN="MONDAY (50 - 80)" BP="668" sC="1555"/>      
					<row RN="MONDAY (50 - 80)" BP="669" sC="1555"/>      
				</RM>'      
	
	------------------------------------------------------------------------------------------------------      
	AUTHOR     DESCRIPTON          MODIFIED DATE      
	------------------------------------------------------------------------------------------------------      
	
	*****************************************************************************************************/ 
	
	--Declaring a Temp table variable      
	DECLARE @temp TABLE (
	            RouteName VARCHAR(50),
	            BarPosition VARCHAR(50),
	            SiteCode VARCHAR(10),
	            site_id INT,
	            Bar_Position_ID INT,
	            route_id INT
	        ) 
	
	--SELECT @doc
	--Inserting Data in the temp table      
	INSERT INTO @temp
	SELECT b.value('@RN', 'varchar(30)'),
	       b.value('@BPN', 'varchar(30)'),
	       b.value('@SC', 'varchar(10)'),
	       s.site_id,
	       bp.Bar_Position_ID,
	       NULL
	FROM   @doc.nodes('/RM') AS XMLDATA(fileds)
	       CROSS APPLY Fileds.nodes('row') AS a(b) 
	INNER JOIN SITE s
	            ON  s.Site_ID = @Site_ID
	       INNER  JOIN Bar_Position bp
	            ON  bp.Site_ID = s.Site_ID
	            AND bp.Bar_Position_Name = b.value('@BPN', 'varchar(30)') 
	
	BEGIN TRAN      
	
	DELETE 
	FROM   [Route_MEMBER]
	WHERE  route_id IN (SELECT route_id
	                    FROM   [Route]
	                    WHERE  site_id = @Site_id)      
	
	DELETE 
	FROM   [Route]
	WHERE  site_id = @Site_id      
	
	INSERT INTO [Route]
	  (
	    Route_Name,
	    CreatedAt,
	    [Active],
	    [User_id],
	    Site_ID
	  )
	SELECT DISTINCT RouteName,
	       GETDATE(),
	       1,
	       1,
	       site_id
	FROM   @temp      
	
	INSERT INTO Route_Member
	  (
	    Route_ID,
	    Bar_Position_ID,
	    Route_Order
	  )
	SELECT DISTINCT r.Route_ID,
	       bp.Bar_Position_ID,
	       0
	FROM   [Route] r
	       INNER JOIN @temp t
	            ON  r.Route_Name = t.RouteName
	       INNER JOIN Bar_Position bp
	            ON  bp.Bar_Position_Name = t.BarPosition 
	Where  bp.Site_ID =@Site_id
	--RETURN DATA       
	DECLARE @Data VARCHAR(MAX)      
	
	SET @Data = (
	        SELECT R.Route_ID R_ID,
	               r.Route_Name RN,
	               r.CreatedAt CA,
	               r.[Active] A,
	               rm.Bar_Position_ID BP,
	               rm.Route_Member_ID RMI
	        FROM   [ROUTE] r
	               INNER JOIN route_member rm
	                    ON  r.Route_ID = rm.Route_ID
	        WHERE  r.Site_ID = @Site_id
	               FOR XML RAW,
	               ROOT('RM')
	    ) 
	--Return       
	
	IF (@@ERROR <> 0)
	BEGIN
	    ROLLBACK TRAN      
	    SELECT ''
	END
	ELSE
	BEGIN
	    COMMIT TRAN      
	    SELECT @Data
	END
END
GO