USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetAttendantPay]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[GetAttendantPay]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[GetAttendantPay] (@collection_ID INT)
RETURNS FLOAT
AS
BEGIN
DECLARE @Amount FLOAT
DECLARE @SGVI_Enabled varchar(10)  
select @SGVI_Enabled = setting_value from setting where setting_name= 'SGVi_Enabled'  
  
  
  
DECLARE @SGVI_autodeclare varchar(10)  
select @SGVI_autodeclare = setting_value from setting where setting_name= 'Auto_Declare_Monies'  
  
  
if (@SGVI_Enabled = 'True' and @SGVI_autodeclare = 'True' )  
begin  
  
select @Amount = Collection_DecHandpay 
 from   
Collection_calcs  C Inner join Installation I  
ON I.Installation_ID = C.Installation_ID and C.collection_id = @collection_ID
end  
else
BEGIN

if (@collection_ID >0)
BEGIN
SELECT @Amount = ISNULL(SUM (Treasury_Amount),0)
FROM Treasury_Entry 
WHERE Treasury_Type IN ( 'handpay credit', 'handpay jackpot', 'mystery jackpot', 'Attendantpay Credit','Attendantpay Jackpot', 'PROGRESSIVE', 'PROG')      
and Collection_Id = @collection_ID
END
ELSE
	set @Amount = 0
END
RETURN @Amount
END

GO

