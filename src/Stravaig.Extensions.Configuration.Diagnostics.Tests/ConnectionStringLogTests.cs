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
        private const string DodgySecurityKey = "DodgyConnection";
        private const string DodgyConnectionString = "*** Comes from Secrets ***";
        
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
                    {
                        $"{BaseConfigKey}:{DodgySecurityKey}",
                        DodgyConnectionString
                    }
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
            Logger.LogConnectionString(ConfigRoot, SqlServerStandardSecurityKey, level);
            VerifySqlServerStandardSecurityConnectionString(level, SqlServerStandardSecurityKey);
        }

        [Test]
        [TestCaseSource(typeof(LogLevelSource))]
        public void ConfigReportExceptionWithDodgyConnectionString(LogLevel level)
        {
            Logger.LogConnectionString(ConfigRoot, DodgySecurityKey, level);
            VerifyDodgyConnectionStringLoggedProperly(level, DodgySecurityKey);
        }
        
        [Test]
        [TestCaseSource(typeof(LogLevelSource))]
        public void ConfigSqlServerStandardSecurityConnectionStringWithObfuscatedPassword(LogLevel level)
        {
            var options = SetupOptions();
            Logger.LogConnectionString(ConfigRoot, SqlServerStandardSecurityKey, level, options);
            VerifyObfuscatedSqlServerStandardSecurityConnectionString(level, SqlServerStandardSecurityKey);
        }

        [Test]
        [TestCaseSource(typeof(LogLevelSource))]
        public void DbConnectionConnectionString(LogLevel level)
        {
            var conn = new FakeDbConnection(SqlServerStandardSecurityValue);
            Logger.LogConnectionString(conn, level);
            VerifySqlServerStandardSecurityConnectionString(level);
        }
        
        [Test]
        [TestCaseSource(typeof(LogLevelSource))]
        public void ReportExceptionWithDodgyConnectionString(LogLevel level)
        {
            var conn = new FakeDbConnection(DodgyConnectionString);
            Logger.LogConnectionString(conn, level);
            VerifyDodgyConnectionStringLoggedProperly(level);
        }

        private static ConfigurationDiagnosticsOptions SetupOptions()
        {
            var options = new ConfigurationDiagnosticsOptions();
            options.BuildConnectionStringElementMatcher(builder => { builder.AddContainsMatcher("password"); });
            options.Obfuscator = new MatchedLengthAsteriskObfuscator();
            return options;
        }
        
        private void VerifyDodgyConnectionStringLoggedProperly(LogLevel level, string name = null)
        {
            var logs = GetLogs();
            logs.Count.ShouldBe(1);
            var log = logs[0];
            ((int) log.LogLevel).ShouldBeGreaterThanOrEqualTo((int) level);
            log.Exception.ShouldNotBeNull();
            log.FormattedMessage.ShouldContain("connection string value could not be interpreted as a connection string.");
                    
            if (!string.IsNullOrWhiteSpace(name))
                log.FormattedMessage.ShouldContain(name);
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