USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetActiveInstallationFromMachineID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetActiveInstallationFromMachineID]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*  
 * Revision History  
 *   
 * <Name of the owner>         DateCreated       Type(Created/Modified)  Description  
   Kalaiyarasan.P              05-NOV-2012         Created               This SP is used to get active installation details   
                                                                         based on Machine_ID. 
Exec  rsp_GetActiveInstallationFromMachineID  111
*/  
CREATE PROCEDURE rsp_GetActiveInstallationFromMachineID
	@Machine_ID INT,
	@Installation_ID INT OUTPUT,
	@Site_Code VARCHAR(50) OUTPUT
AS
BEGIN
	
	
	SELECT @Site_Code=S.Site_Code ,@Installation_ID = i.Installation_ID
	FROM   SITE S WITH(NOLOCK)
	       INNER JOIN Bar_Position BP WITH(NOLOCK)
	            ON  S.Site_Id = Bp.Site_Id
	       INNER JOIN Installation i
	            ON  i.Bar_Position_ID = bp.Bar_Position_ID
	WHERE  installation_end_date IS NULL	     
	       AND i.Machine_ID = @Machine_ID
	           
	           --END
END


GO

