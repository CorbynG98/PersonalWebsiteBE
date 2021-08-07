using PersonalWebsiteBE.Core.Models.Auth;
using PersonalWebsiteBE.Core.Services.Auth;
using PersonalWebsiteBE.Core.Repositories.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonalWebsiteBE.Core.Helpers.Security;
using PersonalWebsiteBE.Core.Exceptions;
using PersonalWebsiteBE.Core.Constants;
using PersonalWebsiteBE.Core.Helpers.HelperModels;

namespace PersonalWebsiteBE.Services.Services.Auth
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly ISessionRepository sessionRepository;

        public UserService(IUserRepository userRepository, ISessionRepository sessionRepository)
        {
            this.userRepository = userRepository;
            this.sessionRepository = sessionRepository;
        }

        public async Task<AuthData> CreateUserAsync(User newUser) {
            // Hash the password and plonk the object in database. Set some default values.
            newUser.Password = HashData.GetHashString(newUser.Password);
            var userId = await userRepository.CreateOneAsync(newUser);

            // Create session and put in database
            var sessionToken = HashData.GetHashString(Guid.NewGuid().ToString("N")); // Need this here as we return it to user for their logged in session
            var session = new Session() { SessionToken = HashData.GetHashString(sessionToken), UserId = userId };
            var sessionId = await sessionRepository.CreateOneAsync(session);

            // Create activity for this login and put in database
            var activity = new AuthActivity() { ActionedAt = DateTime.UtcNow, SessionId = sessionId, Type = AuthActivityType.Login };
            await userRepository.CreateLoginActivityAsync(userId, activity);

            // Return session token to the controller
            return new AuthData() { SessionToken = sessionToken, Username = newUser.Username };
        }

        public async Task UpdateUserAsync() { 
            
        }

        public async Task<AuthData> LoginUserAsync(User userLoginData) {
            // Get the user from database
            var user = await userRepository.GetUserByUsernameAndPassword(userLoginData.Username, HashData.GetHashString(userLoginData.Password));
            if (user == null) throw new UserLoginException("Username or password incorrect");
            var userId = user.Id;

            // Create session and put in database
            var sessionToken = HashData.GetHashString(Guid.NewGuid().ToString("N")); // Need this here as we return it to user for their logged in session
            var session = new Session() { SessionToken = HashData.GetHashString(sessionToken), UserId = userId };
            var sessionId = await sessionRepository.CreateOneAsync(session);

            // Create activity for this login and put in database
            var activity = new AuthActivity() { ActionedAt = DateTime.UtcNow, SessionId = sessionId, Type = AuthActivityType.Login };
            await userRepository.CreateLoginActivityAsync(userId, activity);

            // Return session token to the controller
            return new AuthData() { SessionToken = sessionToken, Username = user.Username };
        }

        public async Task LogoutUserAsync(string sessionToken) {
            // Find session by session token
            var session = await sessionRepository.GetSessionByTokenAsync(HashData.GetHashString(sessionToken));
            if (session == null) return;

            // Create activity for this logout and put in database
            var activity = new AuthActivity() { ActionedAt = DateTime.UtcNow, SessionId = session.Id, Type = AuthActivityType.Logout };
            await userRepository.CreateLoginActivityAsync(session.UserId, activity);

            // Delete the session
            await sessionRepository.DeleteOneAsync(session.Id);
        }

        public async Task DeleteUserAsync() { 
        
        }
    }
}
