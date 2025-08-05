using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Models;
using System.Security.Claims;
using Backend.Dtos;
using Backend.Utility;

namespace Backend.Controllers;

[ApiController]
[Route("api")]
public class AuthController : ControllerBase {
    private readonly AppDbContext _db;

    public AuthController(AppDbContext db) {
        _db = db;
    }

    // POST /api/login
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request) {
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            return Unauthorized("Invalid credentials");

        user.LastLogin = DateTime.UtcNow;
        await _db.SaveChangesAsync();

        var token = JwtToken.Generate(user);
        return Ok(new { token });
    }

    // GET /api/validate
    [HttpGet("validate")]
    public IActionResult Validate() {
        var userId = HttpContext.Items["UserId"]?.ToString();
        if (userId == null) return Unauthorized();

        var user = _db.Users.FirstOrDefault(u => u.Id.ToString() == userId);
        if (user == null) return Unauthorized();

        return Ok(user);
    }
}