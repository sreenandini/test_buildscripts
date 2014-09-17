USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetMachineDetailsFromAsset]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetMachineDetailsFromAsset]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****
Version History
----------------------------------------
Kirubakar S       28 May 2010       Created
Yoganandh P       02 July 2010      Modified - Increased '@sMachine' paramter size to 50
----------------------------------------
***/
CREATE PROCEDURE [dbo].rsp_GetMachineDetailsFromAsset
      @sMachine VARCHAR(50),
      @SITE INT = 0
AS
      SELECT Bar_Position_Name,
             Machine_Stock_No,
             Installation_ID,
             Machine_Class.Machine_Name,
             Bar_Position.Site_ID,
             s.Site_Code
      FROM   Installation
             JOIN Bar_Position
                  ON  Installation.Bar_Position_ID = Bar_Position.Bar_Position_ID
                  AND (@SITE = 0 OR Bar_Position.Site_ID = @SITE)
             JOIN MACHINE
                  ON  Installation.Machine_ID = MACHINE.Machine_ID
             JOIN Machine_Class
                  ON  Machine_Class.Machine_Class_ID = MACHINE.Machine_Class_ID
             LEFT OUTER JOIN [Site] s
                  ON  S.Site_ID = Bar_Position.Site_ID
      WHERE  ISNULL(Bar_Position.Bar_Position_End_Date,'') = ''
             AND Installation.Installation_End_Date IS NULL
             AND (
                     MACHINE.Machine_Stock_No = @sMachine
                     OR MACHINE.Machine_Manufacturers_Serial_No = @sMachine
                 )


GO
