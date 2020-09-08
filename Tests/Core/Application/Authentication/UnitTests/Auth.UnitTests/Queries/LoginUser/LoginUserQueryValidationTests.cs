using FluentValidation.TestHelper;
using NUnit.Framework;
using Solen.Core.Application.Auth.Queries;

namespace Auth.UnitTests.Queries.LoginUser
{
    [TestFixture]
    public class LoginUserQueryValidationTests
    {
        private LoginUserQueryValidator _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new LoginUserQueryValidator();
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void EmptyOrNullEmail_ShouldHaveError(string email)
        {
            var query = new LoginUserQuery {Email = email};

            _sut.ShouldHaveValidationErrorFor(x => x.Email, query);
        }

        [Test]
        public void InvalidEmail_ShouldHaveError()
        {
            var query = new LoginUserQuery {Email = "invalidEmail"};

            _sut.ShouldHaveValidationErrorFor(x => x.Email, query);
        }
        
        [Test]
        public void EmptyPassword_ShouldHaveError()
        {
            var query = new LoginUserQuery {Password = ""};

            _sut.ShouldHaveValidationErrorFor(x => x.Password, query);
        }
        
        [Test]
        public void NullPassword_ShouldHaveError()
        {
            var query = new LoginUserQuery {Password = null};

            _sut.ShouldHaveValidationErrorFor(x => x.Password, query);
        }
        
        [Test]
        public void ValidLoginAndPassword_ShouldNotHaveErrors()
        {
            var query = new LoginUserQuery {Email = "email@example.com", Password = "password"};

            _sut.ShouldNotHaveValidationErrorFor(x => x.Email, query);
            _sut.ShouldNotHaveValidationErrorFor(x => x.Password, query);
        }
    }
}