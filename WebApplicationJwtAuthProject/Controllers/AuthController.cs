using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebApplicationJwtAuthProject.Db;
using WebApplicationJwtAuthProject.Dto;
using WebApplicationJwtAuthProject.Services;

namespace WebApplicationJwtAuthProject.Controllers;

[ApiController]
[Route("api/auth/")]
public class AuthController : ControllerBase
{
    private IConfiguration _configuration;
    private UsersService _usersService;

    public AuthController(IConfiguration configuration, UsersService usersService)
    {
        _configuration = configuration;
        _usersService = usersService;
    }

    [HttpPost("authenticate")]
    public IActionResult Post(AuthRequestDto authRequestDto)
    {
        User user = _usersService.GetUserByUsernameAndPassword(authRequestDto.Username, authRequestDto.Password);

        if (user != null)
        {
            Claim[] claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToFileTimeUtc().ToString()),
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            SigningCredentials signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            
            JwtSecurityToken token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddSeconds(60),
                signingCredentials: signIn);

            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }
        else
        {
            return BadRequest("Invalid credentials");
        }
    }
}