using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Solen.Core.Application.Users.Queries;

namespace UsersManagement.UnitTests.Queries.GetUsersList
{
    [TestFixture]
    public class GetUsersListQueryHandlerTests
    {
        private Mock<IGetUsersListService> _service;
        private GetUsersListQuery _query;
        private GetUsersListQueryHandler _sut;


        [SetUp]
        public void SetUp()
        {
            _service = new Mock<IGetUsersListService>();
            _sut = new GetUsersListQueryHandler(_service.Object);

            _query = new GetUsersListQuery();
        }

        [Test]
        public void WhenCalled_ReturnUserInfo()
        {
            var users = new List<UsersListItemDto>();
            _service.Setup(x => x.GetUsersList(default)).ReturnsAsync(users);

            var result = _sut.Handle(_query, default).Result;
            
            Assert.That(result.Users, Is.EqualTo(users));
        }
    }
}