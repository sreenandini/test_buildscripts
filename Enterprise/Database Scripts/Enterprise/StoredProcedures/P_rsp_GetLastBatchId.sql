USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetLastBatchId]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetLastBatchId]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO


CREATE PROCEDURE rsp_GetLastBatchId(@SiteId INT, @Weeks INT, @Periods INT)  
AS  
BEGIN  
 SET NOCOUNT ON              
 IF @Weeks > 0  
 BEGIN  
     SELECT DISTINCT TOP(@Weeks) Calendar_Week_ID,  
            CONVERT(DATETIME, CW.Calendar_Week_Start_Date, 103) AS Week_Start_Date   
            INTO #TempWeek  
     FROM   SITE S WITH(NOLOCK)  
            INNER JOIN Sub_Company_Calendar SC WITH(NOLOCK)  
                 ON  S.Sub_Company_ID = SC.Sub_Company_ID  
            INNER JOIN Calendar_Week CW WITH(NOLOCK)  
                 ON  SC.Calendar_ID = CW.Calendar_ID  
            INNER JOIN Collection_Details CD WITH(NOLOCK)  
                 ON  CD.Week_ID = CW.Calendar_Week_ID  
     WHERE  S.Site_ID = @SiteId  
            AND CONVERT(DATETIME, CW.Calendar_Week_Start_Date, 103) <= GETDATE()  
     ORDER BY  
            CONVERT(DATETIME, CW.Calendar_Week_Start_Date, 103) DESC  
       
     SELECT DISTINCT B.Batch_ID AS Batch_ID  
     FROM   BATCH B WITH(NOLOCK)  
            INNER JOIN COLLECTION C WITH(NOLOCK)  
                 ON  C.Batch_ID = B.Batch_ID  
            INNER JOIN Collection_Details CD  
                 ON  C.Collection_ID = CD.Collection_ID  
            INNER JOIN #TempWeek CW  
                 ON  CD.Week_ID = CW.Calendar_Week_ID  
            INNER JOIN Installation I WITH(NOLOCK)
				 ON I.Installation_ID = C.Installation_ID
			INNER JOIN Bar_Position BP WITH(NOLOCK)
				 ON BP.Bar_Position_ID = I.Bar_Position_ID
			INNER JOIN [Site] S WITH(NOLOCK)
				 ON S.Site_ID = BP.Site_ID 
     WHERE  S.Site_ID = @SiteId  
     ORDER BY  
            B.Batch_ID DESC  
       
     DROP TABLE #TempWeek   
     RETURN  
 END                
   
 IF @Periods > 0  
 BEGIN  
     SELECT TOP(@Periods) Calendar_Period_ID,  
            CONVERT(DATETIME, CP.Calendar_Period_Start_Date, 103) AS   
            Period_Start_Date   
            INTO #TempPeriod  
     FROM   SITE S WITH(NOLOCK)  
            INNER JOIN Sub_Company_Calendar SC WITH(NOLOCK)  
                 ON  S.Sub_Company_ID = SC.Sub_Company_ID  
            INNER JOIN Calendar_Period CP WITH(NOLOCK)  
                 ON  SC.Calendar_ID = CP.Calendar_ID  
            INNER JOIN Collection_Details CD  
                 ON  CD.Period_ID = CP.Calendar_Period_ID  
     WHERE  S.Site_ID = @SiteId  
            AND CONVERT(DATETIME, CP.Calendar_Period_Start_Date, 103) <= GETDATE()  
     ORDER BY  
            CONVERT(DATETIME, CP.Calendar_Period_Start_Date, 103) DESC  
       
     SELECT DISTINCT B.Batch_ID AS Batch_ID  
     FROM   BATCH B WITH(NOLOCK)  
            INNER JOIN COLLECTION C WITH(NOLOCK)
                 ON  C.Batch_ID = B.Batch_ID  
            INNER JOIN Collection_Details CD WITH(NOLOCK)  
                 ON  C.Collection_ID = CD.Collection_ID  
            INNER JOIN #TempPeriod CW  
                 ON  CD.Period_ID = CW.Calendar_Period_ID
            INNER JOIN Installation I WITH(NOLOCK)
				 ON I.Installation_ID = C.Installation_ID
			INNER JOIN Bar_Position BP WITH(NOLOCK)
				 ON BP.Bar_Position_ID = I.Bar_Position_ID
			INNER JOIN [Site] S WITH(NOLOCK)
				 ON S.Site_ID = BP.Site_ID 
     WHERE  S.Site_ID = @SiteId  
     ORDER BY  
            B.Batch_ID DESC   
       
     DROP TABLE #TempPeriod   
     RETURN  
 END

IF (@Weeks = -1 or @Periods = -1)
	 BEGIN
		 SELECT DISTINCT B.Batch_ID AS Batch_ID 
		 FROM   BATCH B WITH(NOLOCK)
			INNER JOIN COLLECTION C WITH(NOLOCK)
                 ON  C.Batch_ID = B.Batch_ID
			INNER JOIN Installation I WITH(NOLOCK)
				 ON I.Installation_ID = C.Installation_ID
			INNER JOIN Bar_Position BP WITH(NOLOCK)
				 ON BP.Bar_Position_ID = I.Bar_Position_ID
			INNER JOIN [Site] S WITH(NOLOCK)
				 ON S.Site_ID = BP.Site_ID 
		 WHERE  S.Site_ID = @SiteId  
		 ORDER BY  
				Batch_ID DESC  
	 END
END   

GO

