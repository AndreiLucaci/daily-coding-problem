using System;
using NUnit.Framework;

namespace DailyCodingProblem._2019.March
{
    /// <summary>
    /// This problem was asked by Jane Street.
    /// cons(a, b) constructs a pair, and car(pair) and cdr(pair) returns the first and last element of that pair.
    ///  For example, car(cons(3, 4)) returns 3, and cdr(cons(3, 4)) returns 4.
    /// Given this implementation of cons:
    /// def cons(a, b):
    ///     def pair(f):
    ///         return f(a, b)
    ///     return pair
    /// Implement car and cdr.
    /// </summary>
    public class March10 : ISolution
    {
        public void Solve()
        {
            var expectedFirst = 3;
            var expectedLast = 4;

            var first = Car(Cons(3, 4));
            var last = Cdr(Cons(3, 4));

            Assert.AreEqual(expectedFirst, first);
            Assert.AreEqual(expectedLast, last);

            Console.WriteLine("Ok");
        }

        private Func<Func<int, int, int>, int> Cons(int a, int b)
        {
            var pair = new Func<Func<int, int, int>, int>(f => f(a, b));

            return pair;
        }

        private int Car(Func<Func<int, int, int>, int> cons)
        {
            var firstFunc = new Func<int, int, int>((a, b) => a);

            return cons(firstFunc);
        }

        private int Cdr(Func<Func<int, int, int>, int> cons)
        {
            var lastFunc = new Func<int, int, int>((a, b) => b);

            return cons(lastFunc);
        }
    }

    /*
     * For the sake of completness, I'll add the python code as well
    
def cons(a, b):
    def pair(f):
        return f(a, b)
    return pair

def car(cons):
    def first(a, b):
        return a
    return cons(first)

def cdr(cons):
    def last(a, b):
        return b
    return cons(last)

     */
}
