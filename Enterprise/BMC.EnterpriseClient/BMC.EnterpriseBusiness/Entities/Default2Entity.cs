using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.EnterpriseBusiness.Entities
{
   public  class DefaultEntity
    {

        private System.Nullable<int> _Terms_Group_ID;
		
		private string _Terms_Group_Name;
 
		public System.Nullable<int> Terms_Group_ID
		{
			get
			{
				return this._Terms_Group_ID;
			}
			set
			{
				if ((this._Terms_Group_ID != value))
				{
					this._Terms_Group_ID = value;
				}
			}
		}
		
		
		public string Terms_Group_Name
		{
			get
			{
				return this._Terms_Group_Name;
			}
			set
			{
				if ((this._Terms_Group_Name != value))
				{
					this._Terms_Group_Name = value;
				}
			}
		}
        

        /// <summary>
        /// ////////
        /// </summary>

         private System.Nullable<int> _Access_Key_ID;
		
		private string _Access_Key_Name;
		
		
		
		public System.Nullable<int> Access_Key_ID
		{
			get
			{
				return this._Access_Key_ID;
			}
			set
			{
				if ((this._Access_Key_ID != value))
				{
					this._Access_Key_ID = value;
				}
			}
		}
		
		
		public string Access_Key_Name
		{
			get
			{
				return this._Access_Key_Name;
			}
			set
			{
				if ((this._Access_Key_Name != value))
				{
					this._Access_Key_Name = value;
				}
			}
		}
	

        /// <summary>
        /// ///////
        /// </summary>
        private System.Nullable<int> _Staff_ID;
		
		private string _Staff_Last_Name;
		
		private string _Staff_First_Name;
						
		
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
       

        private string _Access_Key_Ref;

        public string Access_Key_Ref
        {
            get
            {
                return this._Access_Key_Ref;
            }
            set
            {
                if ((this._Access_Key_Ref != value))
                {
                    this._Access_Key_Ref = value;
                }
            }
        }

        private string _Access_Key_Manufacturer;


        public string Access_Key_Manufacturer
        {
            get
            {
                return this._Access_Key_Manufacturer;
            }
            set
            {
                if ((this._Access_Key_Manufacturer != value))
                {
                    this._Access_Key_Manufacturer = value;
                }
            }
        }
        private string _Access_Key_Type;

        public string Access_Key_Type
        {
            get
            {
                return this._Access_Key_Type;
            }
            set
            {
                if ((this._Access_Key_Type != value))
                {
                    this._Access_Key_Type = value;
                }
            }
        }
       /// <summary>
       /// //////
       /// </summary>
        private System.Nullable<bool> _Terms_Group_ID_Default;

        public System.Nullable<bool> Terms_Group_ID_Default
        {
            get
            {
                return this._Terms_Group_ID_Default;
            }
            set
            {
                if ((this._Terms_Group_ID_Default != value))
                {
                    this._Terms_Group_ID_Default = value;
                }
            }
        }
       //apply button entity end

       	}
    

    }

