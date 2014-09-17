USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetExceptionTicketsByPos]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetExceptionTicketsByPos]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE  PROC dbo.[rsp_GetExceptionTicketsByPos]
 @Bar_Pos_Name VARCHAR(50),          
 @Collection_No int          
AS          
BEGIN           
 DECLARE @DeviceID INT          
 DECLARE @Installation_No INT          
 DECLARE @CollectionDate DATETIME          
           
 SELECT @DeviceID=D.ideviceid, @Installation_No=I.Installation_id          
 FROM bar_position B           
 INNER JOIN installation I On B.Bar_Position_ID=I.Bar_Position_ID AND  Bar_position_Name=@Bar_Pos_Name  AND I.Installation_End_Date is null          
 INNER JOIN machine M ON I.Machine_id=M.Machine_id           
 INNER JOIN device D ON D.strserial=m.machine_stock_no Collate SQL_Latin1_General_CP1_CI_AS          
          
 SELECT @CollectionDate= Collection_Date_Of_Collection  from collection           
 WHERE Collection_id=@Collection_No          
 AND Installation_id=@Installation_No          
-- select @DeviceID,  @Installation_No,@CollectionDate          
           
 select  LTRIM(RTRIM(V.strBarcode)) strBarcode ,cast ((V.iAmount/ 100.00 ) as decimal(10,2)) iAmount, ISNULL(V.ErrCode,0) ErrCode ,@Bar_Pos_Name As Device,          
   @Installation_No as  Installation_id,@Collection_No as Collection_No,E.Error_Description          
 from  voucher V          
 INNER JOIN ErrorCodes  E          
 ON V.ErrCode= E.Error_Code          
 AND V.ErrTime <=@CollectionDate          
 AND V.ErrDeviceID=@DeviceID          
 AND (V.strVoucherStatus IS NULL OR V.strVoucherStatus = 'PP')          
 AND ISNULL(IRedeemCollectionCompleted,0) <>1          
 UNION          
  select  LTRIM(RTRIM(V.strBarcode)) strBarcode ,cast ((V.iAmount/ 100.00 ) as decimal(10,2)) iAmount, ISNULL(V.ErrCode,0) ErrCode  ,@Bar_Pos_Name As Device,            
   @Installation_No as  Installation_no,@Collection_No as Collection_No,'Un Known Error' AS Error_Description            
  from  voucher V           
  WHERE V.dtPaid <=@CollectionDate            
  AND V.iPayDeviceID= @DeviceID          
  AND V.strVoucherStatus = 'PP' AND Isnull(V.ErrCode,0) NOT IN (SELECT Error_Code FROM ErrorCodes)          
  AND ISNULL(IRedeemCollectionCompleted,0) <>1          
          
END

GO

