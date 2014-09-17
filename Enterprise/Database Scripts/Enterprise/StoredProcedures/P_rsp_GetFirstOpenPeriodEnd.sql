USE [Enterprise]
GO



IF EXISTS (
		SELECT *
		FROM sys.objects
		WHERE OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_GetFirstOpenPeriodEnd]')
			AND TYPE IN (
				N'P'
				,N'PC'
				)
		)
	DROP PROCEDURE [dbo].[rsp_GetFirstOpenPeriodEnd]
GO



/****** Object:  StoredProcedure [dbo].[rsp_GetFirstOpenPeriodEnd]    Script Date: 06/30/2014 20:16:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[rsp_GetFirstOpenPeriodEnd](@CurrentDate varchar(20))
AS
BEGIN
	  SELECT min(cast( Period_End_Final_Date as datetime ) ) as myDate from Period_End WHERE COALESCE(Statement_No,0) in ( 0, -1 ) AND cast ( Period_End_Final_Date as datetime ) < CAST(@CurrentDate as DateTime)
END;
GO
