using ProfileService.Business.Interfaces;
using ProfileService.Business.Models;
using ProfileService.Data.Entities;
using ProfileService.Data.Repositories;

namespace ProfileService.Business.Services;

public class ProfileService(IUserProfileRepository profileRepository) : IProfileService
{
    private readonly IUserProfileRepository _profileRepository = profileRepository;

    public async Task<ProfileResult> CreateProfileAsync(CreateProfileRequest request)
    {
        var existingProfile = await _profileRepository.GetAsync(x => x.AuthUserId == request.AuthUserId);

        if (existingProfile.Success)
        {
            return new ProfileResult
            {
                Success = false,
                Error = "Profile already exists."
            };
        }

        var entity = new UserProfileEntity
        {
            AuthUserId = request.AuthUserId,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Phone = request.Phone,
            ImageUrl = request.ImageUrl
        };

        var result = await _profileRepository.AddAsync(entity);

        return result.Success
            ? new ProfileResult { Success = true }
            : new ProfileResult { Success = false, Error = result.Error };
    }

    public async Task<ProfileResult<Profile?>> GetProfileAsync(string authUserId)
    {
        var result = await _profileRepository.GetAsync(x => x.AuthUserId == authUserId);

        if (!result.Success || result.Result == null)
        {
            return new ProfileResult<Profile?>
            {
                Success = false,
                Error = result.Error ?? "Profile not found."
            };
        }

        return new ProfileResult<Profile?>
        {
            Success = true,
            Result = MapToProfile(result.Result)
        };
    }

    public async Task<ProfileResult<IEnumerable<Profile>>> GetProfilesAsync()
    {
        var result = await _profileRepository.GetAllAsync();

        if (!result.Success || result.Result == null)
        {
            return new ProfileResult<IEnumerable<Profile>>
            {
                Success = false,
                Error = result.Error
            };
        }

        var profiles = result.Result.Select(MapToProfile);

        return new ProfileResult<IEnumerable<Profile>>
        {
            Success = true,
            Result = profiles
        };
    }

    public async Task<ProfileResult> UpdateProfileAsync(string authUserId, UpdateProfileRequest request)
    {
        var result = await _profileRepository.GetAsync(x => x.AuthUserId == authUserId);

        if (!result.Success || result.Result == null)
        {
            return new ProfileResult
            {
                Success = false,
                Error = result.Error ?? "Profile not found."
            };
        }

        var profile = result.Result;

        profile.FirstName = request.FirstName ?? profile.FirstName;
        profile.LastName = request.LastName ?? profile.LastName;
        profile.Phone = request.Phone ?? profile.Phone;
        profile.ImageUrl = request.ImageUrl ?? profile.ImageUrl;
        profile.UpdatedAt = DateTime.UtcNow;

        var updateResult = await _profileRepository.UpdateAsync(profile);

        return updateResult.Success
            ? new ProfileResult { Success = true }
            : new ProfileResult { Success = false, Error = updateResult.Error };
    }

    public async Task<ProfileResult> DeleteProfileAsync(string authUserId)
    {
        var result = await _profileRepository.GetAsync(x => x.AuthUserId == authUserId);

        if (!result.Success || result.Result == null)
        {
            return new ProfileResult
            {
                Success = false,
                Error = result.Error ?? "Profile not found."
            };
        }

        var deleteResult = await _profileRepository.DeleteAsync(result.Result);

        return deleteResult.Success
            ? new ProfileResult { Success = true }
            : new ProfileResult { Success = false, Error = deleteResult.Error };
    }

    private static Profile MapToProfile(UserProfileEntity entity)
    {
        return new Profile
        {
            Id = entity.Id,
            AuthUserId = entity.AuthUserId,
            Email = entity.Email,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Phone = entity.Phone,
            ImageUrl = entity.ImageUrl,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt
        };
    }
}