using Microsoft.Extensions.DependencyInjection;
using PersonalWebsiteBE.Core.Helpers.Security;
using PersonalWebsiteBE.Core.Repositories.Auth;
using PersonalWebsiteBE.Core.Models.Core;
using PersonalWebsiteBE.Core.Constants;
using PersonalWebsiteBE.Core.Repositories.Core;
using PersonalWebsiteBE.Core.Services.Core;
using PersonalWebsiteBE.Core.Models.Auth;

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

        await serviceProvider.ManuallyResetUserPassword("8GuVX9H9fx5WwCB8HVeQ", "Th1515594rt4");
        // var data = await serviceProvider.GetDocumentsInCollectionJSONAsync<User>();

        Console.WriteLine("Not Corbyn's Terminal");
        Console.WriteLine("Done... [hit any key to close]");
        Console.ReadKey();
    }
}
