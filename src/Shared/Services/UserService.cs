using Domain.Helpers;

namespace Shared.Services;

public class UserService : IUserService
{
    public Guid GetUserId()
    {
        return new Guid();
    }
}
