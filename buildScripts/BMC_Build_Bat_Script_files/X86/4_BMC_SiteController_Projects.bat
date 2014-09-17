@Echo OFF

ECHO				SITE CONTROLLER BINARIES AND DLL
SET varSourcePath="..\..\..\"

IF NOT EXIST %varSourcePath% GOTO LAST

FOR /f "useback tokens=*" %%a in ('%varSourcePath%') do set varSourcePath=%%~a

SET varFrameWorkDIR="C:\WINDOWS\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe"

"%varFrameWorkDIR%" "%varSourcePath%\Source\Site Controller\BGSMachineManager.Net\BGSMachineManager.Net\BGSMachineManager.vbproj" /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly; 

"%varFrameWorkDIR%" "%varSourcePath%\Source\Site Controller\BMC Guardian Service\SiteStats\SiteStatusService.csproj"  /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly; 

"%varFrameWorkDIR%" "%varSourcePath%\Source\Site Controller\BMC NetworkService\BMC.Transport.NetworkService\BMC.Transport.NetworkService.csproj" /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly; 

"%varFrameWorkDIR%" "%varSourcePath%\Source\Site Controller\BMC NetworkService\BMC.DBInterface.NetworkService\BMC.DBInterface.NetworkService.csproj" /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly; 

"%varFrameWorkDIR%" "%varSourcePath%\Source\Site Controller\BMC NetworkService\BMC.Business.NetworkService\BMC.Business.NetworkService.csproj" /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly; 

"%varFrameWorkDIR%" "%varSourcePath%\Source\Site Controller\BMC NetworkService\BMCNetworkService\BMCNetworkService.csproj" /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly; 

"%varFrameWorkDIR%" "%varSourcePath%\Source\Site Controller\BMC.EnterpriseProxy\BMC.EnterpriseProxy\BMC.EnterpriseProxy.csproj" /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly; 

"%varFrameWorkDIR%" "%varSourcePath%\Source\Site Controller\BMC.ExchangeConfiguration\BMC.Transport.ExchangeConfig\BMC.Transport.ExchangeConfig.csproj" /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly; 
pause
"%varFrameWorkDIR%" "%varSourcePath%\Source\Site Controller\BMC.ExchangeConfiguration\BMC.DBInterface.ExchangeConfig\BMC.DBInterface.ExchangeConfig.csproj" /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly; 

"%varFrameWorkDIR%" "%varSourcePath%\Source\Site Controller\BMC.ExchangeConfiguration\BMC.Business.ExchangeConfig\BMC.Business.ExchangeConfig.csproj" /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly; 

"%varFrameWorkDIR%" "%varSourcePath%\Source\Site Controller\BMC.Configuration\BMC.ExchangeConfig.csproj" /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly; 

"%varFrameWorkDIR%" "%varSourcePath%\Source\Site Controller\BMC.Configuration.Client\BMC.ExchangeConfig.Client.csproj" /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly; 

"%varFrameWorkDIR%" "%varSourcePath%\Source\Site Controller\BMC.ExchangeImportExportService\BMC.ExchangeWindowService\BMC.BusinessClasses\BMC.BusinessClasses.csproj" /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly; 

"%varFrameWorkDIR%" "%varSourcePath%\Source\Site Controller\BMC.ExchangeImportExportService\BMC.ExchangeWindowService\BMC.DataImportExport\BMC.DataImportExport.csproj" /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly; 

"%varFrameWorkDIR%" "%varSourcePath%\Source\Site Controller\BMC.ExchangeImportExportService\BMC.ExchangeWebService\BMC.ExchangeWebService.csproj" /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly; 

"%varFrameWorkDIR%" "%varSourcePath%\Source\Site Controller\BMC.ExchangeTicketExportService\BMC.ExchangeTicketExportService\BMC.ExchangeTicketExportService.csproj" /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly; 
pause
"%varFrameWorkDIR%" "%varSourcePath%\Source\Site Controller\BMCHourlyDailyReadService\BMC.HourlyDailyReadJobs.csproj" /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly; 

"%varFrameWorkDIR%" "%varSourcePath%\Source\Site Controller\CageBMCInterface\CageBMCInterface\CageBMCInterface.Service.csproj" /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly; 

"%varFrameWorkDIR%" "%varSourcePath%\Source\Site Controller\Comms\TicketingCOM\TicketingCOM\TicketingCOM.csproj" /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly; 

"%varFrameWorkDIR%" "%varSourcePath%\Source\Site Controller\TicketingWebService\TicketingWCFService\TicketingWCFService.csproj" /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly; 

"%varFrameWorkDIR%" "%varSourcePath%\Source\Site Controller\BMC.VaultInterface\BMC.VaultWebService\BMC.VaultWebService.csproj" /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly; 

"%varFrameWorkDIR%" "%varSourcePath%\Source\Site Controller\CashDispenser\BMC.CashDispenser.Core\BMC.CashDispenser.Core.csproj" /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly; 

"%varFrameWorkDIR%" "%varSourcePath%\Source\Site Controller\CashDeskOperator\BMC.Transport\BMC.Transport.csproj" /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly; 
pause
"%varFrameWorkDIR%" "%varSourcePath%\Source\Site Controller\CashDeskOperator\BMC.DBInterface.CashDeskOperator\BMC.DBInterface.CashDeskOperator.csproj" /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly; 

"%varFrameWorkDIR%" "%varSourcePath%\Source\Site Controller\CashDeskOperator\BMC.Business.CashDeskOperator\BMC.Business.CashDeskOperator.csproj" /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly; 

"%varFrameWorkDIR%" "%varSourcePath%\Source\Site Controller\CashDeskOperator\BMC.Presentation.CashDeskOperator\BMC.Presentation.CashDeskOperator\BMC.Presentation.CashDeskOperator.csproj" /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly; 

"%varFrameWorkDIR%" "%varSourcePath%\Source\Site Controller\Command Handpay\CommandHandPay.csproj" /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly; 

"%varFrameWorkDIR%" "%varSourcePath%\Source\Site Controller\MSMQ\BGSExchangeMonitor\BGSExchangeMonitor.vbproj" /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly; 

"%varFrameWorkDIR%" "%varSourcePath%\Source\Site Controller\ExchangeHost\BMCExchangeHost.csproj" /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly; 

"%varFrameWorkDIR%" "%varSourcePath%\Source\Site Controller\BMC.Configuration\BMC.ExchangeConfig.csproj" /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly; 
pause
"%varFrameWorkDIR%" "%varSourcePath%\Source\Site Controller\BMC.Configuration.Client\BMC.ExchangeConfig.Client.csproj" /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly; 

"%varFrameWorkDIR%" "%varSourcePath%\Source\Site Controller\CmdLine\Cmdline.csproj" /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly; 

"%varFrameWorkDIR%" "%varSourcePath%\Source\Site Controller\BMC.VaultInterface\BMC.VaultWebService\BMC.VaultWebService.csproj" /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly; 

"%varFrameWorkDIR%" "%varSourcePath%\Source\Utilities\BMCWrap\BMCWrap.csproj" /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly; 

"%varFrameWorkDIR%" "%varSourcePath%\Source\Site Controller\SiteLicensing DataViewer\SiteLicensing DataViewer.csproj" /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly; 

"%varFrameWorkDIR%" "%varSourcePath%\Source\Site Controller\BMC NetworkService\BMCNetworkService\BMCNetworkService.csproj" /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly; 

"%varFrameWorkDIR%" "%varSourcePath%\Source\Site Controller\BMC.PCIntegrationService\DataXChangeEndPointService\DataXChangeEndPointService.csproj" /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly; 

"%varFrameWorkDIR%" "%varSourcePath%\Source\Site Controller\FreeForm\FreeForm\FreeForm.csproj" /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly; 

"%varFrameWorkDIR%" "%varSourcePath%\Source\Site Controller\TIS\TISWinService\TISWinService.csproj" /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly; 

"%varFrameWorkDIR%" "%varSourcePath%\Source\Site Controller\TIS\TISCoreService\TISCoreService.csproj" /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly; 


"%varFrameWorkDIR%" "%varSourcePath%\Source\Site Controller\BMC.EBSCommunication\BMC.EBSInterface.Hosting\BMC.EBSComms.Hosting.csproj" /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly; 
"%varFrameWorkDIR%" "%varSourcePath%\Source\Site Controller\BMC.EBSCommunication\BMC.EBSComms.Server\BMC.EBSComms.Server.csproj" /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly; 
"%varFrameWorkDIR%" "%varSourcePath%\Source\Site Controller\BMC.EBSCommunication\BMC.EBSComms.DataLayer.Exchange\BMC.EBSComms.DataLayer.Exchange.csproj" /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly; 
"%varFrameWorkDIR%" "%varSourcePath%\Source\Site Controller\BMC.EBSCommunication\BMC.EBSComms.DataLayer.Enterprise\BMC.EBSComms.DataLayer.Enterprise.csproj" /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly; 
"%varFrameWorkDIR%" "%varSourcePath%\Source\Site Controller\BMC.EBSCommunication\BMC.EBSComms.DataLayer\BMC.EBSComms.DataLayer.csproj" /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly; 
"%varFrameWorkDIR%" "%varSourcePath%\Source\Site Controller\BMC.EBSCommunication\BMC.EBSComms.Contracts\BMC.EBSComms.Contracts.csproj" /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly; 




PAUSE
ECHO BUILDING SITECONROLLER
"%varFrameWorkDIR%" "%varSourcePath%\Source\Site Controller\CashDeskOperator\BMC.Presentation.CashDeskOperator\BMC.Presentation.CashDeskOperator\BMC.Presentation.CashDeskOperator.csproj" /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly; 


EXIT

:LAST
ECHO EXITING SITE CONTROLLER BUILD SCRIPT
PAUSE
