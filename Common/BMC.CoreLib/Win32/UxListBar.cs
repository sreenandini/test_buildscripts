using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BMC.CoreLib.Win32
{
    public partial class UxListBar 
        : UserControl
    {
        public UxListBar()
        {
            InitializeComponent();
        }
    }

    public class UxListBarItems 
        : IList<UxListBarItem>
    {

        public int IndexOf(UxListBarItem item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, UxListBarItem item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public UxListBarItem this[int index]
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void Add(UxListBarItem item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(UxListBarItem item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(UxListBarItem[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        public bool Remove(UxListBarItem item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<UxListBarItem> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }

    public class UxListBarItem
        : DisposableObjectNotify
    {
        public UxListBarItem() { }

        #region Text
        private string _text = default(string);

        public string Text
        {
            get { return _text; }
            set
            {
                this.SetProperty<string>(ref _text, value, "Text");
            }
        }
        #endregion 
    }
}
