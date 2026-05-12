using ProfileService.Data.Contexts;
using ProfileService.Data.Entities;

namespace ProfileService.Data.Repositories;

public class UserProfileRepository(ProfileDbContext context)
    : BaseRepository<UserProfileEntity>(context), IUserProfileRepository
{
}