using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using PersonalWebsiteBE.Core.Extensions;
using PersonalWebsiteBE.Core.Settings;
using System.IO;

public static class Startup
{
    public static IServiceProvider ConfigureServices()
    {
        #if DEBUG
        var keyPath = "C:\\Git\\Personal website\\keys\\personal-313620-455b9a56117a.json"; // Change this if running on different computer
        Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", keyPath);
        #endif

        var services = new ServiceCollection();

        // Get the appsettings file and build config
        IConfigurationBuilder builder = new ConfigurationBuilder();
        builder = builder.SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: false)
            .AddEnvironmentVariables();
        IConfiguration configuration = builder.Build();
        services.AddSingleton(configuration);

        services.AddOptions();

        // Configuration based injects
        services.AddConfigurationLayer(configuration);
        // Repositories
        services.AddRepositoryLayer();
        // Services
        services.AddServiceLayer();

        return services.BuildServiceProvider();
    }
}
