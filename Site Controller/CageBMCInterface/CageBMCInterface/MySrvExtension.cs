using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.ServiceModel.Description;
using System.ServiceModel.Configuration;
using System.ServiceModel.Dispatcher;

namespace CageBMCInterface
{
    public class MySrvExtension : WCFHelperUtils.InteropServiceExtension
    {
        protected override object CreateBehavior()
        {
            return new MySrvExtension();
        }

        protected override void OnIterateEndpointDispatcher(EndpointDispatcher dispatcher)
        {
            dispatcher.DispatchRuntime.MessageInspectors.Add(new ServiceInspector());
        }
    }
}
