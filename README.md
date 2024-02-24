# What is HttpLoggingW3CLoggingSerilogExamples?

HttpLoggingW3CLoggingExamples contains four ASP.NET Core 8.0 minimal APIs that demonstrates examples of logging HTTP requests and responses.

The solution contains the following projects:
- `Metalhead.Examples.W3CLogging.Api` Implements [W3C Logging](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/w3c-logger/?view=aspnetcore-8.0) to write HTTP requests and responses to a rolling log file.
- `Metalhead.Examples.HttpLogging.Api` Implements [HTTP Logging](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/http-logging/?view=aspnetcore-8.0) to emit HTTP requests and responses to the Kestrel console.
- `Metalhead.Examples.HttpLoggingSerilog.Api` Implements [HTTP Logging](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/http-logging/?view=aspnetcore-8.0) to emit HTTP requests and responses to the Kestrel console, and also writes to a rolling log file using Serilog.
- `Metalhead.Examples.SerilogRequestLogging.Api` Implements Serilog's Request Logging to emit HTTP requests to the Kestrel console, and also writes to a rolling log file using Serilog.