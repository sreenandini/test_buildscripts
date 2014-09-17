
namespace BMC.EnterpriseBusiness.Entities.ServiceCalls
{
   public class FaultEntity
    {
       private string _Fault_Name;
       private bool _Is_ToMail;

       public FaultEntity() { }
       
       public string Fault_Name
       {
           get {return  this._Fault_Name; }
           set
           {
               if ((this._Fault_Name != value))
               {
                   this._Fault_Name = value;
               }
           }
       }

       public bool Is_ToMail
       {
           get { return this._Is_ToMail; }
           set
           {
               if ((this._Is_ToMail != value))
               {
                   this._Is_ToMail = value;
               }
           }
       }
    }

   public class FaultGroupEntity
   {
       private int _FaultGroup_ID;
       private string _Fault_Group_Description;

       public FaultGroupEntity() { }
       public string Fault_Group_Description
       {
           get { return this._Fault_Group_Description; }
           set
           {
               if ((this._Fault_Group_Description != value))
               {
                   this._Fault_Group_Description = value;
               }
           }
       }

       public int FaultGroup_ID
       {
           get { return this._FaultGroup_ID; }
           set
           {
               if ((this._FaultGroup_ID != value))
               {
                   this._FaultGroup_ID = value;
               }
           }
       }
   }
   public class FaultGroupChildEntity
   {
       private int _Fault_ID;
       private string _Fault_Description;

       public FaultGroupChildEntity() { }
       public string Fault_Description
       {
           get { return this._Fault_Description; }
           set
           {
               if ((this._Fault_Description != value))
               {
                   this._Fault_Description = value;
               }
           }
       }

       public int Fault_ID
       {
           get { return this._Fault_ID; }
           set
           {
               if ((this._Fault_ID != value))
               {
                   this._Fault_ID = value;
               }
           }
       }
   }
   public class FaultEventEntity
   {
       private int _Fault_Event_ID;
       private string _Fault_Event_Description;

       public FaultEventEntity() { }
       public string Fault_Event_Description
       {
           get { return this._Fault_Event_Description; }
           set
           {
               if ((this._Fault_Event_Description != value))
               {
                   this._Fault_Event_Description = value;
               }
           }
       }

       public int Fault_Event_ID
       {
           get { return this._Fault_Event_ID; }
           set
           {
               if ((this._Fault_Event_ID != value))
               {
                   this._Fault_Event_ID = value;
               }
           }
       }
   }

   public partial class GMUConfigurationEntity
   {

       private System.Nullable<int> _Code;

       private System.Nullable<int> _subcode;

       private System.Nullable<int> _SourceProtocol;

       private string _Fault;

       private System.Nullable<bool> _CreateServiceCall;

       private System.Nullable<bool> _CloseServiceCall;

       private System.Nullable<int> _SourceID;

       private System.Nullable<int> _Type;

       private string _Description;

       private System.Nullable<bool> _ToMail;

       private int _Datapak_Fault_ID;

       private System.Nullable<int> _Call_Fault_ID;

       private string _Mail_CC;

       private string _Mail_TO;

       private System.Nullable<int> _Call_Group_ID;

       public GMUConfigurationEntity()
       {
       }

      
       public System.Nullable<int> Code
       {
           get
           {
               return this._Code;
           }
           set
           {
               if ((this._Code != value))
               {
                   this._Code = value;
               }
           }
       }

       
       public System.Nullable<int> subcode
       {
           get
           {
               return this._subcode;
           }
           set
           {
               if ((this._subcode != value))
               {
                   this._subcode = value;
               }
           }
       }

       
       public System.Nullable<int> SourceProtocol
       {
           get
           {
               return this._SourceProtocol;
           }
           set
           {
               if ((this._SourceProtocol != value))
               {
                   this._SourceProtocol = value;
               }
           }
       }

       
       public string Fault
       {
           get
           {
               return this._Fault;
           }
           set
           {
               if ((this._Fault != value))
               {
                   this._Fault = value;
               }
           }
       }

       
       public System.Nullable<bool> CreateServiceCall
       {
           get
           {
               return this._CreateServiceCall;
           }
           set
           {
               if ((this._CreateServiceCall != value))
               {
                   this._CreateServiceCall = value;
               }
           }
       }

      
       public System.Nullable<bool> CloseServiceCall
       {
           get
           {
               return this._CloseServiceCall;
           }
           set
           {
               if ((this._CloseServiceCall != value))
               {
                   this._CloseServiceCall = value;
               }
           }
       }

       
       public System.Nullable<int> SourceID
       {
           get
           {
               return this._SourceID;
           }
           set
           {
               if ((this._SourceID != value))
               {
                   this._SourceID = value;
               }
           }
       }

      
       public System.Nullable<int> Type
       {
           get
           {
               return this._Type;
           }
           set
           {
               if ((this._Type != value))
               {
                   this._Type = value;
               }
           }
       }

      
       public string Description
       {
           get
           {
               return this._Description;
           }
           set
           {
               if ((this._Description != value))
               {
                   this._Description = value;
               }
           }
       }

     
       public System.Nullable<bool> ToMail
       {
           get
           {
               return this._ToMail;
           }
           set
           {
               if ((this._ToMail != value))
               {
                   this._ToMail = value;
               }
           }
       }

       
       public int Datapak_Fault_ID
       {
           get
           {
               return this._Datapak_Fault_ID;
           }
           set
           {
               if ((this._Datapak_Fault_ID != value))
               {
                   this._Datapak_Fault_ID = value;
               }
           }
       }

       
       public System.Nullable<int> Call_Fault_ID
       {
           get
           {
               return this._Call_Fault_ID;
           }
           set
           {
               if ((this._Call_Fault_ID != value))
               {
                   this._Call_Fault_ID = value;
               }
           }
       }

      
       public string Mail_CC
       {
           get
           {
               return this._Mail_CC;
           }
           set
           {
               if ((this._Mail_CC != value))
               {
                   this._Mail_CC = value;
               }
           }
       }

       
       public string Mail_TO
       {
           get
           {
               return this._Mail_TO;
           }
           set
           {
               if ((this._Mail_TO != value))
               {
                   this._Mail_TO = value;
               }
           }
       }
       public System.Nullable<int> Call_Group_ID
       {
           get
           {
               return this._Call_Group_ID;
           }
           set
           {
               if ((this._Call_Group_ID != value))
               {
                   this._Call_Group_ID = value;
               }
           }
       }
   }

   public class MailConfigurationEntity
   {
       private int _Fault_ID;
       private string _To_Address;
       private string _CC_Address;

       public int Fault_ID
       {
           get { return this._Fault_ID; }
           set
           {
               if ((this._Fault_ID != value))
               {
                   this._Fault_ID = value;
               }
           }
       }

       public string To_Address
       {
           get { return this._To_Address; }
           set
           {
               if ((this._To_Address != value))
               {
                   this._To_Address = value;
               }
           }
       }
       public string CC_Address
       {
           get { return this._CC_Address; }
           set
           {
               if ((this._CC_Address != value))
               {
                   this._CC_Address = value;
               }
           }
       }
   }
}
