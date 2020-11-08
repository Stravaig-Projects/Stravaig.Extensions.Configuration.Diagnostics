using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Shouldly;
using Stravaig.Extensions.Configuration.Diagnostics.Obfuscators;
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
        public void SqlServerStandardSecurityConnectionStringWithObfuscatedPassword(LogLevel level)
        {
            var options = SetupOptions();
            Logger.LogConnectionString(level, SqlServerStandardSecurityValue, null, options);
            VerifyObfuscatedSqlServerStandardSecurityConnectionString(level);
        }

        [Test]
        [TestCaseSource(typeof(LogLevelSource))]
        public void ConfigSqlServerStandardSecurityConnectionString(LogLevel level)
        {
            ConfigRoot.LogConnectionString(SqlServerStandardSecurityKey, Logger, level);
            VerifySqlServerStandardSecurityConnectionString(level, SqlServerStandardSecurityKey);
        }

        [Test]
        [TestCaseSource(typeof(LogLevelSource))]
        public void ConfigSqlServerStandardSecurityConnectionStringWithObfuscatedPassword(LogLevel level)
        {
            var options = SetupOptions();
            ConfigRoot.LogConnectionString(SqlServerStandardSecurityKey, Logger, level, options);
            VerifyObfuscatedSqlServerStandardSecurityConnectionString(level, SqlServerStandardSecurityKey);
        }

        [Test]
        [TestCaseSource(typeof(LogLevelSource))]
        public void DbConnectionConnectionString(LogLevel level)
        {
            var conn = new FakeDbConnection(SqlServerStandardSecurityValue);
            conn.LogConnectionString(Logger, level);
            VerifySqlServerStandardSecurityConnectionString(level);
        }

        private static ConfigurationDiagnosticsOptions SetupOptions()
        {
            var options = new ConfigurationDiagnosticsOptions();
            options.BuildConnectionStringElementMatcher(builder => { builder.AddContainsMatcher("password"); });
            options.Obfuscator = new MatchedLengthAsteriskObfuscator();
            return options;
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
        
        private void VerifyObfuscatedSqlServerStandardSecurityConnectionString(LogLevel level, string name = null)
        {
            var logs = GetLogs();
            logs.Count.ShouldBe(1);
            var log = logs[0];
            log.LogLevel.ShouldBe(level);
            log.Exception.ShouldBeNull();
            log.FormattedMessage.ShouldContain("myServerAddress");
            log.FormattedMessage.ShouldContain("myDataBase");
            log.FormattedMessage.ShouldContain("myUsername");
            log.FormattedMessage.ShouldContain("**********");
            log.FormattedMessage.ShouldNotContain("myPassword");
            
            if (!string.IsNullOrWhiteSpace(name))
                log.FormattedMessage.ShouldContain(name);
        }
    }
}