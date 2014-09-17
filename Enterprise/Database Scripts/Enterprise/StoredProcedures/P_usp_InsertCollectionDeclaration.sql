USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_InsertCollectionDeclaration]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_InsertCollectionDeclaration]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[usp_InsertCollectionDeclaration]      
      
  @Site_Id						 INT,
  @Collection_Type               VARCHAR(2),      
  @Installation_No               INT,      
  @Collection_No                 INT,      
  @User_ID                       INT,      
  @Manual                        BIT,      
  @forceCash                     BIT,       
  @forceMeters                   BIT,      
  @VATRate                       FLOAT,      
  @HandHeldsActive               BIT,      
      
  @DeclarationCoinsIn            MONEY = 0,      
  @DeclarationCoinsOut           MONEY = 0,      
  @DeclarationCoinDrop           MONEY = 0,      
  @DeclarationHandPay            MONEY = 0,      
  @DeclarationExternalCredit     MONEY = 0,      
  @DeclarationGamesBet           INT = 0,      
  @DeclarationGamesWon           INT = 0,      
  @DeclarationNotes              MONEY = 0,      
  @DeclarationCashCashOut        MONEY = 0,      
  @DeclarationCashTokensOut      MONEY = 0,      
  @DeclarationCashTokenRefills   MONEY = 0, 
  @DeclarationCoinBreakDown1p    MONEY = 0,      
  @DeclarationCoinBreakDown2p    MONEY = 0,      
  @DeclarationCoinBreakDown5p    MONEY = 0,      
  @DeclarationCoinBreakDown10p   MONEY = 0,      
  @DeclarationCoinBreakDown20p   MONEY = 0,      
  @DeclarationCoinBreakDown50p   MONEY = 0,      
  @DeclarationCoinBreakDown100p  MONEY = 0,      
  @DeclarationCoinBreakDown200p  MONEY = 0,      
  @DeclarationCoinBreakDown500p  MONEY = 0,      
  @DeclarationCoinBreakDown1000p MONEY = 0,      
  @DeclarationCoinBreakDown2000p  MONEY = 0,      
  @DeclarationCoinBreakDown5000p MONEY = 0,      
  @DeclarationCoinBreakDown10000p MONEY = 0,      
  @DeclarationCoinBreakDown20000p MONEY = 0,
  @DeclarationCoinBreakDown50000p MONEY = 0,
  @DeclarationCoinBreakDown100000p MONEY = 0,
  @DeclarationTicketValue        MONEY = 0,      
  @DeclarationTicketQty          INT = 0,      
      
  @DeclarationMetersCashIn       MONEY = 0,      
  @DeclarationMetersCashOut      MONEY = 0,      
  @DeclarationMetersTokensIn     MONEY = 0,      
  @DeclarationMetersTokensOut    MONEY = 0,      
  @DeclarationMetersPrize        MONEY = 0,      
  @DeclarationMetersJukebox      MONEY = 0,      
  @DeclarationMetersTournament   MONEY = 0,      
  @DeclarationMetersRefills      MONEY = 0,
  @IsSuccess					 INT OUTPUT
      
AS       
      
  SET DATEFORMAT DMY      
      
  -- constants      
  --      
  DECLARE @FLOAT_DEFLOATED       INT,      
          @EVENT_COLLECT         INT,      
          @SF_VALUE_PUSHER       INT,
			@Treasury_No		 INT
      
  SELECT @FLOAT_DEFLOATED = 1,      
         @EVENT_COLLECT   = 2,      
     @SF_VALUE_PUSHER = 2      
          
  DECLARE @Special_Feature       INT,      
          @TreasuryDefloatReason varchar(50),      
          @TreasuryDefloatTotal  MONEY,      
          @TreasuryDefloat2p     MONEY,      
          @TreasuryDefloat5p     MONEY,      
          @TreasuryDefloat10p    MONEY,      
          @TreasuryDefloat20p    MONEY,      
          @TreasuryDefloat50p    MONEY,      
          @TreasuryDefloat100p   MONEY,                
          @TreasuryDefloat200p   MONEY,        
          @TreasurySoFar         MONEY,      
          @TreasuryRepayments    MONEY,      
          @TreasuryRefills       MONEY,      
          @TreasuryTokens        MONEY,      
          @TreasuryHandpay       MONEY,      
          @ProgHandpay           MONEY,  -- new field
      
          @CashCollected         MONEY,      
          @CashRefills           MONEY,      
          @TokensCollected       MONEY,      
          @TokenRefills          MONEY,    
          @Cash_Collected_1p     MONEY,   
          @Cash_Collected_2p     MONEY,      
          @Cash_Collected_5p     MONEY,      
          @Cash_Collected_10p    MONEY,      
          @Cash_Collected_20p    MONEY,      
          @Cash_Collected_50p    MONEY,      
          @Cash_Collected_100p   MONEY,      
          @Cash_Collected_200p   MONEY,      
          @Cash_Collected_500p   MONEY,      
          @Cash_Collected_1000p  MONEY,      
          @Cash_Collected_2000p  MONEY,      
          @Cash_Collected_5000p  MONEY,      
          @Cash_Collected_10000p  MONEY,      
          @Cash_Collected_20000p  MONEY,      
          @Cash_Collected_50000p  MONEY,      
          @Cash_Collected_100000p  MONEY,      
                
          @TicketValue           MONEY,      
          @TicketQty             INT,      
		  @PartCashIn1p          MONEY,
          @PartCashIn2p          MONEY,      
          @PartCashIn5p          MONEY,      
          @PartCashIn10p         MONEY,      
      
          @PartCashIn20p         MONEY,      
          @PartCashIn50p         MONEY,      
          @PartCashIn100p        MONEY,      
          @PartCashIn200p        MONEY,      
          @PartCashIn500p        MONEY,      
          @PartCashIn1000p       MONEY,      
          @PartCashIn2000p       MONEY,      
          @PartCashIn5000p       MONEY,      
          @PartCashIn10000p       MONEY,      
          @PartCashIn20000p       MONEY,      
          @PartCashIn50000p       MONEY,      
          @PartCashIn100000p       MONEY,      
          @PartCashCollected     MONEY,      
          @PartCashRefills       MONEY,      
          @PartTokensCollected   MONEY,      
          @PartTokenRefills      MONEY,      
          @PartTicketValue       MONEY,      
          @PartTicketQty         INT,      
      
          @Installation_Price_Of_Play            INT,      
          @Machine_Float_Maximum_Capacity        INT,      
          @Installation_Counter_Cash_In_Units    INT,      
          @Installation_Counter_Cash_Out_Units   INT,      
          @Installation_Counter_Refill_Units     INT,      
          @Installation_Counter_Token_In_Units   INT,         
          @Installation_Counter_Token_Out_Units  INT,        
          @Installation_Counter_Jukebox_Units    INT,      
          @Installation_Counter_Prize_Units      INT,      
          @Installation_Counter_Tournament_Units INT,       
          @Installation_Percentage_Payout         FLOAT,      
      
          @Collection_Treasury_Defloat           MONEY,      
          @Batch_no INT,
      
          @PrintedTickets_Count                  INT,      
          @PrintedTickets_Value                  MONEY,      
                
          @InsertedTickets_Count                 INT,      
          @InsertedTickets_Value                 MONEY,      
      
          @ret                                   INT  -- error value      
      
  -- only works for @Collection_Type = 'DC'       
  --      

	DECLARE @Auto_Declare_Monies Varchar(20)
	EXEC [dbo].[rsp_GetSiteSetting] @Site_ID, 'Auto_Declare_Monies', @Auto_Declare_Monies OUTPUT

	DECLARE @DeclareMethod Varchar(20)   
	EXEC [dbo].[rsp_GetSiteSetting] @Site_ID, 'TicketDeclarationMethod', @DeclareMethod OUTPUT

	DECLARE @CLIENT Varchar(20)   
	EXEC [dbo].[rsp_GetSiteSetting] @Site_ID, 'CLIENT', @CLIENT OUTPUT  

 -- Update User name  
  DECLARE @UserName Varchar(50)         
     SELECT @UserName = UserName  FROM  [User] WHERE  SecurityUserID IN (SELECT UserTableID from Staff where Staff_ID= @User_ID    )

  IF @Collection_Type NOT IN ( 'DC', 'FC' )      
    RETURN (1)      
      
  BEGIN TRAN coll      
      
  -- get special features information ...      
  --      
  SELECT @Special_Feature                       = Machine_Class.Machine_Class_Sp_Features,      
         @Installation_Price_Of_Play            = Installation_Price_Per_Play,      
         @Installation_Counter_Cash_In_Units    = Installation_Counter_Cash_In_Units,      
         @Installation_Counter_Cash_Out_Units   = Installation_Counter_Cash_Out_Units,      
         @Installation_Counter_Refill_Units     = Installation_Counter_Refill_Units,      
         @Installation_Counter_Token_In_Units   = Installation_Counter_Tokens_In_Units,         
         @Installation_Counter_Token_Out_Units  = Installation_Counter_Tokens_Out_Units,        
         @Installation_Counter_Jukebox_Units = Installation_Counter_Jukebox_Units,          
         @Installation_Counter_Prize_Units      = Installation_Counter_Prize_Units,            
         @Installation_Counter_Tournament_Units = Installation_Counter_Tournament_Play_Units,      
         @Installation_Percentage_Payout         = Installation_Percentage_Payout      
      
    FROM Installation      
            
    JOIN Machine       
      ON Installation.Machine_Id = Machine.Machine_Id 
      
    JOIN Machine_Class       
      ON Machine.Machine_Class_Id = Machine_Class.Machine_Class_Id       
      
   WHERE Installation.Installation_Id = @Installation_No      
      
      
  SELECT @Collection_Treasury_Defloat = COALESCE ( Collection_Treasury_Defloat, 0 )     , @Batch_no = Batch_Id
    FROM Collection      
   WHERE Collection_Id = @Collection_No      
      
      
  IF ( @Collection_Type = 'DC' )      
  BEGIN      
      
    -- If it is a defloat collection.....      
          
    -- set installation status      
    --      
    UPDATE Installation      
       SET Installation_Float_Status = @FLOAT_DEFLOATED      
     WHERE Installation_id = @Installation_No      
            
                                
    -- We now want to find all floats that haven't been allocated (to a defloat).       
    -- They may already have been linked to a collection and exported (collection_No) but not defloated.      
    --      
    --SELECT * FROM TREASURY_ENTRY
    IF EXISTS ( SELECT 1       
                  FROM Treasury_Entry       
                 WHERE Installation_Id = @Installation_No       
                   AND Treasury_Type = 'Float'       
                   AND Treasury_Allocated = 0       
                   AND Collection_Id = @Collection_No
              )      
    BEGIN      
      -- If there was a Float recorded in the treasury table...      
      --      
      SELECT @TreasuryDefloatTotal= SUM( Treasury_Amount ),      
             @TreasuryDefloat2p   = SUM( Treasury_Breakdown_2p ),       
             @TreasuryDefloat5p   = SUM( Treasury_Breakdown_5p ),      
             @TreasuryDefloat10p  = SUM( Treasury_Breakdown_10p ),      
             @TreasuryDefloat20p  = SUM( Treasury_Breakdown_20p ),      
             @TreasuryDefloat50p  = SUM( Treasury_Breakdown_50p ),      
             @TreasuryDefloat100p = SUM( Treasury_Breakdown_100p ),      
             @TreasuryDefloat200p = SUM( Treasury_Breakdown_200p )      
            
        FROM Treasury_Entry       
            
       WHERE Installation_Id = @Installation_No       
         AND Treasury_Type = 'Float'       
         AND Collection_id = @Collection_No
         AND Treasury_Allocated = 0       
            
      -- set allocation flag      
      --      
      UPDATE Treasury_Entry       
         SET Treasury_Allocated = 1      
               
       WHERE Installation_Id = @Installation_No       
         AND Treasury_Type = 'Float'       
         AND Treasury_Allocated = 0       
         AND Collection_id = @Collection_No
            
      SET @TreasuryDefloatReason = 'Machine Defloat using Float Issued values'      
          
    END          
    ELSE      
    BEGIN      
              
      -- If not, find max float level from Machine record...      
      --      
      SELECT @TreasuryDefloat2p              = COALESCE ( Machine_Float_2p_Capacity, 0 ),       
             @TreasuryDefloat5p              = COALESCE ( Machine_Float_5p_Capacity, 0 ),      
             @TreasuryDefloat10p             = COALESCE ( Machine_Float_10p_Capacity, 0 ),      
             @TreasuryDefloat20p             = COALESCE ( Machine_Float_20p_Capacity, 0 ),      
             @TreasuryDefloat50p             = COALESCE ( Machine_Float_50p_Capacity, 0 ),      
             @TreasuryDefloat100p            = @Collection_Treasury_Defloat + COALESCE ( Machine_Float_100p_Capacity, 0 ),      
             @TreasuryDefloat200p            = COALESCE ( Machine_Float_200p_Capacity, 0 ),      
             @Machine_Float_Maximum_Capacity = COALESCE ( Machine_Float_Maximum_Capacity, 0 )      
                   
        FROM Installation      
              
        JOIN Machine       
          ON Installation.Machine_Id = Machine.Machine_Id       
                
       WHERE Installation_Id = @Installation_No      
      
        -- set reason      
        --              
        SET @TreasuryDefloatReason = 'Machine Defloat using Machine default float values'      
                      
    END      
                    
    -- Create treasury record ..      
    --      
    DECLARE @Amount MONEY      
      
    SET @Amount = 0-@TreasuryDefloatTotal --AW 20050804 -@Collection_Treasury_Defloat          
           -- convert defloat to a minus figure       
        
    EXEC usp_InsertTreasury   @Installation_No              = @Installation_No,      
                              @Collection_No                = @Collection_No,      
                              @User_ID                      = @User_ID,      
                              @Treasury_Type                = 'defloat',      
                              @Treasury_Reason              = @TreasuryDefloatReason,      
--                              @Treasury_Breakdown_2p        = @TreasuryDefloat2p,      
--                              @Treasury_Breakdown_5p        = @TreasuryDefloat5p,      
--                              @Treasury_Breakdown_10p       = @TreasuryDefloat10p,      
--                              @Treasury_Breakdown_20p       = @TreasuryDefloat20p,      
--                              @Treasury_Breakdown_50p       = @TreasuryDefloat50p,      
--                              @Treasury_Breakdown_100p      = @TreasuryDefloat100p,      
--                              @Treasury_Breakdown_200p      = @TreasuryDefloat200p,      
                              @Treasury_Amount              = @Amount,      
                              @Treasury_Allocated           = 1,
							  @SiteId						= @Site_Id,
								@TreasuryNo = @Treasury_No OUTPUT
      
  END      
      
  -- Find all Part Collections, then allocate them to this collection      
  --      
--  SELECT @PartCashCollected   = SUM ( Part_Collection_CashCollected ),      
--         @PartCashRefills     = SUM ( Part_Collection_CashRefills ),      
--         @PartTokensCollected = SUM ( Part_Collection_TokensCollected ),      
--         @PartTokenRefills    = SUM ( Part_Collection_TokenRefills ),      
--               
--         @PartCashIn2p        = SUM ( Part_Collection_Cash_Collected_2p ),      
--         @PartCashIn5p        = SUM ( Part_Collection_Cash_Collected_5p ),      
--         @PartCashIn10p       = SUM ( Part_Collection_Cash_Collected_10p ),      
--         @PartCashIn20p       = SUM ( Part_Collection_Cash_Collected_20p ),      
--         @PartCashIn50p       = SUM ( Part_Collection_Cash_Collected_50p ),      
--         @PartCashIn100p      = SUM ( Part_Collection_Cash_Collected_100p ),      
--         @PartCashIn200p      = SUM ( Part_Collection_Cash_Collected_200p ),      
--         @PartCashIn500p      = SUM ( Part_Collection_Cash_Collected_500p ),      
--         @PartCashIn1000p     = SUM ( Part_Collection_Cash_Collected_1000p ),      
--         @PartCashIn2000p     = SUM ( Part_Collection_Cash_Collected_2000p ),      
--         @PartCashIn5000p     = SUM ( Part_Collection_Cash_Collected_5000p ),      
--         @PartCashIn10000p    = SUM ( Part_Collection_Cash_Collected_10000p ),      
--         @PartCashIn20000p    = SUM ( Part_Collection_Cash_Collected_20000p ),      
--         @PartCashIn50000p    = SUM ( Part_Collection_Cash_Collected_50000p ),      
--         @PartCashIn100000p   = SUM ( Part_Collection_Cash_Collected_100000p ),      
--         @PartTicketValue     = SUM ( Part_Collection_ValueTickets ),      
--         @PartTicketQty       = SUM ( Part_Collection_QtyTickets )      
--      
--    FROM Part_Collection       
--          
--   WHERE Collection_id = 0       
--      
--     AND Installation_id = @Installation_No      
--      
--      
--  -- Allocate part collections to main collection      
--  --      
--  UPDATE Part_Collection      
--     SET Collection_Id = @Collection_No      
--      
--   WHERE Collection_Id = 0       
--      
--     AND Installation_Id = @Installation_No      
                  
                      
 -- Now Find all treasury entries and allocate them to this collection...      
  --      
      
  -- if floats, Allocate to the collection_No but DO NOT SET ALLOCATED=TRUE!      
  --      
  UPDATE Treasury_Entry       
      
     SET Collection_Id = @Collection_No      
      
   WHERE Installation_Id = @Installation_No      
     AND Collection_Id = 0      
     AND Treasury_Type = 'float'      
          
  -- get treasury values      
  --      
  SELECT Treasury_Type, Treasury_Amount        
    INTO #tmpTreasury       
    FROM Treasury_Entry       
   WHERE Installation_Id = @Installation_No      
     AND Collection_Id = @Collection_No -- 0
             
  SELECT @TreasurySoFar = SUM ( Treasury_Amount )      
    FROM #tmpTreasury      
             
  SELECT @TreasuryRepayments = SUM ( Treasury_Amount )      
    FROM #tmpTreasury        
   WHERE Treasury_Type = 'refund'      
           
  SELECT @TreasuryRefills = SUM ( Treasury_Amount )      
    FROM #tmpTreasury        
   WHERE Treasury_Type = 'refill'      
      
  SELECT @TreasuryTokens  = SUM ( Treasury_Amount )      
    FROM #tmpTreasury        
   WHERE Treasury_Type = 'prize'      
      
  SELECT @TreasuryHandpay = SUM ( Treasury_Amount ) 
    FROM #tmpTreasury 
   WHERE Treasury_Type IN ( 'handpay credit', 'handpay jackpot', 'mystery jackpot', 'Attendantpay Credit','Attendantpay Jackpot' , 'PROGRESSIVE', 'PROG')      

-- progressives.

  SELECT @ProgHandpay = SUM ( Treasury_Amount ) 
   FROM #tmpTreasury 
  WHERE Treasury_Type IN ( 'PROG' ,  'PROGRESSIVE')      
      
/**
  -- All others allocate to the collection_No and set allocation flag      
  --      
  UPDATE Treasury       
      
     SET Collection_No = @Collection_No,      
         Treasury_Allocated = 1      
      
   WHERE Installation_No = @Installation_No      
     AND Collection_No = 0      
**/
      
      
  -- Get calendar collection periods      
  --      
 DECLARE @Site_Week_ID       INT,      
         @Site_Period_ID     INT,      
         @Operator_Week_ID   INT,      
         @Operator_Period_ID INT,      
         @Collection_Date    DATETIME,      
         @Operator_No        INT      
      
   -- get date of collection and operator at the time      
   SELECT @Collection_Date = CAST(Collection_Date + ' ' + Collection_Time + '.000' AS DATETIME),
          @Operator_No = Operator.Operator_Id        
          
     FROM Collection 
     JOIN Installation       
       ON Collection.Installation_Id = Installation.Installation_Id      
      
LEFT JOIN Machine       
       ON Installation.Machine_Id = Machine.Machine_Id      
      
LEFt JOIN Operator       
       ON Machine.Operator_Id = Operator.Operator_Id       
          
    WHERE Collection.Collection_id = @Collection_No      
       
  -- get the period information      
  EXEC rsp_GetCalendarPeriods 
							  @Site_Id		  = @Site_Id,
							  @Date               = @Collection_Date,      
                              @Operator_No        = @Operator_No,      
                              @Site_Week_ID       = @Site_Week_ID OUTPUT,      
                              @Site_Period_ID     = @Site_Period_ID OUTPUT,      
                              @Operator_Week_ID   = @Operator_Week_ID OUTPUT,      
                              @Operator_Period_ID = @Operator_Period_ID OUTPUT      
      
      
  -- assign events to collections ..      
  -- power        
  --      
  EXEC usp_AssignPowerEventsToCollection @Collection_No   = @Collection_No,      
                                         @Installation_No = @Installation_No      
                                               
  -- door      
  --      
  EXEC usp_AssignDoorEventsToCollection @Collection_No   = @Collection_No,      
                                        @Installation_No = @Installation_No      
      
  -- fault      
  --      
  EXEC usp_AssignFaultEventsToCollection @Collection_No   = @Collection_No,      
                                         @Installation_No = @Installation_No      
      
      
           
              
  -- calculate cash collected ..      
  --      
  If ( @forceCash = 1 OR ( @forceCash = 0 AND @Manual = 0 ) )       
  BEGIN         
      print 'forced cash declaration'      
        
      -- The collection record already contains the Cash Counter for the full col but it needs the Part Collections adding to it      
   --      
      SELECT @Cashcollected        = Cashcollected        + COALESCE ( @PartCashCollected, 0 ),      
             @CashRefills          = CashRefills          + COALESCE ( @PartCashRefills, 0 ),      
             @TokensCollected      = TokensCollected      + COALESCE ( @PartTokensCollected, 0 ),      
             @TokenRefills         = TokenRefills         + COALESCE ( @PartTokenRefills, 0 ),  
             @Cash_Collected_1p    = Cash_Collected_1p    + COALESCE ( @PartCashIn1p, 0 ),       
             @Cash_Collected_2p    = Cash_Collected_2p    + COALESCE ( @PartCashIn2p, 0 ),      
             @Cash_Collected_5p    = Cash_Collected_5p    + COALESCE ( @PartCashIn5p, 0 ),      
             @Cash_Collected_10p   = Cash_Collected_10p   + COALESCE ( @PartCashIn10p, 0 ),      
             @Cash_Collected_20p   = Cash_Collected_20p   + COALESCE ( @PartCashIn20p, 0 ),      
             @Cash_Collected_50p   = Cash_Collected_50p   + COALESCE ( @PartCashIn50p, 0 ),      
             @Cash_Collected_100p  = Cash_Collected_100p  + COALESCE ( @PartCashIn100p, 0 ),      
             @Cash_Collected_200p  = Cash_Collected_200p  + COALESCE ( @PartCashIn200p, 0 ),      
             @Cash_Collected_500p  = Cash_Collected_500p  + COALESCE ( @PartCashIn500p, 0 ),      
             @Cash_Collected_1000p = Cash_Collected_1000p + COALESCE ( @PartCashIn1000p, 0 ),      
             @Cash_Collected_2000p = Cash_Collected_2000p + COALESCE ( @PartCashIn2000p, 0 ),      
             @Cash_Collected_5000p = Cash_Collected_5000p + COALESCE ( @PartCashIn5000p, 0 ),      
             @Cash_Collected_10000p = Cash_Collected_10000p + COALESCE ( @PartCashIn10000p, 0 ),      
             @Cash_Collected_20000p = Cash_Collected_20000p + COALESCE ( @PartCashIn20000p, 0 ),      
             @Cash_Collected_50000p = Cash_Collected_50000p + COALESCE ( @PartCashIn50000p, 0 ),      
             @Cash_Collected_100000p = Cash_Collected_100000p + COALESCE ( @PartCashIn100000p, 0 ),      

             @TicketValue          = DeclaredTicketValue  + COALESCE ( @PartTicketValue, 0 ),      
             @TicketQty            = DeclaredTicketQty    + COALESCE ( @PartTicketQty, 0 )      
      
        FROM Collection       
       WHERE Collection_Id = @Collection_No      
      
  END          
  ELSE      
  BEGIN      
    IF ( @Manual = 1 )      
    BEGIN          
      print 'manual cash declaration'      
      
      -- only manual entry, no cash counter      
      --      
      SELECT @Cashcollected        = @DeclarationCashCashOut        + COALESCE ( @PartCashCollected, 0 ),      
             @CashRefills          = CashRefills                    + COALESCE ( @PartCashRefills, 0 ),      
             @TokensCollected      = @DeclarationCashTokensOut      + COALESCE ( @PartTokensCollected, 0 ),      
             @TokenRefills         = @DeclarationCashTokenRefills   + COALESCE ( @PartTokenRefills, 0 ),    
             @Cash_Collected_1p    = @DeclarationCoinBreakDown1p    + COALESCE ( @PartCashIn1p, 0 ),  
             @Cash_Collected_2p    = @DeclarationCoinBreakDown2p    + COALESCE ( @PartCashIn2p, 0 ),      
             @Cash_Collected_5p    = @DeclarationCoinBreakDown5p    + COALESCE ( @PartCashIn5p, 0 ),      
             @Cash_Collected_10p   = @DeclarationCoinBreakDown10p   + COALESCE ( @PartCashIn10p, 0 ),      
             @Cash_Collected_20p   = @DeclarationCoinBreakDown20p   + COALESCE ( @PartCashIn20p, 0 ),      
             @Cash_Collected_50p   = @DeclarationCoinBreakDown50p   + COALESCE ( @PartCashIn50p, 0 ),      
             @Cash_Collected_100p  = @DeclarationCoinBreakDown100p  + COALESCE ( @PartCashIn100p, 0 ),      
             @Cash_Collected_200p  = @DeclarationCoinBreakDown200p  + COALESCE ( @PartCashIn200p, 0 ),      
             @Cash_Collected_500p  = @DeclarationCoinBreakDown500p  + COALESCE ( @PartCashIn500p, 0 ),      
             @Cash_Collected_1000p = @DeclarationCoinBreakDown1000p + COALESCE ( @PartCashIn1000p, 0 ),      
             @Cash_Collected_2000p = @DeclarationCoinBreakDown2000p + COALESCE ( @PartCashIn2000p, 0 ),      
             @Cash_Collected_5000p = @DeclarationCoinBreakDown5000p + COALESCE ( @PartCashIn5000p, 0 ),      
             @Cash_Collected_10000p = @DeclarationCoinBreakDown10000p + COALESCE ( @PartCashIn10000p, 0 ),      
             @Cash_Collected_20000p = @DeclarationCoinBreakDown20000p + COALESCE ( @PartCashIn20000p, 0 ),      
             @Cash_Collected_50000p = @DeclarationCoinBreakDown50000p + COALESCE ( @PartCashIn50000p, 0 ),      
             @Cash_Collected_100000p = @DeclarationCoinBreakDown100000p + COALESCE ( @PartCashIn100000p, 0 ),      
             @TicketValue          = @DeclarationTicketValue        + COALESCE ( @PartTicketValue, 0 ),      
             @TicketQty            = @DeclarationTicketQty          + COALESCE ( @PartTicketQty, 0 )      
      
        FROM Collection       
       WHERE Collection_Id = @Collection_No      
    END       
  END      
      
  -- Calculate Net Values      
  --      
  DECLARE @tmpCashCollected As MONEY,      
          @tmpRDCRefills    As MONEY,      
          @tmpMeterRefills  As MONEY,      
          @TmpRefills       As MONEY,      
          @TmpRefunds       As MONEY,      
          @TmpFloatRec      As MONEY,      
          @TmpNetCash       As MONEY      
      
  SELECT @TmpCashCollected = @CashCollected,      
         @TmpFloatRec      = @Collection_Treasury_Defloat,    -- float recovered      
         @TmpRefills       = @CashRefills + @TreasuryRefills,  -- refills      
         @TmpRefunds       = @TreasuryRepayments,             -- refunds      
                
         -- RDC refills      
         @TmpRDCRefills = ( CASH_REFILL_5P / 20 ) +      
                          ( CASH_REFILL_10P  / 10) +      
                          ( CASH_REFILL_20P / 5) +      
                          ( CASH_REFILL_50P / 2) +      
						  ( CASH_REFILL_100P * 1 ) +
                          ( CASH_REFILL_200P * 2 ) +      
                          ( CASH_REFILL_500P * 5 ) +      
                          ( CASH_REFILL_1000P * 10 ) +      
                          ( CASH_REFILL_2000P * 20 ) +      
                          ( CASH_REFILL_5000P * 50 ) +      
                          ( CASH_REFILL_10000P * 100 ) +      
                          ( CASH_REFILL_20000P * 200 ) +      
                          ( CASH_REFILL_50000P * 500 ) +      
                          ( CASH_REFILL_100000P * 1000) ,
                
         @TmpMeterRefills = ( CounterRefill - PreviousCounterRefills ) * ( @Installation_Counter_Refill_Units / 100 )      
         
    FROM Collection                  
         
   WHERE Collection_Id = @Collection_No      
         
   -- Net cash      
   --                      
   If ( @TmpRDCRefills > 0 And @TmpMeterRefills = 0 )      
    SET @TmpNetCash = ( @TmpCashCollected - ( @TmpFloatRec + @TmpRefills + @TmpRefunds ) ) - @TmpRDCRefills      
                      
   ELSE      
    IF ( @TmpRDCRefills = 0 And @TmpMeterRefills > 0 )      
      SET @TmpNetCash = ( @TmpCashCollected - ( @TmpFloatRec + @TmpRefills + @TmpRefunds ) ) - @TmpMeterRefills      
                          
    Else      
      SET @TmpNetCash = ( @TmpCashCollected - ( @TmpFloatRec + @TmpRefills + @TmpRefunds ) )      
      
      
   -- get ticket totals for printed tickets      
   SELECT @PrintedTickets_Value = SUM(CT_Value),       
          @PrintedTickets_Count = Count(*)      
      
     FROM Collection_Ticket      
      
    WHERE CT_Printed_Collection_ID = @Collection_No      
      AND CT_Printed_Installation_ID = @Installation_No 

    
   -- get ticket totals for inserted tickets      
   SELECT @InsertedTickets_Value = SUM(CT_Value),       
          @InsertedTickets_Count = Count(*)      
      
     FROM Collection_Ticket      
      
    WHERE CT_Inserted_Collection_ID = @Collection_No      
      AND CT_Inserted_Installation_ID = @Installation_No       
      
  -- set collection fields      
  --                                  
  UPDATE Collection       
        
     SET Declaration = 1,      
      
         Week_ID            = @Site_Week_ID,      
         Period_ID          = @Site_Period_ID,       
         Operator_Week_ID   = @Operator_Week_ID,      
         Operator_Period_ID = @Operator_Period_ID,      
      
         -- update door duration/count figures..      
         CollectionNoDoorEvents = CollectionNoDoorEvents + ( SELECT COUNT( * )       
                                                               FROM Door_Event       
                                                              WHERE Collection_Id = @Collection_No       
                                                       ),      
                                                                   
         CollectionTotalDurationDoor   = CollectionTotalDurationDoor + ( SELECT SUM( Door_Event_Duration )       
                                                                         FROM Door_Event       
                                                                        WHERE Collection_Id = @Collection_No       
                                                                     ),      
      
         -- update power duration/count figures..      
         CollectionNoPowerEvents = CollectionNoPowerEvents + ( SELECT COUNT( * )       
                                                                 FROM Power_Event       
                                                                WHERE Collection_Id = @Collection_No       
                                                             ),      
                                                                   
         CollectionTotalDurationPower = CollectionTotalDurationPower + ( SELECT SUM( Power_Event_Duration )       
                                                                           FROM Power_Event       
                                                                          WHERE Collection_Id = @Collection_No       
                                                                       ),      
      
      -- update fault duration/count figures..      
         CollectionNoFaultEvents = CollectionNoFaultEvents + ( SELECT COUNT( * )       
                                                                  FROM Fault_Event       
                                                                 WHERE Collection_Id = @Collection_No       
                                                              ),      
      
/**                                                                   
        -- not available ..      
         CollectionTotalDurationFault = CollectionTotalDurationFault + ( SELECT SUM ( Duration )       
                                                                           FROM Fault_Event       
                                                                          WHERE Collection_Id = @Collection_No       
                                                                       ),      
**/      
      
         -- set treasury values      
         Treasury_Total      = COALESCE ( @TreasurySoFar, 0 ),      
         Treasury_Refills    = COALESCE ( @TreasuryRefills, 0 ),      
         Treasury_Repayments = COALESCE ( @TreasuryRepayments, 0 ),      
         Treasury_Tokens     = COALESCE ( @TreasuryTokens, 0 ),      
         Collection_Treasury_Handpay    =CASE WHEN @CLIENT='SGVI' AND @DeclareMethod='AUTO' THEN 
								CAST(
									ISNULL(CAST(Collection.COLLECTION_RDC_JACKPOT AS FLOAT)* @Installation_Price_Of_Play/100, 0)
									+ISNULL(CAST(Collection.COLLECTION_RDC_HANDPAY AS FLOAT)* @Installation_Price_Of_Play/100, 0)
									AS MONEY) 
								ELSE
								COALESCE ( @TreasuryHandpay, 0 )
								END,      
      
         --AW 20050804 Collection_Treasury_Defloat = COALESCE ( Collection_Treasury_Defloat + @Machine_Float_Maximum_Capacity, 0 ),      
         Collection_Treasury_Defloat = COALESCE ( @TreasuryDefloatTotal, 0 ),   --@TreasuryDefloatTotal is the float allocated, retrieved from the treasury table earlier      
      
         -- Cashcollected        = COALESCE ( @Cashcollected, 0 ), removed      
      
         -- calculate using known information      
         --      
         Cashcollected       = COALESCE ( @Cash_Collected_1p, 0 ) / 100 +       
						       COALESCE ( @Cash_Collected_2p, 0 ) / 100 +       
                               COALESCE ( @Cash_Collected_5p, 0 ) / 100 +      
                               COALESCE ( @Cash_Collected_10p, 0 ) / 100 +      
                               COALESCE ( @Cash_Collected_20p, 0 ) / 100 +      
                               COALESCE ( @Cash_Collected_50p, 0 ) / 100 +      
                               COALESCE ( @Cash_Collected_100p, 0 ) / 100 +      
                               COALESCE ( @Cash_Collected_200p, 0 ) / 100 +      
                               COALESCE ( @Cash_Collected_500p, 0 ) / 100 +      
                               COALESCE ( @Cash_Collected_1000p, 0 ) / 100 +      
                               COALESCE ( @Cash_Collected_2000p, 0 ) / 100 +      
                               COALESCE ( @Cash_Collected_5000p, 0 ) / 100 +       
          COALESCE ( @Cash_Collected_10000p, 0 ) / 100 +       
COALESCE ( @Cash_Collected_20000p, 0 ) / 100 +       
COALESCE ( @Cash_Collected_50000p, 0 ) / 100 +       
COALESCE ( @Cash_Collected_100000p, 0 ) / 100 +       
                               CASE WHEN @CLIENT='SGVI' AND @DeclareMethod='AUTO' THEN 
									CAST(ISNULL(CAST(Collection.COLLECTION_RDC_TICKETS_INSERTED_VALUE AS FLOAT)/100, 0)AS MONEY)
									+CAST(ISNULL(CAST(Collection.RDC_TICKETS_INSERTED_NONCASHABLE_VALUE AS FLOAT)/100, 0)AS MONEY)
											ELSE
											COALESCE ( @InsertedTickets_Value, 0)
											END,      
      
         CashRefills          = COALESCE ( @CashRefills, 0 ),      
         TokensCollected      = COALESCE ( @TokensCollected, 0 ),      
         TokenRefills         = COALESCE ( @TokenRefills, 0 ), 
         Cash_Collected_1p    = COALESCE ( @Cash_Collected_1p, 0 ),      
         Cash_Collected_2p    = COALESCE ( @Cash_Collected_2p, 0 ),      
         Cash_Collected_5p    = COALESCE ( @Cash_Collected_5p, 0 ),      
         Cash_Collected_10p   = COALESCE ( @Cash_Collected_10p, 0 ),      
         Cash_Collected_20p   = COALESCE ( @Cash_Collected_20p, 0 ),      
         Cash_Collected_50p   = COALESCE ( @Cash_Collected_50p, 0 ),      
         Cash_Collected_100p  = COALESCE ( @Cash_Collected_100p, 0 ),      
         Cash_Collected_200p  = COALESCE ( @Cash_Collected_200p, 0 ),      
         Cash_Collected_500p  = COALESCE ( @Cash_Collected_500p, 0 ),      
         Cash_Collected_1000p = COALESCE ( @Cash_Collected_1000p, 0 ),      
         Cash_Collected_2000p = COALESCE ( @Cash_Collected_2000p, 0 ),      
         Cash_Collected_5000p = COALESCE ( @Cash_Collected_5000p, 0 ),      
         Cash_Collected_10000p = COALESCE ( @Cash_Collected_10000p, 0 ),      
         Cash_Collected_20000p = COALESCE ( @Cash_Collected_20000p, 0 ),      
         Cash_Collected_50000p = COALESCE ( @Cash_Collected_50000p, 0 ),      
         Cash_Collected_100000p = COALESCE ( @Cash_Collected_100000p, 0 ),      
        
         Collection_PoP_Actual = COALESCE ( @Installation_Price_Of_Play, 0 ),      
         Collection_PoP_Configured = COALESCE ( @Installation_Price_Of_Play, 0 ),      
      
         Collection_NetEx = @TmpNetCash / @VATRate,       
         Collection_VAT_Rate = @VATRate,      
            
         Collection_Meters_CoinsIn = CASE WHEN ( Collection_Meters_CoinsIn = 0 ) THEN      
                                             @DeclarationCoinsIn      
                                           END,      
      
         Collection_Meters_CoinsOut = CASE WHEN ( Collection_Meters_CoinsOut = 0 ) THEN      
                                              @DeclarationCoinsOut      
                                            END,      
      
         Collection_Meters_CoinsDrop = CASE WHEN ( Collection_Meters_CoinsDrop = 0 ) THEN      
                                              @DeclarationCoinDrop      
                                            END,      
      
         Collection_Meters_Handpay = CASE WHEN ( Collection_Meters_Handpay = 0 ) THEN      
                                            @DeclarationHandPay      
                                          END,      
      
         Collection_Meters_ExternalCredit = CASE WHEN ( Collection_Meters_ExternalCredit = 0 ) THEN      
                                                    @DeclarationExternalCredit      
                                                  END,      
      
         Collection_Meters_GamesBet = CASE WHEN ( Collection_Meters_GamesBet = 0 ) THEN      
                                              @DeclarationGamesBet      
                                            END,      
      
         Collection_Meters_GamesWon = CASE WHEN ( Collection_Meters_GamesWon = 0 ) THEN      
                                              @DeclarationGamesWon      
                                            END,      
      
         Collection_Meters_Notes = CASE WHEN ( Collection_Meters_Notes = 0 ) THEN      
                                          @DeclarationNotes      
                                        END,      
       
         CounterCashIn = CASE WHEN ( @HandHeldsActive = 1 AND CounterCashIn = 0 ) THEN      
                                @DeclarationMetersCashIn       
                              WHEN ( @HandHeldsActive = 0 AND @Installation_Counter_Cash_In_Units = 0 ) THEN      
                                0      
                              END,      
                                     
         PreviousCounterCashIn = CASE WHEN ( @HandHeldsActive = 0 AND @Installation_Counter_Cash_In_Units = 0 ) THEN      
                                        0      
                                      END,      
--      
         CounterCashOut = CASE WHEN ( @HandHeldsActive = 1 ) THEN       
                                 CASE WHEN ( @Special_Feature = @SF_VALUE_PUSHER ) THEN      
                                        PreviousCounterCashOut + ( ( ( @DeclarationMetersCashIn - PreviousCounterCashIn ) / 100 ) * @Installation_Percentage_Payout )      
                                      ELSE       
                                        CASE WHEN ( CounterCashOut = 0 ) THEN      
                                               @DeclarationMetersCashOut      
                                             END      
                                      END                                                                                      
                               ELSE      
                                 CASE WHEN ( @Special_Feature <> @SF_VALUE_PUSHER ) THEN      
                                        CASE WHEN ( @Installation_Counter_Cash_Out_Units = 0 ) THEN      
                                               0      
                                             END      
                                      ELSE      
                                        PreviousCounterCashOut + ( ( ( CounterCashIn - PreviousCounterCashIn ) / 100 ) * @Installation_Percentage_Payout )      
                                      END      
                               END,      
                                     
         PreviousCounterCashOut = CASE WHEN ( @HandHeldsActive = 0 ) THEN       
                                         CASE WHEN ( @Installation_Counter_Cash_Out_Units = 0 ) THEN       
                                                CASE WHEN ( @Special_Feature = @SF_VALUE_PUSHER ) THEN      
                                                       0      
                                                     END      
                                              END      
                                      END,      
--      
         CounterTokensIn = CASE WHEN ( @HandHeldsActive = 1 AND CounterTokensIn = 0 ) THEN      
                                  @DeclarationMetersTokensIn      
                                WHEN ( @HandHeldsActive = 0 AND @Installation_Counter_Token_In_Units = 0 ) THEN      
                                  0      
                                END,      
      
         PreviousCounterTokensIn = CASE WHEN ( @HandHeldsActive = 0 AND @Installation_Counter_Token_In_Units = 0 ) THEN      
                                          0      
                                        END,      
--      
         CounterTokensOut = CASE WHEN ( @HandHeldsActive = 1 AND CounterCashOut = 0 ) THEN      
                                   @DeclarationMetersTokensOut      
                                 WHEN ( @HandHeldsActive = 0 AND @Installation_Counter_Token_Out_Units = 0 ) THEN      
                                   0      
                                 END,      
      
         PreviousCounterTokensOut = CASE WHEN ( @HandHeldsActive = 0 AND @Installation_Counter_Token_Out_Units = 0 ) THEN      
                                           0      
                                         END,      
--                          
         CounterPrize = CASE WHEN ( @HandHeldsActive = 1 AND CounterPrize = 0 ) THEN      
                               @DeclarationMetersPrize      
                             WHEN ( @HandHeldsActive = 0 AND @Installation_Counter_Prize_Units = 0 ) THEN      
                               0      
                             END,      
      
         PreviousCounterPrize = CASE WHEN ( @HandHeldsActive = 0 AND @Installation_Counter_Prize_Units = 0 ) THEN      
                                       0      
                                     END,      
--      
         CounterJukeBoxPlay = CASE WHEN ( @HandHeldsActive = 1 AND CounterJukeBoxPlay = 0 ) THEN      
                   @DeclarationMetersJukebox      
                               WHEN ( @HandHeldsActive = 0 AND @Installation_Counter_Jukebox_Units = 0 ) THEN      
                                 0      
                               END,      
      
         PreviousCounterJukebox = CASE WHEN ( @HandHeldsActive = 0 AND @Installation_Counter_Jukebox_Units = 0 ) THEN      
                      0      
                                       END,      
--      
         CounterTournamentPlay = CASE WHEN ( @HandHeldsActive = 1 AND CounterTournamentPlay = 0 ) THEN      
                                    @DeclarationMetersTournament      
                                  WHEN ( @HandHeldsActive = 0 AND @Installation_Counter_Tournament_Units = 0 ) THEN      
                                    0      
                                  END,      
      
         PreviousCounterTournament = CASE WHEN ( @HandHeldsActive = 0 AND @Installation_Counter_Tournament_Units = 0 ) THEN      
                                            0      
                                          END,      
--      
         CounterRefill = CASE WHEN ( @HandHeldsActive = 1 AND JackpotsOut = 0 ) THEN      
                                 @DeclarationMetersRefills      
                               WHEN ( @HandHeldsActive = 0 AND @Installation_Counter_Refill_Units = 0 ) THEN      
                                 0      
                               END,      
      
         PreviousCounterRefills = CASE WHEN ( @HandHeldsActive = 0 AND @Installation_Counter_Refill_Units = 0 ) THEN      
                                         0      
                                       END,      
      
         -- ticket totals      
         --      
         DeclaredTicketValue        = CASE WHEN @CLIENT='SGVI' AND @DeclareMethod='AUTO' THEN 
												CAST(ISNULL(CAST(Collection.COLLECTION_RDC_TICKETS_INSERTED_VALUE AS FLOAT)/100, 0)AS MONEY)
												+CAST(ISNULL(CAST(Collection.RDC_TICKETS_INSERTED_NONCASHABLE_VALUE AS FLOAT)/100, 0)AS MONEY)

											ELSE
											COALESCE ( @InsertedTickets_Value, 0)
											END,      

         DeclaredTicketQty          = COALESCE ( @InsertedTickets_Count, 0),      
               
         DeclaredTicketPrintedValue =CASE WHEN @CLIENT='SGVI' AND @DeclareMethod='AUTO' THEN 
												CAST((ISNULL(CAST(Collection.COLLECTION_RDC_TICKETS_PRINTED_VALUE AS FLOAT)/100, 0)) AS MONEY)
												+CAST(ISNULL(CAST(Collection.RDC_TICKETS_PRINTED_NONCASHABLE_VALUE AS FLOAT)/100, 0)AS MONEY)
										  ELSE 
									 COALESCE ( @PrintedTickets_Value, 0)
										  END,                      
         DeclaredTicketPrintedQty   = COALESCE ( @PrintedTickets_Count, 0), 

         Progressive_Value_Declared = CASE WHEN @CLIENT='SGVI' AND @DeclareMethod='AUTO' THEN 
									CAST(
										ISNULL(CAST(Collection.progressive_win_handpay_value AS FLOAT)* @Installation_Price_Of_Play/100, 0)
									AS MONEY) 
									ELSE
										COALESCE ( @ProgHandpay, 0 ) 
									END,
      [User_Name]=@UserName
      
   WHERE Collection_Id = @Collection_No      
   
   IF NOT EXISTS (SELECT 1 FROM [Collection] C WHERE C.Batch_ID = @Batch_no AND C.Declaration = 0)
   BEGIN
		UPDATE Batch
		SET
			Batch_Declared = 1
		WHERE Batch_ID = @Batch_no
   END

   exec esp_Calculate_Batch_Negative_Net @Batch_no
      
  SET @ret = @@ERROR      
      
  IF @ret <> 0
  BEGIN
    ROLLBACK TRAN coll
	SET @IsSuccess = -1
  END
  ELSE
  BEGIN
    COMMIT TRAN coll
	SET @IsSuccess = 0
  END

GO

