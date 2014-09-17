USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_AddModifyStaffDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_AddModifyStaffDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*    
* Revision History    
*     
* <Name of the owner>         DateCreated       Type(Created/Modified)  Description    
  Kalaiyarasan.P              25-May-2012         Created               This SP is used to Add/Modify Staff details     
                                                                        based on Staff_ID.    
*/    
CREATE PROCEDURE usp_AddModifyStaffDetails
				@User_Group_ID INT,
				@Staff_First_Name VARCHAR(50),
				@Staff_Last_Name VARCHAR(50),
				@Staff_Title VARCHAR(5),
				@Staff_Address NVARCHAR(MAX),
				@Staff_Postcode varchar(10),
				@Staff_Phone_No varchar(15),
				@Staff_Extension_No varchar(15),
				@Staff_Mobile_No varchar(15),
				@Staff_Job_Title varchar(50),
				@Staff_Department varchar(50),           
				@Staff_IsAnEngineer BIT,
				@Staff_IsARepresentative BIT,
				@Staff_IsAStockController BIT,
				@Staff_Start_Date varchar(30),
				@Staff_End_Date varchar(30),  
				@Staff_Username varchar(50),
				@Staff_Password  varchar(50),
				@Depot_ID INT,        
				@Service_Area_ID INT,    
				@Supplier_ID INT,    
				@Staff_Personel_No varchar(10),
				@Staff_Terminated  BIT,         
				@Staff_Notes  varchar(255),       
				@Email_Address varchar(100),
				@UserTableID INT,	
				@Staff_ID INT OUTPUT
AS
BEGIN
	

IF NOT EXISTS (
        SELECT 1  
        FROM   STAFF
        WHERE  Staff_ID =@Staff_ID    
    )  
    BEGIN  	
    
		INSERT INTO Staff ( User_Group_ID, Staff_First_Name, Staff_Last_Name, 
							Staff_Title, Staff_Address, Staff_Postcode, 
							Staff_Phone_No, Staff_Extension_No, Staff_Mobile_No, 
							Staff_Job_Title, Staff_Department, Staff_IsAnEngineer, 
							Staff_IsARepresentative, Staff_IsAStockController, Staff_Start_Date, 
							Staff_End_Date, Staff_Username, Staff_Password, Depot_ID,
							Service_Area_ID, Supplier_ID, Staff_Personel_No, Staff_Terminated, 
							Staff_Notes, Email_Address, UserTableID ) 
		VALUES 
							( @User_Group_ID, @Staff_First_Name, @Staff_Last_Name, 
							  @Staff_Title, @Staff_Address, @Staff_Postcode, 
							  @Staff_Phone_No, @Staff_Extension_No, @Staff_Mobile_No, 
							  @Staff_Job_Title, @Staff_Department, @Staff_IsAnEngineer, 
							  @Staff_IsARepresentative, @Staff_IsAStockController, @Staff_Start_Date, 
							  @Staff_End_Date, @Staff_Username, @Staff_Password, @Depot_ID, 
							  @Service_Area_ID, @Supplier_ID, @Staff_Personel_No, @Staff_Terminated, 
							  @Staff_Notes, @Email_Address, @UserTableID  )		
							  SELECT @Staff_ID=SCOPE_IDENTITY()					  
	
	END
	ELSE
	BEGIN		
	
		UPDATE Staff
		SET	User_Group_ID = @User_Group_ID, Staff_First_Name = @Staff_First_Name, Staff_Last_Name = @Staff_Last_Name, 
			Staff_Title = @Staff_Title, Staff_Address = @Staff_Address, Staff_Postcode = @Staff_Postcode, 
			Staff_Phone_No = @Staff_Phone_No, Staff_Extension_No = @Staff_Extension_No, 
			Staff_Mobile_No = @Staff_Mobile_No, Staff_Job_Title = @Staff_Job_Title, 
			Staff_Department = @Staff_Department, Staff_IsAnEngineer = @Staff_IsAnEngineer, 
			Staff_IsARepresentative = @Staff_IsARepresentative, Staff_IsAStockController = @Staff_IsAStockController, 
			Staff_End_Date = @Staff_End_Date, Staff_Personel_No = @Staff_Personel_No, 
			Staff_Username = @Staff_Username, Staff_Password = @Staff_Password, 
			Depot_ID = @Depot_ID, Service_Area_ID = @Service_Area_ID, Supplier_ID = @Supplier_ID,
			Staff_Terminated = @Staff_Terminated, Staff_Notes = @Staff_Notes, 
			Email_Address = @Email_Address, UserTableID = @UserTableID
	WHERE	Staff_ID =@Staff_ID
	
	IF @Staff_Terminated = 1
	BEGIN
		INSERT INTO Export_History
		(EH_Date,EH_Reference1,EH_Type,EH_Site_Code)
		SELECT GETDATE(),UsrLnk.SecurityUserID,'Userdetails',St.Site_Code
		FROM UserSite_lnk UsrLnk INNER JOIN [Site] St ON  UsrLnk.SiteID = St.Site_ID AND UsrLnk.SecurityUserID = @UserTableID
	END
		
	END
	 
END

GO

