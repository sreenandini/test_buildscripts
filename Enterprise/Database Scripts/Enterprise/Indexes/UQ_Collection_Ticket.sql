/************************************************************
 * Code formatted by SoftTree SQL Assistant © v4.8.29
 * Time: 12/9/2013 1:12:07 PM
 ************************************************************/

USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.indexes
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[Collection_Ticket]')
              AND NAME = N'PK_Collection_Ticket_ID'
   )
    DROP INDEX [PK_Collection_Ticket_ID] ON [dbo].[Collection_Ticket] WITH (ONLINE = OFF)
GO

USE [Enterprise]
GO

CREATE CLUSTERED INDEX [PK_Collection_Ticket_ID] ON [dbo].[Collection_Ticket] 
(CT_ID ASC) ON [PRIMARY]
GO

IF EXISTS (
       SELECT *
       FROM   sys.indexes
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[Collection_Ticket]')
              AND NAME = N'IDX_Collection_Ticket_Promo'
   )
    DROP INDEX [IDX_Collection_Ticket_Promo] ON [dbo].[Collection_Ticket] WITH (ONLINE = OFF)
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [IDX_Collection_Ticket_Promo] ON [dbo].[Collection_Ticket] 
(CT_TicketType ASC, CT_IsPromotionalTicket ASC, CT_Inserted_Collection_ID ASC)INCLUDE(CT_Value) 
WITH (
         SORT_IN_TEMPDB = OFF,
         DROP_EXISTING = OFF,
         IGNORE_DUP_KEY = OFF,
         ONLINE = OFF
     ) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF NOT EXISTS (
       SELECT *
       FROM   sys.indexes
       WHERE  NAME = 'IDX_Collection_Ticket_Printed_Installation_Collection_ID'
              AND [object_id] = OBJECT_ID('Collection_Ticket')
   )
BEGIN
    CREATE NONCLUSTERED INDEX 
    [IDX_Collection_Ticket_Printed_Installation_Collection_ID]
    ON [dbo].[Collection_Ticket] ([CT_Printed_Installation_ID], [CT_Printed_Collection_ID])
    INCLUDE([CT_Value])
END
GO