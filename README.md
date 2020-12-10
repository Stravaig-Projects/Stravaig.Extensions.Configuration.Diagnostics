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

### Connection Strings

There are three main variants to this. First is an extension method on IConfiguration, Second, an extension method on the logger, and third an extension method on IDbConnection

- `IConfiguration.LogConnectionString` will deconstruct and log at the given level the connection string with the given name that was found in the configuration.
  - `IConfiguration.LogConnectionStringAsInformation` is a variant that will log at the information level.
  - `IConfiguration.LogConnectionStringAsDebug` is a variant that will log at the debug level.
  - `IConfiguration.LogConnectionStringAsTrace` is a variant that will log at the trace level.
- `ILogger.LogConnectionString` will deconstruct the given connection string at the desired log level.
  - `ILogger.LogConnectionStringAsInformation` is a variant that will log at the information level.
  - `ILogger.LogConnectionStringAsDebug` is a variant that will log at the debug level.
  - `ILogger.LogConnectionStringAsTrace` is a variant that will log at the trace level.
- `IDbConnection.LogConnectionString` will deconstruct the connection's connection string and log at the desired level.
  - `IDbConnection.LogConnectionStringAsInformation` is a variant that will log at the information level.
  - `IDbConnection.LogConnectionStringAsDebug` is a variant that will log at the debug level.
  - `IDbConnection.LogConnectionStringAsTrace` is a variant that will log at the trace level.

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
