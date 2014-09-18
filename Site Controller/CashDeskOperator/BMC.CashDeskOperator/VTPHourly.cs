using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BMC.Transport.CashDeskOperatorEntity;
using BMC.Business.CashDeskOperator;


namespace BMC.CashDeskOperator.BusinessObjects
{
    public class HourlyBusinessObject : IHourly
    {

        public HourlyBusinessObject()
        { }
        public BMC.Business.CashDeskOperator.HourlyDetails hourlyDetails = new BMC.Business.CashDeskOperator.HourlyDetails();
        public DataTable GetInstallationDetails()
        { 
          return hourlyDetails.GetInstallationDetails();
        }
        public DataTable GetSiteName()
        {
            return hourlyDetails.GetSiteName();
        }
        public DataTable GetMachineTypeDetails()
        {
            return hourlyDetails.GetMachineTypeDetails();
        }
        public DataTable GetZones()
        {
            return hourlyDetails.GetZoneDetails();
        }
        public DataTable GetPositions()
        {
            return hourlyDetails.GetPositionDetails();
        }
        public DataTable GetHSTypes()
        {
            return hourlyDetails.GetHSTypes();
        }
        public  HourlyDetailsEntity GetHourlyStatistics(int startHour, int rows, string dataType, int? category,
             int? zone, int? position, DateTime? date,  bool isCalenderDay)
        {
           return  hourlyDetails.GetHourlyStatistics(startHour,  rows,  dataType,  category,
              zone,  position,  date,  isCalenderDay);
        }
    }
}
