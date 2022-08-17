using Microsoft.IdentityModel.Tokens;

namespace Messenger.Core.Infrastructure;

public interface IJwtSigningDecodingKey
{
    SecurityKey GetKey();
}