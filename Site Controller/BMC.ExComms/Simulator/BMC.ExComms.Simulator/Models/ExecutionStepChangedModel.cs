using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Simulator.Models
{
    public class ExecutionStepChangedModel
    {
        public string GmuIpAddress { get; set; }

        public List<string> Steps { get; set; }
    }

    public class ExecutionStepChangedModelCollection : ObservableCollection<ExecutionStepChangedModel> { }
}
