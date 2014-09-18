using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Security;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    public class FFTgt_B2B_AppQualifier
        : FFTgt_B2B, IFFTgt_B2B
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_TargetIds.ApplicationQualifier;
            }
        }

        public FFTgt_B2B_AppQualifier_Data QualifierData { get; set; }
    }

    public class FFTgt_B2B_AppQualifier_Data
        : FFTgt_B2B { }

    public class FFTgt_G2H_AppQualifier_PlayerCardId_Request
        : FFTgt_B2B_AppQualifier_Data, IFFTgt_G2H
    {
        #region Properties
        public String PlayerId { get; set; }
        #endregion

        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_ApplicationQualifier.PlayerCardId;
            }
        }
    }

    public class FFTgt_B2B_AppQualifier_PlayerCardId_Response
        : FFTgt_B2B_AppQualifier_Data, IFFTgt_H2G
    {
        #region Properties
        public bool IsQualified { get; set; }
        #endregion

        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_ApplicationQualifier.PlayerCardId;
            }
        }
    }

    public class FFTgt_B2B_AppQualifier_PlayerCardPresent_Request
        : FFTgt_B2B_AppQualifier_Data, IFFTgt_G2H
    {
        #region Properties
        public bool IsCardPresent { get; set; }
        #endregion

        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_ApplicationQualifier.PlayerCardPresent;
            }
        }
    }

    public class FFTgt_B2B_AppQualifier_PlayerCardPresent_Response
        : FFTgt_B2B_AppQualifier_Data, IFFTgt_H2G
    {
        #region Properties
        public bool IsQualified { get; set; }
        #endregion

        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_ApplicationQualifier.PlayerCardPresent;
            }
        }
    }

    public class FFTgt_G2H_AppQualifier_Unknown
        : FFTgt_B2B_AppQualifier_Data, IFFTgt_B2B
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_ApplicationQualifier.Unknown;
            }
        }
    }
}
