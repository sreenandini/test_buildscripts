USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateModelConfigurations]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateModelConfigurations]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[usp_UpdateModelConfigurations] 
(
	@MachineTypeID INT,
	@MachineName VARCHAR(50),
	@ManufacturerID INT,
	@RecreateCancelledCredits BIT,
	@JackpotAddedToCancelledCredits BIT,
	@AddTrueCoinInToDrop BIT,
	@UseCancelledCreditsAsTicketsPrinted BIT,
	@RecreateTicketsInsertedfromDrop BIT
)
AS    
BEGIN  
	DECLARE @Machine_Class_No INT
	SELECT @Machine_Class_No = Machine_Class_ID FROM Machine_Class MC WITH(NOLOCK) INNER JOIN Manufacturer M ON M.Manufacturer_ID = MC.Manufacturer_ID
	INNER JOIN Machine_Type MT WITH(NOLOCK) ON MT.Machine_Type_ID = MC.Machine_Type_ID WHERE
	MT.Machine_Type_ID = @MachineTypeID AND RTRIM(LTRIM(MC.MAchine_Name)) = RTRIM(LTRIM(@MachineName))
	AND M.Manufacturer_ID = @ManufacturerID
	
	UPDATE Machine_Class
		SET
			Machine_Class_RecreateCancelledCredits = @RecreateCancelledCredits,
			Machine_Class_JackpotAddedToCancelledCredits = @JackpotAddedToCancelledCredits,
			Machine_Class_AddTrueCoinInToDrop = @AddTrueCoinInToDrop,
			Machine_Class_UseCancelledCreditsAsTicketsPrinted = @UseCancelledCreditsAsTicketsPrinted,
			Machine_Class_RecreateTicketsInsertedfromDrop = @RecreateTicketsInsertedfromDrop
		WHERE Machine_Class_ID = @Machine_Class_No
		
	 INSERT INTO dbo.Export_History(EH_Date,EH_Reference1,EH_Type, EH_Site_Code) SELECT GETDATE(),@Machine_Class_No,'MODEL', Site_Code FROM dbo.SITE
END

GO

