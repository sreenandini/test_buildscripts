USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Staff_GPS_History]') AND name = N'Staff_GPS_History_ID')
DROP INDEX [Staff_GPS_History_ID] ON [dbo].[Staff_GPS_History] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Staff_GPS_History_ID] ON [dbo].[Staff_GPS_History] 
(
	[Staff_GPS_History_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Staff_GPS_History]') AND name = N'Staff_GPS_History_Staff_ID')
DROP INDEX [Staff_GPS_History_Staff_ID] ON [dbo].[Staff_GPS_History] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Staff_GPS_History_Staff_ID] ON [dbo].[Staff_GPS_History] 
(
	[Staff_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

