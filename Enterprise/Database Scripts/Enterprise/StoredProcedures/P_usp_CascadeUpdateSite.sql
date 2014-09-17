USE [Enterprise]
GO



IF EXISTS (
		SELECT *
		FROM sys.objects
		WHERE OBJECT_ID = OBJECT_ID(N'[dbo].[usp_CascadeUpdateSite]')
			AND TYPE IN (
				N'P'
				,N'PC'
				)
		)
	DROP PROCEDURE [dbo].[usp_CascadeUpdateSite]
GO



/****** Object:  StoredProcedure [dbo].[usp_CascadeUpdateSite]    Script Date: 06/30/2014 20:16:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_CascadeUpdateSite] (
	@CascadeOptions VARCHAR(20)
	,@Value VARCHAR(40)
	,@CascadeType VARCHAR(40)
	,@SetAsDefault CHAR(1)
	,@SubCompanyId INT
	,@UseAllSites CHAR(1)
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

	BEGIN
		EXEC dbo.rsp_GetCascadeColumnFieldNames 'Site'
			,'Bar_Position'
			,@CascadeOptions
			,@ColumnName OUT
			,@DefaultColumnName OUT
			,@DefaultCascadeColumnName OUT;

		IF @ColumnName <> ''
		BEGIN
			IF @UseAllSites = '1'
			BEGIN
				SET @Screen_Name = 'Cascade.Site'
				SET @UpdateQuery = 'SELECT @OldDefaultColumnValue=' + @DefaultColumnName + ', @OldColumnValue=' + @ColumnName + ' from Sub_Company where Sub_Company_ID = ' + CAST(@SubCompanyId AS VARCHAR(20));

				EXEC sp_executesql @UpdateQuery
					,N'@OldDefaultColumnValue int out, @OldColumnValue int out'
					,@OldDefaultColumnValue = @OldDefaultColumnValue OUT
					,@OldColumnValue = @OldColumnValue OUT

				SET @UpdateQuery = 'UPDATE Site SET ' + @ColumnName + ' = ' + @Value + ' , ' + @DefaultColumnName + ' =  ' + @SetAsDefault + 'WHERE Sub_Company_ID   =  ' + CAST(@SubCompanyId AS VARCHAR(20));

				EXEC (@UpdateQuery);

				SET @TempValue = 'Cascade update for site 0, type (' + Replace(@CascadeType, 'CASCADE_', '') + '), default = ' + @SetAsDefault + ' ..[' + Replace(Replace(@DefaultColumnName, '_ID', ''), '_', ' ') + '] ' + Cast(@OldDefaultColumnValue AS VARCHAR(max)) + ' --> ' + @SetAsDefault + '';

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

				SET @TempValue = 'Cascade update for site 0, type (' + Replace(@CascadeType, 'CASCADE_', '') + ', default = ' + @SetAsDefault + ' ..[' + Replace(Replace(@ColumnName, '_ID', ''), '_', ' ') + '] ' + CAst(@OldColumnValue AS VARCHAR(max)) + ' --> ' + @Value + '';

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

				IF @CascadeType = 'CASCADE_ALL'
				BEGIN
					EXEC dbo.usp_CascadeUpdateBarPosition @CascadeOptions
						,@Value
						,@CascadeType
						,@SetAsDefault
						,@SubCompanyId
						,0;
				END;
			END;
			ELSE
			BEGIN
				-- Declare & init (2008 syntax)
				DECLARE @SiteId INT = 0;

				-- Iterate over all site
				WHILE 1 = 1
				BEGIN
					-- Get next site
					SET @UpdateQuery = 'SELECT TOP 1  @SiteId = Site_Id FROM Site WHERE Site_ID > ' + @SiteId + 'And Sub_Company_Id = ' + @SubCompanyId + ' And ' + @DefaultCascadeColumnName + ' = true ORDER BY Site_ID';

					-- Exit loop if no more site available
					IF @@ROWCOUNT = 0
					BEGIN
						BREAK;
					END;

					SET @UpdateQuery = 'UPDATE Site SET ' + @ColumnName + ' = ' + @Value + ' , ' + @DefaultColumnName + ' =  ' + @SetAsDefault + 'WHERE Site_Id   =  ' + @SiteId;

					EXEC (@UpdateQuery);

					SET @TempValue = 'Cascade update for site 0, type (' + Replace(@CascadeType, 'CASCADE_', '') + '), default = ' + @SetAsDefault + ' ..[' + Replace(Replace(@DefaultColumnName, '_ID', ''), '_', ' ') + '] ' + CAST(@OldDefaultColumnValue AS VARCHAR(max)) + ' --> ' + @SetAsDefault + '';

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

					SET @TempValue = 'Cascade update for site 0, type (' + Replace(@CascadeType, 'CASCADE_', '') + ', default = ' + @SetAsDefault + ' ..[' + Replace(Replace(@ColumnName, '_ID', ''), '_', ' ') + '] ' + CAST(@OldColumnValue AS VARCHAR(max)) + ' --> ' + @Value + '';

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

					SET @UpdateQuery = 'SELECT @OldDefaultColumnValue=' + @DefaultColumnName + ', @OldColumnValue=' + @ColumnName + ' from Site WHERE Site_Id = ' + CAST(@SiteId AS VARCHAR(20));

					EXEC sp_executesql @UpdateQuery
						,N'@OldDefaultColumnValue int out, @OldColumnValue int out'
						,@OldDefaultColumnValue = @OldDefaultColumnValue OUT
						,@OldColumnValue = @OldColumnValue OUT

					SET @TempValue = 'Cascade update for site ' + @SiteId + ' type (' + Replace(@CascadeType, 'CASCADE_', '') + ', default = ' + @SetAsDefault + ' ..[' + Replace(Replace(@ColumnName, '_ID', ''), '_', ' ') + '] ' + CAST(@OldColumnValue AS VARCHAR(max)) + ' --> ' + @Value + '';

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

					IF @CascadeType = 'CASCADE_DEFAULT'
					BEGIN
						EXEC dbo.usp_CascadeUpdateBarPosition @CascadeOptions
							,@Value
							,@CascadeType
							,@SetAsDefault
							,@SubCompanyId
							,@SiteId;
					END;
					ELSE
					BEGIN
						IF @CascadeType = 'CASCADE_NONE'
						BEGIN
							SET @UpdateQuery = 'Update Bar_Position set ' + @DefaultCascadeColumnName + '= FALSE where Site_Id = ' + @SiteId + ' And ' + @DefaultCascadeColumnName + '=TRUE';

							EXEC (@UpdateQuery);

							SET @TempValue = 'Cascade update of position information - removing default ..[' + Replace(Replace(@ColumnName, '_ID', ''), '_', ' ') + '] ' + @OldColumnValue + ' --> ' + @Value + '';

							EXEC dbo.usp_InsertAuditData @User_ID
								,@User_Name
								,@Module_ID
								,@Module_Name
								,'Cascade.Position'
								,''
								,@ColumnName
								,@OldColumnValue
								,@Value
								,@TempValue
								,'MODIFY'
						END;
					END;
				END;
			END;
		END;
	END;
END;
GO
