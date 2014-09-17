using System;
using System.Collections.Generic;
using System.Text;

namespace BMC.EnterpriseReportsTransport
{
    public class ListItem
    {
        private string str_Item = "";
        private int i_ItemID;

        public ListItem(int itemid, string itemvalue)
        {
            GetItemID = itemid;
            GetItemValue = itemvalue;
        }

        public int GetItemID
        {
            get
            {
                return i_ItemID;
            }
            set
            {
                i_ItemID = value;
            }
        }

        public string GetItemValue
        {
            get
            {
                return str_Item;
            }
            set
            {
                str_Item = value;
            }
        }


        public override string ToString()
        {
            return str_Item;
        }

    }
}