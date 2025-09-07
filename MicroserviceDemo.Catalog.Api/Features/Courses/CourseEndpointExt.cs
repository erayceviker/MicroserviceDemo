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
        public static void AddCourseGroupEndpointExt(this WebApplication app)
        {
            app.MapGroup("api/courses").WithTags("Courses")
                .CreateCourseGroupItem()
                .GetAllCoursesGroupItem()
                .GetCourseByIdGroupItem()
                .UpdateCourseEndpointGroupItem()
                .DeleteCourseEndpointGroupItem()
                .GetAllByUserIdCoursesGroupItem();
        }
    }
}
