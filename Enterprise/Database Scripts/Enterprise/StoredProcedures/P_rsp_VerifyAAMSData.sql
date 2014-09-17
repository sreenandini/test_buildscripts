USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_VerifyAAMSData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_VerifyAAMSData]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[rsp_VerifyAAMSData]  
@MessageType VARCHAR(20),   
@MessageID VARCHAR(20),   
@VLTID VARCHAR(20),   
@GameID VARCHAR(20),  
@Status INT OUTPUT  
  
AS  
  
SET @Status = 0  
DECLARE @MachineID INT  
DECLARE @GameName VARCHAR(100)  
DECLARE @GamePartNo VARCHAR(20)  
  
SELECT @MachineID = ISNULL(M.Machine_ID,0) FROM Machine M INNER JOIN BMC_AAMS_Details BAD  
ON M.Machine_ID = BAD.BAD_Reference_ID  
WHERE BAD.BAD_AAMS_Code = @VLTID AND BAD.BAD_AAMS_Entity_Type = 3  
  
IF ISNULL(@MachineID,0) = 0  
BEGIN  
 SET @Status = 1  
 RETURN  
END  
  
IF NOT EXISTS(SELECT Installation_ID FROM Installation I INNER JOIN Machine M  
ON I.Machine_ID = M.Machine_ID WHERE M.Machine_ID = @MachineID AND I.Installation_End_Date IS NULL)  
BEGIN  
 SET @Status = 2  
 RETURN  
END  
  
IF (@MessageType = '102' OR @MessageType = '211')  
BEGIN  
  
 SELECT @GameName = BAD_Game_Name, @GamePartNo = BAD_Game_Part_Number FROM BMC_AAMS_Details  
 WHERE BAD_AAMS_Code = @GameID AND BAD_AAMS_Entity_Type = 4   
  
 IF (ISNULL(@GameName,'') = '' OR ISNULL(@GamePartNo, '') = '')  
 BEGIN  
  SET @Status = 3  
  RETURN  
 END  
  
 IF NOT EXISTS(SELECT I.Installation_ID FROM Installation I INNER JOIN Installation_Game_Info IGI  
 ON I.Installation_ID = IGI.Installation_No WHERE I.Machine_ID = @MachineID AND I.Installation_End_Date IS NULL   
 AND IGI.IsAvailable = 1 AND IGI.Game_Name = @GameName AND IGI.Game_Part_Number = @GamePartNo)  
 BEGIN  
  SET @Status = 4  
  RETURN  
 END  
END  

IF (@MessageType = '210' OR @MessageType = '213' OR @MessageType = '100' OR @MessageType = '101')  
BEGIN

	DECLARE @SErialNo Varchar(20)
	SELECT @SerialNo = BAD_Asset_Serial_No FROM BMC_AAMS_DETAILS WHERE BAD_AAMS_Code = @VLTID
	IF NOT EXISTS (Select 1 From dbo.CV_Component_Request_Status Where CCRS_Machine_Serial_No = @SerialNo)
	BEGIN
		SET @Status = 1
		RETURN 
	END


END 

  
SET @Status = 5  
  
  

  


GO

