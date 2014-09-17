USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_InsertComponentVerificationRecord]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_InsertComponentVerificationRecord]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- ===================================================================================================================================   
------------------------------ usp_InsertComponentVerificationRecord ---------------------------------------------------------------------  
-- -----------------------------------------------------------------------------------------------------------------------------------   
-- Revision History  --    
-- 29/05/2010   Renjish  Created 
-- 14/06/2010 Senthil Modified for inserting into export history table.  
-- ==================================================================================================================================     

CREATE PROCEDURE dbo.usp_InsertComponentVerificationRecord      
@MachineSerialNo VARCHAR(30), 
@ComponentTypeID INT,
@VerificationType INT
AS     
BEGIN
DECLARE @Site_Code VARCHAR(10)
DECLARE @InstallationID INT
DECLARE @Reference VARCHAR(50)

SELECT @Site_Code = S.Site_Code, @InstallationID = I.Installation_ID FROM [Site] S 
INNER JOIN Bar_Position BP ON S.Site_ID = BP.Site_ID
INNER JOIN Installation I ON BP.Bar_Position_ID = I.Bar_Position_ID
INNER JOIN Machine M ON I.Machine_ID = M.Machine_ID
WHERE M.Machine_Manufacturers_Serial_No = @MachineSerialNo AND I.Installation_End_Date IS NULL

SET @Reference = CAST(@VerificationType AS VARCHAR) + ':' + @MachineSerialNo + ',' + CAST(@ComponentTypeID AS VARCHAR) 

INSERT INTO Export_History(EH_Date,EH_Reference1,EH_Type,EH_Site_Code)
VALUES(GetDate(), @Reference,'ONDEMANDVERIFICATION',@Site_Code)

END


GO

