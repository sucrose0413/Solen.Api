using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;
using Solen.Core.Application.Auth.Commands;
using Solen.Core.Application.Auth.Queries;
using Solen.Core.Domain.Common.Entities;
using Solen.Core.Domain.Identity.Entities;
using Solen.Core.Domain.Identity.Enums.UserStatuses;
using Solen.Core.Domain.Subscription.Constants;
using Solen.SpecTests;
using TechTalk.SpecFlow;

namespace Auth.SpecTests.Commands
{
    [Binding, Scope(Tag = "ResetPassword")]
    public class ResetPasswordSteps
    {
        private const string BaseUrl = "/api/auth/resetPassword";
        private ResetPasswordCommand _command;

        private AuthTestsFactory _factory;
        private HttpClient _client;
        private User _user;
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
        

        [When(@"I submit a reset password request with an empty or a null password reset token ""(.*)""")]
        public async Task WhenISubmitAResetPasswordRequestWithAnEmptyOrANullPasswordResetToken(string passwordToken)
        {
            _command = new ResetPasswordCommand
            {
                PasswordToken = passwordToken,
                NewPassword = "new password",
                ConfirmNewPassword = "new password"
            };

            _response = await _client.PostAsync(BaseUrl, Utilities.GetRequestContent(_command));
        }
        
        [When(@"I submit a reset password request with an empty or a null new password ""(.*)""")]
        public async Task WhenISubmitAResetPasswordRequestWithAnEmptyOrANullNewPassword(string newPassword)
        {
            _command = new ResetPasswordCommand
            {
                PasswordToken = "Password token",
                NewPassword = newPassword,
                ConfirmNewPassword = "new password"
            };

            _response = await _client.PostAsync(BaseUrl, Utilities.GetRequestContent(_command));
        }
        
        [When(@"I submit a reset password request with an empty or a null confirm new password ""(.*)""")]
        public async Task WhenISubmitAResetPasswordRequestWithAnEmptyOrANullConfirmNewPassword(string confirmNewPassword)
        {
            _command = new ResetPasswordCommand
            {
                PasswordToken = "Password token",
                NewPassword = "New password",
                ConfirmNewPassword = confirmNewPassword
            };

            _response = await _client.PostAsync(BaseUrl, Utilities.GetRequestContent(_command));
        }
        
        [When(@"I submit a reset password request with a confirm password that does not match the password")]
        public async Task WhenISubmitAResetPasswordRequestWithAConfirmPasswordThatDoesNotMatchThePassword()
        {
            _command = new ResetPasswordCommand
            {
                PasswordToken = "Password token",
                NewPassword = "New password",
                ConfirmNewPassword = "other password"
            };

            _response = await _client.PostAsync(BaseUrl, Utilities.GetRequestContent(_command));
        }

        [Then(@"I should get a bad request error")]
        public void ThenIShouldGetABadRequestError()
        {
            Assert.That(_response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [When(@"I submit a reset password request with an invalid password reset token")]
        public async Task WhenISubmitAResetPasswordRequestWithAnInvalidPasswordResetToken()
        {
            _command = new ResetPasswordCommand
            {
                PasswordToken = "Invalid Password token",
                NewPassword = "New password",
                ConfirmNewPassword = "New password"
            };

            _response = await _client.PostAsync(BaseUrl, Utilities.GetRequestContent(_command));
        }

        [Then(@"I should get a not found error")]
        public void ThenIShouldGetANotFoundError()
        {
            Assert.That(_response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [When(@"I submit a reset password request with valid properties")]
        public async Task WhenISubmitAResetPasswordRequestWithValidProperties()
        {
            _command = new ResetPasswordCommand
            {
                PasswordToken = _user.PasswordToken,
                NewPassword = "New password",
                ConfirmNewPassword = "New password"
            };

            _response = await _client.PostAsync(BaseUrl, Utilities.GetRequestContent(_command));
        }
        
        [Given(@"I am a user of the system and I already got the password reset token '(.*)'")]
        public void GivenIAmAUserOfTheSystemAndIAlreadyGotThePasswordResetToken(string passwordResetToken)
        {
            var organization = new Organization("Test organization", SubscriptionPlans.Free);
            _factory.CreateOrganization(organization);
            
            _user = new User("user@email.com", organization.Id);
            _user.ChangeUserStatus(ActiveStatus.Instance);
            _user.SetPasswordToken(passwordResetToken);
            _factory.CreateUser(_user, "old password");
        }

        [Then(@"The request should be successful")]
        public void ThenTheRequestShouldBeSuccessful()
        {
            _response.EnsureSuccessStatusCode();
        }
        
        [Then(@"The reset password token should be initialized")]
        public async Task ThenTheResetPasswordTokenShouldBeInitialized()
        {
            var user = await _factory.GetUserById(_user.Id);
            
            Assert.That(user.PasswordToken, Is.Null);
        }

        [Then(@"Login with the new password should be successful")]
        public async Task ThenLoginWithTheNewPasswordShouldBeSuccessful()
        {
            var query = new LoginUserQuery {Email = _user.Email, Password = _command.NewPassword};

            _response = await _client.PostAsync("/api/auth/login", Utilities.GetRequestContent(query));

            _response.EnsureSuccessStatusCode();
        }
    }
}