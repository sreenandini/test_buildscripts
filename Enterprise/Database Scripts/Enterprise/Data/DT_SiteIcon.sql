

USE [Enterprise]
GO

IF NOT EXISTS(SELECT 1 FROM SiteIcon WHERE Machine_Type_Site_Icon = 'SLOT')
BEGIN
    INSERT INTO SiteIcon VALUES ( 'SLOT', 'SlotImages\SLOT.PNG'  )
END

IF NOT EXISTS(SELECT 1 FROM SiteIcon WHERE Machine_Type_Site_Icon = 'ER')
BEGIN
    INSERT INTO SiteIcon VALUES ( 'ER', 'SlotImages\ER.PNG'  )
END
GO

USE [Enterprise]
GO
INSERT INTO dbo.Export_History(EH_Date, EH_Reference1,EH_Type,EH_Site_Code)
SELECT GETDATE(), SITE_ID,'SITESETUP',Site_Code FROM dbo.Site
GO