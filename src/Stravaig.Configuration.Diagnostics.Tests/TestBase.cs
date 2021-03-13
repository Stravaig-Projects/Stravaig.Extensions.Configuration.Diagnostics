using System;
using Microsoft.Extensions.Configuration;

namespace Stravaig.Extensions.Configuration.Diagnostics.Tests
{
    public class TestBase
    {
        protected IConfigurationRoot ConfigRoot;

        protected void SetupConfig(Action<IConfigurationBuilder> configure)
        {
            var builder = new ConfigurationBuilder();
            configure(builder);
            ConfigRoot = builder.Build();
        }
    }
}