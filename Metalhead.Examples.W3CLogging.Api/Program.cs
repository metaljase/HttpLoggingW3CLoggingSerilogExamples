using Microsoft.AspNetCore.HttpLogging;

namespace Metalhead.Examples.W3CLogging.Api;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddAuthorization();

        // Add W3C Logging services.
        builder.Services.AddW3CLogging(options =>
        {
            options.LoggingFields = W3CLoggingFields.All;
            options.RetainedFileCountLimit = 3; // Retain only the last 3 log files.
            options.LogDirectory = @"Logs\W3C"; // Override the default log directory.
            options.FileName = "W3CLog"; // Override the default log filename.
        });

        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
        }

        app.UseHttpsRedirection();

        app.UseW3CLogging(); // Add W3CLogging middleware to the pipeline.

        app.UseAuthorization();

        app.MapGet("/", () => "Hello World!");

        app.Run();
    }
}
