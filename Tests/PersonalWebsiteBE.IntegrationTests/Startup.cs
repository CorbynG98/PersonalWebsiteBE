using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using PersonalWebsiteBE.Core.Settings;
using System.IO;
using PersonalWebsiteBE.Core.Extensions;

public static class Startup
{
    public static IServiceProvider ConfigureServices()
    {
        #if DEBUG || TEST
        var keyPath = "C:\\Git\\Personal Website\\Keys\\personal-313620.json"; // Change this if running on different computer
        if (File.Exists(keyPath))
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", keyPath);
        #endif

        var services = new ServiceCollection();
        // Get the appsettings file and build config
        IConfigurationBuilder builder = new ConfigurationBuilder();
        builder = builder.SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Test.json", optional: false, reloadOnChange: false)
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
