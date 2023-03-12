using PersonalWebsiteBE.Core.Helpers.HelperModels;
using PersonalWebsiteBE.Core.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebsiteBE.Core.Services.Auth
{
    public interface IUserService : IService<User>
    {
        Task<AuthData> CreateUserAsync(User newUser, string ip);
        Task UpdateUserAsync();
        Task<AuthData> LoginUserAsync(User userLoginData, string ip);
        Task LogoutUserAsync(string sessionToken);
        Task<bool> VerifyUserSession(string sessionToken);
        Task DeleteUserAsync();
    }
}
