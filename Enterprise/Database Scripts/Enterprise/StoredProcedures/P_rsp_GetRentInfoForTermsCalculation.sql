USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_GetRentInfoForTermsCalculation]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_GetRentInfoForTermsCalculation]
GO

CREATE PROCEDURE [dbo].[rsp_GetRentInfoForTermsCalculation] 
(@MachineClassID INT, @RentScheduleID INT)
AS
BEGIN
	SELECT MCRB.Machine_Class_Past_Date,
	       MCRB.Machine_Class_Future_Date,
	       RBC.Rent_Band_Future_Start_Date AS Current_Future_Date,
	       RBC.Rent_Band_Past_End_Date AS Current_Past_Date,
	       RBC.Rent_Band_Price AS Current_Current_Price,
	       RBC.Rent_Band_Supplementary_Period_Charge AS 
	       Current_Current_SupCharge,
	       RBC.Rent_Band_Future_Price AS Current_Future_Price,
	       RBC.Rent_Band_Supplementary_Period_Future_Charge AS 
	       Current_Future_SupCharge,
	       RBC.Rent_Band_Past_Price AS Current_Past_Price,
	       RBC.Rent_Band_Supplementary_Period_Past_Charge AS 
	       Current_Past_SupCharge,
	       RBP.Rent_Band_Future_Start_Date AS Past_Future_Date,
	       RBP.Rent_Band_Past_End_Date AS Past_Past_Date,
	       RBP.Rent_Band_Price AS Past_Current_Price,
	       RBP.Rent_Band_Supplementary_Period_Charge AS Past_Current_SupCharge,
	       RBP.Rent_Band_Future_Price AS Past_Future_Price,
	       RBP.Rent_Band_Supplementary_Period_Future_Charge AS 
	       Past_Future_SupCharge,
	       RBP.Rent_Band_Past_Price AS Past_Past_Price,
	       RBP.Rent_Band_Supplementary_Period_Past_Charge AS Past_Past_SupCharge,
	       RBF.Rent_Band_Future_Start_Date AS Future_Future_Date,
	       RBF.Rent_Band_Past_End_Date AS Future_Past_Date,
	       RBF.Rent_Band_Price AS Future_Current_Price,
	       RBF.Rent_Band_Supplementary_Period_Charge AS Future_Current_SupCharge,
	       RBF.Rent_Band_Future_Price AS Future_Future_Price,
	       RBF.Rent_Band_Supplementary_Period_Future_Charge AS 
	       Future_Future_SupCharge,
	       RBF.Rent_Band_Past_Price AS Future_Past_Price,
	       RBF.Rent_Band_Supplementary_Period_Past_Charge AS 
	       Future_Past_SupCharge,
	       RS.Rent_Schedule_Name,
	       MCRB.Machine_Class_Rent_Band_Supplemental_Charge,
	       RS.Rent_Schedule_Supplemental_Is_Percentage
	FROM   (
	           (
	               (
	                   (
	                       Machine_Class_Rent_Band MCRB 
	                       INNER JOIN Rent_Band RBC ON MCRB.Rent_Band_ID = RBC.Rent_Band_ID
	                   ) 
	                   INNER JOIN Rent_Schedule RS ON RBC.Rent_Schedule_ID = RS.Rent_Schedule_ID
	               ) 
	               LEFT JOIN Rent_Band RBF ON MCRB.Rent_Band_ID_Future = RBF.Rent_Band_ID
	           ) 
	           LEFT JOIN Rent_Band RBP ON MCRB.Rent_Band_ID_Past = RBP.Rent_Band_ID
	       )
	WHERE  (
	           MCRB.Machine_Class_ID = @MachineClassID
	           OR MCRB.Machine_Class_ID = 0
	       )
	       AND RS.Rent_Schedule_ID = @RentScheduleID
	ORDER BY
	       Machine_Class_ID DESC
END
GO
