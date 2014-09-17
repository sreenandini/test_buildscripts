USE [Enterprise]
GO



IF EXISTS (
		SELECT *
		FROM sys.objects
		WHERE OBJECT_ID = OBJECT_ID(N'[dbo].[usp_CascadeUpdateBarPosition]')
			AND TYPE IN (
				N'P'
				,N'PC'
				)
		)
	DROP PROCEDURE [dbo].[usp_CascadeUpdateBarPosition]
GO



/****** Object:  StoredProcedure [dbo].[usp_CascadeUpdateBarPosition]    Script Date: 06/30/2014 20:16:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_CascadeUpdateBarPosition] (
	@CascadeOptions VARCHAR(20)
	,@Value VARCHAR(40)
	,@CascadeType VARCHAR(40)
	,@SetAsDefault CHAR(1)
	,@SubCompanyId INT
	,@SiteId INT
	)
AS
BEGIN
	DECLARE @ColumnName VARCHAR(40)
		,@DefaultColumnName VARCHAR(40)
		,@DefaultCascadeColumnName VARCHAR(40)
		,@UpdateQuery VARCHAR(max);

	BEGIN
		EXEC dbo.rsp_GetCascadeColumnFieldNames 'Bar_Position'
			,''
			,@CascadeOptions
			,@ColumnName OUT
			,@DefaultColumnName OUT
			,@DefaultCascadeColumnName OUT;

		IF @ColumnName <> ''
		BEGIN
			IF @CascadeType = 'CASCADE_ALL'
			BEGIN
				IF @CascadeOptions <> 'CASCADE_ITEM_TERMS'
				BEGIN
					SET @UpdateQuery = 'update BP set ' + @ColumnName + ' = ' + @Value + ' , ' + @DefaultColumnName + ' =  ' + @SetAsDefault + '  from Bar_Position BP inner join Site s on BP.Site_Id = s.Site_Id where Sub_Company_Id = ' + CAST(@SubCompanyId AS VARCHAR(20));
				END;
				ELSE
				BEGIN
					SET @UpdateQuery = 'update BP set  Terms_Group_ID= ' + @Value + ' , Terms_Group_ID_Default = ' + @SetAsDefault + ' , Terms_Group_Changeover_Date='''', Terms_Group_Future_ID= ' + @Value + '  from Bar_Position BP inner join Site s on BP.Site_Id = s.Site_Id where Sub_Company_Id = ' + CAST(@SubCompanyId AS VARCHAR(20));
				END;

				PRINT @UpdateQuery

				EXEC (@UpdateQuery);
			END;
			ELSE IF @CascadeType = 'CASCADE_DEFAULT'
			BEGIN
				-- Declare & init (2008 syntax)
				DECLARE @BarPositionId INT = 0;

				-- Iterate over all bar positions
				WHILE 1 = 1
				BEGIN
					-- Get next bar positions
					SET @UpdateQuery = 'SELECT TOP 1 @BarPositionId = Bar_Position_ID FROM Bar_Position WHERE Bar_Position_ID
                                                                                                          > 
                                                                                                          @BarPositionId AND Site_ID = ' + @SiteId + ' And ' + @DefaultCascadeColumnName + '=true ' + ' ORDER BY Bar_Position_ID';

					EXEC (@UpdateQuery);

					-- Exit loop if no more bar positions
					IF @@ROWCOUNT = 0
					BEGIN
						BREAK;
					END;

					IF @CascadeOptions <> 'CASCADE_ITEM_TERMS'
					BEGIN
						SET @UpdateQuery = 'UPDATE Bar_Position
									   SET ' + @ColumnName + ' = ' + @Value + ', ' + @DefaultColumnName + ' = ' + @SetAsDefault + 'WHERE Bar_Position_Id  =  ' + @BarPositionId;
					END;
					ELSE
					BEGIN
						SET @UpdateQuery = 'UPDATE Bar_Position
									   SET ' + @ColumnName + ' = ' + @Value + ', 
									   Terms_Group_Changeover_Date ='',  
									   Terms_Group_Future_ID = ' + @Value + ', ' + @DefaultColumnName + ' = ' + @SetAsDefault + 'WHERE Bar_Position_Id  =  ' + @BarPositionId;
					END;

					EXEC (@UpdateQuery);
				END;
			END;
		END;
	END;
END;
GO
