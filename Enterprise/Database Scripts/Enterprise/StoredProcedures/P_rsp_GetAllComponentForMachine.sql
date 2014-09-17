USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetAllComponentForMachine]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetAllComponentForMachine]
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
--- Description: Get All Component For Machine.  
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

CREATE PROCEDURE dbo.rsp_GetAllComponentForMachine  
(@Serial_No VARCHAR(30))
AS  
BEGIN   

IF(@Serial_No= 'ALL') 
	BEGIN
		SELECT CCD_ID As 'Component_ID', 
			   CCD_Name As 'Component_Name'
		FROM dbo.CV_Component_Details CD 
	END
	ELSE
	BEGIN
		SELECT CD.CCD_ID As 'Component_ID', 
			   CD.CCD_Name As 'Component_Name'	
		FROM  dbo.CV_Machine_Component_Details MCD
		INNER JOIN dbo.CV_Component_Details CD ON MCD.CVMCD_CCD_ID = CD.CCD_ID
		WHERE MCD.CVMCD_Machine_Serial_No = @Serial_No
	END
END  
  

GO

