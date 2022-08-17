using Messenger.Core.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Messenger.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TokenController : ControllerBase
{
    private readonly ITokenService _tokenService;

    public TokenController(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    [AllowAnonymous]
    [HttpPost("refresh-token")]
    public async Task<IActionResult> Refresh()
    {
        var refreshToken = Request.Cookies["refreshToken"];
        var response = await _tokenService.RefreshToken(refreshToken);
        if (response == null)
        {
            return Unauthorized();
        }

        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTime.Now.AddDays(7),
            Secure = true,
        };
        HttpContext.Response.Cookies.Delete("refreshToken");
        HttpContext.Response.Cookies.Append("refreshToken", response.RefreshToken, cookieOptions);
        return Ok(new { jwtToken = response.JwtToken });
    }

    [HttpPost("revoke-token")]
    public async Task<IActionResult> Revoke()
    {
        var token = Request.Cookies["refreshToken"];
        var response = await _tokenService.RevokeToken(token);
        if (!response)
        {
            return NotFound(new { message = "Token not found" });
        }

        return Ok(new { message = "Token revoked" });
    }
}