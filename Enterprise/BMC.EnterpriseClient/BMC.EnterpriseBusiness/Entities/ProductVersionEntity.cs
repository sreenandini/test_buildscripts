using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib;

namespace BMC.EnterpriseBusiness.Entities
{
    public class ProductVersionEntity : DisposableObject
    {
        public ProductVersionEntity() { }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Company { get; set; }
        public string Copyright { get; set; }
        public string Version { get; set; }
    }
}
