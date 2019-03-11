using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using NUnit.Framework;


namespace DailyCodingProblem._2019.March
{
    public class March11 : ISolution
    {
        /// <summary>
        /// This problem was asked by Google.
        /// An XOR linked list is a more memory efficient doubly linked list. Instead of each node holding next and prev fields, 
        /// it holds a field named both, which is an XOR of the next node and the previous node. Implement an XOR linked list; 
        /// it has an add(element) which adds the element to the end, and a get(index) which returns the node at index.
        /// If using a language that has no pointers (such as Python), you can assume you have access to get_pointer and 
        /// dereference_pointer functions that converts between nodes and memory addresses.
        /// </summary>
        public void Solve()
        {
            var list = new List<int> { 3, 5, 1, 3, 2, 5 };
            var xorList = new XorLinkedList(list);

            CollectionAssert.AreEqual(list, xorList, "Case[1]: the expected list is not correct");

            Console.WriteLine("Ok");
        }
    }

    public unsafe class XorLinkedList : IDisposable, IEnumerable<int>
    {
        private readonly Node* _firstNode;
        private readonly Node* _secondNode;

        public XorLinkedList(IEnumerable<int> values)
        {
            using (var e = values.GetEnumerator())
            {
                if (!e.MoveNext())
                    throw new ArgumentException("XorLinkedList needs at least two elements");
                _firstNode = Allocate();
                var firstValue = e.Current;

                if (!e.MoveNext())
                    throw new ArgumentException("XorLinkedList needs at least two elements");
                _secondNode = Allocate(firstValue);
                _firstNode->value = e.Current;

                while (e.MoveNext())
                {
                    _firstNode = Insert(_firstNode, _secondNode, e.Current);
                }

                var newFirst = _secondNode;
                _secondNode = Next(_firstNode, _secondNode);
                _firstNode = newFirst;
            }
        }

        private static Node* Insert(Node* first, Node* second, int value)
        {
            var node = Allocate(CreateLink(first, second), value);
            var prev = Prev(first, second);
            first->xorLink = CreateLink(prev, node);
            var next = Next(first, second);
            second->xorLink = CreateLink(node, next);
            return node;
        }

        private static Node* Allocate(int value = 0)
        {
            return Allocate(null, value);
        }

        private static Node* Allocate(Node* link, int value = 0)
        {
            var node = (Node*)Marshal.AllocHGlobal(sizeof(Node));
            node->xorLink = link;
            node->value = value;
            return node;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct Node
        {
            internal int value;
            internal Node* xorLink;
        }

        private static Node* CreateLink(Node* a, Node* b)
        {
            return (Node*)((ulong)a ^ (ulong)b);
        }

        private static Node* Next(Node* first, Node* second)
        {
            return CreateLink(second->xorLink, first);
        }

        private static Node* Prev(Node* first, Node* second)
        {
            return Next(second, first);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            var first = _firstNode;
            var second = _secondNode;

            var start = first;
            while (true)
            {
                var next = Next(first, second);
                Marshal.FreeHGlobal((IntPtr)first);
                if (next == start)
                    break;
                first = second;
                second = next;
            }
            Marshal.FreeHGlobal((IntPtr)second);
        }

        #region Implementation of IEnumerable

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Implementation of IEnumerable<out int>

        private class XorEnumerator : IEnumerator<int>, IEnumerable<int>
        {
            private Node* _first;
            private Node* _second;
            private readonly Node* _start;
            private int _index = 0;

            public XorEnumerator(Node* first, Node* second)
            {
                _first = first;
                _second = second;
                _start = first;
            }

            #region Implementation of IDisposable

            public void Dispose()
            {
                // nothing to do
            }

            #endregion

            #region Implementation of IEnumerator

            public bool MoveNext()
            {
                switch (_index++)
                {
                    case 0:
                        Current = _first->value;
                        return true;
                    case 1:
                        Current = _second->value;
                        return true;
                    default:
                        var next = Next(_first, _second);
                        if (next == _start)
                            return false;

                        Current = next->value;
                        _first = _second;
                        _second = next;
                        return true;
                }
            }

            public void Reset()
            {
                throw new NotSupportedException("IEnumerator.Reset is not supported.");
            }

            object IEnumerator.Current => Current;

            #endregion

            #region Implementation of IEnumerator<out int>

            public int Current { get; private set; } = 0;

            #endregion

            #region Implementation of IEnumerable

            public IEnumerator<int> GetEnumerator()
            {
                return new XorEnumerator(_first, _second);
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            #endregion
        }

        public IEnumerator<int> GetEnumerator()
        {
            return new XorEnumerator(_firstNode, _secondNode);
        }

        public IEnumerable<int> GetReverse()
        {
            var last = Prev(_firstNode, _secondNode);
            return new XorEnumerator(last, Prev(last, _firstNode));
        }

        #endregion
    }
}
