---
layout: default
title: Quick start with Serilog
---

# Quick start with Serilog

Install the `Stravaig.Configuration.Diagnostics.Serilog` into your main project. (See [Installing the Nuget Package](installing-the-package.md) for 
details of installing the NuGet Package.)

In your `Startup` class in the `Configure` or `ConfigureServices` method add the following:

```
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    // Get the logger
    var logger = Log.ForContext<Startup>();

    // Define which keys contain secrets
    ConfigurationDiagnosticsOptions.SetupBy
        .Obfuscating.WithAsterisks()
        .And.MatchingConfigurationKeys.Where
        .KeyContains("AccessToken")
        .OrContains("ConnectionString")
        .And.MatchingConnectionStringKeys
        .Containing("Password")
        .AndFinally.ApplyOptions(ConfigurationDiagnosticsOptions.GlobalOptions);
    
    // Log the state of the configuration
    logger.LogProvidersAsInformation(Configuration);
    logger.LogConfigurationValuesAsInformation(Configuration);
    logger.LogConfigurationKeySourceAsInformation(Configuration, "ExtenalSystem:AccessToken");
    logger.LogAllConnectionStringsAsInformation(Configuration);

    // Set up other things...
}
```

The above setup up the diagnostics as follows:

* Any secret that is found is obfuscated by replacing the value with asterisks.
* Any configuration key that contains "AccessToken" is considered a secret an will be obfuscated.
* Any configuration key that contains "ConnectionString" is considered a secret.
* When deconstructing connection strings, any part where the key contains "password"is considered a secret.

## LogProvidersAsInformation

The output will look something like this:

```
info: Example.Startup[0]
    The following configuration providers were registered:
    Microsoft.Extensions.Configuration.ChainedConfigurationProvider
    JsonConfigurationProvider for 'appsettings.json' (Optional)
    JsonConfigurationProvider for 'appsettings.Development.json' (Optional)
    JsonConfigurationProvider for 'secrets.json' (Optional)
    EnvironmentVariablesConfigurationProvider
    CommandLineConfigurationProvider
```

## LogConfigurationValuesAsInformation

The output will look something like this:

```
info: Example.Startup[0]
    The following values are available:
    Logging : (null)
    Logging:LogLevel : (null)
    Logging:LogLevel:Microsoft.Hosting.Lifetime : Information
    Logging:LogLevel:Microsoft : Warning
    Logging:LogLevel:Default : Information
    ExternalSystem : (null)
    ExternalSystem:Url : https://dev.some-external-system.com/api/v1
    ExternalSystem:AccessToken : *****************************
    ExtenalSystem : (null)
    ExtenalSystem:AccessToken : **************************************
    ENVIRONMENT : Development
    ConnectionStrings :
    ConnectionStrings:PostCodeLookupDatabase : **********************************************************************************************************************************************************************************
    ConnectionStrings:MainDatabase : ********************************************************************************************************
    ComSpec : C:\WINDOWS\system32\cmd.exe
    ASPNETCORE_URLS : https://localhost:5001;http://localhost:5000
    ASPNETCORE_ENVIRONMENT : Development
    applicationName : Example
    AllowedHosts : *
```

Note: I've omitted many of the environment variables from the above example.

One of the things you can see here, is that there is a typo in one of the configuration providers. If we had an issue with the `ExternalSystem.AccessToken` we can now see that somewhere it has been mistyped.

## LogConfigurationKeySourceAsInformation

It is possible to trace where a specific key value came from, so in the case of the key with the typo, you can add a line to log out the source of that specific key/value.

```
    logger.LogConfigurationKeySourceAsInformation(Configuration, "ExtenalSystem:AccessToken");
```

The output looks something like this:

```
info: Example.Startup[0]
    Provider sources for value of ExtenalSystem:AccessToken
    * Microsoft.Extensions.Configuration.ChainedConfigurationProvider ==> null
    * JsonConfigurationProvider for 'appsettings.json' (Optional) ==> null
    * JsonConfigurationProvider for 'appsettings.Development.json' (Optional) ==> null
    * JsonConfigurationProvider for 'secrets.json' (Optional) ==> **************************************
    * EnvironmentVariablesConfigurationProvider ==> null
    * CommandLineConfigurationProvider ==> null
```

As the only provider with a value for the key with the typo is the `secrets.json` file we can instantly tell where the issue is.

## LogAllConnectionStringsAsInformation

Although we've designated that any configuration key that matches `ConnectionString` has a secret value associated with it, we can deconstruct a connection string into its component parts as they are not all secrets. This way you can examine a large portion of a connection string without exposing, for example, the password used to access it.

The output looks something like this:

```
info: Example.Startup[0]
    The following connection strings were found: MainDatabase, PostCodeLookupDatabase.
    Connection string (named MainDatabase) parameters:
    * server = dev.my-database-server.my-company.com
    * database = myDataBase
    * user id = myUsername
    * password = **********

    Connection string (named PostCodeLookupDatabase) parameters:
    * provider = MSOLEDBSQL
    * server = tcp:AvailabilityGroupListenerDnsName,1433
    * multisubnetfailover = Yes
    * applicationintent = ReadOnly
    * database = MyDB
    * integrated security = SSPI
    * connect timeout = 30
```
