USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetMachineAFTEnabledSetting]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetMachineAFTEnabledSetting]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


Create PROCEDURE rsp_GetMachineAFTEnabledSetting  
(@Installation_ID int)  
AS  
  
SELECT ISNULL(ISAFTENABLED,0) AS AFTENABLED,I.Installation_ID as HQ_ID FROM MACHINE  M  
INNER JOIN INSTALLATION I  
ON M.MACHINE_ID= I.MACHINE_ID AND   
I.INSTALLATION_ID=@Installation_ID  
for xml path('AFTInfo'), root('AFT')  




GO

