USE [Enterprise]
GO

IF EXISTS (
		SELECT *
		FROM sys.objects
		WHERE OBJECT_ID = OBJECT_ID(N'[dbo].[usp_UpdateBarPosRentTerms]')
			AND TYPE IN (
				N'P'
				,N'PC'
				)
		)
	DROP PROCEDURE [dbo].[usp_UpdateBarPosRentTerms]
GO

/****** Object:  StoredProcedure [dbo].[usp_UpdateBarPosRentTerms]    Script Date: 06/30/2014 20:13:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_UpdateBarPosRentTerms] @BarPosId INT
	,@BarPosRent INT
	,@BarPosSupplierShare INT
	,@BarPosSiteShare INT
	,@BarPosOwnersShare INT
	,@BarPosLicenceCharge INT
AS
BEGIN
	UPDATE Bar_Position
	SET Bar_Position_Rent = @BarPosRent
		,Bar_Position_Supplier_Share = @BarPosSupplierShare
		,Bar_Position_Site_Share = @BarPosSiteShare
		,Bar_Position_Owners_Share = @BarPosOwnersShare
		,Bar_Position_Licence_Charge = @BarPosLicenceCharge
	WHERE Bar_Position_ID = @BarPosId
END
GO


