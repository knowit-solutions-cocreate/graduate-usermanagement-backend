namespace Backend.Dtos;

public class CreateUserRequest {
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string Username { get; set; }
    public required string Firstname { get; set; }
    public required string Lastname { get; set; }
    public required string Gender { get; set; }
    public required DateTime DateOfBirth { get; set; }
    public string? Bio { get; set; }
    public string? ProfileImageUrl { get; set; }
    public string? Country { get; set; }
}