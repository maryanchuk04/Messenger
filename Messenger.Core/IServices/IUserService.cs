using Messenger.Core.DTOs;
using Messenger.db.Entities;

namespace Messenger.Core.IServices;

public interface IUserService
{
    Task<AuthenticateResponseModel> Authenticate(string email, string password);

    Task<AuthenticateResponseModel> Register(RegisterDto registerDto);

    Task ChangeAvatar(string avatar);

    UserDto GetCurrentUser();

    List<UserDto> GetAllUsers();

    Task ChangeUserName(string userName);

    Task ChangeEmail(string email);

    List<UserDto> SearchUsers(string keyword);

}