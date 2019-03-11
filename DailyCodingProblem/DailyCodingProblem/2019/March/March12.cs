using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        private readonly Dictionary<string, string> _alphabet =
            Enumerable.Range(97, 26).ToDictionary(x => (x - 96).ToString(), x => ((char)x).ToString());

        protected override void Solution()
        {
            var input = "1234";

            nul(input);

            //var result = Decode(input).ToList();
        }

        private IEnumerable<string> Decode(string input)
        {
            var parts = _alphabet.Where(x => input.Contains(x.Key)).OrderBy(x => x.Key.Length)
                .ToDictionary(x => x.Key, x => x.Value);

            var level1 = input.Select(x => x.ToString());

            var level21 = new List<List<string>>();
            var len = 2;
            for (var i = 0; i < input.Length - len; i++)
            {
                var split = Split(input, 2, i);
                level21.Add(split.ToList());
            }

            return null;
        }

        private void nul(string str)
        {
            var accumulator = new List<List<string>>();
            var idx = 0;
            var len = 2;
            while (idx + len <= str.Length)
            {
                var acc = new List<string>();

                for (var i = 0; i < idx; i++) acc.Add(str[i].ToString());

                acc.Add(str.Substring(idx, len));

                for (var i = idx + len; i < str.Length; i++) acc.Add(str[i].ToString());

                idx++;
                accumulator.Add(acc);
            }

            idx = 0;

            while (idx + len <= str.Length)
            {
                var acc = new List<string>();

                for (var i = 0; i < idx; i++) acc.Add(str[i].ToString());

                for (var i = idx; i < (str.Length % 2 == 0 ? str.Length- len : str.Length-1); i += len)
                    acc.Add(str.Substring(i, len));

                for (var i = idx + len; i < str.Length; i++) acc.Add(str[i].ToString());

                idx++;
                accumulator.Add(acc);
            }


        }

        static IEnumerable<string> Split(string str, int chunkSize, int start = 0)
        {
            var rng = Enumerable.Range(start, str.Length / chunkSize + 1);

            return rng.Select(i => Substr(str, i * chunkSize, chunkSize));
        }

        static string Substr(string str, int start, int length)
        {
            var sb = new StringBuilder();
            for (var i = start; i < ((start + length) <= str.Length ? start + length : str.Length); i++)
            {
                sb.Append(str[i]);
            }

            return sb.ToString();
        }
    }
}
