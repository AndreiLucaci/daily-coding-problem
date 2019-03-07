using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace DailyCodingProblem._2019.March
{
    public class Day7 : ISolution
    {
        public void Solve()
        {
            var input1 = new[] { 1, 2, 3, 4, 5 };
            var input2 = new[] { 3, 2, 1 };

            var expectedOutput1 = new[] { 120, 60, 40, 30, 24 };
            var expectedOutput2 = new[] { 2, 3, 6 };

            var solve1 = ProdArray(input1);
            var solve2 = ProdArray(input2);

            CollectionAssert.AreEqual(expectedOutput1, solve1, "The example 1 is not ok");
            CollectionAssert.AreEqual(expectedOutput2, solve2, "The example 2 is not ok");

            Console.WriteLine("Ok");
        }

        private IEnumerable<int> ProdArray(IReadOnlyCollection<int> input)
        {
            var result = new int[input.Count];

            for (var i = 0; i < input.Count; i++)
            {
                var prod = input.Where((t, j) => i != j).Aggregate(1, (current, t) => current * t);
                result[i] = prod;
            }

            return result;
        }
    }
}
