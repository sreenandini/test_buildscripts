USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[rsp_GetAllGMUConfiguration]    Script Date: 07/31/2014 16:36:23 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetAllGMUConfiguration]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetAllGMUConfiguration]
GO

USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[rsp_GetAllGMUConfiguration]    Script Date: 07/31/2014 16:36:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[rsp_GetAllGMUConfiguration]
AS
BEGIN

SELECT Datapak_Fault.Datapak_Fault_Code as Code,
Datapak_Fault.Datapak_Fault_Supplementary_Code as subcode,
Datapak_Fault.Datapak_Fault_Source_Protocol as SourceProtocol,
 Call_Fault.Call_Fault_Description as Fault,
Datapak_Fault.Datapak_Fault_Auto_Log_Service_Call_Critical as CreateServiceCall,
Datapak_Fault.Auto_close_Service_Call as CloseServiceCall,
Datapak_Fault.Auto_Close_Source_ID as SourceID,
Datapak_Fault.Type,
Datapak_Fault.Datapak_Fault_Text as Description,
Datapak_Fault.SendMail as ToMail,

Datapak_Fault.Datapak_Fault_ID,
Datapak_Fault.Call_Fault_ID ,
Datapak_Fault.Mail_CC ,
Datapak_Fault.Mail_TO,
Call_Fault.Call_Group_ID

FROM Datapak_Fault LEFT JOIN Call_Fault ON Datapak_Fault.Call_Fault_ID = Call_Fault.Call_Fault_ID ORDER BY Datapak_Fault_Text


END



GO


