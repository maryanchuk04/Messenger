using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using AutoMapper;
using Messenger.Core.DTOs;
using Messenger.Core.Infrastructure;
using Messenger.Core.IServices;
using Messenger.db.EF;
using Messenger.db.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Messenger.Core.Services;

public class TokenService : BaseService<UserToken>, ITokenService
{
    private readonly IJwtSigningEncodingKey _signingEncodingKey;
    private readonly IOptions<JwtOptions> _jwtOptions;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;

    public TokenService(
        MessengerContext context,
        IJwtSigningEncodingKey signingEncodingKey,
        IOptions<JwtOptions> jwtOptions,
        IHttpContextAccessor? httpContextAccessor,
        IMapper mapper
    ) : base(context)
    {
        _mapper = mapper;
        _jwtOptions = jwtOptions;
        _httpContextAccessor = httpContextAccessor;
        _signingEncodingKey = signingEncodingKey;
    }

    public string GenerateAccessToken(User user)
    {
        var lifeTime = _jwtOptions.Value.LifeTime;
        var claims = GetClaims(user);
        var jwtToken = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddSeconds(lifeTime),
            signingCredentials: new SigningCredentials(
                _signingEncodingKey.GetKey(),
                _signingEncodingKey.SigningAlgorithm));

        return new JwtSecurityTokenHandler().WriteToken(jwtToken);
    }

    private IEnumerable<Claim> GetClaims(User user)
    {
        List<Claim> claims = new List<Claim>();
        if (user.Id != null)
        {
            claims.Add(new Claim(ClaimTypes.Name, $"{user.Id}"));
        }

        return claims.ToArray();
    }
    public UserToken GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);

        return new UserToken
        {
            Token = Convert.ToBase64String(randomNumber),
            Expires = DateTime.Now.AddDays(7),
            Created = DateTime.Now,
        };
    }

    public async Task<AuthenticateResponseModel> RefreshToken(string token)
    {
        var account = Context.Users
            .Include(a => a.RefreshTokens)
            .SingleOrDefault(a => a.RefreshTokens.Any(t => t.Token.Equals(token)));

        // return null if no user found with token
        if (account == null)
        {
            return null;
        }

        var refreshToken = account.RefreshTokens.Single(x => x.Token == token);

        // return null if token is no longer active
        if (refreshToken.Expires < DateTime.Now && refreshToken.Revoked == null)
        {
            return null;
        }

        // replace old refresh token with a new one and save
        var newRefreshToken = GenerateRefreshToken();
        refreshToken.Revoked = DateTime.Now;
        refreshToken.ReplacedByToken = newRefreshToken.Token;
        account.RefreshTokens.Add(newRefreshToken);

        await Context.SaveChangesAsync();

        // generate new jwt
        var jwtToken = GenerateAccessToken(account);
        return new AuthenticateResponseModel(jwtToken, newRefreshToken.Token);
    }


    public async Task<bool> RevokeToken(string token)
    {
        var refreshToken = Context.UserTokens.SingleOrDefault(rt => rt.Token == token);

        // return false if token is not active
        if (refreshToken == null || !_mapper.Map<RefreshTokenDto>(refreshToken).IsActive)
        {
            return false;
        }

        // revoke token and save
        refreshToken.Revoked = DateTime.Now;
        await Context.SaveChangesAsync();
        _httpContextAccessor.HttpContext?.Response.Cookies.Delete("refreshToken");

        return true;
    }
}