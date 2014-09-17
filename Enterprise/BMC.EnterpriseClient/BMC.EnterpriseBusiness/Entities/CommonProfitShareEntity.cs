using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace BMC.EnterpriseBusiness.Entities
{
    public partial class CommonProfitShareEntity
    {
        public CommonProfitShareEntity() { }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public int Id { get; set; }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public ShareHolderEntity ShareHolder { get; set; }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public CommonProfitShareGroupEntity Parent { get; set; }

        [DisplayName("Share Holder")]
        public string ShareHolderName
        {
            get
            {
                if (this.ShareHolder != null) return this.ShareHolder.Name;
                return string.Empty;
            }
        }

        public double Percentage { get; set; }
        public string Description { get; set; }
    }
}
