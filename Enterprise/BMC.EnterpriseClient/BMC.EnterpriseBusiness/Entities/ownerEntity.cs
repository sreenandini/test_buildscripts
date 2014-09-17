using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.EnterpriseBusiness.Entities
{


    public partial class ownerEntity
    {
        private int _Company_ID;

        private string _Company_Name;

        private string _company_subcompany;
              
            public int Company_ID
            {
                get
                {
                    return this._Company_ID;
                }
                set
                {
                    if ((this._Company_ID != value))
                    {
                        this._Company_ID = value;
                    }
                }
            }

            
            public string Company_Name
            {
                get
                {
                    return this._Company_Name;
                }
                set
                {
                    if ((this._Company_Name != value))
                    {
                        this._Company_Name = value;
                    }
                }
            }

        public System.Nullable<bool> _Customer_Access_View_All_Companies;

		public System.Nullable<bool> Customer_Access_View_All_Companies
		{
			get
			{
				return this._Customer_Access_View_All_Companies;
			}
			set
			{
				if ((this._Customer_Access_View_All_Companies != value))
				{
					this._Customer_Access_View_All_Companies = value;
				}
			}
		}

            private int _Sub_Company_ID;

            private string _Sub_Company_Name;
                
            
            public int Sub_Company_ID
            {
                get
                {
                    return this._Sub_Company_ID;
                }
                set
                {
                    if ((this._Sub_Company_ID != value))
                    {
                        this._Sub_Company_ID = value;
                    }
                }
            }

           
            public string Sub_Company_Name
            {
                get
                {
                    return this._Sub_Company_Name;
                }
                set
                {
                    if ((this._Sub_Company_Name != value))
                    {
                        this._Sub_Company_Name = value;
                    }
                }
            }

            public string company_name
            {

                get
                {
                    return _Company_Name;
                }

                set
                {
                    _Company_Name = value;

                }
            }
            public string company_subcompany
            {
                get
                {
                    return _company_subcompany;
                }

                set
                {
                    _company_subcompany = value;

                }
            }

            private string _Sub_Company_Region_Name;
            private string _Staff_Last_Name;
            private string _Staff_First_Name;


            public string Sub_Company_Region_Name
            {
                get
                {
                    return this._Sub_Company_Region_Name;
                }
                set
                {
                    if ((this._Sub_Company_Region_Name != value))
                    {
                        this._Sub_Company_Region_Name = value;
                    }
                }
            }

            public string Staff_Last_Name
            {
                get
                {
                    return this._Staff_Last_Name;
                }
                set
                {
                    if ((this._Staff_Last_Name != value))
                    {
                        this._Staff_Last_Name = value;
                    }
                }
            }
            public string Staff_First_Name
            {
                get
                {
                    return this._Staff_First_Name;
                }
                set
                {
                    if ((this._Staff_First_Name != value))
                    {
                        this._Staff_First_Name = value;
                    }
                }
            }
            private System.Nullable<int> _Staff_ID;


            public System.Nullable<int> Staff_ID
            {
                get
                {
                    return this._Staff_ID;
                }
                set
                {
                    if ((this._Staff_ID != value))
                    {
                        this._Staff_ID = value;
                    }
                }
            }


           
             
        }
        
        }
            


      
    

 