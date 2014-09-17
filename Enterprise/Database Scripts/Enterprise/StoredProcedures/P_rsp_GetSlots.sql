/************************************************************
 * Code formatted by SoftTree SQL Assistant © v6.3.171
 * Time: 7/22/2013 6:57:02 PM
 ************************************************************/

USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_GetSlots]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_GetSlots]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--------------------------------------------------------------------------   
--  
-- Description: Fetches the current version of BMC

-- =======================================================================  
--   
-- Revision History  

-- Kirubakar S 12/05/2010 Created
  
--------------------------------------------------------------------------- 

CREATE PROCEDURE rsp_GetSlots
	@SITE INT,
	@CompanyID INT,
	@SubCompanyId INT
AS
BEGIN
	IF ISNULL(@SITE, 0) = 0
		    SET @SITE = NULL
	IF ISNULL(@CompanyID, 0) = 0
			SET @CompanyID = NULL
	IF ISNULL(@SubCompanyId, 0) = 0
			SET @SubCompanyId = NULL
	
	SELECT  DISTINCT M.Machine_Stock_No
	FROM   MACHINE M WITH (NOLOCK)
	INNER JOIN Installation i WITH (NOLOCK)
			ON  m.machine_id = i.Machine_ID
	INNER JOIN Bar_Position BP WITH (NOLOCK)
			ON  i.Bar_Position_ID = bp.Bar_Position_ID
	INNER JOIN Site S 
			ON BP.Site_ID = S.Site_ID 
    INNER JOIN Sub_Company SC 
			ON S.Sub_Company_ID = SC.Sub_Company_ID 
    INNER JOIN Company C 
			ON SC.Company_ID = C.Company_ID 
	WHERE  
	    bp.Site_ID = ISNULL(@SITE, S.Site_ID) 
	AND C.Company_ID = ISNULL(@CompanyId,C.Company_ID)
	AND SC.Sub_Company_ID = ISNULL(@SubCompanyId,S.Sub_Company_ID)
	
	ORDER BY Machine_Stock_No
	
	

	
END
GO

