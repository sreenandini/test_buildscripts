USE Exchange
GO
CREATE TABLE DoorEvent_lkp
(	
	EventID INT PRIMARY KEY,
	EventDesc VARCHAR(30),
	OpenStatus BIT DEFAULT 1,
	AssociatedEvent INT
)
GO
INSERT INTO DoorEvent_lkp VALUES (1, 'Front Door', 1, 2)
INSERT INTO DoorEvent_lkp VALUES (2, 'Front Door', 0, 1)
INSERT INTO DoorEvent_lkp VALUES (3, 'Cash Door', 1, 4)
INSERT INTO DoorEvent_lkp VALUES (4, 'Cash Door', 0, 3)
INSERT INTO DoorEvent_lkp VALUES (10, 'Slot Door', 1, 11)
INSERT INTO DoorEvent_lkp VALUES (11, 'Slot Door', 0, 10)
INSERT INTO DoorEvent_lkp VALUES (12, 'Stacker Door', 1, 13)
INSERT INTO DoorEvent_lkp VALUES (13, 'Stacker Door', 0, 12)
INSERT INTO DoorEvent_lkp VALUES (14, 'Drop Door', 1, 15)
INSERT INTO DoorEvent_lkp VALUES (15, 'Drop Door', 0, 14)
INSERT INTO DoorEvent_lkp VALUES (16, 'Card Cage Door', 1, 17)
INSERT INTO DoorEvent_lkp VALUES (17, 'Card Cage Door', 0, 16)
INSERT INTO DoorEvent_lkp VALUES (18, 'Belly Door', 1, 19)
INSERT INTO DoorEvent_lkp VALUES (19, 'Belly Door', 0, 18)
INSERT INTO DoorEvent_lkp VALUES (22, 'GMU Compartment', 1, 23)
INSERT INTO DoorEvent_lkp VALUES (23, 'GMU Compartment', 0, 22)
INSERT INTO DoorEvent_lkp VALUES (35, 'Aux fill Door', 1, 36)
INSERT INTO DoorEvent_lkp VALUES (36, 'Aux fill Door', 0, 35)
INSERT INTO DoorEvent_lkp VALUES (75, 'Acceptor Door', 1, 76)
INSERT INTO DoorEvent_lkp VALUES (76, 'Acceptor Door', 0, 75)
INSERT INTO DoorEvent_lkp VALUES (167, 'MPU Compartment', 1, 168)
INSERT INTO DoorEvent_lkp VALUES (168, 'MPU Compartment', 0, 167)

GO
CREATE TABLE CodeMaster
(
	ID INT Identity(1,1),
	Code CHAR(6) PRIMARY KEY,
	Description Varchar(100)
)
GO
CREATE TABLE LookupMaster
(
	ID INT Identity(1,1),
	Code CHAR(6) REFERENCES CodeMaster(Code),
	DisplayText Varchar(100),
	Description Varchar(100),
	Constraint PKLookupMaster  PRIMARY KEY (Code, DisplayText)	
)
GO
CREATE TABLE LanguageLookup
(
	ID INT Identity(1,1),
	LookupMasterID Varchar(100),
	LanguageCode Char(5),
	ForeignText Varchar(100)
)
GO
CREATE TABLE MaintenanceSession
(
	ID INT IDENTITY(1,1) PRIMARY KEY,
	Installation_No INT REFERENCES Installation(Installation_No),
	isAuthorized BIT DEFAULT 1,
	CreatedBy INT,
	CreatedOn DATETIME DEFAULT GETDATE(),
	ClosedBy INT, 
	ClosedOn DATETIME,
	CategoryID INT,
	Reason INT ,
	Comment VARCHAR(MAX),
	IsSessionOpen BIT DEFAULT 1
)
GO
CREATE TABLE MaintenanceHistory
(
	ID INT IDENTITY(1,1) PRIMARY KEY,
	SessionID INT REFERENCES MaintenanceSession(ID),
	EventID INT REFERENCES DoorEvent_lkp(EventID),
	[TimeStamp] DateTime,
	CoinIn INT,
	CoinOut INT,
	Bill100 INT,
	Bill50 INT,
	Bill20 INT,
	Bill10 INT,
	Bill5 INT,
	Bill1 INT,
	TrueCoinIn INT,
	TrueCoinOut INT,
	[Drop] INT,
	Jackpot INT,
	CancelledCredits INT,
	HandPaidCancelledCredits INT,
	CashableTicketIn INT,
	CashableTicketOut INT,
	CashableTicketInQty INT,
	CashableTicketOutQty INT,
	ProgressiveHandPay INT	
)
GO

CREATE PROCEDURE USP_InsertMaintenanceHistory(@InstallationNo INT, @SessionID INT, @EventID INT, @MHID INT OUTPUT)
AS
BEGIN
	DECLARE @SnapID INT

	EXEC dbo.usp_CreateSnapShot @InstallationNo 

	SELECT 
		@SnapID = MH_ID 
	FROM 
		meter_history  
	WHERE 
		mh_process = 'SNAP' 
		AND Mh_Installation_no=@InstallationNo

	INSERT INTO MaintenanceHistory(
		SessionID,
		EventID,
		[TimeStamp],
		CoinIn,
		CoinOut,
		Bill100,
		Bill50,
		Bill20,
		Bill10,
		Bill5,
		Bill1,
		TrueCoinIn,
		TrueCoinOut,
		[Drop],
		Jackpot,
		CancelledCredits,
		HandPaidCancelledCredits,
		CashableTicketIn,
		CashableTicketOut,
		CashableTicketInQty,
		CashableTicketOutQty,
		ProgressiveHandPay)
	SELECT 
		@SessionID,
		@EventID,
		GETDATE(),
		MH_Coins_In,
		MH_Coins_Out,	
		MH_Bill_100,
		MH_Bill_50,
		MH_Bill_20,
		MH_Bill_10,
		MH_Bill_5,
		MH_Bill_1,
		MH_True_Coin_In,
		MH_True_Coin_Out,
		MH_Coin_Drop,
		MH_Jackpot,
		MH_Cancelled_Credits,
		MH_Handpay,
		MH_Ticket_Inserted_Value,
		MH_Ticket_Inserted_Qty,
		MH_Ticket_Printed_Value,
		MH_Ticket_Printed_Qty,
		MH_Progressive_Win_Handpay_Value
	FROM
		Meter_History
	WHERE
		MH_ID = @SnapID

	SET @MHID = @@IDENTITY 
END


GO

CREATE PROCEDURE usp_ManageMaintenance (@InstallationNo INT, @EventID INT = 0,@UserID INT = 0)
AS
BEGIN

	DECLARE @SessionID INT	
	DECLARE @MaintenanceID INT

	IF NOT EXISTS (SELECT * FROM MaintenanceSession WHERE Installation_No = @InstallationNo AND IsSessionOpen = 1)
	BEGIN

		INSERT INTO MaintenanceSession(
			Installation_No,
			isAuthorized,
			CreatedBy)
		VALUES
			@InstallationNo,
			CASE WHEN ISNULL(@UserID,0) = 0 THEN 0 ELSE 1 END,
			@UserID

		SET @SessionID = @@IDENTITY

	END
	ELSE
	BEGIN
		SELECT @SessionID = ID FROM MaintenanceSession WHERE Installation_No = @InstallationNo AND IsSessionOpen = 1
	END

	SET @MaintenanceID = 0

	IF (ISNULL(@EventID,0) <> 0 
	BEGIN
		EXEC USP_InsertMaintenanceHistory @InstallationNo, @SessionID, @EventID, @MaintenanceID OUTPUT
	END
END
GO
CREATE FUNCTION fn_CheckMachineMaintenance (@InstallationNo INT)
RETURNS INT
AS
BEGIN

	IF (SELECT Count(*) FROM MaintenanceSession tMS
				INNER JOIN MaintenanceHistory tMH ON tMH.SessionID = tMS.ID
				INNER JOIN DoorEvent_lkp tDE_Main ON tDE_Main.EventID = tMH.EventID
				LEFT OUTER JOIN DoorEvent_lkp tDE_Sub ON  tDE_Sub.EventID = tDE_Main.AssociatedEvent
				WHERE tMS.Installation_No = @InstallationNo AND tMS.IsSessionOpen = 1 AND tDE_Sub.EventID IS NULL) = 0
	BEGIN
		RETURN 1
	END
	ELSE
	BEGIN
		DECLARE @NoOfOpenDoor INT
		DECLARE @NoOfCloseDoor INT

		SELECT @NoOfOpenDoor = Count(*) FROM MaintenanceSession tMS
		INNER JOIN MaintenanceHistory tMH ON tMH.SessionID = tMS.ID
		INNER JOIN DoorEvent_lkp tDE ON tDE.EventID = tMH.EventID AND tDE.OpenStatus = 1
		WHERE tMS.Installation_No = @InstallationNo AND tMS.IsSessionOpen = 1

		SELECT @NoOfCloseDoor = Count(*) FROM MaintenanceSession tMS
		INNER JOIN MaintenanceHistory tMH ON tMH.SessionID = tMS.ID
		INNER JOIN DoorEvent_lkp tDE ON tDE.EventID = tMH.EventID AND tDE.OpenStatus = 0
		WHERE tMS.Installation_No = @InstallationNo AND tMS.IsSessionOpen = 1

		IF @NoOfOpenDoor > @NoOfCloseDoor 
		BEGIN 
			RETURN -1
		END
		ELSE
		BEGIN
			RETURN -2
		END

	END

END
GO
CREATE PROCEDURE usp_CloseMaintenance(@InstallationNo INT, @ReasonID INT, @CategoryID INT, @Comment VARCHAR(MAX), @UserID INT)
AS
BEGIN

	UPDATE 
		MaintenanceSession
	SET 
		ClosedBy = @UserID,
		ClosedOn = GETDATE(),
		CategoryID = @CategoryID,
		Reason = @ReasonID,
		Comment = @Comment,
		IsSessionOpen = 0
	WHERE 
		Installation_No  = @InstallationNo
		AND IsSessionOpen  = 1

END
GO