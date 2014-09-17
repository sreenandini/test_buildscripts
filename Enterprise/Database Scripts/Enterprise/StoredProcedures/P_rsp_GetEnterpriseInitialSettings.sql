USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetEnterpriseInitialSettings]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetEnterpriseInitialSettings]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE  PROCEDURE [dbo].[rsp_GetEnterpriseInitialSettings]
AS
BEGIN

	SELECT   
   BGSAdminWSUserID,  
   BGSAdminWSPwd,  
   SGVI_Enabled,  
   SGVI_Payment_Days,  
   SGVI_Statement_Number,  
   SGVI_Batch_Net_Value,  
   ReportServerURL,  
   ReportFolder, 
   ReportServerInstance,  
   EmptyReportMessage,  
   IsRegulatoryEnabled,  
   RegulatoryType,  
   IsAAMSApprovalForSiteRequired,  
   LGEConnectionDetails,  
   BASWebService,
   LGEEnabled,
   SenderCode,
   ISNull(IsLGERequired,'false') AS IsLGERequired,
   ISNull(WindowsServices,'') AS WindowsServices,
   IsPowerPromoReportsRequired,
   ISNULL(IsCertificateRequired, 'false') AS IsCertificateRequired,
   ISNULL(CertificateIssuer, '') AS     CertificateIssuer,
   Client,
   GuardianServerIPAddress,
   ISNULL(IsTransmitterEnabled,'0') AS IsTransmitterEnabled,
   ISNULL(STMServerIP,'') AS STMServerIP,
    EBSEndPointURL,
    IsEBSEnabled
   
     
 FROM  
  (  
  SELECT Setting_Name, Setting_Value  
  FROM Setting) AS Source  
  PIVOT  
   (  
   MAX(Setting_Value)  
    FOR Setting_Name IN (BGSAdminWSUserID,  
     BGSAdminWSPwd,  
     SGVI_Enabled,  
     SGVI_Payment_Days,  
     SGVI_Statement_Number,  
     SGVI_Batch_Net_Value,  
     ReportServerURL,  
     ReportFolder,  
	 ReportServerInstance   ,
     EmptyReportMessage,  
     IsRegulatoryEnabled,  
     RegulatoryType,  
     IsAAMSApprovalForSiteRequired,  
     LGEConnectionDetails,  
     BASWebService,
	 LGEEnabled,
	 SenderCode,
	 IsLGERequired,
	 WindowsServices,IsPowerPromoReportsRequired,IsCertificateRequired, CertificateIssuer,
	 Client,
	 GuardianServerIPAddress,IsTransmitterEnabled,STMServerIP,
	  EBSEndPointURL,
    IsEBSEnabled
)  
   ) AS Pvt  

	END

GO

