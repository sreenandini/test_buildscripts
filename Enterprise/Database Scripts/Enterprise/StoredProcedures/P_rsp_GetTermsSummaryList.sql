USE [Enterprise]
GO

IF EXISTS (
		SELECT *
		FROM sys.objects
		WHERE OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_GetTermsSummaryList]')
			AND TYPE IN (
				N'P'
				,N'PC'
				)
		)
	DROP PROCEDURE [dbo].[rsp_GetTermsSummaryList]
GO

/****** Object:  StoredProcedure [dbo].[rsp_GetTermsSummaryList]    Script Date: 06/30/2014 20:13:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[rsp_GetTermsSummaryList] (
	@OperatorId INT
	,@DepotId INT
	,@MachineId INT
	,@SubCompanyId INT
	)
AS
BEGIN
	--select statement 
	SELECT site_name
		,bar_position_name
		,installation_id
		,installation_end_date
		,installation_price_per_play
		,installation_jackpot_value
		,machine_name
		,bar_position_supplier_position_code
		,site_code
		,bar_position_supplier_site_code
		,bar_position_use_terms
		,bar_position.bar_position_id
		,Bar_Pos_Machine_Type.machine_type_code AS Bar_Pos_Machine_Type_Code
		,terms_group.terms_group_name
	FROM (
		(
			(
				(
					(
						(
							(
								(
									bar_position LEFT JOIN installation ON bar_position.bar_position_id = installation.bar_position_id
									) LEFT JOIN depot ON bar_position.depot_id = depot.depot_id
								) LEFT JOIN machine ON installation.machine_id = machine.machine_id
							) LEFT JOIN machine_class ON machine.machine_class_id = machine_class.machine_class_id
						) LEFT JOIN machine_type ON machine_class.machine_type_id = machine_type.machine_type_id
					) INNER JOIN site ON bar_position.site_id = site.site_id
				) LEFT JOIN machine_type AS Bar_Pos_Machine_Type ON bar_position.machine_type_id = Bar_Pos_Machine_Type.machine_type_id
			) LEFT JOIN terms_group ON bar_position.terms_group_id = terms_group.terms_group_id
		)
	WHERE bar_position.bar_position_end_date IS NULL
		AND (
			(
				@DepotID > 0
				AND depot.depot_id = @DepotID
				)
			OR (
				@OperatorId > 0
				AND depot.supplier_id = @OperatorID
				)
			OR (
				@DepotID = 0
				AND @OperatorId = 0
				AND 1 = 1
				)
			)
		AND (
			(
				@MachineId > 0
				AND bar_position.machine_type_id = @MachineId
				)
			OR (
				@MachineID < 0
				AND (
					bar_position.machine_type_id = 0
					OR bar_position.machine_type_id IS NULL
					)
				)
			OR (
				@MachineID = 0
				AND 1 = 1
				)
			)
		AND (
			(
				@SubCompanyId > 0
				AND site.sub_company_id = @SubCompanyId
				)
			OR (
				@SubCompanyId = 0
				AND 1 = 1
				)
			)
	ORDER BY site.site_name ASC
		,site.site_id ASC
		,bar_position.bar_position_name ASC
		,bar_position.bar_position_id ASC
		,Cast((
				(
					CASE 
						WHEN installation.installation_end_date IS NULL
							THEN '01/01/2500'
						ELSE installation_end_date
						END
					)
				) AS DATETIME) DESC
END
GO


