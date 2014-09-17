USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateVoucherCollection]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateVoucherCollection]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[usp_UpdateVoucherCollection]  
 @BarCode varchar(40),  
 @Value varchar(5)   
AS      
BEGIN    
  
 UPDATE [Ticketing].[dbo].Voucher SET iRedeemCollectionCompleted = @Value    
 FROM [Ticketing].[dbo].Voucher V   
 WHERE V.strBarCode = @BarCode  
 AND V.strVoucherstatus = 'PD'  
  
END    

GO

