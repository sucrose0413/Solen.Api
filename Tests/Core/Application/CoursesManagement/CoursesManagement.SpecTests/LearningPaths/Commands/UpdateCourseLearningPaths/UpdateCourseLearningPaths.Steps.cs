using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;
using Solen.Core.Application.CoursesManagement.Edit.LearningPaths.Commands;
using Solen.Core.Domain.Common.Entities;
using Solen.Core.Domain.Courses.Entities;
using Solen.Core.Domain.Courses.Enums.CourseStatuses;
using Solen.Core.Domain.Identity.Entities;
using Solen.Core.Domain.Identity.Enums;
using Solen.Core.Domain.Identity.Enums.UserStatuses;
using Solen.Core.Domain.Subscription.Constants;
using Solen.SpecTests;
using TechTalk.SpecFlow;

namespace CoursesManagement.SpecTests.LearningPaths.Commands
{
    [Binding, Scope(Tag = "UpdateCourseLearningPaths")]
    public class UpdateCourseLearningPathsSteps
    {
        private const string BaseUrl = "/api/courses-management/learning-paths";
        private UpdateCourseLearningPathsCommand _command;

        private CoursesManagementTestsFactory _factory;
        private HttpClient _client;
        private HttpResponseMessage _response;
        private string _instructorId;
        private string[] _learningPathsIds;
        private string _organizationId;

        [BeforeScenario]
        public void ScenarioSetUp()
        {
            _factory = new CoursesManagementTestsFactory();
            _client = _factory.GetAnonymousClient();
        }

        [AfterScenario]
        public void ScenarioTearDown()
        {
            _factory.Dispose();
            _client.Dispose();
        }

        [Given(@"I'm an authenticated instructor within the organization")]
        public void GivenImAnAuthenticatedInstructorWithinTheOrganization()
        {
            var organization = new Organization("Test organization", SubscriptionPlans.Free);
            _organizationId = organization.Id;
            _factory.CreateOrganization(organization);

            var instructor = new User("instructor@email.com", organization.Id);
            instructor.ChangeUserStatus(new ActiveStatus());
            instructor.AddRoleId(UserRoles.Instructor);
            _instructorId = instructor.Id;
            _factory.CreateUser(instructor);

            _client = _factory.GetAuthenticatedClient(instructor);
        }

        [When(@"I update a course learning paths with an empty or a null Id ""(.*)""")]
        public async Task WhenIUpdateACourseLearningPathsWithAnEmptyOrANullId(string courseId)
        {
            _command = new UpdateCourseLearningPathsCommand {CourseId = courseId, LearningPathsIds = new string[1]};

            _response = await _client.PutAsync(BaseUrl, Utilities.GetRequestContent(_command));
        }

        [When(@"I update a course learning paths without specifying learning paths Ids")]
        public async Task WhenIUpdateACourseLearningPathsWithoutSpecifyingLearningPathsIds()
        {
            _command = new UpdateCourseLearningPathsCommand
                {CourseId = "courseId", LearningPathsIds = new List<string>()};

            _response = await _client.PutAsync(BaseUrl, Utilities.GetRequestContent(_command));
        }

        [Then(@"I should get a bad request error")]
        public void ThenIShouldGetABadRequestError()
        {
            Assert.That(_response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [When(@"I update a course learning paths with an invalid course Id")]
        public async Task WhenIUpdateACourseLearningPathsWithAnInvalidCourseId()
        {
            _command = new UpdateCourseLearningPathsCommand
                {CourseId = "invalidCourseId", LearningPathsIds = new string[1]};

            _response = await _client.PutAsync(BaseUrl, Utilities.GetRequestContent(_command));
        }

        [When(@"I update a course belonging to an other organization")]
        public async Task WhenIUpdateACourseBelongingToAnOtherOrganization()
        {
            var otherOrganization = new Organization("other organization", SubscriptionPlans.Free);
            _factory.CreateOrganization(otherOrganization);

            var otherInstructor = new User("otherinstructor@email.com", otherOrganization.Id);
            _factory.CreateUser(otherInstructor);

            var course = new Course("course", otherInstructor.Id, DateTime.Now);
            _factory.CreateCourse(course);

            _command = new UpdateCourseLearningPathsCommand {CourseId = course.Id, LearningPathsIds = new string[1]};
            _response = await _client.PutAsync(BaseUrl, Utilities.GetRequestContent(_command));
        }

        [When(@"I update a course learning paths with an invalid learning paths Ids")]
        public async Task WhenIUpdateACourseLearningPathsWithAnInvalidLearningPathsIds()
        {
            var course = new Course("course", _instructorId, DateTime.Now);
            _factory.CreateCourse(course);

            _command = new UpdateCourseLearningPathsCommand
                {CourseId = course.Id, LearningPathsIds = new[] {"invalidPathId1"}};
            _response = await _client.PutAsync(BaseUrl, Utilities.GetRequestContent(_command));
        }

        [Then(@"I should get a not found error")]
        public void ThenIShouldGetANotFoundError()
        {
            Assert.That(_response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [When(@"I update the learning paths of a published course")]
        public async Task WhenIUpdateTheLearningPathsOfAPublishedCourse()
        {
            var course = new Course("course", _instructorId, DateTime.Now);
            course.ChangeCourseStatus(PublishedStatus.Instance);
            _factory.CreateCourse(course);

            var learningPath = new LearningPath("developer", _organizationId);
            _factory.AddLearningPath(learningPath);

            _command = new UpdateCourseLearningPathsCommand
                {CourseId = course.Id, LearningPathsIds = new[] {learningPath.Id}};
            _response = await _client.PutAsync(BaseUrl, Utilities.GetRequestContent(_command));
        }

        [Given(@"A draft course that has an empty learning paths list")]
        public void GivenADraftCourseThatHasAnEmptyLearningPathsList(Table table)
        {
            var course = new Course("course", _instructorId, DateTime.Now);
            _factory.CreateCourse(course);

            _command = new UpdateCourseLearningPathsCommand {CourseId = course.Id};
        }

        [Given(@"The following learning paths")]
        public void GivenTheFollowingLearningPaths(Table table)
        {
            var developer = new LearningPath("developer", _organizationId);
            _factory.AddLearningPath(developer);

            var businessAnalyst = new LearningPath("Business Analyst", _organizationId);
            _factory.AddLearningPath(businessAnalyst);

            _learningPathsIds = new[] {developer.Id, businessAnalyst.Id};
        }

        [When(@"I update the course learning paths list as")]
        public async Task WhenIUpdateTheCourseLearningPathsListAs(Table table)
        {
            _command.LearningPathsIds = _learningPathsIds;

            _response = await _client.PutAsync(BaseUrl, Utilities.GetRequestContent(_command));
        }

        [Then(@"The learning paths should be successfully be added to the course learning paths list")]
        public async Task ThenTheLearningPathsShouldBeSuccessfullyBeAddedToTheCourseLearningPathsList(Table table)
        {
            _response.EnsureSuccessStatusCode();

            var course = await _factory.GetCourseById(_command.CourseId);
            var courseLearningPathsIds = course.CourseLearningPaths.Select(x => x.LearningPathId).ToArray();

            Assert.That(_learningPathsIds, Is.EquivalentTo(courseLearningPathsIds));
        }

        [Given(@"A draft course that has a non-empty learning paths list")]
        public void GivenADraftCourseThatHasANon_EmptyLearningPathsList(Table table)
        {
            var developer = new LearningPath("developer", _organizationId);
            _factory.AddLearningPath(developer);

            var businessAnalyst = new LearningPath("Business Analyst", _organizationId);
            _factory.AddLearningPath(businessAnalyst);

            var course = new Course("course", _instructorId, DateTime.Now);
            course.AddLearningPath(new LearningPathCourse(developer.Id, course.Id, 1));
            course.AddLearningPath(new LearningPathCourse(businessAnalyst.Id, course.Id, 2));
            _factory.CreateCourse(course);

            _command = new UpdateCourseLearningPathsCommand {CourseId = course.Id};
            _learningPathsIds = new[] {developer.Id};
        }

        [Then(@"The '(.*)' learning path should successfully be removed from the course learning paths list")]
        public async Task ThenTheLearningPathShouldSuccessfullyBeRemovedFromTheCourseLearningPathsList(string p0,
            Table table)
        {
            _response.EnsureSuccessStatusCode();

            var course = await _factory.GetCourseById(_command.CourseId);
            var courseLearningPathsIds = course.CourseLearningPaths.Select(x => x.LearningPathId).ToArray();

            Assert.That(courseLearningPathsIds.Length, Is.EqualTo(1));
            Assert.That(courseLearningPathsIds[0], Is.EqualTo(_learningPathsIds[0]));
        }
    }
}