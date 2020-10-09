using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using NUnit.Framework;
using Solen.Core.Application.Auth.Queries;
using Solen.Core.Domain.Common.Entities;
using Solen.Core.Domain.Identity.Entities;
using Solen.Core.Domain.Identity.Enums.UserStatuses;
using Solen.Core.Domain.Subscription.Constants;
using Solen.SpecTests;
using TechTalk.SpecFlow;
using RefreshTokenEntity = Solen.Core.Domain.Security.Entities.RefreshToken;

namespace Auth.SpecTests.Queries
{
    [Binding, Scope(Tag = "RefreshToken")]
    public class RefreshTokenSteps
    {
        private const string BaseUrl = "/api/auth/refreshToken";

        private AuthTestsFactory _factory;
        private HttpClient _client;
        private string _refreshToken;
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


        [When(@"The given refresh token is an unknown token")]
        public async Task WhenTheGivenRefreshTokenIsAnUnknownToken()
        {
            _refreshToken = "unknownRefreshToken";

            _response = await _client.GetAsync($@"{BaseUrl}/{_refreshToken}");
        }

        [Then(@"The system should return an unauthorized error")]
        public void ThenTheSystemShouldReturnAnUnauthorizedError()
        {
            Assert.That(_response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        }

        [When(@"The given refresh token is expired")]
        public async Task WhenTheGivenRefreshTokenIsExpired()
        {
            var organization = new Organization("Test organization", SubscriptionPlans.Free);
            _factory.CreateOrganization(organization);

            var user = new User("user@email.com", organization.Id);
            user.ChangeUserStatus(ActiveStatus.Instance);
            _factory.CreateUser(user);

            var refreshTokenEntity = new RefreshTokenEntity(user, DateTime.Now.AddDays(-1));
            _factory.AddRefreshToken(refreshTokenEntity);
            _refreshToken = refreshTokenEntity.Id;

            _response = await _client.GetAsync($@"{BaseUrl}/{_refreshToken}");
        }

        [When(@"The given refresh token bearer has been blocked")]
        public async Task WhenTheGivenRefreshTokenBearerHasBeenBlocked()
        {
            var organization = new Organization("Test organization", SubscriptionPlans.Free);
            _factory.CreateOrganization(organization);

            var user = new User("user@email.com", organization.Id);
            user.ChangeUserStatus(BlockedStatus.Instance);
            _factory.CreateUser(user);

            var refreshTokenEntity = new RefreshTokenEntity(user, null);
            _factory.AddRefreshToken(refreshTokenEntity);
            _refreshToken = refreshTokenEntity.Id;

            _response = await _client.GetAsync($@"{BaseUrl}/{_refreshToken}");
        }

        [Then(@"The system should return a locked error")]
        public void ThenTheSystemShouldReturnALockedError()
        {
            Assert.That(_response.StatusCode, Is.EqualTo(HttpStatusCode.Locked));
        }

        [When(@"The given refresh token is valid and the bearer is active")]
        public async Task WhenTheGivenRefreshTokenIsValidAndTheBearerIsActive()
        {
            var organization = new Organization("Test organization", SubscriptionPlans.Free);
            _factory.CreateOrganization(organization);

            var user = new User("user@email.com", organization.Id);
            user.ChangeUserStatus(ActiveStatus.Instance);
            _factory.CreateUser(user);

            var refreshTokenEntity = new RefreshTokenEntity(user, null);
            _factory.AddRefreshToken(refreshTokenEntity);
            _refreshToken = refreshTokenEntity.Id;

            _response = await _client.GetAsync($@"{BaseUrl}/{_refreshToken}");
        }

        [Then(@"The request should be successful")]
        public void ThenTheRequestShouldBeSuccessful()
        {
            _response.EnsureSuccessStatusCode();
        }

        [Then(@"A new refresh token and a valid authentication token should be returned")]
        public async Task ThenANewRefreshTokenAndAValidAuthenticationTokenShouldBeReturned()
        {
            var loggedUserVm = await Utilities.GetResponseContent<LoggedUserViewModel>(_response);

            Assert.That(loggedUserVm, Is.Not.Null);
            Assert.That(loggedUserVm.Token, Is.Not.Null);
            Assert.That(loggedUserVm.RefreshToken, Is.Not.Null);
            Assert.That(loggedUserVm.RefreshToken, Is.Not.EqualTo(_refreshToken));

            await CheckTokenValidity(loggedUserVm.Token);
        }

        private async Task CheckTokenValidity(string token)
        {
            _client = _factory.GetAnonymousClient();
            _client.SetBearerToken(token);

            _response = await _client.GetAsync("/api/auth/currentUser");
            _response.EnsureSuccessStatusCode();
        }
    }
}