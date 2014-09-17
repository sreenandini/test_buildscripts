USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_ExportCollectionForXMLFromEnterprise]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_ExportCollectionForXMLFromEnterprise]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- ===================================================================================================================================  
-- rsp_ExportCollectionForXMLFromEnterprise  
-- -----------------------------------------------------------------------------------------------------------------------------------  
--  
-- exports the edited collection to exchange from enterprise
--   
-- -----------------------------------------------------------------------------------------------------------------------------------  
-- Revision History   
--   
-- 13/12/2008 Madhusudhanan  Created  
-- ===================================================================================================================================  

CREATE PROCEDURE [dbo].[rsp_ExportCollectionForXMLFromEnterprise]  

@Collection_ID int  

 AS  

select Batch.Batch_Ref, Collection.Collection_ID, 
Collection.Batch_ID,
Collection.Cash_Collected_1p,
Collection.Cash_Collected_2p,
Collection.Cash_Collected_5p,
Collection.Cash_Collected_10p,
Collection.Cash_Collected_20p,
Collection.Cash_Collected_50p,
Collection.Cash_Collected_100p,
Collection.Cash_Collected_200p,
Collection.Cash_Collected_500p,
Collection.Cash_Collected_1000p,
Collection.Cash_Collected_2000p,
Collection.Cash_Collected_5000p,
Collection.Cash_Collected_10000p,
Collection.Cash_Collected_20000p,
Collection.Cash_Collected_50000p,
Collection.Cash_Collected_100000p,
Collection.Cash_Collected_200000p,
Collection.Cash_Collected_500000p,
Collection.Cash_Collected_1000000p,
Collection.DeclaredTicketPrintedValue,

 Collection.DeclaredTicketPrintedValue,    
    
 Collection.Collection_Treasury_Handpay,     
 Collection.Progressive_Value_Declared,
 collection.User_Name

from Collection  INNER JOIN Batch ON Collection.Batch_ID = Batch.Batch_ID

where collection_id=@Collection_ID

  for xml AUTO,ELEMENTS ,ROOT('Batch')


GO

