using System;
using Microsoft.Extensions.Configuration;

namespace Stravaig.Extensions.Configuration.Diagnostics.Tests
{
    public class TestBase
    {
        protected IConfigurationRoot ConfigRoot;

        protected void SetupConfig(Action<IConfigurationBuilder> configure = null)
        {
            var builder = new ConfigurationBuilder();
            configure?.Invoke(builder);
            ConfigRoot = builder.Build();
        }
    }
}