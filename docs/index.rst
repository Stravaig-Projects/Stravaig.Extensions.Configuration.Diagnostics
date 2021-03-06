.. Stravaig Configuration Diagnostics documentation master file, created by
   sphinx-quickstart on Tue Jan  5 16:04:48 2021.
   You can adapt this file completely to your liking, but it should at least
   contain the root `toctree` directive.

Stravaig Projects: Configuration Diagnostics documentation!
===========================================================

``Stravaig.Configuration.Diagnostics`` is a set 
of extensions to assist diagnosing configuration setting 
issues by logging the current state which allows them to 
be examined. It also provides options for obfuscating 
secret values so that they are not exposed in application 
logs.

.. toctree::
   :maxdepth: 2
   :caption: Introduction:

   intro/quick-start-logging
   intro/quick-start-serilog
   intro/installing-the-package
   intro/diagnostics-options

.. toctree::
   :maxdepth: 2
   :caption: Microsoft Logging Extensions

   logging-extensions/configuration-values
   logging-extensions/configuration-providers
   logging-extensions/track-value-source
   logging-extensions/connection-strings

.. toctree::
   :maxdepth: 2
   :caption: Serilog Extensions

   serilog/configuration-values
   serilog/configuration-providers
   serilog/track-value-source
   serilog/connection-strings

.. toctree::
   :maxdepth: 2
   :caption: Advanced

   advanced/creating-obfuscators
   advanced/creating-matchers
   advanced/creating-renderers

Indices and tables
==================

* :ref:`genindex`
* :ref:`modindex`
* :ref:`search`
