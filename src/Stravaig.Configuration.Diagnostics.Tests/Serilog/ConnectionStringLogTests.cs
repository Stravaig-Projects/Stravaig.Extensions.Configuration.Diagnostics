using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using Serilog.Events;
using Shouldly;
using Stravaig.Configuration.Diagnostics;
using Stravaig.Configuration.Diagnostics.Obfuscators;
using Stravaig.Configuration.Diagnostics.Serilog;
using Stravaig.Extensions.Configuration.Diagnostics.Tests.__data;
using Stravaig.Extensions.Configuration.Diagnostics.Tests.__Extensions;
using Stravaig.Extensions.Configuration.Diagnostics.Tests.__fakes;

namespace Stravaig.Extensions.Configuration.Diagnostics.Tests.Serilog
{
    public class ConnectionStringLogTests : SerilogTestBase
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
        [TestCaseSource(typeof(LogEventLevelSource))]
        public void NullConnectionStringLogsNoConnectionString(LogEventLevel level)
        {
            Logger.LogConnectionString(level, null);
            var logs = GetLogs();
            logs.Count.ShouldBe(1);
            logs[0].GetMessage().ShouldBe("No connection string to log.");
        }

        [Test]
        [TestCaseSource(typeof(LogEventLevelSource))]
        public void NullNamedConnectionStringLogsNoConnectionString(LogEventLevel level)
        {
            Logger.LogConnectionString(level, null, "NotAConnectionString");
            var logs = GetLogs();
            logs.Count.ShouldBe(1);
            logs[0].GetMessage().ShouldBe("No connection string to log for \"NotAConnectionString\".");
            logs[0].GetProperties().Count.ShouldBe(1);
            logs[0].GetProperties()[0].Key.ShouldBe("NotAConnectionString_name");
            logs[0].GetProperties()[0].Value.ShouldBe("NotAConnectionString");
        }

        [Test]
        [TestCaseSource(typeof(LogEventLevelSource))]
        public void NamedConnectionStringWithNonAlphaNumericCharactersPropertiesRenderSafely(LogEventLevel level)
        {
            Logger.LogConnectionString(level, SqlServerStandardSecurityValue, "$My-Connection-String");
            var logs = GetLogs();
            logs.Count.ShouldBe(1);
            logs[0].GetMessage().ShouldContain("Connection string (named \"$My-Connection-String\") parameters");
            logs[0].GetProperties().Count.ShouldBeGreaterThan(2);
            logs[0].GetProperties()[0].Key.ShouldBe("_My_Connection_String_name");
            logs[0].GetProperties()[0].Value.ShouldBe("$My-Connection-String");
        }

        [Test]
        [TestCaseSource(typeof(LogEventLevelSource))]
        public void NamedConnectionStringStartingWithNumberCharactersPropertiesRenderSafely(LogEventLevel level)
        {
            Logger.LogConnectionString(level, SqlServerStandardSecurityValue, "0My-Connection-String");
            var logs = GetLogs();
            logs.Count.ShouldBe(1);
            logs[0].GetMessage().ShouldContain("Connection string (named \"0My-Connection-String\") parameters");
            logs[0].GetProperties().Count.ShouldBeGreaterThan(2);
            logs[0].GetProperties()[0].Key.ShouldBe("_0My_Connection_String_name");
            logs[0].GetProperties()[0].Value.ShouldBe("0My-Connection-String");
        }
        
        [Test]
        [TestCaseSource(typeof(LogEventLevelSource))]
        public void SqlServerStandardSecurityConnectionString(LogEventLevel level)
        {
            Logger.LogConnectionString(level, SqlServerStandardSecurityValue);
            VerifySqlServerStandardSecurityConnectionString(level);
        }
        
        [Test]
        [TestCaseSource(typeof(LogEventLevelSource))]
        public void SqlServerStandardSecurityConnectionStringWithObfuscatedPassword(LogEventLevel level)
        {
            var options = SetupOptions();
            Logger.LogConnectionString(level, SqlServerStandardSecurityValue, null, options);
            VerifyObfuscatedSqlServerStandardSecurityConnectionString(level);
        }

        [Test]
        [TestCaseSource(typeof(LogEventLevelSource))]
        public void ConfigSqlServerStandardSecurityConnectionString(LogEventLevel level)
        {
            Logger.LogConnectionString(ConfigRoot, SqlServerStandardSecurityKey, level);
            VerifySqlServerStandardSecurityConnectionString(level, SqlServerStandardSecurityKey);
        }

        [Test]
        [TestCaseSource(typeof(LogEventLevelSource))]
        public void ConfigReportExceptionWithDodgyConnectionString(LogEventLevel level)
        {
            Logger.LogConnectionString(ConfigRoot, DodgySecurityKey, level);
            VerifyDodgyConnectionStringLoggedProperly(level, DodgySecurityKey);
        }
        
        [Test]
        [TestCaseSource(typeof(LogEventLevelSource))]
        public void ConfigSqlServerStandardSecurityConnectionStringWithObfuscatedPassword(LogEventLevel level)
        {
            var options = SetupOptions();
            Logger.LogConnectionString(ConfigRoot, SqlServerStandardSecurityKey, level, options);
            VerifyObfuscatedSqlServerStandardSecurityConnectionString(level, SqlServerStandardSecurityKey);
        }

        [Test]
        [TestCaseSource(typeof(LogEventLevelSource))]
        public void DbConnectionConnectionString(LogEventLevel level)
        {
            var conn = new FakeDbConnection(SqlServerStandardSecurityValue);
            Logger.LogConnectionString(conn, level);
            VerifySqlServerStandardSecurityConnectionString(level);
        }
        
        [Test]
        [TestCaseSource(typeof(LogLevelSource))]
        public void ReportExceptionWithDodgyConnectionString(LogEventLevel level)
        {
            var conn = new FakeDbConnection(DodgyConnectionString);
            Logger.LogConnectionString(conn, level);
            VerifyDodgyConnectionStringLoggedProperly(level);
        }
        
        [Test]
        [TestCaseSource(typeof(LogEventLevelSource))]
        public void LogAllConnectionStringsWithOneDodgyConnectionString(LogEventLevel level)
        {
            var options = SetupOptions();
            Logger.LogAllConnectionStrings(ConfigRoot, level, options);
            var logs = GetLogs();
            logs.Count.ShouldBe(1);
            Console.WriteLine(logs[0].GetMessageTemplate());
            ((int)logs[0].GetLevel()).ShouldBeGreaterThanOrEqualTo((int)LogEventLevel.Warning);
            logs[0].GetMessage().ShouldContain("The following connection strings were found");
            logs[0].GetMessage().ShouldContain(DodgySecurityKey);
            logs[0].GetMessage().ShouldContain(SqlServerStandardSecurityKey);
            
            logs[0].GetMessageTemplate().ShouldContain("The following connection strings were found: {preamble_DodgyConnection_name}, {preamble_SqlServerStandardSecurity_name}.");
            logs[0].GetMessageTemplate().ShouldContain("The \"{DodgyConnection_name}\" connection string value could not be interpreted as a connection string.");
            logs[0].GetMessageTemplate().ShouldContain("Connection string (named {SqlServerStandardSecurity_name}) parameters:");
            logs[0].GetProperties().Select(p => p.Key).ShouldBeUnique();
        }
        
        [Test]
        [TestCaseSource(typeof(LogEventLevelSource))]
        public void LogAllConnectionStringsGoodConnectionString(LogEventLevel level)
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
            ValidateGoodConnectionStringTest(LogEventLevel.Information);
        }

        [Test]
        public void LogAllConnectionStringsAsDebugGoodConnectionString()
        {
            var options = SetupGoodConnectionStringTest();
            Logger.LogAllConnectionStringsAsDebug(ConfigRoot, options);
            ValidateGoodConnectionStringTest(LogEventLevel.Debug);
        }

        [Test]
        public void LogAllConnectionStringsAsVerboseGoodConnectionString()
        {
            var options = SetupGoodConnectionStringTest();
            Logger.LogAllConnectionStringsAsVerbose(ConfigRoot, options);
            ValidateGoodConnectionStringTest(LogEventLevel.Verbose);
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

        private void ValidateGoodConnectionStringTest(LogEventLevel level)
        {
            var logs = GetLogs();
            logs.Count.ShouldBe(1);
            Console.WriteLine(logs[0].GetMessageTemplate());
            logs[0].GetLevel().ShouldBe(level);
            logs[0].GetMessage().ShouldContain("The following connection strings were found");
            logs[0].GetMessage().ShouldContain(SqlServerTrustedSecurityKey);
            logs[0].GetMessage().ShouldContain(SqlServerStandardSecurityKey);

            logs[0].GetMessageTemplate()
                .ShouldContain(
                    "The following connection strings were found: {preamble_SqlServerStandardSecurity_name}, {preamble_SqlServerTrustedSecurity_name}.");
            logs[0].GetMessageTemplate().ShouldContain("Connection string (named {SqlServerTrustedSecurity_name}) parameters:");
            logs[0].GetMessageTemplate().ShouldContain("Connection string (named {SqlServerStandardSecurity_name}) parameters:");
        }
        
        [Test]
        [TestCaseSource(typeof(LogEventLevelSource))]
        public void LogAllConnectionStringsWhenThereAreNone(LogEventLevel level)
        {
            SetupConfig(builder => builder.AddInMemoryCollection());
            var options = SetupOptions();
            Logger.LogAllConnectionStrings(ConfigRoot, level, options);
            var logs = GetLogs();
            logs.Count.ShouldBe(1);
            logs[0].GetMessage().ShouldContain("No connections strings found in the configuration.");
        }

        private static ConfigurationDiagnosticsOptions SetupOptions()
        {
            var options = new ConfigurationDiagnosticsOptions();
            options.BuildConnectionStringElementMatcher(builder => { builder.AddContainsMatcher("password"); });
            options.Obfuscator = new MatchedLengthAsteriskObfuscator();
            return options;
        }
        
        private void VerifyDodgyConnectionStringLoggedProperly(LogEventLevel level, string name = null)
        {
            var logs = GetLogs();
            logs.Count.ShouldBe(1);
            var log = logs[0];
            ((int) log.GetLevel()).ShouldBeGreaterThanOrEqualTo((int) level);
            log.GetException().ShouldNotBeNull();
            log.GetMessage().ShouldContain("connection string value could not be interpreted as a connection string.");
                    
            if (!string.IsNullOrWhiteSpace(name))
                log.GetMessage().ShouldContain(name);
        }
        
        private void VerifySqlServerStandardSecurityConnectionString(LogEventLevel level, string name = null)
        {
            var logs = GetLogs();
            logs.Count.ShouldBe(1);
            var log = logs[0];
            log.GetLevel().ShouldBe(level);
            log.GetException().ShouldBeNull();
            log.GetMessage().ShouldContain("myServerAddress");
            log.GetMessage().ShouldContain("myDataBase");
            log.GetMessage().ShouldContain("myUsername");
            log.GetMessage().ShouldContain("myPassword");
            
            if (!string.IsNullOrWhiteSpace(name))
                log.GetMessage().ShouldContain(name);
        }
        
        private void VerifyObfuscatedSqlServerStandardSecurityConnectionString(LogEventLevel level, string name = null)
        {
            var logs = GetLogs();
            logs.Count.ShouldBe(1);
            var log = logs[0];
            log.GetLevel().ShouldBe(level);
            log.GetException().ShouldBeNull();
            log.GetMessage().ShouldContain("myServerAddress");
            log.GetMessage().ShouldContain("myDataBase");
            log.GetMessage().ShouldContain("myUsername");
            log.GetMessage().ShouldContain("**********");
            log.GetMessage().ShouldNotContain("myPassword");
            
            if (!string.IsNullOrWhiteSpace(name))
                log.GetMessage().ShouldContain(name);
        }
    }
}