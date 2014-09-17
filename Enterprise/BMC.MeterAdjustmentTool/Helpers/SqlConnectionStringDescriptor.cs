using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace BMC.MeterAdjustmentTool.Helpers
{
    public class SqlConnectionStringDescriptor : PropertyDescriptor {
        private Type _componentType; 
        private Type _propertyType; 
        private bool _isReadOnly;
        private bool _refreshOnChange; 

        internal SqlConnectionStringDescriptor(string propertyName, Type propertyType, bool isReadOnly, Attribute[] attributes)
            : base(propertyName, attributes) {
            //Bid.Trace("<comm.SqlConnectionStringDescriptor|INFO> propertyName='%ls', propertyType='%ls'\n", propertyName, propertyType.Name);
            _componentType = typeof(BmcConnectionStringBuilder); 
            _propertyType = propertyType;
            _isReadOnly = isReadOnly; 
        } 

        internal bool RefreshOnChange { 
            get {
                return _refreshOnChange;
            }
            set { 
                _refreshOnChange = value;
            } 
        } 

        public override Type ComponentType { 
            get {
                return _componentType;
            }
        } 
        public override bool IsReadOnly {
            get { 
                return _isReadOnly; 
            }
        } 
        public override Type PropertyType {
            get {
                return _propertyType;
            } 
        }

        public override bool CanResetValue(object component)
        {
            BmcConnectionStringBuilder builder = (component as BmcConnectionStringBuilder);
            return ((null != builder) && builder.ShouldSerialize(DisplayName));
        }

        public override object GetValue(object component)
        {
            BmcConnectionStringBuilder builder = (component as BmcConnectionStringBuilder);
            if (null != builder)
            {
                object value;
                if (builder.TryGetValue(DisplayName, out value))
                {
                    return value;
                }
            }
            return null; 
        }

        public override void ResetValue(object component)
        {
            BmcConnectionStringBuilder builder = (component as BmcConnectionStringBuilder);
            if (null != builder)
            {
               // builder.Remove(DisplayName);

                if (RefreshOnChange)
                {
                }
            }
        }

        public override void SetValue(object component, object value)
        {
            BmcConnectionStringBuilder builder = (component as BmcConnectionStringBuilder);
            if (null != builder)
            {
                // via the editor, empty string does a defacto Reset 
                if ((typeof(string) == PropertyType) && String.Empty.Equals(value))
                {
                    value = null;
                }
                builder[DisplayName] = value;

                if (RefreshOnChange)
                {
                }
            }
        }

        public override bool ShouldSerializeValue(object component)
        {
            BmcConnectionStringBuilder builder = (component as BmcConnectionStringBuilder);
            return ((null != builder) && builder.ShouldSerialize(DisplayName));
        }
    }
}
