USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[fnServiceGetDownTime]')
              AND TYPE IN (N'FN', N'IF', N'TF', N'FS', N'FT')
   )
    DROP FUNCTION [dbo].[fnServiceGetDownTime]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-------------------------------------------------------------------------- 
--
-- Description: fetches the downtime
--
-- Inputs:     Date and open hours for all days
-- Outputs:     
--
-- =======================================================================
-- 
-- Revision History
-- 
-- Sudarsan S	 24/08/2009   Created
-- Lekha B		22/08/2014	  Modified for better performance
--------------------------------------------------------------------------- 
CREATE FUNCTION [dbo].[fnServiceGetDownTime]
(
	@StartDate  DATETIME,
	@Mon        VARCHAR(100),
	@Tue        VARCHAR(100),
	@Wed        VARCHAR(100),
	@Thu        VARCHAR(100),
	@Fri        VARCHAR(100),
	@Sat        VARCHAR(100),
	@Sun        VARCHAR(100),
	@EndDate    DATETIME = NULL
)
RETURNS VARCHAR(20)
AS
BEGIN
	DECLARE @Week            VARCHAR(700)
	DECLARE @Start           VARCHAR(MAX)
	DECLARE @End             VARCHAR(MAX)
	DECLARE @SoFar           INT
	DECLARE @Return          VARCHAR(20)
	DECLARE @LenOfWeek       INT
	DECLARE @NoOfWeeks       INT
	DECLARE @TimeString      VARCHAR(MAX)
	DECLARE @StartDateName   VARCHAR(10)
	DECLARE @EndDateName     VARCHAR(10)
	DECLARE @DayRecord       VARCHAR(100)
	DECLARE @StartDateAlone  DATETIME
	DECLARE @EndDateAlone    DATETIME
	
	SET @SoFar = 0
	
	IF (@EndDate IS NULL)
	BEGIN
	    SET @EndDate = GETDATE()
	END	
	
	SET @StartDateName = DATENAME(dw, @StartDate)
	SET @EndDateName = DATENAME(dw, @EndDate)
	
	IF (DATEDIFF(MINUTE, @StartDate, @EndDate) = 0)
	BEGIN
	    SET @Return = '00:00'
	    RETURN @Return
	END
	
	SET @StartDateAlone = CONVERT(DATETIME, CONVERT(VARCHAR, @StartDate, 106), 101)
	SET @EndDateAlone = CONVERT(DATETIME, CONVERT(VARCHAR, @EndDate, 106), 101)
	
	--For Service Call Opened Today
	IF (@StartDateAlone = @EndDateAlone)
	BEGIN
	    SELECT @DayRecord = CASE 
	                             WHEN @StartDateName = 'TUESDAY' THEN @Tue
	                             WHEN @StartDateName = 'WEDNESDAY' THEN @Wed
	                             WHEN @StartDateName = 'THURSDAY' THEN @Thu
	                             WHEN @StartDateName = 'FRIDAY' THEN @Fri
	                             WHEN @StartDateName = 'SATURDAY' THEN @Sat
	                             WHEN @StartDateName = 'SUNDAY' THEN @Sun
	                             ELSE @Mon
	                        END
	    
	    SET @TimeString = SUBSTRING(
	            ISNULL(@DayRecord, REPLICATE('0', 96)),
	            (DATEDIFF(MINUTE, @StartDateAlone, @StartDate) / 15) + 1,
	            (
	                (DATEDIFF(MINUTE, @EndDateAlone, @EndDate) / 15) -(DATEDIFF(MINUTE, @StartDateAlone, @StartDate) / 15)
	            ) + 1
	        )
	END
	ELSE
	BEGIN
	    --For Service Call Opened more than one day
	    SET @Week = ISNULL(@Mon, REPLICATE('0', 96)) + ISNULL(@Tue, REPLICATE('0', 96)) 
	        + ISNULL(@Wed, REPLICATE('0', 96))
	        + ISNULL(@Thu, REPLICATE('0', 96)) + ISNULL(@Fri, REPLICATE('0', 96)) 
	        + ISNULL(@Sat, REPLICATE('0', 96))
	        + ISNULL(@Sun, REPLICATE('0', 96))
	    
	    IF (LEN(REPLACE(@Week, '0', '')) = 0)
	    BEGIN
	        SET @SoFar = DATEDIFF(MINUTE, @StartDate, @EndDate)
	        SET @Return = CASE 
	                           WHEN LEN(CAST(@SoFar / 60 AS VARCHAR)) = 1 THEN 
	                                '0' +
	                                CAST(@SoFar / 60 AS VARCHAR)
	                           ELSE CAST(@SoFar / 60 AS VARCHAR)
	                      END + ':' +
	            CASE 
	                 WHEN LEN(CAST(@SoFar%60 AS VARCHAR)) = 1 THEN '0' + CAST(@SoFar%60 AS VARCHAR)
	                 ELSE CAST(@SoFar%60 AS VARCHAR)
	            END 
	        
	        RETURN @Return
	    END
	    
	    SET @LenOfWeek = LEN(@Week) 
	    
	    SELECT @Start = CASE 
	                         WHEN @StartDateName = 'TUESDAY' THEN SUBSTRING(@Week, 97, @LenOfWeek)
	                         WHEN @StartDateName = 'WEDNESDAY' THEN SUBSTRING(@Week, (96 * 2) + 1, @LenOfWeek)
	                         WHEN @StartDateName = 'THURSDAY' THEN SUBSTRING(@Week, (96 * 3) + 1, @LenOfWeek)
	                         WHEN @StartDateName = 'FRIDAY' THEN SUBSTRING(@Week, (96 * 4) + 1, @LenOfWeek)
	                         WHEN @StartDateName = 'SATURDAY' THEN SUBSTRING(@Week, (96 * 5) + 1, @LenOfWeek)
	                         WHEN @StartDateName = 'SUNDAY' THEN SUBSTRING(@Week, (96 * 6) + 1, @LenOfWeek)
	                         ELSE @Week
	                    END			 
	    
	    SELECT @End = CASE 
	                       WHEN @EndDateName = 'MONDAY' THEN SUBSTRING(@Week, 1, @LenOfWeek -(96 * 6))
	                       WHEN @EndDateName = 'TUESDAY' THEN SUBSTRING(@Week, 1, @LenOfWeek -(96 * 5))
	                       WHEN @EndDateName = 'WEDNESDAY' THEN SUBSTRING(@Week, 1, @LenOfWeek -(96 * 4))
	                       WHEN @EndDateName = 'THURSDAY' THEN SUBSTRING(@Week, 1, @LenOfWeek -(96 * 3))
	                       WHEN @EndDateName = 'FRIDAY' THEN SUBSTRING(@Week, 1, @LenOfWeek -(96 * 2))
	                       WHEN @EndDateName = 'SATURDAY' THEN SUBSTRING(@Week, 1, @LenOfWeek -96)
	                       ELSE @Week
	                  END 						 				 
	    
	    SELECT @Start = SUBSTRING(
	               @Start,
	               (DATEDIFF(MINUTE, @StartDateAlone, @StartDate) / 15) + 1,
	               LEN(@Start)
	           )
	    
	    SET @NoOfWeeks = DATEDIFF(
	            week,
	            DATEADD(DAY, -1, CAST(@StartDate AS SMALLDATETIME)),
	            DATEADD(DAY, -1, CAST(@EndDate AS SMALLDATETIME))
	        );
	    
	    IF (@NoOfWeeks >= 1)
	    BEGIN
	        SET @TimeString = @Start + ISNULL(REPLICATE(@Week, @NoOfWeeks -1),'') + @End
	    END
	    ELSE
	    BEGIN
	        SET @TimeString = SUBSTRING(
	                @Start,
	                1,
	                (DATEDIFF(MINUTE, @StartDate, @EndDate) / 15) + 1
	        )
	    END
	END 
	
	SET @SoFar = LEN(REPLACE(@TimeString, '0', '')) * 15
	
	IF (@SoFar = 0)
	BEGIN
	    SET @SoFar = DATEDIFF(MINUTE, @StartDate, @EndDate)
	END
	ELSE IF(@SoFar>DATEDIFF(MINUTE, @StartDate, @EndDate))
	BEGIN
		--Since data only availbale for 15 min interval, the value may exceeds +15 min, so to avoid that
		SET @SoFar = DATEDIFF(MINUTE, @StartDate, @EndDate)
	END
	
	SET @Return = CASE 
	                   WHEN LEN(CAST(@SoFar / 60 AS VARCHAR)) = 1 THEN '0' +
	                        CAST(@SoFar / 60 AS VARCHAR)
	                   ELSE CAST(@SoFar / 60 AS VARCHAR)
	              END + ':' +
	    CASE 
	         WHEN LEN(CAST(@SoFar%60 AS VARCHAR)) = 1 THEN '0' + CAST(@SoFar%60 AS VARCHAR)
	         ELSE CAST(@SoFar%60 AS VARCHAR)
	    END 
	
	RETURN @Return
END
GO

