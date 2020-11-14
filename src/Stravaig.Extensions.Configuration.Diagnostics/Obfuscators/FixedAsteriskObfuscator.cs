namespace Stravaig.Extensions.Configuration.Diagnostics.Obfuscators
{
    /// <summary>
    /// An obfuscator that replaces secrets with a fixed number of asterisks.
    /// </summary>
    public class FixedAsteriskObfuscator : FixedStringObfuscator
    {
        /// <summary>
        /// Initialises the obfuscator with 4 asterisks.
        /// </summary>
        public FixedAsteriskObfuscator()
            : base("****")
        {
        }

        /// <summary>
        /// Initialises the obfuscator with the given number of asterisks.
        /// </summary>
        /// <param name="numAsterisks">The number of asterisks to replace a secret with.</param>
        public FixedAsteriskObfuscator(int numAsterisks)
            : base(new string('*', numAsterisks))
        {
        }
    }
}