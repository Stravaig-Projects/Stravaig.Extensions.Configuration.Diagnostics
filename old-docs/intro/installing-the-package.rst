.. _refInstalling:

Installing
==========

This library is made up of several packages. The core functionality is contained in a package called ``Stravaig.Configuration.Diagnostics.Core``, the Logger extensions are in ``Stravaig.Configuration.Diagnostics.Logging``.

All the packages that contain the final output form (e.g. Logging) also reference the Core package, so there is no need to include that separately. It also means that you only have one package 

The latest packages are available on `NuGet`_. 

.. _NuGet: https://www.nuget.org/packages?q=Stravaig.Configuration.Diagnostics

.. list-table:: packages
   :header-rows: 1

   * - Package
     - Stable Version
     - Preview Version
   * - Stravaig.Configuration.Diagnostics.Core
     - .. image:: https://img.shields.io/nuget/v/Stravaig.Configuration.Diagnostics.Core?color=004880&label=nuget%20stable&logo=nuget
     - .. image:: https://img.shields.io/nuget/vpre/Stravaig.Configuration.Diagnostics.Core?color=ffffff&label=nuget%20latest&logo=nuget)
   * - Stravaig.Configuration.Diagnostics.Logging
     - .. image:: https://img.shields.io/nuget/v/Stravaig.Configuration.Diagnostics.Logging?color=004880&label=nuget%20stable&logo=nuget
     - .. image:: https://img.shields.io/nuget/vpre/Stravaig.Configuration.Diagnostics.Logging?color=ffffff&label=nuget%20latest&logo=nuget)
   * - Stravaig.Configuration.Diagnostics.Serilog
     - .. image:: https://img.shields.io/nuget/v/Stravaig.Configuration.Diagnostics.Serilog?color=004880&label=nuget%20stable&logo=nuget
     - .. image:: https://img.shields.io/nuget/vpre/Stravaig.Configuration.Diagnostics.Serilog?color=ffffff&label=nuget%20latest&logo=nuget)

Installing from a PowerShell prompt
-----------------------------------

You can install the package into your project from a PowerShell 
prompt. Navigate to the folder your project file is in and type:

::

    Install-Package Stravaig.Configuration.Diagnostics.Logging

or

::

    Install-Package Stravaig.Configuration.Diagnostics.Serilog

Installing using the .NET CLI
-----------------------------

You can install the package into your project with the .NET CLI 
command. Navigate to the folder your project file is in and type:

::

    dotnet add package Stravaig.Configuration.Diagnostics.Logging

or

::

    dotnet add package Stravaig.Configuration.Diagnostics.Serilog

