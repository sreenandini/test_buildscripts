USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetComponentDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetComponentDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =======================================================================    
-- OUTPUT -- Component Details -- exec rsp_GetComponentDetails     
-- Revision History    
-- M Senthil 31/05/2010  Created    
-- ======================================================================= 

CREATE PROCEDURE rsp_GetComponentDetails   
(@CCD_ID int)  
AS  
SELECT 
	cd.ccd_name,
	cd.ccd_model_name,
	ct.cct_name,
	at.cat_name ,
	cd.ccd_seed_value,
	cd.ccd_hash_value 
from dbo.CV_Component_Details cd   
join dbo.CV_Component_Types CT on CD.CCD_CCT_Code = CT.CCT_Code  
join dbo.CV_Algorithm_Types at on cd.ccd_cat_code = at.cat_code   
and ccd_id =  @ccd_id
GO

