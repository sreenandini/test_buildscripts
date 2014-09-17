USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetCommonShareHolders]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetCommonShareHolders]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- exec dbo.rsp_GetCommonShareHolders 0, 2, 0
CREATE PROCEDURE dbo.rsp_GetCommonShareHolders
(
    @CommonProfitShareType  SMALLINT,
    @ShareGroupId           INT,
    @ShareId                INT = 0
)
AS
BEGIN
	SET NOCOUNT ON
	-- BEGIN

	CREATE TABLE #tmpShareHolders
	(
		ShareHolderId INT NOT NULL
	)

	IF (@CommonProfitShareType = 1)
	BEGIN
	    -- Selected share
	    IF (@ShareId > 0)
	    BEGIN
	        INSERT INTO #tmpShareHolders(ShareHolderId)
	        SELECT es.ShareHolderId
	        FROM   [dbo].[ExpenseShare] es
	        WHERE  es.ExpenseShareGroupId = @ShareGroupId
	               AND es.ExpenseShareId = @ShareId
	               AND es.SysDelete = 0
	        GROUP BY
	               es.ShareHolderId
	    END

	    -- Deleted share holders for the corresponding group
	    INSERT INTO #tmpShareHolders(ShareHolderId)
	    SELECT es1.ShareHolderId
	    FROM   (
	               --GROUP BY es1.shareholderid, es2.ShareHolderId
	               SELECT shareholderid
	               FROM   ExpenseShare
	               WHERE  ExpenseShareGroupId = @ShareGroupId
	                      AND SysDelete = 1
	               GROUP BY
	                      shareholderid
	           ) es1
	           LEFT JOIN (
	                    SELECT shareholderid
	                    FROM   ExpenseShare
	                    WHERE  ExpenseShareGroupId = @ShareGroupId
	                           AND SysDelete = 0
	                    GROUP BY
	                           shareholderid
	                ) es2
	                ON  es1.shareholderid = es2.shareholderid
	    WHERE  es2.shareholderid IS NULL

	    -- Combined share holders
	    SELECT SH.[ShareHolderId] AS [Id],
	           SH.[ShareHolderName] AS [Name]
	    FROM   [dbo].[ShareHolders] SH WITH(NOLOCK)
	           LEFT JOIN [dbo].[ExpenseShare] ES WITH(NOLOCK)
	                ON  ES.ShareHolderId = SH.ShareHolderId
	                AND ES.ExpenseShareGroupId = @ShareGroupId
	    WHERE  (ES.ExpenseShareId IS NULL)
	     AND SH.SysDelete = 0
	           OR  (
	                   (
	                       SH.ShareHolderId IN (SELECT ShareHolderId
	                                            FROM   #tmpShareHolders)
	                   )
	               )

	    GROUP BY
	           SH.[ShareHolderId],
	           SH.[ShareHolderName]
	    ORDER BY
	           2 ASC
	END
	ELSE
	BEGIN
		-- Selected share
	    IF (@ShareId > 0)
	    BEGIN
	        INSERT INTO #tmpShareHolders(ShareHolderId)
	        SELECT es.ShareHolderId
	        FROM   [dbo].[ProfitShare] es
	        WHERE  es.ProfitShareGroupId = @ShareGroupId
	               AND es.ProfitShareId = @ShareId
	               AND es.SysDelete = 0
	        GROUP BY
	               es.ShareHolderId
	    END

	    -- Deleted share holders for the corresponding group
	    INSERT INTO #tmpShareHolders(ShareHolderId)
	    SELECT es1.ShareHolderId
	    FROM   (
	               --GROUP BY es1.shareholderid, es2.ShareHolderId
	               SELECT shareholderid
	               FROM   ProfitShare
	               WHERE  ProfitShareGroupId = @ShareGroupId
	                      AND SysDelete = 1
	               GROUP BY
	                      shareholderid
	           ) es1
	           LEFT JOIN (
	                    SELECT shareholderid
	                    FROM   ProfitShare
	                    WHERE  ProfitShareGroupId = @ShareGroupId
	                           AND SysDelete = 0
	                    GROUP BY
	                           shareholderid
	                ) es2
	                ON  es1.shareholderid = es2.shareholderid
	    WHERE  es2.shareholderid IS NULL

	    -- Combined share holders
	    SELECT SH.[ShareHolderId] AS [Id],
	           SH.[ShareHolderName] AS [Name]
	    FROM   [dbo].[ShareHolders] SH WITH(NOLOCK)
	           LEFT JOIN [dbo].[ProfitShare] ES WITH(NOLOCK)
	                ON  ES.ShareHolderId = SH.ShareHolderId
	                AND ES.ProfitShareGroupId = @ShareGroupId
	    WHERE  (ES.ProfitShareId IS NULL)
	     AND SH.SysDelete = 0
	           OR  (

	                   (
	                       SH.ShareHolderId IN (SELECT ShareHolderId
	                                            FROM   #tmpShareHolders)
	                   )
	               )

	    GROUP BY
	           SH.[ShareHolderId],
	           SH.[ShareHolderName]
	    ORDER BY
	           2 ASC
	END

	-- END
	SET NOCOUNT OFF
END
GO

