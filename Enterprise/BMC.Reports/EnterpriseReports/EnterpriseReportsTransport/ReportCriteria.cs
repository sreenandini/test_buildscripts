using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.EnterpriseReportsTransport
{
  public  class ReportCriteria
    {
      public string Company
      { get; set; }
      public string SubCompany
      { get; set; }
      public string Region
      { get; set; }
      public string Area
      { get; set; }
      public string District
      { get; set; }
      public string Site
      { get; set; }
      public string Category
      { get; set; }
      public string OrderBy
      { get; set; }
      public string FromDate
      { get; set; }
      public string ToDate
      { get; set; }
      public string FromDateTime
      { get; set; }
      public string ToDateTime
      { get; set; }
      public bool HideZeroVarianceCollections
      {get;set;}
    }
}
