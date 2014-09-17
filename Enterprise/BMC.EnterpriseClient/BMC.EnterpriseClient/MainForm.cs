using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.EnterpriseClient.Views;
using BMC.CoreLib.Win32;
using BMC.EnterpriseBusiness.Entities;
using BMC.EnterpriseBusiness.Business;
using BMC.Common.ExceptionManagement;
using BMC.EnterpriseClient.Helpers;
using System.IO;
using System.Drawing.Imaging;
using RES = BMC.EnterpriseClient.Properties.Resources;
using BMC.CoreLib.Concurrent;
using BMC.CoreLib;
using System.Threading;
using System.Data.SqlClient;
using BMC.Common.Utilities;
using BMC.SecurityVB;
using BMC.CoreLib.Diagnostics;
using BMC.Common.LogManagement;
using System.Globalization;
using System.Reflection;
using System.Diagnostics;
using BMC.Common;
using System.Configuration;
using BMC.EnterpriseClient.Views.ServiceCalls;


namespace BMC.EnterpriseClient
{
    public partial class MainForm : BMCExtendedForm
    {
        private AdminBusiness _businessAdmin = null;
        private SettingsBusiness _settingsBusiness = null;
        NotificationBusiness objBusiness = new NotificationBusiness();
        private int _iNotificationCount = 0;

        private class ImageDetails
        {
            public ImageDetails() { }
            public Image Image { get; set; }
            public Icon Icon { get; set; }
            public int ImageListIndex { get; set; }
        }

        private IDictionary<ToolStripItem, ImageDetails> _toolButtonIcons = null;
        private IDictionary<ToolStripButton, ToolStrip> _childToolbars = null;
        private IDictionary<string, ToolStripButton> _widgetMappings = null;
        private IDictionary<string, ToolStripButton> _toolMappings = null;
        private ToolStripButton _mainToolItemChecked = null;
        private ImageList _imglstSmallIcons = null;
        private bool _isconfigured = false;
        private object _inlineFormsLock = new object();
        private IExecutorService _executor = null;
        private bool? _isNetworkOK = false;
        private bool? _isDatabaseOK = null;
        private BMCSecurityCallMethod _bmcSecurityMethod = null;

        private bool _controlWindows = false;
        private bool _homeScreenWidgets = false;
        private HomeScreen _homeScreen = null;

        private bool isServiceCallEnabled = false;
        bool isFinancialEnabled = false;

        public MainForm()
        {
            InitializeComponent();
            SetTagProperty();
            this.Initialize();
            timer1.Start();
        }

        /// <summary>
        /// Assigns the Resource Key names to the controls--Created by kishore sivagnanam
        /// </summary>
        public void SetTagProperty()
        {

            this.mnuHelpAbout.Tag = "Key_About";
            this.mnuFile.Tag = "Key_FileCaption";
            this.helpToolStripMenuItem.Tag = "Key_HelpCaption";
            this.mnuViewStatusbar.Tag = "Key_Statusbar";
            this.mnuViewToolbar.Tag = "Key_Toolbar";
            this.mnuView.Tag = "Key_View";
            this.tbrItemAddVault.Tag = "Key_AddVault";
            this.tbrItemAdmin.Tag = "Key_Admin_WOShortCut";
            this.tsbAGSCombination.Tag = "Key_AGSCombination";
            this.tbrItemAnalysis.Tag = "Key_Analysis";
            this.tbrItemAssets.Tag = "Key_Assets";
            this.tbrItemVaultAudit.Tag = "Key_Audit";
            this.tbrItemMonitoring.Tag = "Key_AuditMonitoring";
            this.Tag = "Key_BMCEnterpriseClient";
            this.tbrItemCalendars.Tag = "Key_Calendars";
            this.sbrCaps.Tag = "Key_CAPS";
            this.tbrItemChangePwd.Tag = "Key_ChangePassword";
            this.mnuFileCloseAll.Tag = "Key_CloseAll";
            this.tbrItemClosedServiceCalls.Tag = "Key_ClosedCalls";
            this.tsbCollectionLiquidation.Tag = "Key_CollectionBasedLiquidation";
            this.tbrItemCreateServiceCall.Tag = "Key_CreateCall";
            this.tbrItemCurrentServiceCalls.Tag = "Key_CurrentCalls";
            this.tbrItemDataSheet.Tag = "Key_DataSheet";
            this.tbrItemDeclaration.Tag = "Key_Declaration";
            this.tbrItemVaultDeclaration.Tag = "Key_Declaration";
            this.tbrItemDepot.Tag = "Key_Depot";
            this.tbrItemDropSchedule.Tag = "Key_DropSchedule";
            this.mnuFileExit.Tag = "Key_ExitCaption1";
            this.tbrItemEventViewer.Tag = "Key_EventViewer";
            this.tbrItemExpenseShareGroup.Tag = "Key_ExpenseShareGroups";
            this.tbrItemFinancial.Tag = "Key_Financial";
            this.tbrItemGameLibrary.Tag = "Key_GameLibrary";
            this.tbrItemGMUEvents.Tag = "Key_GMUEvents";
            this.tbrItemLogin.Tag = "Key_Login";
            this.tbrItemLogout.Tag = "Key_Logout";
            this.sbrNum.Tag = "Key_NUM";
            this.tbrItemOpenHours.Tag = "Key_OpenHours";
            this.tbrItemOperators.Tag = "Key_Operators";
            this.tbrItemOrganisation.Tag = "Key_Organisation";
            this.tbrItemProfitShareGroup.Tag = "Key_ProfitShareGroups";
            this.tbrItemReadLiquidation.Tag = "Key_ReadBasedLiquidation";
            this.tbrItemReports.Tag = "Key_Reports";
            this.tbrItemServiceAdmin.Tag = "Key_ServiceAdmin";
            this.tbrItemServiceCalls.Tag = "Key_ServiceCalls";
            this.tbrItemSettings.Tag = "Key_Settings";
            this.tbrItemShareHolders.Tag = "Key_ShareHolders";
            this.tbrItemSiteLicensing.Tag = "Key_SiteLicensing";
            this.tbrItemSiteSettings.Tag = "Key_SiteSettings";
            this.tbrItemStacker.Tag = "Key_Stacker";
            this.tbrItemUserAdmin.Tag = "Key_UserAdmin";
            this.sbrItemUserDetails.Tag = "Key_UserName";
            this.tbrItemVault.Tag = "Key_Vault";
            this.tbrItemViewSites.Tag = "Key_ViewSites";
            this.tbrItemMeterAdjustment.Tag = "Key_MeterAdjustment";
            this.tbrItemSystemAudit.Tag = "Key_SystemAudit";
            this.tbrItemSystemMonitoring.Tag = "Key_SystemMonitoring";
            this.tbrItemDataCommsAudit.Tag = "Key_DataCommsAudit";
            this.tbrItemEmployee.Tag = "Key_EmployeeCard";
            this.tbrItemTerms.Tag = "Key_Terms";
            this.tbrItemTermsSummary.Tag = "Key_TermsSummary";
            this.tbrItemShares.Tag = "Key_Shares";
            this.tbrItemPeriodEnd.Tag = "Key_PeriodEnd";

        }

        private void Initialize()
        {
            ModuleProc PROC = new ModuleProc("MainForm", "Initialize");

            try
            {
                AppGlobals.Current.ActiveForm = this;
                _executor = ExecutorServiceFactory.CreateExecutorService();
                _businessAdmin = AppGlobals.Current.BusinessAdmin;
                _settingsBusiness = SettingsBusiness.CreateInstance();
                this.SuppressConfirmMessageBox = true;
                isServiceCallEnabled = BMC.CoreLib.Extensions.GetAppSettingValueBool("isServiceCallEnabled", false);
                isFinancialEnabled = BMC.CoreLib.Extensions.GetAppSettingValueBool("isFinancialEnabled", false);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
            finally
            {
                this.LoadInitialSettings();
                this.InitToolbars();
                this.InitializeSubTasks();
                this.InitComplete();
            }
        }

        private void InitToolbars()
        {
            try
            {
                if (BMC.CoreLib.Win32.Win32Extensions.IsRemoteSession)
                {
                    this.BackgroundImage = null;
                }

                this.Text = this.GetResourceTextByKey(1, "MSG_APP_TITLE");
                _controlWindows = Extensions.GetAppSettingValueBool("ControlWindows", false);
                _homeScreenWidgets = Extensions.GetAppSettingValueBool("HomeScreenWidgets", false);

                _bmcSecurityMethod = new BMCSecurityCallMethod();
                _toolButtonIcons = new Dictionary<ToolStripItem, ImageDetails>();

                _childToolbars = new Dictionary<ToolStripButton, ToolStrip>()
                {
                     { tbrItemAdmin, tbrChildAdmin },
                     {tbrItemVault, tbrChildVault},
                     { tbrItemServiceCalls, tbrChildServiceCalls },
                     { tbrItemMonitoring, tbrChildMonitoring },
                     { tbrItemFinancial,SettingsEntity.SGVI_Enabled?tbrChildFinancialSGVI:tbrChildFinancial },
                };
                _widgetMappings = new SortedDictionary<string, ToolStripButton>(StringComparer.InvariantCultureIgnoreCase);
                _toolMappings = new SortedDictionary<string, ToolStripButton>(StringComparer.InvariantCultureIgnoreCase);

                _imglstSmallIcons = new ImageList();
                dgvActiveItems.Dock = DockStyle.Fill;
                dgvActiveItems.ColumnHeadersVisible = false;

                this.AddInlineForm(tbrItemStacker, (g) =>
                        {
                            return (g.HasUserAccess("HQ_Admin_StackerFeature") && SettingsEntity.StackerFeature);
                        },
                        (o) =>
                        {
                            return new frmStackerDetails(frmStackerDetails.GridFormTypes.gftSakcer);
                        })
                    .AddInlineForm(tbrItemDropSchedule, (g) =>
                        {
                            return (g.HasUserAccess("HQ_Admin_DropSchedule") && SettingsEntity.DropScheduleAlert && SettingsEntity.StackerFeature);
                        },
                        (o) =>
                        {
                            return new frmStackerDetails(frmStackerDetails.GridFormTypes.gftSchedule);
                        })
                    .AddInlineForm(tbrItemDepot, (g) =>
                        {
                            return (g.HasUserAccess("HQ_Admin_Depot"));
                        },
                        (o) =>
                        {
                            return new frmDepot();
                        })
                    .AddInlineForm(tbrItemOperators, (g) =>
                        {
                            return (g.HasUserAccess("HQ_Admin_Operator"));
                        },
                        (o) =>
                        {
                            return new frmOperator();
                        })
                    .AddInlineForm(tbrItemUserAdmin, (g) =>
                        {
                            return (g.HasUserAccess("HQ_Admin_Groups") || g.HasUserAccess("HQ_Admin_Users"));
                        },
                        (o) =>
                        {
                            return new frmUserAdministration(this.LoggedinUser.UserName, this.LoggedinUser.SecurityUserID);
                        })
                    //.AddInlineForm(tbrItemGroups, (o) => { return new frmUserGroup(); })
                    // .AddInlineForm(tbrItemAccess, (o) => { return new frmUserSiteAccess(); })
                    // .AddInlineForm(tbrItemUsers, (o) => { return new UserAdminForm(this.LoggedinUser.UserName); })
                    .AddInlineForm(tbrItemAssets, (g) =>
                        {
                            return (g.HasUserAccess("HQ_Stock"));
                        },
                        (o) =>
                        {
                            return new frmAssetManagement();
                        })
                    .AddInlineForm(tbrItemOrganisation, (g) =>
                        {
                            return (g.HasUserAccess("HQ_Admin_Customers"));
                        },
                        (o) =>
                        {
                            if (o != null)
                            {
                                return new frmOrganisation(o as OrganisationInput);
                            }
                            else
                            {
                                return new frmOrganisation();
                            }
                        })
                    .AddInlineForm(tbrItemEventViewer,
                        (o) =>
                        {
                            return new frmEventViewer();
                        })
                    .AddInlineForm(tbrItemGameLibrary, (g) =>
                        {
                            return (g.HasUserAccess("HQ_Gamelibrary"));
                        },
                        (o) =>
                        {
                            return new frmGameLibrary();
                        })
                    .AddInlineForm(tbrItemOpenHours, (g) =>
                        {
                            return (g.HasUserAccess("HQ_Admin_OpenHours"));
                        },
                        (o) =>
                        {
                            return new frmAdminOpeningTimes();
                        })
                    .AddInlineForm(tbrItemSettings, (g) =>
                        {
                            return (g.HasUserAccess("HQ_Admin_Settings"));
                        },
                        (o) =>
                        { return new frmAdminSettings(); })

                    // Profit share
                    .AddInlineForm(tbrItemShareHolders, (g) =>
                        {
                            return (!SettingsEntity.SGVI_Enabled && g.HasUserAccess("HQ_Financial_ShareHolder") && SettingsEntity.LiquidationProfitShare);
                        },
                        (o) =>
                        {
                            return new frmShareHolderList();
                        })
                    .AddInlineForm(tbrItemProfitShareGroup, (g) =>
                        {
                            return (!SettingsEntity.SGVI_Enabled && g.HasUserAccess("HQ_Financial_ProfitShare") && SettingsEntity.LiquidationProfitShare);

                        },
                        (o) =>
                        {
                            return new frmCommonShareGroupList(CommonProfitShareType.ProfitShare);
                        })
                    .AddInlineForm(tbrItemExpenseShareGroup, (g) =>
                        {
                            return (!SettingsEntity.SGVI_Enabled && g.HasUserAccess("HQ_Financial_ExpenseShare") && SettingsEntity.LiquidationProfitShare);

                        },
                        (o) =>
                        {
                            return new frmCommonShareGroupList(CommonProfitShareType.ExpenseShare);
                        })
                    .AddInlineForm(tbrItemReadLiquidation, (g) =>
                        {
                            return (!SettingsEntity.SGVI_Enabled && g.HasUserAccess("HQ_Financial_ReadLiquidation") && SettingsEntity.LiquidationProfitShare && SettingsEntity.CentralizedReadLiquidation);
                        },
                        (o) =>
                        {
                            return new frmReadBasedLiquidation();
                        })
                    .AddInlineForm(tsbCollectionLiquidation, (g) =>
                        {
                            return (!SettingsEntity.SGVI_Enabled && g.HasUserAccess("HQ_Financial_CollectionLiquidation") && SettingsEntity.LiquidationProfitShare && SettingsEntity.CentralizedDeclaration);

                        },
                        (o) =>
                        {
                            return new frmLiquidationBatch();
                        })

                        //SGVI screens
                        .AddInlineForm(tbrItemTerms, (g) =>
                        {
                            return (SettingsEntity.SGVI_Enabled && g.HasUserAccess("HQ_Financial_TermsProfiles"));
                        },
                        (o) =>
                        {
                            return new frmTersmsProfiles();
                        })
                    .AddInlineForm(tbrItemShares, (g) =>
                    {
                        return (SettingsEntity.SGVI_Enabled && g.HasUserAccess("HQ_Financial_ShareSchedules"));
                    },
                        (o) =>
                        {
                            return new frmShareSchedules();
                        })
                    .AddInlineForm(tbrItemTermsSummary, (g) =>
                    {
                        return (SettingsEntity.SGVI_Enabled && g.HasUserAccess("HQ_Financial_TermsSummary"));
                    },
                        (o) =>
                        {
                            return new frmTermsSummary();
                        })
                    .AddInlineForm(tbrItemPeriodEnd, (g) =>
                    {
                        return (SettingsEntity.SGVI_Enabled && g.HasUserAccess("HQ_Financial_PeriodEndTermsProcessor"));
                    },
                        (o) =>
                        {
                            return new frmPeriodEndTermsProcessor();
                        })

                    .AddInlineForm(tbrItemCalendars, (g) =>
                        {
                            return (g.HasUserAccess("HQ_Admin_Calendar"));
                        },
                        (o) =>
                        {
                            return new frmCompanyCalender();
                        })
                        .AddInlineForm(tbrItemEmployee, (g) =>
                        {
                            return (SettingsEntity.IsEmployeeCardTrackingEnabled);
                        },
                        (o) =>
                        {
                            return new frmCreateViewEmployeeCards();
                        })

                     //AGS Combination is now hidden (since it has been moved to Setting screen)
                     .AddInlineForm(tsbAGSCombination, (g) =>
                         {
                             return false;
                         },
                         (o) =>
                         {
                             return new frmAGSCombination();
                         })

                        .AddInlineForm(tbrItemViewSites, (g) =>
                        {
                            return (g.HasUserAccess("HQ_Sites"));
                        },
                        (o) =>
                        {
                            return new ViewSitesForm();
                        })
                        .AddInlineForm(tbrItemVault, (g) =>
                        {
                            return (g.HasUserAccess("HQ_Admin_VaultInterface")) && SettingsEntity.IsVaultEnabled;
                        },
                        null
                        )
                        .AddInlineForm(tbrItemAddVault, (g) =>
                        {
                            return (g.HasUserAccess("HQ_Admin_CreateVault"));
                        },
                           (o) =>
                           {
                               return new frmVaultAdmin();
                           }
                        )
                        .AddInlineForm(tbrItemVaultDeclaration, (g) =>
                        {
                            return (g.HasUserAccess("HQ_Admin_VaultDeclaration"));
                        },
                           (o) =>
                           {
                               return new frmOutstandingVaultDrops();
                           }
                        )
                        .AddInlineForm(tbrItemVaultAudit, (g) =>
                        {
                            return (g.HasUserAccess("HQ_Admin_AuditVaultDeclaration"));
                        },
                           (o) =>
                           {
                               return new frmVaultDropHistory();
                           }
                        )
                        .AddInlineForm(tbrItemGMUEvents, (g) =>
                        {
                            return true;
                            //return (g.HasUserAccess("HQ_GMU_Events"));
                        },
                        (o) =>
                        {
                            return new frmConfiguareGMUFaults();
                        })
                                .AddInlineForm(tbrItemCurrentServiceCalls, (g) =>
                        {
                            return (g.HasUserAccess("HQ_Engineers_Current"));
                        },
                        (o)=>
                            {
                                return new frmServiceCalls("CurrentCalls");
                            }
                            )
 						.AddInlineForm(tbrItemServiceAdmin, (g) =>
                            {
                               return (g.HasUserAccess("HQ_Engineers_Engineers")); 
                            },
                            (o) =>
                                {
                                    return new frmServiceAdmin();
                                }
                         )
                          .AddInlineForm(tbrItemCreateServiceCall, (g) =>
                            {
                                return (g.HasUserAccess("HQ_Engineers_CreateCall")); 
                            },
                            (o) =>
                            {
                                return new frmServiceCallCreate();
                            }
                         )
                        .AddInlineForm(tbrItemClosedServiceCalls, (g) =>
                        {
                            return (g.HasUserAccess("HQ_Engineers_Closed"));
                        },
                        (o) =>
                        {
                            return new frmServiceCalls("ClosedCalls");
                        })
                         .AddInlineForm(tbrItemAlerts, (g) =>
                         {
                             return (g.HasUserAccess("HQ_Admin_EmailAlerts") && SettingsEntity.IsAlertEnabled && SettingsEntity.IsEmailAlertEnabled);
                         },
                           (o) =>
                           {
                               return new frmMailAlertConfiguration();
                           }
                        )

                         .AddInlineForm(tbrItemShowAlerts, (g) =>
                         {
                             return (g.HasUserAccess("HQ_Admin_AlertAudit") && SettingsEntity.IsAlertEnabled );
                         },
                           (o) =>
                           {
                               return new frmShowAlerts();
                           }
                        )
                        .AddInlineForm(tbrItemSiteSettings, (g) =>
                        {
                            return (g.HasUserAccess("HQ_Admin_SiteSettings"));
                        },
                            (o) =>
                            {
                                return new frmCentralizedSiteSetting();
                            }
                        )
                        .AddInlineForm(tbrItemTerms, (g) =>
                        {
                            return (g.HasUserAccess("HQ_Financial_Terms"));
                        },
                            (o) =>
                            {
                                return new frmTersmsProfiles();
                            }
                        )



                    // External Links
                    .AddExternalExe(tbrItemDeclaration, (g) =>
                    {
                        return g.HasUserAccess("HQ_Admin_Declaration") && SettingsEntity.CentralizedDeclaration;
                    },
                                                        "BMC.Declaration.exe", this.GetEncryptedDecUserInfo)

                    .AddExternalExe(tbrItemReports, (g) =>
                    {
                        return g.HasUserAccess("HQ_Reports");
                    },
                                                        "BMC.EnterpriseReports.exe", this.GetEncryptedReportRoleInfo, true)

                    .AddExternalExe(tbrItemDataSheet, (g) =>
                    {
                        return g.HasUserAccess("HQ_DataSheet");
                    },
                                                        "BMC.DataSheet.exe", this.GetEncryptedDecUserInfo)

                    .AddExternalExe(tbrItemSiteLicensing,
                                                    (g) =>
                                                    {
                                                        return g.HasUserAccess("HQ_Admin_SiteLicensing") && SettingsEntity.IsSiteLicensingEnabled;
                                                    },
                                                    "BMC.SiteLicensing.exe", this.GetEncryptedSLUserInfo, false)

                    .AddExternalExe(tbrItemMeterAdjustment,
                                                    (g) =>
                                                    {
                                                        return (g.HasUserAccess("HQ_Admin_MeterAdjustment"));
                                                    },
                                                    "BMC.MeterAdjustmentTool.exe", this.GetEncryptedSLUserInfo, false)

                    // Audit & Monitoring
                    .AddExternalExe(tbrItemSystemAudit, (g) =>
                    {
                        return g.HasUserAccess("HQ_SystemAudit");
                    }, "AuditViewer.exe", this.GetEncryptedAuditRoleInfo, true)

                    .AddExternalExe(tbrItemDataCommsAudit, (g) =>
                    {
                        return g.HasUserAccess("HQ_DataCommsAudit");
                    }, "BMC.Profiler.exe", string.Empty)

                    .AddInlineForm(tbrItemAnalysis, (g) =>
                    {
                        return g.HasUserAccess("HQ_Analysis");
                    },
                           (o) =>
                           {
                               return new frmMeterAnalysis(this.LoggedinUser.SecurityUserID);
                           }
                        )
                    // custom action
                    .AddCustomAction(tbrItemChangePwd, null, () =>
                    {
                        this.ShowChangePasswordForm(AuthenticationResult.Unauthenticated);
                    }
                                     )
                    .AddCustomAction(tbrItemSystemMonitoring, (g) =>
                                                                { return g.HasUserAccess("HQ_Guardian"); }, this.OnOpenSystemMonitoring)
                    ;

                foreach (KeyValuePair<ToolStripButton, ToolStrip> pair in _childToolbars)
                {
                    pair.Key.CheckOnClick = true;
                    pair.Key.CheckedChanged += new EventHandler(OnMainToolCheckItem_CheckedChanged);
                }

                foreach (KeyValuePair<object, AppSubTaskLink> pair in AppGlobals.Current.SubTasks)
                {
                    ((ToolStripItem)pair.Key).Click += new EventHandler(OnMainToolItem_Clicked);
                }
                this.CreateToolItemResources();
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void OnOpenSystemMonitoring()
        {
            new Future(() =>
            {
                bool isCertificateRequired = TypeSystem.GetValueBoolSimple(AdminBusiness.GetSetting("IsCertificateRequired", string.Empty));
                string guardianServerIP = AdminBusiness.GetSetting("GuardianServerIPAddress", string.Empty);
                string guardianWebURL = string.Format("http{0}://{1}{2}/BMCGuardian/Login.aspx",
                    (isCertificateRequired ? "s" : ""),
                    guardianServerIP,
                    (isCertificateRequired ? ":4444" : ":" + BMC.Common.ConfigurationManagement.ConfigManager.Read("GuardianPortNo")));
                this.CrossThreadInvoke(new Action(() =>
                {
                    NativeMethods.OpenApplication(guardianWebURL, null);
                }));
            });
        }

        private string[] GetEncryptedUserInfo()
        {
            return new string[] 
            {
                _bmcSecurityMethod.Encrypt(this.LoggedinUser.UserName.Replace(" ", "")),
                _bmcSecurityMethod.Encrypt(this.LoggedinUser.SecurityUserID.ToString())
            };
        }

        private string[] GetEncryptedSecurityUserInfo()
        {
            return new string[] 
            {               
                _bmcSecurityMethod.Encrypt(this.LoggedinUser.SecurityUserID.ToString())
            };
        }
        private string[] GetEncryptedUserInfo1()
        {
            return new string[] 
            {
                _bmcSecurityMethod.Encrypt(this.LoggedinUser.UserName.Replace(" ", "")),
                _bmcSecurityMethod.Encrypt(this.LoggedinUser.StaffID.ToString())
            };
        }

        private string[] GetEncryptedDecUserInfo()
        {
            return new string[] 
            {
                _bmcSecurityMethod.Encrypt(this.LoggedinUser.UserName.Replace(" ", "")),
                _bmcSecurityMethod.Encrypt(this.LoggedinUser.StaffID.ToString()),
                _bmcSecurityMethod.Encrypt(this.LoggedinUser.SecurityUserID.ToString())
            };
        }

        private string[] GetEncryptedSLUserInfo()
        {
            return new string[] 
            {
                _bmcSecurityMethod.Encrypt(this.LoggedinUser.UserName.Replace(" ", "")),
                _bmcSecurityMethod.Encrypt(this.LoggedinUser.StaffID.ToString()),
                _bmcSecurityMethod.Encrypt(this.LoggedinUser.SecurityUserID.ToString())
            };
        }

        private string[] GetEncryptedReportRoleInfo()
        {

            return new string[] 
            {
                _bmcSecurityMethod.Encrypt(this.LoggedinUser.RoleName),
                "[DELIM]",
                _bmcSecurityMethod.Encrypt(this.LoggedinUser.SecurityUserID.ToString())
            };
        }

        private string[] GetEncryptedAuditRoleInfo()
        {

            return new string[] 
            {
                _bmcSecurityMethod.Encrypt("AuditViewer"),
                "[DELIM]",
                 _bmcSecurityMethod.Encrypt(this.LoggedinUser.SecurityRoleID.ToString()),
                 "[DELIM]",
                _bmcSecurityMethod.Encrypt(this.LoggedinUser.UserName),
                "[DELIM]",
                _bmcSecurityMethod.Encrypt(this.LoggedinUser.SecurityUserID.ToString())
            };
        }

        private void CreateToolItemResources()
        {
            try
            {
                var imageDecoder = (from d in ImageCodecInfo.GetImageDecoders()
                                    where d.CodecName == "Built-in ICO Codec"
                                    select d).FirstOrDefault();
                if (imageDecoder == null) return;

                ToolStrip[] toolStrips = new ToolStrip[] 
                {
                    tbrNoLogin, tbrMain, 
                    tbrChildAdmin, tbrChildMonitoring, 
                    tbrChildServiceCalls, tbrChildFinancial, tbrChildVault,tbrChildFinancialSGVI
                };

                foreach (ToolStrip toolStrip in toolStrips)
                {
                    foreach (ToolStripItem item in toolStrip.Items)
                    {
                        if (item is ToolStripButton &&
                            item.Image != null)
                        {
                            try
                            {
                                ToolStripButton button = (ToolStripButton)item;
                                Size iconSize = new Size(16, 16);
                                Image imageOriginal = item.Image;
                                Icon icon = null;
                                Image imageThumbnail = null;
#if !NO_TOOL_ICONS
                                imageThumbnail = imageOriginal.GetThumbnailImage(iconSize.Width, iconSize.Height, null, IntPtr.Zero);
                                {
                                    using (Bitmap bitmapIcon = new Bitmap(imageThumbnail, new Size(16, 16)))
                                    {
                                        bitmapIcon.MakeTransparent(item.ImageTransparentColor);
                                        IntPtr icoNative = bitmapIcon.GetHicon();
                                        icon = Icon.FromHandle(icoNative);
                                    }
                                }
#endif

                                if (icon != null &&
                                    !_toolButtonIcons.ContainsKey(button))
                                {
                                    int index = _imglstSmallIcons.Images.Count;
                                    _imglstSmallIcons.Images.Add(icon);
                                    ImageDetails details = new ImageDetails()
                                    {
                                        Icon = icon,
                                        Image = imageThumbnail,
                                        ImageListIndex = index,
                                    };
                                    _toolButtonIcons.Add(button, details);
                                }

                                // Resource Name
                                string resourceText = string.Empty;
                                try
                                {
                                    if (button.Tag != null &&
                                        button.Tag.GetType() == typeof(string))
                                    {
                                        resourceText = ResourceExtensions.GetResourceTextByKey(null, button.Tag.ToString());
                                    }
                                }
                                catch { }
                                if (!resourceText.IsEmpty())
                                {
                                    button.Text = resourceText;
                                    button.ToolTipText = resourceText.Replace("&&", "&");
                                }
                            }
                            catch (Exception ex)
                            {
                                ExceptionManager.Publish(ex);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void LoadInitialSettings()
        {
            _settingsBusiness.GetInitialSettings();
        }

        private MainForm AddInlineForm(ToolStripButton button, Func<object, Form> createForm)
        {
            return this.AddInlineForm(button, (Func<AppGlobals, bool>)null, createForm);
        }

        private MainForm AddInlineForm(ToolStripButton button, Func<AppGlobals, bool> accessName, Func<object, Form> createForm)
        {
            string name = button.Name;
            if (!name.IsEmpty())
            {
                if (!_widgetMappings.ContainsKey(name))
                {
                    _widgetMappings.Add(name, button);
                }
                if (!_toolMappings.ContainsKey(name))
                {
                    _toolMappings.Add(name, button);
                }
            }
            AppGlobals.Current.AddSubTask(button, accessName, createForm);
            return this;
        }

        private MainForm AddExternalExe(ToolStripButton button, AppSubTaskLink task)
        {
            button.Enabled = task.IsExternalFileExists;
            task.ExternalProcessOpened += new ExternalProcessOpenedHandler(OnTask_ExternalProcessOpened);
            task.ExternalProcessClosed += new ExternalProcessClosedHandler(OnLink_ExternalProcessClosed);
            return this;
        }

        private MainForm AddExternalExe(ToolStripButton button, string filePath, string arguments)
        {
            return this.AddExternalExe(button, (Func<AppGlobals, bool>)null, filePath, arguments);
        }

        private MainForm AddExternalExe(ToolStripButton button, Func<AppGlobals, bool> accessName, string filePath, string arguments)
        {
            return this.AddExternalExe(button, AppGlobals.Current.AddSubTask(button, accessName, filePath, arguments));
        }

        private MainForm AddExternalExe(ToolStripButton button, string filePath, Func<string[]> argumentsFunc)
        {
            return this.AddExternalExe(button, (Func<AppGlobals, bool>)null, filePath, argumentsFunc);
        }

        private MainForm AddExternalExe(ToolStripButton button, Func<AppGlobals, bool> accessName, string filePath, Func<string[]> argumentsFunc, bool ignoreSpace)
        {
            return this.AddExternalExe(button, AppGlobals.Current.AddSubTask(button, accessName, filePath, argumentsFunc, ignoreSpace));
        }

        private MainForm AddExternalExe(ToolStripButton button, Func<AppGlobals, bool> accessName, string filePath, Func<string[]> argumentsFunc)
        {
            return this.AddExternalExe(button, AppGlobals.Current.AddSubTask(button, accessName, filePath, argumentsFunc));
        }

        private MainForm AddCustomAction(ToolStripButton button, Func<AppGlobals, bool> accessName, Action customAction)
        {
            AppGlobals.Current.AddSubTask(button, accessName, customAction);
            return this;
        }

        private AppSubTaskLink GetSubTask(ToolStripButton button)
        {
            return AppGlobals.Current.GetSubTask(button);
        }

        void OnMainToolItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (!this.Validate(sender)) return;
                AppSubTaskLink link = AppGlobals.Current.GetSubTask(sender);
                this.OnMainToolItem_Clicked(link);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private bool Validate(object sender)
        {
            if (sender.ToString().ToUpper() == "REPORTS")
            {
                //if (!_businessAdmin.RoleHasRightsAllReports(LoggedinUser.SecurityRoleID))
                //{
                //    MessageBox.Show("Sorry! No Reports assigned to the Role", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return false;
                //}
            }
            return true;
        }
        private void dgvActiveItems_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvActiveItems.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dgvActiveItems.SelectedRows[0];
                    AppSubTaskLink link = null;
                    if (selectedRow.Tag != null)
                    {
                        link = selectedRow.Tag as AppSubTaskLink;
                    }

                    if (link != null)
                    {
                        if (e.ColumnIndex == 0)
                        {
                            link.Close();
                        }
                        else
                        {
                            this.OnMainToolItem_Clicked(link);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        void OnMainToolItem_Clicked(AppSubTaskLink link)
        {
            try
            {
                if (link != null)
                {
                    ToolStripItem source = link.SourceItem as ToolStripItem;
                    switch (link.LinkType)
                    {
                        case ToolLinkType.InlineForm:
                            {
                                Form frmChild = link.Instance;

                                if (frmChild != null)
                                {
                                    try
                                    {
                                        this.SuspendLayout();
                                        frmChild.Activate();
                                        IControlActivator activator = frmChild as IControlActivator;
                                        if (activator != null)
                                        {
                                            activator.ActivateControl(link.InlineFormTag);
                                        }
                                    }
                                    finally
                                    {
                                        this.ResumeLayout();
                                    }
                                }
                                else
                                {
                                    try
                                    {
                                        this.SuspendLayout();
                                        frmChild = link.CreateForm(link.InlineFormTag);
                                        this.ShowMDIChildForm(frmChild, link);
                                        ////frmChild.AutoScaleMode = AutoScaleMode.None;
                                        ////frmChild.FormBorderStyle = FormBorderStyle.Sizable;
                                        //frmChild.MdiParent = this;
                                        //frmChild.ShowInTaskbar = false;
                                        //frmChild.Tag = link;
                                        //frmChild.Text = source.Text;
                                        //frmChild.Load += new EventHandler(OnChild_Load);
                                        //frmChild.FormClosing += new FormClosingEventHandler(OnChild_FormClosing);
                                        //link.Instance = frmChild;

                                        //frmChild.WindowState = FormWindowState.Normal;
                                        //frmChild.StartPosition = FormStartPosition.Manual;

                                        //frmChild.FormBorderStyle = FormBorderStyle.Sizable;
                                        //frmChild.MinimizeBox = false;
                                        //frmChild.MaximizeBox = true;
                                        //frmChild.ControlBox = false;
                                        //frmChild.AutoScrollMinSize = new Size(800, 600);
                                        //frmChild.AutoScroll = true;

                                        //if (_toolButtonIcons.ContainsKey(source))
                                        //{
                                        //    frmChild.Icon = _toolButtonIcons[source].Icon;
                                        //}

                                        //if (_controlWindows)
                                        //{
                                        //    this.ControlWindows(frmChild);
                                        //}

                                        //this.ModifySubTaskItem(link, false);
                                        //frmChild.Show();
                                    }
                                    finally
                                    {
                                        this.ResumeLayout(true);
                                    }
                                }

                                if (source is ToolStripButton)
                                {
                                    ((ToolStripButton)source).Checked = true;
                                }
                            }
                            break;

                        case ToolLinkType.ExternalExe:
                        case ToolLinkType.ExternalExeEmbedded:
                            {
                                link.ActiveExternalProcess();
                                //this.ModifySubTaskItem(link, tbrOpenedProcesses, false);
                            }
                            break;

                        case ToolLinkType.CustomAction:
                            {
                                try
                                {
                                    if (link.CustomAction != null)
                                    {
                                        link.CustomAction();
                                    }
                                }
                                catch (Exception ex)
                                {
                                    ExceptionManager.Publish(ex);
                                }
                            }
                            break;

                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                link.InlineFormTag = null;
            }
        }

        private void ShowMDIChildForm(Form frmChild, AppSubTaskLink link)
        {
            ModuleProc PROC = new ModuleProc("MainForm", "ShowMDIChildForm");

            try
            {
                ToolStripItem source = ((ToolStripItem)link.SourceItem);

                //frmChild.AutoScaleMode = AutoScaleMode.None;
                //frmChild.FormBorderStyle = FormBorderStyle.Sizable;
                //if (link.LinkType == ToolLinkType.InlineForm)
                //{
                //    frmChild.MdiParent = this;
                //}
                //else
                //{
                //    //// we cannot assign MDIParent from another app domain
                //    //try
                //    //{
                //    //    // MdiClient
                //    //    PropertyInfo pi = this.GetType().GetProperty("MdiClient", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                //    //    object mdiClient = pi.GetValue(this, null);

                //    //    // ParentInternal
                //    //    PropertyInfo pi2 = frmChild.GetType().GetProperty("ParentInternal", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                //    //    pi2.SetValue(frmChild, mdiClient, null);
                //    //}
                //    //catch (Exception ex)
                //    //{
                //    //    Log.Exception(PROC, ex);
                //    //}
                //}

                frmChild.MdiParent = this;
                frmChild.ShowInTaskbar = false;
                frmChild.Tag = link;
                frmChild.Text = source.Text;
                frmChild.Load += new EventHandler(OnChild_Load);
                frmChild.FormClosing += new FormClosingEventHandler(OnChild_FormClosing);
                link.Instance = frmChild;

                frmChild.WindowState = FormWindowState.Normal;
                frmChild.StartPosition = FormStartPosition.Manual;

                frmChild.FormBorderStyle = FormBorderStyle.Sizable;
                frmChild.MinimizeBox = false;
                frmChild.MaximizeBox = true;
                frmChild.ControlBox = false;
                frmChild.MinimumSize = new Size(0, 0);
                frmChild.MaximumSize = new Size(0, 0);
                frmChild.AutoScrollMinSize = new Size(800, 600);
                frmChild.AutoScroll = true;

                if (_toolButtonIcons.ContainsKey(source))
                {
                    frmChild.Icon = _toolButtonIcons[source].Icon;
                }

                if (_controlWindows)
                {
                    this.ControlWindows(frmChild);
                }

                this.ModifySubTaskItem(link, false);
                frmChild.Show();
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        private void ControlWindows(Form ownerForm)
        {
            ModuleProc PROC = new ModuleProc("MainForm", "ControlWindows");

            try
            {
                Padding pad = ownerForm.Padding;
                Control.ControlCollection controls = ownerForm.Controls;

                UxHeaderContent container = this.AddChildContainer(pad);
                container.ChildContainer.AutoScroll = true;
                container.HeaderText = ownerForm.Text;
                container.Tag = ownerForm;
                container.PinClick += new EventHandler(OnContainer_PinClick);

                if (controls != null)
                {
                    int length = controls.Count;
                    int i = length;
                    while (length > 0)
                    {
                        Control ctl = controls[0];
                        controls.RemoveAt(0);
                        container.ChildContainer.Controls.Add(ctl);
                        length--;
                    }

                    ownerForm.Padding = new Padding(3);
                    ownerForm.Controls.Add(container);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        protected override void OnMdiChildActivate(EventArgs e)
        {
            base.OnMdiChildActivate(e);
            if (this.ActiveMdiChild != null)
            {
                this.BringChildFormMaximized(this.ActiveMdiChild);
            }
        }
        void OnContainer_PinClick(object sender, EventArgs e)
        {
            ModuleProc PROC = new ModuleProc("MainForm", "OnContainer_PinClick");

            try
            {
                Form ownerForm = ((UxHeaderContent)sender).Tag as Form;
                ownerForm.Close();
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        private UxHeaderContent AddChildContainer(Padding padding)
        {
            UxHeaderContent container = new UxHeaderContent();
            container.ChildContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            container.ChildContainer.Padding = new System.Windows.Forms.Padding(3);
            container.ContentPadding = padding;
            container.Dock = System.Windows.Forms.DockStyle.Fill;
            container.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(149)))), ((int)(((byte)(192)))));
            container.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            container.PinVisible = true;
            container.BorderStyle = BorderStyle.None;
            container.IsClosable = true;
            container.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(64)))), ((int)(((byte)(114)))));
            return container;
        }

        private void ModifySubTaskItem(AppSubTaskLink source, bool isRemove)
        {
            try
            {
                ToolStripItem button = source.SourceItem as ToolStripItem;
                if (_toolButtonIcons.ContainsKey(button))
                {
                    ImageDetails details = _toolButtonIcons[button];
                    if (!isRemove && source.InstanceRow == null)
                    {
                        int rowIndex = dgvActiveItems.Rows.Add();
                        DataGridViewRow row = dgvActiveItems.Rows[rowIndex];
                        row.Cells[chdrActiveImage.Index].Value = details.Image;
                        row.Cells[chdrActiveClose.Index].Value = "X";
                        row.Cells[chdrActiveItem.Index].Value = button.Text;
                        source.InstanceRow = row;
                        row.Tag = source;
                    }
                    else if (isRemove &&
                        source.InstanceRow != null)
                    {
                        dgvActiveItems.Rows.Remove(source.InstanceRow);
                        source.InstanceRow = null;
                    }
                }
                dgvActiveItems.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                bool isVisible = (dgvActiveItems.Rows.Count > 0);
                uxActiveItems.Visible = isVisible;
                mnuFileSep1.Visible = isVisible;
                mnuFileCloseAll.Visible = isVisible;
                this.HandleHomeScreen((this.LoggedinUser != null) && !isVisible);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void BringChildFormMaximized(Form ownerForm)
        {
            ModuleProc PROC = new ModuleProc("MainForm", "BringChildFormMaximized");

            try
            {
                ownerForm.ControlBox = false;
                //ownerForm.WindowState = FormWindowState.Normal;
                ownerForm.WindowState = FormWindowState.Maximized;
                ownerForm.BringToFront();
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        void OnChild_Load(object sender, EventArgs e)
        {
            ModuleProc PROC = new ModuleProc("MainForm", "OnChild_Load");

            try
            {
                Form ownerForm = sender as Form;
                this.BringChildFormMaximized(ownerForm);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        void OnChild_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                // forms should not be closed on following scenarios
                // 1. when the user clicked the cancel button on the confirm message box
                // 2. during MDI parent form closing
                if (e.CloseReason == CloseReason.MdiFormClosing)
                {
                    e.Cancel = true;
                }
                if (e.Cancel) return;

                Form ownerForm = sender as Form;
                ownerForm.Load -= this.OnChild_Load;
                ownerForm.FormClosing -= this.OnChild_FormClosing;

                AppSubTaskLink link = ownerForm.Tag as AppSubTaskLink;
                if (link != null)
                {
                    string uniqueKey = ownerForm.Text;
                    if (link.SourceItem is ToolStripButton)
                    {
                        ToolStripButton button = ((ToolStripButton)link.SourceItem);
                        button.Checked = false;
                        if (!button.Name.IsEmpty())
                        {
                            uniqueKey = button.Name;
                        }
                    }
                    if (_homeScreenWidgets)
                    {
                        Form frmOwner = link.Instance;
                        _homeScreen.CaptureWidget(frmOwner, uniqueKey);
                    }
                    link.Instance = null;
                    this.ModifySubTaskItem(link, true);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        //void OnChild_Resize(object sender, EventArgs e)
        //{
        //    ((Form)sender).WindowState = FormWindowState.Maximized;
        //}

        void OnTask_ExternalProcessOpened(AppSubTaskLink link)
        {
            if (link.LinkType == ToolLinkType.ExternalExeEmbedded)
            {
                if (link.Activator.Metadata != null)
                {
                    if (!link.Activator.Metadata.NonMdiClient)
                    {
                        this.ShowMDIChildForm(link.Activator.EmbedForm, link);
                    }
                }
            }
        }

        void OnLink_ExternalProcessClosed(AppSubTaskLink link)
        {
            try
            {
                this.CrossThreadInvoke(new Action(() =>
                {
                    //this.ModifySubTaskItem(link, tbrOpenedProcesses, true);
                }));
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void OnMainToolCheckItem_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                ToolStripButton source = (ToolStripButton)sender;
                if (_mainToolItemChecked != null &&
                    _mainToolItemChecked != source)
                {
                    _mainToolItemChecked.Checked = false;
                }

                _childToolbars[source].Visible = source.Checked;
                _mainToolItemChecked = (source.Checked ? source : null);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private ToolStripMenuItem AddToolStripDropDownItem(ToolStripDropDownButton target, string text, Image image)
        {
            ToolStripMenuItem item = null;
            try
            {
                item = new ToolStripMenuItem(text, image);
                target.DropDownItems.Add(item);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return item;
        }

        private void InitComplete()
        {
            ModuleProc PROC = new ModuleProc("MainForm", "InitComplete");

            try
            {
                if (_homeScreenWidgets)
                {
                    //this.BackgroundImage = null;
                    AppGlobals.Current.CleanupCompleted += new EventHandler(OnAppGlobals_CleanupCompleted);
                    _homeScreen = new HomeScreen();
                    _homeScreen.WidgetLoad += new EventHandler<WidgetEventArgs>(OnHomeScreen_WidgetLoad);
                    _homeScreen.WidgetClick += new EventHandler<WidgetEventArgs>(OnHomeScreen_WidgetClick);
                    _homeScreen.MdiParent = this;
                    _homeScreen.FormBorderStyle = FormBorderStyle.Sizable;
                    _homeScreen.WindowState = FormWindowState.Normal;
                    this.HandleHomeScreen(false);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        void OnHomeScreen_WidgetClick(object sender, WidgetEventArgs e)
        {
            BMC.CoreLib.Win32.HomeScreenWidgets.WidgetDetails widget = e.Widget;
            if (widget != null &&
                _widgetMappings.ContainsKey(widget.UniqueKey))
            {
                AppSubTaskLink link = AppGlobals.Current.GetSubTask(_widgetMappings[widget.UniqueKey]);
                this.OnMainToolItem_Clicked(link);
            }
        }

        void OnHomeScreen_WidgetLoad(object sender, WidgetEventArgs e)
        {
            BMC.CoreLib.Win32.HomeScreenWidgets.WidgetDetails widget = e.Widget;

        }

        void OnAppGlobals_CleanupCompleted(object sender, EventArgs e)
        {
            this.HandleHomeScreen((this.LoggedinUser != null));
        }

        public void InvokeInlineForm(string name, object input)
        {
            if (_toolMappings.ContainsKey(name))
            {
                AppSubTaskLink link = AppGlobals.Current.GetSubTask(_toolMappings[name]);
                link.InlineFormTag = input;
                this.OnMainToolItem_Clicked(link);
            }
        }

        private void InitializeSubTasks()
        {
            ModuleProc PROC = new ModuleProc("MainForm", "InitializeSubTasks");

            try
            {
                this.InitializeServices();
                Extensions.CreateThreadAndStart(new ThreadStart(this.InitializeNetwork));
                Extensions.CreateThreadAndStart(new ThreadStart(this.InitializeDatabase));
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        private void InitializeServices()
        {
            try
            {
                List<string> serviceNames = _businessAdmin.GetWindowsServiceNames();
                foreach (string serviceName in serviceNames)
                {
                    ToolStripMenuItem item = this.AddToolStripDropDownItem(sbrItemServices, serviceName, RES.ICO_SRV_OK);
                    item.ImageTransparentColor = Color.Black;
                    //item.Tag = new BMC.CoreLib.WMI.Win32.Service(
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void InitializeNetwork()
        {
            try
            {
                System.Net.NetworkInformation.NetworkChange.NetworkAvailabilityChanged += new System.Net.NetworkInformation.NetworkAvailabilityChangedEventHandler(NetworkChange_NetworkAvailabilityChanged);
                int timeout = AppGlobals.Current.NETWORK_CHECK_TIMEOUT;

                while (!_executor.WaitForShutdown(timeout))
                {
                    try
                    {
                        this.ChangeNetworkIcon(System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable());
                    }
                    catch { }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        void NetworkChange_NetworkAvailabilityChanged(object sender, System.Net.NetworkInformation.NetworkAvailabilityEventArgs e)
        {
            this.ChangeNetworkIcon(e.IsAvailable);
        }

        private void ChangeNetworkIcon(bool isOK)
        {
            if ((_isNetworkOK == null) ||
                (_isNetworkOK.Value != isOK))
            {
                try
                {
                    this.CrossThreadInvoke(new Action(() =>
                    {
                        sbrItemNetwork.Image = (isOK ? RES.ICO_NET_OK : RES.ICO_NET_NO);
                    }));
                }
                catch { }
                _isNetworkOK = isOK;
            }
        }

        private void InitializeDatabase()
        {
            try
            {
                int timeout = AppGlobals.Current.DB_CHECK_TIMEOUT;
                string connectionString = DatabaseHelper.GetConnectionString();
                SqlConnection connection = null;
                ConnectionState state = ConnectionState.Closed;

                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);
                builder.ConnectTimeout = 10;
                connectionString = builder.ToString();

                SqlCommand cmd = new SqlCommand("SELECT 1");
                bool isInit = true;

                while (isInit || !_executor.WaitForShutdown(timeout))
                {
                    try
                    {
                        state = ConnectionState.Closed;

                        if (connection == null)
                        {
                            connection = new SqlConnection(connectionString);
                            cmd.Connection = connection;
                        }
                        if (connection.State != ConnectionState.Open)
                        {
                            connection.Open();
                        }

                        cmd.ExecuteNonQuery();
                        state = connection.State;
                    }
                    catch (Exception ex)
                    {
                        if (connection != null)
                        {
                            if (ex.GetType() == typeof(ObjectDisposedException))
                            {

                                try
                                {
                                    connection.Dispose();
                                }
                                catch { }
                                connection = null;
                            }
                            else
                            {
                                try
                                {
                                    connection.Close();
                                }
                                catch { }
                            }
                        }
                        if (connection != null)
                        {
                            state = connection.State;
                        }
                    }
                    finally
                    {
                        this.ChangeDatabaseIcon(state == ConnectionState.Open);
                        if (isInit) isInit = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void ChangeDatabaseIcon(bool isOK)
        {
            if ((_isDatabaseOK == null) ||
                (_isDatabaseOK.Value != isOK))
            {
                try
                {
                    this.CrossThreadInvoke(new Action(() =>
                    {
                        sbrItemDatabase.Image = (isOK ? RES.ICO_DB_OK1 : RES.ICO_DB_NO1);
                    }));
                }
                catch { }

                _isDatabaseOK = isOK;
            }
        }
        public UserEntity LoggedinUser
        {
            get { return AppGlobals.Current.LoggedinUser; }
            set
            {
                AppGlobals.Current.LoggedinUser = value;
                bool isOK = (value != null);
                bool isToolbar = !AppSettings.Current.HideToolbar;
                bool isStatusbar = !AppSettings.Current.HideStatusbar;

                mnuViewToolbar.Checked = isToolbar;
                mnuViewStatusbar.Checked = isStatusbar;
                sbrItemUserDetails.Visible = isOK;
                mnuFileSep1.Visible = false;
                mnuFileCloseAll.Visible = false;
                uxActiveItems.Visible = false;
                uxActiveItems.IsHidden = true;

                this.OnViewToolbarChecked();
                this.OnViewStatusbarChecked();

                this.CleanupToolbars();
                this.HandleHomeScreen((value != null));
                if (_homeScreenWidgets)
                {
                    _homeScreen.UserName = string.Empty;
                    if (value != null)
                    {
                        _homeScreen.UserName = value.UserName.ToLower();
                        _homeScreen.LoadWidgets();
                    }
                }

                if (isOK)
                {
                    sbrItemUserDetails.Text = value.UserName + " [" + value.WindowsUserName + "]";
                    this.LoadInitialSettings();
                    AppGlobals.Current.PerformUserAccess();
                    EnableDisableUserMenu();
                }
            }
        }

        private void EnableDisableUserMenu()
        {
            tbrItemAdmin.Visible = AppGlobals.Current.HasUserAccess("HQ_Admin");
            tbrItemServiceCalls.Visible = isServiceCallEnabled && AppGlobals.Current.HasUserAccess("HQ_Engineers");
            tbrItemMonitoring.Visible = AppGlobals.Current.HasUserAccess("HQ_Engineers");
            tbrItemFinancial.Visible = isFinancialEnabled && (AppGlobals.Current.HasUserAccess("HQ_Financial") && SettingsEntity.LiquidationProfitShare);
            tbrItemMonitoring.Visible = AppGlobals.Current.HasUserAccess("HQ_AuditViewer");
            tbrItemCreateServiceCall.Visible = AppGlobals.Current.HasUserAccess("HQ_Engineers_CreateCall");
            tbrItemCurrentServiceCalls.Visible = AppGlobals.Current.HasUserAccess("HQ_Engineers_Current");
            tbrItemClosedServiceCalls.Visible = AppGlobals.Current.HasUserAccess("HQ_Engineers_Closed");
            tbrItemServiceAdmin.Visible = AppGlobals.Current.HasUserAccess("HQ_Engineers_Engineers");
        }

        private void HandleHomeScreen(bool visible)
        {
            if (!_homeScreenWidgets) return;
            ModuleProc PROC = new ModuleProc("MainForm", "HandleHomeScreen");
            this.SuspendLayout();

            try
            {
                if (visible)
                {
                    _homeScreen.Show();
                    this.BringChildFormMaximized(_homeScreen);
                }
                else
                {
                    _homeScreen.Hide();
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
            finally
            {
                this.ResumeLayout(true);
            }
        }

        private void CleanupToolbars()
        {
            try
            {
                foreach (ToolStripButton button in _childToolbars.Keys)
                {
                    if (button.Checked)
                        button.Checked = false;
                }
                dgvActiveItems.Rows.Clear();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void Signout()
        {
            try
            {
                AppGlobals.Current.Signout();
                this.LoggedinUser = null;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            this.ShowLoginForm();
        }

        private void ShowLoginForm()
        {
            AuthenticationResult result = AuthenticationResult.Unauthenticated;

            try
            {
                BMC.CoreLib.Win32.Win32Extensions.ShowDialogExResultAndDestroy<LoginForm>(new LoginForm(), this,
                       null,
                       (f) =>
                       {
                           result = f.AuthenticationResult;
                           if (result == AuthenticationResult.Authenticated)
                           {
                               this.LoggedinUser = f.User;
                           }
                       });
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                if ((result == AuthenticationResult.FirstLoginSinceReset)
                    || (result == AuthenticationResult.PasswordExpired))
                {
                    this.ShowChangePasswordForm(result);
                }
            }
        }

        private void ShowChangePasswordForm(AuthenticationResult lastAuthenticationResult)
        {
            try
            {
                BMC.CoreLib.Win32.Win32Extensions.ShowDialogExResultAndDestroy<ChangePasswordForm>(new ChangePasswordForm(_bmcSecurityMethod, lastAuthenticationResult), this,
                       null,
                       (f) =>
                       {
                           //this.LoggedinUser = f.User;
                       });
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.ResolveResources();
            this.uxActiveItems.HeaderText = this.GetResourceTextByKey("Key_ActiveItems");
            this.LoggedinUser = null;
            string connectionstring = "";
            _isconfigured = false;
            try
            {
                connectionstring = DatabaseHelper.GetConnectionString();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            if (String.IsNullOrEmpty(connectionstring))
            {
                BMC.CoreLib.Win32.Win32Extensions.ShowErrorMessageBox(this, this.GetResourceTextByKey(1, "MSG_CONFIGURE_APP"), this.Text);
                bool ent_client = (Application.StartupPath.IndexOf("Enterprise Client") != -1);
                bool ent_server = (Application.StartupPath.IndexOf("Enterprise Server") != -1);
                if (ent_client || ent_server)
                {
                    string exe_path = Application.StartupPath + (ent_client ? @"\BMCEnterpriseClientConfig.exe" : @"\BMCEnterpriseConfig.exe");
                    ProcessStartInfo psi = new ProcessStartInfo(exe_path);
                    Process ps = new Process();
                    ps.StartInfo = psi;
                    ps.Start();
                }
                this.Close();
            }
            else
            {
                _isconfigured = true;
                this.ShowLoginForm();
            }

            ShowNotificationsOnLogin();
        }

        private void ShowNotificationsOnLogin()
        {
            try
            {
                if (Convert.ToBoolean(ConfigurationManager.AppSettings["ShowNotificationsOnLogin"]))
                {
                    if (_iNotificationCount > 0)
                    {
                        try
                        {
                            frmNotifications objNotify = new frmNotifications();
                            objNotify.ShowDialog();
                        }
                        catch (Exception ex)
                        {
                            ExceptionManager.Publish(ex);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (!_isconfigured || BMC.CoreLib.Win32.Win32Extensions.ShowQuestionMessageBox(this, this.GetResourceTextByKey(1, "MSP_APP_EXIT"), this.Text) == DialogResult.Yes)
                {
                    e.Cancel = false;
                    this.Signout();
                    _executor.AwaitTermination(new TimeSpan(0, 0, 10));
                }
                else
                {
                    e.Cancel = true;
                }
                AppSettings.Current.HideToolbar = !mnuViewToolbar.Checked;
                AppSettings.Current.HideStatusbar = !mnuViewStatusbar.Checked;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }

        private void LayoutChildren(MdiLayout layout)
        {
            try
            {
                this.LayoutMdi(layout);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void mnuFileCloseAll_Click(object sender, EventArgs e)
        {
            try
            {
                AppGlobals.Current.CleanupTasks();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void tbrItemLogout_Click(object sender, EventArgs e)
        {
            if (BMC.CoreLib.Win32.Win32Extensions.ShowQuestionMessageBox(this, this.GetResourceTextByKey(1, "MSG_CONFIRM_LOGOUT"), this.Text) == System.Windows.Forms.DialogResult.No) return;
            this.Signout();
        }

        private void mnuHelpAbout_Click(object sender, EventArgs e)
        {
            BMC.CoreLib.Win32.Win32Extensions.ShowDialogExResultAndDestroy(
                new AboutForm(AboutFormTypes.About), this);
        }

        private void mnuFileExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mnuViewToolbar_CheckedChanged(object sender, EventArgs e)
        {
            this.OnViewToolbarChecked();
        }

        private void OnViewToolbarChecked()
        {
            bool isToolbar = mnuViewToolbar.Checked;
            tbrNoLogin.Visible = (this.LoggedinUser == null && isToolbar);
            tbrMain.Visible = (this.LoggedinUser != null && isToolbar);
        }

        private void mnuViewStatusbar_CheckedChanged(object sender, EventArgs e)
        {
            this.OnViewStatusbarChecked();
        }

        private void OnViewStatusbarChecked()
        {
            sbarMain.Visible = mnuViewStatusbar.Checked;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                DateTime dtNow = DateTime.Now;
                this.sbrDateTime.Text = dtNow.ToString();
                this.sbrCaps.Enabled = (Control.IsKeyLocked(Keys.CapsLock)) ? true : false;
                this.sbrNum.Enabled = (Control.IsKeyLocked(Keys.NumLock)) ? true : false;
                _iNotificationCount = objBusiness.GetNotificationsCount();
                sbr_Notitfications.Text = _iNotificationCount.ToString();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }

        private void sbr_Notitfications_Click(object sender, EventArgs e)
        {
            try
            {
                frmNotifications objNotify = new frmNotifications();
                objNotify.ShowDialog();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
    }
}
