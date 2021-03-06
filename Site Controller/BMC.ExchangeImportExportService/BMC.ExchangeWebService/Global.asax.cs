﻿using BMC.Common.LogManagement;
using BMC.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace BMC.ExchangeWebService
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            LogManager.WriteLog("Entering Exchange Web Service", LogManager.enumLogLevel.Debug);
            BMCRegistryHelper.ActiveInstallationType = BMCCategorizedInstallationTypes.Exchange;
            LogManager.WriteLog("BMCRegistryHelper.InstallationType is :" + BMCRegistryHelper.InstallationType, LogManager.enumLogLevel.Debug);
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");

            if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
            {
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods",
                "GET, POST");
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers",
                "Content-Type, Accept");
                HttpContext.Current.Response.AddHeader("Access-Control-Max-Age",
                "1728000");
                HttpContext.Current.Response.End();
            }
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}