using Microsoft.Extensions.DependencyInjection;
using PersonalWebsiteBE.Core.Exceptions;
using PersonalWebsiteBE.Core.Helpers.HelperModels;
using PersonalWebsiteBE.Core.Helpers.Security;
using PersonalWebsiteBE.Core.Models.Auth;
using PersonalWebsiteBE.Core.Repositories.Auth;
using PersonalWebsiteBE.Core.Services.Auth;
using System.Security.Authentication;

namespace PersonalWebsiteBE.IntegrationTests.StepDefinitions
{
    [Binding]
    public sealed class SessionStepDefinition
    {

        private readonly ScenarioContext scenarioContext;
        // Services
        private readonly ISessionService sessionService;
        // Repositories
        private readonly ISessionRepository sessionRepository;

        public SessionStepDefinition(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
            // Services
            sessionService = Environments.ServiceProvider.GetRequiredService<ISessionService>();
            // Repositories
            sessionRepository = Environments.ServiceProvider.GetRequiredService<ISessionRepository>();
        }
        

        [Given("An existing session with sessionToken as (.*)")]
        public async Task GivenAnExistingSessionWithToken(string sessionToken) {
            var result = await sessionRepository.GetSessionByTokenAsync(HashData.GetHashString(sessionToken));
            result.Should().NotBeNull();
        }

        [Given("No existing session with sessionToken as (.*)")]
        public async Task GivenNoExistingSessionWithToken(string sessionToken)
        {
            var result = await sessionRepository.GetSessionByTokenAsync(HashData.GetHashString(sessionToken));
            result.Should().BeNull();
        }

        [Given("No existing session with no sessionToken")]
        public async Task GivenNoExistingSessionWithNoToken()
        {
            var result = await sessionRepository.GetSessionByTokenAsync(HashData.GetHashString(""));
            result.Should().BeNull();
        }

        [When("I verify the session with token (.*)")]
        public async Task WhenIVerifyASessionWithToken(string sessionToken) {
            var result = await sessionService.VerifyUserSession(sessionToken);
            scenarioContext.Add("verifySessionResult", result);
        }

        [When("I verify the session with no token")]
        public async Task WhenIVerifyASessionWithNoToken()
        {
            var result = await sessionService.VerifyUserSession("");
            scenarioContext.Add("verifySessionResult", result);
        }

        [When("I get all the sessions for user with Id (.*)")]
        public async Task WhenIGetAllSessionsByUserId(string userId) {
            var sessions = await sessionService.GetSessionsForUser(userId, 0, 5);
            scenarioContext.Add("sessions", sessions);
        }

        [Then("The verify result should be true")]
        public void ThenTheVerifyResultShouldBeTrue() {
            var result = scenarioContext.Get<bool>("verifySessionResult");
            result.Should().BeTrue();
        }

        [Then("The verify result should be false")]
        public void ThenTheVerifyResultShouldBeFalse()
        {
            var result = scenarioContext.Get<bool>("verifySessionResult");
            result.Should().BeFalse();
        }

        [Then("No existing session with sessionToken as (.*)")]
        public async Task ThenNoExistingSessionWithToken(string sessionToken)
        {
            var result = await sessionRepository.GetSessionByTokenAsync(HashData.GetHashString(sessionToken));
            result.Should().BeNull();
        }
        
        [Then("The count of session should be more than 1")]
        public void ThenTheCountOfSessionsShouldBeMoreThan1() {
            var sessions = scenarioContext.Get<List<Session>>("sessions");
            sessions.Should().NotBeNull();
            sessions.Should().HaveCountGreaterThanOrEqualTo(1);
        }

        [Then("The count of session should be 0")]
        public void ThenTheCountOfSessionsShouldBe0()
        {
            var sessions = scenarioContext.Get<List<Session>>("sessions");
            sessions.Should().HaveCount(0);
        }
    };
}