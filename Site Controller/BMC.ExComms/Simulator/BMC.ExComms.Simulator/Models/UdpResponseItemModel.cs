using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using BMC.CoreLib;
using BMC.CoreLib.Collections;

namespace BMC.ExComms.Simulator.Models
{
    public class UdpRawMessageItemModel
    {
        public int SNo { get; set; }

        public string IPAddress { get; set; }

        public DateTime ProcessedTime { get; set; }

        public string SessionId { get; set; }

        public int TransactionId { get; set; }

        public string TargetName { get; set; }

        public string RawDataInHex { get; set; }
    }

    public class UdpRawRequestItemModel : UdpRawMessageItemModel { }

    public class UdpRawRequestItemModelCollection : ObservableCollection<UdpRawRequestItemModel> { }

    public class UdpRawRequestItemModelCollectionByGmu : StringDictionary<UdpRawRequestItemModelCollection>
    {
        public void AddItem(UdpRawRequestItemModel model)
        {
            this[model].Add(model);
        }

        public UdpRawRequestItemModelCollection this[UdpRawRequestItemModel model]
        {
            get
            {
                UdpRawRequestItemModelCollection collection = null;
                string key = model.IPAddress;

                if (!this.ContainsKey(key))
                {
                    this.Add(key, (collection = new UdpRawRequestItemModelCollection()));
                }
                else
                {
                    collection = this[key];
                }
                return collection;
            }
        }
    }

    public class UdpRawResponseItemModel : UdpRawMessageItemModel { }

    public class UdpRawResponseItemModelCollection : ObservableCollection<UdpRawResponseItemModel> { }

    public class UdpRawResponseItemModelCollectionByGmu : StringDictionary<UdpRawResponseItemModelCollection>
    {
        public void AddItem(UdpRawResponseItemModel model)
        {
            this[model].Add(model);
        }

        public UdpRawResponseItemModelCollection this[UdpRawResponseItemModel model]
        {
            get
            {
                UdpRawResponseItemModelCollection collection = null;
                string key = model.IPAddress;

                if (!this.ContainsKey(key))
                {
                    this.Add(key, (collection = new UdpRawResponseItemModelCollection()));
                }
                else
                {
                    collection = this[key];
                }
                return collection;
            }
        }
    }
}
