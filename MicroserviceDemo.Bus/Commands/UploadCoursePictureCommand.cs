namespace MicroserviceDemo.Bus.Commands
{
    public record UploadCoursePictureCommand(Guid courseId, Byte[] picture, string FileName);

}
