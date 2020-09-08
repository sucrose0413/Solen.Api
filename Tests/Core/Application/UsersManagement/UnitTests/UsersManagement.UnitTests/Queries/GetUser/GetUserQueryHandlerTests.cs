using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Solen.Core.Application.Users.Queries;


namespace UsersManagement.UnitTests.Queries.GetUser
{
    [TestFixture]
    public class GetUserQueryHandlerTests
    {
        private Mock<IGetUserService> _service;
        private GetUserQuery _query;
        private GetUserQueryHandler _sut;


        [SetUp]
        public void SetUp()
        {
            _service = new Mock<IGetUserService>();
            _sut = new GetUserQueryHandler(_service.Object);

            _query = new GetUserQuery("userId");
        }

        [Test]
        public void WhenCalled_ReturnUserInfo()
        {
            var user = new UserDto();
            _service.Setup(x => x.GetUser(_query.UserId, default)).ReturnsAsync(user);

            var result = _sut.Handle(_query, default).Result;
            
            Assert.That(result.User, Is.EqualTo(user));
        }
        
        [Test]
        public void WhenCalled_ReturnLearningPathsList()
        {
            var learningPaths = new List<LearningPathForUserDto>();
            _service.Setup(x => x.GetLearningPaths(default)).ReturnsAsync(learningPaths);

            var result = _sut.Handle(_query, default).Result;
            
            Assert.That(result.LearningPaths, Is.EqualTo(learningPaths));
        }
        
        [Test]
        public void WhenCalled_ReturnRolesList()
        {
            var roles = new List<RoleForUserDto>();
            _service.Setup(x => x.GetRoles(default)).ReturnsAsync(roles);

            var result = _sut.Handle(_query, default).Result;
            
            Assert.That(result.Roles, Is.EqualTo(roles));
        }
    }
}