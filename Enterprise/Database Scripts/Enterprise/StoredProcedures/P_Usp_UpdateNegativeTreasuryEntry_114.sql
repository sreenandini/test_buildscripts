USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[Usp_UpdateNegativeTreasuryEntry_114]    Script Date: 04/10/2014 16:51:48 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Usp_UpdateNegativeTreasuryEntry_114]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Usp_UpdateNegativeTreasuryEntry_114]
GO

USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[Usp_UpdateNegativeTreasuryEntry_114]    Script Date: 04/10/2014 16:51:48 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


GO
CREATE PROCEDURE [dbo].[Usp_UpdateNegativeTreasuryEntry_114] 
(
@Treasury_ID INT = NULL
)      
     
AS      
BEGIN   
	SET NOCOUNT ON
	DECLARE @Treasury_VoidedDate DATETIME
	DECLARE @Original INT
	
	SELECT TOP 1 
		@Original = T.Treasury_ID, 
		@Treasury_ID = T1.Treasury_ID, 
		@Treasury_VoidedDate = ISNULL(T1.Treasury_VoidedDate, GETDATE()) 
	FROM 
		Treasury_Entry T, 
		Treasury_Entry T1  
	WHERE 
		T.Treasury_ID < T1.Treasury_ID 
		AND T.Treasury_Reason_Code = 0
		AND T.Collection_ID = T1.Collection_ID
		AND T.Installation_ID = T1.Installation_ID 
		AND T.Treasury_Amount = -(T1.Treasury_Amount) 
		AND T.Treasury_Type = T1.Treasury_Type
		AND T1.Treasury_ID = @Treasury_ID
	ORDER BY T.Treasury_ID DESC
	 
	 IF(@Original > 0)
	 BEGIN
		Update Treasury_Entry
		SET 
			Treasury_Reason_Code = @Original,
			Treasury_VoidedDate = @Treasury_VoidedDate
		WHERE 
			Treasury_ID IN (@Original, @Treasury_ID)
	END
	
SET NOCOUNT OFF
END
GO
