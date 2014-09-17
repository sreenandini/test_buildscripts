using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    public class FFTgt_H2G_FeaturesSupport_GMU :
        FFTgt_H2G
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_FeatureSupports_Types.GMUFeaturesSupport;
            }
        }
    }

    public class FFTgt_G2H_FeaturesSupport_GMU :
        FFTgt_G2H
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_FeatureSupports_Types.GMUFeaturesSupport;
            }
        }

        public byte FeaturesListLength { get; set; }
        public byte[] FeaturesList { get; set; }
    }
}
