USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(
                  N'[dbo].[rsp_GetAccessoryInstallationInfoForTermsCalculation]'
              )
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_GetAccessoryInstallationInfoForTermsCalculation]
GO

CREATE PROCEDURE [dbo].[rsp_GetAccessoryInstallationInfoForTermsCalculation] 
(@Bar_Position_ID INT)
AS
BEGIN
	SELECT Accessory_Installation_ID,
	       Bar_Position_ID,
	       Accessory_ID,
	       Accessory_Installation_Start_Date,
	       Accessory_Installation_End_Date,
	       Accessory_Installation_Serial_No,
	       Accessory_Installation_Supplier_Position,
	       Accessory_Installation_Company_Position,
	       Accessory_Installation_Meters_In,
	       Accessory_Installation_Meters_Out,
	       Accessory_Installation_Amedis_Import_Log_ID,
	       Accessory_Installation_Amedis_Import_Log_Withdrawl_ID,
	       Accessory_Installation_Charge
	FROM   Accessory_Installation
	WHERE  Bar_Position_ID = @Bar_Position_ID
	       AND Accessory_Installation_End_Date IS NULL
END
GO
