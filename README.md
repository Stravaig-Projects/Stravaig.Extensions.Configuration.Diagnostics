# Stravaig.Extensions.Configuration.Diagnostics

Provides logged diagnostics for the app configuration.

* ![Stravaig Configuration Diagnostics](https://github.com/Stravaig-Projects/Stravaig.Extensions.Configuration.Diagnostics/workflows/Stravaig%20Configuration%20Diagnostics/badge.svg)
* ![Nuget](https://img.shields.io/nuget/v/Stravaig.Extensions.Configuration.Diagnostics?color=004880&label=nuget%20stable&logo=nuget) [View on NuGet](https://www.nuget.org/packages/Stravaig.Extensions.Configuration.Diagnostics)
* ![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/Stravaig.Extensions.Configuration.Diagnostics?color=ffffff&label=nuget%20latest&logo=nuget) [View on NuGet](https://www.nuget.org/packages/Stravaig.Extensions.Configuration.Diagnostics)

## Usage

### Logging Provider Type Names

- `ILogger.LogProviderNames(IConfigurationRoot, LogLevel)`
  - `ILogger.LogProviderNamesAsInformation(IConfigurationRoot)`
  - `ILogger.LogProviderNamesAsDebug(IConfigurationRoot)`
  - `ILogger.LogProviderNamesAsTrace(IConfigurationRoot)`

Logs the provider type names that are in the `IConfigurationRoot` at the requested level.

e.g.

```csharp
    logger.LogProviderNames(configRoot, LogLevel.Information);
```

will produce a log entry that looks like this from the standard console logger:

```
info: Stravaig.Extensions.Configuration.Diagnostics.Tests.MultipleProviderNameTests[0]
      The following configuration providers were registered:
      Microsoft.Extensions.Configuration.Memory.MemoryConfigurationProvider
      Microsoft.Extensions.Configuration.Json.JsonConfigurationProvider
      Microsoft.Extensions.Configuration.EnvironmentVariables.EnvironmentVariablesConfigurationProvider
      Microsoft.Extensions.Configuration.CommandLine.CommandLineConfigurationProvider
```

### Logging Providers

- `ILogger.LogProviders(IConfigurationRoot, LogLevel)`
  - `ILogger.LogProvidersAsInformation(IConfigurationRoot)`
  - `ILogger.LogProvidersAsDebug(IConfigurationRoot)`
  - `ILogger.LogProvidersAsTrace(IConfigurationRoot)`

e.g.

```csharp
    logger.LogProviders(configRoot, LogLevel.Information);
```

will produce a log entry that looks like this from the standard console logger:

```
info: Stravaig.Extensions.Configuration.Diagnostics.Tests.MultipleProvidersTests[0]
      The following configuration providers were registered:
      MemoryConfigurationProvider
      JsonConfigurationProvider for 'appsettings.json' (Optional)
      JsonConfigurationProvider for 'appsettings.test.json' (Optional)
      EnvironmentVariablesConfigurationProvider
      CommandLineConfigurationProvider
```

### Tracking where a value came from

- `ILogger.LogConfigurationKeySource(LogLevel, IConfigurationRoot, string key, bool compressed = false, ConfigurationDiagnosticsOptions options = null)`
  - `ILogger.LogConfigurationKeySourceAsInformation(IConfigurationRoot, string key, bool compressed  = false, ConfigurationDiagnosticsOptions options = null)`
  - `ILogger.LogConfigurationKeySourceAsDebug(IConfigurationRoot, string key, bool compressed  = false, ConfigurationDiagnosticsOptions options = null)`
  - `ILogger.LogConfigurationKeySourceAsTrace(IConfigurationRoot, string key, bool compressed  = false, ConfigurationDiagnosticsOptions options = null)`

You can track where a value came from with the `LogConfigurationKeySource` based methods. It will output the providers that have the given key, the last of which will be the provider that was used for the final value that is picked up by your application.

The parameters are: 
- `LogLevel level` : The level to log at.
- `string key` : The key to look up.
- `bool compressed` : Whether to log providers that did not have a value for that key. If false then all providers are logged, but indicate `null` where no value was present. If true then providers that did not provide a value are ignored.
- `options`: The options for obfuscating and key matching secrets so that values that are secrets are not inadvertently rendered to the log. If no options are specified then the `ConfigurationDiagnosticsOptions.GlobalOptions` are used.

e.g.
I'
```
info: Stravaig.Extensions.Configuration.Diagnostics.Tests.ConfigurationProviderTrackingExtensionsTests[0]
      Provider sources for value of SomeSection:SomeKey
      * MemoryConfigurationProvider ==> "SomeValue"
      * MemoryConfigurationProvider ==> null
      * JsonStreamConfigurationProvider ==> "SomeNewValue"
```

### Log Configuration Values

As the `IConfigurationRoot` interface is derived from the `IConfiguration` interface, that the extension methods here will work with an `IConfigurationRoot` reference too.

- `ILogger.LogConfigurationValues(IConfiguration config, LogLevel level, ConfigurationDiagnosticsOptions options = null)`
  - `ILogger.LogConfigurationValuesAsInformation(IConfiguration config, ConfigurationDiagnosticsOptions options = null)`
  - `ILogger.LogConfigurationValuesAsDebug(IConfiguration config, ConfigurationDiagnosticsOptions options = null)`
  - `ILogger.LogConfigurationValuesAsTrace(IConfiguration config, ConfigurationDiagnosticsOptions options = null)`

The parameters are: 
- `LogLevel level` : The level to log at.
- `options`: The options for obfuscating and key matching secrets so that values that are secrets are not inadvertently rendered to the log. If no options are specified then the `ConfigurationDiagnosticsOptions.GlobalOptions` are used.

e.g.

```csharp 
logger.LogConfigurationValues(config, LogLevel.Information);
```

will produce a log entry that looks something like this:

```
info: Stravaig.Extensions.Configuration.Diagnostics.Tests.LogValuesTests[0]
      The following values are available:
      ConfigOne : One
      ConfigThree : Tres
      ConfigTwo : Dos
```



### Log Connection Strings

This will deconstruct the connection string into its component parts.

There are a few variants to this. You can specify the name of a connection string in the configuration and it will pick out the value from the `ConnectionStrings` section for you. You can pass in a connection string directly. Or, you can pass an `IDbConnection` object and the connection string will be extracted from that.
 
- `ILogger.LogConnectionString(IConfiguration config, LogLevel level, string name, ConfigurationDiagnosticOptions = null)`
  - `ILogger.LogConnectionStringAsInformation(IConfiguration config, string name, ConfigurationDiagnosticOptions = null)`
  - `ILogger.LogConnectionStringAsDebug(IConfiguration config, string name, ConfigurationDiagnosticOptions = null)`
  - `ILogger.LogConnectionStringAsTrace(IConfiguration config, string name, ConfigurationDiagnosticOptions = null)`
- `ILogger.LogConnectionString(IDbConnection connection, LogLevel level, ConfigurationDiagnosticOptions = null)`
  - `ILogger.LogConnectionStringAsInformation(IDbConnection connection, ConfigurationDiagnosticOptions = null)`
  - `ILogger.LogConnectionStringAsDebug(IDbConnection connection, ConfigurationDiagnosticOptions = null)`
  - `ILogger.LogConnectionStringAsTrace(IDbConnection connection, ConfigurationDiagnosticOptions = null)`
- `ILogger.LogAllConnectionStrings(IConfiguration config, LogLevel level, ConfigurationDiagnosticOptions = null)`
  - `ILogger.LogAllConnectionStringsAsInformation(IConfiguration config, LogLevel level, ConfigurationDiagnosticOptions = null)`
  - `ILogger.LogAllConnectionStringsAsDebug(IConfiguration config, LogLevel level, ConfigurationDiagnosticOptions = null)`
  - `ILogger.LogAllConnectionStringsAsTrace(IConfiguration config, LogLevel level, ConfigurationDiagnosticOptions = null)`
- `ILogger.LogConnectionString(LogLevel level, string connectionString, string name = null, ConfigurationDiagnosticOptions options = null)`
  - `ILogger.LogConnectionStringAsInformation(string connectionString, string name = null, ConfigurationDiagnosticOptions options = null)`
  - `ILogger.LogConnectionStringAsDebug(string connectionString, string name = null, ConfigurationDiagnosticOptions options = null)`
  - `ILogger.LogConnectionStringAsTrace(string connectionString, string name = null, ConfigurationDiagnosticOptions options = null)`

The parameters are:
* `IConfiguration config` : The configuration containing a `ConnectionStrings` section.
* `IDbConnection connection` : A connection object, which contains a connection string.
* `LogLevel level` : The level at which to log.
* `string name` : The name of the connection string element.
* `string connectionString` : The connection string itself.
* `ConfigurationDiagnosticOptions options` : An optional parameter that defines the options for obfuscation and secret key matching. If not give uses `ConfigurationDiagnosticOptions.GlobalOptions`

For all of these the deconstructed connection string log message will look something like this:

```
info: Stravaig.Extensions.Configuration.Diagnostics.Tests.ConnectionStringLogTests[0]
      Connection string parameters:
       * server = myServerAddress
       * database = myDataBase
       * user id = myUsername
       * password = myPassword
```

### Options Builder

You can build the options up using a fluent interface, for example:

```csharp
ConfigurationDiagnosticsOptions.GlobalOptions = ConfigurationDiagnosticsOptions
  .SetUpBy.Obfuscating.WithAsterisks()
  .And.MatchingConfigurationKeys.Containing(/*string*/)
  .And.MatchingConnectionStringKeys.MatchingPattern(/*regExPattern*/)
  .AndFinally.BuildOptions();
```

or

```csharp
ConfigurationDiagnosticsOptions
  .SetUpBy.Obfuscating.ByRedacting()
  .And.MatchingConfigurationKeys.Where
    .KeyContains(/*string*/)
    .OrContains(/*string*/)
    .OrMatchesPattern(/*regExPattern*/)
  .And.ConnectionStringKeys.Where
    .KeyMatchesPattern(/*regExPattern*/)
    .OrContains(/*string*/)
  .AndFinally.ApplyOptions(ConfigurationDiagnosticsOptions.GlobalOptions);
```
