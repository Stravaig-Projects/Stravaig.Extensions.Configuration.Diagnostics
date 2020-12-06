using System;
using NUnit.Framework;
using Shouldly;
using Stravaig.Extensions.Configuration.Diagnostics.FluentOptions;
using Stravaig.Extensions.Configuration.Diagnostics.Obfuscators;

namespace Stravaig.Extensions.Configuration.Diagnostics.Tests.FluentOptions
{
    [TestFixture]
    public class FluentOptionsFuncObfuscatorExtensionsTests
    {
        [Test]
        public void CheckAsteriskExtension()
        {
            bool hasBeenCalled = false;
            string secret = "";
            Func<string, string> func = (s) =>
            {
                secret = s;
                hasBeenCalled = true;
                return "world";
            };
            
            var options = SetupByObfuscating().With(func)
                .AndFinally.BuildOptions();
            
            options.Obfuscator.ShouldBeOfType<FuncObfuscator>();
            var obfuscator = (FuncObfuscator)options.Obfuscator;
            
            var result = obfuscator.Obfuscate("hello");
            
            result.ShouldBe("world");
            hasBeenCalled.ShouldBeTrue();
            secret.ShouldBe("hello");
        }
        
        private static IObfuscatorOptionsBuilder SetupByObfuscating()
        {
            return ConfigurationDiagnosticsOptions.SetupBy
                .Obfuscating;
        }
    }
}