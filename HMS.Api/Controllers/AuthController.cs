using HMS.Api.Models;
using HMS.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _config;
    private readonly DepartmentDbContext _context;

    public AuthController(IConfiguration config,
                          DepartmentDbContext context)
    {
        _config = config;
        _context = context;
    }

    // Register
    [HttpPost("register")]
    public async Task<IActionResult> Register(User user)
    {
        if (_context.Users.Any(x => x.Username == user.Username))
        {
            return BadRequest("User already exists");
        }

        _context.Users.Add(user);

        await _context.SaveChangesAsync();

        return Ok("User Registered Successfully");
    }


    // Login
    [HttpPost("login")]
    public IActionResult Login(LoginModel model)
    {
        var user = _context.Users
            .FirstOrDefault(x =>
                x.Username == model.Username &&
                x.Password == model.Password);

        if (user == null)
            return Unauthorized();

        var token = GenerateToken(user);

        return Ok(new
        {
            token,
            role = user.Role
        });
    }


    private string GenerateToken(User user)
    {
        var jwtSettings = _config.GetSection("Jwt");

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtSettings["Key"])
        );

        var creds = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(2),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler()
            .WriteToken(token);
    }
}