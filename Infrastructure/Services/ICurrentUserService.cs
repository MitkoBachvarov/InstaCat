namespace Catstagram.Server.Infrastructure.Services
{
    public interface ICurrentUserService
    {
        string GetId();

        string GetUserName();
    }
}
