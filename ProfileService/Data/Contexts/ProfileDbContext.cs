using Microsoft.EntityFrameworkCore;
using ProfileService.Data.Entities;

namespace ProfileService.Data.Contexts;

public class ProfileDbContext(DbContextOptions<ProfileDbContext> options)
    : DbContext(options)
{
    public DbSet<UserProfileEntity> UserProfiles { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserProfileEntity>()
            .HasIndex(x => x.AuthUserId)
            .IsUnique();
    }
}