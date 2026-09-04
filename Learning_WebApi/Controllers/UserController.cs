using Learning_WebApi.Data;
using Learning_WebApi.Data.Entity;
using Learning_WebApi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// ReSharper disable All

namespace Learning_WebApi.Controllers;

[ApiController]
[Route("api/auth")]
public class UserController : ControllerBase
{
    private readonly AppDbContext _context;

    public UserController(AppDbContext context)
    {
        _context = context;
    }
    
    [HttpPost("register")]
    public IActionResult Registration(UserDto userDto)
    {
        List<string> Errors = UserValidator.ValidateUsersRegistration(userDto).ToList();
        if (Errors.Any())
        {
            return BadRequest(Errors);
        }

        var User = new User {
            FirstName = userDto.FirstName,
            LastName = userDto.LastName,
            Email = userDto.Email,
            Password = userDto.Password };

        try
        {
            _context.Users.Add(User);
            _context.SaveChanges();
        }
        catch (DbUpdateException ex)
        {
            return BadRequest("This email adress is already registered");
        }
        return Ok($"You were successfully registrated, your id:{_context.Users.FirstOrDefault(u => u.Email == userDto.Email && u.Password == userDto.Password).UserId}");
    }

    [HttpPost("login")]
    public IActionResult Login(LoginDto loginDto)
    {
        List<string> loginErrors = UserValidator.ValidateUsersLogin(loginDto).ToList();
        if (loginErrors.Any())
        {
            return BadRequest(loginErrors);
        }

        var user = _context.Users.FirstOrDefault(u => u.Email == loginDto.Email && u.Password == loginDto.Password);

        if (user == null)
        {
            return Unauthorized("Invalid email or password.");
        }
        return Ok($"You were successfully logged in, your id:{user.UserId}");
    }
}