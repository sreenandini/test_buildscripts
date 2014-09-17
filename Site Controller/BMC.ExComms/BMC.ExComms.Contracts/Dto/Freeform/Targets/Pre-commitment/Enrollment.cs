using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    #region Enrollment Parameter Request

    /// <summary>
    /// This message is sent from the GMU to host requesting enrollment parameters for pre-commitment.
    /// </summary>
    public class FFTgt_G2H_PC_EnrollmentParameterRequest 
        : FFTgt_G2H_PC_StatusRequest
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_PreCommitment_Action.EnrollmentParam;
            }
        }

    }
    #endregion

    #region Enrollment Parameter Response

    /// <summary>
    /// This message is from the host to GMU as a response to Enrollment Parameter Response. 
    /// </summary>
    public class FFTgt_H2G_PC_EnrollmentParameterResponse 
        : FFTgt_B2B_Precommitment_Data, IFFTgt_H2G
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_PreCommitment_Action.EnrollmentParam;
            }
        }

        ///<summary>
        ///Status; 0 – No error  
        ///</summary>  
        public bool Status { get; set; }
        ///<summary>
        ///Indicates if rules are based on Time spent on device per Day. 
        ///</summary>  
        public bool IsDayTimeBasis { get; set; }
        ///<summary>
        ///Default time for the Gaming Day. HHMM (HH – Hours; MM – Minutes)
        ///</summary>  
        public TimeSpan DayDefaultTime { get; set; }
        ///<summary>
        ///Indicates if day time basis is mandatory for enrollment 
        ///</summary>  
        public bool IsDayTimeBasisMandatory { get; set; }
        ///<summary>
        ///Indicates if rules are based on Gaming days per Week. 
        ///</summary>  
        public bool IsWeekTimeBasis { get; set; }
        ///<summary>
        ///Default time for the patron for the Gaming days per Week. (DD – Days) 
        ///</summary>  
        public byte WeekDefaultTime { get; set; }
        ///<summary>
        ///Indicates if week time basis is mandatory for enrollment
        ///</summary>  
        public bool IsWeekTimeBasisMandatory { get; set; }
        ///<summary>
        ///Indicates if rules are based on Gaming days per Month.
        ///</summary>  
        public bool IsMonthTimeBasis { get; set; }
        ///<summary>
        ///Default time for the patron for the Gaming days per Month. (DD – Days)
        ///</summary>  
        public byte MonthDefaultTime { get; set; }
        ///<summary>
        ///Indicates if month time basis is mandatory for enrollment
        ///</summary>  
        public bool IsMonthTimeBasisMandatory { get; set; }
        ///<summary>
        ///Indicates if rules are based on Net Loss per Gaming Day.
        ///</summary>  
        public bool IsDayLossBasis { get; set; }
        ///<summary>
        ///Default loss limit for the Gaming Day (in cents)
        ///</summary>  
        public double DayDefaultLossValue { get; set; }
        ///<summary>
        ///Indicates if day loss basis is mandatory for enrollment 
        ///</summary>  
        public bool IsDayLossBasisMandatory { get; set; }
        ///<summary>
        ///Indicates if rules are based on Net Loss for the Gaming days per Week.
        ///<summary>
        public bool IsWeekLossBasis { get; set; }
        ///<summary>
        ///Default Loss limit for the Gaming days per Week (in cents)    
        ///</summary>  
        public double WeekDefaultLossValue { get; set; }
        ///<summary>
        ///Indicates if week loss basis is mandatory for enrollment
        ///</summary>  
        public bool IsWeekLossBasisMandatory { get; set; }
        ///<summary>
        ///Indicates if rules are based on Net Loss for the Gaming days per Month. 
        ///</summary>  
        public bool IsMonthLossBasis { get; set; }
        ///<summary>
        ///Default Loss limit for the Gaming days per Month (in cents)   
        ///</summary>  
        public double MonthDefaultLossValue { get; set; }
        ///<summary>
        ///Indicates if month loss basis is mandatory for enrollment
        ///</summary>  
        public bool IsMonthLossBasisMandatory { get; set; }
        ///<summary>
        ///Indicates if rules are based on Total Wager (Turnover) per Gaming Day.
        ///</summary>  
        public bool IsDayWagerBasis { get; set; }
        ///<summary>
        ///Default Wager for the Gaming Day (in cents)   
        ///</summary>  
        public double DayDefaultWager { get; set; }
        ///<summary>
        ///Indicates if day wager basis is mandatory for enrollment
        ///</summary>  
        public bool IsDayWagerBasisMandatory { get; set; }
        ///<summary>
        ///Indicates if rules are based on Total Wager (Turnover) for the Gaming days per Week.  
        ///</summary>  
        public bool IsWeekWagerBasis { get; set; }
        ///<summary>
        ///Default Wager for the Gaming days per Week (in cents) 
        ///</summary>  
        public double WeekDefaultWager { get; set; }
        ///<summary>
        ///Indicates if week wager basis is mandatory for enrollment
        ///</summary>  
        public bool IsWeekWagerBasisMandatory { get; set; }
        ///<summary>
        ///Indicates if rules are based on Total Wager (Turnover) for the Gaming days per Month. 
        ///</summary>  
        public bool IsMonthWagerBasis { get; set; }
        ///<summary>
        ///Default Wager for the Gaming days per Month (in cents)
        ///</summary>  
        public double MonthDefaultWager { get; set; }
        ///<summary>
        ///Indicates if month wager basis is mandatory for enrollment    
        ///</summary>  
        public bool IsMonthWagerBasisMandatory { get; set; }
        ///<summary>
        ///Display message length
        ///</summary>  
        public byte DisplayMessageLength { get; set; }
        ///<summary>
        ///Display message 
        ///</summary>  
        public string DisplayMessage { get; set; }
    }
    #endregion

    #region Player Enrollment Request

    /// <summary>
    /// This message is sent from the GMU to host to enroll a player for pre commitment
    /// </summary>
    public class FFTgt_G2H_PC_PlayerEnrollmentRequest 
        : FFTgt_B2B_PC_Player_AccDetails, IFFTgt_G2H
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_PreCommitment_Action.EnrollmentResponse;
            }
        }

        ///<summary>
        ///Indicates if rules are based on Time spent on device per Day.
        ///</summary>
        public bool IsDayTimeBasis { get; set; }
        ///<summary>
        ///Target time for the Gaming Day. HHMM (HH – Hours; MM – Minutes) 
        ///</summary>
        public TimeSpan DayDefaultTime { get; set; }
        ///<summary>
        ///Indicates if rules are based on Gaming days per Week.  
        ///</summary>
        public bool IsWeekTimeBasis { get; set; }
        ///<summary>
        ///Target time for the patron for the Gaming days per Week. (DD – Days)  
        ///</summary>
        public byte WeekTargetTime { get; set; }
        ///<summary>
        ///Indicates if rules are based on Gaming days per Month. 
        ///</summary>
        public bool IsMonthTimeBasis { get; set; }
        ///<summary>
        ///Target time for the patron for the Gaming days per Month. (DD – Days) 
        ///</summary>
        public byte MonthTargetTime { get; set; }
        ///<summary>
        ///Indicates if rules are based on Net Loss per Gaming Day.    
        ///</summary>
        public bool IsDayLossBasis { get; set; }
        ///<summary>
        ///Target loss limit for the Gaming Day (in cents) 
        ///</summary>
        public double DayTargetLossValue { get; set; }
        ///<summary>
        ///Indicates if rules are based on Net Loss for the Gaming days per Week.
        ///</summary>
        public bool IsWeekLossBasis { get; set; }
        ///<summary>
        ///Target Loss limit for the Gaming days per Week (in cents) 
        ///</summary> 
        public double WeekTargetLossValue { get; set; }
        ///<summary>
        ///Indicates if rules are based on Net Loss for the Gaming days per Month.
        ///</summary> 
        public bool IsMonthLossBasis { get; set; }
        ///<summary>
        ///Target Loss limit for the Gaming days per Month (in cents)
        ///</summary>
        public double MonthTargetLossValue { get; set; }
        ///<summary>
        ///Indicates if rules are based on Total Wager (Turnover) per Gaming Day. 
        ///</summary>
        public bool IsDayWagerBasis { get; set; }
        ///<summary>
        ///Target Wager for the Gaming Day (in cents)
        ///</summary> 
        public double DayTargetWager { get; set; }
        ///<summary>
        ///Indicates if rules are based on Total Wager (Turnover) for the Gaming days per Week.    
        ///</summary>
        public bool IsWeekWagerBasis { get; set; }
        ///<summary>
        ///Target Wager for the Gaming days per Week (in cents) 
        ///</summary>
        public double WeekTargetWager { get; set; }
        ///<summary>
        ///Indicates if rules are based on Total Wager (Turnover) for the Gaming days per Month.   
        ///</summary>
        public bool IsMonthWagerBasis { get; set; }
        ///<summary>
        ///Target Wager for the Gaming days per Month (in cents)
        ///</summary> 
        public double MonthTargetWager { get; set; }
    }

    #endregion

    #region Player Enrollment Response
    /// <summary>
    /// This message is sent from the Host to GMU in response to player enrollment Request.
    /// </summary>
    public class FFTgt_H2G_PC_PlayerEnrollmentResponse 
        : FFTgt_B2B_Precommitment_Data, IFFTgt_H2G
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_PreCommitment_Action.Status;
            }
        }

        public bool IsError { get; set; }

        public int DisplayMessageLength { get; set; }

        public string DisplayMessage { get; set; }
    }
    #endregion
}
