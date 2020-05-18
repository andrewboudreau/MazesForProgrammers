using System;

namespace MazesForProgrammers
{
    /// <summary>
    /// A shared random source.
    /// </summary>
    public static class RandomSource
    {
        /// <summary>
        /// A random number generator.
        /// </summary>
        public static Random Random = new Random(0);

        /// <summary>
        /// Sets the random number generator seed. Null is random.
        /// </summary>
        /// <param name="seed">A random number seed, null for random seed.</param>
        public static void SetRandom(int? seed = null)
        {
            if (seed.HasValue)
            {
                Random = new Random(seed.Value);
            }
            else
            {
                Random = new Random();
            }
        }
    }
}
