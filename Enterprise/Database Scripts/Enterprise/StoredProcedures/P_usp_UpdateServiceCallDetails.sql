USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[usp_UpdateServiceCallDetails]    Script Date: 07/31/2014 16:31:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateServiceCallDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateServiceCallDetails]
GO

USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[usp_UpdateServiceCallDetails]    Script Date: 07/31/2014 16:31:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*
* **********************************************************************************************************
* Revision History
* 
* Manikandan		Created		29/07/2014		
* 
* Store the GMU service faults		
* 
* **********************************************************************************************************
*/
CREATE procedure [dbo].[usp_UpdateServiceCallDetails]  
(  
 @FaultName VARCHAR(100),   
 @ToMail INT  
)   
AS
DECLARE @MAXCALLFAULTID INT 
BEGIN
INSERT INTO Call_Fault(Call_Group_ID, Call_Fault_Description, Call_Fault_Reference) VALUES(6,@FaultName, 'CDO')
SELECT @MAXCALLFAULTID = MAX(Call_Fault_ID) FROM Call_Fault
INSERT INTO Datapak_Fault(Call_Fault_ID, Datapak_Fault_Code, Datapak_Fault_Supplementary_Code, Datapak_Fault_Text, Datapak_Fault_Auto_Log_Service_Call_Non_Critical, Datapak_Fault_Auto_Log_Service_Call_Critical, Datapak_Fault_Source_Protocol, Type, SendMail, Auto_Close_Service_Call) 
VALUES(@MAXCALLFAULTID, 300, 1, @FaultName, 0, 1, 3, 1, @ToMail, 0)
  
END
  





GO


