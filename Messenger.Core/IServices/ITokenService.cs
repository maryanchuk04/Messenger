using Messenger.Core.DTOs;
using Messenger.db.Entities;

namespace Messenger.Core.IServices;

public interface ITokenService
{
    string GenerateAccessToken(User user);

    UserToken GenerateRefreshToken();

    Task<AuthenticateResponseModel> RefreshToken(string token);

    Task<bool> RevokeToken(string token);
}