using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;
using Solen.Core.Application.Auth.Queries;
using Solen.Core.Domain.Common.Entities;
using Solen.Core.Domain.Identity.Entities;
using Solen.Core.Domain.Identity.Enums.UserStatuses;
using Solen.Core.Domain.Subscription.Constants;
using Solen.SpecTests;
using TechTalk.SpecFlow;

namespace Auth.SpecTests.Queries.CheckPasswordToken
{
    [Binding, Scope(Tag = "CheckPasswordToken")]
    public class CheckPasswordTokenSteps
    {
        private const string BaseUrl = "/api/auth/checkPasswordToken";
        private CheckPasswordTokenQuery _query;

        private AuthTestsFactory _factory;
        private HttpClient _client;
        private HttpResponseMessage _response;


        [BeforeScenario]
        public void ScenarioSetUp()
        {
            _factory = new AuthTestsFactory();
            _client = _factory.GetAnonymousClient();
        }

        [AfterScenario]
        public void ScenarioTearDown()
        {
            _factory.Dispose();
            _client.Dispose();
        }

        [When(@"The password reset token is null")]
        public async Task WhenThePasswordResetTokenIsNull()
        {
            _query = new CheckPasswordTokenQuery {PasswordToken = null};

            _response = await _client.PostAsync(BaseUrl, Utilities.GetRequestContent(_query));
        }

        [Then(@"The system should return a bad request error")]
        public void ThenTheSystemShouldReturnABadRequestError()
        {
            Assert.That(_response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [When(@"The password reset token is invalid")]
        public async Task WhenThePasswordResetTokenIsInvalid()
        {
            _query = new CheckPasswordTokenQuery {PasswordToken = "Invalid token"};

            _response = await _client.PostAsync(BaseUrl, Utilities.GetRequestContent(_query));
        }

        [Then(@"The system should return a not found error")]
        public void ThenTheSystemShouldReturnANotFoundError()
        {
            Assert.That(_response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [When(@"The password reset token is valid")]
        public async Task WhenThePasswordResetTokenIsValid()
        {
            var organization = new Organization("Test organization", SubscriptionPlans.Free);
            _factory.CreateOrganization(organization);

            var user = new User("email", organization.Id);
            user.ChangeUserStatus(ActiveStatus.Instance);
            user.SetPasswordToken("passwordToken");
            _factory.CreateUser(user);

            _query = new CheckPasswordTokenQuery {PasswordToken = user.PasswordToken};

            _response = await _client.PostAsync(BaseUrl, Utilities.GetRequestContent(_query));
        }


        [Then(@"The request should be successful")]
        public void ThenTheRequestShouldBeSuccessful()
        {
            _response.EnsureSuccessStatusCode();
        }
    }
}