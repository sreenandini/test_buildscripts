using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib;
using BMC.CoreLib.Concurrent;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    /// <summary>
    /// Freeform entity interface
    /// </summary>
    public interface IFreeformEntity
        : IFreeformEntityInformation,
        IDisposableObject,
        ICloneable,
        IExecutorKeyThread,
        ICommsUniqueEntity
    {
        IFreeformEntity Parent { get; }
        bool IsLeafNode { get; }
        bool IsRootNode { get; }
        bool IsSecured { get; set; }
        bool IsResponseRequired { get; set; }
        string TypeKey { get; }
        string TypeDescriptionKey { get; }
        FF_AppId_Encryption_Types EncryptionType { get; set; }

        IFreeformEntity_MsgTgt EncryptedTarget { get; set; }
        IList<IFreeformEntity_MsgTgt> Targets { get; }
        IFreeformEntity_MsgTgt PrimaryTarget { get; set; }
        void AddTarget(IFreeformEntity_MsgTgt target);
        void CopyTo(Stack<IFreeformEntity> stack);

        T GetTarget<T>()
            where T : IFreeformEntity;
        void GetTarget<T>(Action<T> doWork)
            where T : IFreeformEntity;
        IEnumerable<T> GetTargets<T>()
            where T : IFreeformEntity;

        string ToString();
        string ToStringDetail();
        void ToStringDetail(StringBuilder sb, string prefix);
        string EntityUniqueKeyDirection { get; }

        void FromRawData(byte[] rawData);
        byte[] ToRawData();

        string CombinedTargetNames { get; }
        string CombinedTargetDatas { get; }
    }

    public interface IFreeformEntityInformation
    {
        int UniqueEntityId { get; }
        int EntityId { get; }
        byte[] EntityData { get; set; }
        string EntityKey { get; }
        object Extra { get; set; }
        //string EntityKeyFull { get; }
    }

    public struct FreeformEntityKeyValue
    {
        private string _key;
        private FF_FlowDirection _value;
        private string _fullKey;

        public static FreeformEntityKeyValue Empty = new FreeformEntityKeyValue();

        public FreeformEntityKeyValue(string key, FF_FlowDirection value)
        {
            _key = key;
            _value = value;
            _fullKey = string.Format("{0}, {1}", _key, _value);
        }

        public string Key
        {
            get { return _key; }
            set { _key = value; }
        }

        public FF_FlowDirection Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public string FullKey
        {
            get { return _fullKey; }
        }

        public override string ToString()
        {
            return _fullKey;
        }
    }

    public class FreeformEntity
        : DisposableObject,
        IFreeformEntity
    {
        internal FreeformEntity_MsgTgt_Collection _targets = null;
        //internal string _entityKey = string.Empty;
        //internal string _entityKeyFull = string.Empty;
        private IFreeformEntity _parent = null;
        protected int _entityId = 0;
        private readonly string _typeKey = string.Empty;
        private FF_AppId_Encryption_Types _encryptionByte = FF_AppId_Encryption_Types.None;

        /// <summary>
        /// Initializes a new instance of the <see cref="FreeformEntity"/> class.
        /// </summary>
        public FreeformEntity()
        {
            _targets = new FreeformEntity_MsgTgt_Collection(this);
            _typeKey = this.GetType().Name;
        }

        public FreeformEntity(int entityId)
            : this()
        {
            _entityId = entityId;
        }

        public virtual string TypeKey
        {
            get { return _typeKey; }
        }

        public virtual string TypeDescriptionKey
        {
            get { return _typeKey; }
        }

        public virtual int UniqueEntityId { get { return _entityId; } }
        public virtual int EntityId { get { return _entityId; } }
        public byte[] EntityData { get; set; }
        public virtual string EntityKey
        {
            get { return string.Empty; }
            //set { _entityKey = value; }
        }

        //public string EntityKeyFull
        //{
        //    get { return _entityKeyFull; }
        //    internal set { _entityKeyFull = value; }
        //}

        public virtual string UniqueKey { get { return string.Empty; } }
        public object Extra { get; set; }

        public IFreeformEntity Parent
        {
            get { return _parent; }
            internal set { _parent = value; }
        }

        public virtual bool IsLeafNode { get { return false; } }

        public virtual bool IsRootNode { get { return false; } }

        public bool IsSecured { get; set; }

        public bool IsResponseRequired { get; set; }

        public virtual FF_AppId_Encryption_Types EncryptionType
        {
            get { return _encryptionByte; }
            set { _encryptionByte = value; }
        }

        public IFreeformEntity_MsgTgt EncryptedTarget { get; set; }

        public IFreeformEntity_MsgTgt PrimaryTarget
        {
            get { return _targets.PrimaryTarget; }
            set { _targets.PrimaryTarget = value; }
        }

        public IList<IFreeformEntity_MsgTgt> Targets
        {
            get { return _targets; }
        }

        protected virtual T GetPrimaryTarget<T>()
            where T : IFreeformEntity_MsgTgt
        {
            return (T)this.PrimaryTarget;
        }

        protected virtual void SetPrimaryTarget<T>(T value)
            where T : IFreeformEntity_MsgTgt
        {
            this.PrimaryTarget = value;
        }

        public void AddTarget(IFreeformEntity_MsgTgt target)
        {
            this.Targets.Add(target);
        }

        public void AddTargets(params IFreeformEntity_MsgTgt[] targets)
        {
            if (targets == null) return;
            foreach (var target in targets)
            {
                this.Targets.Add(target);
            }
        }

        protected bool GetBaseTargets<T>(Action<IEnumerable<T>> doWork)
            where T : IFreeformEntity
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "ToStringDetail"))
            {
                string typeOfT = typeof(T).FullName;

                try
                {
                    var items = (from t in this.Targets
                                 where t is T
                                 select t).OfType<T>();
                    doWork(items);
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return true;
            }
        }

        protected bool GetBaseTarget<T>(Action<T> doWork)
            where T : IFreeformEntity
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "GetBaseTarget<T>"))
            {
                string typeOfT = typeof(T).FullName;

                try
                {
                    var item = (from t in this.Targets
                                where t is T
                                select t).OfType<T>().FirstOrDefault();
                    if (item != null)
                    {
                        doWork(item);
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return true;
            }
        }

        public T GetTarget<T>()
            where T : IFreeformEntity
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "GetTarget<T>"))
            {
                T result = default(T);

                try
                {
                    this.GetBaseTarget<T>((e) =>
                    {
                        result = e;
                    });
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return result;
            }
        }

        public void GetTarget<T>(Action<T> doWork)
            where T : IFreeformEntity
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "GetTarget<T>"))
            {
                try
                {
                    this.GetBaseTarget<T>(doWork);
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }

        public IEnumerable<T> GetTargets<T>()
            where T : IFreeformEntity
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "GetTargets<T>"))
            {
                IEnumerable<T> result = default(IEnumerable<T>);

                try
                {
                    this.GetBaseTargets<T>((e) =>
                    {
                        result = e.ToList();
                    });
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return result;
            }
        }

        public void CopyTo(Stack<IFreeformEntity> stack)
        {
            if (this.Targets.Count == 0) return;
            foreach (var target in this.Targets)
            {
                stack.Push(target);
            }
        }

        public virtual int EntityUniqueKeyInt
        {
            get
            {
                if (this.EntityPrimaryTarget != null)
                    return this.EntityPrimaryTarget.EntityUniqueKeyInt;
                return 0;
            }
        }

        public virtual string EntityUniqueKeyString
        {
            get
            {
                if (this.EntityPrimaryTarget != null)
                    return this.EntityPrimaryTarget.EntityUniqueKeyString;
                return this.GetType().Name;
            }
        }

        public virtual int EntityPrimaryKeyId
        {
            get { return 0; }
        }

        public ICommsUniqueEntity EntityPrimaryTarget { get; set; }

        public virtual string ToStringDetail()
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "ToStringDetail"))
            {
                StringBuilder sb = new StringBuilder();

                try
                {
                    this.ToStringDetail(sb, string.Empty);
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return sb.ToString();
            }
        }

        /// <summary>
        /// Toes the string.
        /// </summary>
        /// <param name="sb">The sb.</param>
        public virtual void ToStringDetail(StringBuilder sb, string prefix)
        {
            if (this.Targets.Count > 0)
            {
                foreach (var target in this.Targets)
                {
                    target.ToStringDetail(sb, prefix);
                }
            }
        }

        public virtual void FromRawData(byte[] rawData) { }

        public virtual byte[] ToRawData() { return null; }

        public virtual string EntityUniqueKeyDirection
        {
            get { return string.Empty; }
        }

        /// <summary>
        /// Writes the log string line.
        /// </summary>
        /// <param name="sb">The sb.</param>
        /// <param name="prefix">The prefix.</param>
        public virtual void WriteLogStringLine(StringBuilder sb, string prefix)
        {
            FreeformHelper.WriteLogStringLine(sb, prefix);
        }

        /// <summary>
        /// Gets the index of the thread.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        /// <returns>Thread index.</returns>
        public virtual int GetThreadIndex(int capacity)
        {
            return 0;
        }

        public string CombinedTargetNames
        {
            get
            {
                using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "CombinedTargetNames"))
                {
                    StringBuilder result = new StringBuilder();

                    try
                    {
                        int count = 1;
                        foreach (var target in this.Targets)
                        {
                            if (result.Length > 0)
                            {
                                if (count > 3)
                                {
                                    result.AppendLine();
                                    count = 1;
                                }
                                else
                                {
                                    result.Append(", ");
                                }
                            }
                            result.Append(target.ToString());
                            count++;
                        }
                    }
                    catch (Exception ex)
                    {
                        method.Exception(ex);
                    }

                    return result.ToString();
                }
            }
        }

        public string CombinedTargetDatas
        {
            get
            {
                using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "CombinedTargetNames"))
                {
                    StringBuilder result = new StringBuilder();

                    try
                    {
                        foreach (var target in this.Targets)
                        {
                            result.AppendLine(target.ToStringDetail());
                        }
                    }
                    catch (Exception ex)
                    {
                        method.Exception(ex);
                    }

                    return result.ToString();
                }
            }
        }
    }

    public sealed class FreeformEntityList
        : List<IFreeformEntity> { }
}
