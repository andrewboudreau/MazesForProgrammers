using System;
using System.Collections.Generic;

namespace MazesForProgrammers.Extensions
{    /// <summary>
    /// Base36
    /// </summary>
    public static class Base36
    {
        private const string CharList = "0123456789abcdefghijklmnopqrstuvwxyz";

        /// <summary>
        /// Encode the given number into a Base36 string.
        /// </summary>
        /// <param name="input">An int value to convert to base36.</param>
        /// <returns>A base36 string</returns>
        public static string Encode(int input)
        {
            if (input < 0) return "▒";
            if (input == 0) return "0";

            char[] chars = CharList.ToCharArray();
            var result = new Stack<char>();
            while (input != 0)
            {
                result.Push(chars[input % 36]);
                input /= 36;
            }

            return new string(result.ToArray());
        }

        /// <summary>
        /// Decode the Base36 Encoded string into a number.
        /// </summary>
        /// <param name="input">A base36 string to convert to an int.</param>
        /// <returns>A integer value decoded from base36.</returns>
        public static int Decode(string input)
        {
            var reversed = input.ToLower().Reverse();
            int result = 0;
            int pos = 0;
            foreach (char c in reversed)
            {
                result += CharList.IndexOf(c) * (int)Math.Pow(36, pos);
                pos++;
            }

            return result;
        }
    }
}