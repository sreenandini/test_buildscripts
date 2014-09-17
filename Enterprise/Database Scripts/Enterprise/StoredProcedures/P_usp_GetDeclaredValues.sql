USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_GetDeclaredValues]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_GetDeclaredValues]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================  
-- Author:  <Rakesh Marwaha>  
-- ALTER  date: <19/04/2007>  
-- Description: <For Meter Spikes>  
-- =============================================  
CREATE PROCEDURE [dbo].[usp_GetDeclaredValues]   
 -- Add the parameters for the stored procedure here  
 @collID as int  
AS  
BEGIN  
 -- SET NOCOUNT ON added to prevent extra result sets from  
 -- interfering with SELECT statements.  
 SET NOCOUNT ON;  
  
    -- Insert statements for procedure here  
 select cash_collected_100p,cash_collected_200p,cash_collected_500p,  
 cash_collected_1000p,cash_collected_2000p,cash_collected_5000p,  
 cash_collected_10000p,COLLECTION_RDC_CANCELLED_CREDITS,  
 collection_Treasury_Handpay,DeclaredTicketValue,  
 DeclaredTicketPrintedValue,  
 DeclaredTicketQty,DeclaredTicketPrintedQty from Collection   
 where Collection_ID=@collID  
END  

GO

