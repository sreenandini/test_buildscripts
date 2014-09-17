using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib;

namespace BMC.EnterpriseBusiness.Entities
{
    public class UserAccessEntity : DisposableObject
    {
        public UserAccessEntity() { }

        public UserAccessEntity(string name, bool value)
        {
            this.Name = name;
            this.Value = value;
        }

        public string Name { get; set; }

        public bool Value { get; set; }
    }
}
