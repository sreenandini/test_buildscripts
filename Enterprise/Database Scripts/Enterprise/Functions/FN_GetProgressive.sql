USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetProgressive]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[GetProgressive]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[GetProgressive] (@collection_ID INT)
RETURNS Float
AS
BEGIN
DECLARE @Amount float
DECLARE @SGVI_Enabled varchar(10)  
select @SGVI_Enabled = setting_value from setting where setting_name= 'SGVi_Enabled'   
DECLARE @SGVI_autodeclare varchar(10)  
select @SGVI_autodeclare = setting_value from setting where setting_name= 'Auto_Declare_Monies'  
if (@SGVI_Enabled = 'True' and @SGVI_autodeclare = 'True' )  
begin  
select @Amount = Progressive_Value_Declared 
 from   
Collection  C Inner join Installation I  
ON I.Installation_ID = C.Installation_ID and C.collection_id = @collection_ID  
end
else
begin
if (@collection_ID >0)
BEGIN
SELECT @Amount = ISNULL(SUM (Treasury_Amount),0)
FROM Treasury_Entry 
WHERE Treasury_Type IN ( 'PROGRESSIVE', 'PROG')      
and Collection_id = @collection_ID
END
ELSE
	set @Amount = 0
end
RETURN @Amount
END

GO

