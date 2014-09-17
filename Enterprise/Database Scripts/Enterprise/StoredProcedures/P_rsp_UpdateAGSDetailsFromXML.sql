USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_UpdateAGSDetailsFromXML]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_UpdateAGSDetailsFromXML]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*  
Declare @IsSuccess BIT

Exec rsp_UpdateAGSDetailsFromXML '<AGSDetail><MACHINE><Stock_No>LC0016</Stock_No><ActAssetNo>12345</ActAssetNo><NewGMUNo>0</NewGMUNo><ActSerialNo>12345</ActSerialNo></MACHINE></AGSDetail>',@IsSuccess OUTPUT
SELECT @IsSuccess
SELECT * from Machine
*/  
CREATE PROCEDURE rsp_UpdateAGSDetailsFromXML
	@doc VARCHAR(MAX),
	@IsSuccess BIT OUTPUT
AS
BEGIN
    DECLARE @Stock_No     VARCHAR(50)
           ,@ActAssetNo   VARCHAR(50)
           ,@NewGMUNo     VARCHAR(50)
           ,@ActSerialNo  VARCHAR(50)
           ,@idoc         INT
		   ,@ValidateAGSForGMU VARCHAR(10)
		   ,@MachineID INT		  
           ,@HQ_Installation_No INT  
  
    SELECT @ValidateAGSForGMU =Setting_Value 
    FROM Setting WITH(NOLOCK) 
    WHERE Setting_Name='ValidateAGSForGMU'
    
    SET @IsSuccess = 0
    --Create an internal representation of the XML document.
    EXEC sp_xml_preparedocument @idoc OUTPUT
        ,@doc  
    
    SELECT @Stock_No = Stock_No
          ,@ActAssetNo = ActAssetNo
          ,@NewGMUNo = NewGMUNo
          ,@ActSerialNo = ActSerialNo
          ,@HQ_Installation_No= HQ_Installation_No  
    FROM   OPENXML(@idoc ,'./AGSDetail/MACHINE' ,2)   
           WITH (  
               Stock_No VARCHAR(50) './Stock_No'  
              ,ActAssetNo VARCHAR(50) './ActAssetNo'  
              ,NewGMUNo VARCHAR(50) './NewGMUNo'  
              ,ActSerialNo VARCHAR(50) './ActSerialNo'  
              ,HQ_Installation_No INT './HQ_Installation_No'  
           )
    --Removes the internal representation of the XML document.
    EXEC sp_xml_removedocument @idoc
	
	SELECT @MachineID= Machine_ID FROM Installation WITH(NOLOCK) 
	WHERE  Installation_ID = @HQ_Installation_No
	
	UPDATE MACHINE 
	SET GMUNO = @NewGMUNo 
	WHERE Machine_ID=@MachineID
	AND (LOWER(@NewGMUNo) = 'null' OR LOWER(@ValidateAGSForGMU) = 'false' OR 
	(LOWER(@ValidateAGSForGMU) = 'true' AND dbo.FN_CheckAGSCombination(@ActAssetNo ,@NewGMUNo ,@ActSerialNo) = 0)) 
           
   
    IF (@@ROWCOUNT>0)
    BEGIN
        SET @IsSuccess = 1
    END
    
END

GO

