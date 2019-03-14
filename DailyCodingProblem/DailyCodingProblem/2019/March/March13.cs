using System;
using System.Linq;
using NUnit.Framework;

namespace DailyCodingProblem._2019.March
{
    /// <summary>
    /// This problem was asked by Google.
    /// A unival tree (which stands for "universal value") is a tree where all nodes under it have the same value.
    /// Given the root to a binary tree, count the number of unival subtrees.
    /// For example, the following tree has 5 unival subtrees:
    ///    0
    ///   / \
    ///  1   0
    ///     / \
    ///    1   0
    ///   / \
    ///  1   1
    /// 
    /// </summary>
    public class March13 : BaseSolution
    {
        protected override void Solution()
        {
            var bst = new BineryTree<string>
            {
                Root = new BineryTree<string>.Node("0")
                {
                    Left = new BineryTree<string>.Node("1"),
                    Right = new BineryTree<string>.Node("0")
                    {
                        Left = new BineryTree<string>.Node("1")
                        {
                            Left = new BineryTree<string>.Node("1"),
                            Right = new BineryTree<string>.Node("1")
                        },
                        Right = new BineryTree<string>.Node("0")
                    }
                }
            };

            var univalCount = bst.CountUnival(bst.Root);
            var expectedCound = 5;
            
            Assert.AreEqual(expectedCound, univalCount, "Case [1]: the expected output is not correct.");
        }
    }

    internal class BineryTree<T>
        where T : IComparable<T>
    {
        public Node Root { get; set; }

        public int CountUnival(Node root)
        {
            if (root == null)
            {
                return 0;
            }

            var leftCount = CountUnival(root.Left);
            var rightCount = CountUnival(root.Right);

            return IsUnival(root) ? (1 + leftCount + rightCount) : (leftCount + rightCount);
        }

        private bool IsUnival(Node root)
        {
            return IsUnival(root, root.Value);
        }

        private bool IsUnival(Node root, T value)
        {
            if (root == null)
            {
                return true;
            }

            if (root.Value.Equals(value))
            {
                return IsUnival(root.Left, value) && IsUnival(root.Right, value);
            }

            return false;
        }

        public void Add(T value)
        {
            if (Root == null)
            {
                Root = new Node(value);
            }
            else
            {
                Insert(Root, value);
            }
        }

        private Node Insert(Node node, T value)
        {
            if (node == null)
            {
                node = new Node(value);
            }
            else if (node.Value.CompareTo(value) <= 0)
            {
                node.Left = Insert(node.Left, value);
            }
            else
            {
                node.Right = Insert(node.Right, value);
            }

            return node;
        }

        internal class Node
        {
            public Node Left { get; set; }
            public Node Right { get; set; }

            public T Value { get; set; }

            public Node(T value)
            {
                Value = value;
            }
        }
    }
}
