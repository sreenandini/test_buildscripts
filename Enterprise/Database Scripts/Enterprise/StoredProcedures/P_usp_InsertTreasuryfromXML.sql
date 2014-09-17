USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_InsertTreasuryfromXML]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_InsertTreasuryfromXML]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/* ===================================================================================================================================            
 StoredProcedure [dbo].[usp_InsertTreasuryfromXML]            
 -----------------------------------------------------------------------------------------------------------------------------------            
            
Procedure to insert treasury entries from Exchange into the corresponding Treasury_Entry table in Enterprise.            
            
-----------------------------------------------------------------------------------------------------------------------------------            
Revision History             
 19/06/08	Anuradha    Created            
 04/08/08	Anuradha    Modified - Added code to check if reexport is done and if yes, the record is updated.            
 07/08/08	Naveen      Code Indented       
 10/03/09	Anuradha    Code changed to include Treasury_Actual_Date    
 28/03/09	poorna		Fixed bug with date fields combining  
 19/08/09	Sudarsan S	Treasury actual date = NULL    
 16/06/10	Yoganandh P	Treasury_Reason_Code & IsManualAttendantPay added
 22/09/10	Yoganandh P	Modified - Insert/Update into Treasury_Entry based on HQ Installation No & HQ Treasury No
 ===================================================================================================================================            
*/            
            
CREATE PROCEDURE [dbo].[usp_InsertTreasuryfromXML]            
    @doc VARCHAR(MAX),            
    @TreasuryID INT OUTPUT            
AS            
BEGIN          
    DECLARE @iDoc INT          
    DECLARE @iTreasury_No INT                  
    DECLARE @ENTTREASURY INT                
	DECLARE @InstallationID INT
          
    set @TreasuryID=0                  
          
    SET @doc = '<?xml version="1.0" encoding="ISO-8859-1"?>' + @doc                 
          
    CREATE TABLE dbo.#TempTreasury              
        (              
        Installation_ID int NULL DEFAULT (0),              
        Treasury_User varchar(50),              
        Treasury_Date DATETIME,  
        Treasury_Time varchar(50),              
        Treasury_Amount float(25) NULL DEFAULT (0),              
        Treasury_Reason varchar(200),              
        Treasury_Allocated bit NULL DEFAULT (0),              
        Treasury_Type varchar(50) ,              
        Treasury_Temp bit NULL,              
        Treasury_Docket_No varchar(50),              
        Treasury_Breakdown_2000p real NULL DEFAULT (0),              
        Treasury_Breakdown_1000p real NULL DEFAULT (0),              
        Treasury_Breakdown_500p real NULL DEFAULT (0),              
        Treasury_Breakdown_200p real NULL DEFAULT (0),              
        Treasury_Breakdown_100p real NULL DEFAULT (0),              
        Treasury_Breakdown_50p real NULL DEFAULT (0),              
        Treasury_Breakdown_20p real NULL DEFAULT (0),              
        Treasury_Breakdown_10p real NULL DEFAULT (0),              
        Treasury_Breakdown_5p real NULL DEFAULT (0),              
        Treasury_Breakdown_2p real NULL DEFAULT (0),              
        Treasury_Float_Issued_By int NULL DEFAULT (0),              
        Treasury_Float_Recovered_Total real NULL DEFAULT (0),              
        Treasury_Float_Recovered_2000p real NULL DEFAULT (0),              
        Treasury_Float_Recovered_1000p real NULL DEFAULT (0),              
        Treasury_Float_Recovered_500p real NULL DEFAULT (0),              
        Treasury_Float_Recovered_200p real NULL DEFAULT (0),              
        Treasury_Float_Recovered_100p real NULL DEFAULT (0),              
        Treasury_Float_Recovered_50p real NULL DEFAULT (0),              
        Treasury_Float_Recovered_20p real NULL DEFAULT (0),              
        Treasury_Float_Recovered_10p real NULL DEFAULT (0),              
        Treasury_Float_Recovered_5p real NULL DEFAULT (0),   
        Treasury_Float_Recovered_2p real NULL DEFAULT (0),              
        Treasury_Membership_No varchar(50) ,           
		Treasury_Actual_Date datetime  ,  
		AuthorizedUser_No int,  
		Authorized_Date datetime,
		Treasury_Reason_Code INT NOT NULL DEFAULT(0),
		IsManualAttendantPay INT NOT NULL DEFAULT(0),
		UserId INT NULL DEFAULT (0),
		Treasury_VoidedDate [datetime],
		Treasury_TicketNumber VARCHAR(50)
        )              
          
          
    --Create an internal representation of the XML document.                  
          
    EXEC sp_xml_PrepareDocument @idoc OUTPUT, @doc                  
          
    insert into #TempTreasury               
    SELECT A.* FROM OPENXML(@idoc, './TreasuryDetails/Treasury', 2) WITH                
        (            
        Installation_ID INT './HQ_Installation_No',                
        Treasury_User VARCHAR(50) './USERNAME',                
        Treasury_Date DATETIME './Treasury_Date',         
        Treasury_Time VARCHAR(50) './Treasury_Time',                
        Treasury_Amount FLOAT(25) './Treasury_Amount',                
        Treasury_Reason VARCHAR(200) './Treasury_Reason',                
        Treasury_Allocated BIT './Treasury_Allocated',                
        Treasury_Type VARCHAR(50) './Treasury_Type',                
		Treasury_Temp BIT './Treasury_Temp',             
        Treasury_Docket_No VARCHAR(50) './Treasury_Docket_No',                
        Treasury_Breakdown_2000p REAL './Treasury_Breakdown_2000p',                
        Treasury_Breakdown_1000p REAL './Treasury_Breakdown_1000p',                
        Treasury_Breakdown_500p REAL './Treasury_Breakdown_500p',                
        Treasury_Breakdown_200p REAL './Treasury_Breakdown_200p',                
        Treasury_Breakdown_100p REAL './Treasury_Breakdown_100p',                
        Treasury_Breakdown_50p REAL './Treasury_Breakdown_50p',                
        Treasury_Breakdown_20p REAL './Treasury_Breakdown_20p',                
        Treasury_Breakdown_10p REAL './Treasury_Breakdown_10p',                
        Treasury_Breakdown_5p REAL './Treasury_Breakdown_5p',                
        Treasury_Breakdown_2p REAL './Treasury_Breakdown_2p',                
        Treasury_Float_Issued_By INT './Treasury_Float_Issued_By',                
        Treasury_Float_Recovered_Total REAL './Treasury_Float_Recovered_Total',                
        Treasury_Float_Recovered_2000p REAL './Treasury_Float_Recovered_2000p',                
        Treasury_Float_Recovered_1000p REAL './Treasury_Float_Recovered_1000p',                
        Treasury_Float_Recovered_500p REAL './Treasury_Float_Recovered_500p',                
        Treasury_Float_Recovered_200p REAL './Treasury_Float_Recovered_200p',                
        Treasury_Float_Recovered_100p REAL './Treasury_Float_Recovered_100p',                
        Treasury_Float_Recovered_50p REAL './Treasury_Float_Recovered_50p',                
        Treasury_Float_Recovered_20p REAL './Treasury_Float_Recovered_20p',                
        Treasury_Float_Recovered_10p REAL './Treasury_Float_Recovered_10p',                
        Treasury_Float_Recovered_5p REAL './Treasury_Float_Recovered_5p',                
        Treasury_Float_Recovered_2p REAL './Treasury_Float_Recovered_2p',                
        Treasury_Membership_No VARCHAR(50) './Treasury_Membership_No',          
		Treasury_Actual_Date datetime './Treasury_Actual_Date',  
		AuthorizedUser_No int './AuthorizedUser_No',  
		Authorized_Date datetime './Authorized_Date',                           
		Treasury_Reason_Code int './Treasury_Reason_Code',
		IsManualAttendantPay int './IsManualAttendantPay',
		UserId int './UserId',
		Treasury_VoidedDate datetime './Treasury_VoidedDate',
		Treasury_TicketNumber VARCHAR(50) './Treasury_TicketNumber'
  ) AS A                
          
    IF @@Error <> 0              
    BEGIN              
        GOTO ErrorHandler              
    END                        
         
    SELECT           
        @iTreasury_No = Treasury_No                   
    FROM           
        OPENXML(@idoc, '/TreasuryDetails', 2) WITH                  
        (Treasury_No INT '/TreasuryDetails/Treasury/Treasury_No')    

	SELECT           
        @InstallationID = Installation_ID                   
    FROM           
        #TempTreasury
          
    IF @iTreasury_No IS NOT NULL  	               
    BEGIN                  
		IF NOT EXISTS(SELECT 1 FROM Treasury_Entry WHERE INSTALLATION_ID = @InstallationID AND HQ_Treasury_No = @iTreasury_No)
		BEGIN
			INSERT INTO Treasury_Entry(                  
				INSTALLATION_ID,                  
				Treasury_User,                  
				Treasury_Date,                  
				Treasury_Time,                  
				Treasury_Amount,                  
				Treasury_Reason,                  
				Treasury_Allocated,                
				Treasury_Type,                  
				Treasury_Temp,                  
				Treasury_Docket_No,      
				Treasury_Breakdown_2000p ,                  
				Treasury_Breakdown_1000p,                    
				Treasury_Breakdown_500p ,                    
				Treasury_Breakdown_200p,                    
				Treasury_Breakdown_100p,                    
				Treasury_Breakdown_50p,                    
				Treasury_Breakdown_20p,                    
				Treasury_Breakdown_10p,                    
				Treasury_Breakdown_5p,                    
				Treasury_Breakdown_2p,                  
				Treasury_Float_Issued_By,                    
				Treasury_Float_Recovered_Total,                    
				Treasury_Float_Recovered_2000p ,                    
				Treasury_Float_Recovered_1000p,                    
				Treasury_Float_Recovered_500p,                    
				Treasury_Float_Recovered_200p,                    
				Treasury_Float_Recovered_100p,                    
				Treasury_Float_Recovered_50p,                    
				Treasury_Float_Recovered_20p,                    
				Treasury_Float_Recovered_10p,                    
				Treasury_Float_Recovered_5p,                    
				Treasury_Float_Recovered_2p,                    
				Treasury_Membership_No,          
				Treasury_Actual_Date,  
				AuthorizedUser_No,  
				Authorized_Date,
				Treasury_Reason_Code,
				IsManualAttendantPay,
				HQ_Treasury_No,
				UserId,
				Treasury_VoidedDate,
				Treasury_TicketNumber)                                    
			SELECT                  
				T.Installation_ID,               
				T.Treasury_User,               
				CONVERT(VARCHAR, T.Treasury_Date, 106),  
				CONVERT(VARCHAR, T.Treasury_Date, 108),  
				T.Treasury_Amount,              
				T.Treasury_Reason,              
				T.Treasury_Allocated,              
				T.Treasury_Type ,              
				T.Treasury_Temp ,              
				T.Treasury_Docket_No,              
				T.Treasury_Breakdown_2000p,              
				T.Treasury_Breakdown_1000p,              
				T.Treasury_Breakdown_500p ,               
				T.Treasury_Breakdown_200p ,              
				T.Treasury_Breakdown_100p,              
				T.Treasury_Breakdown_50p ,              
				T.Treasury_Breakdown_20p ,              
				T.Treasury_Breakdown_10p ,              
				T.Treasury_Breakdown_5p ,              
				T.Treasury_Breakdown_2p ,              
				T.Treasury_Float_Issued_By,              
				T.Treasury_Float_Recovered_Total,              
				T.Treasury_Float_Recovered_2000p ,              
				T.Treasury_Float_Recovered_1000p,              
				T.Treasury_Float_Recovered_500p ,              
				T.Treasury_Float_Recovered_200p ,                  
				T.Treasury_Float_Recovered_100p,              
				T.Treasury_Float_Recovered_50p,              
				T.Treasury_Float_Recovered_20p,              
				T.Treasury_Float_Recovered_10p,                 
				T.Treasury_Float_Recovered_5p ,              
				T.Treasury_Float_Recovered_2p,              
				T.Treasury_Membership_No,          
				T.Treasury_Actual_Date,  
				T.AuthorizedUser_No,  
				T.Authorized_Date,
				T.Treasury_Reason_Code,
				T.IsManualAttendantPay,
				@iTreasury_No,
				T.UserId,
				T.Treasury_VoidedDate,
				T.Treasury_TicketNumber
			FROM            
				dbo.#TempTreasury T  
			LEFT JOIN dbo.Treasury_Entry TE ON T.Installation_ID = TE.Installation_ID AND CONVERT(VARCHAR, T.Treasury_Date, 106) = TE.Treasury_Date AND CONVERT(VARCHAR, T.Treasury_Date, 114) = TE.Treasury_Time  
			WHERE   
				TE.Installation_ID IS NULL AND TE.Treasury_Date IS NULL AND TE.Treasury_Time IS NULL  		
		END
	END

    IF @@Error <> 0              
    BEGIN              
        GOTO ErrorHandler              
    END  

    IF @iTreasury_No IS NOT NULL  	
	BEGIN
		UPDATE           
			Treasury_Entry                
		SET           
			Installation_ID = T.Installation_ID              
			,Treasury_User =T.Treasury_User     
			,Treasury_Date =CONVERT(VARCHAR, T.Treasury_Date, 106)  
			,Treasury_Time = CONVERT(VARCHAR, T.Treasury_Date, 108)  
			,Treasury_Amount = T.Treasury_Amount              
			,Treasury_Reason = T.Treasury_Reason                
			,Treasury_Allocated = T.Treasury_Allocated             
			,Treasury_Type = T.Treasury_Type              
			,Treasury_Temp = T.Treasury_Temp              
			,Treasury_Docket_No = T.Treasury_Docket_No              
			,Treasury_Breakdown_2000p = T.Treasury_Breakdown_2000p              
			,Treasury_Breakdown_1000p = T.Treasury_Breakdown_1000p              
			,Treasury_Breakdown_500p = T.Treasury_Breakdown_500p              
			,Treasury_Breakdown_200p = T.Treasury_Breakdown_200p              
			,Treasury_Breakdown_100p = T.Treasury_Breakdown_100p              
			,Treasury_Breakdown_50p = T.Treasury_Breakdown_50p              
			,Treasury_Breakdown_20p = T.Treasury_Breakdown_20p              
			,Treasury_Breakdown_10p = T.Treasury_Breakdown_10p              
			,Treasury_Breakdown_5p = T.Treasury_Breakdown_5p              
			,Treasury_Breakdown_2p = T.Treasury_Breakdown_2p              
			,Treasury_Float_Issued_By =T.Treasury_Float_Issued_By              
			,Treasury_Float_Recovered_Total = T.Treasury_Float_Recovered_Total              
			,Treasury_Float_Recovered_2000p = T.Treasury_Float_Recovered_2000p              
			,Treasury_Float_Recovered_1000p =T.Treasury_Float_Recovered_1000p              
			,Treasury_Float_Recovered_500p = T.Treasury_Float_Recovered_500p              
			,Treasury_Float_Recovered_200p = T.Treasury_Float_Recovered_200p              
			,Treasury_Float_Recovered_100p = T.Treasury_Float_Recovered_100p              
			,Treasury_Float_Recovered_50p = T.Treasury_Float_Recovered_50p              
			,Treasury_Float_Recovered_20p = T.Treasury_Float_Recovered_20p              
			,Treasury_Float_Recovered_10p =T.Treasury_Float_Recovered_10p              
			,Treasury_Float_Recovered_5p =T.Treasury_Float_Recovered_5p              
			,Treasury_Float_Recovered_2p = T.Treasury_Float_Recovered_2p              
			,Treasury_Membership_No = T.Treasury_Membership_No         
			,Treasury_Actual_Date = T.Treasury_Actual_Date          
			,AuthorizedUser_No = T.AuthorizedUser_No  
			,Authorized_Date = T.Authorized_Date  
			,Treasury_Reason_Code = T.Treasury_Reason_Code
			,IsManualAttendantPay = T.IsManualAttendantPay
			,UserId=T.UserId,
			Treasury_VoidedDate=T.Treasury_VoidedDate,
			Treasury_TicketNumber=T.Treasury_TicketNumber
			
		FROM            
			dbo.#TempTreasury T inner join Treasury_Entry TR              
		ON T.Installation_ID = TR.Installation_Id --AND CONVERT(VARCHAR, T.Treasury_Date, 106) = TR.Treasury_Date AND CONVERT(VARCHAR, T.Treasury_Date, 114) = TR.Treasury_Time  
		AND TR.HQ_Treasury_No = @iTreasury_No
		WHERE           
			ISNULL(T.Installation_ID, 0) <> 0                
	END

    IF @@Error <> 0                  
    BEGIN                  
        SET @TreasuryID = -9            
        GOTO ErrorHandler                  
    END                  
      
    SELECT           
        @TreasuryID = TR.Treasury_ID           
    FROM            
        dbo.#TempTreasury T inner join Treasury_Entry TR              
    ON T.Installation_ID = TR.Installation_Id AND TR.HQ_Treasury_No = @iTreasury_No
        
    EXEC sp_xml_RemoveDocument @idoc                  
	RETURN @TreasuryID                  
    
    ErrorHandler:                  
    RETURN -99                  

END                  


GO

