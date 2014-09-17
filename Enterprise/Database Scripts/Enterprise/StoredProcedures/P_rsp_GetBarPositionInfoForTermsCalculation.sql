USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_GetBarPositionInfoForTermsCalculation]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_GetBarPositionInfoForTermsCalculation]
GO

CREATE PROCEDURE [dbo].[rsp_GetBarPositionInfoForTermsCalculation] 
(@Installation_ID INT)
AS
BEGIN
	SELECT Bar_Position.Terms_Group_ID,
	       Bar_Position.Terms_Group_Changeover_Date,
	       Bar_Position.Terms_Group_Future_ID,
	       Terms_Group_Past_ID,
	       Terms_Group_Past_Changeover_Date
	FROM   Installation
	       INNER JOIN Bar_Position
	            ON  Installation.Bar_Position_ID = Bar_Position.Bar_Position_ID
	WHERE  Installation_ID = @Installation_ID
END
GO
