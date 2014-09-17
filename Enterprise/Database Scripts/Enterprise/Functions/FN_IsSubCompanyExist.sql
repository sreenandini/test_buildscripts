/************************************************************
 * Code formatted by SoftTree SQL Assistant © v4.8.29
 * Time: 02/05/2013 4:34:34 PM
 ************************************************************/

USE [Enterprise]
GO

/****** Object:  UserDefinedFunction [dbo].[IsSubCompanyExist]    Script Date: 05/02/2013 16:26:12 ******/
IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[IsSubCompanyExist]')
              AND TYPE IN (N'FN', N'IF', N'TF', N'FS', N'FT')
   )
    DROP FUNCTION [dbo].[IsSubCompanyExist]
GO

USE [Enterprise]
GO

/****** Object:  UserDefinedFunction [dbo].[IsSubCompanyExist]    Script Date: 05/02/2013 16:26:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--  
-- Description: Check SubCompany exist with the same name
--  
--  
-- ====================================================================  
-- Revision History  
--   
-- Venkatesan Haridass   02/May/13    Created   
---------------------------------------------------------------------------   

CREATE FUNCTION [dbo].[IsSubCompanyExist]
(
	@SubCompanyName  VARCHAR(50),
	@CompanyID       INT,
	@SubCompanyID INT
)
RETURNS BIT
AS
BEGIN
	IF EXISTS (
	       SELECT 1
	       FROM   Sub_Company(NOLOCK)
	       WHERE  Sub_Company_Name = @SubCompanyName
	              AND Company_ID = @CompanyID
	              AND Sub_Company_ID <> @SubCompanyID
	   )
	BEGIN
	    RETURN 1
	END
	
	RETURN 0
END
GO



