using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{    
    public class FFTgt_G2H_FeaturesSupport_BMC :
        FFTgt_G2H
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_FeatureSupports_Types.BMCFeaturesSupport;
            }
        }
    }

    public class FFTgt_H2G_FeaturesSupport_BMC :
        FFTgt_H2G
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_FeatureSupports_Types.BMCFeaturesSupport;
            }
        }

        public byte FeaturesListLength { get; set; }
        public byte[] FeaturesList { get; set; } 
    }
}
