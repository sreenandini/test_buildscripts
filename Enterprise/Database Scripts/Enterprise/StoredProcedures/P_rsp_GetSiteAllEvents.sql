USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetSiteAllEvents]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetSiteAllEvents]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =======================================================================  
-- OUTPUT    To get all events based on site for X number of days
-- =======================================================================  
-- Revision History  --  Exec  [Rsp_GetSiteAllEvents] 1003,10    
-- Vineetha Mathew 21/02/09  Created
---------------------------------------------------------------------------  
CREATE PROCEDURE [dbo].[rsp_GetSiteAllEvents]    
	@Site_Code VARCHAR(50),  
	@XDays int
AS   
BEGIN	
	DECLARE @Siteid INT

	SELECT @Siteid=site_id FROM SITE WHERE site_code=@Site_Code
	
		IF @Siteid>0
	
			BEGIN				
						SELECT 							
							
							Evt_Installation_ID,							
							Evt_Fault_Source,							
							Evt_Fault_Type,
							Evt_Datetime
							
						FROM Event  E						
							JOIN Installation i on i.Installation_ID=E.Evt_Installation_ID
							JOIN Bar_Position b on i.Bar_Position_ID=b.Bar_Position_ID
							JOIN Site S on b.Site_ID=S.Site_ID
						WHERE E.Evt_Datetime >= E.Evt_Datetime - @XDays and S.Site_id=@Siteid ORDER BY 1 DESC
						FOR XML PATH('Event') ,TYPE, ROOT('EventSiteXML')  
			 END

          
END  



GO

