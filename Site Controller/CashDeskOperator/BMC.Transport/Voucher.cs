using System;

namespace BMC.Transport
{
    public class Voucher
    {
        public string PrintDevice { get; set; }
        public string PayDevice { get; set; }
        public string PrintDeviceName { get; set; }
        public string SBarCode { get; set; }
        public long Amount { get; set; }
        public string VoucherStatus { get; set; }
        public DateTime Datepaid { get; set; }
        public DateTime DatePrinted { get; set; }
        public string DeviceType { get; set; }
        public string BarPositionName { get; set; }
        public int InstallationNo { get; set; }
        public string StockNo { get; set; }
        public string PrintDeviceSerial { get; set; }
        public string SiteName { get; set; }
        public string SiteCode { get; set; }
        public string FullTicketNumber { get; set; }
        public double Value { get; set; }
        public string Header { get; set; }
        public int TicketType { get; set; }
    }
}
