using Asp.Versioning.Builder;
using MicroserviceDemo.Catalog.Api.Features.Courses.Create;
using MicroserviceDemo.Catalog.Api.Features.Courses.Delete;
using MicroserviceDemo.Catalog.Api.Features.Courses.GetAll;
using MicroserviceDemo.Catalog.Api.Features.Courses.GetAllByUserId;
using MicroserviceDemo.Catalog.Api.Features.Courses.GetById;
using MicroserviceDemo.Catalog.Api.Features.Courses.Update;

namespace MicroserviceDemo.Catalog.Api.Features.Courses
{
    public static class CourseEndpointExt
    {
        public static void AddCourseGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/courses").WithTags("Courses")
                .WithApiVersionSet(apiVersionSet)
                .CreateCourseGroupItem()
                .GetAllCoursesGroupItem()
                .GetCourseByIdGroupItem()
                .UpdateCourseEndpointGroupItem()
                .DeleteCourseEndpointGroupItem()
                .GetAllByUserIdCoursesGroupItem();
        }
    }
}
