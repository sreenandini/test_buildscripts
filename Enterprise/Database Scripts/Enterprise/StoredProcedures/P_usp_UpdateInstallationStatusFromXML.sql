USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateInstallationStatusFromXML]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateInstallationStatusFromXML]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--SP to update Installation status from XML 
--in installation table
create proc usp_UpdateInstallationStatusFromXML
(
	@doc xml
)
as

declare @InstallationID int
declare @Status varchar(50)
declare @Installation_Token_Value int
declare @Installation_Price_Of_Play int
declare @Anticipated_Percentage_Payout real
declare @Installation_MaxBet int

DECLARE @docHandle int  
EXEC sp_xml_preparedocument @docHandle OUTPUT, @doc  
  
 
 
SELECT	@InstallationID = InstallationID,
		@Status = InstallationStatus,
		@Installation_Token_Value = Installation_Token_Value,
		@Installation_Price_Of_Play = Installation_Price_Of_Play,
		@Anticipated_Percentage_Payout = Anticipated_Percentage_Payout,
		@Installation_MaxBet = Installation_MaxBet
 FROM OPENXML (@docHandle,  '/InstallationStatusUpdate/Installation',2)  
with  
(  
 InstallationID int 'InstallationNo',  
 InstallationStatus varchar(50) 'InstallationStatus'  ,
 Installation_Token_Value int 'Installation_Token_Value',
 Installation_Price_Of_Play int 'Installation_Price_Of_Play',
 Anticipated_Percentage_Payout real 'Anticipated_Percentage_Payout',
 Installation_MaxBet int 'Installation_MaxBet'
) x   

update installation
set Installation_Status = @Status,
	Installation_Token_Value = @Installation_Token_Value,
	Installation_Price_Per_Play = @Installation_Price_Of_Play,
	Installation_Percentage_Payout = @Anticipated_Percentage_Payout,
	Installation_MaxBet = @Installation_MaxBet
where Installation_ID = @InstallationID

EXEC sp_xml_removedocument @docHandle 


GO

