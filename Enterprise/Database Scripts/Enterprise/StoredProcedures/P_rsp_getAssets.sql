USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_getAssets]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_getAssets]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROC rsp_getAssets      
AS      
BEGIN      
DECLARE @AFTEnabled bit        
EXEC rsp_GetSetting NULL, 'IsAFTEnabledForSite', 'false', @AFTEnabled OUTPUT           


SELECT 
m.machine_Stock_No as Stock_No ,       
m.actAssetNo,       
m.GMUNo,    
m.ActSerialNo,        
CASE WHEN (ISNULL(m.IsTITOEnabled,0) = 1) THEN 'Y' ELSE 'N' END AS TITOEnabled,       
CASE WHEN (ISNULL(m.IsNonCashVoucherEnabled,0) = 1) THEN 'Y' ELSE 'N' END AS NonCashVoucherEnabled,      
CASE WHEN (ISNULL(m.IsAFTEnabled,0) = 1) THEN 'Y' ELSE 'N' END AS AFTEnabled,      
CASE WHEN (ISNULL(m.IsMultiGame,0) = 1) THEN 'Y' ELSE 'N' END AS MultiGame,      
ISNULL(m.Machine_Alternative_Serial_Numbers,'') as AltSerialNo,      
ISNULL(m.Machine_MAC_Address,'') as MAC_Address,      
ISNULL(mt.Machine_Type_Code,'')as Machine_Type_Code,      
ISNULL(op.Operator_Name,'') as Operator_Name,       
ISNULL(dp.Depot_Name,'') as Depot_Name, 
--ISNULL(CAST(m.CMPGameType as varchar(50)),@temp) as CMPGameType,
--ISNULL(CAST(tbl.GameTypeCode AS VARCHAR(50)),@temp) as GameTypeCode,
--CASE WHEN(@AFTEnabled=1) THEN ISNULL(m.CMPGameType,'') ELSE
--CAST(ISNULL(m.CMPGameType, '') AS VARCHAR(2))  AS CmpGametype,
--CAST(ISNULL(tbl.GameTypeCode, '') AS VARCHAR(2))  AS GameTypeCode,
 --'' as CMPGameType,
 --''  AS GameTypeCode,
(case cast(ISNULL(m.CMPGameType, '') as varbinary(1)) when 0x00 then '' else cast(ISNULL(m.CMPGameType, '') as varbinary(1)) end)  AS CmpGametype,
(ISNULL(tbl.GameTypeCode, ''))  AS GameTypeCode,
--LEN(ISNULL(m.CMPGameType, ''))  AS CmpGametype1,
--LEN(ISNULL(tbl.GameTypeCode, ''))  AS GameTypeCode1,
--cast(ISNULL(m.CMPGameType, '') as varbinary(2)) as CmpGametype2,
ISNULL(Validation_Length,'18') as Validation_Length,       
ISNULL(mc.machine_Name,'') as machine_Name,      
ISNULL(ma.Manufacturer_Name,'') as Manufacturer_Name ,       
ISNULL(mot.MT_Model_Name,'') as MT_Model_Name,      
ISNULL(m.machine_occupancy_hour,0) as OccPerHour,      
CASE WHEN (m.machine_status_flag = 1) THEN 'In Use'       
  WHEN (m.machine_status_flag = 0) THEN 'In Stock'      
  WHEN (m.machine_status_flag = 13) THEN 'Terminated'      
  WHEN (m.machine_status_flag = 6) THEN 'Sold'        
  ELSE 'Unknown' END as Status,      
ISNULL(st.StackerName,'') as StackerName ,    
ISNULL(dep.Depreciation_Policy_Description,'') as Depreciation_Policy_Description,    
CASE WHEN (ISNULL(m.Depreciation_Policy_Use_Default,0) = 1) THEN 'Y' ELSE 'N' END AS Depreciation_Policy_Use_Default,    
ISNULL(m.Machine_Purchase_Invoice_Number,'') as Machine_Purchase_Invoice_Number,    
ISNULL(m.Machine_Purchased_From,'') as Machine_Purchased_From,    
ISNULL(m.Machine_Memo,'') as Machine_Memo,
CASE WHEN (isnull(m.IsDefaultAssetDetail,0)=1) then 
ISNULL(m.Base_Denom,0)
ELSE
0 END AS Base_Denom,
ISNULL(m.Percentage_Payout,0.0) AS Percentage_Payout,
CASE WHEN (ISNULL(m.IsDefaultAssetDetail,0) = 1) THEN 'Y' ELSE 'N' END AS IsDefaultAssetDetail,
isnull(m.Machine_Date_Entered,0) as Machine_Date_Entered,
CASE WHEN (ISNULL(m.GetGameDetails,0) = 1) THEN 'Y' ELSE 'N' END AS GetGameDetails,
CASE WHEN (ISNULL(m.IsGameCappingEnabled,0) = 1) THEN 'Y' ELSE 'N' END AS IsGameCappingEnabled,
isnull(m.AssetDisplayName,'') as AssetDisplayName,
ISNULL(M.Machine_Type_Of_Sale,'') AS Machine_Type_Of_Sale
,ISNULL(bp.Bar_Position_Name,0) AS BarPositionName
,ISNULL(z.Zone_Name,'') AS ZoneName,
ISNULL(m.Machine_Sold_To,'') AS MachineSoldTo,
CASE WHEN (ISNULL(m.Staff_ID,0)<>0) THEN 
ISNULL(sta.Staff_First_Name,0)
+' '+ISNULL(sta.Staff_Last_Name,0)
ELSE
'' END AS StaffRepresentative,
ISNULL(ac.TemplateName,'') AS AssetTemplateName,
ISNULL(MR.MTRT_Description,'') AS MachineTerminationReason,
ISNULL(m.Machine_End_Date,'') AS MachineEndDate,
ISNULL(m.Machine_Original_Purchase_Price,0.00) AS Machine_Original_Purchase_Price
from machine m
inner join machine_class mc on mc.machine_class_id = m.machine_class_id      
Left outer join Machine_Type mt on mc.Machine_Type_ID = mt.Machine_Type_ID      
Left outer join Manufacturer ma on ma.Manufacturer_ID = mc.Manufacturer_ID      
Left outer join Operator op on op.Operator_ID = m.Operator_ID      
Left outer join Depot dp on dp.Depot_ID = m.Depot_ID      
Left outer join Model_Type mot on mot.MT_ID = m.Machine_ModelTypeID      
Left outer join stacker st on st.Stacker_Id = m.Stacker_ID     
LEft outer join Depreciation_Policy dep on dep.Depreciation_Policy_ID = m.Depreciation_Policy_ID  
LEFT OUTER JOIN (
	SELECT A.installation_id,
       A.Machine_ID,
       A.Bar_Position_ID,
       A.Installation_End_Date
FROM   (
           SELECT MAX(installation_id) AS installation_id,
                  i.Machine_ID,
                  i.Bar_Position_ID,
                  MAX(
                      CAST(
                          CONVERT(VARCHAR(12), i.Installation_End_Date, 106) AS 
                          DATETIME
                      )
                  ) AS Installation_End_Date,
                  RANK()OVER(
                      PARTITION BY i.machine_id ORDER BY 
                      MAX(
                          CAST(
                              CONVERT(VARCHAR(12), i.Installation_End_Date, 106) 
                              AS DATETIME
                          )
                      )DESC,
                      MAX(installation_id) DESC
                  ) AS Rnumber
           FROM   dbo.Installation i
           WHERE  (i.Installation_End_Date IS NOT NULL)
           GROUP BY
                  i.machine_id,
                  i.Bar_Position_ID
       ) A
WHERE  A.Rnumber = 1 
       
       UNION
       
SELECT B.installation_id,
       B.Machine_ID,
       B.Bar_Position_ID,
       B.Installation_End_Date
FROM   (
           SELECT MAX(installation_id) AS installation_id,
                  i.Machine_ID,
                  i.Bar_Position_ID,
                  MAX(
                      CAST(
                          CONVERT(VARCHAR(12), i.Installation_End_Date, 106) AS 
                          DATETIME
                      )
                  ) AS Installation_End_Date,
                  RANK()OVER(
                      PARTITION BY i.machine_id ORDER BY 
                      MAX(
                          CAST(
                              CONVERT(VARCHAR(12), i.Installation_End_Date, 106) 
                              AS DATETIME
                          )
                      )DESC,
                      MAX(installation_id) DESC
                  ) AS Rnumber
           FROM   dbo.Installation i
           WHERE  (i.Installation_End_Date IS NULL)
           GROUP BY
                  i.machine_id,
                  i.Bar_Position_ID
       ) B
WHERE  B.Rnumber = 1
			
			
			) i 
ON i.Machine_ID=m.Machine_ID
LEFT OUTER JOIN Bar_Position bp ON i.Bar_Position_ID=bp.Bar_Position_ID  
LEFT OUTER JOIN Zone z ON bp.Zone_ID=z.Zone_ID 
LEFT OUTER JOIN Staff sta on sta.Staff_ID=m.Staff_ID
OUTER APPLY(select top 1 GameTypeCode from  tblCMPGameTypes tb where tb.GamePrefix = m.CMPGameType ) tbl
OUTER APPLY (select  top 1 TemplateName from AssetCreationTemplate ac where ac.Machine_Stock_No=m.Machine_Stock_No) ac
LEFT OUTER JOIN Machine_Termination_Reason_Types MR on MR.MTRT_ID=m.Machine_Termination_Reason
where 
	(mt.IsNonGamingAssetType = 0) 
	AND
	(
		(i.Installation_End_Date is null and
		 m.Machine_Status_Flag in (1))
		or  
		(m.Machine_Status_Flag in (13, 6,0))
	)
	
order by m.machine_Stock_No
END

GO



