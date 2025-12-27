using System.IdentityModel.Tokens.Jwt;
using Academy.Api.DTOs;
using Academy.Api.Models;
using Academy.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Academy.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IJwtTokenService _jwtTokenService;

    public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IJwtTokenService jwtTokenService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtTokenService = jwtTokenService;
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<ActionResult<ApiResponse<AuthResponse>>> Register(RegisterRequest request)
    {
        var exists = await _userManager.FindByEmailAsync(request.Email);
        if (exists is not null)
        {
            return BadRequest(ApiResponse<AuthResponse>.Fail("ایمیل تکراری است."));
        }

        var user = new AppUser
        {
            UserName = request.Email,
            Email = request.Email,
            FullName = request.FullName,
            Role = "Student",
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            var errors = result.Errors.GroupBy(e => "password").ToDictionary(g => g.Key, g => g.Select(x => x.Description).ToArray());
            return BadRequest(ApiResponse<AuthResponse>.Fail("خطا در ثبت‌نام", errors));
        }

        await _userManager.AddToRoleAsync(user, "Student");
        var roles = await _userManager.GetRolesAsync(user);
        var token = _jwtTokenService.GenerateToken(user, roles);
        return Ok(ApiResponse<AuthResponse>.Success(new AuthResponse
        {
            Token = token.Token,
            ExpiresAt = token.ExpiresAt,
            Role = roles.First(),
            FullName = user.FullName
        }, "ثبت‌نام با موفقیت انجام شد"));
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<ApiResponse<AuthResponse>>> Login(LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null)
        {
            return Unauthorized(ApiResponse<AuthResponse>.Fail("کاربر یافت نشد."));
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
        if (!result.Succeeded)
        {
            return Unauthorized(ApiResponse<AuthResponse>.Fail("نام کاربری یا رمز عبور نادرست است."));
        }

        var roles = await _userManager.GetRolesAsync(user);
        var token = _jwtTokenService.GenerateToken(user, roles);

        return Ok(ApiResponse<AuthResponse>.Success(new AuthResponse
        {
            Token = token.Token,
            ExpiresAt = token.ExpiresAt,
            Role = roles.First(),
            FullName = user.FullName
        }, "ورود موفقیت‌آمیز بود"));
    }

    [HttpGet("me")]
    [Authorize]
    public async Task<ActionResult<ApiResponse<object>>> Profile()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user is null)
        {
            return NotFound(ApiResponse<object>.Fail("کاربر یافت نشد."));
        }

        var roles = await _userManager.GetRolesAsync(user);
        return Ok(ApiResponse<object>.Success(new
        {
            user.FullName,
            user.Email,
            Role = roles.FirstOrDefault() ?? "Student"
        }));
    }
}
