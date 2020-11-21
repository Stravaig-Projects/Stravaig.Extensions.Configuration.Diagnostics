# Stravaig.Extensions.Configuration.Diagnostics

Provides logged diagnostics for the app configuration.

* ![Stravaig Configuration Diagnostics](https://github.com/Stravaig-Projects/Stravaig.Extensions.Configuration.Diagnostics/workflows/Stravaig%20Configuration%20Diagnostics/badge.svg)
* ![Nuget](https://img.shields.io/nuget/v/Stravaig.Extensions.Configuration.Diagnostics?color=004880&label=nuget%20stable&logo=nuget) [View on NuGet](https://www.nuget.org/packages/Stravaig.Extensions.Configuration.Diagnostics)
* ![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/Stravaig.Extensions.Configuration.Diagnostics?color=ffffff&label=nuget%20latest&logo=nuget) [View on NuGet](https://www.nuget.org/packages/Stravaig.Extensions.Configuration.Diagnostics)

## Usage

The library consists of a number of extension methods on the following interfaces:
- `IConfigurationRoot`
- `IConfiguration`
- `IDbConnection` (for convenience when dealing with connection strings)

### IConfigurationRoot

- `LogProviderNames` will list the config providers in the log at the desired log level.
  - `LogProviderNamesAsInformation` variant that logs at the Information level.
  - `LogProviderNamesAsDebug` variant that logs at the Debug level.
  - `LogProviderNamesAsTrace` variant that logs at the Trace level.

e.g.

```csharp
    configRoot.LogProviderNames(logger, LogLevel.Information);
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

### IConfiguration

It goes without saying that as the `IConfigurationRoot` interface is derived from the `IConfiguration` interface, that the extension methods here will work with an `IConfigurationRoot` reference too.

- `LogConfigurationValues` will list the configuration keys and values at the desired log level.
  - `LogConfigurationValuesAsInformation` variant that logs at the information level.
  - `LogConfigurationValuesAsDebug` variant that logs at the debug level.
  - `LogConfigurationValuesAsTrace` variant that logs at the trace level.
      
e.g.

```csharp 
config.LogConfigurationValues(logger, LogLevel.Information);
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

*Note: It will be an upcoming feature that known secrets will be masked out if desired.*

### Options Builder

```csharp
ConfigurationDiagnosticsOptions.GlobalOptions = ConfigurationDiagnosticsOptions
  .SetUpBy.Obfuscating.With(/*IObfuscator*/)
  .And.MatchingConfigurationKeys().Matching(/*IMatcher*/)
  .And.MatchingConnectionStringKeys().Matching(/*IMatcher*/)
  .AndFinally.BuildOptions();
```

```csharp
ConfigurationDiagnosticsOptions
  .SetUpBy.Obfuscating.With(/*IObfuscator*/)
  .And.MatchingConfigurationKeys().Where()
    .KeyContains(/*string*/)
    .OrContains(/*string*/)
    .OrMatchesPattern(/*regExPattern*/)
  .And().ConnectionStringKeys().Where()
    .MatchesPattern(/*regExPattern*/)
    .OrContains(/*string*/)
  .Finally.ApplyOptions(ConfigurationDiagnosticsOptions.GlobalOptions)
```


