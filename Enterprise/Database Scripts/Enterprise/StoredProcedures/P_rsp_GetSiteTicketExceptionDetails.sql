USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetSiteTicketExceptionDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetSiteTicketExceptionDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =================================================================
-- rsp_GetSiteTicketExceptionDetails
-- -----------------------------------------------------------------
--
-- returns ticket exception details to be exported.
-- 
-- -----------------------------------------------------------------    
-- Revision History       
--       
-- 09/09/09 Renjish Created      
-- =================================================================    

CREATE PROCEDURE dbo.rsp_GetSiteTicketExceptionDetails
@iSiteID INT,
@NoofDays INT

AS

DECLARE @FetchDate DATETIME

SELECT  @FetchDate = TE_Date FROM dbo.Ticket_Exception WHERE TE_ID =(SELECT MAX(TE_ID) FROM dbo.Ticket_Exception)

SELECT * FROM dbo.Ticket_Exception WITH(NOLOCK) WHERE TE_Date >= @FetchDate - @NoofDays AND TE_Site_ID = @iSiteID

UNION

SELECT * FROM dbo.Ticket_Exception WITH(NOLOCK) WHERE te_hp_type = 'HANDPAY' AND TE_Status_Final_Actual IS NULL 
AND TE_Date < @FetchDate - @NoofDays

--FOR XML PATH ('Ticket'), ELEMENTS, ROOT('Ticket_Exception') 

GO

