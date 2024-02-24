using Serilog;
using System.Reflection;

namespace Metalhead.Examples.SerilogRequestLogging.Api;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Load configuration and settings.
        IConfigurationRoot configuration = GetConfiguration(builder);

        // Configure the logger.
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
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

    private static IConfigurationRoot GetConfiguration(IHostApplicationBuilder builder)
    {
        var configurationBuilder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false, true)
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true);

        if (builder.Environment.IsDevelopment())
        {
            configurationBuilder.AddUserSecrets(Assembly.GetExecutingAssembly(), true);
        }

        return configurationBuilder.Build();
    }
}
