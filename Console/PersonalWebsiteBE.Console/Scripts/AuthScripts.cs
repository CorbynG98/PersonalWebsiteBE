using Microsoft.Extensions.DependencyInjection;
using PersonalWebsiteBE.Core.Helpers.Security;
using PersonalWebsiteBE.Core.Services.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class AuthScripts
{
    public static async Task<string> ManuallyResetUserPassword(this IServiceProvider serviceProvider, string userId, string password)
    {
        // Find the user
        var userService = serviceProvider.GetService<IUserService>();
        var user = await userService.GetByIdAsync(userId);
        // Generate salt and hash password with it
        var hashedPassword = HashData.GetHashString(password);
        // Set data on user and save
        user.Password = hashedPassword;
        await userService.SaveAsync(user.Id, user);
        // Return new password
        return password;
    }
}
