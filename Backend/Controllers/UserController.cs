using Backend.Dtos;
using Microsoft.AspNetCore.Mvc;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase {
    private readonly AppDbContext _db;

    public UserController(AppDbContext db) {
        _db = db;
    }

    // POST /api/user
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request) {
        if (await _db.Users.AnyAsync(u => u.Email == request.Email || u.Username == request.Username))
            return BadRequest("Email or Username already exists.");

        var user = new User {
            Email = request.Email,
            Username = request.Username,
            Firstname = request.Firstname,
            Lastname = request.Lastname,
            Gender = request.Gender,
            DateOfBirth = request.DateOfBirth,
            Bio = request.Bio,
            ProfileImageUrl = request.ProfileImageUrl,
            Country = request.Country,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            Role = "user"
        };

        _db.Users.Add(user);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
    }

    // GET /api/user
    [HttpGet]
    public async Task<IActionResult> GetAll() {
        return Ok(await _db.Users.ToListAsync());
    }

    // GET /api/user/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(Guid id) {
        var user = await _db.Users.FindAsync(id);
        return user is null ? NotFound() : Ok(user);
    }

    // PUT /api/user/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserRequest update) {
        var user = await _db.Users.FindAsync(id);
        if (user is null) return NotFound();

        user.Email = update.Email;
        user.Username = update.Username ?? user.Username;
        user.Firstname = update.Firstname ?? user.Firstname;
        user.Lastname = update.Lastname ?? user.Lastname;
        user.Gender = update.Gender ?? user.Gender;
        user.Bio = update.Bio ?? user.Bio;
        user.ProfileImageUrl = update.ProfileImageUrl ?? user.ProfileImageUrl;
        user.DateOfBirth = update.DateOfBirth ?? user.DateOfBirth;
        user.Country = update.Country ?? user.Country;
        if (!string.IsNullOrWhiteSpace(update.Password))
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(update.Password);
        if (!string.IsNullOrWhiteSpace(update.Role))
            user.Role = update.Role;

        await _db.SaveChangesAsync();
        return NoContent();
    }

    // DELETE /api/user/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id) {
        var user = await _db.Users.FindAsync(id);
        if (user is null) return NotFound();

        _db.Users.Remove(user);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}