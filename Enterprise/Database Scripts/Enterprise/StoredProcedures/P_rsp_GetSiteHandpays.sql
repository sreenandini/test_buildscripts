USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetSiteHandpays]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetSiteHandpays]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE rsp_GetSiteHandpays 
@iSiteID INT,
@NoofDays INT

AS 

SELECT * FROM dbo.Ticket_Exception WITH(NOLOCK) WHERE TE_Date >= GETDATE() - @NoofDays 
AND TE_Site_ID = @iSiteID AND TE_HP_Type = 'HANDPAY'
--FOR XML PATH ('Handpay'), ELEMENTS, ROOT('Ticket_Exception') 


GO

