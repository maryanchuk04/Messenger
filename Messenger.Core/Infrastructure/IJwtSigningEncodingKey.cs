using Microsoft.IdentityModel.Tokens;

namespace Messenger.Core.Infrastructure;

public interface IJwtSigningEncodingKey
{
    string SigningAlgorithm { get; }

    SecurityKey GetKey();
}