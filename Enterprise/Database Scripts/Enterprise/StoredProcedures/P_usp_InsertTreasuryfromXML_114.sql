USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[usp_InsertTreasuryfromXML_114]    Script Date: 04/19/2014 00:01:55 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_InsertTreasuryfromXML_114]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_InsertTreasuryfromXML_114]
GO

USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[usp_InsertTreasuryfromXML_114]    Script Date: 04/19/2014 00:01:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/* ===================================================================================================================================          
 StoredProcedure [dbo].[usp_InsertTreasuryfromXML_114]          
 -----------------------------------------------------------------------------------------------------------------------------------          
          
Procedure to insert treasury entries from Exchange into the corresponding Treasury_Entry table in Enterprise.          
          
-----------------------------------------------------------------------------------------------------------------------------------          
Revision History           
 19/06/08  Anuradha     created          
 04/08/08  Anuradha     Modified - Added code to check if reexport is done and if yes, the record is updated.          
 07/08/08  Naveen       Code Indented     
 10/03/09  Anuradha     Code changed to include Treasury_Actual_Date  
 28/03/09 poorna  Fixed bug with date fields combining
 19/08/09	Sudarsan S	treasury actual date = NULL  
 ===================================================================================================================================          
*/          
          
CREATE PROCEDURE [dbo].[usp_InsertTreasuryfromXML_114]          
    @doc VARCHAR(MAX),          
    @TreasuryID INT OUTPUT          
AS          
BEGIN        
    DECLARE @iDoc INT        
    DECLARE @iTreasury_No INT                
    DECLARE @ENTTREASURY INT              
        
    set @TreasuryID=0                
        
    SET @doc = '<?xml version="1.0" encoding="ISO-8859-1"?>' + @doc               
        
    CREATE TABLE dbo.#TempTreasury            
        (            
        Installation_ID int NULL DEFAULT (0),            
        Treasury_User varchar(50),            
        Treasury_Date varchar(30),            
        Treasury_Time varchar(50),            
        Treasury_Amount real NULL DEFAULT (0),            
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
		Treasury_Actual_Date datetime           
        )            
        
        
    --Create an internal representation of the XML document.                
        
    EXEC sp_xml_PrepareDocument @idoc OUTPUT, @doc                
        
    insert into #TempTreasury             
    SELECT A.* FROM OPENXML(@idoc, './TreasuryDetails/Treasury', 2) WITH              
        (          
        Installation_ID INT './HQ_Installation_No',              
        Treasury_User VARCHAR(50) './USERNAME',              
        Treasury_Date VARCHAR(30) './Treasury_Date',       
        Treasury_Time VARCHAR(50) './Treasury_Time',              
        Treasury_Amount REAL './Treasury_Amount',              
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
		Treasury_Actual_Date datetime './Treasury_Actual_Date'        
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
        
    IF @iTreasury_No IS NOT NULL                
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
			Treasury_Actual_Date)                
        SELECT                
            Installation_ID,             
            Treasury_User,             
            Treasury_Date,            
            Treasury_Time,            
            Treasury_Amount,            
            Treasury_Reason,            
            Treasury_Allocated,            
            Treasury_Type ,            
            Treasury_Temp ,            
            Treasury_Docket_No,            
            Treasury_Breakdown_2000p,            
            Treasury_Breakdown_1000p,            
            Treasury_Breakdown_500p ,             
            Treasury_Breakdown_200p ,            
            Treasury_Breakdown_100p,            
            Treasury_Breakdown_50p ,            
            Treasury_Breakdown_20p ,            
            Treasury_Breakdown_10p ,            
            Treasury_Breakdown_5p ,            
            Treasury_Breakdown_2p ,            
            Treasury_Float_Issued_By,            
            Treasury_Float_Recovered_Total,            
            Treasury_Float_Recovered_2000p ,            
            Treasury_Float_Recovered_1000p,            
            Treasury_Float_Recovered_500p ,            
            Treasury_Float_Recovered_200p ,                
            Treasury_Float_Recovered_100p,            
            Treasury_Float_Recovered_50p,            
            Treasury_Float_Recovered_20p,            
            Treasury_Float_Recovered_10p,               
            Treasury_Float_Recovered_5p ,            
            Treasury_Float_Recovered_2p,            
            Treasury_Membership_No,        
			Treasury_Actual_Date       
        FROM          
            dbo.#TempTreasury T        
        WHERE          
            ISNULL(Installation_ID, 0) <> 0 AND             
            Convert(Varchar(10),Installation_ID) + ' ' + Convert(Varchar(20),Treasury_Date) + ' ' + Convert(Varchar(10),Treasury_Time) NOT IN             
            (SELECT         
                    Convert(Varchar(10),Installation_Id) + ' ' + Convert(Varchar(20),Treasury_Date) + ' ' + Convert(Varchar(10),Treasury_Time)             
            FROM         
                Treasury_Entry)            
        
        IF @@Error <> 0            
        BEGIN            
            GOTO ErrorHandler            
        END            
        
        UPDATE         
            Treasury_Entry              
        SET         
            Installation_ID = T.Installation_ID            
            ,Treasury_User =T.Treasury_User            
            ,Treasury_Date =T.Treasury_Date            
            ,Treasury_Time = T.Treasury_Time            
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
     FROM          
            dbo.#TempTreasury T inner join Treasury_Entry TR            
        ON T.Installation_ID = TR.Installation_Id AND T.Treasury_Date = TR.Treasury_Date AND T.Treasury_Time = TR.Treasury_Time          
        WHERE         
            ISNULL(T.Installation_ID, 0) <> 0              
        
        IF @@Error <> 0                
        BEGIN                
            SET @TreasuryID = -9          
            GOTO ErrorHandler                
        END                
        
        SELECT         
            @TreasuryID = TR.Treasury_ID         
        FROM          
            dbo.#TempTreasury T inner join Treasury_Entry TR            
        ON T.Installation_ID = TR.Installation_Id AND T.Treasury_Date = TR.Treasury_Date AND T.Treasury_Time = TR.Treasury_Time          
        WHERE         
            ISNULL(T.Installation_ID, 0) <> 0        
		
		IF EXISTS (SELECT 1 FROM dbo.#TempTreasury WHERE Treasury_Amount < 0)
		BEGIN
			EXEC Usp_UpdateNegativeTreasuryEntry_114 @Treasury_ID = @TreasuryID 
		END
		                
        RETURN @TreasuryID                
    END                
    EXEC sp_xml_RemoveDocument @idoc                
    RETURN 0                
    ErrorHandler:                
    RETURN -99                
END                

GO


