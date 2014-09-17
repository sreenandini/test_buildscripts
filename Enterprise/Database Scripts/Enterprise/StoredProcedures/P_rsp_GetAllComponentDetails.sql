USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetAllComponentDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetAllComponentDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =======================================================================    
-- OUTPUT --Select all Component details -- exec rsp_GetAllComponentDetails     
-- Revision History    
-- M Senthil 31/05/2010  Created    
-- ======================================================================= 

CREATE PROCEDURE rsp_GetAllComponentDetails
AS 
SELECT CT.CCt_Code,  
CT.CCT_Name,  
isnull(CD.CCD_ID,0) As CCD_ID,  
isnull(CD.CCD_Name,'') As CCD_Name,  
ISNULL(CD.CCD_Model_Name,'') As CCD_Model_Name  
FROM dbo.CV_Component_Types CT  
LEFT JOIN dbo.CV_Component_Details CD ON CD.CCD_CCT_Code = CT.CCT_Code  
ORDER BY CT.CCT_Name  

GO

