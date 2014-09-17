GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_GetRemarkscollection]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_GetRemarkscollection]
GO

/************************************************************
 * Code Create by Rajkumar R
 * Time: 25/06/13 7:11:06 PM
 ************************************************************/
CREATE PROCEDURE usp_GetRemarkscollection
(
	@Collection_ID INT,@Remarks NTEXT
	)
AS
BEGIN
UPDATE [Collection]
SET

	Remarks =@Remarks WHERE Collection_ID=@Collection_ID
END
GO
