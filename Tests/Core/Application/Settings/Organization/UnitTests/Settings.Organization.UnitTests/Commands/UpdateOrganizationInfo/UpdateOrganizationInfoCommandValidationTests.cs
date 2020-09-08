using FluentValidation.TestHelper;
using NUnit.Framework;
using Solen.Core.Application.Settings.Organization.Commands;

namespace Settings.Organization.UnitTests.Commands.UpdateOrganizationInfo
{
    [TestFixture]
    public class UpdateOrganizationInfoCommandValidationTests
    {
        private UpdateOrganizationInfoCommandValidator _sut;
        private UpdateOrganizationInfoCommand _command;

        [SetUp]
        public void SetUp()
        {
            _sut = new UpdateOrganizationInfoCommandValidator();
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void NameIsNullOrEmpty_ShouldHaveError(string name)
        {
            _command = new UpdateOrganizationInfoCommand {OrganizationName = name};

            _sut.ShouldHaveValidationErrorFor(x => x.OrganizationName, _command);
        }

        [Test]
        public void NameLengthIsOver60_ShouldHaveError()
        {
            _command = new UpdateOrganizationInfoCommand {OrganizationName = new string('*', 61)};

            _sut.ShouldHaveValidationErrorFor(x => x.OrganizationName, _command);
        }

        [Test]
        public void NameIsValid_ShouldNotHaveError()
        {
            _command = new UpdateOrganizationInfoCommand {OrganizationName = "organization name"};

            _sut.ShouldNotHaveValidationErrorFor(x => x.OrganizationName, _command);
        }
    }
}