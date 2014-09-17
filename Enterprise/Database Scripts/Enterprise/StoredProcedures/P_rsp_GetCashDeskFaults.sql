USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[rsp_GetCallFaultsByGroupID]    Script Date: 07/25/2014 15:35:21 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetCashDeskFaults]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetCashDeskFaults]
GO

CREATE PROC [dbo].[rsp_GetCashDeskFaults]
AS
BEGIN

SELECT Datapak_Fault_Code, Datapak_Fault_Supplementary_Code, Datapak_Fault_Text 
FROM Datapak_Fault 
WHERE Datapak_Fault_Code = 300

END
GO