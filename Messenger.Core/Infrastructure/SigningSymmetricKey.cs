using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Messenger.Core.Infrastructure;

public class SigningSymmetricKey : IJwtSigningEncodingKey, IJwtSigningDecodingKey
{
    private readonly SymmetricSecurityKey _secretKey;

    public SigningSymmetricKey(string key)
    {
        this._secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
    }

    public string SigningAlgorithm { get; } = SecurityAlgorithms.HmacSha256;

    public SecurityKey GetKey() => this._secretKey;
}