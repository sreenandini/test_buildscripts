/************************************************************
 * Created by Bally Technologies � 2011
 * Time: 26/12/12 5:43:30 PM
 ************************************************************/
USE [Enterprise]
GO

SET IDENTITY_INSERT System_Parameters ON

GO

IF NOT EXISTS(SELECT 1 FROM [System_Parameters]WHERE System_Parameter_Company_Name = 'New Company')
    INSERT [System_Parameters] (System_Parameter_ID, System_Parameter_Company_Name, System_Parameter_Company_Address, System_Parameter_End_Of_Week, System_Parameter_Display_Company, System_Parameter_Display_Sub, System_Parameter_Display_Site, System_Parameter_Display_Zone, System_Parameter_Display_Bar, System_Parameter_Database_Type, System_Parameter_Database_Connection, System_Parameter_Sage_Connection, System_Parameter_Sage_Upload_Collection, System_Parameter_Sage_Upload_Transaction, System_Parameter_EDI_InBox, System_Parameter_EDI_OutBox, System_Parameter_Company_Logo, System_Parameter_Company_Address_1, System_Parameter_Company_Address_2, System_Parameter_Company_Address_3, System_Parameter_Company_Address_4, System_Parameter_Company_Address_5, System_Parameter_Company_PostCode, System_Parameter_Key_1, System_Parameter_Key_2, System_Parameter_Key_3, System_Parameter_Key_4, System_Parameter_Key_5, System_Parameter_Ram_Inbox, System_Parameter_Ram_Outbox, System_Parameter_Send_Messages, 
           System_Parameter_Man_Number, McMullens_Export_Sequence, LeisureData_Export_Sequence, LeisureData_Export_Record_Sequence, Amedis_Export_Sequence, LeisureData_Enterprise_Export_Sequence, Licence_Ledger_Code, PPL_Ledger_Code, System_Parameter_Docket_Printer, System_Parameter_Site_Import_AutoCreateBarPos, System_Parameter_Site_Import_AutoCreateMachine, System_Parameter_Site_Import_AutoInstallMachine, System_Parameter_Site_Import_AutoCreateManufacturer, System_Parameter_Site_Import_AutoCreateModel, System_Parameter_Site_Import_AutoCreateType, System_Parameter_Auto_Generate_Model_Codes, System_Parameter_Model_Code_Prefix, System_Parameter_Model_Code_Number_Length, System_Parameter_Auto_Generate_Stock_Codes, System_Parameter_Stock_Code_Prefix, System_Parameter_Stock_Code_Number_Length, System_Parameter_Allow_Company_Filter, System_Parameter_Curr_Service_ID, System_Parameter_Service_Handheld, System_Parameter_Server_Name, System_Parameter_Auto_Generate_Site_Codes, 
           System_Parameter_Site_Code_Next_Number, System_Parameter_Service_Events_Printer, System_Parameter_Force_Site_Reps_On_Stock, System_Parameter_Stock_Allow_Bulk_Purchase, System_Parameter_Enforce_Masks_To_Site, System_Parameter_Curr_Service_Job_ID, System_Parameter_Unallocated_Model, System_Parameter_Region_Culture )
    SELECT 1,'New Company', NULL, 'Sunday', 'Company', 'Sub Company', 'Site', 'Zone', 'Bar Position', 'Database Type', 'Database Connection Parameters', 'Saga Line 50 Connection Parameters', 1, 0, 'System_Parameter_EDI_InBox', 'System_Parameter_EDI_InBox', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '', '', 0, '', 0, 0, 0, 0, 0, '', '', 'Oki ML3391', 0, 0, 0, 0, 0, 0, 1, 'M', 3, 1, 'LC', 4, NULL, 2033, 0, '', 0, 0, 'Oki ML3391', 0, 1, 0, 2013, -1, 'en-GB'
ELSE
    UPDATE [System_Parameters]
    SET    System_Parameter_Company_Name = 'New Company', System_Parameter_Company_Address = NULL, System_Parameter_End_Of_Week = 'Sunday', System_Parameter_Display_Company = 'Company', System_Parameter_Display_Sub = 'Sub Company', System_Parameter_Display_Site = 'Site', System_Parameter_Display_Zone = 'Zone', System_Parameter_Display_Bar = 'Bar Position', System_Parameter_Database_Type = 'Database Type', System_Parameter_Database_Connection = 'Database Connection Parameters', System_Parameter_Sage_Connection = 'Saga Line 50 Connection Parameters', System_Parameter_Sage_Upload_Collection = 1, System_Parameter_Sage_Upload_Transaction = 0, System_Parameter_EDI_InBox = 'System_Parameter_EDI_InBox', System_Parameter_EDI_OutBox = 'System_Parameter_EDI_InBox', System_Parameter_Company_Logo = NULL, System_Parameter_Company_Address_1 = NULL, System_Parameter_Company_Address_2 = NULL, System_Parameter_Company_Address_3 = NULL, System_Parameter_Company_Address_4 = NULL, 
           System_Parameter_Company_Address_5 = NULL, System_Parameter_Company_PostCode = NULL, System_Parameter_Key_1 = NULL, System_Parameter_Key_2 = NULL, System_Parameter_Key_3 = NULL, System_Parameter_Key_4 = NULL, System_Parameter_Key_5 = NULL, System_Parameter_Ram_Inbox = '', System_Parameter_Ram_Outbox = '', System_Parameter_Send_Messages = 0, System_Parameter_Man_Number = '', McMullens_Export_Sequence = 0, LeisureData_Export_Sequence = 0, LeisureData_Export_Record_Sequence = 0, Amedis_Export_Sequence = 0, LeisureData_Enterprise_Export_Sequence = 0, Licence_Ledger_Code = '', PPL_Ledger_Code = '', System_Parameter_Docket_Printer = 'Oki ML3391', System_Parameter_Site_Import_AutoCreateBarPos = 0, System_Parameter_Site_Import_AutoCreateMachine = 0, System_Parameter_Site_Import_AutoInstallMachine = 0, System_Parameter_Site_Import_AutoCreateManufacturer = 0, System_Parameter_Site_Import_AutoCreateModel = 0, System_Parameter_Site_Import_AutoCreateType = 0, 
           System_Parameter_Auto_Generate_Model_Codes = 1, System_Parameter_Model_Code_Prefix = 'M', System_Parameter_Model_Code_Number_Length = 3, System_Parameter_Auto_Generate_Stock_Codes = 1, System_Parameter_Stock_Code_Prefix = 'LC', System_Parameter_Stock_Code_Number_Length = 4, System_Parameter_Allow_Company_Filter = NULL, System_Parameter_Curr_Service_ID = 2033, System_Parameter_Service_Handheld = 0, System_Parameter_Server_Name = '', System_Parameter_Auto_Generate_Site_Codes = 0, System_Parameter_Site_Code_Next_Number = 0, System_Parameter_Service_Events_Printer = 'Oki ML3391', System_Parameter_Force_Site_Reps_On_Stock = 0, System_Parameter_Stock_Allow_Bulk_Purchase = 1, System_Parameter_Enforce_Masks_To_Site = 0, System_Parameter_Curr_Service_Job_ID = 2013, System_Parameter_Unallocated_Model = -1, System_Parameter_Region_Culture = 'en-GB'
    WHERE  System_Parameter_ID = 1

GO

SET IDENTITY_INSERT System_Parameters OFF

GO