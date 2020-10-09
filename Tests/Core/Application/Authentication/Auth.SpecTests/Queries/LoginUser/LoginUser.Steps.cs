using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using NUnit.Framework;
using Solen.Core.Application.Auth.Queries;
using Solen.Core.Domain.Common.Entities;
using Solen.Core.Domain.Identity.Entities;
using Solen.Core.Domain.Identity.Enums.UserStatuses;
using Solen.Core.Domain.Subscription.Constants;
using Solen.SpecTests;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Solen.Core.Domain.Courses.Entities;
using Solen.Core.Domain.Identity.Enums;
using TechTalk.SpecFlow;

namespace Auth.SpecTests.Queries
{
    [Binding, Scope(Tag = "LoginUser")]
    public class LoginUserSteps
    {
        private const string BaseUrl = "/api/auth/login";
        private User _user;
        private LoginUserQuery _query;

        private AuthTestsFactory _factory;
        private HttpClient _client;
        private LoggedUserViewModel _loggedUserVm;
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


        [When(@"I enter an empty or a null ""(.*)"" address")]
        public async Task WhenIEnterAnEmptyOrANullAddress(string email)
        {
            _query = new LoginUserQuery {Email = email, Password = "password"};

            _response = await _client.PostAsync(BaseUrl, Utilities.GetRequestContent(_query));
        }
        
        [When(@"I enter an invalid Email address like '(.*)'")]
        public async Task WhenIEnterAnInvalidEmailAddressLike(string invalidEmail)
        {
            _query = new LoginUserQuery {Email = invalidEmail, Password = "password"};

            _response = await _client.PostAsync(BaseUrl, Utilities.GetRequestContent(_query));
        }

        [When(@"I enter an empty or a null ""(.*)""")]
        public async Task WhenIEnterAnEmptyOrANull(string password)
        {
            _query = new LoginUserQuery {Email = "user@email.com", Password = password};

            _response = await _client.PostAsync(BaseUrl, Utilities.GetRequestContent(_query));
        }

        [Then(@"I should get bad request error")]
        public void ThenIShouldGetBadRequestError()
        {
            Assert.That(_response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [When(@"I enter a bad Email address or a bad password")]
        public async Task WhenIEnterABadEmailAddressOrABadPassword()
        {
            _query = new LoginUserQuery {Email = "bad@email.com", Password = "badPassword"};

            _response = await _client.PostAsync(BaseUrl, Utilities.GetRequestContent(_query));
        }

        [Then(@"I should get unauthorised error")]
        public void ThenIShouldGetUnauthorisedError()
        {
            Assert.That(_response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        }


        [Given(@"I'm a blocked user")]
        public void GivenImABlockedUser()
        {
            var organization = new Organization("Test organization", SubscriptionPlans.Free);
            _factory.CreateOrganization(organization);

            _user = new User("user@email.com", organization.Id);
            _user.ChangeUserStatus(BlockedStatus.Instance);
            _factory.CreateUser(_user, "password");
        }

        [When(@"I enter my valid credentials")]
        public async Task WhenIEnterMyValidCredentials()
        {
            _query = new LoginUserQuery {Email = "user@email.com", Password = "password"};

            _response = await _client.PostAsync(BaseUrl, Utilities.GetRequestContent(_query));
        }

        [Then(@"I should get locked error")]
        public void ThenIShouldGetLockedError()
        {
            Assert.That(_response.StatusCode, Is.EqualTo(HttpStatusCode.Locked));
        }

        [Given(@"I'm an active user with")]
        public void GivenImAnActiveUserWith(Table table)
        {
            var organization = new Organization("Test organization", SubscriptionPlans.Free);
            _factory.CreateOrganization(organization);

            var learningPath = new LearningPath("Developer", organization.Id);
            _factory.AddLearningPath(learningPath);

            _user = new User("user@email.com", organization.Id);
            _user.ChangeUserStatus(ActiveStatus.Instance);
            _user.UpdateUserName("Jean Dupont");
            _user.UpdateLearningPath(learningPath);
            _user.AddRoleId(UserRoles.Learner);
            _user.AddRoleId(UserRoles.Instructor);

            _factory.CreateUser(_user, "password");
        }


        [Then(@"I should get logged in successfully")]
        public async Task ThenIShouldGetLockedIn()
        {
            _response.EnsureSuccessStatusCode();

            _loggedUserVm = await Utilities.GetResponseContent<LoggedUserViewModel>(_response);
        }

        [Then(@"My info should be returned as")]
        public void ThenMyInfoShouldBeReturnedAs(Table table)
        {
            Assert.That(_loggedUserVm.LoggedUser, Is.Not.Null);
            Assert.That(_loggedUserVm.LoggedUser.Id, Is.EqualTo(_user.Id));
            Assert.That(_loggedUserVm.LoggedUser.UserName, Is.EqualTo(_user.UserName));
            Assert.That(_loggedUserVm.LoggedUser.LearningPath, Is.EqualTo(_user.LearningPath.Name));
        }


        [Then(@"The following claims should be set correctly in the authentication Token")]
        public void ThenTheFollowingClaimsShouldBeSetCorrectlyInTheAuthenticationToken(Table table)
        {
            var decodedToken = DecodeToken(_loggedUserVm.Token);

            Assert.That(decodedToken, Is.Not.Null);
            AssertIdentifier(decodedToken);
            AssertName(decodedToken);
            AssertEmail(decodedToken);
            AssertOrganization(decodedToken);
            AssertLearningPath(decodedToken);
            AssertRoles(decodedToken);
        }

        [Then(@"A refresh token should be set and returned as well")]
        public void ThenARefreshTokenShouldBeSetAndReturnedAsWell()
        {
            Assert.That(_loggedUserVm.RefreshToken, Is.Not.Null);
        }

        #region private Methods

        private void AssertIdentifier(JwtSecurityToken decodedToken)
        {
            Assert.That(decodedToken.Claims.First(claim => claim.Type == "nameid").Value,
                Is.EqualTo(_user.Id));
        }

        private void AssertName(JwtSecurityToken decodedToken)
        {
            Assert.That(decodedToken.Claims.First(claim => claim.Type == "unique_name").Value,
                Is.EqualTo(_user.UserName));
        }

        private void AssertEmail(JwtSecurityToken decodedToken)
        {
            Assert.That(decodedToken.Claims.First(claim => claim.Type == "email").Value,
                Is.EqualTo(_user.Email));
        }

        private void AssertOrganization(JwtSecurityToken decodedToken)
        {
            Assert.That(decodedToken.Claims.First(claim => claim.Type == "organizationId").Value,
                Is.EqualTo(_user.OrganizationId));
        }

        private void AssertLearningPath(JwtSecurityToken decodedToken)
        {
            Assert.That(decodedToken.Claims.First(claim => claim.Type == "learningPathId").Value,
                Is.EqualTo(_user.LearningPathId));
        }

        private void AssertRoles(JwtSecurityToken decodedToken)
        {
            var userRoles = _user.UserRoles.Select(x => x.RoleId).ToArray();
            var tokenRoles = decodedToken.Claims.Where(claim => claim.Type == "role").Select(x => x.Value).ToArray();
            Assert.That(tokenRoles,
                Is.EquivalentTo(userRoles));
        }

        private static JwtSecurityToken DecodeToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var decodedToken = handler.ReadToken(token) as JwtSecurityToken;
            return decodedToken;
        }

        #endregion
    }
}