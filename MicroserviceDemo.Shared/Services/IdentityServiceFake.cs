namespace MicroserviceDemo.Shared.Services
{
    public class IdentityServiceFake : IIdentityService
    {
        public Guid UserId => Guid.Parse("69090000-6125-107c-c133-08ddee61768f");
        public string UserName => "Eray";
        public List<string> Roles => [];
    }
}
