using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib;

namespace BMC.ExComms.Contracts.DTO
{
    public sealed class ExCommsPlayerFlags
        : DisposableObject
    {
        public readonly ExCommsPlayerFlag[] _flags = null;
        private readonly byte[] _emptyValues = new byte[3] { 0, 0, 0 };

        public ExCommsPlayerFlags()
        {
            _flags = new ExCommsPlayerFlag[] {
                new ExCommsPlayerFlag1(),
                new ExCommsPlayerFlag2(),
                new ExCommsPlayerFlag3(),
            };
        }
        public ExCommsPlayerFlag1 Flag1
        {
            get { return (ExCommsPlayerFlag1)_flags[0]; }
        }

        public ExCommsPlayerFlag2 Flag2
        {
            get { return (ExCommsPlayerFlag2)_flags[1]; }
        }

        public ExCommsPlayerFlag3 Flag3
        {
            get { return (ExCommsPlayerFlag3)_flags[2]; }
        }

        public byte[] BytesValue
        {
            get
            {
                return new byte[] 
                {
                    this.Flag1.ByteValue,
                    this.Flag2.ByteValue,
                    this.Flag3.ByteValue,
                };
            }
            set
            {
                if (value != null)
                {
                    if (value.Length >= 1) this.Flag1.ByteValue = value[0];
                    if (value.Length >= 2) this.Flag2.ByteValue = value[1];
                    if (value.Length >= 3) this.Flag3.ByteValue = value[2];
                }
            }
        }

        public void Clear()
        {
            this.BytesValue = _emptyValues;
        }

        protected override void ToString(StringBuilder sb)
        {
            for (int i = 0; i < _flags.Length; i++)
            {
                sb.Append(_flags[i].ToString());
            }
        }
    }

    public abstract class ExCommsPlayerFlag
        : DisposableObject
    {
        protected readonly bool[] _values = null;
        private byte _value = 0;

        public ExCommsPlayerFlag()
        {
            _values = new bool[8];
        }

        public bool this[int index]
        {
            get
            {
                return _values[index];
            }
            set
            {
                _values[index] = value;
                _value |= (byte)(_values[index] ? 1 : 0);
            }
        }

        public byte ByteValue
        {
            get { return _value; }
            set
            {
                _value = value;
                for (int i = 0, j = _values.Length - 1; i < _values.Length; i++, j--)
                {
                    _values[j] = (((value >> i) & 0x01) == 1);
                }
            }
        }

        protected override void ToString(StringBuilder sb)
        {
            for (int i = 0; i < _values.Length; i++)
            {
                sb.Append(_values[i] ? "1" : "0");
            }
        }
    }

    public sealed class ExCommsPlayerFlag1
        : ExCommsPlayerFlag
    {
        public bool Flag1
        {
            get { return this[0]; }
            set { this[0] = value; }
        }
        public bool Flag2
        {
            get { return this[1]; }
            set { this[1] = value; }
        }
        public bool Flag3
        {
            get { return this[2]; }
            set { this[2] = value; }
        }
        public bool Flag4
        {
            get { return this[3]; }
            set { this[3] = value; }
        }
        public bool Flag5
        {
            get { return this[4]; }
            set { this[4] = value; }
        }
        public bool Flag6
        {
            get { return this[5]; }
            set { this[5] = value; }
        }
        public bool Flag7
        {
            get { return this[6]; }
            set { this[6] = value; }
        }
        public bool Flag8
        {
            get { return this[7]; }
            set { this[7] = value; }
        }
    }

    public sealed class ExCommsPlayerFlag2
        : ExCommsPlayerFlag
    {
        public bool Flag_1
        {
            get { return this[0]; }
            set { this[0] = value; }
        }
        public bool Flag_2
        {
            get { return this[1]; }
            set { this[1] = value; }
        }
        public bool Flag_3
        {
            get { return this[2]; }
            set { this[2] = value; }
        }
        public bool Flag_4
        {
            get { return this[3]; }
            set { this[3] = value; }
        }
        public bool Flag_5
        {
            get { return this[4]; }
            set { this[4] = value; }
        }
        public bool Flag_6
        {
            get { return this[5]; }
            set { this[5] = value; }
        }
        public bool Flag_OfferOpt
        {
            get { return this[6]; }
            set { this[6] = value; }
        }
        public bool Flag_WithdrawCash
        {
            get { return this[7]; }
            set { this[7] = value; }
        }
    }

    public sealed class ExCommsPlayerFlag3
        : ExCommsPlayerFlag
    {
        public bool Flag_WithdrawPoints
        {
            get { return this[0]; }
            set { this[0] = value; }
        }
        public bool Flag_WithdrawPromo
        {
            get { return this[1]; }
            set { this[1] = value; }
        }
        public bool Flag_DepositPromo
        {
            get { return this[2]; }
            set { this[2] = value; }
        }
        public bool Flag_IsVIP
        {
            get { return this[3]; }
            set { this[3] = value; }
        }
        public bool Flag_DepositCash
        {
            get { return this[4]; }
            set { this[4] = value; }
        }
        public bool Flag_QueryAmount
        {
            get { return this[5]; }
            set { this[5] = value; }
        }
        public bool Flag_IsPINRequired
        {
            get { return this[6]; }
            set { this[6] = value; }
        }
        public bool Flag_IsECashPlayer
        {
            get { return this[7]; }
            set { this[7] = value; }
        }
    }
}
