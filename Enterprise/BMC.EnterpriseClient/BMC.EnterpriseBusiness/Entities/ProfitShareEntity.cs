using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.EnterpriseBusiness.Entities
{
    public partial class ProfitShareEntity
    {
        private string _Name = string.Empty;
       // private int _id = 0;
        private int _ShareHolderId = 0;
        private double _Percentage = 0;
        private string _Description = string.Empty;
        private int _Id = 0;

        public string ShareHolderName
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
            }
        }
        //public int ID
        //{
        //    get
        //    {
        //        return _id;
        //    }
        //    set
        //    {
        //        _id = value;
        //    }
        //}
        public int ShareHolderId
        {
            get
            {
                return _ShareHolderId;
            }
            set
            {
                _ShareHolderId = value;
            }
        }

        public double Percentage
        {
            get
            {
                return _Percentage;
            }
            set
            {
                _Percentage = value;
            }
        }

        public string ProfitShareGroupDescription
        {
            get
            {
                return _Description;
            }
            set
            {
                _Description = value;
            }
        }
        public int ProfitShareGroupId
        {
            get
            {
                return _Id;
            }
            set
            {
                _Id = value;
            }
        }

    }

    public partial class ShareHolderPercentageDetails
    {
        public int ShareHolderId
        {
            get;
            set;
        }

        public string ShareHolderName
        {
            get;
            set;
        }

        public double ProfitSharePercentage
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

    }

    public  partial  class ShareHolderPercentage
    {
        private int _ShareHolderId = int.MinValue;
        private string _ShareHolderName = string.Empty;

        public int ShareHolderId
        {
            get
            {
                return _ShareHolderId;
            }
            set
            {
                _ShareHolderId = value;
            }
        }

        public string ShareHolderName
        {
            get
            {
                return _ShareHolderName;
            }
            set
            {
                _ShareHolderName = value;
            }
        }

    }

    public class ShareHolderDetailsResult
    {

        private int _ShareHolderId;

        private string _ShareHolderName;

        private string _ShareHolderDescription;

        private System.Nullable<System.DateTime> _DateCreated;

        private System.Nullable<System.DateTime> _DateModified;

        public ShareHolderDetailsResult()
        {
        }

      
        public int ShareHolderId
        {
            get
            {
                return this._ShareHolderId;
            }
            set
            {
                if ((this._ShareHolderId != value))
                {
                    this._ShareHolderId = value;
                }
            }
        }

       
        public string ShareHolderName
        {
            get
            {
                return this._ShareHolderName;
            }
            set
            {
                //if ((this._ShareHolderName != value))
                //{
                    this._ShareHolderName = value;
                //}
            }
        }

    
        public string ShareHolderDescription
        {
            get
            {
                return this._ShareHolderDescription;
            }
            set
            {
                if ((this._ShareHolderDescription != value))
                {
                    this._ShareHolderDescription = value;
                }
            }
        }

      
        public System.Nullable<System.DateTime> DateCreated
        {
            get
            {
                return this._DateCreated;
            }
            set
            {
                if ((this._DateCreated != value))
                {
                    this._DateCreated = value;
                }
            }
        }

       
        public System.Nullable<System.DateTime> DateModified
        {
            get
            {
                return this._DateModified;
            }
            set
            {
                if ((this._DateModified != value))
                {
                    this._DateModified = value;
                }
            }
        }
    }

    public partial class GetProfitShareGroupDetailsResult
    {

        private int _ProfitShareId;

        private int _ShareHolderId;

        private string _ShareHolderName;

        private double _ProfitSharePercentage;

        private string _ProfitShareDescription;

       

        public GetProfitShareGroupDetailsResult()
        {
        }



        public int ProfitShareId
        {
            get
            {
                return this._ProfitShareId;
            }
            set
            {
                if ((this._ProfitShareId != value))
                {
                    this._ProfitShareId = value;
                }
            }
        }

      
        public int ShareHolderId
        {
            get
            {
                return this._ShareHolderId;
            }
            set
            {
                if ((this._ShareHolderId != value))
                {
                    this._ShareHolderId = value;
                }
            }
        }

        public string ShareHolderName
        {
            get
            {
                return this._ShareHolderName;
            }
            set
            {
                if ((this._ShareHolderName != value))
                {
                    this._ShareHolderName = value;
                }
            }
        }

       
        public double ProfitSharePercentage
        {
            get
            {
                return this._ProfitSharePercentage;
            }
            set
            {
                if ((this._ProfitSharePercentage != value))
                {
                    this._ProfitSharePercentage = value;
                }
            }
        }

     
        public string ProfitShareDescription
        {
            get
            {
                return this._ProfitShareDescription;
            }
            set
            {
                if ((this._ProfitShareDescription != value))
                {
                    this._ProfitShareDescription = value;
                }
            }
        }

       
        
    }


    public partial class ShareHoldersList
    {
        private int _Id;
        private string _Name;
        public int Id
        {
            get
            {
                return _Id;
            }
            set
            {
                _Id = value;
            }
        }
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
            }
        }
    }

}

