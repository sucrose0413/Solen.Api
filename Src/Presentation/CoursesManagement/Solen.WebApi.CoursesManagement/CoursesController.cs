using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Solen.Core.Application.CoursesManagement.Courses.Queries;
using Solen.WebApi.Controllers;
using Swashbuckle.AspNetCore.Annotations;

namespace Solen.WebApi.CoursesManagement
{
    [Authorize(Policy = AuthorizationPolicies.CoursesManagementPolicy)]
    [Route("api/courses-management/courses")]
    [SwaggerTag("(Courses Management)")]
    public class CoursesController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<CoursesListViewModel>> GetAll([FromQuery] GetCoursesListQuery query,
            CancellationToken token)
        {
            return Ok(await Mediator.Send(query, token));
        }

        [HttpGet("{courseId}")]
        public async Task<ActionResult<CourseViewModel>> GetCourse(string courseId, CancellationToken token)
        {
            return Ok(await Mediator.Send(new GetCourseQuery
                {CourseId = courseId}, token));
        }

        [HttpGet("{courseId}/detail")]
        public async Task<ActionResult<CourseContentViewModel>> GetCourseDetail(string courseId,
            CancellationToken token)
        {
            return Ok(await Mediator.Send(new GetCourseContentQuery
                {CourseId = courseId}, token));
        }

        [HttpGet("filters")]
        public async Task<ActionResult<CoursesFiltersViewModel>> GetCoursesFilters(CancellationToken token)
        {
            return Ok(await Mediator.Send(new GetCoursesFiltersQuery(), token));
        }
    }
}