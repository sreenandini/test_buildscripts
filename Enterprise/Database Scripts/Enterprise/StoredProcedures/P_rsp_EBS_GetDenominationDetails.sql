USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[rsp_EBS_GetDenominationDetails]    Script Date: 03/03/2014 18:56:21 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_EBS_GetDenominationDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_EBS_GetDenominationDetails]
GO

USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[rsp_EBS_GetDenominationDetails]    Script Date: 03/03/2014 18:56:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*****************************************************************************************************
DESCRIPTION : Gets the Denomination Details  
CREATED DATE: 04 Mar 2014
CREATED BY: Venkat
MODULE            : Denomination      
CHANGE HISTORY :
------------------------------------------------------------------------------------------------------
AUTHOR                              DESCRIPTON                                                        MODIFIED DATE
------------------------------------------------------------------------------------------------------
                                                              
*****************************************************************************************************/

/*
	[rsp_EBS_GetDenominationDetails] '1212', '1.0'
*/
CREATE PROCEDURE [dbo].[rsp_EBS_GetDenominationDetails]
(
	@SiteCode VARCHAR(50) = NULL,
    @Denomination_Id VARCHAR(50) = NULL
)
AS

BEGIN
	SET NOCOUNT ON 
	SELECT 
		DISTINCT BD.Name AS DenominationId, 
		BD.Name AS DenominationName,
		BD.Name AS DenominationValue,
	CAST((CASE BD.SysDelete WHEN 0 THEN 1 ELSE 0 END) AS BIT) AS IsActive
	FROM BaseDenom BD
	WHERE
		Name = ISNULL(@Denomination_Id, Name)
	
	
      
END
GO