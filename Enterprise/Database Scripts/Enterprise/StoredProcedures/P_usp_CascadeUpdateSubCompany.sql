USE [Enterprise]
GO



IF EXISTS (
		SELECT *
		FROM sys.objects
		WHERE OBJECT_ID = OBJECT_ID(N'[dbo].[usp_CascadeUpdateSubCompany]')
			AND TYPE IN (
				N'P'
				,N'PC'
				)
		)
	DROP PROCEDURE [dbo].[usp_CascadeUpdateSubCompany]
GO



/****** Object:  StoredProcedure [dbo].[usp_CascadeUpdateSubCompany]    Script Date: 06/30/2014 20:16:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_CascadeUpdateSubCompany] (
	@CascadeOptions VARCHAR(20)
	,@Value VARCHAR(40)
	,@CascadeType VARCHAR(40)
	,@Id INT
	,@SetAsDefault CHAR(1)
	,@User_ID INT
	,@User_Name VARCHAR(50)
	,@Module_ID INT
	,@Module_Name VARCHAR(50)
	)
AS
BEGIN
	DECLARE @ColumnName VARCHAR(40)
		,@DefaultColumnName VARCHAR(40)
		,@DefaultCascadeColumnName VARCHAR(40)
		,@UpdateQuery NVARCHAR(max)
		,@OldColumnValue INT
		,@OldDefaultColumnValue INT
		,@Old_Value VARCHAR(500)
		,@New_Value VARCHAR(500)
		,@TempValue VARCHAR(200)
		,@Screen_Name VARCHAR(50)

	SET NOCOUNT ON
	SET XACT_ABORT ON

	BEGIN
		EXEC dbo.rsp_GetCascadeColumnFieldNames 'Sub_Company'
			,'Site'
			,@CascadeOptions
			,@ColumnName OUT
			,@DefaultColumnName OUT
			,@DefaultCascadeColumnName OUT;

		BEGIN TRAN;

		IF @ColumnName <> ''
		BEGIN
			SET @Screen_Name = 'Cascade.SubCompany'
			SET @UpdateQuery = 'SELECT @OldDefaultColumnValue = ' + @DefaultColumnName + ', @OldColumnValue = ' + @ColumnName + ' from Sub_Company where Sub_Company_ID = ' + CAST(@Id AS VARCHAR(20))

			EXEC sp_executesql @UpdateQuery
				,N'@OldDefaultColumnValue int out, @OldColumnValue int out'
				,@OldDefaultColumnValue = @OldDefaultColumnValue OUT
				,@OldColumnValue = @OldColumnValue OUT

			SET @UpdateQuery = 'UPDATE Sub_Company set ' + @DefaultColumnName + '=' + @SetAsDefault + ', ' + @ColumnName + '=' + @Value + ' where Sub_Company_ID  = ' + CAST(@Id AS VARCHAR(20));

			EXEC (@UpdateQuery);

			SET @TempValue = 'Cascade update type (' + Replace(@CascadeType, 'CASCADE_', '') + ', default = ' + @SetAsDefault + ' ..[' + Replace(Replace(@DefaultCascadeColumnName, '_ID', ''), '_', ' ') + '] ' + CAST(@OldDefaultColumnValue AS VARCHAR(2)) + ' --> ' + @SetAsDefault + '';

			EXEC dbo.usp_InsertAuditData @User_ID
				,@User_Name
				,@Module_ID
				,@Module_Name
				,@Screen_Name
				,''
				,@DefaultColumnName
				,@OldDefaultColumnValue
				,@SetAsDefault
				,@TempValue
				,'MODIFY'

			SET @TempValue = 'Cascade update type (' + Replace(@CascadeType, 'CASCADE_', '') + ', default = ' + @SetAsDefault + ' ..[' + Replace(Replace(@ColumnName, '_ID', ''), '_', ' ') + '] ' + CAST(@OldColumnValue AS VARCHAR(2)) + ' --> ' + @Value + '';

			EXEC dbo.usp_InsertAuditData @User_ID
				,@User_Name
				,@Module_ID
				,@Module_Name
				,@Screen_Name
				,''
				,@ColumnName
				,@OldColumnValue
				,@Value
				,@TempValue
				,'MODIFY'
		END;

		IF @CascadeType = 'CASCADE_NONE'
		BEGIN
			SET @UpdateQuery = 'Update Site set ' + @DefaultCascadeColumnName + '= False where Sub_Company_Id = ' + CAST(@Id AS VARCHAR(20)) + ' And ' + @DefaultCascadeColumnName + ' = True';

			EXEC (@UpdateQuery);

			SET @TempValue = 'Cascade update of Site information - removing default ' + ' ..[' + Replace(Replace(@DefaultCascadeColumnName, '_ID', ''), '_', ' ') + '] 1 --> 0';

			EXEC dbo.usp_InsertAuditData @User_ID
				,@User_Name
				,@Module_ID
				,@Module_Name
				,@Screen_Name
				,''
				,@DefaultCascadeColumnName
				,'1'
				,'0'
				,@TempValue
				,'MODIFY'
		END;
		ELSE IF @CascadeType = 'CASCADE_DEFAULT'
		BEGIN
			EXEC dbo.usp_CascadeUpdateSite @CascadeOptions
				,@Value
				,@CascadeType
				,@SetAsDefault
				,@Id
				,'0'
				,@User_Id
				,@User_Name
				,@Module_Id
				,@Module_Name
		END;
		ELSE IF @CascadeType = 'CASCADE_ALL'
		BEGIN
			EXEC dbo.usp_CascadeUpdateSite @CascadeOptions
				,@Value
				,@CascadeType
				,@SetAsDefault
				,@Id
				,'1'
				,@User_Id
				,@User_Name
				,@Module_Id
				,@Module_Name
		END;;

		SELECT 1 AS ReturnValue

		COMMIT TRAN;
	END;

	SET XACT_ABORT OFF
	SET NOCOUNT OFF
END;
GO
