using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using BMC.CoreLib;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    public interface IFreeformEntity_MsgTgt : IFreeformEntity
    {
        int TargetID { get; set; }
        byte TargetLength { get; set; }
        string TargetKey { get; }
    }

    public interface IFreeformEntity_MsgTgt_Primary : IFreeformEntity_MsgTgt { }

    public sealed class FreeformEntity_MsgTgt_Collection_DebugView
    {
        private IList<IFreeformEntity_MsgTgt> _list;

        public FreeformEntity_MsgTgt_Collection_DebugView(IList<IFreeformEntity_MsgTgt> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }

            this._list = list;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public IFreeformEntity_MsgTgt[] Items
        {
            get
            {
                return _list.ToArray();
            }
        }
    }

    [Serializable]
    [ComVisible(false)]
    [DebuggerTypeProxy(typeof(FreeformEntity_MsgTgt_Collection_DebugView))]
    [DebuggerDisplay("Count = {Count}")]
    internal class FreeformEntity_MsgTgt_Collection : IList<IFreeformEntity_MsgTgt>
    {
        private readonly IList<IFreeformEntity_MsgTgt> _innerList = null;
        private readonly FreeformEntity _parent = null;
        private IFreeformEntity _grandParent = null;
        private readonly string DYN_MODULE_NAME = "FreeformEntity_MsgTgt_Collection";

        internal FreeformEntity_MsgTgt_Collection(FreeformEntity parent)
        {
            _parent = parent;
            _innerList = new List<IFreeformEntity_MsgTgt>();
        }

        private void SetParentInfo(IFreeformEntity_MsgTgt item)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "SetParentInfo"))
            {
                try
                {
                    ((FreeformEntity)item).Parent = _parent;

                    // i am primary target
                    if (item is IFreeformEntity_MsgTgt_Primary &&
                        _parent != null)
                    {
                        Stack<IFreeformEntity> st = new Stack<IFreeformEntity>();
                        st.Push(_parent);

                        while (st.Count != 0)
                        {
                            IFreeformEntity tgt = st.Pop();
                            if (tgt.IsRootNode ||
                                tgt.Parent == null)
                            {
                                tgt.EntityPrimaryTarget = item;
                                break;
                            }

                            if (tgt.Parent != null)
                                st.Push(tgt.Parent);
                        }
                    }
                    // transfer my primary target
                    if (item.EntityPrimaryTarget != null &&
                        _parent != null &&
                        _parent.IsRootNode &&
                        _parent.EntityPrimaryTarget == null)
                    {
                        _parent.EntityPrimaryTarget = item.EntityPrimaryTarget;
                        item.EntityPrimaryTarget = null;
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }

        public int IndexOf(IFreeformEntity_MsgTgt item)
        {
            return _innerList.IndexOf(item);
        }

        public void Insert(int index, IFreeformEntity_MsgTgt item)
        {
            this.SetParentInfo(item);
            _innerList.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _innerList.RemoveAt(index);
        }

        public IFreeformEntity_MsgTgt this[int index]
        {
            get
            {
                return _innerList[index];
            }
            set
            {
                _innerList[index] = value;
                this.SetParentInfo(value);
            }
        }

        public IFreeformEntity_MsgTgt PrimaryTarget
        {
            get
            {
                if (_innerList.Count > 0)
                    return _innerList[0];
                return null;
            }
            set
            {
                if (_innerList.Count > 0)
                {
                    _innerList[0] = value;
                }
                else
                {
                    _innerList.Add(value);
                }
                this.SetParentInfo(value);
            }
        }

        public void Add(IFreeformEntity_MsgTgt item)
        {
            this.SetParentInfo(item);
            _innerList.Add(item);
        }

        public void Clear()
        {
            _innerList.Clear();
        }

        public bool Contains(IFreeformEntity_MsgTgt item)
        {
            return _innerList.Contains(item);
        }

        public void CopyTo(IFreeformEntity_MsgTgt[] array, int arrayIndex)
        {
            _innerList.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _innerList.Count; }
        }

        public bool IsReadOnly
        {
            get { return _innerList.IsReadOnly; }
        }

        public bool Remove(IFreeformEntity_MsgTgt item)
        {
            return _innerList.Remove(item);
        }

        public IEnumerator<IFreeformEntity_MsgTgt> GetEnumerator()
        {
            return _innerList.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return ((System.Collections.IEnumerable)_innerList).GetEnumerator();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }

    public interface IFFTgt_G2H : IFreeformEntity_MsgTgt { }

    public interface IFFTgt_H2G : IFreeformEntity_MsgTgt { }

    public interface IFFTgt_B2B
        : IFFTgt_G2H, IFFTgt_H2G { }

    public interface IFFTgt_Override : IDisposable { }

    public class FreeformEntity_MsgTgt
        : FreeformEntity, IFreeformEntity_MsgTgt
    {
        protected int _targetID = 0;
        protected byte _targetLength = 0;

        public FreeformEntity_MsgTgt() { }

        public int TargetID
        {
            get { return _targetID; }
            set { _targetID = value; }
        }

        public byte TargetLength
        {
            get { return _targetLength; }
            set { _targetLength = value; }
        }

        public virtual string TargetKey { get { return string.Empty; } }

        public override int UniqueEntityId
        {
            get
            {
                return this.EntityId;
            }
        }

        public override string ToString()
        {
            if (this.PrimaryTarget != null)
                return this.PrimaryTarget.TypeDescriptionKey;
            return this.TypeDescriptionKey;
        }

        protected virtual void WriteLogTargetHeaderInfo(StringBuilder sb, string prefix)
        {
            this.WriteLogStringLine(sb, prefix);
            sb.Append(prefix + string.Format("Target => ID : {0:D}, Length : {1:D}", this.TargetID, this.TargetLength));
            if (this.EntityData != null)
            {
                FreeformHelper.ConvertBytesToHexString(this.EntityData, sb, ", Data : ");
            }
            sb.Append(Environment.NewLine);
            this.WriteLogStringLine(sb, prefix);
        }

        public override void ToStringDetail(StringBuilder sb, string prefix)
        {
            sb.Append(string.Format("ID : {0:D}, Length : {1:D}", this.TargetID, this.TargetLength));
            if (this.EntityData != null)
            {
                FreeformHelper.ConvertBytesToHexString(this.EntityData, sb, ", Data : ");
            }
        }
    }

    public class FFTgt_G2H : FreeformEntity_MsgTgt, IFFTgt_G2H { }

    public class FFTgt_H2G : FreeformEntity_MsgTgt, IFFTgt_H2G { }

    public class FFTgt_B2B : FreeformEntity_MsgTgt
    {
    }

    public class FFTgt_B2B_SubData<T>
        : FFTgt_B2B
        where T : FreeformEntity_MsgTgt
    {
        public T SubTargetData
        {
            get { return this.GetPrimaryTarget<T>(); }
            set { this.SetPrimaryTarget<T>(value); }
        }
    }
}
