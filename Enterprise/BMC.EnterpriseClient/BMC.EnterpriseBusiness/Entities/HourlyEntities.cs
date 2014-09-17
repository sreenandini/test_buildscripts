using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib;

namespace BMC.EnterpriseBusiness.Entities
{
    public enum HourlyFilterByEntity
    {
        Position = 0,
        Site,
        Zone,
        Category
    }

    public partial class HourlyDetailEntity : DisposableObject
    {
        private int _ID;

        private DateTime _Date;

        private string _Bar_Position_Name;

        private string _Machine_Name;

        private string _Machine_Category;

        private string _Stock;

        public double[] _hourValues = null;

        private int _occupancyHour = 0;

        private int _siteOpeningHour = 0;

        public HourlyDetailEntity()
        {
            _hourValues = new double[24];
        }

        public override string ToString()
        {
            return string.Format("{0:D} - {1}", _ID, _Date.ToString("dd/MM/yyyy hh:mm:ss.fff"));
        }

        public double[] GetHourValues()
        {
            return _hourValues;
        }

        public double this[int index]
        {
            get { return _hourValues[index]; }
            set
            {
                _hourValues[index] = value;
            }
        }

        //[global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ID", DbType = "Int")]
        public int ID
        {
            get
            {
                return this._ID;
            }
            set
            {
                if ((this._ID != value))
                {
                    this._ID = value;
                }
            }
        }

        //[global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Date", DbType = "DateTime NOT NULL")]
        public DateTime Date
        {
            get
            {
                return this._Date;
            }
            set
            {
                if ((this._Date != value))
                {
                    this._Date = value;
                }
            }
        }

        //[global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Bar_Position_Name", DbType = "VarChar(50)")]
        public string Bar_Position_Name
        {
            get
            {
                return this._Bar_Position_Name;
            }
            set
            {
                if ((this._Bar_Position_Name != value))
                {
                    this._Bar_Position_Name = value;
                }
            }
        }

        //[global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Machine_Name", DbType = "VarChar(50)")]
        public string Machine_Name
        {
            get
            {
                return this._Machine_Name;
            }
            set
            {
                if ((this._Machine_Name != value))
                {
                    this._Machine_Name = value;
                }
            }
        }

        //[global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Machine_Category", DbType = "VarChar(50)")]
        public string Machine_Category
        {
            get
            {
                return this._Machine_Category;
            }
            set
            {
                if ((this._Machine_Category != value))
                {
                    this._Machine_Category = value;
                }
            }
        }

        public string Stock
        {
            get
            {
                return this._Stock;
            }
            set
            {
                if ((this._Stock != value))
                {
                    this._Stock = value;
                }
            }
        }

        //[global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_hourValues[1_Value", DbType = "Float")]
        public double HS_Hour1_Value
        {
            get
            {
                return this._hourValues[0];
            }
            set
            {
                if ((this._hourValues[0] != value))
                {
                    this._hourValues[0] = value;
                }
            }

        }

        //[global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_hourValues[2]", DbType = "Float")]
        public double HS_Hour2_Value
        {
            get
            {
                return this._hourValues[1];
            }
            set
            {
                if ((this._hourValues[1] != value))
                {
                    this._hourValues[1] = value;
                }
            }

        }

        //[global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_hourValues[3]", DbType = "Float")]
        public double HS_Hour3_Value
        {
            get
            {
                return this._hourValues[2];
            }
            set
            {
                if ((this._hourValues[2] != value))
                {
                    this._hourValues[2] = value;
                }
            }

        }

        //[global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_hourValues[4]", DbType = "Float")]
        public double HS_Hour4_Value
        {
            get
            {
                return this._hourValues[3];
            }
            set
            {
                if ((this._hourValues[3] != value))
                {
                    this._hourValues[3] = value;
                }
            }

        }

        //[global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_hourValues[5]", DbType = "Float")]
        public double HS_Hour5_Value
        {
            get
            {
                return this._hourValues[4];
            }
            set
            {
                if ((this._hourValues[4] != value))
                {
                    this._hourValues[4] = value;
                }
            }

        }

        //[global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_hourValues[6]", DbType = "Float")]
        public double HS_Hour6_Value
        {
            get
            {
                return this._hourValues[5];
            }
            set
            {
                if ((this._hourValues[5] != value))
                {
                    this._hourValues[5] = value;
                }
            }

        }

        //[global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_hourValues[7]", DbType = "Float")]
        public double HS_Hour7_Value
        {
            get
            {
                return this._hourValues[6];
            }
            set
            {
                if ((this._hourValues[6] != value))
                {
                    this._hourValues[6] = value;
                }
            }

        }

        //[global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_hourValues[8]", DbType = "Float")]
        public double HS_Hour8_Value
        {
            get
            {
                return this._hourValues[7];
            }
            set
            {
                if ((this._hourValues[7] != value))
                {
                    this._hourValues[7] = value;
                }
            }

        }

        //[global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_hourValues[9]", DbType = "Float")]
        public double HS_Hour9_Value
        {
            get
            {
                return this._hourValues[8];
            }
            set
            {
                if ((this._hourValues[8] != value))
                {
                    this._hourValues[8] = value;
                }
            }

        }

        //[global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_hourValues[10]", DbType = "Float")]
        public double HS_Hour10_Value
        {
            get
            {
                return this._hourValues[9];
            }
            set
            {
                if ((this._hourValues[9] != value))
                {
                    this._hourValues[9] = value;
                }
            }

        }

        //[global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_hourValues[11]", DbType = "Float")]
        public double HS_Hour11_Value
        {
            get
            {
                return this._hourValues[10];
            }
            set
            {
                if ((this._hourValues[10] != value))
                {
                    this._hourValues[10] = value;
                }
            }

        }

        //[global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_hourValues[12]", DbType = "Float")]
        public double HS_Hour12_Value
        {
            get
            {
                return this._hourValues[11];
            }
            set
            {
                if ((this._hourValues[11] != value))
                {
                    this._hourValues[11] = value;
                }
            }

        }

        //[global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_hourValues[13]", DbType = "Float")]
        public double HS_Hour13_Value
        {
            get
            {
                return this._hourValues[12];
            }
            set
            {
                if ((this._hourValues[12] != value))
                {
                    this._hourValues[12] = value;
                }
            }

        }

        //[global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_hourValues[14]", DbType = "Float")]
        public double HS_Hour14_Value
        {
            get
            {
                return this._hourValues[13];
            }
            set
            {
                if ((this._hourValues[13] != value))
                {
                    this._hourValues[13] = value;
                }
            }

        }

        //[global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_hourValues[15]", DbType = "Float")]
        public double HS_Hour15_Value
        {
            get
            {
                return this._hourValues[14];
            }
            set
            {
                if ((this._hourValues[14] != value))
                {
                    this._hourValues[14] = value;
                }
            }

        }

        //[global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_hourValues[16]", DbType = "Float")]
        public double HS_Hour16_Value
        {
            get
            {
                return this._hourValues[15];
            }
            set
            {
                if ((this._hourValues[15] != value))
                {
                    this._hourValues[15] = value;
                }
            }

        }

        //[global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_hourValues[17]", DbType = "Float")]
        public double HS_Hour17_Value
        {
            get
            {
                return this._hourValues[16];
            }
            set
            {
                if ((this._hourValues[16] != value))
                {
                    this._hourValues[16] = value;
                }
            }

        }

        //[global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_hourValues[18]", DbType = "Float")]
        public double HS_Hour18_Value
        {
            get
            {
                return this._hourValues[17];
            }
            set
            {
                if ((this._hourValues[17] != value))
                {
                    this._hourValues[17] = value;
                }
            }

        }

        //[global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_hourValues[19]", DbType = "Float")]
        public double HS_Hour19_Value
        {
            get
            {
                return this._hourValues[18];
            }
            set
            {
                if ((this._hourValues[18] != value))
                {
                    this._hourValues[18] = value;
                }
            }

        }

        //[global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_hourValues[20]", DbType = "Float")]
        public double HS_Hour20_Value
        {
            get
            {
                return this._hourValues[19];
            }
            set
            {
                if ((this._hourValues[19] != value))
                {
                    this._hourValues[19] = value;
                }
            }
        }

        //[global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_hourValues[21]", DbType = "Float")]
        public double HS_Hour21_Value
        {
            get
            {
                return this._hourValues[20];
            }
            set
            {
                if ((this._hourValues[20] != value))
                {
                    this._hourValues[20] = value;
                }
            }

        }

        //[global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_hourValues[22]", DbType = "Float")]
        public double HS_Hour22_Value
        {
            get
            {
                return this._hourValues[21];
            }
            set
            {
                if ((this._hourValues[21] != value))
                {
                    this._hourValues[21] = value;
                }
            }

        }

        //[global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_hourValues[23]", DbType = "Float")]
        public double HS_Hour23_Value
        {
            get
            {
                return this._hourValues[22];
            }
            set
            {
                if ((this._hourValues[22] != value))
                {
                    this._hourValues[22] = value;
                }
            }

        }

        //[global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_hourValues[24]", DbType = "Float")]
        public double HS_Hour24_Value
        {
            get
            {
                return this._hourValues[23];
            }
            set
            {
                if ((this._hourValues[23] != value))
                {
                    this._hourValues[23] = value;
                }
            }
        }

        private double _total = 0;
        private bool _isTotalSet = false;
        public double Total
        {
            get
            {
                if (_isTotalSet) return _total;
                else return _hourValues.Sum(x => x);
            }
            set
            {
                _total = value;
                _isTotalSet = true;
            }
        }

        public int OccupancyHour
        {
            get { return _occupancyHour; }
            set { _occupancyHour = value; }
        }

        public int SiteOpeningHour
        {
            get { return _siteOpeningHour; }
            set { _siteOpeningHour = value; }
        }
    }

    public partial class HourlyDetailsEntity : List<HourlyDetailEntity>
    {
        public HourlyDetailsEntity() { }

        public HourlyDetailsEntity(IEnumerable<HourlyDetailEntity> enumerable)
            : base(enumerable) { }
    }

    public partial class HourlyStatisticsTypeEntity : DisposableObject
    {

        private int _HST_ID;

        private string _HST_Type;

        private string _HST_Desc;

        private string _HST_DisplayName;

        public HourlyStatisticsTypeEntity()
        {
        }

        public string HST_DisplayName
        {
            get
            {
                return this._HST_DisplayName;
            }
            set
            {
                if ((this._HST_DisplayName != value))
                {
                    this._HST_DisplayName = value;
                }
            }
        }

        //[Column(Storage = "_HST_ID", DbType = "Int NOT NULL")]
        public int HST_ID
        {
            get
            {
                return this._HST_ID;
            }
            set
            {
                if ((this._HST_ID != value))
                {
                    this._HST_ID = value;
                }
            }
        }

        //[Column(Storage = "_HST_Type", DbType = "VarChar(50)")]
        public string HST_Type
        {
            get
            {
                return this._HST_Type;
            }
            set
            {
                if ((this._HST_Type != value))
                {
                    this._HST_Type = value;
                }
            }
        }

        //[Column(Storage = "_HST_Desc", DbType = "VarChar(8000)")]
        public string HST_Desc
        {
            get
            {
                return this._HST_Desc;
            }
            set
            {
                if ((this._HST_Desc != value))
                {
                    this._HST_Desc = value;
                }
            }
        }
    }

    public partial class HourlyStatisticsTypesEntity : List<HourlyStatisticsTypeEntity> { }

    public partial class HourlyFilterByValueEntity : DisposableObject
    {
        public int FilterById { get; set; }
        public string FilterByName { get; set; }
    }

    public partial class HourlyFilterByValuesEntity : List<HourlyFilterByValueEntity> { }

    //public partial class HourlySiteEntity : HourlyFilterByEntityBase { }

    //public partial class HourlySitesEntity : HourlyFilterBysEntityBase<HourlySiteEntity> { }

    //public partial class HourlyPositionEntity : HourlyFilterByEntityBase { }

    //public partial class HourlyPositionsEntity : HourlyFilterBysEntityBase<HourlyPositionEntity> { }

    //public partial class HourlyZoneEntity : HourlyFilterByEntityBase { }

    //public partial class HourlyZonesEntity : HourlyFilterBysEntityBase<HourlyZoneEntity> { }

    //public partial class HourlyCategoryEntity : HourlyFilterByEntityBase { }

    //public partial class HourlyCategoriesEntity : HourlyFilterBysEntityBase<HourlyCategoryEntity> { }
}
