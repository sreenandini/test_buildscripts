using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.EnterpriseBusiness.Entities;

namespace BMC.EnterpriseClient.Views
{
    public interface IAdminSite
    {
        void LoadDetails(AdminSiteEntity entity);
        bool SaveDetails(AdminSiteEntity entity);
    }

    public interface IUserControl
    {
        bool IsControlInitialized { get; set; }        

        void LoadControl();
        bool SaveControl();
    }

    public interface IUserControl2 : IUserControl
    {
        void ClearControl();
    }

    public interface IUserControl3 : IUserControl
    {
        void Activated();
        void DeActivated();
    }

    public interface IControlActivator
    {
        void ActivateControl(object input);
    }
}
