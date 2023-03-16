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
using PersonalWebsiteBE.Core.Services.Core;
using PersonalWebsiteBE.IpApi;

namespace PersonalWebsiteBE.Services.Services.Auth
{
    public class UserService : Service<User>, IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly ISessionRepository sessionRepository;
        private readonly IpApiIntegration ipApiIntegration;

        public UserService(IUserRepository userRepository, ISessionRepository sessionRepository) : base(userRepository)
        {
            this.userRepository = userRepository;
            this.sessionRepository = sessionRepository;
            this.ipApiIntegration = new IpApiIntegration();
        }

        public async Task<AuthData> CreateUserAsync(User newUser, string ip) {
            // Hash the password and plonk the object in database. Set some default values.
            newUser.Password = HashData.GetHashString(newUser.Password);
            var userId = await userRepository.CreateOneAsync(newUser);

            // Get data on IpAddress
            IpApiData ipApiData = await ipApiIntegration.GetIpInformation(ip);

            // Create session and put in database
            var sessionToken = HashData.GetHashString(Guid.NewGuid().ToString("N")); // Need this here as we return it to user for their logged in session
            var session = new Session()
            {
                SessionToken = HashData.GetHashString(sessionToken),
                UserId = userId,
                IpAddress = ip,
                CountryCode = ipApiData.CountryCode,
                Country = ipApiData.Country,
                RegionName = ipApiData.RegionName,
                Latitude = ipApiData.Lat,
                Longitude = ipApiData.Lon,
                City = ipApiData.City
            };
            var sessionId = await sessionRepository.CreateOneAsync(session);

            // Create activity for this login and put in database
            var activity = new AuthActivity() { ActionedAt = DateTime.UtcNow, UserId = userId, SessionId = sessionId, Type = AuthActivityType.Login };
            await userRepository.CreateLoginActivityAsync(userId, activity);

            // Return session token to the controller
            return new AuthData() { SessionToken = sessionToken, Username = newUser.Username };
        }

        public async Task UpdateUserAsync() {

        }

        public async Task<AuthData> LoginUserAsync(User userLoginData, string ip) {
            // Get the user from database
            if (string.IsNullOrWhiteSpace(userLoginData.Username) || string.IsNullOrWhiteSpace(userLoginData.Password)) throw new UserLoginException("Username or password incorrect");
            var user = await userRepository.GetUserByUsernameAndPassword(userLoginData.Username, HashData.GetHashString(userLoginData.Password));
            if (user == null) throw new UserLoginException("Username or password incorrect");
            var userId = user.Id;

            // Get data on IpAddress
            IpApiData ipApiData;
            if (ip == "0.0.0.1" || ip == "127.0.0.1" || ip == "localhost")
            {
                ipApiData = new()
                {
                    CountryCode = "localhost",
                    Country = "localhost",
                    RegionName = "localhost",
                    Lat = 0,
                    Lon = 0,
                    City = "localhost",
                };
            }
            else
            {
                ipApiData = await ipApiIntegration.GetIpInformation(ip);
            }

            // Create session and put in database
            var sessionToken = HashData.GetHashString(Guid.NewGuid().ToString("N")); // Need this here as we return it to user for their logged in session
            var session = new Session() { 
                SessionToken = HashData.GetHashString(sessionToken), 
                UserId = userId,
                IpAddress = ip,
                CountryCode = ipApiData.CountryCode,
                Country = ipApiData.Country,
                RegionName = ipApiData.RegionName,
                Latitude = ipApiData.Lat,
                Longitude = ipApiData.Lon,
                City = ipApiData.City
            };
            var sessionId = await sessionRepository.CreateOneAsync(session);

            // Create activity for this login and put in database
            var activity = new AuthActivity() { ActionedAt = DateTime.UtcNow, UserId = userId, SessionId = sessionId, Type = AuthActivityType.Login };
            await userRepository.CreateLoginActivityAsync(userId, activity);

            // Update last login date
            user.LastLoginAt = DateTime.UtcNow;
            await userRepository.UpdateOneAsync(userId, user);

            // Return session token to the controller
            return new AuthData() { SessionToken = sessionToken, Username = user.Username };
        }

        public async Task LogoutUserAsync(string sessionToken) {
            // Find session by session token
            var session = await sessionRepository.GetSessionByTokenAsync(HashData.GetHashString(sessionToken));
            if (session == null) return;

            // Create activity for this logout and put in database
            var activity = new AuthActivity() { ActionedAt = DateTime.UtcNow, UserId = session.UserId, SessionId = session.Id, Type = AuthActivityType.Logout };
            await userRepository.CreateLoginActivityAsync(session.UserId, activity);

            // Delete the session
            await sessionRepository.DeleteOneAsync(session.Id);
        }

        public async Task<bool> VerifyUserSession(string sessionToken)
        {
            // Find session by session token
            var session = await sessionRepository.GetSessionByTokenAsync(HashData.GetHashString(sessionToken));
            if (session == null) return false;
            return true;
        }

        public async Task DeleteUserAsync() { 
        
        }
    }
}
