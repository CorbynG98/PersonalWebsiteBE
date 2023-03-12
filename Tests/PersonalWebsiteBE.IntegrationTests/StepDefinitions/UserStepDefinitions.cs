using Microsoft.Extensions.DependencyInjection;
using PersonalWebsiteBE.Core.Helpers.HelperModels;
using PersonalWebsiteBE.Core.Models.Auth;
using PersonalWebsiteBE.Core.Repositories.Auth;
using PersonalWebsiteBE.Core.Services.Auth;

namespace PersonalWebsiteBE.IntegrationTests.StepDefinitions
{
    [Binding]
    public sealed class UserStepDefinition
    {

        private readonly ScenarioContext scenarioContext;
        // Services
        private readonly IUserService userService;
        // Repositories
        private readonly IUserRepository userRepository;

        public UserStepDefinition(ScenarioContext scenarioContext)
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

        [When("I login with username (.*) and password (.*)")]
        public async Task WhenILoginWithUsernameAndPassword(string username, string password) {
            var user = new User() { Username = username, Password = password };
            var authData = await userService.LoginUserAsync(user, "localhost");
            scenarioContext.Add("authData", authData);
        }

        [Then("AuthData should not be null")]
        public void ThenAuthDataShouldNotBeNull() {
            var authData = scenarioContext.Get<AuthData>("authData");
            authData.Should().NotBeNull();
            authData.SessionToken.Should().NotBeNull();
        }
    }
}