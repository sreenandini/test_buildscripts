using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.EnterpriseBusiness.Business;
using BMC.Common;

namespace BMC.EnterpriseClient.Views
{
    public static class ProfitShareFunctional
    {
        private static IDictionary<CommonProfitShareType, string> _groupListCaptions =
            new Dictionary<CommonProfitShareType, string>()
            {
                { CommonProfitShareType.ProfitShare, BMC.Common.ResourceExtensions.GetResourceTextByKey(null,"Key_ProfitShare_ProfitShareGroups")},//"Profit Share Groups" },
                { CommonProfitShareType.ExpenseShare, BMC.Common.ResourceExtensions.GetResourceTextByKey(null,"Key_ProfitShare_ExpenseShareGroups")},//"Expense Share Groups" },
            };

        private static IDictionary<CommonProfitShareType, string> _groupEditCaptions =
            new Dictionary<CommonProfitShareType, string>()
            {
                { CommonProfitShareType.ProfitShare, BMC.Common.ResourceExtensions.GetResourceTextByKey(null,"Key_ProfitShare_ProfitShareGroups")},//"Profit Share Group" },
                { CommonProfitShareType.ExpenseShare, BMC.Common.ResourceExtensions.GetResourceTextByKey(null,"Key_ProfitShare_ExpenseShareGroups")},//"Expense Share Group" },
            };

        private static IDictionary<CommonProfitShareType, string> _itemEditCaptions =
            new Dictionary<CommonProfitShareType, string>()
            {
                { CommonProfitShareType.ProfitShare, BMC.Common.ResourceExtensions.GetResourceTextByKey(null,"Key_ProfitShare_ProfitShareGroups")},//"Profit Share" },
                { CommonProfitShareType.ExpenseShare, BMC.Common.ResourceExtensions.GetResourceTextByKey(null,"Key_ProfitShare_ExpenseShareGroups")},//"Expense Share" },
            };

        public static string GetCaption(FormEditTypes editType, FormGroupTypes groupType, CommonProfitShareType functionalType)
        {
            string result = string.Empty;
            switch (editType)
            {
                case FormEditTypes.Add:
                    result = ResourceExtensions.GetResourceTextByKey(null,"Key_Add");
                    switch (groupType)
                    {
                        case FormGroupTypes.Group:
                            result += _groupEditCaptions[functionalType];
                            break;

                        default:
                            result += _itemEditCaptions[functionalType];
                            break;
                    }                    
                    break;

                case FormEditTypes.Edit:
                    result = ResourceExtensions.GetResourceTextByKey(null, "Key_Edit_WOShortCut");
                    switch (groupType)
                    {
                        case FormGroupTypes.Group:
                            result += _groupEditCaptions[functionalType];
                            break;

                        default:
                            result += _itemEditCaptions[functionalType];
                            break;
                    }
                    break;

                default:
                    result = _groupListCaptions[functionalType];
                    break;
            }
            return result;
        }

        public static string GetCaptionForHeader(FormEditTypes editType, FormGroupTypes groupType, CommonProfitShareType functionalType)
        {
            string result = string.Empty;
            switch (editType)
            {
                case FormEditTypes.Add:
                    switch (groupType)
                    {
                        case FormGroupTypes.Group:
                            result += _groupEditCaptions[functionalType];
                            break;

                        default:
                            result += _itemEditCaptions[functionalType];
                            break;
                    }
                    break;

                case FormEditTypes.Edit:
                    switch (groupType)
                    {
                        case FormGroupTypes.Group:
                            result += _groupEditCaptions[functionalType];
                            break;

                        default:
                            result += _itemEditCaptions[functionalType];
                            break;
                    }
                    break;

                default:
                    result = _groupListCaptions[functionalType];
                    break;
            }
            return result;
        }
    }
}
