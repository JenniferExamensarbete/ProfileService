namespace ProfileService.Business.Models;

public class UpdateProfileRequest
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Phone { get; set; }
    public string? ImageUrl { get; set; }
}