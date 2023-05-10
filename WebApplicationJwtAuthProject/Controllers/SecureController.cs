using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplicationJwtAuthProject.Db;
using WebApplicationJwtAuthProject.Services;

namespace WebApplicationJwtAuthProject.Controllers;

[Authorize]
[ApiController]
[Route("api/secure/")]
public class SecureController : ControllerBase
{
    private UsersService _usersService;

    public SecureController(UsersService usersService)
    {
        _usersService = usersService;
    }

    [HttpGet("get-hello")]
    public String getHello()
    {
        return "secure hello";
    }

    [HttpGet("get-user")]
    public User getUser()
    {
        return _usersService.GetUserByUsername("admin");
    }
}