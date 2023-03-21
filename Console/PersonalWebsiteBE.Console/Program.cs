using Microsoft.Extensions.DependencyInjection;
using PersonalWebsiteBE.Core.Helpers.Security;
using PersonalWebsiteBE.Core.Repositories.Auth;
using PersonalWebsiteBE.Core.Models.Core;
using PersonalWebsiteBE.Core.Constants;
using PersonalWebsiteBE.Core.Repositories.Core;
using PersonalWebsiteBE.Core.Services.Core;
using PersonalWebsiteBE.Core.Models.Auth;
using PersonalWebsiteBE.Services.Helpers.GoogleCloud;
using PersonalWebsiteBE.Core.Settings;
using PersonalWebsiteBE.Core.Static;

public class Program
{
    public static async Task Main(string[] args)
    {
        // ========================================
        // ========================================
        // ========== DO NOT DELETE THIS ==========
        // ========================================
        // ========================================
        var serviceProvider = Startup.ConfigureServices();
        // ========================================
        // ========================================
        // ========================================

        Console.WriteLine("Not Corbyn's Terminal");
        Console.WriteLine("Done... [hit any key to close]");
        Console.ReadKey();
    }
}
