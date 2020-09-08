﻿using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Core.Application.CoursesManagement.Edit.Services.Lectures
{
    public interface ILecturesCommonRepository
    {
        Task<Lecture> GetLectureWithCourse(string lectureId, string organizationId, CancellationToken token);
    }
}