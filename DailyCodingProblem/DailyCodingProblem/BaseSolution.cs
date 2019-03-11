using System;

namespace DailyCodingProblem
{
    public abstract class BaseSolution : ISolution
    {
        protected abstract void Solution();

        public void Solve()
        {
            Solution();

            Console.WriteLine("Ok");
        }
    }
}
