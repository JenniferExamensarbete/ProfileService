using ProfileService.Business.Models;

namespace ProfileService.Business.Interfaces;

public interface IProfileService
{
    Task<ProfileResult> CreateProfileAsync(CreateProfileRequest request);
    Task<ProfileResult<Profile?>> GetProfileAsync(string authUserId);
    Task<ProfileResult<IEnumerable<Profile>>> GetProfilesAsync();
    Task<ProfileResult> UpdateProfileAsync(string authUserId, UpdateProfileRequest request);
    Task<ProfileResult> DeleteProfileAsync(string authUserId);
}