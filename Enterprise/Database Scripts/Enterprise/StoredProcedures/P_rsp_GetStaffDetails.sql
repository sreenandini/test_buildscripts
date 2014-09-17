USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetStaffDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetStaffDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*    
* Revision History    
*     
* <Name of the owner>         DateCreated       Type(Created/Modified)  Description    
  Kalaiyarasan.P              25-May-2012         Created               This SP is used to get Staff details     
                                                                        based on Staff_ID.    
*/    
CREATE PROCEDURE rsp_GetStaffDetails
	@Staff_ID INT
AS
	SELECT Staff_ID,
	       Operator_ID,
	       User_Group_ID,
	       Staff_First_Name,
	       Staff_Last_Name,
	       Staff_Title,
	       Staff_Address,
	       Staff_Postcode,
	       Staff_Phone_No,
	       Staff_Extension_No,
	       Staff_Mobile_No,
	       Staff_Job_Title,
	       Staff_Department,
	       Staff_IsACollector,
	       Staff_IsAnEngineer,
	       Staff_IsARepresentative,
	       Staff_IsAStockController,
	       Staff_Start_Date,
	       Staff_End_Date,
	       Staff_Username,
	       Staff_Password,
	       Depot_ID,
	       Service_Area_ID,
	       Supplier_ID,
	       Staff_Personel_No,
	       Staff_Terminated,
	       Staff_Notes,
	       Email_Address,
	       UserTableID
	FROM   Staff WITH(NOLOCK)
	WHERE  Staff_ID = COALESCE(@Staff_ID, Staff_ID) 
	       --Exec  rsp_GetStaffDetails null


GO

