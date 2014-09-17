using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using BMC.ExComms.Contracts.DTO.Freeform;

namespace BMC.ExComms.Simulator.Models
{
    public class TicketTypeModel
    {
        public TicketTypeModel(FF_AppId_TicketTypes type)
        {
            this.Type = type;
        }
        public FF_AppId_TicketTypes Type { get; set; }
    }

    public class TicketTypeModelCollection : ObservableCollection<TicketTypeModel>
    {
        public TicketTypeModelCollection()
        {
            this.Add(new TicketTypeModel(FF_AppId_TicketTypes.Cashable));
            this.Add(new TicketTypeModel(FF_AppId_TicketTypes.NonCashable));
            this.Add(new TicketTypeModel(FF_AppId_TicketTypes.CashablePromo));
        }
    }

    public class TicketInfoModel
    {
        public int SNo { get; set; }

        public string Barcode { get; set; }

        public double Amount { get; set; }

        public FF_AppId_TicketTypes TicketType { get; set; }

        public string ProcessType { get; set; }
    }

    public class TicketInfoModelCollection : ObservableCollection<TicketInfoModel>
    {
        public void AddWithSNo(TicketInfoModel model)
        {
            model.SNo = (this.Count + 1);
            this.Add(model);
        }
    }
}
