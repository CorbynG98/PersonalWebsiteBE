using Microsoft.Extensions.DependencyInjection;
using PersonalWebsiteBE.Core.Exceptions;
using PersonalWebsiteBE.Core.Helpers.HelperModels;
using PersonalWebsiteBE.Core.Helpers.Security;
using PersonalWebsiteBE.Core.Models.Auth;
using PersonalWebsiteBE.Core.Repositories.Auth;
using PersonalWebsiteBE.Core.Services.Auth;
using PersonalWebsiteBE.Services.Services.Auth;
using System.Security.Authentication;

namespace PersonalWebsiteBE.IntegrationTests.StepDefinitions
{
    [Binding]
    public sealed class ActivityStepDefinition
    {

        private readonly ScenarioContext scenarioContext;
        // Services
        private readonly IActivityService activityService;
        // Repositories
        private readonly IActivityRepository activityRepository;

        public ActivityStepDefinition(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
            // Services
            activityService = Environments.ServiceProvider.GetRequiredService<IActivityService>();
            // Repositories
            activityRepository = Environments.ServiceProvider.GetRequiredService<IActivityRepository>();
        }

        [When("I get all the activities for user with Id (.*)")]
        public async Task WhenIGetActivitiesByUserId(string userId)
        {
            var activities = await activityService.GetActivitiesByUserId(userId, 0, 5);
            scenarioContext.Add("activities", activities);
        }

        [Then("The count of activities should be more than 1")]
        public void ThenTheCountOfSessionsShouldBeMoreThan1()
        {
            var sessions = scenarioContext.Get<List<AuthActivity>>("activities");
            sessions.Should().NotBeNull();
            sessions.Should().HaveCountGreaterThanOrEqualTo(1);
        }

        [Then("The count of activities should be 0")]
        public void ThenTheCountOfSessionsShouldBe0()
        {
            var sessions = scenarioContext.Get<List<AuthActivity>>("activities");
            sessions.Should().HaveCount(0);
        }
    };
}