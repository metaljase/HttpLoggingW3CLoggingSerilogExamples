using Serilog;

namespace Metalhead.Examples.SerilogRequestLogging.Api;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Configure the logger.
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .Enrich.FromLogContext()
            .CreateLogger();

        builder.Host.UseSerilog(); // Set Serilog as the logging provider.

        builder.Services.AddAuthorization();

        var app = builder.Build();

        app.UseSerilogRequestLogging(); // Add the SerilogRequestLogging middleware.

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();

        app.MapGet("/", () => "Hello World!");

        try
        {
            app.Run();
        }
        finally
        {
            // Flush and close the log before application exit.
            Log.CloseAndFlush();
        }
    }
}
