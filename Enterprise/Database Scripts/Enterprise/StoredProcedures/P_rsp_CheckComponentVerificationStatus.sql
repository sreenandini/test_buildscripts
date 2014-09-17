USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_CheckComponentVerificationStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_CheckComponentVerificationStatus]
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
--- Description: Check Component Verification Status
---  
--- Inputs:      see inputs  
---  
--- Outputs:       
---   
--- =======================================================================  
---   
--- Revision History  
---   
--- Senthil  08/06/10     Created   
---------------------------------------------------------------------------   
CREATE PROCEDURE dbo.rsp_CheckComponentVerificationStatus  
(
@Serial_No VARCHAR(30),
@ComponentID INT,
@Success INT OUTPUT
)
AS  
SET @Success = 0
BEGIN    
 IF EXISTS (SELECT CVMCD_Machine_Serial_No
				FROM   dbo.CV_Machine_Component_Details MCD
				WHERE CVMCD_CCD_ID = @ComponentID
				AND CVMCD_Machine_Serial_No = @Serial_No
				AND CVMCD_Request_Verification = 0
				AND CVMCD_IsAvailable = 0)
RETURN 1
END



GO

