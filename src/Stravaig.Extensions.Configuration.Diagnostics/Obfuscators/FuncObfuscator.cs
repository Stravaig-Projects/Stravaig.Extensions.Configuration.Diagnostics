using System;
using Stravaig.Extensions.Configuration.Diagnostics.Obfuscators;

namespace Stravaig.Extensions.Configuration.Diagnostics.Obfuscators
{
    /// <summary>
    /// Wraps a <see cref="Func&lt;string, string&gt;"/> as a obfuscator.
    /// </summary>
    public class FuncObfuscator : ISecretObfuscator
    {
        private readonly Func<string, string> _obfuscateFunction;

        /// <summary>
        /// Initialises a <see cref="FuncObfuscator"/> with a function that runs when <see cref="Obfuscate"/> is called.
        /// </summary>
        /// <param name="obfuscateFunction">The function that obfuscates the secret.</param>
        public FuncObfuscator(Func<string, string> obfuscateFunction)
        {
            _obfuscateFunction = obfuscateFunction 
                                 ?? throw new ArgumentNullException(nameof(obfuscateFunction));
        }
        
        /// <inheritdoc />
        public string Obfuscate(string secret)
        {
            return _obfuscateFunction(secret);
        }
    }
}