using MicroserviceDemo.Catalog.Api.Features.Courses.Create;
using MicroserviceDemo.Catalog.Api.Features.Courses.Dtos;
using MicroserviceDemo.Catalog.Api.Features.Courses.Update;

namespace MicroserviceDemo.Catalog.Api.Features.Courses
{
    public class CourseMapping : Profile
    {
        public CourseMapping()
        {
            CreateMap<CreateCourseCommand, Course>().ReverseMap();
            CreateMap<UpdateCourseCommand, Course>().ReverseMap();

            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<Feature,FeatureDto>().ReverseMap();
        }
    }
}
