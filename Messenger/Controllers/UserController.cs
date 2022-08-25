using AutoMapper;
using Messenger.Core.DTOs;
using Messenger.Core.IServices;
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
        return Ok(new { Token = result.JwtToken });
    }


    [HttpPost("[action]")]
    public async Task<IActionResult> ChangeAvatar(AvatarViewModel avatarViewModel)
    {
        await _userService.ChangeAvatar(avatarViewModel.Avatar);
        return Ok();
    }

    [HttpGet("[action]")]
    public IActionResult GetCurrentUserInfo()
    {
        var res = _mapper.Map<UserDto, UserViewModel>(_userService.GetCurrentUser());
        return Ok(res);
    }


    [HttpGet("[action]")]
    public IActionResult GetAllUsers()
    {
        return Ok(_mapper.Map<List<UserPreviewViewModel>>(_userService.GetAllUsers()));
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> ChangeUserName(UserNameViewModel userNameViewModel)
    {
        await _userService.ChangeUserName(userNameViewModel.UserName);
        return Ok();
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> ChangeMail(ChangeMailViewModel changeMail)
    {
        await _userService.ChangeEmail( changeMail.NewEmail);
        return Ok();
    }

    [HttpPost("[action]")]
    public IActionResult SearchUsers(SearchViewModel searchViewModel)
    {
        return Ok(_userService.SearchUsers(searchViewModel.KeyWord));
    }



}