using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib.Diagnostics;
using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace BMC.CoreLib.Collections
{
    #region BinaryTree
    [Serializable]
    [ComVisible(false)]
    [DebuggerTypeProxy(typeof(CoreLib_CollectionDebugView<>))]
    [DebuggerDisplay("Count = {Count}")]
    public class BinaryTree<T> : DisposableObject, ICollection<T>
    {
        private IComparer<T> _comparer = null;
        private BinaryTreeNode _rootNode = null;
        private BinaryTreeNode _minNode = null;
        private BinaryTreeNode _maxNode = null;
        private int _count = 0;

        private enum BinaryTreeColor
        {
            Black = 0,
            Red = 1
        }
        private enum NodePosition
        {
            Left = 0,
            Right = 1
        }

        private class BinaryTreeNode
        {
            public BinaryTreeNode Parent { get; set; }
            public BinaryTreeNode Left { get; set; }
            public BinaryTreeNode Right { get; set; }
            public T Value { get; set; }

            public BinaryTreeNode this[bool isLeft]
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

            public BinaryTreeNode this[NodePosition position]
            {
                get
                {
                    return (position == BinaryTree<T>.NodePosition.Left) ? this.Left : this.Right;
                }
                set
                {
                    if (position == BinaryTree<T>.NodePosition.Left)
                        this.Left = value;
                    else
                        this.Right = value;
                }
            }

            public bool IsRoot
            {
                get { return this.Parent == null; }
            }
            public bool IsLeaf
            {
                get { return (this.Left == null && this.Right == null); }
            }

            public override string ToString()
            {
                string value = string.Empty;
                value += "[ ";
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
                value += " ]";
                return value;
            }

            public BinaryTreeNode Minimum
            {
                get
                {
                    BinaryTreeNode x = this;
                    while (x.Left != null)
                    {
                        x = x.Left;
                    }
                    return x;
                }
            }

            public BinaryTreeNode Maximum
            {
                get
                {
                    BinaryTreeNode x = this;
                    while (x.Right != null)
                    {
                        x = x.Right;
                    }
                    return x;
                }
            }

            public BinaryTreeNode Successor
            {
                get
                {
                    BinaryTreeNode x = this;
                    BinaryTreeNode p = x.Parent;

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

            public BinaryTreeNode Predecessor
            {
                get
                {
                    BinaryTreeNode x = this;
                    BinaryTreeNode p = x.Parent;

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

        public BinaryTree()
            : this(Comparer<T>.Default) { }

        public BinaryTree(IComparer<T> comparer)
        {
            _comparer = comparer;
        }

        public bool IsEmpty
        {
            get { return (_count == 0); }
        }

        private class SearchResult
        {
            public BinaryTreeNode Node { get; set; }
            public BinaryTreeNode ParentNode { get; set; }
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
            BinaryTreeNode currentNode = _rootNode;
            SearchResult result = new BinaryTree<T>.SearchResult();

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

        private void IterateNodes(Func<BinaryTreeNode, bool> doWork)
        {
            if (_rootNode == null) return;
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "IterateNodes");
            Stack<BinaryTreeNode> st = new Stack<BinaryTreeNode>();

            try
            {
                BinaryTreeNode node = _rootNode;
                BinaryTreeNode current = null;
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
                lookup.Node = new BinaryTree<T>.BinaryTreeNode()
                {
                    Left = null,
                    Right = null,
                    Value = item,
                    Parent = lookup.ParentNode
                };
                BinaryTreeNode x = lookup.Node;
                BinaryTreeNode p = lookup.ParentNode;
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

        private void Transplant(BinaryTreeNode x, BinaryTreeNode y)
        {
            BinaryTreeNode p = x.Parent;

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
                BinaryTreeNode x = r.Node;

                // left is null
                if (x.Left == null)
                {
                    this.Transplant(x, x.Right);
                }
                else if (x.Right == null)
                {
                    this.Transplant(x, x.Left);
                }
                else
                {
                    BinaryTreeNode y = x.Right.Minimum;

                    if (y.Parent != x)
                    {
                        this.Transplant(y, y.Right);
                        y.Right = x.Right;
                        y.Right.Parent = y;
                    }

                    this.Transplant(x, y);
                    y.Left = x.Left;
                    y.Left.Parent = y;
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
            private BinaryTree<T> _parent;
            private BinaryTreeNode _node;
            private BinaryTreeNode _current;
            private DoubleLinkedListStack<BinaryTreeNode> _stack;

            public Enumerator(BinaryTree<T> parent)
            {
                _parent = parent;
                _node = _parent._rootNode;
                _stack = new DoubleLinkedListStack<BinaryTreeNode>();
                _current = null;
                this.Initialize();
            }

            private void Initialize()
            {
                BinaryTreeNode node = _node;
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
                BinaryTreeNode node = _current.Right;
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
