
USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_SL_GetuserName]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_SL_GetuserName]
GO
 
    
CREATE PROCEDURE rsp_SL_GetuserName    
    
AS    
BEGIN 
SET NOCOUNT ON 

SELECT  DISTINCT Staff_ID,Staff_First_Name+ ', '+Staff_Last_Name AS Staff_Name
FROM [dbo].[Staff] st 
INNER JOIN [dbo].[SL_LicenseInfo] Sll ON st.Staff_ID=Sll.ActivatedStaffID
OR st.Staff_ID=sll.CancelledStaffID  OR
st.Staff_ID = sll.CreatedStaffID

ORDER BY Staff_Name 

END    


