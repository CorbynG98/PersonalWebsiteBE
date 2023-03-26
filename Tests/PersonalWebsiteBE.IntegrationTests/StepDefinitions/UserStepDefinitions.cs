using Microsoft.Extensions.DependencyInjection;
using PersonalWebsiteBE.Core.Exceptions;
using PersonalWebsiteBE.Core.Helpers.HelperModels;
using PersonalWebsiteBE.Core.Models.Auth;
using PersonalWebsiteBE.Core.Repositories.Auth;
using PersonalWebsiteBE.Core.Services.Auth;
using System.Security.Authentication;

namespace PersonalWebsiteBE.IntegrationTests.StepDefinitions
{
    [Binding]
    public sealed class AuthStepDefinition
    {

        private readonly ScenarioContext scenarioContext;
        // Services
        private readonly IUserService userService;
        // Repositories
        private readonly IUserRepository userRepository;

        public AuthStepDefinition(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
            // Services
            userService = Environments.ServiceProvider.GetRequiredService<IUserService>();
            // Repositories
            userRepository = Environments.ServiceProvider.GetRequiredService<IUserRepository>();
        }

        [Given("An existing user with username (.*)")]
        public async Task GivenAnExistingUserWithUsername(string username) {
            var user = await userRepository.GetByUsernameOnly(username);
            user.Should().NotBeNull();
        }

        [Given("No existing user with username (.*)")]
        public async Task GivenNoExistingUserWithUsername(string username)
        {
            var user = await userRepository.GetByUsernameOnly(username);
            user.Should().BeNull();
        }

        [Given("An existing user with Id (.*)")]
        public async Task GivenAnExistingUserWithId(string userId)
        {
            var user = await userRepository.GetOneAsync(userId);
            user.Should().NotBeNull();
        }

        [Given("No existing user with Id (.*)")]
        public async Task GivenNoExistingUserWithId(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId)) return;
            var user = await userRepository.GetOneAsync(userId);
            user.Should().BeNull();
        }

        [When("I login with username (.*) and password (.*)")]
        public async Task WhenILoginWithUsernameAndPassword(string username, string password) {
            var user = new User() { Username = username, Password = password };
            try
            {
                var authData = await userService.LoginUserAsync(user, "localhost");
                scenarioContext.Add("authData", authData);
            }
            catch (UserLoginException)
            {
                scenarioContext.Add("authData", null);
            }
        }

        [When("I login with no username and password (.*)")]
        public async Task WhenILoginWithNoUsernameAndPassword(string password) {
            var user = new User() { Username = null, Password = password };
            try
            {
                var authData = await userService.LoginUserAsync(user, "localhost");
                scenarioContext.Add("authData", authData);
            }
            catch (UserLoginException)
            {
                scenarioContext.Add("authData", null);
            }
        }

        [When("I login with username (.*) and no password")]
        public async Task WhenILoginWithUsernameAndNoPassword(string username)
        {
            var user = new User() { Username = username, Password = null };
            try
            {
                var authData = await userService.LoginUserAsync(user, "localhost");
                scenarioContext.Add("authData", authData);
            }
            catch (UserLoginException)
            {
                scenarioContext.Add("authData", null);
            }
        }

        [When("I logout with sessionToken (.*)")]
        public async Task WhenILogoutWithSessionToken(string sessionToken) { 
            await userService.LogoutUserAsync(sessionToken);
        }

        [Then("AuthData should not be null")]
        public void ThenAuthDataShouldNotBeNull() {
            var authData = scenarioContext.Get<AuthData>("authData");
            authData.Should().NotBeNull();
            authData.SessionToken.Should().NotBeNull();
        }

        [Then("Auth should fail")]
        public void ThenAuthDataShouldBeNull()
        {
            var authData = scenarioContext.Get<AuthData>("authData");
            authData.Should().BeNull();
        }
    }
}