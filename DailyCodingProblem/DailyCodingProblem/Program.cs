using System;
using System.Collections.Generic;
using DailyCodingProblem._2019.March;

namespace DailyCodingProblem
{
	class Program
	{
		static void Main(string[] args)
		{
		    Methods[DateTime.Today].Solve();
		}

        private static Dictionary<DateTime, ISolution> Methods { get; } =
            new Dictionary<DateTime, ISolution>
            {
                [new DateTime(2019, 3, 6)] = new Day6(),
                [new DateTime(2019, 3, 7)] = new Day7()
            };
	}
}
