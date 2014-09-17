USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_CheckForDeclaredVouchers]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_CheckForDeclaredVouchers]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[rsp_CheckForDeclaredVouchers]
	@company INT = 0,
	@subcompany INT = 0,
	@site INT = 0,
	@Slot VARCHAR(50) = 'ALL',
	@startdate DATETIME,
	@enddate DATETIME
AS
BEGIN
	SET ROWCOUNT 0
	SET DATEFORMAT DMY
	
	DECLARE @SlotId INT

	
	IF (ISNULL(@company, 0) = 0)
	    SET @company = NULL
	
	IF (ISNULL(@subcompany, 0) = 0)
	    SET @subcompany = NULL
	
	IF (ISNULL(@site, 0) = 0)
	    SET @site = NULL
	
	SET @SlotId = 0
	IF(ISNULL(@Slot,'ALL') <> 'ALL')
	BEGIN
		SELECT @SlotId = Machine_Id FROM MACHINE WHERE Machine_Stock_No = @Slot
	END
	
	IF (ISNULL(@SlotId, 0) = 0)
	    SET @SlotId = NULL
	
	SELECT TOP 1 C.Collection_ID
	FROM   COLLECTION c WITH(NOLOCK)
	       INNER JOIN collection_ticket ct WITH(NOLOCK)
	            ON  CT_Inserted_Installation_ID = c.installation_id
	            AND CT_Inserted_Collection_ID = c.collection_id
	            AND CT_Inserted_Installation_ID > 0
	            AND CT_Inserted_Collection_ID > 0
	       --INNER JOIN voucher v WITH(NOLOCK)
	       --     ON  v.strBarCode = ct.CT_Barcode
	       INNER JOIN installation i WITH(NOLOCK)
	            ON  i.installation_id = c.installation_id
	            AND CT_Inserted_Installation_ID = i.installation_id
	       INNER JOIN bar_position bp WITH(NOLOCK)
	            ON  bp.Bar_Position_ID = I.Bar_Position_ID
	       INNER JOIN MACHINE m WITH(NOLOCK)
	            ON  m.machine_id = i.machine_id
	       INNER JOIN batch BI WITH(NOLOCK)
	            ON  C.batch_id = Bi.Batch_ID
	       INNER JOIN SITE S WITH(NOLOCK)
	            ON  Bi.Batch_Ref LIKE S.Site_Code + ',%'
	       INNER JOIN Sub_Company SC WITH(NOLOCK)
	            ON  SC.Sub_Company_ID = S.Sub_Company_ID
	       INNER JOIN Company Co WITH(NOLOCK)
	            ON  Co.Company_ID = SC.Company_ID
	WHERE  CT_VoucherStatus = 'PD'
	       AND m.machine_id = ISNULL(@SlotId, m.machine_id)
	       AND SC.Sub_Company_ID = ISNULL(@subcompany, SC.Sub_Company_ID)
	       AND CO.Company_ID = ISNULL(@company, CO.Company_ID)
	       AND S.Site_ID = ISNULL(@site, S.Site_ID)
	       AND (DATEADD(DD, 0, DATEDIFF(DD, 0, CAST(BATCH_DATE AS DATETIME))) BETWEEN DATEADD(DD, 0, DATEDIFF(DD, 0, @startdate)) AND DATEADD(DD, 0, DATEDIFF(DD, 0, @enddate)) )
END

GO

