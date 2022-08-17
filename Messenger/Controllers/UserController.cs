using AutoMapper;
using Messenger.Core.DTOs;
using Messenger.Core.IServices;
using Messenger.Core.Services;
using Messenger.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Messenger.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;


    public UserController(IUserService userService, ITokenService tokenService, IMapper mapper)
    {
        _userService = userService;
        _tokenService = tokenService;
        _mapper = mapper;
    }

    [AllowAnonymous]
    [HttpPost("[action]")]
    [Produces("application/json")]
    public async Task<IActionResult> Login(LoginViewModel loginViewModel)
    {
        var authResponseModel = await _userService.Authenticate(loginViewModel.Email, loginViewModel.Password);

        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTime.Now.AddDays(7),
            Secure = true,
        };

        HttpContext.Response.Cookies.Delete("refreshToken");
        HttpContext.Response.Cookies.Append("refreshToken", authResponseModel.RefreshToken, cookieOptions);

        return Ok(new { Token = authResponseModel.JwtToken });
    }


    [AllowAnonymous]
    [HttpPost("[action]")]
    public async Task<IActionResult> Registration([FromBody] RegisterDto registerDto)
    {
        var result = await _userService.Register(registerDto);
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTime.Now.AddDays(7),
            Secure = true,
        };

        HttpContext.Response.Cookies.Delete("refreshToken");
        HttpContext.Response.Cookies.Append("refreshToken", result.RefreshToken, cookieOptions);
        return Ok();
    }


    [HttpPut("[action]")]
    public async Task<IActionResult> ChangeAvatar(AvatarViewModel avatarViewModel)
    {
        await _userService.ChangeAvatar(avatarViewModel.Avatar);
        return Ok();
    }




}