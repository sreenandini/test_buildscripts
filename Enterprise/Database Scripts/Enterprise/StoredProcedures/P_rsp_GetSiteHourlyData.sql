USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetSiteHourlyData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetSiteHourlyData]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*  
 * this stored procedure is to export the Hourly details for X weeks to the corresponding Exchange  
 * Change History:  --EXEC dbo.rsp_GetSiteHourlyData 1012,2   
 * Vineetha M  19 Sep 2009  Created
 * Vineetha M  27 jan 2010  Modified To add count(duplicate)
 * Anil		   11 Mar 2011  Added new column named HS_CreditInd to fix CR 93121
*/  
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  

CREATE PROCEDURE rsp_GetSiteHourlyData
	@Site_Code VARCHAR(50) ,
	@XDays int
 
AS  
  
BEGIN  

	DECLARE @dttopdate  DATETIME	
	DECLARE @fetchdate DATETIME
	DECLARE @siteid INT
	DECLARE @xml XML

	SELECT @Siteid = site_id FROM SITE WHERE site_code = @Site_Code
	
		IF @Siteid > 0
	
			BEGIN

				SELECT  @dtTopDate = HS_Date FROM Hourly_Statistics WHERE HS_ID=(SELECT MAX(HS_ID) FROM Hourly_Statistics HS
				INNER JOIN Installation I ON HS.HS_Installation_No = I.Installation_ID 
				INNER JOIN Bar_Position BP ON I.Bar_Position_ID = BP.Bar_Position_ID
				INNER JOIN [Site] S ON BP.Site_ID = S.Site_ID WHERE S.Site_ID = @Siteid)

				SET @FetchDate=DATEADD(dd, -@XDays, @dtTopDate)

				SET @xml = 	(SELECT 
							HS_ID,							
							HS_Installation_No,
							HS_Date,
							HS_Type,
							HS_MoneyInd,
							HS_Hour1,
							HS_Hour2,
							HS_Hour3,
							HS_Hour4,
							HS_Hour5,
							HS_Hour6,
							HS_Hour7,
							HS_Hour8,
							HS_Hour9,
							HS_Hour10,
							HS_Hour11,
							HS_Hour12,
							HS_Hour13,
							HS_Hour14,
							HS_Hour15,
							HS_Hour16,
							HS_Hour17,
							HS_Hour18,
							HS_Hour19,
							HS_Hour20,
							HS_Hour21,
							HS_Hour22,
							HS_Hour23,
							HS_Hour24,
							HS_CreditInd  	
						FROM	Hourly_Statistics  where HS_ID IN
						(SELECT								
							H.HS_ID							
							FROM	Hourly_Statistics  H
								JOIN Installation i on i.Installation_ID = H.HS_Installation_No
								JOIN Bar_Position b on i.Bar_Position_ID = b.Bar_Position_ID
								JOIN Site S on b.Site_ID = S.Site_ID
							WHERE  H.HS_Date >= @FetchDate and S.Site_id = @Siteid 					
							GROUP BY H.HS_ID
							HAVING Count(cast(H.HS_ID as varchar(5))+cast(H.HS_Installation_No as varchar(5))+ cast(H.HS_Date as varchar(50)))<=1
						)
					ORDER BY 1 DESC
					FOR XML PATH ('HOURLY'), ELEMENTS, ROOT('HOURLYROOT') )

					SELECT @xml 
			 END

END


GO

