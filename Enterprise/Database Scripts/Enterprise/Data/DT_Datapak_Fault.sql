/************************************************************
 * Created by Bally Technologies © 2011
 * Time: 28/01/13 1:34:43 PM
 ************************************************************/
USE Enterprise
GO

SET IDENTITY_INSERT Datapak_Fault ON
GO

IF NOT EXISTS(SELECT 1 FROM [Datapak_Fault]WHERE Datapak_Fault_ID = '46')
    INSERT [Datapak_Fault] ( Datapak_Fault_ID, Call_Fault_ID, Datapak_Fault_Code, Datapak_Fault_Supplementary_Code, Datapak_Fault_Text, Datapak_Fault_Auto_Log_Service_Call_Non_Critical, Datapak_Fault_Auto_Log_Service_Call_Critical, Datapak_Fault_Source_Protocol, TYPE, SendMail, Mail_TO, Mail_CC, Auto_Close_Service_Call, Auto_Close_Source_ID, Auto_Close_Type_ID )
    SELECT 46, 52, 21, 10, 'Comms Failure', 0, 0, 3, 2, 0, NULL, NULL, 0, NULL, NULL
ELSE
    UPDATE [Datapak_Fault]
    SET    Call_Fault_ID = 52, Datapak_Fault_Code = 21, Datapak_Fault_Supplementary_Code = 10, Datapak_Fault_Text = 'Comms Failure', Datapak_Fault_Auto_Log_Service_Call_Non_Critical = 0, Datapak_Fault_Auto_Log_Service_Call_Critical = 0, Datapak_Fault_Source_Protocol = 3, TYPE = 2, SendMail = 0, Mail_TO = NULL, Mail_CC = NULL, Auto_Close_Service_Call = 0, Auto_Close_Source_ID = NULL, Auto_Close_Type_ID = NULL
    WHERE  Datapak_Fault_ID = '46'

IF NOT EXISTS(SELECT 1 FROM [Datapak_Fault]WHERE Datapak_Fault_ID = '47')
    INSERT [Datapak_Fault] ( Datapak_Fault_ID, Call_Fault_ID, Datapak_Fault_Code, Datapak_Fault_Supplementary_Code, Datapak_Fault_Text, Datapak_Fault_Auto_Log_Service_Call_Non_Critical, Datapak_Fault_Auto_Log_Service_Call_Critical, Datapak_Fault_Source_Protocol, TYPE, SendMail, Mail_TO, Mail_CC, Auto_Close_Service_Call, Auto_Close_Source_ID, Auto_Close_Type_ID )
    SELECT 47, 43, 21, 11, 'Door open event', 0, 0, 3, 1, 0, NULL, NULL, 0, NULL, NULL
ELSE
    UPDATE [Datapak_Fault]
    SET    Call_Fault_ID = 43, Datapak_Fault_Code = 21, Datapak_Fault_Supplementary_Code = 11, Datapak_Fault_Text = 'Door open event', Datapak_Fault_Auto_Log_Service_Call_Non_Critical = 0, Datapak_Fault_Auto_Log_Service_Call_Critical = 0, Datapak_Fault_Source_Protocol = 3, TYPE = 1, SendMail = 0, Mail_TO = NULL, Mail_CC = NULL, Auto_Close_Service_Call = 0, Auto_Close_Source_ID = NULL, Auto_Close_Type_ID = NULL
    WHERE  Datapak_Fault_ID = '47'

IF NOT EXISTS(SELECT 1 FROM [Datapak_Fault]WHERE Datapak_Fault_ID = '48')
    INSERT [Datapak_Fault] ( Datapak_Fault_ID, Call_Fault_ID, Datapak_Fault_Code, Datapak_Fault_Supplementary_Code, Datapak_Fault_Text, Datapak_Fault_Auto_Log_Service_Call_Non_Critical, Datapak_Fault_Auto_Log_Service_Call_Critical, Datapak_Fault_Source_Protocol, TYPE, SendMail, Mail_TO, Mail_CC, Auto_Close_Service_Call, Auto_Close_Source_ID, Auto_Close_Type_ID )
    SELECT 48, 62, 20, 10, 'Slot Door Open', 0, 0, 3, 1, 0, NULL, NULL, 0, NULL, NULL
ELSE
    UPDATE [Datapak_Fault]
    SET    Call_Fault_ID = 62, Datapak_Fault_Code = 20, Datapak_Fault_Supplementary_Code = 10, Datapak_Fault_Text = 'Slot Door Open', Datapak_Fault_Auto_Log_Service_Call_Non_Critical = 0, Datapak_Fault_Auto_Log_Service_Call_Critical = 0, Datapak_Fault_Source_Protocol = 3, TYPE = 1, SendMail = 0, Mail_TO = NULL, Mail_CC = NULL, Auto_Close_Service_Call = 0, Auto_Close_Source_ID = NULL, Auto_Close_Type_ID = NULL
    WHERE  Datapak_Fault_ID = '48'

IF NOT EXISTS(SELECT 1 FROM [Datapak_Fault]WHERE Datapak_Fault_ID = '50')
    INSERT [Datapak_Fault] ( Datapak_Fault_ID, Call_Fault_ID, Datapak_Fault_Code, Datapak_Fault_Supplementary_Code, Datapak_Fault_Text, Datapak_Fault_Auto_Log_Service_Call_Non_Critical, Datapak_Fault_Auto_Log_Service_Call_Critical, Datapak_Fault_Source_Protocol, TYPE, SendMail, Mail_TO, Mail_CC, Auto_Close_Service_Call, Auto_Close_Source_ID, Auto_Close_Type_ID )
    SELECT 50, 45, 21, 6, 'Power up', 0, 0, 3, 3, 0, NULL, NULL, 0, NULL, NULL
ELSE
    UPDATE [Datapak_Fault]
    SET    Call_Fault_ID = 45, Datapak_Fault_Code = 21, Datapak_Fault_Supplementary_Code = 6, Datapak_Fault_Text = 'Power up', Datapak_Fault_Auto_Log_Service_Call_Non_Critical = 0, Datapak_Fault_Auto_Log_Service_Call_Critical = 0, Datapak_Fault_Source_Protocol = 3, TYPE = 3, SendMail = 0, Mail_TO = NULL, Mail_CC = NULL, Auto_Close_Service_Call = 0, Auto_Close_Source_ID = NULL, Auto_Close_Type_ID = NULL
    WHERE  Datapak_Fault_ID = '50'

IF NOT EXISTS(SELECT 1 FROM [Datapak_Fault]WHERE Datapak_Fault_ID = '52')
    INSERT [Datapak_Fault] ( Datapak_Fault_ID, Call_Fault_ID, Datapak_Fault_Code, Datapak_Fault_Supplementary_Code, Datapak_Fault_Text, Datapak_Fault_Auto_Log_Service_Call_Non_Critical, Datapak_Fault_Auto_Log_Service_Call_Critical, Datapak_Fault_Source_Protocol, TYPE, SendMail, Mail_TO, Mail_CC, Auto_Close_Service_Call, Auto_Close_Source_ID, Auto_Close_Type_ID )
    SELECT 52, 44, 21, 7, 'Power down event', 0, 0, 3, 3, 0, NULL, NULL, 0, NULL, NULL
ELSE
    UPDATE [Datapak_Fault]
    SET    Call_Fault_ID = 44, Datapak_Fault_Code = 21, Datapak_Fault_Supplementary_Code = 7, Datapak_Fault_Text = 'Power down event', Datapak_Fault_Auto_Log_Service_Call_Non_Critical = 0, Datapak_Fault_Auto_Log_Service_Call_Critical = 0, Datapak_Fault_Source_Protocol = 3, TYPE = 3, SendMail = 0, Mail_TO = NULL, Mail_CC = NULL, Auto_Close_Service_Call = 0, Auto_Close_Source_ID = NULL, Auto_Close_Type_ID = NULL
    WHERE  Datapak_Fault_ID = '52'

IF NOT EXISTS(SELECT 1 FROM [Datapak_Fault]WHERE Datapak_Fault_ID = '53')
    INSERT [Datapak_Fault] ( Datapak_Fault_ID, Call_Fault_ID, Datapak_Fault_Code, Datapak_Fault_Supplementary_Code, Datapak_Fault_Text, Datapak_Fault_Auto_Log_Service_Call_Non_Critical, Datapak_Fault_Auto_Log_Service_Call_Critical, Datapak_Fault_Source_Protocol, TYPE, SendMail, Mail_TO, Mail_CC, Auto_Close_Service_Call, Auto_Close_Source_ID, Auto_Close_Type_ID )
    SELECT 53, 47, 20, 12, 'stacker door open', 0, 0, 3, 1, 0, NULL, NULL, 0, NULL, NULL
ELSE
    UPDATE [Datapak_Fault]
    SET    Call_Fault_ID = 47, Datapak_Fault_Code = 20, Datapak_Fault_Supplementary_Code = 12, Datapak_Fault_Text = 'stacker door open', Datapak_Fault_Auto_Log_Service_Call_Non_Critical = 0, Datapak_Fault_Auto_Log_Service_Call_Critical = 0, Datapak_Fault_Source_Protocol = 3, TYPE = 1, SendMail = 0, Mail_TO = NULL, Mail_CC = NULL, Auto_Close_Service_Call = 0, Auto_Close_Source_ID = NULL, Auto_Close_Type_ID = NULL
    WHERE  Datapak_Fault_ID = '53'

IF NOT EXISTS(SELECT 1 FROM [Datapak_Fault]WHERE Datapak_Fault_ID = '54')
    INSERT [Datapak_Fault] ( Datapak_Fault_ID, Call_Fault_ID, Datapak_Fault_Code, Datapak_Fault_Supplementary_Code, Datapak_Fault_Text, Datapak_Fault_Auto_Log_Service_Call_Non_Critical, Datapak_Fault_Auto_Log_Service_Call_Critical, Datapak_Fault_Source_Protocol, TYPE, SendMail, Mail_TO, Mail_CC, Auto_Close_Service_Call, Auto_Close_Source_ID, Auto_Close_Type_ID )
    SELECT 54, 72, 20, 20, 'Stacker removed', 0, 0, 3, 1, 0, NULL, NULL, 0, NULL, NULL
ELSE
    UPDATE [Datapak_Fault]
    SET    Call_Fault_ID = 72, Datapak_Fault_Code = 20, Datapak_Fault_Supplementary_Code = 20, Datapak_Fault_Text = 'Stacker removed', Datapak_Fault_Auto_Log_Service_Call_Non_Critical = 0, Datapak_Fault_Auto_Log_Service_Call_Critical = 0, Datapak_Fault_Source_Protocol = 3, TYPE = 1, SendMail = 0, Mail_TO = NULL, Mail_CC = NULL, Auto_Close_Service_Call = 0, Auto_Close_Source_ID = NULL, Auto_Close_Type_ID = NULL
    WHERE  Datapak_Fault_ID = '54'

IF NOT EXISTS(SELECT 1 FROM [Datapak_Fault]WHERE Datapak_Fault_ID = '55')
    INSERT [Datapak_Fault] ( Datapak_Fault_ID, Call_Fault_ID, Datapak_Fault_Code, Datapak_Fault_Supplementary_Code, Datapak_Fault_Text, Datapak_Fault_Auto_Log_Service_Call_Non_Critical, Datapak_Fault_Auto_Log_Service_Call_Critical, Datapak_Fault_Source_Protocol, TYPE, SendMail, Mail_TO, Mail_CC, Auto_Close_Service_Call, Auto_Close_Source_ID, Auto_Close_Type_ID )
    SELECT 55, 48, 20, 13, 'stacker door close', 0, 0, 3, 1, 0, NULL, NULL, 0, 20, 12
ELSE
    UPDATE [Datapak_Fault]
    SET    Call_Fault_ID = 48, Datapak_Fault_Code = 20, Datapak_Fault_Supplementary_Code = 13, Datapak_Fault_Text = 'stacker door close', Datapak_Fault_Auto_Log_Service_Call_Non_Critical = 0, Datapak_Fault_Auto_Log_Service_Call_Critical = 0, Datapak_Fault_Source_Protocol = 3, TYPE = 1, SendMail = 0, Mail_TO = NULL, Mail_CC = NULL, Auto_Close_Service_Call = 0, Auto_Close_Source_ID = 20, Auto_Close_Type_ID = 12
    WHERE  Datapak_Fault_ID = '55'

IF NOT EXISTS(SELECT 1 FROM [Datapak_Fault]WHERE Datapak_Fault_ID = '56')
    INSERT [Datapak_Fault] ( Datapak_Fault_ID, Call_Fault_ID, Datapak_Fault_Code, Datapak_Fault_Supplementary_Code, Datapak_Fault_Text, Datapak_Fault_Auto_Log_Service_Call_Non_Critical, Datapak_Fault_Auto_Log_Service_Call_Critical, Datapak_Fault_Source_Protocol, TYPE, SendMail, Mail_TO, Mail_CC, Auto_Close_Service_Call, Auto_Close_Source_ID, Auto_Close_Type_ID )
    SELECT 56, 73, 20, 21, 'Stacker replaced', 0, 0, 3, 1, 0, NULL, NULL, 0, 20, 20
ELSE
    UPDATE [Datapak_Fault]
    SET    Call_Fault_ID = 73, Datapak_Fault_Code = 20, Datapak_Fault_Supplementary_Code = 21, Datapak_Fault_Text = 'Stacker replaced', Datapak_Fault_Auto_Log_Service_Call_Non_Critical = 0, Datapak_Fault_Auto_Log_Service_Call_Critical = 0, Datapak_Fault_Source_Protocol = 3, TYPE = 1, SendMail = 0, Mail_TO = NULL, Mail_CC = NULL, Auto_Close_Service_Call = 0, Auto_Close_Source_ID = 20, Auto_Close_Type_ID = 20
    WHERE  Datapak_Fault_ID = '56'

IF NOT EXISTS(SELECT 1 FROM [Datapak_Fault]WHERE Datapak_Fault_ID = '57')
    INSERT [Datapak_Fault] ( Datapak_Fault_ID, Call_Fault_ID, Datapak_Fault_Code, Datapak_Fault_Supplementary_Code, Datapak_Fault_Text, Datapak_Fault_Auto_Log_Service_Call_Non_Critical, Datapak_Fault_Auto_Log_Service_Call_Critical, Datapak_Fault_Source_Protocol, TYPE, SendMail, Mail_TO, Mail_CC, Auto_Close_Service_Call, Auto_Close_Source_ID, Auto_Close_Type_ID )
    SELECT 57, 51, 254, 2, 'GMU Data Comms Down', 0, 1, 3, 2, 1, NULL, NULL, 0, NULL, NULL
ELSE
    UPDATE [Datapak_Fault]
    SET    Call_Fault_ID = 51, Datapak_Fault_Code = 254, Datapak_Fault_Supplementary_Code = 2, Datapak_Fault_Text = 'GMU Data Comms Down', Datapak_Fault_Auto_Log_Service_Call_Non_Critical = 0, Datapak_Fault_Auto_Log_Service_Call_Critical = 1, Datapak_Fault_Source_Protocol = 3, TYPE = 2, SendMail = 1, Mail_TO = NULL, Mail_CC = NULL, Auto_Close_Service_Call = 0, Auto_Close_Source_ID = NULL, Auto_Close_Type_ID = NULL
    WHERE  Datapak_Fault_ID = '57'

IF NOT EXISTS(SELECT 1 FROM [Datapak_Fault]WHERE Datapak_Fault_ID = '58')
    INSERT [Datapak_Fault] ( Datapak_Fault_ID, Call_Fault_ID, Datapak_Fault_Code, Datapak_Fault_Supplementary_Code, Datapak_Fault_Text, Datapak_Fault_Auto_Log_Service_Call_Non_Critical, Datapak_Fault_Auto_Log_Service_Call_Critical, Datapak_Fault_Source_Protocol, TYPE, SendMail, Mail_TO, Mail_CC, Auto_Close_Service_Call, Auto_Close_Source_ID, Auto_Close_Type_ID )
    SELECT 58, 49, 254, 3, 'GMU Data Comms Resum', 0, 1, 3, 2, 1, NULL, NULL, 0, 254, 2
ELSE
    UPDATE [Datapak_Fault]
    SET    Call_Fault_ID = 49, Datapak_Fault_Code = 254, Datapak_Fault_Supplementary_Code = 3, Datapak_Fault_Text = 'GMU Data Comms Resum', Datapak_Fault_Auto_Log_Service_Call_Non_Critical = 0, Datapak_Fault_Auto_Log_Service_Call_Critical = 1, Datapak_Fault_Source_Protocol = 3, TYPE = 2, SendMail = 1, Mail_TO = NULL, Mail_CC = NULL, Auto_Close_Service_Call = 0, Auto_Close_Source_ID = 254, Auto_Close_Type_ID = 2
    WHERE  Datapak_Fault_ID = '58'

IF NOT EXISTS(SELECT 1 FROM [Datapak_Fault]WHERE Datapak_Fault_ID = '59')
    INSERT [Datapak_Fault] ( Datapak_Fault_ID, Call_Fault_ID, Datapak_Fault_Code, Datapak_Fault_Supplementary_Code, Datapak_Fault_Text, Datapak_Fault_Auto_Log_Service_Call_Non_Critical, Datapak_Fault_Auto_Log_Service_Call_Critical, Datapak_Fault_Source_Protocol, TYPE, SendMail, Mail_TO, Mail_CC, Auto_Close_Service_Call, Auto_Close_Source_ID, Auto_Close_Type_ID )
    SELECT 59, 52, 254, 4, 'Machine Data Comms D', 0, 1, 3, 2, 1, NULL, NULL, 0, NULL, NULL
ELSE
    UPDATE [Datapak_Fault]
    SET    Call_Fault_ID = 52, Datapak_Fault_Code = 254, Datapak_Fault_Supplementary_Code = 4, Datapak_Fault_Text = 'Machine Data Comms D', Datapak_Fault_Auto_Log_Service_Call_Non_Critical = 0, Datapak_Fault_Auto_Log_Service_Call_Critical = 1, Datapak_Fault_Source_Protocol = 3, TYPE = 2, SendMail = 1, Mail_TO = NULL, Mail_CC = NULL, Auto_Close_Service_Call = 0, Auto_Close_Source_ID = NULL, Auto_Close_Type_ID = NULL
    WHERE  Datapak_Fault_ID = '59'

IF NOT EXISTS(SELECT 1 FROM [Datapak_Fault]WHERE Datapak_Fault_ID = '60')
    INSERT [Datapak_Fault] ( Datapak_Fault_ID, Call_Fault_ID, Datapak_Fault_Code, Datapak_Fault_Supplementary_Code, Datapak_Fault_Text, Datapak_Fault_Auto_Log_Service_Call_Non_Critical, Datapak_Fault_Auto_Log_Service_Call_Critical, Datapak_Fault_Source_Protocol, TYPE, SendMail, Mail_TO, Mail_CC, Auto_Close_Service_Call, Auto_Close_Source_ID, Auto_Close_Type_ID )
    SELECT 60, 50, 254, 5, 'Machine Data Comms R', 0, 1, 3, 2, 1, NULL, NULL, 0, 254, 4
ELSE
    UPDATE [Datapak_Fault]
    SET    Call_Fault_ID = 50, Datapak_Fault_Code = 254, Datapak_Fault_Supplementary_Code = 5, Datapak_Fault_Text = 'Machine Data Comms R', Datapak_Fault_Auto_Log_Service_Call_Non_Critical = 0, Datapak_Fault_Auto_Log_Service_Call_Critical = 1, Datapak_Fault_Source_Protocol = 3, TYPE = 2, SendMail = 1, Mail_TO = NULL, Mail_CC = NULL, Auto_Close_Service_Call = 0, Auto_Close_Source_ID = 254, Auto_Close_Type_ID = 4
    WHERE  Datapak_Fault_ID = '60'

IF NOT EXISTS(SELECT 1 FROM [Datapak_Fault]WHERE Datapak_Fault_ID = '61')
    INSERT [Datapak_Fault] ( Datapak_Fault_ID, Call_Fault_ID, Datapak_Fault_Code, Datapak_Fault_Supplementary_Code, Datapak_Fault_Text, Datapak_Fault_Auto_Log_Service_Call_Non_Critical, Datapak_Fault_Auto_Log_Service_Call_Critical, Datapak_Fault_Source_Protocol, TYPE, SendMail, Mail_TO, Mail_CC, Auto_Close_Service_Call, Auto_Close_Source_ID, Auto_Close_Type_ID )
    SELECT 61, 63, 20, 11, 'slot door closed', 0, 0, 3, 1, 0, NULL, NULL, 0, 20, 10
ELSE
    UPDATE [Datapak_Fault]
    SET    Call_Fault_ID = 63, Datapak_Fault_Code = 20, Datapak_Fault_Supplementary_Code = 11, Datapak_Fault_Text = 'slot door closed', Datapak_Fault_Auto_Log_Service_Call_Non_Critical = 0, Datapak_Fault_Auto_Log_Service_Call_Critical = 0, Datapak_Fault_Source_Protocol = 3, TYPE = 1, SendMail = 0, Mail_TO = NULL, Mail_CC = NULL, Auto_Close_Service_Call = 0, Auto_Close_Source_ID = 20, Auto_Close_Type_ID = 10
    WHERE  Datapak_Fault_ID = '61'

IF NOT EXISTS(SELECT 1 FROM [Datapak_Fault]WHERE Datapak_Fault_ID = '62')
    INSERT [Datapak_Fault] ( Datapak_Fault_ID, Call_Fault_ID, Datapak_Fault_Code, Datapak_Fault_Supplementary_Code, Datapak_Fault_Text, Datapak_Fault_Auto_Log_Service_Call_Non_Critical, Datapak_Fault_Auto_Log_Service_Call_Critical, Datapak_Fault_Source_Protocol, TYPE, SendMail, Mail_TO, Mail_CC, Auto_Close_Service_Call, Auto_Close_Source_ID, Auto_Close_Type_ID )
    SELECT 62, 92, 21, 2, 'UnRecognised', 0, 0, 3, 4, 0, NULL, NULL, 0, NULL, NULL
ELSE
    UPDATE [Datapak_Fault]
    SET    Call_Fault_ID = 92, Datapak_Fault_Code = 21, Datapak_Fault_Supplementary_Code = 2, Datapak_Fault_Text = 'UnRecognised', Datapak_Fault_Auto_Log_Service_Call_Non_Critical = 0, Datapak_Fault_Auto_Log_Service_Call_Critical = 0, Datapak_Fault_Source_Protocol = 3, TYPE = 4, SendMail = 0, Mail_TO = NULL, Mail_CC = NULL, Auto_Close_Service_Call = 0, Auto_Close_Source_ID = NULL, Auto_Close_Type_ID = NULL
    WHERE  Datapak_Fault_ID = '62'

IF NOT EXISTS(SELECT 1 FROM [Datapak_Fault]WHERE Datapak_Fault_ID = '63')
    INSERT [Datapak_Fault] ( Datapak_Fault_ID, Call_Fault_ID, Datapak_Fault_Code, Datapak_Fault_Supplementary_Code, Datapak_Fault_Text, Datapak_Fault_Auto_Log_Service_Call_Non_Critical, Datapak_Fault_Auto_Log_Service_Call_Critical, Datapak_Fault_Source_Protocol, TYPE, SendMail, Mail_TO, Mail_CC, Auto_Close_Service_Call, Auto_Close_Source_ID, Auto_Close_Type_ID )
    SELECT 63, 1, 21, 4, '** UNKNOWN **', 0, 0, 3, NULL, 0, NULL, NULL, 0, NULL, NULL
ELSE
    UPDATE [Datapak_Fault]
    SET    Call_Fault_ID = 1, Datapak_Fault_Code = 21, Datapak_Fault_Supplementary_Code = 4, Datapak_Fault_Text = '** UNKNOWN **', Datapak_Fault_Auto_Log_Service_Call_Non_Critical = 0, Datapak_Fault_Auto_Log_Service_Call_Critical = 0, Datapak_Fault_Source_Protocol = 3, TYPE = NULL, SendMail = 0, Mail_TO = NULL, Mail_CC = NULL, Auto_Close_Service_Call = 0, Auto_Close_Source_ID = NULL, Auto_Close_Type_ID = NULL
    WHERE  Datapak_Fault_ID = '63'

IF NOT EXISTS(SELECT 1 FROM [Datapak_Fault]WHERE Datapak_Fault_ID = '64')
    INSERT [Datapak_Fault] ( Datapak_Fault_ID, Call_Fault_ID, Datapak_Fault_Code, Datapak_Fault_Supplementary_Code, Datapak_Fault_Text, Datapak_Fault_Auto_Log_Service_Call_Non_Critical, Datapak_Fault_Auto_Log_Service_Call_Critical, Datapak_Fault_Source_Protocol, TYPE, SendMail, Mail_TO, Mail_CC, Auto_Close_Service_Call, Auto_Close_Source_ID, Auto_Close_Type_ID )
    SELECT 64, 1, 21, 5, '** UNKNOWN **', 0, 0, 3, NULL, 0, NULL, NULL, 0, NULL, NULL
ELSE
    UPDATE [Datapak_Fault]
    SET    Call_Fault_ID = 1, Datapak_Fault_Code = 21, Datapak_Fault_Supplementary_Code = 5, Datapak_Fault_Text = '** UNKNOWN **', Datapak_Fault_Auto_Log_Service_Call_Non_Critical = 0, Datapak_Fault_Auto_Log_Service_Call_Critical = 0, Datapak_Fault_Source_Protocol = 3, TYPE = NULL, SendMail = 0, Mail_TO = NULL, Mail_CC = NULL, Auto_Close_Service_Call = 0, Auto_Close_Source_ID = NULL, Auto_Close_Type_ID = NULL
    WHERE  Datapak_Fault_ID = '64'

IF NOT EXISTS(SELECT 1 FROM [Datapak_Fault]WHERE Datapak_Fault_ID = '65')
    INSERT [Datapak_Fault] ( Datapak_Fault_ID, Call_Fault_ID, Datapak_Fault_Code, Datapak_Fault_Supplementary_Code, Datapak_Fault_Text, Datapak_Fault_Auto_Log_Service_Call_Non_Critical, Datapak_Fault_Auto_Log_Service_Call_Critical, Datapak_Fault_Source_Protocol, TYPE, SendMail, Mail_TO, Mail_CC, Auto_Close_Service_Call, Auto_Close_Source_ID, Auto_Close_Type_ID )
    SELECT 65, 1, 21, 3, '** UNKNOWN **', 0, 0, 3, NULL, 0, NULL, NULL, 0, NULL, NULL
ELSE
    UPDATE [Datapak_Fault]
    SET    Call_Fault_ID = 1, Datapak_Fault_Code = 21, Datapak_Fault_Supplementary_Code = 3, Datapak_Fault_Text = '** UNKNOWN **', Datapak_Fault_Auto_Log_Service_Call_Non_Critical = 0, Datapak_Fault_Auto_Log_Service_Call_Critical = 0, Datapak_Fault_Source_Protocol = 3, TYPE = NULL, SendMail = 0, Mail_TO = NULL, Mail_CC = NULL, Auto_Close_Service_Call = 0, Auto_Close_Source_ID = NULL, Auto_Close_Type_ID = NULL
    WHERE  Datapak_Fault_ID = '65'

IF NOT EXISTS(SELECT 1 FROM [Datapak_Fault]WHERE Datapak_Fault_ID = '66')
    INSERT [Datapak_Fault] ( Datapak_Fault_ID, Call_Fault_ID, Datapak_Fault_Code, Datapak_Fault_Supplementary_Code, Datapak_Fault_Text, Datapak_Fault_Auto_Log_Service_Call_Non_Critical, Datapak_Fault_Auto_Log_Service_Call_Critical, Datapak_Fault_Source_Protocol, TYPE, SendMail, Mail_TO, Mail_CC, Auto_Close_Service_Call, Auto_Close_Source_ID, Auto_Close_Type_ID )
    SELECT 66, 70, 20, 18, 'belly door open', 0, 0, 3, 1, 0, NULL, NULL, 0, NULL, NULL
ELSE
    UPDATE [Datapak_Fault]
    SET    Call_Fault_ID = 70, Datapak_Fault_Code = 20, Datapak_Fault_Supplementary_Code = 18, Datapak_Fault_Text = 'belly door open', Datapak_Fault_Auto_Log_Service_Call_Non_Critical = 0, Datapak_Fault_Auto_Log_Service_Call_Critical = 0, Datapak_Fault_Source_Protocol = 3, TYPE = 1, SendMail = 0, Mail_TO = NULL, Mail_CC = NULL, Auto_Close_Service_Call = 0, Auto_Close_Source_ID = NULL, Auto_Close_Type_ID = NULL
    WHERE  Datapak_Fault_ID = '66'

IF NOT EXISTS(SELECT 1 FROM [Datapak_Fault]WHERE Datapak_Fault_ID = '67')
    INSERT [Datapak_Fault] ( Datapak_Fault_ID, Call_Fault_ID, Datapak_Fault_Code, Datapak_Fault_Supplementary_Code, Datapak_Fault_Text, Datapak_Fault_Auto_Log_Service_Call_Non_Critical, Datapak_Fault_Auto_Log_Service_Call_Critical, Datapak_Fault_Source_Protocol, TYPE, SendMail, Mail_TO, Mail_CC, Auto_Close_Service_Call, Auto_Close_Source_ID, Auto_Close_Type_ID )
    SELECT 67, 71, 20, 19, 'belly door closed', 0, 0, 3, 1, 0, NULL, NULL, 0, 20, 18
ELSE
    UPDATE [Datapak_Fault]
    SET    Call_Fault_ID = 71, Datapak_Fault_Code = 20, Datapak_Fault_Supplementary_Code = 19, Datapak_Fault_Text = 'belly door closed', Datapak_Fault_Auto_Log_Service_Call_Non_Critical = 0, Datapak_Fault_Auto_Log_Service_Call_Critical = 0, Datapak_Fault_Source_Protocol = 3, TYPE = 1, SendMail = 0, Mail_TO = NULL, Mail_CC = NULL, Auto_Close_Service_Call = 0, Auto_Close_Source_ID = 20, Auto_Close_Type_ID = 18
    WHERE  Datapak_Fault_ID = '67'

IF NOT EXISTS(SELECT 1 FROM [Datapak_Fault]WHERE Datapak_Fault_ID = '68')
    INSERT [Datapak_Fault] ( Datapak_Fault_ID, Call_Fault_ID, Datapak_Fault_Code, Datapak_Fault_Supplementary_Code, Datapak_Fault_Text, Datapak_Fault_Auto_Log_Service_Call_Non_Critical, Datapak_Fault_Auto_Log_Service_Call_Critical, Datapak_Fault_Source_Protocol, TYPE, SendMail, Mail_TO, Mail_CC, Auto_Close_Service_Call, Auto_Close_Source_ID, Auto_Close_Type_ID )
    SELECT 68, 60, 20, 3, 'cash door open', 0, 0, 3, 1, 0, NULL, NULL, 0, NULL, NULL
ELSE
    UPDATE [Datapak_Fault]
    SET    Call_Fault_ID = 60, Datapak_Fault_Code = 20, Datapak_Fault_Supplementary_Code = 3, Datapak_Fault_Text = 'cash door open', Datapak_Fault_Auto_Log_Service_Call_Non_Critical = 0, Datapak_Fault_Auto_Log_Service_Call_Critical = 0, Datapak_Fault_Source_Protocol = 3, TYPE = 1, SendMail = 0, Mail_TO = NULL, Mail_CC = NULL, Auto_Close_Service_Call = 0, Auto_Close_Source_ID = NULL, Auto_Close_Type_ID = NULL
    WHERE  Datapak_Fault_ID = '68'

IF NOT EXISTS(SELECT 1 FROM [Datapak_Fault]WHERE Datapak_Fault_ID = '69')
    INSERT [Datapak_Fault] ( Datapak_Fault_ID, Call_Fault_ID, Datapak_Fault_Code, Datapak_Fault_Supplementary_Code, Datapak_Fault_Text, Datapak_Fault_Auto_Log_Service_Call_Non_Critical, Datapak_Fault_Auto_Log_Service_Call_Critical, Datapak_Fault_Source_Protocol, TYPE, SendMail, Mail_TO, Mail_CC, Auto_Close_Service_Call, Auto_Close_Source_ID, Auto_Close_Type_ID )
    SELECT 69, 61, 20, 4, 'cash door close', 0, 0, 3, 1, 0, NULL, NULL, 0, 20, 3
ELSE
    UPDATE [Datapak_Fault]
    SET    Call_Fault_ID = 61, Datapak_Fault_Code = 20, Datapak_Fault_Supplementary_Code = 4, Datapak_Fault_Text = 'cash door close', Datapak_Fault_Auto_Log_Service_Call_Non_Critical = 0, Datapak_Fault_Auto_Log_Service_Call_Critical = 0, Datapak_Fault_Source_Protocol = 3, TYPE = 1, SendMail = 0, Mail_TO = NULL, Mail_CC = NULL, Auto_Close_Service_Call = 0, Auto_Close_Source_ID = 20, Auto_Close_Type_ID = 3
    WHERE  Datapak_Fault_ID = '69'

IF NOT EXISTS(SELECT 1 FROM [Datapak_Fault]WHERE Datapak_Fault_ID = '70')
    INSERT [Datapak_Fault] ( Datapak_Fault_ID, Call_Fault_ID, Datapak_Fault_Code, Datapak_Fault_Supplementary_Code, Datapak_Fault_Text, Datapak_Fault_Auto_Log_Service_Call_Non_Critical, Datapak_Fault_Auto_Log_Service_Call_Critical, Datapak_Fault_Source_Protocol, TYPE, SendMail, Mail_TO, Mail_CC, Auto_Close_Service_Call, Auto_Close_Source_ID, Auto_Close_Type_ID )
    SELECT 70, 1, 21, 0, '** UNKNOWN **', 0, 0, 3, NULL, 0, NULL, NULL, 0, NULL, NULL
ELSE
    UPDATE [Datapak_Fault]
    SET    Call_Fault_ID = 1, Datapak_Fault_Code = 21, Datapak_Fault_Supplementary_Code = 0, Datapak_Fault_Text = '** UNKNOWN **', Datapak_Fault_Auto_Log_Service_Call_Non_Critical = 0, Datapak_Fault_Auto_Log_Service_Call_Critical = 0, Datapak_Fault_Source_Protocol = 3, TYPE = NULL, SendMail = 0, Mail_TO = NULL, Mail_CC = NULL, Auto_Close_Service_Call = 0, Auto_Close_Source_ID = NULL, Auto_Close_Type_ID = NULL
    WHERE  Datapak_Fault_ID = '70'

IF NOT EXISTS(SELECT 1 FROM [Datapak_Fault]WHERE Datapak_Fault_ID = '71')
    INSERT [Datapak_Fault] ( Datapak_Fault_ID, Call_Fault_ID, Datapak_Fault_Code, Datapak_Fault_Supplementary_Code, Datapak_Fault_Text, Datapak_Fault_Auto_Log_Service_Call_Non_Critical, Datapak_Fault_Auto_Log_Service_Call_Critical, Datapak_Fault_Source_Protocol, TYPE, SendMail, Mail_TO, Mail_CC, Auto_Close_Service_Call, Auto_Close_Source_ID, Auto_Close_Type_ID )
    SELECT 71, 1, 255, 10, '** UNKNOWN **', 0, 0, 3, NULL, 0, NULL, NULL, 0, NULL, NULL
ELSE
    UPDATE [Datapak_Fault]
    SET    Call_Fault_ID = 1, Datapak_Fault_Code = 255, Datapak_Fault_Supplementary_Code = 10, Datapak_Fault_Text = '** UNKNOWN **', Datapak_Fault_Auto_Log_Service_Call_Non_Critical = 0, Datapak_Fault_Auto_Log_Service_Call_Critical = 0, Datapak_Fault_Source_Protocol = 3, TYPE = NULL, SendMail = 0, Mail_TO = NULL, Mail_CC = NULL, Auto_Close_Service_Call = 0, Auto_Close_Source_ID = NULL, Auto_Close_Type_ID = NULL
    WHERE  Datapak_Fault_ID = '71'

IF NOT EXISTS(SELECT 1 FROM [Datapak_Fault]WHERE Datapak_Fault_ID = '72')
    INSERT [Datapak_Fault] ( Datapak_Fault_ID, Call_Fault_ID, Datapak_Fault_Code, Datapak_Fault_Supplementary_Code, Datapak_Fault_Text, Datapak_Fault_Auto_Log_Service_Call_Non_Critical, Datapak_Fault_Auto_Log_Service_Call_Critical, Datapak_Fault_Source_Protocol, TYPE, SendMail, Mail_TO, Mail_CC, Auto_Close_Service_Call, Auto_Close_Source_ID, Auto_Close_Type_ID )
    SELECT 72, 1, 20, 14, 'Drop Door Open', 0, 0, 3, NULL, 0, NULL, NULL, 0, NULL, NULL
ELSE
    UPDATE [Datapak_Fault]
    SET    Call_Fault_ID = 1, Datapak_Fault_Code = 20, Datapak_Fault_Supplementary_Code = 14, Datapak_Fault_Text = 'Drop Door Open', Datapak_Fault_Auto_Log_Service_Call_Non_Critical = 0, Datapak_Fault_Auto_Log_Service_Call_Critical = 0, Datapak_Fault_Source_Protocol = 3, TYPE = NULL, SendMail = 0, Mail_TO = NULL, Mail_CC = NULL, Auto_Close_Service_Call = 0, Auto_Close_Source_ID = NULL, Auto_Close_Type_ID = NULL
    WHERE  Datapak_Fault_ID = '72'

IF NOT EXISTS(SELECT 1 FROM [Datapak_Fault]WHERE Datapak_Fault_ID = '73')
    INSERT [Datapak_Fault] ( Datapak_Fault_ID, Call_Fault_ID, Datapak_Fault_Code, Datapak_Fault_Supplementary_Code, Datapak_Fault_Text, Datapak_Fault_Auto_Log_Service_Call_Non_Critical, Datapak_Fault_Auto_Log_Service_Call_Critical, Datapak_Fault_Source_Protocol, TYPE, SendMail, Mail_TO, Mail_CC, Auto_Close_Service_Call, Auto_Close_Source_ID, Auto_Close_Type_ID )
    SELECT 73, 1, 20, 15, 'Drop Door Closed', 0, 0, 3, NULL, 0, NULL, NULL, 0, NULL, NULL
ELSE
    UPDATE [Datapak_Fault]
    SET    Call_Fault_ID = 1, Datapak_Fault_Code = 20, Datapak_Fault_Supplementary_Code = 15, Datapak_Fault_Text = 'Drop Door Closed', Datapak_Fault_Auto_Log_Service_Call_Non_Critical = 0, Datapak_Fault_Auto_Log_Service_Call_Critical = 0, Datapak_Fault_Source_Protocol = 3, TYPE = NULL, SendMail = 0, Mail_TO = NULL, Mail_CC = NULL, Auto_Close_Service_Call = 0, Auto_Close_Source_ID = NULL, Auto_Close_Type_ID = NULL
    WHERE  Datapak_Fault_ID = '73'

IF NOT EXISTS(SELECT 1 FROM [Datapak_Fault]WHERE Datapak_Fault_ID = '74')
    INSERT [Datapak_Fault] ( Datapak_Fault_ID, Call_Fault_ID, Datapak_Fault_Code, Datapak_Fault_Supplementary_Code, Datapak_Fault_Text, Datapak_Fault_Auto_Log_Service_Call_Non_Critical, Datapak_Fault_Auto_Log_Service_Call_Critical, Datapak_Fault_Source_Protocol, TYPE, SendMail, Mail_TO, Mail_CC, Auto_Close_Service_Call, Auto_Close_Source_ID, Auto_Close_Type_ID )
    SELECT 74, 1, 0, 0, '** UNKNOWN **', 0, 0, 3, NULL, 0, NULL, NULL, 0, NULL, NULL
ELSE
    UPDATE [Datapak_Fault]
    SET    Call_Fault_ID = 1, Datapak_Fault_Code = 0, Datapak_Fault_Supplementary_Code = 0, Datapak_Fault_Text = '** UNKNOWN **', Datapak_Fault_Auto_Log_Service_Call_Non_Critical = 0, Datapak_Fault_Auto_Log_Service_Call_Critical = 0, Datapak_Fault_Source_Protocol = 3, TYPE = NULL, SendMail = 0, Mail_TO = NULL, Mail_CC = NULL, Auto_Close_Service_Call = 0, Auto_Close_Source_ID = NULL, Auto_Close_Type_ID = NULL
    WHERE  Datapak_Fault_ID = '74'

IF NOT EXISTS(SELECT 1 FROM [Datapak_Fault]WHERE Datapak_Fault_ID = '75')
    INSERT [Datapak_Fault] ( Datapak_Fault_ID, Call_Fault_ID, Datapak_Fault_Code, Datapak_Fault_Supplementary_Code, Datapak_Fault_Text, Datapak_Fault_Auto_Log_Service_Call_Non_Critical, Datapak_Fault_Auto_Log_Service_Call_Critical, Datapak_Fault_Source_Protocol, TYPE, SendMail, Mail_TO, Mail_CC, Auto_Close_Service_Call, Auto_Close_Source_ID, Auto_Close_Type_ID )
    SELECT 75, 1, 21, 8, '** UNKNOWN **', 0, 0, 3, NULL, 0, NULL, NULL, 0, NULL, NULL
ELSE
    UPDATE [Datapak_Fault]
    SET    Call_Fault_ID = 1, Datapak_Fault_Code = 21, Datapak_Fault_Supplementary_Code = 8, Datapak_Fault_Text = '** UNKNOWN **', Datapak_Fault_Auto_Log_Service_Call_Non_Critical = 0, Datapak_Fault_Auto_Log_Service_Call_Critical = 0, Datapak_Fault_Source_Protocol = 3, TYPE = NULL, SendMail = 0, Mail_TO = NULL, Mail_CC = NULL, Auto_Close_Service_Call = 0, Auto_Close_Source_ID = NULL, Auto_Close_Type_ID = NULL
    WHERE  Datapak_Fault_ID = '75'

IF NOT EXISTS(SELECT 1 FROM [Datapak_Fault]WHERE Datapak_Fault_ID = '76')
    INSERT [Datapak_Fault] ( Datapak_Fault_ID, Call_Fault_ID, Datapak_Fault_Code, Datapak_Fault_Supplementary_Code, Datapak_Fault_Text, Datapak_Fault_Auto_Log_Service_Call_Non_Critical, Datapak_Fault_Auto_Log_Service_Call_Critical, Datapak_Fault_Source_Protocol, TYPE, SendMail, Mail_TO, Mail_CC, Auto_Close_Service_Call, Auto_Close_Source_ID, Auto_Close_Type_ID )
    SELECT 76, 1, 19, 6, '** UNKNOWN **', 0, 0, 3, NULL, 0, NULL, NULL, 0, NULL, NULL
ELSE
    UPDATE [Datapak_Fault]
    SET    Call_Fault_ID = 1, Datapak_Fault_Code = 19, Datapak_Fault_Supplementary_Code = 6, Datapak_Fault_Text = '** UNKNOWN **', Datapak_Fault_Auto_Log_Service_Call_Non_Critical = 0, Datapak_Fault_Auto_Log_Service_Call_Critical = 0, Datapak_Fault_Source_Protocol = 3, TYPE = NULL, SendMail = 0, Mail_TO = NULL, Mail_CC = NULL, Auto_Close_Service_Call = 0, Auto_Close_Source_ID = NULL, Auto_Close_Type_ID = NULL
    WHERE  Datapak_Fault_ID = '76'

IF NOT EXISTS(SELECT 1 FROM [Datapak_Fault]WHERE Datapak_Fault_ID = '341')
    INSERT [Datapak_Fault] ( Datapak_Fault_ID, Call_Fault_ID, Datapak_Fault_Code, Datapak_Fault_Supplementary_Code, Datapak_Fault_Text, Datapak_Fault_Auto_Log_Service_Call_Non_Critical, Datapak_Fault_Auto_Log_Service_Call_Critical, Datapak_Fault_Source_Protocol, TYPE, SendMail, Mail_TO, Mail_CC, Auto_Close_Service_Call, Auto_Close_Source_ID, Auto_Close_Type_ID )
    SELECT 341, 357, 300, 1, 'Cosmetic', 0, 1, 3, 1, 0, NULL, NULL, 0, NULL, NULL
ELSE
    UPDATE [Datapak_Fault]
    SET    Call_Fault_ID = 357, Datapak_Fault_Code = 300, Datapak_Fault_Supplementary_Code = 1, Datapak_Fault_Text = 'Cosmetic', Datapak_Fault_Auto_Log_Service_Call_Non_Critical = 0, Datapak_Fault_Auto_Log_Service_Call_Critical = 1, Datapak_Fault_Source_Protocol = 3, TYPE = 1, SendMail = 0, Mail_TO = NULL, Mail_CC = NULL, Auto_Close_Service_Call = 0, Auto_Close_Source_ID = NULL, Auto_Close_Type_ID = NULL
    WHERE  Datapak_Fault_ID = '341'

IF NOT EXISTS(SELECT 1 FROM [Datapak_Fault]WHERE Datapak_Fault_ID = '342')
    INSERT [Datapak_Fault] ( Datapak_Fault_ID, Call_Fault_ID, Datapak_Fault_Code, Datapak_Fault_Supplementary_Code, Datapak_Fault_Text, Datapak_Fault_Auto_Log_Service_Call_Non_Critical, Datapak_Fault_Auto_Log_Service_Call_Critical, Datapak_Fault_Source_Protocol, TYPE, SendMail, Mail_TO, Mail_CC, Auto_Close_Service_Call, Auto_Close_Source_ID, Auto_Close_Type_ID )
    SELECT 342, 358, 300, 2, 'Out of Order', 0, 1, 3, 1, 0, NULL, NULL, 0, NULL, NULL
ELSE
    UPDATE [Datapak_Fault]
    SET    Call_Fault_ID = 358, Datapak_Fault_Code = 300, Datapak_Fault_Supplementary_Code = 2, Datapak_Fault_Text = 'Out of Order', Datapak_Fault_Auto_Log_Service_Call_Non_Critical = 0, Datapak_Fault_Auto_Log_Service_Call_Critical = 1, Datapak_Fault_Source_Protocol = 3, TYPE = 1, SendMail = 0, Mail_TO = NULL, Mail_CC = NULL, Auto_Close_Service_Call = 0, Auto_Close_Source_ID = NULL, Auto_Close_Type_ID = NULL
    WHERE  Datapak_Fault_ID = '342'

IF NOT EXISTS(SELECT 1 FROM [Datapak_Fault]WHERE Datapak_Fault_ID = '343')
    INSERT [Datapak_Fault] ( Datapak_Fault_ID, Call_Fault_ID, Datapak_Fault_Code, Datapak_Fault_Supplementary_Code, Datapak_Fault_Text, Datapak_Fault_Auto_Log_Service_Call_Non_Critical, Datapak_Fault_Auto_Log_Service_Call_Critical, Datapak_Fault_Source_Protocol, TYPE, SendMail, Mail_TO, Mail_CC, Auto_Close_Service_Call, Auto_Close_Source_ID, Auto_Close_Type_ID )
    SELECT 343, 359, 300, 3, 'Reason 2', 0, 1, 3, 1, 0, NULL, NULL, 0, NULL, NULL
ELSE
    UPDATE [Datapak_Fault]
    SET    Call_Fault_ID = 359, Datapak_Fault_Code = 300, Datapak_Fault_Supplementary_Code = 3, Datapak_Fault_Text = 'Reason 2', Datapak_Fault_Auto_Log_Service_Call_Non_Critical = 0, Datapak_Fault_Auto_Log_Service_Call_Critical = 1, Datapak_Fault_Source_Protocol = 3, TYPE = 1, SendMail = 0, Mail_TO = NULL, Mail_CC = NULL, Auto_Close_Service_Call = 0, Auto_Close_Source_ID = NULL, Auto_Close_Type_ID = NULL
    WHERE  Datapak_Fault_ID = '343'

IF NOT EXISTS(SELECT 1 FROM [Datapak_Fault]WHERE Datapak_Fault_ID = '344')
    INSERT [Datapak_Fault] ( Datapak_Fault_ID, Call_Fault_ID, Datapak_Fault_Code, Datapak_Fault_Supplementary_Code, Datapak_Fault_Text, Datapak_Fault_Auto_Log_Service_Call_Non_Critical, Datapak_Fault_Auto_Log_Service_Call_Critical, Datapak_Fault_Source_Protocol, TYPE, SendMail, Mail_TO, Mail_CC, Auto_Close_Service_Call, Auto_Close_Source_ID, Auto_Close_Type_ID )
    SELECT 344, 360, 300, 4, 'Reason 3', 0, 1, 3, 1, 0, NULL, NULL, 0, NULL, NULL
ELSE
    UPDATE [Datapak_Fault]
    SET    Call_Fault_ID = 360, Datapak_Fault_Code = 300, Datapak_Fault_Supplementary_Code = 4, Datapak_Fault_Text = 'Reason 3', Datapak_Fault_Auto_Log_Service_Call_Non_Critical = 0, Datapak_Fault_Auto_Log_Service_Call_Critical = 1, Datapak_Fault_Source_Protocol = 3, TYPE = 1, SendMail = 0, Mail_TO = NULL, Mail_CC = NULL, Auto_Close_Service_Call = 0, Auto_Close_Source_ID = NULL, Auto_Close_Type_ID = NULL
    WHERE  Datapak_Fault_ID = '344'

IF NOT EXISTS(SELECT 1 FROM [Datapak_Fault]WHERE Datapak_Fault_ID = '345')
    INSERT [Datapak_Fault] ( Datapak_Fault_ID, Call_Fault_ID, Datapak_Fault_Code, Datapak_Fault_Supplementary_Code, Datapak_Fault_Text, Datapak_Fault_Auto_Log_Service_Call_Non_Critical, Datapak_Fault_Auto_Log_Service_Call_Critical, Datapak_Fault_Source_Protocol, TYPE, SendMail, Mail_TO, Mail_CC, Auto_Close_Service_Call, Auto_Close_Source_ID, Auto_Close_Type_ID )
    SELECT 345, 361, 300, 5, 'Reason 4', 0, 1, 3, 1, 0, NULL, NULL, 0, NULL, NULL
ELSE
    UPDATE [Datapak_Fault]
    SET    Call_Fault_ID = 361, Datapak_Fault_Code = 300, Datapak_Fault_Supplementary_Code = 5, Datapak_Fault_Text = 'Reason 4', Datapak_Fault_Auto_Log_Service_Call_Non_Critical = 0, Datapak_Fault_Auto_Log_Service_Call_Critical = 1, Datapak_Fault_Source_Protocol = 3, TYPE = 1, SendMail = 0, Mail_TO = NULL, Mail_CC = NULL, Auto_Close_Service_Call = 0, Auto_Close_Source_ID = NULL, Auto_Close_Type_ID = NULL
    WHERE  Datapak_Fault_ID = '345'

IF NOT EXISTS(SELECT 1 FROM [Datapak_Fault]WHERE Datapak_Fault_ID = '346')
    INSERT [Datapak_Fault] ( Datapak_Fault_ID, Call_Fault_ID, Datapak_Fault_Code, Datapak_Fault_Supplementary_Code, Datapak_Fault_Text, Datapak_Fault_Auto_Log_Service_Call_Non_Critical, Datapak_Fault_Auto_Log_Service_Call_Critical, Datapak_Fault_Source_Protocol, TYPE, SendMail, Mail_TO, Mail_CC, Auto_Close_Service_Call, Auto_Close_Source_ID, Auto_Close_Type_ID )
    SELECT 346, 362, 300, 6, 'Other', 0, 1, 3, 1, 0, NULL, NULL, 0, NULL, NULL
ELSE
    UPDATE [Datapak_Fault]
    SET    Call_Fault_ID = 362, Datapak_Fault_Code = 300, Datapak_Fault_Supplementary_Code = 6, Datapak_Fault_Text = 'Other', Datapak_Fault_Auto_Log_Service_Call_Non_Critical = 0, Datapak_Fault_Auto_Log_Service_Call_Critical = 1, Datapak_Fault_Source_Protocol = 3, TYPE = 1, SendMail = 0, Mail_TO = NULL, Mail_CC = NULL, Auto_Close_Service_Call = 0, Auto_Close_Source_ID = NULL, Auto_Close_Type_ID = NULL
    WHERE  Datapak_Fault_ID = '346'

IF NOT EXISTS(SELECT 1 FROM [Datapak_Fault]WHERE Datapak_Fault_ID = '351')
    INSERT [Datapak_Fault] ( Datapak_Fault_ID, Call_Fault_ID, Datapak_Fault_Code, Datapak_Fault_Supplementary_Code, Datapak_Fault_Text, Datapak_Fault_Auto_Log_Service_Call_Non_Critical, Datapak_Fault_Auto_Log_Service_Call_Critical, Datapak_Fault_Source_Protocol, TYPE, SendMail, Mail_TO, Mail_CC, Auto_Close_Service_Call, Auto_Close_Source_ID, Auto_Close_Type_ID )
    SELECT 351, 363, 400, 8, 'site comms failure / 1', 0, 1, 3, NULL, 0, NULL, NULL, 0, NULL, NULL
ELSE
    UPDATE [Datapak_Fault]
    SET    Call_Fault_ID = 363, Datapak_Fault_Code = 400, Datapak_Fault_Supplementary_Code = 8, Datapak_Fault_Text = 'site comms failure / 1', Datapak_Fault_Auto_Log_Service_Call_Non_Critical = 0, Datapak_Fault_Auto_Log_Service_Call_Critical = 1, Datapak_Fault_Source_Protocol = 3, TYPE = NULL, SendMail = 0, Mail_TO = NULL, Mail_CC = NULL, Auto_Close_Service_Call = 0, Auto_Close_Source_ID = NULL, Auto_Close_Type_ID = NULL
    WHERE  Datapak_Fault_ID = '351'

IF NOT EXISTS(SELECT 1 FROM [Datapak_Fault]WHERE Datapak_Fault_ID = '352')
    INSERT [Datapak_Fault] ( Datapak_Fault_ID, Call_Fault_ID, Datapak_Fault_Code, Datapak_Fault_Supplementary_Code, Datapak_Fault_Text, Datapak_Fault_Auto_Log_Service_Call_Non_Critical, Datapak_Fault_Auto_Log_Service_Call_Critical, Datapak_Fault_Source_Protocol, TYPE, SendMail, Mail_TO, Mail_CC, Auto_Close_Service_Call, Auto_Close_Source_ID, Auto_Close_Type_ID )
    SELECT 352, 364, 400, 9, 'site comms failure/2 - site connection resumed', 0, 1, 3, NULL, 0, NULL, NULL, 0, NULL, NULL
ELSE
    UPDATE [Datapak_Fault]
    SET    Call_Fault_ID = 364, Datapak_Fault_Code = 400, Datapak_Fault_Supplementary_Code = 9, Datapak_Fault_Text = 'site comms failure/2 - site connection resumed', Datapak_Fault_Auto_Log_Service_Call_Non_Critical = 0, Datapak_Fault_Auto_Log_Service_Call_Critical = 1, Datapak_Fault_Source_Protocol = 3, TYPE = NULL, SendMail = 0, Mail_TO = NULL, Mail_CC = NULL, Auto_Close_Service_Call = 0, Auto_Close_Source_ID = NULL, Auto_Close_Type_ID = NULL
    WHERE  Datapak_Fault_ID = '352'

IF NOT EXISTS(SELECT 1 FROM [Datapak_Fault]WHERE Datapak_Fault_ID = '353')
    INSERT [Datapak_Fault] ( Datapak_Fault_ID, Call_Fault_ID, Datapak_Fault_Code, Datapak_Fault_Supplementary_Code, Datapak_Fault_Text, Datapak_Fault_Auto_Log_Service_Call_Non_Critical, Datapak_Fault_Auto_Log_Service_Call_Critical, Datapak_Fault_Source_Protocol, TYPE, SendMail, Mail_TO, Mail_CC, Auto_Close_Service_Call, Auto_Close_Source_ID, Auto_Close_Type_ID )
    SELECT 353, 365, 300, 100, 'Machine Auto Disabled', 0, 1, 3, NULL, 0, NULL, NULL, 0, NULL, NULL
ELSE
    UPDATE [Datapak_Fault]
    SET    Call_Fault_ID = 365, Datapak_Fault_Code = 300, Datapak_Fault_Supplementary_Code = 100, Datapak_Fault_Text = 'Machine Auto Disabled', Datapak_Fault_Auto_Log_Service_Call_Non_Critical = 0, Datapak_Fault_Auto_Log_Service_Call_Critical = 1, Datapak_Fault_Source_Protocol = 3, TYPE = NULL, SendMail = 0, Mail_TO = NULL, Mail_CC = NULL, Auto_Close_Service_Call = 0, Auto_Close_Source_ID = NULL, Auto_Close_Type_ID = NULL
    WHERE  Datapak_Fault_ID = '353'

IF NOT EXISTS(SELECT 1 FROM [Datapak_Fault]WHERE Datapak_Fault_ID = '354')
    INSERT [Datapak_Fault] ( Datapak_Fault_ID, Call_Fault_ID, Datapak_Fault_Code, Datapak_Fault_Supplementary_Code, Datapak_Fault_Text, Datapak_Fault_Auto_Log_Service_Call_Non_Critical, Datapak_Fault_Auto_Log_Service_Call_Critical, Datapak_Fault_Source_Protocol, TYPE, SendMail, Mail_TO, Mail_CC, Auto_Close_Service_Call, Auto_Close_Source_ID, Auto_Close_Type_ID )
    SELECT 354, 366, 300, 101, 'Machine Auto Enabled', 0, 1, 3, NULL, 0, NULL, NULL, 0, NULL, NULL
ELSE
    UPDATE [Datapak_Fault]
    SET    Call_Fault_ID = 366, Datapak_Fault_Code = 300, Datapak_Fault_Supplementary_Code = 101, Datapak_Fault_Text = 'Machine Auto Enabled', Datapak_Fault_Auto_Log_Service_Call_Non_Critical = 0, Datapak_Fault_Auto_Log_Service_Call_Critical = 1, Datapak_Fault_Source_Protocol = 3, TYPE = NULL, SendMail = 0, Mail_TO = NULL, Mail_CC = NULL, Auto_Close_Service_Call = 0, Auto_Close_Source_ID = NULL, Auto_Close_Type_ID = NULL
    WHERE  Datapak_Fault_ID = '354'
GO

SET IDENTITY_INSERT Datapak_Fault OFF
GO

IF NOT EXISTS(SELECT 1 FROM [Datapak_Fault] WHERE Datapak_Fault_Code = 20 AND Datapak_Fault_Supplementary_Code = 1)
    INSERT [Datapak_Fault] ( Call_Fault_ID, Datapak_Fault_Code, Datapak_Fault_Supplementary_Code, Datapak_Fault_Text, Datapak_Fault_Auto_Log_Service_Call_Non_Critical, Datapak_Fault_Auto_Log_Service_Call_Critical, Datapak_Fault_Source_Protocol, TYPE, SendMail, Mail_TO, Mail_CC, Auto_Close_Service_Call, Auto_Close_Source_ID, Auto_Close_Type_ID )
    SELECT 1, 20, 1, 'Front Door Open', 0, 0, 3, NULL, 0, NULL, NULL, 0, NULL, NULL
ELSE
    UPDATE [Datapak_Fault]
    SET    Call_Fault_ID = 1, Datapak_Fault_Text = 'Front Door Open', Datapak_Fault_Auto_Log_Service_Call_Non_Critical = 0, Datapak_Fault_Auto_Log_Service_Call_Critical = 0, Datapak_Fault_Source_Protocol = 3, TYPE = NULL, SendMail = 0, Mail_TO = NULL, Mail_CC = NULL, Auto_Close_Service_Call = 0, Auto_Close_Source_ID = NULL, Auto_Close_Type_ID = NULL
    WHERE  Datapak_Fault_Code = 20 AND Datapak_Fault_Supplementary_Code = 1
GO

IF NOT EXISTS(SELECT 1 FROM [Datapak_Fault] WHERE Datapak_Fault_Code = 20 AND Datapak_Fault_Supplementary_Code = 2)
    INSERT [Datapak_Fault] ( Call_Fault_ID, Datapak_Fault_Code, Datapak_Fault_Supplementary_Code, Datapak_Fault_Text, Datapak_Fault_Auto_Log_Service_Call_Non_Critical, Datapak_Fault_Auto_Log_Service_Call_Critical, Datapak_Fault_Source_Protocol, TYPE, SendMail, Mail_TO, Mail_CC, Auto_Close_Service_Call, Auto_Close_Source_ID, Auto_Close_Type_ID )
    SELECT 1, 20, 2, 'Front Door Closed', 0, 0, 3, NULL, 0, NULL, NULL, 0, NULL, NULL
ELSE
    UPDATE [Datapak_Fault]
    SET    Call_Fault_ID = 1, Datapak_Fault_Text = 'Front Door Closed', Datapak_Fault_Auto_Log_Service_Call_Non_Critical = 0, Datapak_Fault_Auto_Log_Service_Call_Critical = 0, Datapak_Fault_Source_Protocol = 3, TYPE = NULL, SendMail = 0, Mail_TO = NULL, Mail_CC = NULL, Auto_Close_Service_Call = 0, Auto_Close_Source_ID = NULL, Auto_Close_Type_ID = NULL
    WHERE  Datapak_Fault_Code = 20 AND Datapak_Fault_Supplementary_Code = 2
GO

IF NOT EXISTS(SELECT 1 FROM [Datapak_Fault] WHERE Datapak_Fault_Code = 20 AND Datapak_Fault_Supplementary_Code = 16)
    INSERT [Datapak_Fault] ( Call_Fault_ID, Datapak_Fault_Code, Datapak_Fault_Supplementary_Code, Datapak_Fault_Text, Datapak_Fault_Auto_Log_Service_Call_Non_Critical, Datapak_Fault_Auto_Log_Service_Call_Critical, Datapak_Fault_Source_Protocol, TYPE, SendMail, Mail_TO, Mail_CC, Auto_Close_Service_Call, Auto_Close_Source_ID, Auto_Close_Type_ID )
    SELECT 1, 20, 16, 'Card Cage Open', 0, 0, 3, NULL, 0, NULL, NULL, 0, NULL, NULL
ELSE
    UPDATE [Datapak_Fault]
    SET    Call_Fault_ID = 1, Datapak_Fault_Text = 'Card Cage Open', Datapak_Fault_Auto_Log_Service_Call_Non_Critical = 0, Datapak_Fault_Auto_Log_Service_Call_Critical = 0, Datapak_Fault_Source_Protocol = 3, TYPE = NULL, SendMail = 0, Mail_TO = NULL, Mail_CC = NULL, Auto_Close_Service_Call = 0, Auto_Close_Source_ID = NULL, Auto_Close_Type_ID = NULL
    WHERE  Datapak_Fault_Code = 20 AND Datapak_Fault_Supplementary_Code = 16
GO

IF NOT EXISTS(SELECT 1 FROM [Datapak_Fault] WHERE Datapak_Fault_Code = 20 AND Datapak_Fault_Supplementary_Code = 17)
    INSERT [Datapak_Fault] ( Call_Fault_ID, Datapak_Fault_Code, Datapak_Fault_Supplementary_Code, Datapak_Fault_Text, Datapak_Fault_Auto_Log_Service_Call_Non_Critical, Datapak_Fault_Auto_Log_Service_Call_Critical, Datapak_Fault_Source_Protocol, TYPE, SendMail, Mail_TO, Mail_CC, Auto_Close_Service_Call, Auto_Close_Source_ID, Auto_Close_Type_ID )
    SELECT 1, 20, 17, 'Card Cage Closed', 0, 0, 3, NULL, 0, NULL, NULL, 0, NULL, NULL
ELSE
    UPDATE [Datapak_Fault]
    SET    Call_Fault_ID = 1, Datapak_Fault_Text = 'Card Cage Closed', Datapak_Fault_Auto_Log_Service_Call_Non_Critical = 0, Datapak_Fault_Auto_Log_Service_Call_Critical = 0, Datapak_Fault_Source_Protocol = 3, TYPE = NULL, SendMail = 0, Mail_TO = NULL, Mail_CC = NULL, Auto_Close_Service_Call = 0, Auto_Close_Source_ID = NULL, Auto_Close_Type_ID = NULL
    WHERE  Datapak_Fault_Code = 20 AND Datapak_Fault_Supplementary_Code = 17
GO

IF NOT EXISTS(SELECT 1 FROM [Datapak_Fault] WHERE Datapak_Fault_Code = 20 AND Datapak_Fault_Supplementary_Code = 22)
    INSERT [Datapak_Fault] ( Call_Fault_ID, Datapak_Fault_Code, Datapak_Fault_Supplementary_Code, Datapak_Fault_Text, Datapak_Fault_Auto_Log_Service_Call_Non_Critical, Datapak_Fault_Auto_Log_Service_Call_Critical, Datapak_Fault_Source_Protocol, TYPE, SendMail, Mail_TO, Mail_CC, Auto_Close_Service_Call, Auto_Close_Source_ID, Auto_Close_Type_ID )
    SELECT 1, 20, 22, 'GMU Compartment Opened', 0, 0, 3, NULL, 0, NULL, NULL, 0, NULL, NULL
ELSE
    UPDATE [Datapak_Fault]
    SET    Call_Fault_ID = 1, Datapak_Fault_Text = 'GMU Compartment Opened', Datapak_Fault_Auto_Log_Service_Call_Non_Critical = 0, Datapak_Fault_Auto_Log_Service_Call_Critical = 0, Datapak_Fault_Source_Protocol = 3, TYPE = NULL, SendMail = 0, Mail_TO = NULL, Mail_CC = NULL, Auto_Close_Service_Call = 0, Auto_Close_Source_ID = NULL, Auto_Close_Type_ID = NULL
    WHERE  Datapak_Fault_Code = 20 AND Datapak_Fault_Supplementary_Code = 22
GO

IF NOT EXISTS(SELECT 1 FROM [Datapak_Fault] WHERE Datapak_Fault_Code = 20 AND Datapak_Fault_Supplementary_Code = 23)
    INSERT [Datapak_Fault] ( Call_Fault_ID, Datapak_Fault_Code, Datapak_Fault_Supplementary_Code, Datapak_Fault_Text, Datapak_Fault_Auto_Log_Service_Call_Non_Critical, Datapak_Fault_Auto_Log_Service_Call_Critical, Datapak_Fault_Source_Protocol, TYPE, SendMail, Mail_TO, Mail_CC, Auto_Close_Service_Call, Auto_Close_Source_ID, Auto_Close_Type_ID )
    SELECT 1, 20, 23, 'GMU Compartment Closed', 0, 0, 3, NULL, 0, NULL, NULL, 0, NULL, NULL
ELSE
    UPDATE [Datapak_Fault]
    SET    Call_Fault_ID = 1, Datapak_Fault_Text = 'GMU Compartment Closed', Datapak_Fault_Auto_Log_Service_Call_Non_Critical = 0, Datapak_Fault_Auto_Log_Service_Call_Critical = 0, Datapak_Fault_Source_Protocol = 3, TYPE = NULL, SendMail = 0, Mail_TO = NULL, Mail_CC = NULL, Auto_Close_Service_Call = 0, Auto_Close_Source_ID = NULL, Auto_Close_Type_ID = NULL
    WHERE  Datapak_Fault_Code = 20 AND Datapak_Fault_Supplementary_Code = 23
GO

IF NOT EXISTS(SELECT 1 FROM [Datapak_Fault] WHERE Datapak_Fault_Code = 20 AND Datapak_Fault_Supplementary_Code = 32)
    INSERT [Datapak_Fault] ( Call_Fault_ID, Datapak_Fault_Code, Datapak_Fault_Supplementary_Code, Datapak_Fault_Text, Datapak_Fault_Auto_Log_Service_Call_Non_Critical, Datapak_Fault_Auto_Log_Service_Call_Critical, Datapak_Fault_Source_Protocol, TYPE, SendMail, Mail_TO, Mail_CC, Auto_Close_Service_Call, Auto_Close_Source_ID, Auto_Close_Type_ID )
    SELECT 1, 20, 32, 'Game MPU Removed', 0, 0, 3, NULL, 0, NULL, NULL, 0, NULL, NULL
ELSE
    UPDATE [Datapak_Fault]
    SET    Call_Fault_ID = 1, Datapak_Fault_Text = 'Game MPU Removed', Datapak_Fault_Auto_Log_Service_Call_Non_Critical = 0, Datapak_Fault_Auto_Log_Service_Call_Critical = 0, Datapak_Fault_Source_Protocol = 3, TYPE = NULL, SendMail = 0, Mail_TO = NULL, Mail_CC = NULL, Auto_Close_Service_Call = 0, Auto_Close_Source_ID = NULL, Auto_Close_Type_ID = NULL
    WHERE  Datapak_Fault_Code = 20 AND Datapak_Fault_Supplementary_Code = 32
GO

IF NOT EXISTS(SELECT 1 FROM [Datapak_Fault] WHERE Datapak_Fault_Code = 20 AND Datapak_Fault_Supplementary_Code = 35)
    INSERT [Datapak_Fault] ( Call_Fault_ID, Datapak_Fault_Code, Datapak_Fault_Supplementary_Code, Datapak_Fault_Text, Datapak_Fault_Auto_Log_Service_Call_Non_Critical, Datapak_Fault_Auto_Log_Service_Call_Critical, Datapak_Fault_Source_Protocol, TYPE, SendMail, Mail_TO, Mail_CC, Auto_Close_Service_Call, Auto_Close_Source_ID, Auto_Close_Type_ID )
    SELECT 1, 20, 35, 'Aux fill door Opened', 0, 0, 3, NULL, 0, NULL, NULL, 0, NULL, NULL
ELSE
    UPDATE [Datapak_Fault]
    SET    Call_Fault_ID = 1, Datapak_Fault_Text = 'Aux fill door Opened', Datapak_Fault_Auto_Log_Service_Call_Non_Critical = 0, Datapak_Fault_Auto_Log_Service_Call_Critical = 0, Datapak_Fault_Source_Protocol = 3, TYPE = NULL, SendMail = 0, Mail_TO = NULL, Mail_CC = NULL, Auto_Close_Service_Call = 0, Auto_Close_Source_ID = NULL, Auto_Close_Type_ID = NULL
    WHERE  Datapak_Fault_Code = 20 AND Datapak_Fault_Supplementary_Code = 35
GO

IF NOT EXISTS(SELECT 1 FROM [Datapak_Fault] WHERE Datapak_Fault_Code = 20 AND Datapak_Fault_Supplementary_Code = 36)
    INSERT [Datapak_Fault] ( Call_Fault_ID, Datapak_Fault_Code, Datapak_Fault_Supplementary_Code, Datapak_Fault_Text, Datapak_Fault_Auto_Log_Service_Call_Non_Critical, Datapak_Fault_Auto_Log_Service_Call_Critical, Datapak_Fault_Source_Protocol, TYPE, SendMail, Mail_TO, Mail_CC, Auto_Close_Service_Call, Auto_Close_Source_ID, Auto_Close_Type_ID )
    SELECT 1, 20, 36, 'Aux fill door Closed', 0, 0, 3, NULL, 0, NULL, NULL, 0, NULL, NULL
ELSE
    UPDATE [Datapak_Fault]
    SET    Call_Fault_ID = 1, Datapak_Fault_Text = 'Aux fill door Closed', Datapak_Fault_Auto_Log_Service_Call_Non_Critical = 0, Datapak_Fault_Auto_Log_Service_Call_Critical = 0, Datapak_Fault_Source_Protocol = 3, TYPE = NULL, SendMail = 0, Mail_TO = NULL, Mail_CC = NULL, Auto_Close_Service_Call = 0, Auto_Close_Source_ID = NULL, Auto_Close_Type_ID = NULL
    WHERE  Datapak_Fault_Code = 20 AND Datapak_Fault_Supplementary_Code = 36
GO

IF NOT EXISTS(SELECT 1 FROM [Datapak_Fault] WHERE Datapak_Fault_Code = 20 AND Datapak_Fault_Supplementary_Code = 66)
    INSERT [Datapak_Fault] ( Call_Fault_ID, Datapak_Fault_Code, Datapak_Fault_Supplementary_Code, Datapak_Fault_Text, Datapak_Fault_Auto_Log_Service_Call_Non_Critical, Datapak_Fault_Auto_Log_Service_Call_Critical, Datapak_Fault_Source_Protocol, TYPE, SendMail, Mail_TO, Mail_CC, Auto_Close_Service_Call, Auto_Close_Source_ID, Auto_Close_Type_ID )
    SELECT 1, 20, 66, 'Acceptor Removed', 0, 0, 3, NULL, 0, NULL, NULL, 0, NULL, NULL
ELSE
    UPDATE [Datapak_Fault]
    SET    Call_Fault_ID = 1, Datapak_Fault_Text = 'Acceptor Removed', Datapak_Fault_Auto_Log_Service_Call_Non_Critical = 0, Datapak_Fault_Auto_Log_Service_Call_Critical = 0, Datapak_Fault_Source_Protocol = 3, TYPE = NULL, SendMail = 0, Mail_TO = NULL, Mail_CC = NULL, Auto_Close_Service_Call = 0, Auto_Close_Source_ID = NULL, Auto_Close_Type_ID = NULL
    WHERE  Datapak_Fault_Code = 20 AND Datapak_Fault_Supplementary_Code = 66
GO

IF NOT EXISTS(SELECT 1 FROM [Datapak_Fault] WHERE Datapak_Fault_Code = 20 AND Datapak_Fault_Supplementary_Code = 75)
    INSERT [Datapak_Fault] ( Call_Fault_ID, Datapak_Fault_Code, Datapak_Fault_Supplementary_Code, Datapak_Fault_Text, Datapak_Fault_Auto_Log_Service_Call_Non_Critical, Datapak_Fault_Auto_Log_Service_Call_Critical, Datapak_Fault_Source_Protocol, TYPE, SendMail, Mail_TO, Mail_CC, Auto_Close_Service_Call, Auto_Close_Source_ID, Auto_Close_Type_ID )
    SELECT 1, 20, 75, 'Acceptor door Opened', 0, 0, 3, NULL, 0, NULL, NULL, 0, NULL, NULL
ELSE
    UPDATE [Datapak_Fault]
    SET    Call_Fault_ID = 1, Datapak_Fault_Text = 'Acceptor door Opened', Datapak_Fault_Auto_Log_Service_Call_Non_Critical = 0, Datapak_Fault_Auto_Log_Service_Call_Critical = 0, Datapak_Fault_Source_Protocol = 3, TYPE = NULL, SendMail = 0, Mail_TO = NULL, Mail_CC = NULL, Auto_Close_Service_Call = 0, Auto_Close_Source_ID = NULL, Auto_Close_Type_ID = NULL
    WHERE  Datapak_Fault_Code = 20 AND Datapak_Fault_Supplementary_Code = 75
GO

IF NOT EXISTS(SELECT 1 FROM [Datapak_Fault] WHERE Datapak_Fault_Code = 20 AND Datapak_Fault_Supplementary_Code = 76)
    INSERT [Datapak_Fault] ( Call_Fault_ID, Datapak_Fault_Code, Datapak_Fault_Supplementary_Code, Datapak_Fault_Text, Datapak_Fault_Auto_Log_Service_Call_Non_Critical, Datapak_Fault_Auto_Log_Service_Call_Critical, Datapak_Fault_Source_Protocol, TYPE, SendMail, Mail_TO, Mail_CC, Auto_Close_Service_Call, Auto_Close_Source_ID, Auto_Close_Type_ID )
    SELECT 1, 20, 76, 'Acceptor door Closed', 0, 0, 3, NULL, 0, NULL, NULL, 0, NULL, NULL
ELSE
    UPDATE [Datapak_Fault]
    SET    Call_Fault_ID = 1, Datapak_Fault_Text = 'Acceptor door Closed', Datapak_Fault_Auto_Log_Service_Call_Non_Critical = 0, Datapak_Fault_Auto_Log_Service_Call_Critical = 0, Datapak_Fault_Source_Protocol = 3, TYPE = NULL, SendMail = 0, Mail_TO = NULL, Mail_CC = NULL, Auto_Close_Service_Call = 0, Auto_Close_Source_ID = NULL, Auto_Close_Type_ID = NULL
    WHERE  Datapak_Fault_Code = 20 AND Datapak_Fault_Supplementary_Code = 76
GO

IF NOT EXISTS(SELECT 1 FROM [Datapak_Fault] WHERE Datapak_Fault_Code = 20 AND Datapak_Fault_Supplementary_Code = 167)
    INSERT [Datapak_Fault] ( Call_Fault_ID, Datapak_Fault_Code, Datapak_Fault_Supplementary_Code, Datapak_Fault_Text, Datapak_Fault_Auto_Log_Service_Call_Non_Critical, Datapak_Fault_Auto_Log_Service_Call_Critical, Datapak_Fault_Source_Protocol, TYPE, SendMail, Mail_TO, Mail_CC, Auto_Close_Service_Call, Auto_Close_Source_ID, Auto_Close_Type_ID )
    SELECT 1, 20, 167, 'MPU compartment Opened', 0, 0, 3, NULL, 0, NULL, NULL, 0, NULL, NULL
ELSE
    UPDATE [Datapak_Fault]
    SET    Call_Fault_ID = 1, Datapak_Fault_Text = 'MPU compartment Opened', Datapak_Fault_Auto_Log_Service_Call_Non_Critical = 0, Datapak_Fault_Auto_Log_Service_Call_Critical = 0, Datapak_Fault_Source_Protocol = 3, TYPE = NULL, SendMail = 0, Mail_TO = NULL, Mail_CC = NULL, Auto_Close_Service_Call = 0, Auto_Close_Source_ID = NULL, Auto_Close_Type_ID = NULL
    WHERE  Datapak_Fault_Code = 20 AND Datapak_Fault_Supplementary_Code = 167
GO

IF NOT EXISTS(SELECT 1 FROM [Datapak_Fault] WHERE Datapak_Fault_Code = 20 AND Datapak_Fault_Supplementary_Code = 168)
    INSERT [Datapak_Fault] ( Call_Fault_ID, Datapak_Fault_Code, Datapak_Fault_Supplementary_Code, Datapak_Fault_Text, Datapak_Fault_Auto_Log_Service_Call_Non_Critical, Datapak_Fault_Auto_Log_Service_Call_Critical, Datapak_Fault_Source_Protocol, TYPE, SendMail, Mail_TO, Mail_CC, Auto_Close_Service_Call, Auto_Close_Source_ID, Auto_Close_Type_ID )
    SELECT 1, 20, 168, 'MPU compartment Closed', 0, 0, 3, NULL, 0, NULL, NULL, 0, NULL, NULL
ELSE
    UPDATE [Datapak_Fault]
    SET    Call_Fault_ID = 1, Datapak_Fault_Text = 'MPU compartment Closed', Datapak_Fault_Auto_Log_Service_Call_Non_Critical = 0, Datapak_Fault_Auto_Log_Service_Call_Critical = 0, Datapak_Fault_Source_Protocol = 3, TYPE = NULL, SendMail = 0, Mail_TO = NULL, Mail_CC = NULL, Auto_Close_Service_Call = 0, Auto_Close_Source_ID = NULL, Auto_Close_Type_ID = NULL
    WHERE  Datapak_Fault_Code = 20 AND Datapak_Fault_Supplementary_Code = 168
GO

IF NOT EXISTS(SELECT 1 FROM [Datapak_Fault] WHERE Datapak_Fault_Code = 20 AND Datapak_Fault_Supplementary_Code = 200)
    INSERT [Datapak_Fault] ( Call_Fault_ID, Datapak_Fault_Code, Datapak_Fault_Supplementary_Code, Datapak_Fault_Text, Datapak_Fault_Auto_Log_Service_Call_Non_Critical, Datapak_Fault_Auto_Log_Service_Call_Critical, Datapak_Fault_Source_Protocol, TYPE, SendMail, Mail_TO, Mail_CC, Auto_Close_Service_Call, Auto_Close_Source_ID, Auto_Close_Type_ID )
    SELECT 1, 20, 200, 'Power Off Card Cage Access', 0, 0, 3, NULL, 0, NULL, NULL, 0, NULL, NULL
ELSE
    UPDATE [Datapak_Fault]
    SET    Call_Fault_ID = 1, Datapak_Fault_Text = 'Power Off Card Cage Access', Datapak_Fault_Auto_Log_Service_Call_Non_Critical = 0, Datapak_Fault_Auto_Log_Service_Call_Critical = 0, Datapak_Fault_Source_Protocol = 3, TYPE = NULL, SendMail = 0, Mail_TO = NULL, Mail_CC = NULL, Auto_Close_Service_Call = 0, Auto_Close_Source_ID = NULL, Auto_Close_Type_ID = NULL
    WHERE  Datapak_Fault_Code = 20 AND Datapak_Fault_Supplementary_Code = 200
GO

IF NOT EXISTS(SELECT 1 FROM [Datapak_Fault] WHERE Datapak_Fault_Code = 20 AND Datapak_Fault_Supplementary_Code = 201)
    INSERT [Datapak_Fault] ( Call_Fault_ID, Datapak_Fault_Code, Datapak_Fault_Supplementary_Code, Datapak_Fault_Text, Datapak_Fault_Auto_Log_Service_Call_Non_Critical, Datapak_Fault_Auto_Log_Service_Call_Critical, Datapak_Fault_Source_Protocol, TYPE, SendMail, Mail_TO, Mail_CC, Auto_Close_Service_Call, Auto_Close_Source_ID, Auto_Close_Type_ID )
    SELECT 1, 20, 201, 'Power Off Slot Door Access', 0, 0, 3, NULL, 0, NULL, NULL, 0, NULL, NULL
ELSE
    UPDATE [Datapak_Fault]
    SET    Call_Fault_ID = 1, Datapak_Fault_Text = 'Power Off Slot Door Access', Datapak_Fault_Auto_Log_Service_Call_Non_Critical = 0, Datapak_Fault_Auto_Log_Service_Call_Critical = 0, Datapak_Fault_Source_Protocol = 3, TYPE = NULL, SendMail = 0, Mail_TO = NULL, Mail_CC = NULL, Auto_Close_Service_Call = 0, Auto_Close_Source_ID = NULL, Auto_Close_Type_ID = NULL
    WHERE  Datapak_Fault_Code = 20 AND Datapak_Fault_Supplementary_Code = 201
GO

IF NOT EXISTS(SELECT 1 FROM [Datapak_Fault] WHERE Datapak_Fault_Code = 20 AND Datapak_Fault_Supplementary_Code = 202)
    INSERT [Datapak_Fault] ( Call_Fault_ID, Datapak_Fault_Code, Datapak_Fault_Supplementary_Code, Datapak_Fault_Text, Datapak_Fault_Auto_Log_Service_Call_Non_Critical, Datapak_Fault_Auto_Log_Service_Call_Critical, Datapak_Fault_Source_Protocol, TYPE, SendMail, Mail_TO, Mail_CC, Auto_Close_Service_Call, Auto_Close_Source_ID, Auto_Close_Type_ID )
    SELECT 1, 20, 202, 'Power Off Cash Box Door Access', 0, 0, 3, NULL, 0, NULL, NULL, 0, NULL, NULL
ELSE
    UPDATE [Datapak_Fault]
    SET    Call_Fault_ID = 1, Datapak_Fault_Text = 'Power Off Cash Box Door Access', Datapak_Fault_Auto_Log_Service_Call_Non_Critical = 0, Datapak_Fault_Auto_Log_Service_Call_Critical = 0, Datapak_Fault_Source_Protocol = 3, TYPE = NULL, SendMail = 0, Mail_TO = NULL, Mail_CC = NULL, Auto_Close_Service_Call = 0, Auto_Close_Source_ID = NULL, Auto_Close_Type_ID = NULL
    WHERE  Datapak_Fault_Code = 20 AND Datapak_Fault_Supplementary_Code = 202
GO

IF NOT EXISTS(SELECT 1 FROM [Datapak_Fault] WHERE Datapak_Fault_Code = 20 AND Datapak_Fault_Supplementary_Code = 203)
    INSERT [Datapak_Fault] ( Call_Fault_ID, Datapak_Fault_Code, Datapak_Fault_Supplementary_Code, Datapak_Fault_Text, Datapak_Fault_Auto_Log_Service_Call_Non_Critical, Datapak_Fault_Auto_Log_Service_Call_Critical, Datapak_Fault_Source_Protocol, TYPE, SendMail, Mail_TO, Mail_CC, Auto_Close_Service_Call, Auto_Close_Source_ID, Auto_Close_Type_ID )
    SELECT 1, 20, 203, 'Power Off Drop Door Access', 0, 0, 3, NULL, 0, NULL, NULL, 0, NULL, NULL
ELSE
    UPDATE [Datapak_Fault]
    SET    Call_Fault_ID = 1, Datapak_Fault_Text = 'Power Off Drop Door Access', Datapak_Fault_Auto_Log_Service_Call_Non_Critical = 0, Datapak_Fault_Auto_Log_Service_Call_Critical = 0, Datapak_Fault_Source_Protocol = 3, TYPE = NULL, SendMail = 0, Mail_TO = NULL, Mail_CC = NULL, Auto_Close_Service_Call = 0, Auto_Close_Source_ID = NULL, Auto_Close_Type_ID = NULL
    WHERE  Datapak_Fault_Code = 20 AND Datapak_Fault_Supplementary_Code = 203
GO

IF NOT EXISTS(SELECT 1 FROM [Datapak_Fault] WHERE Datapak_Fault_Code = 22 AND Datapak_Fault_Supplementary_Code = 37)
    INSERT [Datapak_Fault] ( Call_Fault_ID, Datapak_Fault_Code, Datapak_Fault_Supplementary_Code, Datapak_Fault_Text, Datapak_Fault_Auto_Log_Service_Call_Non_Critical, Datapak_Fault_Auto_Log_Service_Call_Critical, Datapak_Fault_Source_Protocol, TYPE, SendMail, Mail_TO, Mail_CC, Auto_Close_Service_Call, Auto_Close_Source_ID, Auto_Close_Type_ID )
    SELECT 1, 22, 37, 'Employee Card In', 0, 0, 3, NULL, 0, NULL, NULL, 0, NULL, NULL
ELSE
    UPDATE [Datapak_Fault]
    SET    Call_Fault_ID = 1, Datapak_Fault_Text = 'Employee Card in', Datapak_Fault_Auto_Log_Service_Call_Non_Critical = 0, Datapak_Fault_Auto_Log_Service_Call_Critical = 0, Datapak_Fault_Source_Protocol = 3, TYPE = NULL, SendMail = 0, Mail_TO = NULL, Mail_CC = NULL, Auto_Close_Service_Call = 0, Auto_Close_Source_ID = NULL, Auto_Close_Type_ID = NULL
    WHERE  Datapak_Fault_Code = 22 AND Datapak_Fault_Supplementary_Code = 37
GO

IF NOT EXISTS(SELECT 1 FROM [Datapak_Fault] WHERE Datapak_Fault_Code = 22 AND Datapak_Fault_Supplementary_Code = 38)
    INSERT [Datapak_Fault] ( Call_Fault_ID, Datapak_Fault_Code, Datapak_Fault_Supplementary_Code, Datapak_Fault_Text, Datapak_Fault_Auto_Log_Service_Call_Non_Critical, Datapak_Fault_Auto_Log_Service_Call_Critical, Datapak_Fault_Source_Protocol, TYPE, SendMail, Mail_TO, Mail_CC, Auto_Close_Service_Call, Auto_Close_Source_ID, Auto_Close_Type_ID )
    SELECT 1, 22, 38, 'Employee Card Out', 0, 0, 3, NULL, 0, NULL, NULL, 0, NULL, NULL
ELSE
    UPDATE [Datapak_Fault]
    SET    Call_Fault_ID = 1, Datapak_Fault_Text = 'Employee card out', Datapak_Fault_Auto_Log_Service_Call_Non_Critical = 0, Datapak_Fault_Auto_Log_Service_Call_Critical = 0, Datapak_Fault_Source_Protocol = 3, TYPE = NULL, SendMail = 0, Mail_TO = NULL, Mail_CC = NULL, Auto_Close_Service_Call = 0, Auto_Close_Source_ID = NULL, Auto_Close_Type_ID = NULL
    WHERE  Datapak_Fault_Code = 22 AND Datapak_Fault_Supplementary_Code = 38
GO

/***********DATA Correction Script for the Exchange Service Call Creation**************/


EXEC [usp_UpdateDataPakFaultByCallFaultDescription] 'Cosmetic'
EXEC [usp_UpdateDataPakFaultByCallFaultDescription] 'Out of Order'
EXEC [usp_UpdateDataPakFaultByCallFaultDescription] 'Reason 2'
EXEC [usp_UpdateDataPakFaultByCallFaultDescription] 'Reason 3'
EXEC [usp_UpdateDataPakFaultByCallFaultDescription] 'Reason 4'
EXEC [usp_UpdateDataPakFaultByCallFaultDescription] 'Other'
EXEC [usp_UpdateDataPakFaultByCallFaultDescription] 'Machine Auto Disabled'
EXEC [usp_UpdateDataPakFaultByCallFaultDescription] 'Machine Auto Enabled'


/***********DATA Correction Script for the Exchange Service Call Creation**************/
