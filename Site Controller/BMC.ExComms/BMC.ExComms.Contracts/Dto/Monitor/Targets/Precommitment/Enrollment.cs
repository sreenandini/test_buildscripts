using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    #region Enrollment Parameter Request

    /// <summary>
    /// GMU to host requesting enrollment parameters for pre-commitment.
    /// </summary>
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
        Name = "MonTgt_G2H_PC_EnrollmentParameterRequest")]
    [ExCommsMessageKnownType]
    public class MonTgt_G2H_PC_EnrollmentParameterRequest
        : MonTgt_G2H_PC_StatusRequest, IMonTgt_G2H
    {
        #region Constructor

        public MonTgt_G2H_PC_EnrollmentParameterRequest()
        {
        }

        #endregion //Constructor
    }

    #endregion //Enrollment Parameter Request

    #region Enrollment Parameter Response

    /// <summary>
    /// GMU to host requesting enrollment parameters for pre-commitment.
    /// </summary>
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
        Name = "MonTgt_H2G_PC_EnrollmentParameterResponse")]
    [ExCommsMessageKnownType]
    public class MonTgt_H2G_PC_EnrollmentParameterResponse
        : MonTgt_B2B_Precommitment_Data, IMonTgt_H2G
    {
        #region Constructor

        public MonTgt_H2G_PC_EnrollmentParameterResponse()
        {
        }

        #endregion Constructor

        #region Properties

        ///<summary>
        ///Status; 0 – No error  
        ///</summary>
        [DataMember]
        public byte Status { get; set; }
        
        ///<summary>
        ///Indicates if rules are based on Time spent on device per Day. 
        ///</summary>  
        [DataMember]
        public bool IsDayTimeBasis { get; set; }
        
        ///<summary>
        ///Default time for the Gaming Day. HHMM (HH – Hours; MM – Minutes)
        ///</summary>  
        [DataMember]
        public TimeSpan DayDefaultTime { get; set; }
        
        ///<summary>
        ///Indicates if day time basis is mandatory for enrollment 
        ///</summary>  
        [DataMember]
        public bool IsDayTimeBasisMandatory { get; set; }
        
        ///<summary>
        ///Indicates if rules are based on Gaming days per Week. 
        ///</summary>  
        [DataMember]
        public bool IsWeekTimeBasis { get; set; }
        
        ///<summary>
        ///Default time for the patron for the Gaming days per Week. (DD – Days) 
        ///</summary>  
        [DataMember]
        public byte WeekDefaultTime { get; set; }
        
        ///<summary>
        ///Indicates if week time basis is mandatory for enrollment
        ///</summary>  
        [DataMember]
        public bool IsWeekTimeBasisMandatory { get; set; }
        
        ///<summary>
        ///Indicates if rules are based on Gaming days per Month.
        ///</summary>  
        [DataMember]
        public bool IsMonthTimeBasis { get; set; }
        
        ///<summary>
        ///Default time for the patron for the Gaming days per Month. (DD – Days)
        ///</summary>  
        [DataMember]
        public byte MonthDefaultTime { get; set; }
        
        ///<summary>
        ///Indicates if month time basis is mandatory for enrollment
        ///</summary>  
        [DataMember]
        public bool IsMonthTimeBasisMandatory { get; set; }
        
        ///<summary>
        ///Indicates if rules are based on Net Loss per Gaming Day.
        ///</summary>  
        [DataMember]
        public bool IsDayLossBasis { get; set; }
        
        ///<summary>
        ///Default loss limit for the Gaming Day (in cents)
        ///</summary>  
        [DataMember]
        public double DayDefaultLossValue { get; set; }
        
        ///<summary>
        ///Indicates if day loss basis is mandatory for enrollment 
        ///</summary>  
        [DataMember]
        public bool IsDayLossBasisMandatory { get; set; }
        
        ///<summary>
        ///Indicates if rules are based on Net Loss for the Gaming days per Week.
        ///<summary>
        [DataMember]
        public bool IsWeekLossBasis { get; set; }
        
        ///<summary>
        ///Default Loss limit for the Gaming days per Week (in cents)    
        ///</summary>  
        [DataMember]
        public double WeekDefaultLossValue { get; set; }
        
        ///<summary>
        ///Indicates if week loss basis is mandatory for enrollment
        ///</summary>  
        [DataMember]
        public bool IsWeekLossBasisMandatory { get; set; }
        
        ///<summary>
        ///Indicates if rules are based on Net Loss for the Gaming days per Month. 
        ///</summary>  
        [DataMember]
        public bool IsMonthLossBasis { get; set; }
        
        ///<summary>
        ///Default Loss limit for the Gaming days per Month (in cents)   
        ///</summary>  
        [DataMember]
        public double MonthDefaultLossValue { get; set; }
        
        ///<summary>
        ///Indicates if month loss basis is mandatory for enrollment
        ///</summary>  
        [DataMember]
        public bool IsMonthLossBasisMandatory { get; set; }
        
        ///<summary>
        ///Indicates if rules are based on Total Wager (Turnover) per Gaming Day.
        ///</summary>  
        [DataMember]
        public bool IsDayWagerBasis { get; set; }
        
        ///<summary>
        ///Default Wager for the Gaming Day (in cents)   
        ///</summary>  
        [DataMember]
        public double DayDefaultWager { get; set; }
        
        ///<summary>
        ///Indicates if day wager basis is mandatory for enrollment
        ///</summary>  
        [DataMember]
        public bool IsDayWagerBasisMandatory { get; set; }
        
        ///<summary>
        ///Indicates if rules are based on Total Wager (Turnover) for the Gaming days per Week.  
        ///</summary>  
        [DataMember]
        public bool IsWeekWagerBasis { get; set; }
        
        ///<summary>
        ///Default Wager for the Gaming days per Week (in cents) 
        ///</summary>
        [DataMember]
        public double WeekDefaultWager { get; set; }
        
        ///<summary>
        ///Indicates if week wager basis is mandatory for enrollment
        ///</summary>  
        [DataMember]
        public bool IsWeekWagerBasisMandatory { get; set; }
        
        ///<summary>
        ///Indicates if rules are based on Total Wager (Turnover) for the Gaming days per Month. 
        ///</summary>  
        [DataMember]
        public bool IsMonthWagerBasis { get; set; }
        
        ///<summary>
        ///Default Wager for the Gaming days per Month (in cents)
        ///</summary>  
        [DataMember]
        public double MonthDefaultWager { get; set; }
        
        ///<summary>
        ///Indicates if month wager basis is mandatory for enrollment    
        ///</summary>  
        [DataMember]
        public bool IsMonthWagerBasisMandatory { get; set; }
        
        ///<summary>
        ///Display message length
        ///</summary>  
        [DataMember]
        public byte DisplayMessageLength { get; set; }
        
        ///<summary>
        ///Display message 
        ///</summary>  
        [DataMember]
        public string DisplayMessage { get; set; }

        #endregion //Properties
    }

    #endregion //Enrollment Parameter Response

    #region Player Enrollment Request

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
        Name = "MonTgt_G2H_PC_PlayerEnrollmentRequest")]
    [ExCommsMessageKnownType]
    public class MonTgt_G2H_PC_PlayerEnrollmentRequest
        : MonTgt_G2H_PC_StatusRequest, IMonTgt_G2H
    {
        #region Constructor

        public MonTgt_G2H_PC_PlayerEnrollmentRequest()
        {
        }

        #endregion //Constructor

        #region Properties

        ///<summary>
        ///Indicates if rules are based on Time spent on device per Day.
        ///</summary>
        [DataMember]
        public bool IsDayTimeBasis { get; set; }

        ///<summary>
        ///Target time for the Gaming Day. HHMM (HH – Hours; MM – Minutes) 
        ///</summary>
        [DataMember]
        public TimeSpan DayDefaultTime { get; set; }

        ///<summary>
        ///Indicates if rules are based on Gaming days per Week.  
        ///</summary>
        [DataMember]
        public bool IsWeekTimeBasis { get; set; }
        
        ///<summary>
        ///Target time for the patron for the Gaming days per Week. (DD – Days)  
        ///</summary>
        [DataMember]
        public byte WeekTargetTime { get; set; }
        
        ///<summary>
        ///Indicates if rules are based on Gaming days per Month. 
        ///</summary>
        [DataMember]
        public bool IsMonthTimeBasis { get; set; }
        
        ///<summary>
        ///Target time for the patron for the Gaming days per Month. (DD – Days) 
        ///</summary>
        [DataMember]
        public byte MonthTargetTime { get; set; }
        
        ///<summary>
        ///Indicates if rules are based on Net Loss per Gaming Day.    
        ///</summary>
        [DataMember]
        public bool IsDayLossBasis { get; set; }
        
        ///<summary>
        ///Target loss limit for the Gaming Day (in cents) 
        ///</summary>
        [DataMember]
        public double DayTargetLossValue { get; set; }
        
        ///<summary>
        ///Indicates if rules are based on Net Loss for the Gaming days per Week.
        ///</summary>
        [DataMember]
        public bool IsWeekLossBasis { get; set; }
        
        ///<summary>
        ///Target Loss limit for the Gaming days per Week (in cents) 
        ///</summary> 
        [DataMember]
        public double WeekTargetLossValue { get; set; }
        
        ///<summary>
        ///Indicates if rules are based on Net Loss for the Gaming days per Month.
        ///</summary> 
        [DataMember]
        public bool IsMonthLossBasis { get; set; }
        
        ///<summary>
        ///Target Loss limit for the Gaming days per Month (in cents)
        ///</summary>
        [DataMember]
        public double MonthTargetLossValue { get; set; }
        
        ///<summary>
        ///Indicates if rules are based on Total Wager (Turnover) per Gaming Day. 
        ///</summary>
        [DataMember]
        public bool IsDayWagerBasis { get; set; }
        
        ///<summary>
        ///Target Wager for the Gaming Day (in cents)
        ///</summary> 
        [DataMember]
        public double DayTargetWager { get; set; }
        
        ///<summary>
        ///Indicates if rules are based on Total Wager (Turnover) for the Gaming days per Week.    
        ///</summary>
        [DataMember]
        public bool IsWeekWagerBasis { get; set; }
        
        ///<summary>
        ///Target Wager for the Gaming days per Week (in cents) 
        ///</summary>
        [DataMember]
        public double WeekTargetWager { get; set; }
        
        ///<summary>
        ///Indicates if rules are based on Total Wager (Turnover) for the Gaming days per Month.   
        ///</summary>
        [DataMember]
        public bool IsMonthWagerBasis { get; set; }
        
        ///<summary>
        ///Target Wager for the Gaming days per Month (in cents)
        ///</summary> 
        [DataMember]
        public double MonthTargetWager { get; set; }

        #endregion //Properties
    }

    #endregion //Player Enrollment Request

    #region Player Enrollment Response

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
        Name = "MonTgt_H2G_PC_PlayerEnrollmentResponse")]
    [ExCommsMessageKnownType]
    public class MonTgt_H2G_PC_PlayerEnrollmentResponse
        : MonTgt_B2B_Precommitment_Data, IMonTgt_H2G
    {
        #region Constructor
        public MonTgt_H2G_PC_PlayerEnrollmentResponse()
        {
        }
        #endregion Constructor

        #region Properties

        /// <summary>
        /// 0 –> Success. Other than 0 is considered as an error
        /// </summary>
        [DataMember]
        public byte ErrorCode { get; set; }

        /// <summary>
        /// Display message length
        /// </summary>
        [DataMember]
        public int DisplayMessageLength { get; set; }

        /// <summary>
        /// Display message
        /// </summary>
        [DataMember]
        public string DisplayMessage { get; set; }

        #endregion //Properties
    }

    #endregion //Player Enrollment Response
}
