# Full Release Notes

## Version 0.6.0

Date: Sunday, 14 March, 2021 at 21:25:19 +00:00

**THIS RELEASE CONTAINS BREAKING CHANGES**


### Bugs

### Features

- #87 Serilog extensions.

### Miscellaneous

- #84 Update List-Contributors script
- #88 Refactor internal structure to support different logging frameworks by creating a new "Core" package, with the specifics for various logging frameworks ultimately residing in various satellite packages.
- #108 Refactor to improve class names, namespaces, etc.

### Dependabot

- Bump Microsoft.Extensions.Configuration from 3.1.11 to 3.1.13
- Bump Microsoft.Extensions.Configuration.Abstractions from 3.1.11 to 3.1.13
- Bump Microsoft.Extensions.Configuration.Binder from 3.1.11 to 3.1.13
- Bump Microsoft.Extensions.Configuration.CommandLine from 3.1.11 to 3.1.13
- Bump Microsoft.Extensions.Configuration.EnvironmentVariables from 3.1.11 to 3.1.13
- Bump Microsoft.Extensions.Configuration.Json from 3.1.11 to 3.1.13
- Bump Microsoft.Extensions.Configuration.UserSecrets from 3.1.11 to 3.1.13
- Bump Microsoft.Extensions.DependencyInjection from 3.1.11 to 3.1.13
- Bump Microsoft.Extensions.Logging from 3.1.11 to 3.1.13
- Bump Microsoft.Extensions.Logging.Abstractions from 3.1.11 to 3.1.13
- Bump Microsoft.Extensions.Logging.Console from 3.1.11 to 3.1.13
- Bump Microsoft.NET.Test.Sdk from 16.8.3 to 16.9.1
- Bump nunit from 3.13.0 to 3.13.1
- Bump Stravaig.Extensions.Logging.Diagnostics from 0.3.1 to 0.3.2
## Version 0.5.0

Date: Saturday, 23 January, 2021 at 14:31:04 +00:00

### Bugs

### Features

* #34: Create Func obfuscator Can now obfuscate based on a user defined function. 
* #35: Create Func matcher. Can now specify a predicate function that matches the key of secrets.
* #42: Logging all connection strings now does so in a single log message.
* #43: Logging is now using structured renderers.
* #62: Create Documentation for Read The Docs and add an example app.
* #75: Make the connection string structured renderer keys more concise.
* #79: Logging configuration source information now uses structured logging.

### Miscellaneous

* #36: Tidy the build process.
* #37: Fix release so it is not a draft.
* #38: Dependabot update.
* #40: Add missing XML docs.
* #44: Updated the documentation.
* #47: Add a Pull Request Template.
* #60: Tidy Dependabot configuration & make easily available as solution item.
* #71: Update the build process from the template.

### Dependabot

* #73 Aggregate of 
  * Bump `Microsoft.Extensions.Configuration.Binder` from 3.1.10 to 3.1.11
  * Bump `Microsoft.Extensions.Configuration.Json` from 3.1.10 to 3.1.11
  * Bump `Microsoft.Extensions.Configuration.UserSecrets` from 3.1.10 to 3.1.11
  * Bump `Microsoft.Extensions.Logging` from 3.1.10 to 3.1.11
  * Bump `Microsoft.Extensions.Logging.Console` from 3.1.10 to 3.1.11
* Bump `Microsoft.Extensions.Configuration` from 3.1.10 to 3.1.11
* Bump `Microsoft.Extensions.Configuration.CommandLine` from 3.1.10 to 3.1.11
* Bump `Microsoft.Extensions.Configuration.Environment` from 3.1.10 to 3.1.11
* Bump `Microsoft.Extensions.DependencyInjection` from 3.1.10 to 3.1.11
* Bump `Microsoft.Extensions.Logging.Abstractions` from 3.1.10 to 3.1.11
* Bump `Microsoft.NET.Test.Sdk` from 16.8.0 to 16.8.3
* Bump `nunit` from 3.12.0 to 3.13.0
* Bump `Shouldly` from 4.0.1 to 4.0.3
* Bump `Stravaig.Extensions.Logging.Diagnostics`
  * from 0.2.2 to 0.3.0
  * from 0.3.0 to 0.3.1




## Version 0.4.0

Date: Sunday, 29 November, 2020 at 22:52:54 +00:00

### Bugs

* Issue #31: Structured logging providers (e.g. Serilog) were not rendering correctly on Connection String logging

### Features

* Issue #7: Log the source of a value in the configuration.

### Miscellaneous

* Issue #32: Improve the build process to mark a release automatically in the build script.

---
