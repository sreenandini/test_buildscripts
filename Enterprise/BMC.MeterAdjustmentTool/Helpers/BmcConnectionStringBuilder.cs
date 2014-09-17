using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data;
using BMC.Common.ExceptionManagement;
using BMC.Common.ConfigurationManagement;

namespace BMC.MeterAdjustmentTool.Helpers
{
    public partial class BmcConnectionStringBuilder : DisposableObject, ICustomTypeDescriptor
    {
        private enum Keywords
        {
            Server = 0,
            InstanceName,
            UserID,
            Password,
            Timeout
        }

        private IDictionary<Keywords, object> _currentValues = null;
        private IDictionary<string, Keywords> _currentNames = null;
        private PropertyDescriptorCollection _propertyDescriptors = null;

        public BmcConnectionStringBuilder()
        {
            _currentValues = new SortedDictionary<Keywords, object>() {
                { Keywords.Server, string.Empty },
                { Keywords.InstanceName, string.Empty },
                { Keywords.UserID, string.Empty },
                { Keywords.Password, string.Empty },
                { Keywords.Timeout, 30 },
            };
            _currentNames = new SortedDictionary<string, Keywords>() {
                { "Server Name",Keywords.Server },
                { "Instance Name",Keywords.InstanceName },
                { "User Name",Keywords.UserID },
                { "Password",Keywords.Password },
                { "Connection Timeout",Keywords.Timeout },
            };
            this.InitializeProperyDescriptors();
        }

        private void InitializeProperyDescriptors()
        {
            _propertyDescriptors = new PropertyDescriptorCollection(
                new PropertyDescriptor[] {
                    AddPropertyDescriptor("Server", typeof(string), false, 
                        new Attribute[] 
                        { 
                            new TypeConverterAttribute(typeof(SqlDataSourceConverter)),
                            new DisplayNameAttribute("Server Name"),
                            new CategoryAttribute("Source")
                        }),
                    AddPropertyDescriptor("InstanceName", typeof(string), false, 
                        new Attribute[] 
                        { 
                            new DisplayNameAttribute("Instance Name"),
                            new CategoryAttribute("Source")
                        }),
                    AddPropertyDescriptor("UserID", typeof(string), false, 
                        new Attribute[] 
                        { 
                            new DisplayNameAttribute("User Name"),
                            new CategoryAttribute("Security")
                        }),
                    AddPropertyDescriptor("Password", typeof(string), false, 
                        new Attribute[] 
                        { 
                            new DisplayNameAttribute("Password"),
                            new PasswordPropertyTextAttribute(true),
                            new CategoryAttribute("Security")
                        }),
                    AddPropertyDescriptor("Timeout", typeof(int), false, 
                        new Attribute[] 
                        { 
                            new DisplayNameAttribute("Connection Timeout"),
                            new CategoryAttribute("Initialization") 
                        }),
                });
        }

        private Attribute[] GetBrowsableAttributes(Attribute[] attributes)
        {
            Attribute[] browsableAttributes = new Attribute[2 + attributes.Length];
            browsableAttributes[0] = new BrowsableAttribute(true);
            browsableAttributes[1] = new RefreshPropertiesAttribute(RefreshProperties.All);
            attributes.CopyTo(browsableAttributes, 2);
            return attributes;
        }

        // does the keyword exist as a stored value or something that should always be persisted 
        public virtual bool ShouldSerialize(string keyword)
        {
            return _currentNames.ContainsKey(keyword);
        }

        public virtual bool TryGetValue(string keyword, out object value)
        {
            value = null;
            if (!_currentNames.ContainsKey(keyword)) return false;
            return _currentValues.TryGetValue(_currentNames[keyword], out value);
        }

        private PropertyDescriptor AddPropertyDescriptor(string propertyName, Type propertyType, bool isReadOnly, Attribute[] attributes)
        {
            PropertyDescriptor desc = new SqlConnectionStringDescriptor(propertyName, propertyType, isReadOnly, attributes);
            return desc;
        }

        internal Attribute[] GetAttributesFromCollection(AttributeCollection collection)
        {
            Attribute[] attributes = new Attribute[collection.Count];
            collection.CopyTo(attributes, 0);
            return attributes;
        }

        public virtual object this[string keyword]
        {
            get
            {
                if (!_currentNames.ContainsKey(keyword)) return null;
                object value = null;
                if (_currentValues.TryGetValue(_currentNames[keyword], out value))
                {
                    return value;
                }
                return null;
            }
            set
            {
                if (!_currentNames.ContainsKey(keyword)) return;
                _currentValues[_currentNames[keyword]] = value;
            }
        }

        public string GetConnectionString()
        {
            string result = default(string);

            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                {
                    builder.DataSource = this.Server +
                        (string.IsNullOrEmpty(this.InstanceName) ? "" : "\\" + this.InstanceName);
                    builder.InitialCatalog = GetExchangeDB();
                    builder.UserID = this.UserID;
                    builder.Password = this.Password;
                    builder.ConnectTimeout = this.Timeout;
                    result = builder.ConnectionString;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return result;
        }

        private string GetExchangeDB()
        {
            try
            {
                return ConfigManager.Read("MetAdjExchangeDB");
            }
            catch { return "Exchange"; }
        }

        public byte[] GetEncryptedConnectionString()
        {
            return TripleDESEncryption.EncryptToBytes(this.GetConnectionString());
        }

        public void LoadConnectionString(string value)
        {
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(value);
                {
                    this.Server = builder.DataSource;
                    this.UserID = builder.UserID;
                    this.Password = builder.Password;
                    this.Timeout = builder.ConnectTimeout;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        #region Properties
        [TypeConverter(typeof(SqlDataSourceConverter))]
        public string Server
        {
            get
            {
                return _currentValues[Keywords.Server].ToString();
            }
            set
            {
                _currentValues[Keywords.Server] = value;
            }
        }

        public string InstanceName
        {
            get
            {
                return _currentValues[Keywords.InstanceName].ToString();
            }
            set
            {
                _currentValues[Keywords.InstanceName] = value;
            }
        }

        public string UserID
        {
            get
            {
                return _currentValues[Keywords.UserID].ToString();
            }
            set
            {
                _currentValues[Keywords.UserID] = value;
            }
        }

        public string Password
        {
            get
            {
                return _currentValues[Keywords.Password].ToString();
            }
            set
            {
                _currentValues[Keywords.Password] = value;
            }
        }

        public int Timeout
        {
            get
            {
                return Convert.ToInt32(_currentValues[Keywords.Timeout].ToString());
            }
            set
            {
                _currentValues[Keywords.Timeout] = value;
            }
        }

        #endregion

        #region ICustomTypeDescriptor Members

        public AttributeCollection GetAttributes()
        {
            return TypeDescriptor.GetAttributes(this, true);
        }

        public string GetClassName()
        {
            return TypeDescriptor.GetClassName(this, true);
        }

        public string GetComponentName()
        {
            return TypeDescriptor.GetComponentName(this, true);
        }

        public TypeConverter GetConverter()
        {
            return TypeDescriptor.GetConverter(this, true);
        }

        public EventDescriptor GetDefaultEvent()
        {
            return TypeDescriptor.GetDefaultEvent(this, true);
        }

        public PropertyDescriptor GetDefaultProperty()
        {
            return TypeDescriptor.GetDefaultProperty(this, true);
        }

        public object GetEditor(Type editorBaseType)
        {
            return TypeDescriptor.GetEditor(this, editorBaseType, true);
        }

        public EventDescriptorCollection GetEvents(Attribute[] attributes)
        {
            return TypeDescriptor.GetEvents(this, true);
        }

        public EventDescriptorCollection GetEvents()
        {
            return TypeDescriptor.GetEvents(this, true);
        }

        public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            return _propertyDescriptors;
        }

        public PropertyDescriptorCollection GetProperties()
        {
            return _propertyDescriptors;
        }

        public object GetPropertyOwner(PropertyDescriptor pd)
        {
            return this;
        }

        #endregion

        public void CopyTo(BmcConnectionStringBuilder destination)
        {
            destination.Server = this.Server;
            destination.InstanceName = this.InstanceName;
            destination.UserID = this.UserID;
            destination.Password = this.Password;
            destination.Timeout = this.Timeout;
        }
    }
}
