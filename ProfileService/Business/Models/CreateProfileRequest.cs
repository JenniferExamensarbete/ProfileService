using System.ComponentModel.DataAnnotations;

namespace ProfileService.Business.Models;

public class CreateProfileRequest
{
    [Required]
    public string AuthUserId { get; set; } = null!;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Phone { get; set; }
    public string? ImageUrl { get; set; }
}