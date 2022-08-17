using AutoMapper;
using Messenger.Core.DTOs;
using Messenger.Core.IServices;
using Messenger.db.Bridge;
using Messenger.db.EF;
using Messenger.db.Entities;
using Microsoft.EntityFrameworkCore;

namespace Messenger.Core.Services;

public class UserService : BaseService<User>, IUserService
{
    private readonly ITokenService _tokenService;
    private readonly ISecurityContext _securityContext;

    public UserService(MessengerContext context,
        ITokenService tokenService,
        ISecurityContext securityContext,
        IMapper mapper
    ) : base(context, mapper)
    {
        _tokenService = tokenService;
        _securityContext = securityContext;
    }

    public async Task<AuthenticateResponseModel> Authenticate(string email, string password)
    {
        var user = Context.Users
            .Include(x => x.RefreshTokens)
            .FirstOrDefault(x => x.Email == email);

        if (user == null)
        {
            throw new Exception("Incorrect login or password");
        }
        if (!VerifyPassword(password, user.Password))
        {
            throw new Exception("Incorrect login or password");
        }

        var jwt = _tokenService.GenerateAccessToken(user);
        var refreshToken = _tokenService.GenerateRefreshToken();

        user.RefreshTokens.Add(refreshToken);
        await Context.SaveChangesAsync();
        return new AuthenticateResponseModel(jwt, refreshToken.Token);
    }

    public async Task<AuthenticateResponseModel> Register(RegisterDto registerDto)
    {
        if (Context.Users.Any(x => x.Email == registerDto.Email))
        {
            throw new Exception("User is already exist");
        }
        var user = new User()
        {
            Email = registerDto.Email,
            UserName = registerDto.Email.Substring(0,registerDto.Email.IndexOf('@')),
            Password = BCrypt.Net.BCrypt.HashPassword(registerDto.Password)
        };
        var jwtToken = _tokenService.GenerateAccessToken(user);
        var refreshToken = _tokenService.GenerateRefreshToken();
        var result = Insert(user);
        result.RefreshTokens = new List<UserToken>();
        result.RefreshTokens.Add(refreshToken);

        await Context.SaveChangesAsync();
        return new AuthenticateResponseModel(jwtToken, refreshToken.Token);
    }

    public async Task ChangeAvatar(string avatar)
    {
        var user = CurrentUser();
        user.Avatar = avatar;
        Update(user);

        await Context.SaveChangesAsync();
    }

    private bool VerifyPassword(string passwordFromRequest, string password) => BCrypt.Net.BCrypt.Verify(passwordFromRequest,password);

    private User CurrentUser()
    {
        var userId = _securityContext.GetCurrentUserId();

        var user = Context.Users.FirstOrDefault(x => x.Id == userId);

        return user;
    }
}