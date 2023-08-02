using IdentityUserManagment.Shared.DTOs;
using IdentityUserManagment.Shared.Helpers;

namespace IdentityUserManagment.Application.Services;
public class UserService : IUserService
{
    public Task<ResponseModel<GetUserDto>> GetAllUsers()
    {
        throw new NotImplementedException();
    }
}
