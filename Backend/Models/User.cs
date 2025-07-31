namespace Backend.Models;

using System.Text.Json.Serialization;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

[Table("users")]
[Index(nameof(Email), IsUnique = true)]
[Index(nameof(Username), IsUnique = true)]
public class User {
    // Non-optional 
    [Key]
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [StringLength(100)]
    [Column("email", TypeName = "VARCHAR")]
    public string Email { get; set; }

    [Required]
    [StringLength(255)]
    [JsonIgnore]
    [Column("passwordhash", TypeName = "VARCHAR")]
    public string PasswordHash { get; set; }
    
    [Required]
    [StringLength(50)]
    [Column("username", TypeName = "VARCHAR")]
    public string Username { get; set; }

    [Required]
    [StringLength(100)]
    [Column("firstname", TypeName = "VARCHAR")]
    public string Firstname { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    [Column("lastname", TypeName = "VARCHAR")]
    public string Lastname { get; set; } = string.Empty;

    [Required]
    [StringLength(10)]
    [Column("gender", TypeName = "VARCHAR")]
    public string? Gender { get; set; }

    
    [Column("role", TypeName = "VARCHAR")]
    public string? Role { get; set; } = null;
    
    [StringLength(2048)]
    [Column("bio", TypeName = "TEXT")]
    public string? Bio { get; set; }
    
    [StringLength(255)]
    [Column("profileimageurl", TypeName = "VARCHAR")]
    public string? ProfileImageUrl { get; set; } = null;
    

    [Column("dateofbirth")]
    public DateTime DateOfBirth { get; set; }

    [StringLength(100)]
    [Column("country", TypeName = "VARCHAR")]
    public string Country { get; set; } = string.Empty;
    
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    [Column("createdat")]
    public DateTime CreatedAt { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    [Column("updatedat")]
    public DateTime UpdatedAt { get; set; }

    [Column("lastlogin")]
    public DateTime? LastLogin { get; set; }

}