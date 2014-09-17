USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[usp_UpdateGMUConfiguration]    Script Date: 07/31/2014 15:48:22 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateGMUConfiguration]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateGMUConfiguration]
GO

USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[usp_UpdateGMUConfiguration]    Script Date: 07/31/2014 15:48:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[usp_UpdateGMUConfiguration]
(
   
    @Description    VARCHAR(50),
    @CreateServiceCall       BIT,
    @CloseServiceCall BIT,
    @ToMail BIT,
    @Fault VARCHAR(60),
    @Type INT,
     @Datapak_Fault_ID  INT,
     @Call_Fault_ID INT
)
AS
BEGIN
	UPDATE Datapak_Fault
	SET    
		Datapak_Fault_Text=@Description,
		Datapak_Fault_Auto_Log_Service_Call_Critical = @CreateServiceCall,
	    Auto_close_Service_Call= @CloseServiceCall,
	    SendMail=@ToMail,
	    Type=@Type	       
	WHERE  Datapak_Fault_ID = @Datapak_Fault_ID
	
	UPDATE Call_Fault
	SET
	Call_Fault_Description=@Fault
	WHERE Call_Fault_ID=@Call_Fault_ID
	
END



GO


