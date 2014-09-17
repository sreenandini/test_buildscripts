/************************************************************
 * Description: Updates Depot_Id in Bar_Position
 * Author: Aishwarrya V S
 ************************************************************/

USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[usp_UpdateDepotIDForBarPosition]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[usp_UpdateDepotIDForBarPosition]
GO

CREATE PROCEDURE [dbo].[usp_UpdateDepotIDForBarPosition](@Depot_ID INT, @Site_ID INT)
AS
BEGIN
	UPDATE Bar_Position
	SET    Depot_ID = @Depot_ID
	WHERE  Site_ID = @Site_ID
END   



