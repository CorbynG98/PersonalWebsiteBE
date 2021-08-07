using PersonalWebsiteBE.Core.Helpers.HelperModels;
using PersonalWebsiteBE.Core.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebsiteBE.Core.Services.Auth
{
    public interface IUserService
    {
        Task<AuthData> CreateUserAsync(User newUser);
        Task UpdateUserAsync();
        Task<AuthData> LoginUserAsync(User userLoginData);
        Task LogoutUserAsync(string sessionToken);
        Task DeleteUserAsync();
    }
}
