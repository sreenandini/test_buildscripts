using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib;
using BMC.CoreLib.Collections;
using BMC.ExComms.Contracts.Native;

namespace BMC.ExComms.Server.Handlers.Tickets
{
    internal sealed class TicketGlobal
        : DisposableObject
    {
        private int _ticketNumber = 0;

        internal TicketGlobal()
        {
            this.TicketKey = new byte[SDGTicketGenerator.TK_KEY_LENGTH];
        }

        public int TicketNumber
        {
            get { return _ticketNumber; }
            set
            {
                _ticketNumber = Math.Max(1, Math.Min(10000, value));
            }
        }

        public DateTime PrintDate { get; set; }
        public int ExpiryDays { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int SlotID { get; set; }
        public TimeSpan TimeOfDay { get; set; }
        public byte[] TicketKey { get; private set; }
    }

    internal sealed class TicketGlobalCollection
        : StringConcurrentDictionary<TicketGlobal>
    {
        private TicketGlobal Get(string key)
        {
            return this.GetOrAdd(key, (k) => { return new TicketGlobal(); });
        }

        public TicketGlobal UpdateTicketNumber(string key, int ticketNumber)
        {
            TicketGlobal tk = this.Get(key);
            tk.TicketNumber = ticketNumber;
            return tk;
        }

        public TicketGlobal UpdatePrintDate(string key, DateTime printDate)
        {
            TicketGlobal tk = this.Get(key);
            tk.PrintDate = printDate;
            return tk;
        }

        public TicketGlobal UpdateExpiryDate(string key, int expiryDays, DateTime expiryDate)
        {
            TicketGlobal tk = this.Get(key);
            tk.ExpiryDays = expiryDays;
            tk.ExpiryDate = expiryDate;
            return tk;
        }

        public TicketGlobal UpdateTicketSlotID(string key, int slotID)
        {
            TicketGlobal tk = this.Get(key);
            tk.SlotID = slotID;
            return tk;
        }

        public TicketGlobal UpdateTimeOfDay(string key, TimeSpan timeOfDay)
        {
            TicketGlobal tk = this.Get(key);
            tk.TimeOfDay = timeOfDay;
            return tk;
        }
    }
}
