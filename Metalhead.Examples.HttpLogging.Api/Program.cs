using Microsoft.AspNetCore.HttpLogging;

namespace Metalhead.Examples.HttpLogging.Api;

internal class Program
{
    private static readonly string[] s_myResponseHeader = ["My Response Header Value"];

    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddAuthorization();

        // Add HttpLogging services.
        builder.Services.AddHttpLogging(options =>
        {
            //options.LoggingFields = HttpLoggingFields.RequestHeaders | HttpLoggingFields.RequestBody | HttpLoggingFields.Response;
            options.LoggingFields = HttpLoggingFields.All; // Log all request and response fields.
            //options.CombineLogs = true; // Combine the request, request body, response, response body, and duration into a single log entry.
            options.RequestBodyLogLimit = 2048; // Log only the first 2048 bytes of the request body.
            options.RequestHeaders.Add("sec-fetch-mode"); // Do not redact value of 'sec-fetch-mode' header in log.

            options.ResponseBodyLogLimit = 1024; // Log only the first 1024 bytes of the response body.
            options.ResponseHeaders.Add("MyResponseHeader"); // Do not redact value of 'MyResponseHeader' header in log.
        });

        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
        }

        app.UseHttpsRedirection();

        app.UseHttpLogging(); // Add HttpLogging middleware to the pipeline.

        // Add a response header to demonstrate that the response headers are logged.
        app.Use(async (context, next) =>
        {
            context.Response.Headers["MyResponseHeader"] = s_myResponseHeader;
            await next();
        });

        app.UseAuthorization();

        app.MapGet("/", () => "Hello World!");

        app.Run();
    }
}