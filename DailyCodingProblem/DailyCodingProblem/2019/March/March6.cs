using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using NUnit.Framework;

namespace DailyCodingProblem._2019.March
{
	/// <summary>
	/// This problem was recently asked by Google.
	/// Given a list of numbers and a number k, return whether any two numbers from the list add up to k.
	/// For example, given [10, 15, 3, 7] and k of 17, return true since 10 + 7 is 17.
	/// Bonus: Can you do this in one pass?
	/// </summary>
	public class March6 : BaseSolution
    {
	    protected override void Solution()
	    {
		    var input = new[] {10, 15, 3, 7};
		    var target = 17;
		    var expectedResult = true; // 10 + 7 = 17

		    var result = ExistsBinarySearch(input, target);

			Assert.AreEqual(expectedResult, result, "The answer is not correct");
	    }

		/// <summary>
		/// Complexity of O(n^2)
		/// </summary>
	    private bool Exists(IReadOnlyList<int> input, int target)
	    {
		    return input.Where((t1, i) => input.Where((t, j) => i != j).Any(t => t1 + t == target)).Any();
	    }

		/// <summary>
		/// Complexity of O(n log(n))
		/// </summary>
	    private bool ExistsBinarySearch(IEnumerable<int> input, int target)
	    {
		    var list = input.ToList();
			list.Sort();

		    return list.Any(i => BinarySearch(list, target - i) >= 0);
	    }

		/// <summary>
		/// Threw in an own implementaiton of the Binary Search, I know list has one, but hey, it's algo time
		/// </summary>
	    private int BinarySearch(IReadOnlyList<int> input, int target)
	    {
		    var minIndex = 0;
		    var maxIndex = input.Count - 1;

		    while (minIndex <= maxIndex)
		    {
			    var midIndex = (minIndex + maxIndex) / 2;

			    if (input[midIndex] == target)
			    {
				    return midIndex + 1;
				}
			    if (input[midIndex] > target)
			    {
				    minIndex = midIndex + 1;
			    }
			    else
			    {
				    maxIndex = midIndex - 1;
			    }
		    }

		    return -1;
	    }

		/// <summary>
		/// Complexity of O(n) + O(n) - this doesn't work on large scale
		/// </summary>
	    private bool ExistsHax(int[] input, int target)
		{
			var max = input.Max();
			var accumulator = new int[max + 1];

			foreach (var i in input)
			{
				accumulator[i] = 1;
				if (accumulator[target - i] > 0) return true;
			}

			return false;
		}
    }
}
