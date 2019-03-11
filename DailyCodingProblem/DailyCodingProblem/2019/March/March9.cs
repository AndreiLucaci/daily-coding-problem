using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace DailyCodingProblem._2019.March
{
    /// <summary>
    /// This problem was asked by Stripe.
    /// Given an array of integers, find the first missing positive integer in linear time and constant space. 
    /// In other words, find the lowest positive integer that does not exist in the array. 
    /// The array can contain duplicates and negative numbers as well.
    /// For example, the input [3, 4, -1, 1] should give 2. The input [1, 2, 0] should give 3.
    /// You can modify the input array in-place.
    /// </summary>
    public class March9 : BaseSolution
    {
        protected override void Solution()
        {
            var input1 = new[] {3, 4, -1, 1};
            var expectedOutput1 = 2;

            var input2 = new[] { 1, 2, 0 };
            var expectedOutput2 = 3;

            var input3 = new[] { 2, 3, 7, 6, 8, -1, -10, 15 };
            var expectedOutput3 = 1;

            var input4 = new[] {2234, 3432, -75, 1, 2, 3, 6, 282, 164, -104, 152};
            var expectedOutput4 = 4;

            var input5 = new[] {1, 1, 0, -1, -2};
            var expectedOutput5 = 2;

            var result1 = MissingPositiveIntegerSortedSet(input1);
            var result2 = MissingPositiveIntegerSortedSet(input2);
            var result3 = MissingPositiveIntegerSortedSet(input3);
            var result4 = MissingPositiveIntegerSortedSet(input4);
            var result5 = MissingPositiveIntegerSortedSet(input5);

            Assert.AreEqual(expectedOutput1, result1, "Case [1]: The expected output is not correct.");
            Assert.AreEqual(expectedOutput2, result2, "Case [2]: The expected output is not correct.");
            Assert.AreEqual(expectedOutput3, result3, "Case [3]: The expected output is not correct.");
            Assert.AreEqual(expectedOutput4, result4, "Case [4]: The expected output is not correct.");
            Assert.AreEqual(expectedOutput5, result5, "Case [5]: The expected output is not correct.");
        }

        /// <summary>
        /// complexity of time of O(n log(n)) and of space of O(n)
        /// </summary>
        private int MissingPositiveIntegerSortedSet(IEnumerable<int> arr)
        {
            var sortedSet = new SortedSet<int>(arr.Where(i => i > 0).Select(i => i));

            var value = 1;

            if (sortedSet.Any(i => value++ != i))
            {
                value--;
            }

            return value;
        }
    }
}
