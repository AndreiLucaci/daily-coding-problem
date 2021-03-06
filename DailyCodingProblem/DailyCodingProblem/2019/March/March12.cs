﻿using NUnit.Framework;

namespace DailyCodingProblem._2019.March
{
    /// <summary>
    /// This problem was asked by Facebook.
    /// Given the mapping a = 1, b = 2, ... z = 26, and an encoded message, count the number of ways it can be decoded.
    /// For example, the message '111' would give 3, since it could be decoded as 'aaa', 'ka', and 'ak'.
    /// You can assume that the messages are decodable. For example, '001' is not allowed.
    /// </summary>
    public class March12 : BaseSolution
    {
        protected override void Solution()
        {
            var input1 = "12";
            var expectedOutput1 = 2;
            var result1 = Count(input1);
            
            var input2 = "111";
            var expectedOutput2 = 3;
            var result2 = Count(input2);

            Assert.AreEqual(expectedOutput1, result1);
            Assert.AreEqual(expectedOutput2, result2);
        }

        private int Count(string encodedMessage)
        {
            var first = 1;
            var second = 1;

            for (var i = 2; i <= encodedMessage.Length; i++)
            {
                var current = 0;

                if (encodedMessage[i - 2] > '0')
                {
                    current = second;
                }

                if (encodedMessage[i - 2] != '1' && (encodedMessage[i - 2] != '2' || encodedMessage[i - 1] >= '7'))
                {
                    second = current;
                    continue;
                }

                current += first;
                first = second;
                second = current;
            }

            return second;
        }
    }
}
