using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    internal class FFParser_Tgt_Generic_Security 
        : FFTgtParser_NoSubTargets
    {
        internal FFParser_Tgt_Generic_Security()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_B2B_Security tgt = new FFTgt_B2B_Security();
            entity = tgt;
            this.ParseBuffer(entity, rootEntity, buffer, 0, buffer.Length);
            return tgt;
        }
    }

    internal class FFParser_Tgt_MC300_Security 
        : FFParser_Tgt_Generic_Security
    {
        internal FFParser_Tgt_MC300_Security()
            : base() { }
    }

    internal class FFParser_Tgt_MC300_Security_G2H 
        : FFParser_Tgt_MC300_Security
    {
        internal FFParser_Tgt_MC300_Security_G2H()
            : base()
        {
            this.AddSubParsers();
        }

        public override FF_FlowInitiation FlowInitiation
        {
            get
            {
                return FF_FlowInitiation.Gmu;
            }
        }

        protected override void AddSubParsersInternal()
        {
            this.AddBufferEntityParser((int)FF_GmuId_Security_Encryption_Types.SDSKeyExchange, (int)FF_AppId_Security_Encryption_Types.SDSKeyExchange, 
                new FFParser_Tgt_MC300_Security_KeyExchange_GmuInitiated_G2H());
            this.AddBufferEntityParser((int)FF_GmuId_Security_Encryption_Types.SDSKeyExchange, (int)FF_AppId_Security_Encryption_Types.SDSKeyExchange,
                new FFParser_Tgt_MC300_Security_KeyExchange_HostInitiated_H2G());
            //this.AddBufferEntityParser((int)FF_GmuId_Security_Encryption_Types.SDSEncryption, (int)FF_AppId_Security_Encryption_Types.SDSEncryption, new FFParser_Tgt_MC300_BMCEncryption_GmuInit_G2H());
            //this.AddBufferEntityParser((int)FF_GmuId_Security_Encryption_Types.SDSKeyExchange, (int)FF_AppId_Security_Encryption_Types.SDSKeyExchange, new FFParser_Tgt_MC300_Security_KeyExchange_G2H());
            //this.AddBufferEntityParser((int)FF_GmuId_Security_Encryption_Types.SDSKeyExchange, (int)FF_AppId_Security_Encryption_Types.SDSEncryptionAuthentication, new FFParser_Tgt_MC300_Security_KeyExchange_G2H());
            //this.AddBufferEntityParser((int)FF_GmuId_Security_Encryption_Types.SDSEncryptionAuthentication, (int)FF_AppId_Security_Encryption_Types.SDSEncryptionAuthentication, new FFParser_Tgt_MC300_BMCEncryptAuth_G2H());
            //this.AddBufferEntityParser((int)FF_GmuId_Security_Encryption_Types.IndexedKeyExchangeStart, (int)FF_AppId_Security_Encryption_Types.IndexedKeyExchangeStart, new FFParser_Tgt_MC300_IndexKeyExchange_Start_G2H());
            //this.AddBufferEntityParser((int)FF_GmuId_Security_Encryption_Types.IndexedKeyExchangeEnd, (int)FF_AppId_Security_Encryption_Types.IndexedKeyExchangeEnd, new FFParser_Tgt_MC300_IndexKeyExchange_End_G2H());
            //this.AddBufferEntityParser((int)FF_GmuId_Security_Encryption_Types.AFTKeyExchangeEnd, (int)FF_AppId_Security_Encryption_Types.AFTKeyExchangeEnd, new FFParser_Tgt_MC300_Security_KeyExchange_End_G2H());
        }
    }

    internal class FFParser_Tgt_MC300_Security_H2G 
        : FFParser_Tgt_MC300_Security
    {
        internal FFParser_Tgt_MC300_Security_H2G()
            : base()
        {
            this.AddSubParsers();
        }

        public override FF_FlowInitiation FlowInitiation
        {
            get
            {
                return FF_FlowInitiation.Host;
            }
        }

        protected override void AddSubParsersInternal()
        {
            this.AddBufferEntityParser((int)FF_GmuId_Security_Encryption_Types.SDSKeyExchange, (int)FF_AppId_Security_Encryption_Types.SDSKeyExchange,
                new FFParser_Tgt_MC300_Security_KeyExchange_GmuInitiated_H2G());
            this.AddBufferEntityParser((int)FF_GmuId_Security_Encryption_Types.SDSKeyExchange, (int)FF_AppId_Security_Encryption_Types.SDSKeyExchange,
                new FFParser_Tgt_MC300_Security_KeyExchange_HostInitiated_G2H());
            //this.AddBufferEntityParser((int)FF_GmuId_Security_Encryption_Types.SDSEncryption, (int)FF_AppId_Security_Encryption_Types.SDSEncryption,
            //    new FFParser_Tgt_MC300_BMCEncryption_GmuInit_G2H());

            //this.AddBufferEntityParser((int)FF_GmuId_Security_Encryption_Types.SDSEncryptionAuthentication, (int)FF_AppId_Security_Encryption_Types.SDSEncryptionAuthentication, new FFParser_Tgt_MC300_BMCEncryptAuth_H2G());
            //this.AddBufferEntityParser((int)FF_GmuId_Security_Encryption_Types.IndexedKeyExchangeStart, (int)FF_AppId_Security_Encryption_Types.IndexedKeyExchangeStart, new FFParser_Tgt_MC300_IndexKeyExchange_Start_H2G());
            //this.AddBufferEntityParser((int)FF_GmuId_Security_Encryption_Types.IndexedKeyExchangeEnd, (int)FF_AppId_Security_Encryption_Types.IndexedKeyExchangeEnd, new FFParser_Tgt_MC300_IndexKeyExchange_End_H2G());
        }
    }
}
