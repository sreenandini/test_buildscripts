using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib.Diagnostics;
using System.Collections;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace BMC.CoreLib.Collections
{
    #region Rbtree
    [Serializable]
    [ComVisible(false)]
    [DebuggerTypeProxy(typeof(CoreLib_CollectionDebugView<>))]
    [DebuggerDisplay("Count = {Count}")]
    public class Rbtree<T> : DisposableObject, ICollection<T>
    {
        private IComparer<T> _comparer = null;
        private RbtreeNode _rootNode = null;
        private RbtreeNode _minNode = null;
        private RbtreeNode _maxNode = null;
        private int _count = 0;

        private enum RbtreeColor
        {
            Black = 0,
            Red = 1
        }
        private enum NodePosition
        {
            Left = 0,
            Right = 1
        }

        private class RbtreeNode
        {
            public RbtreeNode Parent { get; set; }
            public RbtreeNode Left { get; set; }
            public RbtreeNode Right { get; set; }
            public RbtreeColor Color { get; set; }
            public T Value { get; set; }

            public RbtreeNode p { get { return this.Parent; } }

            public RbtreeNode this[bool isLeft]
            {
                get
                {
                    return this[isLeft ? NodePosition.Left : NodePosition.Right];
                }
                set
                {
                    this[isLeft ? NodePosition.Left : NodePosition.Right] = value;
                }
            }

            public RbtreeNode this[NodePosition position]
            {
                get
                {
                    return (position == Rbtree<T>.NodePosition.Left) ? this.Left : this.Right;
                }
                set
                {
                    if (position == Rbtree<T>.NodePosition.Left)
                        this.Left = value;
                    else
                        this.Right = value;
                }
            }

            public void SetColorBlack()
            {
                this.Color = Rbtree<T>.RbtreeColor.Black;
            }

            public void SetColorRed()
            {
                this.Color = Rbtree<T>.RbtreeColor.Red;
            }

            public bool IsRoot
            {
                get { return this.Parent == null; }
            }
            public bool IsLeaf
            {
                get { return (this.Left == null && this.Right == null); }
            }
            public bool IsRed
            {
                get { return this.Color == Rbtree<T>.RbtreeColor.Red; }
            }
            public bool IsBlack
            {
                get { return this.Color == Rbtree<T>.RbtreeColor.Black; }
            }

            public override string ToString()
            {
                string value = string.Empty;
                value += (this.IsBlack ? "( " : "[ ");
                if (this.Left != null && this.Left.Value != null)
                {
                    value += " " + this.Left.Value + " <-- ";
                }
                if (this.Value != null)
                {
                    value += this.Value.ToString();
                }
                if (this.Right != null && this.Right.Value != null)
                {
                    value += " --> " + this.Right.Value;
                }
                value += (this.IsBlack ? " )" : " ]");
                return value;
            }

            public RbtreeNode Minimum
            {
                get
                {
                    RbtreeNode x = this;
                    while (x.Left != null)
                    {
                        x = x.Left;
                    }
                    return x;
                }
            }

            public RbtreeNode Maximum
            {
                get
                {
                    RbtreeNode x = this;
                    while (x.Right != null)
                    {
                        x = x.Right;
                    }
                    return x;
                }
            }

            public RbtreeNode Successor
            {
                get
                {
                    RbtreeNode x = this;
                    RbtreeNode p = x.Parent;

                    if (x.Right != null)
                    {
                        return x.Right.Minimum;
                    }

                    while (((p = x.Parent) != null) &&
                        (x == p.Right))
                    {
                        x = p;
                    }
                    return p;
                }
            }

            public RbtreeNode Predecessor
            {
                get
                {
                    RbtreeNode x = this;
                    RbtreeNode p = x.Parent;

                    if (x.Left != null)
                    {
                        return x.Left.Maximum;
                    }

                    while (((p = x.Parent) != null) &&
                        (x == p.Left))
                    {
                        x = p;
                    }
                    return p;
                }
            }
        }

        public Rbtree()
            : this(Comparer<T>.Default) { }

        public Rbtree(IComparer<T> comparer)
        {
            _comparer = comparer;
        }

        public bool IsEmpty
        {
            get { return (_count == 0); }
        }

        private class SearchResult
        {
            public RbtreeNode Node { get; set; }
            public RbtreeNode ParentNode { get; set; }
            public int Position { get; set; }
            public bool IsLeft
            {
                get
                {
                    return (this.Position > 0);
                }
            }
        }

        private SearchResult Search(T key)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "DoLookup");
            RbtreeNode currentNode = _rootNode;
            SearchResult result = new Rbtree<T>.SearchResult();

            try
            {
                while (currentNode != null)
                {
                    result.Position = _comparer.Compare(currentNode.Value, key);
                    if (result.Position == 0)
                    {
                        result.Node = currentNode;
                        return result;
                    }

                    result.ParentNode = currentNode;
                    if (result.Position > 0) currentNode = currentNode.Left;
                    else currentNode = currentNode.Right;
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        private void IterateNodes(Func<RbtreeNode, bool> doWork)
        {
            if (_rootNode == null) return;
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "IterateNodes");
            Stack<RbtreeNode> st = new Stack<RbtreeNode>();

            try
            {
                RbtreeNode node = _rootNode;
                RbtreeNode current = null;
                while (node != null)
                {
                    st.Push(node);
                    node = node.Left;
                }

                while (st.Count != 0)
                {
                    current = st.Pop();
                    if (doWork != null)
                    {
                        if (!doWork(current))
                        {
                            return;
                        }
                    }

                    node = current.Right;
                    while (node != null)
                    {
                        st.Push(node);
                        node = node.Left;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        #region ICollection<T> Members

        public void Add(T item)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Add");

            try
            {
                SearchResult lookup = this.Search(item);
                if (lookup.Node != null)
                {
                    return;
                }

                // new node
                lookup.Node = new Rbtree<T>.RbtreeNode()
                {
                    Color = Rbtree<T>.RbtreeColor.Red,
                    Left = null,
                    Right = null,
                    Value = item,
                    Parent = lookup.ParentNode
                };
                RbtreeNode x = lookup.Node;
                RbtreeNode p = lookup.ParentNode;
                bool isLeft = lookup.IsLeft;
                _count++;

                if (p != null)
                {
                    if (isLeft)
                    {
                        if (p == _minNode)
                            _minNode = x;
                    }
                    else
                    {
                        if (p == _maxNode)
                            _maxNode = x;
                    }
                    p[isLeft] = x;
                }
                else
                {
                    _minNode = x;
                    _maxNode = x;
                    _rootNode = x;
                }

                // rotations
                while (((p = x.Parent) != null) &&
                    p.IsRed)
                {
                    RbtreeNode g = p.Parent;

                    // if parent is at left of grandpa
                    if (p == g.Left)
                    {
                        RbtreeNode u = g.Right;

                        // if uncle is red
                        if ((u != null) && (u.IsRed))
                        {
                            p.SetColorBlack();
                            u.SetColorBlack();
                            g.SetColorRed();
                            x = g;
                        }
                        else
                        {
                            if (x == p.Right)
                            {
                                this.RotateLeft(p);
                                x = p;
                                p = x.Parent;
                            }
                            p.SetColorBlack();
                            g.SetColorRed();
                            this.RotateRight(g);
                        }
                    }
                    else // right of parent
                    {
                        RbtreeNode u = g.Left;

                        // if uncle is red
                        if ((u != null) && (u.IsRed))
                        {
                            p.SetColorBlack();
                            u.SetColorBlack();
                            g.SetColorRed();
                            x = g;
                        }
                        else
                        {
                            if (x == p.Left)
                            {
                                this.RotateRight(p);
                                x = p;
                                p = x.Parent;
                            }
                            p.SetColorBlack();
                            g.SetColorRed();
                            this.RotateLeft(g);
                        }
                    }
                }

                // root always black
                _rootNode.Color = Rbtree<T>.RbtreeColor.Black;
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        public void AddRange(params T[] items)
        {
            if (items != null)
            {
                foreach (var item in items)
                {
                    this.Add(item);
                }
            }
        }

        private void RotateLeft(RbtreeNode node)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "RotateLeft");

            try
            {
                RbtreeNode x = node;
                RbtreeNode y = node.Right;
                RbtreeNode parent = x.Parent;

                if (!x.IsRoot)
                {
                    if (x == parent.Left)
                        parent.Left = y;
                    else
                        parent.Right = y;
                }
                else
                {
                    _rootNode = y;
                }

                y.Parent = parent;
                x.Parent = y;

                x.Right = y.Left;
                if (x.Right != null)
                {
                    x.Right.Parent = x;
                }
                y.Left = x;
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        private void RotateRight(RbtreeNode node)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "RotateRight");

            try
            {
                RbtreeNode x = node;
                RbtreeNode y = node.Left;
                RbtreeNode parent = x.Parent;

                if (!x.IsRoot)
                {
                    if (x == parent.Left)
                        parent.Left = y;
                    else
                        parent.Right = y;
                }
                else
                {
                    _rootNode = y;
                }

                y.Parent = parent;
                x.Parent = y;

                x.Left = y.Right;
                if (x.Left != null)
                {
                    x.Left.Parent = x;
                }
                y.Right = x;
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        public void Clear()
        {
            _rootNode = null;
        }

        public bool Contains(T item)
        {
            SearchResult result = this.Search(item);
            return (result.Node != null);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if ((array == null) ||
                (arrayIndex < 0) ||
                ((array.Length - arrayIndex) < Count)) return;

            this.IterateNodes((x) =>
            {
                array[arrayIndex++] = x.Value;
                return true;
            });
        }

        public int Count
        {
            get { return _count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        private void Transplant(RbtreeNode x, RbtreeNode y)
        {
            RbtreeNode p = x.Parent;

            if (p == null)
            {
                _rootNode = y;
            }
            else
            {
                if (p.Left == x)
                {
                    p.Left = y;
                }
                else
                {
                    p.Right = y;
                }
            }

            if (y != null)
            {
                y.Parent = p;
            }
        }

        public bool Remove(T item)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Remove");
            bool result = default(bool);

            try
            {
                SearchResult r = this.Search(item);
                if (r.Node == null)
                {
                    // key not found
                    return false;
                }
                RbtreeNode x = r.Node;
                RbtreeNode z = null;
                RbtreeNode y = x;
                RbtreeColor y_color = y.Color;

                // left is null
                if (x.Left == null)
                {
                    z = x.Right;
                    this.Transplant(x, z);
                }
                else if (x.Right == null)
                {
                    z = x.Left;
                    this.Transplant(x, z);
                }
                else
                {
                    y = x.Right.Minimum;
                    y_color = y.Color;
                    z = y.Right;

                    if (y.Parent != x)
                    {
                        this.Transplant(y, y.Right);
                        y.Right = x.Right;
                        y.Right.Parent = y;
                    }
                    else
                    {
                        z.Parent = y;
                    }

                    this.Transplant(x, y);
                    y.Left = x.Left;
                    y.Left.Parent = y;
                    y.Color = x.Color;
                }

                if (y_color == Rbtree<T>.RbtreeColor.Black)
                {
                    //fix up
                    x = z;
                    while (x != null &&
                        x != _rootNode &&
                        x.IsBlack)
                    {
                        if (x.p.Left == x)
                        {
                            RbtreeNode w = x.p.Right;
                            if (w.IsRed)
                            {
                                w.SetColorBlack();
                                x.p.SetColorRed();
                                this.RotateLeft(x.p);
                                w = x.p.Right;
                            }
                            if (w.Left != null && w.Right != null)
                            {
                                if (w.Left.IsBlack && w.Right.IsBlack)
                                {
                                    w.SetColorRed();
                                    x = x.p;
                                }
                                else if (w.Right.IsBlack)
                                {
                                    w.Left.SetColorBlack();
                                    w.SetColorRed();
                                    this.RotateRight(w);
                                    w = x.p.Right;
                                }
                                w.Color = w.p.Color;
                                x.p.SetColorBlack();
                                w.Right.SetColorBlack();
                                this.RotateLeft(x.p);
                                x = _rootNode;
                            }
                        }
                        else
                        {
                            RbtreeNode w = x.p.Left;
                            if (w.IsRed)
                            {
                                w.SetColorBlack();
                                x.p.SetColorRed();
                                this.RotateRight(x.p);
                                w = x.p.Left;
                            }
                            if (w.Left != null && w.Right != null)
                            {
                                if (w.Left.IsBlack && w.Right.IsBlack)
                                {
                                    w.SetColorRed();
                                    x = x.p;
                                }
                                else if (w.Left.IsBlack)
                                {
                                    w.Right.SetColorBlack();
                                    w.SetColorRed();
                                    this.RotateLeft(w);
                                    w = x.p.Left;
                                }
                                w.Color = w.p.Color;
                                x.p.SetColorBlack();
                                w.Left.SetColorBlack();
                                this.RotateRight(x.p);
                                x = _rootNode;
                            }
                        }
                    }

                    if (x != null)
                    {
                        x.SetColorBlack();
                    }
                }
                _count--;
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        #endregion

        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(this);
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return new Enumerator(this);
        }

        #endregion

        private struct Enumerator : IEnumerator<T>, IEnumerator
        {
            private Rbtree<T> _parent;
            private RbtreeNode _node;
            private RbtreeNode _current;
            private DoubleLinkedListStack<RbtreeNode> _stack;

            public Enumerator(Rbtree<T> parent)
            {
                _parent = parent;
                _node = _parent._rootNode;
                _stack = new DoubleLinkedListStack<RbtreeNode>();
                _current = null;
                this.Initialize();
            }

            private void Initialize()
            {
                RbtreeNode node = _node;
                while (node != null)
                {
                    _stack.Push(node);
                    node = node.Left;
                }
            }

            #region IEnumerator<T> Members

            public T Current
            {
                get { return _current.Value; }
            }

            #endregion

            #region IEnumerator Members

            object IEnumerator.Current
            {
                get { return _current; }
            }

            public bool MoveNext()
            {
                if (_stack.Count == 0)
                {
                    _current = null;
                    return false;
                }

                _current = _stack.Pop();
                RbtreeNode node = _current.Right;
                while (node != null)
                {
                    _stack.Push(node);
                    node = node.Left;
                }
                return true;
            }

            public void Reset()
            {
                _stack.Clear();
                this.Initialize();
            }

            #endregion

            #region IDisposable Members

            public void Dispose()
            {

            }

            #endregion
        }
    }
    #endregion
}
