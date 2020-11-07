using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Shouldly;
using Stravaig.Extensions.Configuration.Diagnostics.Tests.__data;
using Stravaig.Extensions.Configuration.Diagnostics.Tests.__fakes;

namespace Stravaig.Extensions.Configuration.Diagnostics.Tests
{
    public class ConnectionStringLogTests : TestBase
    {
        private const string BaseConfigKey = "ConnectionStrings";
        private const string SqlServerStandardSecurityKey = "SqlServerStandardSecurity";
        private const string SqlServerStandardSecurityValue = "Server=myServerAddress;Database=myDataBase;User Id=myUsername;Password=myPassword;";
        
        [SetUp]
        public void SetUp()
        {
            SetupLogger();
            SetupConfig(builder =>
            {
                builder.AddInMemoryCollection(new Dictionary<string, string>
                {
                    {
                        $"{BaseConfigKey}:{SqlServerStandardSecurityKey}",
                        SqlServerStandardSecurityValue
                    },
                });
            });
        }
        
        [Test]
        [TestCaseSource(typeof(LogLevelSource))]
        public void SqlServerStandardSecurityConnectionString(LogLevel level)
        {
            Logger.LogConnectionString(level, SqlServerStandardSecurityValue);
            VerifySqlServerStandardSecurityConnectionString(level);
        }

        [Test]
        [TestCaseSource(typeof(LogLevelSource))]
        public void ConfigSqlServerStandardSecurityConnectionString(LogLevel level)
        {
            const string name = "SqlServerStandardSecurity";
            ConfigRoot.LogConnectionString(name, Logger, level);
            VerifySqlServerStandardSecurityConnectionString(level, name);
        }

        [Test]
        [TestCaseSource(typeof(LogLevelSource))]
        public void DbConnectionConnectionString(LogLevel level)
        {
            var conn = new FakeDbConnection(SqlServerStandardSecurityValue);
            conn.LogConnectionString(Logger, level);
            VerifySqlServerStandardSecurityConnectionString(level);
        }

        private void VerifySqlServerStandardSecurityConnectionString(LogLevel level, string name = null)
        {
            var logs = GetLogs();
            logs.Count.ShouldBe(1);
            var log = logs[0];
            log.LogLevel.ShouldBe(level);
            log.Exception.ShouldBeNull();
            log.FormattedMessage.ShouldContain("myServerAddress");
            log.FormattedMessage.ShouldContain("myDataBase");
            log.FormattedMessage.ShouldContain("myUsername");
            log.FormattedMessage.ShouldContain("myPassword");
            
            if (!string.IsNullOrWhiteSpace(name))
                log.FormattedMessage.ShouldContain(name);
        }
    }
}