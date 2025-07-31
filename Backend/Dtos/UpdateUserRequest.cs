namespace Backend.Dtos;

public class UpdateUserRequest {
    public required string Email { get; set; }
    public string? Password { get; set; }

    public string? Username { get; set; }
    public string? Firstname { get; set; }
    public string? Lastname { get; set; }
    public string? Gender { get; set; }
    public string? Bio { get; set; }
    public string? ProfileImageUrl { get; set; }
    public string? Role { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? Country { get; set; }
    public string? Extension { get; set; }
}