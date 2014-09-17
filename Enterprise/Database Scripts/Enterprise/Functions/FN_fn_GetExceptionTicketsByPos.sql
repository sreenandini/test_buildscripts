USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fn_GetExceptionTicketsByPos]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fn_GetExceptionTicketsByPos]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION  [dbo].[fn_GetExceptionTicketsByPos]
(        
 @Bar_Pos_Name VARCHAR(50), @Collection_No int          
)        
RETURNS INT  
BEGIN           
 DECLARE @iCount INT        
 DECLARE @DeviceID INT          
 DECLARE @Installation_No INT          
 DECLARE @CollectionDate DATETIME         
 DECLARE @ExCount int        
      
 SET @iCount=0        
 SELECT @DeviceID=D.ideviceid, @Installation_No=I.Installation_no          
 FROM Exchange..bar_position B           
 INNER JOIN Exchange..installation I On B.bar_pos_no=I.bar_pos_no AND  Bar_pos_Name=@Bar_Pos_Name  AND I.End_date is null        
 INNER JOIN Exchange..machine M ON I.Machine_no=M.Machine_NO           
 INNER JOIN device D ON D.strserial=m.stock_no Collate SQL_Latin1_General_CP1_CI_AS          
        
 SELECT @CollectionDate= Collection_Date_Of_Collection  from [collection]
 WHERE Collection_Id=@Collection_No          
 AND Installation_Id=@Installation_No          
 -- select @DeviceID,  @Installation_No,@CollectionDate          
        
        
 select @iCount=      
 COUNT(  E.Error_Description)        
 from  voucher V          
 INNER JOIN ErrorCodes  E          
 ON V.ErrCode= E.Error_Code          
 AND V.ErrTime <=@CollectionDate          
 AND V.ErrDeviceID=@DeviceID          
 AND (V.strVoucherStatus IS NULL OR V.strVoucherStatus = 'PP')          
 AND ISNULL(IRedeemCollectionCompleted,0) <>1          
      
-- INCLUDED TO COUNT ERRCODE=0        
 select @ExCount=      
 COUNT('D')        
 from  voucher V          
 WHERE V.dtPaid <=@CollectionDate        
  AND V.iPayDeviceID= @DeviceID      
  AND V.strVoucherStatus = 'PP' AND Isnull(V.ErrCode,0) NOT IN (SELECT Error_Code FROM ErrorCodes)      
  AND ISNULL(IRedeemCollectionCompleted,0) <>1          
      
 RETURN  ISNULL(@iCount,0)  + ISNULl(@ExCount,0)      
END

GO

