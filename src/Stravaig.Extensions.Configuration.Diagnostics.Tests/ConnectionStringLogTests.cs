using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Shouldly;
using Stravaig.Configuration.Diagnostics;
using Stravaig.Configuration.Diagnostics.Extensions;
using Stravaig.Configuration.Diagnostics.Obfuscators;
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
        private const string SqlServerTrustedSecurityKey = "SqlServerTrustedSecurity";
        private const string SqlServerTrustedSecurityValue = "Server=myServerAddress;Database=myDataBase;Trusted_Connection=True;";

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
        public void NullConnectionStringLogsNoConnectionString(LogLevel level)
        {
            Logger.LogConnectionString(level, null);
            var logs = GetLogs();
            logs.Count.ShouldBe(1);
            logs[0].FormattedMessage.ShouldBe("No connection string to log.");
        }

        [Test]
        [TestCaseSource(typeof(LogLevelSource))]
        public void NullNamedConnectionStringLogsNoConnectionString(LogLevel level)
        {
            Logger.LogConnectionString(level, null, "NotAConnectionString");
            var logs = GetLogs();
            logs.Count.ShouldBe(1);
            logs[0].FormattedMessage.ShouldBe("No connection string to log for NotAConnectionString.");
            logs[0].Properties.Count.ShouldBe(2);
            logs[0].Properties[0].Key.ShouldBe("NotAConnectionString_name");
            logs[0].Properties[0].Value.ShouldBe("NotAConnectionString");
        }

        [Test]
        [TestCaseSource(typeof(LogLevelSource))]
        public void NamedConnectionStringWithNonAlphaNumericCharactersPropertiesRenderSafely(LogLevel level)
        {
            Logger.LogConnectionString(level, SqlServerStandardSecurityValue, "$My-Connection-String");
            var logs = GetLogs();
            logs.Count.ShouldBe(1);
            logs[0].FormattedMessage.ShouldContain("Connection string (named $My-Connection-String) parameters");
            logs[0].Properties.Count.ShouldBeGreaterThan(2);
            logs[0].Properties[0].Key.ShouldBe("_My_Connection_String_name");
            logs[0].Properties[0].Value.ShouldBe("$My-Connection-String");
        }

        [Test]
        [TestCaseSource(typeof(LogLevelSource))]
        public void NamedConnectionStringStartingWithNumberCharactersPropertiesRenderSafely(LogLevel level)
        {
            Logger.LogConnectionString(level, SqlServerStandardSecurityValue, "0My-Connection-String");
            var logs = GetLogs();
            logs.Count.ShouldBe(1);
            logs[0].FormattedMessage.ShouldContain("Connection string (named 0My-Connection-String) parameters");
            logs[0].Properties.Count.ShouldBeGreaterThan(2);
            logs[0].Properties[0].Key.ShouldBe("_0My_Connection_String_name");
            logs[0].Properties[0].Value.ShouldBe("0My-Connection-String");
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
        
        [Test]
        [TestCaseSource(typeof(LogLevelSource))]
        public void LogAllConnectionStringsWithOneDodgyConnectionString(LogLevel level)
        {
            var options = SetupOptions();
            Logger.LogAllConnectionStrings(ConfigRoot, level, options);
            var logs = GetLogs();
            logs.Count.ShouldBe(1);
            Console.WriteLine(logs[0].OriginalMessage);
            ((int)logs[0].LogLevel).ShouldBeGreaterThanOrEqualTo((int)LogLevel.Warning);
            logs[0].FormattedMessage.ShouldContain("The following connection strings were found");
            logs[0].FormattedMessage.ShouldContain(DodgySecurityKey);
            logs[0].FormattedMessage.ShouldContain(SqlServerStandardSecurityKey);
            
            logs[0].OriginalMessage.ShouldContain("The following connection strings were found: {preamble_DodgyConnection_name}, {preamble_SqlServerStandardSecurity_name}.");
            logs[0].OriginalMessage.ShouldContain("The \"{DodgyConnection_name}\" connection string value could not be interpreted as a connection string.");
            logs[0].OriginalMessage.ShouldContain("Connection string (named {SqlServerStandardSecurity_name}) parameters:");
            logs[0].Properties.Select(p => p.Key).ShouldBeUnique();
        }
        
        [Test]
        [TestCaseSource(typeof(LogLevelSource))]
        public void LogAllConnectionStringsGoodConnectionString(LogLevel level)
        {
            var options = SetupGoodConnectionStringTest();
            Logger.LogAllConnectionStrings(ConfigRoot, level, options);
            ValidateGoodConnectionStringTest(level);
        }
        
        [Test]
        public void LogAllConnectionStringsAsInformationGoodConnectionString()
        {
            var options = SetupGoodConnectionStringTest();
            Logger.LogAllConnectionStringsAsInformation(ConfigRoot, options);
            ValidateGoodConnectionStringTest(LogLevel.Information);
        }

        [Test]
        public void LogAllConnectionStringsAsDebugGoodConnectionString()
        {
            var options = SetupGoodConnectionStringTest();
            Logger.LogAllConnectionStringsAsDebug(ConfigRoot, options);
            ValidateGoodConnectionStringTest(LogLevel.Debug);
        }

        [Test]
        public void LogAllConnectionStringsAsTraceGoodConnectionString()
        {
            var options = SetupGoodConnectionStringTest();
            Logger.LogAllConnectionStringsAsTrace(ConfigRoot, options);
            ValidateGoodConnectionStringTest(LogLevel.Trace);
        }
        
        private ConfigurationDiagnosticsOptions SetupGoodConnectionStringTest()
        {
            SetupConfig(builder =>
            {
                builder.AddInMemoryCollection(new Dictionary<string, string>
                {
                    {
                        $"{BaseConfigKey}:{SqlServerStandardSecurityKey}",
                        SqlServerStandardSecurityValue
                    },
                    {
                        $"{BaseConfigKey}:{SqlServerTrustedSecurityKey}",
                        SqlServerTrustedSecurityValue
                    }
                });
            });
            var options = SetupOptions();
            return options;
        }

        private void ValidateGoodConnectionStringTest(LogLevel level)
        {
            var logs = GetLogs();
            logs.Count.ShouldBe(1);
            Console.WriteLine(logs[0].OriginalMessage);
            logs[0].LogLevel.ShouldBe(level);
            logs[0].FormattedMessage.ShouldContain("The following connection strings were found");
            logs[0].FormattedMessage.ShouldContain(SqlServerTrustedSecurityKey);
            logs[0].FormattedMessage.ShouldContain(SqlServerStandardSecurityKey);

            logs[0].OriginalMessage
                .ShouldContain(
                    "The following connection strings were found: {preamble_SqlServerStandardSecurity_name}, {preamble_SqlServerTrustedSecurity_name}.");
            logs[0].OriginalMessage.ShouldContain("Connection string (named {SqlServerTrustedSecurity_name}) parameters:");
            logs[0].OriginalMessage.ShouldContain("Connection string (named {SqlServerStandardSecurity_name}) parameters:");
        }
        
        [Test]
        [TestCaseSource(typeof(LogLevelSource))]
        public void LogAllConnectionStringsWhenThereAreNone(LogLevel level)
        {
            SetupConfig(builder => builder.AddInMemoryCollection());
            var options = SetupOptions();
            Logger.LogAllConnectionStrings(ConfigRoot, level, options);
            var logs = GetLogs();
            logs.Count.ShouldBe(1);
            logs[0].FormattedMessage.ShouldContain("No connections strings found in the configuration.");
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