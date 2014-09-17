USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetModelConfigurations]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetModelConfigurations]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[rsp_GetModelConfigurations] (@MachineTypeID INT, @MachineName VARCHAR(100), @ManufacturerID INT)
AS    
BEGIN  
	SELECT 
			Machine_Class_RecreateCancelledCredits ,
			Machine_Class_JackpotAddedToCancelledCredits ,
			Machine_Class_AddTrueCoinInToDrop ,
			Machine_Class_UseCancelledCreditsAsTicketsPrinted ,
			Machine_Class_RecreateTicketsInsertedfromDrop  FROM Machine_Class MC WITH(NOLOCK) INNER JOIN Manufacturer M ON M.Manufacturer_ID = MC.Manufacturer_ID
	INNER JOIN Machine_Type MT WITH(NOLOCK) ON MT.Machine_Type_ID = MC.Machine_Type_ID WHERE
	MT.Machine_Type_ID = @MachineTypeID AND RTRIM(LTRIM(MC.Machine_Name)) = RTRIM(LTRIM(@MachineName))
	AND M.Manufacturer_ID = @ManufacturerID
		
END

GO

