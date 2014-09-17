USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_SwapInstallation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_SwapInstallation]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--Installation1 -Number
--Instalaltion2 -Inst Number or zero
--BarPosNumber	- Empty Bar Pos number 
CREATE PROCEDURE [dbo].[usp_SwapInstallation]
(
@XML as varchar(8000),
 @HQINSTALLATIONNO INT OUTPUT
)
AS

BEGIN 

 Declare @INSTALLATION1 INT
 Declare @INSTALLATION2 INT
 Declare @DestBarPosNo INT
 Declare @Installation_Date varchar(30)
 Declare @Installation_Time varchar(10)
 Declare @SITE_CODE VARCHAR(50)
 declare @idoc int  

DECLARE @ERROR INT
DECLARE @BARPOSID INT

SET @ERROR = 0

--add the encoding version as we need to process special characters like pound symbol  
	SET @XML='<?xml version="1.0" encoding="ISO-8859-1"?>' + @XML  
  
--Create an internal representation of the XML document.  
  
	EXEC sp_xml_preparedocument @idoc OUTPUT, @XML  

	SELECT 
	 @INSTALLATION1=INSTALLATION1,   
	 @INSTALLATION2=INSTALLATION2,  
	 @DestBarPosNo=DestBarPosNo,  
     @Installation_Date=Installation_Date,  
     @Installation_Time=Installation_Time,  
     @SITE_CODE=SITE_CODE     
	FROM    OPENXML (@idoc, '/INSTALLATION',2)  
   WITH   
   (
	INSTALLATION1 int './INSTALLATION1', 
	INSTALLATION2 int './INSTALLATION2',
	DestBarPosNo int './DestBArPosNo',
	Installation_Date varchar(30) './Installation_Date',
	Installation_Time varchar(10) './Installation_Time',
	SITE_CODE	int './Site_Code'

	)


select @INSTALLATION1


IF EXISTS (SELECT * FROM SYSOBJECTS WHERE NAME ='#SOURCEINSTALLATION' )  
   BEGIN   
	--print 'here 0'
		DROP TABLE  #SOURCEINSTALLATION  
   END   
IF EXISTS (SELECT * FROM SYSOBJECTS WHERE NAME ='#DESTINSTALLATION' )  
   BEGIN   
	--print 'here 0'
		DROP TABLE  #DESTINSTALLATION  
   END 

--Beginning the transaction 
Begin Tran Swap

IF EXISTS(SELECT * FROM INSTALLATION WHERE INSTALLATION_ID=@INSTALLATION1 )
BEGIN
	
	select *  INTO #SOURCEINSTALLATION FROM INSTALLATION WHERE INSTALLATION_ID=@INSTALLATION1 
	--close the installation1
		UPDATE Installation   
		SET Installation_End_Date = @Installation_Date,   
			Installation_End_Time = @Installation_Time    
		WHERE Installation_ID = @INSTALLATION1 
		SET @ERROR=@@ERROR
	 
--print 'here 1'
	IF @INSTALLATION2 > 0 
	BEGIN
		select *  INTO #DESTINSTALLATION FROM INSTALLATION WHERE INSTALLATION_ID=@INSTALLATION2 
		SELECT @BARPOSID = BAR_POSITION.Bar_Position_ID   FROM INSTALLATION 
		join BAR_POSITION ON BAR_POSITION.Bar_Position_ID=INSTALLATION.Bar_Position_ID
		join SITE ON BAR_POSITION.Site_ID=Site.Site_ID
		WHERE INSTALLATION_ID=@INSTALLATION2 and Site.Site_Code=@SITE_CODE
		--Check is it closed 
		--if closed go on with the installation or close it first
		--Close the installation
		IF EXISTS(SELECT 1 FROM INSTALLATION WHERE INSTALLATION_ID=@INSTALLATION1 and INSTALLATION_END_DATE IS NULL)
		BEGIN
			UPDATE Installation   
			SET Installation_End_Date = @Installation_Date,   
				Installation_End_Time = @Installation_Time    
			WHERE Installation_ID = @INSTALLATION2 
			SET @ERROR=@@ERROR
		END
	END
	ELSE
	BEGIN
		SELECT @BARPOSID = Bar_Position_ID  FROM BAR_POSITION 
		Join SITE ON BAR_POSITION.Site_ID=Site.Site_ID
		WHERE BAR_POSITION.Bar_Position_Name=cast(@DestBarPosNo as int) AND Site.Site_Code=@SITE_CODE
	END
	
--print 'here 2'
		INSERT INTO installation                   
		(                  
        Bar_Position_ID,                  
        Machine_ID,                  
        Installation_Start_Date,                  
        Installation_Start_Time,        
        Datapak_ID,                  
        Installation_Price_Per_Play,                  
        Installation_RDC_Datapak_Type,                  
        Installation_RDC_Datapak_Version,                  
        Installation_Token_Value,                  
        Installation_Initial_Change,                  
        Installation_Initial_VTP)                  
    
		Select                  
        @BARPOSID,                  
        Machine_ID,                  
        @Installation_Date,                  
        @Installation_Time,        
        Datapak_ID,        
        Installation_Price_Per_Play,                  
        Installation_RDC_Datapak_Type,                  
        Installation_RDC_Datapak_Version,                  
        Installation_Token_Value,                  
        Installation_Initial_Change,                  
        Installation_Initial_VTP 
		FROM  #SOURCEINSTALLATION    
		SET @ERROR=@@ERROR 

		SET @HQINSTALLATIONNO=SCOPE_IDENTITY() --IDENT_CURRENT('Installation')           
               
	
	

END
else
Begin
	 --no source installation 
	 SET @HQINSTALLATIONNO=-100
end
	IF @ERROR=0
		Begin
			Commit Tran Swap
			Print 'Committed'
			Print @HQINSTALLATIONNO
		End
		ELSE
		BEgin
			Rollback Tran Swap
			SET @HQINSTALLATIONNO=-99
			Print 'Rolledback'
			
		End
END




GO

