using NUnit.Framework;
using Solen.Core.Domain.Resources.Entities;
using Solen.Core.Domain.Resources.Enums.ResourceTypes;

namespace Domain.UnitTests.Resources.Entities
{
    [TestFixture]
    public class AppResourceTests
    {
        private AppResource _sut;

        [Test]
        public void ConstructorWithIdOrganizationCreatorResourceTypeSize_WhenCalled_SetRequiredPropertiesCorrectly()
        {
            var imageType = new ImageType();
            
            _sut = new AppResource("id", "organizationId", "creator", imageType, 10);

            Assert.That(_sut.Id, Is.EqualTo("id"));
            Assert.That(_sut.OrganizationId, Is.EqualTo("organizationId"));
            Assert.That(_sut.CreatorName, Is.EqualTo("creator"));
            Assert.That(_sut.ResourceTypeName, Is.EqualTo(imageType.Name));
            Assert.That(_sut.Size, Is.EqualTo(10));
            Assert.That(_sut.CreationDate, Is.Not.Null);
        }
        
        [Test]
        public void MarkToDelete_WhenCalled_MarkResourceToBeDeleted()
        {
            _sut = new AppResource("id", "organizationId", "creator", new ImageType(), 10);

            _sut.MarkToDelete();
            
            Assert.That(_sut.ToDelete, Is.True);
        }
    }
}