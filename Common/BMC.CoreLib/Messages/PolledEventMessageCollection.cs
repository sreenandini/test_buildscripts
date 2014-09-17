using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib.Collections;

namespace BMC.CoreLib.Messages
{
    public class PolledEventMessageCollection
        : SynchronizedDictionary<int, PolledEventMessage>
    {
        internal PolledEventMessageCollection()
            : base(new GenericComparer<int>()) { }

        public void Add(PolledEventMessage message)
        {
            this.Add(message.InstallationNo, message);
        }

        public override PolledEventMessage this[int key]
        {
            get
            {
                if (this.ContainsKey(key)) return base[key];
                return null;
            }
            set
            {
                base[key] = value;
            }
        }
    }
}
