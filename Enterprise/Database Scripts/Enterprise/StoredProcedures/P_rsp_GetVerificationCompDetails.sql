USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetVerificationCompDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetVerificationCompDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

    
--------------------------------------------------------------------------      
--------------------------------------------------------------------------       
---      
--- Description: Get Comp Verification Records.      
---      
--- Inputs:      see inputs      
---      
--- Outputs:           
---       
--- =======================================================================      
---       
--- Revision History      
---       
--- Senthil  07/06/10     Created       
---------------------------------------------------------------------------       
CREATE PROCEDURE dbo.rsp_GetVerificationCompDetails  
(@FromDate DATETIME,  
@ToDate DATETIME,  
@ComponentType VARCHAR(50),
@VerificationType VARCHAR(50),
@SiteName VARCHAR(50),  
@SeriaNo VARCHAR(50),  
@CompName VARCHAR(50))  
AS      
BEGIN          
  
IF LEN(ISNULL(@CompName,'')) = 0 SET @CompName = ''  
IF LEN(ISNULL(@ComponentType,'')) = 0 SET @ComponentType = ''  
IF LEN(ISNULL(@SeriaNo,'')) = 0 SET @SeriaNo = ''  
IF LEN(ISNULL(@SiteName,'')) = 0 SET @SiteName = ''   
IF LEN(ISNULL(@VerificationType, '')) = 0 SET @VerificationType = '' 
  
SELECT S.Site_Name As 'Site Name', 
	VD.CVD_Installation_No As 'Installation No',    
	VD.CVD_Machine_Serial_No as 'Serial No',    
	CT.CCT_Name As 'Component Type',    
	CD.CCD_Name As 'Component Name',    
	VT.CVT_Name As 'Verification Type',    
	VD.CVD_Verification_Time As 'Verification Time',    
	CASE VD.CVD_Verification_Status WHEN 1 THEN 'PASSED' ELSE 'FAILED' END AS 'Verification Status'    
FROM  dbo.CV_Verification_Details VD     
INNER JOIN dbo.CV_Component_Details CD ON VD.CVD_CCD_ID = CD.CCD_ID    
INNER JOIN dbo.CV_Component_Types CT ON CD.CCD_CCT_Code = CT.CCT_Code
INNER JOIN dbo.Site S ON VD.CVD_Site_Code = S.Site_Code   
INNER JOIN dbo.CV_Verification_Types VT ON VD.CVD_Verification_Type = VT.CVT_Code 
WHERE CVD_Verification_Time BETWEEN @FromDate AND @ToDate  
AND (CD.CCD_ID = @CompName OR @CompName = '')    
AND (CT.CCT_Code = @ComponentType OR @ComponentType = '')  
AND (VD.CVD_Machine_Serial_No = @SeriaNo OR @SeriaNo = '')  
AND (S.Site_ID = @SiteName OR @SiteName = '')  
AND (VT.CVT_Code = @VerificationType OR @VerificationType = '') 
ORDER BY CVD_Verification_Time DESC 
   
END        



GO

