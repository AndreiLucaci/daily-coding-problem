using System;
using NUnit.Framework;

namespace DailyCodingProblem._2019.March
{
    /// <summary>
    /// Given the root to a binary tree, implement serialize(root), which serializes the tree into a string, and deserialize(s),
    ///  which deserializes the string back into the tree.
    /// For example, given the following Node class
    /// class Node:
    ///     def __init__(self, val, left=None, right=None):
    ///         self.val = val
    ///         self.left = left
    ///         self.right = right
    /// The following test should pass:
    /// node = Node('root', Node('left', Node('left.left')), Node('right'))
    /// assert deserialize(serialize(node)).left.left.val == 'left.left'
    /// </summary>
    public class March8 : ISolution
    {
        public void Solve()
        {
            var expectedString = "left.left";
            var input = new Node("root", new Node("left", new Node(expectedString)), new Node("right"));

            var result = Serialize(input);
            var resultNode = Deserialize(result);

            Assert.AreEqual(expectedString, resultNode.Left.Left.Val);

            Console.WriteLine("Ok");
        }

        private const string End = "END";

        private static string Serialize(Node node)
        {
            var str = End;

            if (node == null)
            {
                return str;
            }

            str += node.Val;
            str += Serialize(node.Left);
            str += Serialize(node.Rigth);

            return str.Trim();
        }

        private static Node Deserialize(string input)
        {
            var localStr = input;
            return Deserialize(ref localStr);
        }

        private static Node Deserialize(ref string input)
        {
            if (input.IndexOf(End, End.Length, StringComparison.Ordinal) == -1)
            {
                return null;
            }

            var val = input.Substring(End.Length, input.Substring(End.Length).IndexOf(End, StringComparison.Ordinal));
            if (string.IsNullOrEmpty(val))
            {
                return null;
            }
            
            var root = new Node(val);
            input = input.Substring(input.IndexOf(End, End.Length, StringComparison.Ordinal)).Trim();
            root.Left = Deserialize(ref input);
            input = input.Substring(input.IndexOf(End, End.Length, StringComparison.Ordinal)).Trim();
            root.Rigth = Deserialize(ref input);

            return root;
        }

        private class Node
        {
            public Node(string val, Node left = null, Node right = null)
            {
                this.Val = val;
                this.Left = left;
                this.Rigth = right;
            }

            public Node Rigth { get; set; }

            public Node Left { get; set; }

            public string Val { get; private set; }
        }
    }
}
