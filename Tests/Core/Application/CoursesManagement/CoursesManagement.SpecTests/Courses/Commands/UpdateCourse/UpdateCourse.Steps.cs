using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;
using Solen.Core.Application.CoursesManagement.Edit.Courses.Commands;
using Solen.Core.Domain.Common.Entities;
using Solen.Core.Domain.Courses.Entities;
using Solen.Core.Domain.Courses.Enums.CourseStatuses;
using Solen.Core.Domain.Identity.Entities;
using Solen.Core.Domain.Identity.Enums;
using Solen.Core.Domain.Identity.Enums.UserStatuses;
using Solen.Core.Domain.Subscription.Constants;
using Solen.SpecTests;
using TechTalk.SpecFlow;

namespace CoursesManagement.SpecTests.Courses.Commands
{
    [Binding, Scope(Tag = "UpdateCourse")]
    public class UpdateCourseSteps
    {
        private const string BaseUrl = "/api/courses-management/courses";
        private UpdateCourseCommand _command;

        private CoursesManagementTestsFactory _factory;
        private HttpClient _client;
        private HttpResponseMessage _response;
        private string _instructorId;

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
            _factory.CreateOrganization(organization);

            var instructor = new User("instructor@email.com", organization.Id);
            instructor.ChangeUserStatus(ActiveStatus.Instance);
            instructor.AddRoleId(UserRoles.Instructor);
            _instructorId = instructor.Id;
            _factory.CreateUser(instructor);

            _client = _factory.GetAuthenticatedClient(instructor);
        }

        [When(@"I update a course with an empty or a null Id ""(.*)""")]
        public async Task WhenIUpdateACourseWithAnEmptyOrANullId(string courseId)
        {
            _command = new UpdateCourseCommand {CourseId = courseId, Title = "course title"};

            _response = await _client.PutAsync(BaseUrl, Utilities.GetRequestContent(_command));
        }

        [When(@"I update a course with an empty or a null title ""(.*)""")]
        public async Task WhenIUpdateACourseWithAnEmptyOrANullTitle(string title)
        {
            _command = new UpdateCourseCommand {CourseId = "courseId", Title = title};

            _response = await _client.PutAsync(BaseUrl, Utilities.GetRequestContent(_command));
        }

        [When(@"I update a course with a title length over (.*) characters")]
        public async Task WhenIUpdateACourseWithATitleLengthOverCharacters(int maximumTitleLength)
        {
            _command = new UpdateCourseCommand {CourseId = "courseId", Title = new string('*', maximumTitleLength + 1)};

            _response = await _client.PutAsync(BaseUrl, Utilities.GetRequestContent(_command));
        }

        [When(@"I update a course with a subtitle length over (.*) characters")]
        public async Task WhenIUpdateACourseWithASubtitleLengthOverCharacters(int maximumSubtitleLength)
        {
            _command = new UpdateCourseCommand
            {
                CourseId = "courseId",
                Title = "Course title",
                Subtitle = new string('*', maximumSubtitleLength + 1)
            };

            _response = await _client.PutAsync(BaseUrl, Utilities.GetRequestContent(_command));
        }

        [When(@"I update a course with a skill length over (.*) characters")]
        public async Task WhenIUpdateACourseWithASkillLengthOverCharacters(int maximumSkillLength)
        {
            _command = new UpdateCourseCommand
            {
                CourseId = "courseId",
                Title = "Course title",
                CourseLearnedSkills = new List<string> {new string('*', maximumSkillLength + 1)}
            };

            _response = await _client.PutAsync(BaseUrl, Utilities.GetRequestContent(_command));
        }

        [Then(@"I should get a bad request error")]
        public void ThenIShouldGetABadRequestError()
        {
            Assert.That(_response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [When(@"I update an invalid course")]
        public async Task WhenIUpdateAnInvalidCourse()
        {
            _command = new UpdateCourseCommand {CourseId = "invalidCourseId", Title = "course title"};

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

            _command = new UpdateCourseCommand {CourseId = course.Id, Title = "course title"};
            _response = await _client.PutAsync(BaseUrl, Utilities.GetRequestContent(_command));
        }

        [Then(@"I should get a not found error")]
        public void ThenIShouldGetANotFoundError()
        {
            Assert.That(_response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [When(@"I update a published course")]
        public async Task WhenIUpdateAPublishedCourse()
        {
            var course = new Course("course", _instructorId, DateTime.Now);
            course.ChangeCourseStatus(PublishedStatus.Instance);
            _factory.CreateCourse(course);

            _command = new UpdateCourseCommand {CourseId = course.Id, Title = "course title"};
            _response = await _client.PutAsync(BaseUrl, Utilities.GetRequestContent(_command));
        }

        [Given(@"A draft course with")]
        public void GivenADraftCourseWith(Table table)
        {
            var course = new Course("old title", _instructorId, DateTime.Now);
            course.UpdateSubtitle("old subtitle");
            course.UpdateDescription("old description");
            course.AddLearnedSkill("old skill 1");
            course.AddLearnedSkill("old skill 2");
            _factory.CreateCourse(course);

            _command = new UpdateCourseCommand {CourseId = course.Id};
        }

        [When(@"I update the course with")]
        public async Task WhenIUpdateTheCourseWith(Table table)
        {
            _command.Title = "new title";
            _command.Subtitle = "new subtitle";
            _command.Description = "new description";
            _command.CourseLearnedSkills = new List<string> {"new skill 1", "new skill 2"};
            
            _response = await _client.PutAsync(BaseUrl, Utilities.GetRequestContent(_command));
        }

        [Then(@"The course should be successfully updated with the new data")]
        public async Task ThenTheCourseShouldBeSuccessfullyUpdatedWithTheNewData()
        {
            _response.EnsureSuccessStatusCode();
            
            var course = await _factory.GetCourseById(_command.CourseId);

            Assert.That(course.Title, Is.EqualTo(_command.Title));
            Assert.That(course.Subtitle, Is.EqualTo(_command.Subtitle));
            Assert.That(course.Description, Is.EqualTo(_command.Description));

            var courseSkills = course.CourseLearnedSkills.Select(x => x.Name).ToList();
            Assert.That(courseSkills, Is.EquivalentTo(_command.CourseLearnedSkills));
        }
    }
}