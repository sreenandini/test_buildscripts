-------------------------------------------------------------------------- 
-- Description: Insert a row to setting table for Setting_Name for SGVI
-- =======================================================================
-- Revision History
-- Vineetha Mathew   12/10/2009   Created	Interval to run the HourlyDaily service in milliseconds
--------------------------------------------------------------------------- 
IF NOT EXISTS (SELECT * FROM  setting WHERE setting_name ='Interval_HourlyDaily_InMilliSeconds')
	BEGIN
		INSERT INTO Setting (Setting_Name,Setting_Value)
		VALUES ('Interval_HourlyDaily_InMilliSeconds',1000)
	END


