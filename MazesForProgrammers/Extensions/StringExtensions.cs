namespace MazesForProgrammers.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Receives string and returns the string with its letters reversed.
        /// </summary>
        public static string Reverse(this string s)
        {
            char[] array = new char[s.Length];
            int forward = 0;
            for (int i = s.Length - 1; i >= 0; i--)
            {
                array[forward++] = s[i];
            }

            return new string(array);
        }
    }
}
