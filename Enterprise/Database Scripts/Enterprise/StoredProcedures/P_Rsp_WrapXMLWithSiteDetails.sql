USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Rsp_WrapXMLWithSiteDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Rsp_WrapXMLWithSiteDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--        
-- Description: Retrives the Wrapper for the XMLData along with Site Information       
--        
-- Inputs:           
--               XMLData,       
--               Type of Data that is being Exported       
--               EH ID (Export History ID)      
-- Outputs:      Returns XML File      
--        
-- ============================================================================================================        
--         
-- Revision History        
--         
-- NaveenChander  02/06/08   Created        
---------------------------------------------------------------------------------------------------------------         
      
CREATE PROCEDURE Rsp_WrapXMLWithSiteDetails(@XMLData XML, @TYPE Varchar(50), @EH_ID varchar(20))      
As      
BEGIN      
    DECLARE @ResultXML AS XML      
    DECLARE @Site_Code AS VARCHAR(50)
    Select @Site_Code = EH_Site_Code From Export_History WHERE EH_ID = @EH_ID
    SET @ResultXML = (SELECT @Site_Code AS Site_Code, @TYPE AS [Type], @EH_ID AS EH_ID, @XMLData AS SiteXML FOR XML PATH('Site'), TYPE, ELEMENTS, ROOT('ROOT'))        
    select @ResultXML aS RESULT      
END      

GO

