USE [Enterprise]
GO


IF EXISTS (
		SELECT *
		FROM sys.objects
		WHERE OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_GetCascadeColumnFieldNames]')
			AND TYPE IN (
				N'P'
				,N'PC'
				)
		)
	DROP PROCEDURE [dbo].[rsp_GetCascadeColumnFieldNames]
GO



/****** Object:  StoredProcedure [dbo].[rsp_GetCascadeColumnFieldNames]    Script Date: 06/30/2014 20:16:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[rsp_GetCascadeColumnFieldNames](
	@CurrentCascade varchar(20), 
	@NextCascade varchar(20), 
	@CasecadeOption varchar(20), 
	@ColumnName varchar(40) out,
	@DefaultColumnName varchar(40) out ,
	@DefaultCascadeColumnName varchar(40) out)
as
begin
	set @ColumnName = case @casecadeOption
				When 'CASCADE_ITEM_ACCESSKEY' then 'ACCESS_KEY_ID'
				when 'CASCADE_ITEM_JACKPOT' then @CurrentCascade + '_Jackpot'
				when 'CASCADE_ITEM_PERC_PAYOUT' then @CurrentCascade + '_Percentage_Payout'
				when 'CASCADE_ITEM_PPP' then @CurrentCascade + '_Price_Per_Play'
				when 'CASCADE_ITEM_REP' then 'Staff_ID'
				when 'CASCADE_ITEM_TERMS' then 'Terms_Group_ID'
				else ''
				end
	set @DefaultColumnName = @ColumnName+'_Default'
	if (@NextCascade <> '')
	begin
		set @DefaultCascadeColumnName = REPLACE(@DefaultColumnName, @CurrentCascade, @NextCascade)
	end
	return;
end;
GO
