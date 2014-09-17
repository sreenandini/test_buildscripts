using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using BMC.CoreLib;

namespace BMC.ExComms.Simulator.Models
{
    public class ExceptionModel 
    {
        public int SNo { get; set; }

        public DateTime ReceivedTime { get; set; }

        public string Exception { get; set; }
    }

    public class ExceptionModelCollection : ObservableCollection<ExceptionModel> { }
}
