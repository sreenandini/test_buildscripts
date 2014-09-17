using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Simulator.Models
{
    public abstract class CardInfoModel
    {
        // to avoid wpf data binding issue
        internal bool _isPropertyModified = false;

        public long RowNo { get; set; }

        public int HashCode { get; set; }

        public string CardNo { get; set; }

        public bool IsSelected { get; set; }

        public long GmuRowNo { get; set; }

        public string DisplayText { get; set; }
    }

    public class PlayerCardInfoModel : CardInfoModel { }

    public class PlayerCardInfoModelCollection : ObservableCollection<PlayerCardInfoModel> { }

    public class EmployeeCardInfoModel : CardInfoModel { }

    public class EmployeeCardInfoModelCollection : ObservableCollection<EmployeeCardInfoModel> { }
}
