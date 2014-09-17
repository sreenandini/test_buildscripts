s@Echo OFF

ECHO                          BUILDING ENTERPRISE BINARIES AND DLL

SET varSourcePath="..\..\..\"

IF NOT EXIST %varSourcePath% GOTO LAST

FOR /f "useback tokens=*" %%a in ('%varSourcePath%') do set varSourcePath=%%~a

SET varFrameWorkDIR="C:\WINDOWS\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe"

ECHO BUILDING BMC AUDIT VIEWER...
"%varFrameWorkDIR%" "%varSourcePath%\Source\Enterprise\Audit Viewer\AuditViewer.csproj"  /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly

ECHO BUILDING BMC DECLARATION...
"%varFrameWorkDIR%" "%varSourcePath%\Source\Enterprise\BMC Declaration\Declaration\BMC.Declaration.csproj" /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly

ECHO BUILDING BMC GUARDIAN...
"%varFrameWorkDIR%" "%varSourcePath%\Source\Enterprise\BMC Guardian\BMC.GuardianWebApp\BMC.GuardianWebApp.csproj" /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly

ECHO BUILDING BMC DATA SHEET...
"%varFrameWorkDIR%" "%varSourcePath%\Source\Enterprise\BMC.DataSheet\BMC.DataSheet.csproj" /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly

ECHO BUILDING BMC ENTERPRISE CLIENT...
"%varFrameWorkDIR%" "%varSourcePath%\Source\Enterprise\BMC.EnterpriseClient\BMC.EnterpriseClient\BMC.EnterpriseClient.csproj" /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly

ECHO BUILDING BMC DATA EXPORT TO SITE...
"%varFrameWorkDIR%" "%varSourcePath%\Source\Enterprise\BMC.EnterpriseImportExportService\BMC.DataExportToSite\BMC.DataExportToSite.csproj" /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly

ECHO BUILDING BMC ENTERPRISE WEB SERVICE...
"%varFrameWorkDIR%" "%varSourcePath%\Source\Enterprise\BMC.EnterpriseImportExportService\BMC.EnterpriseWebService\BMC.EnterpriseWebService.csproj" /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly

ECHO BUILDING BMC DATA IMPORT EXPORT...
"%varFrameWorkDIR%" "%varSourcePath%\Source\Enterprise\BMC.EnterpriseImportExportService\BMC.EnterpriseWindowService\BMC.DataImportExport\BMC.DataImportExport.csproj" /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly

ECHO BUILDING BMC METER ADJUSTMENT TOOL...
"%varFrameWorkDIR%" "%varSourcePath%\Source\Enterprise\BMC.MeterAdjustmentTool\BMC.MeterAdjustmentTool.csproj" /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly

ECHO BUILDING BMC MONITORING SERVICE...
"%varFrameWorkDIR%" "%varSourcePath%\Source\Enterprise\BMC.MonitoringService\BMC.MonitoringService.csproj" /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly

ECHO BUILDING BMC ENTERPRISE REPORTS...
"%varFrameWorkDIR%" "%varSourcePath%\Source\Enterprise\BMC.Reports\EnterpriseReports\EnterpriseReportsUI\BMC.EnterpriseReportsUI.csproj" /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly

ECHO BUILDING BMC RESOURCES...
"%varFrameWorkDIR%" "%varSourcePath%\Source\Enterprise\BMC.Resources\BMC.Resources.csproj" /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly

ECHO BUILDING BMC ENTERPRISE CLIENT CONFIG...
"%varFrameWorkDIR%" "%varSourcePath%\Source\Enterprise\BMCEnterpriseClientConfig\BMCEnterpriseClientConfig.csproj" /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly

ECHO BUILDING BMC ENTERPRISE CONFIG...
"%varFrameWorkDIR%" "%varSourcePath%\Source\Enterprise\BMCEnterpriseConfig\BMCEnterpriseConfig.csproj" /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly

ECHO BUILDING BMC SECURITY VB...
"%varFrameWorkDIR%" "%varSourcePath%\Source\Enterprise\BMCSecurityVB\BMC.SecurityVB.vbproj" /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly

ECHO BUILDING BMC SITE LICENSING...
"%varFrameWorkDIR%" "%varSourcePath%\Source\Enterprise\BMCSiteLicensing\BMCSiteLicensing\BMC.SiteLicensing.csproj"  /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly

ECHO BUILDING BMC TICKETING CONFIG...
"%varFrameWorkDIR%" "%varSourcePath%\Source\Enterprise\BMCTicketingConfig\BMCTicketingConfig\BMCTicketingConfig.csproj" /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly

ECHO BUILDING BMC EXPORT IMPORT ASSETDETAILS...
"%varFrameWorkDIR%" "%varSourcePath%\Source\Enterprise\ExportImportAssetDetails\ExportImportAssetDetails\ExportImportAssetDetails.csproj"  /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly

ECHO BUILDING BMC EXPORT PROFILER...
"%varFrameWorkDIR%" "%varSourcePath%\Source\Enterprise\ExportProfiler\BMC.Profiler\BMC.Profiler.csproj" /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly

ECHO BUILDING BMC REPORT ROLE ADMIN...
"%varFrameWorkDIR%" "%varSourcePath%\Source\Enterprise\ReportRoleAdmin\ReportRoleAdmin.csproj" /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly

ECHO BUILDING BMC ROUTE MANAGER...
"%varFrameWorkDIR%" "%varSourcePath%\Source\Enterprise\Route Manager\RouteManager\RouteManager.csproj"  /property:Configuration=Release;Platform=x86	 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly		

ECHO BUILDING BMC USER GROUP ADMIN...
"%varFrameWorkDIR%" "%varSourcePath%\Source\Enterprise\UserRoleAdmin\UserGroupAdmin.csproj" /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly

ECHO BUILDING BMC USER SITE ADMIN...
"%varFrameWorkDIR%" "%varSourcePath%\Source\Enterprise\UserSiteAdmin\UserSiteAdmin.csproj" /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly

ECHO BUILDING BMC DEPLOY REPORT...
"%varFrameWorkDIR%" "%varSourcePath%\Source\Enterprise\BMC.Reports\DeployReport\DeployReport.csproj" /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly

ECHO BUILDING ENTERPRISE PROJECTS COMPLETED...
PAUSE
ECHO 

EXIT

:LAST
ECHO EXITING ENTERPRISE BUILD SCRIPT
PAUSE
