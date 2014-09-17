USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GeTicketVarByPOS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GeTicketVarByPOS]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROC dbo.[rsp_GeTicketVarByPOS]
@Collection_Batch_No INT  
AS  
BEGIN  
 SELECT MC.Machine_Stock_No,  
        B.bar_position_name,  
        I.installation_id,  
        CAST(  
            (  
                (  
                    COLLECTION_RDC_TICKETS_INSERTED_VALUE +  
                    RDC_TICKETS_INSERTED_NONCASHABLE_VALUE  
                ) / 100.00  
            ) AS DECIMAL(20, 2)  
        ) AS collection_rdc_tickets_inserted_value,  
        --declaredticketvalue,  
        CAST(  
            dbo.[fn_GetTicketsInByCollectionNo](C.Collection_Id, I.Installation_Id) AS DECIMAL(20, 2)  
        ) AS TicketsIN,  
        CAST(  
            (  
                dbo.[fn_GetTicketsInByCollectionNo](C.Collection_id, I.Installation_id)  
                -(  
                    (  
                        COLLECTION_RDC_TICKETS_INSERTED_VALUE +  
                        RDC_TICKETS_INSERTED_NONCASHABLE_VALUE  
                    ) / 100.00  
                )  
            )  
            AS DECIMAL(20, 2)  
        ) AS Variance,  
        Collection_Id
 FROM   dbo.collection C  
        INNER JOIN dbo.INSTALLATION I  
             ON  I.Installation_ID = C.Installation_id  
             AND C.declaration = 0  
             AND C.batch_id = @Collection_Batch_no  
        INNER JOIN dbo.BAR_POSITION B  
             ON  B.Bar_Position_id = I.Bar_Position_id
                 --AND (collection_rdc_tickets_inserted_value/100 - declaredticketvalue ) >0  
             AND dbo.fn_GetExceptionTicketsByPos(B.bar_position_name, Collection_id)  
                 > 0  
        INNER JOIN MACHINE MC  
             ON  MC.Machine_id = I.Machine_id  
END   

GO

