using Messenger.Core.DTOs;

namespace Messenger.Core.IServices;

public interface IUserService
{
    Task<AuthenticateResponseModel> Authenticate(string email, string password);

    Task<AuthenticateResponseModel> Register(RegisterDto registerDto);

    Task ChangeAvatar(string avatar);

}