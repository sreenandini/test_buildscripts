USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_CreateAuditHistory]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_CreateAuditHistory]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-------------------------------------------------------------------------- 
---
--- Description: Creates an entry in the audit_history table
---
--- Inputs:      see inputs
---
--- Outputs:     (0)   - no error .. 
---              OTHER - SQL error 
--- 
--- =======================================================================
--- 
--- Revision History
--- 
--- C.Taylor   Mar 2010			Created 
---------------------------------------------------------------------------
CREATE PROCEDURE usp_CreateAuditHistory

  @Audit_Date           datetime,
  @Audit_User_ID        int,
  @Audit_User_Name      varchar(50),
  @Audit_Module_ID      int,
  @Audit_Module_Name    varchar(50),
  @Audit_Screen_Name    varchar(50),
  @Audit_Desc           varchar(500),
  @Audit_Slot           varchar(50),        -- ?
  @Audit_Field          varchar(100),
  @Audit_Old_Vl         varchar(500),
  @Audit_New_Vl         varchar(500),
  @Audit_Operation_Type varchar(25)

AS

INSERT INTO [Audit_History]

           (
            [Audit_Date]
           ,[Audit_User_ID]
           ,[Audit_User_Name]
           ,[Audit_Module_ID]
           ,[Audit_Module_Name]
           ,[Audit_Screen_Name]
           ,[Audit_Desc]
           ,[Audit_Slot]
           ,[Audit_Field]
           ,[Audit_Old_Vl]
           ,[Audit_New_Vl]
           ,[Audit_Operation_Type]
           )
     VALUES
           (
             @Audit_Date
            ,@Audit_User_ID
            ,@Audit_User_Name 
            ,@Audit_Module_ID
            ,@Audit_Module_Name
            ,@Audit_Screen_Name
            ,@Audit_Desc
            ,@Audit_Slot
            ,@Audit_Field
            ,@Audit_Old_Vl
            ,@Audit_New_Vl
            ,@Audit_Operation_Type
)

return @@error


GO

