using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    internal abstract class FFFactory_Generic_B2B : FFFactory
    {
        internal FFFactory_Generic_B2B() { }

        internal override byte[] CreateBufferInternal(IFreeformEntity entity)
        {
            if (entity is IFreeformEntity_MsgTgt)
                return _targetParser.ParseEntity(entity);
            else
                return _messageParser.ParseEntity(entity);
        }

        internal override IFreeformEntity CreateEntityInternal(byte[] buffer, object extra)
        {
            // create the entity and parse
            IFreeformEntity_Msg entity = _messageParser.ParseBuffer(null, buffer) as IFreeformEntity_Msg;
            if (entity == null)
            {
                Log.Warning("Invalid message passed (Unable to convert the message)");
                return null;
            }
            else if (!entity.IsValid)
            {
                Log.Warning(string.Format("Invalid message passed (Checksum mismatch => Actual : {0:D}), Calculated : {1:D})", entity.Checksum, entity.ChecksumCalculated));
                entity.Dispose();
                return null;
            }
            entity.IpAddress = extra.ToString();

            // valid message
            _targetParser.ParseBuffer(entity, entity, entity.EntityData, 0, entity.DataLength);

            // convert the encrypted targets
            if (entity.EncryptedTarget != null &&
                entity.Targets.Contains(entity.EncryptedTarget))
            {
                FFTgt_B2B_Encrypted encryptedTarget = entity.EncryptedTarget as FFTgt_B2B_Encrypted;
                entity.Targets.Remove(encryptedTarget);
                _targetParser.ParseBuffer(entity, entity, encryptedTarget.DecryptedData, 0, encryptedTarget.DecryptedData.Length);
            } 
            return entity;
        }

        internal override IFreeformEntity CreateEntityInternal(FFCreateEntityRequest request)
        {
            return _messageParser.CreateEntity(request);
        }
    }

    internal abstract class FFFactory_Generic_G2H : FFFactory_Generic_B2B
    {
        internal FFFactory_Generic_G2H() { }

        public override FF_FlowDirection Direction
        {
            get { return FF_FlowDirection.G2H; }
        }
    }

    internal abstract class FFFactory_Generic_H2G : FFFactory_Generic_B2B
    {
        internal FFFactory_Generic_H2G() { }

        public override FF_FlowDirection Direction
        {
            get { return FF_FlowDirection.H2G; }
        }
    }
}
