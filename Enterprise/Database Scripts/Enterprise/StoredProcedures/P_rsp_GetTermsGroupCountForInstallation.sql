USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_GetTermsGroupCountForInstallation]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_GetTermsGroupCountForInstallation] 
GO

CREATE PROCEDURE [dbo].[rsp_GetTermsGroupCountForInstallation] 
(@TermsGroupID INT)
AS
BEGIN
 SELECT sum(CASE   
    WHEN installation_end_date IS NULL  
     THEN 1  
    END) AS active  
  ,count(installation_end_date) AS inactive  
 FROM Bar_Position  
 LEFT JOIN Installation ON Bar_Position.Bar_Position_ID = Installation.Bar_Position_ID  
 WHERE Terms_Group_ID = @TermsGroupID  
  AND installation_id IS NOT NULL  
END
GO
