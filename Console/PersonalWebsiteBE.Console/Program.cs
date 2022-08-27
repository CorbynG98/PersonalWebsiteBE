using Microsoft.Extensions.DependencyInjection;
using PersonalWebsiteBE.Core.Helpers.Security;
using PersonalWebsiteBE.Core.Repositories.Auth;
using PersonalWebsiteBE.Core.Models.Core;
using PersonalWebsiteBE.Core.Constants;
using PersonalWebsiteBE.Core.Repositories.Core;
using PersonalWebsiteBE.Core.Services.Core;

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

        var projectService = serviceProvider.GetService<IProjectService>();
        var project = new Project()
        {
            Name = "Personal Website BE",
            Description = "Back end code that powers my personal website",
            Source = "https://github.com/CorbynG98/PersonalWebsiteBE",
            LiveUrl = null,
            Stars = 1,
            ImageUrl = null,
            TechStack = new List<string>() { "C#", ".NET" }
        };
        await projectService.CreateProjectAsync(project);

        Console.WriteLine("Not Corbyn's Terminal");
        Console.WriteLine("Done... [hit any key to close]");
        Console.ReadKey();
    }
}
