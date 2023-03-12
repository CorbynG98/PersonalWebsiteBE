using PersonalWebsiteBE.Core.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebsiteBE.Core.Repositories.Auth
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserByUsernameAndPassword(string username, string password);
        Task<User> GetByUsernameOnly(string username);
        Task CreateLoginActivityAsync(string id, AuthActivity activity);
    }
}
