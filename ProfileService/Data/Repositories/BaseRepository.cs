using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ProfileService.Data.Contexts;
using ProfileService.Data.Models;

namespace ProfileService.Data.Repositories;

public abstract class BaseRepository<TEntity>(ProfileDbContext context)
    : IBaseRepository<TEntity> where TEntity : class
{
    protected readonly ProfileDbContext _context = context;
    protected readonly DbSet<TEntity> _table = context.Set<TEntity>();

    public async Task<RepositoryResult> AddAsync(TEntity entity)
    {
        try
        {
            _table.Add(entity);
            await _context.SaveChangesAsync();

            return new RepositoryResult { Success = true };
        }
        catch (Exception ex)
        {
            return new RepositoryResult { Success = false, Error = ex.Message };
        }
    }

    public async Task<RepositoryResult<IEnumerable<TEntity>>> GetAllAsync()
    {
        try
        {
            var result = await _table.ToListAsync();

            return new RepositoryResult<IEnumerable<TEntity>>
            {
                Success = true,
                Result = result
            };
        }
        catch (Exception ex)
        {
            return new RepositoryResult<IEnumerable<TEntity>>
            {
                Success = false,
                Error = ex.Message
            };
        }
    }

    public async Task<RepositoryResult<TEntity>> GetAsync(Expression<Func<TEntity, bool>> expression)
    {
        try
        {
            var result = await _table.FirstOrDefaultAsync(expression);

            if (result == null)
            {
                return new RepositoryResult<TEntity>
                {
                    Success = false,
                    Error = "Not found."
                };
            }

            return new RepositoryResult<TEntity>
            {
                Success = true,
                Result = result
            };
        }
        catch (Exception ex)
        {
            return new RepositoryResult<TEntity>
            {
                Success = false,
                Error = ex.Message
            };
        }
    }

    public async Task<RepositoryResult> UpdateAsync(TEntity entity)
    {
        try
        {
            _table.Update(entity);
            await _context.SaveChangesAsync();

            return new RepositoryResult { Success = true };
        }
        catch (Exception ex)
        {
            return new RepositoryResult { Success = false, Error = ex.Message };
        }
    }

    public async Task<RepositoryResult> DeleteAsync(TEntity entity)
    {
        try
        {
            _table.Remove(entity);
            await _context.SaveChangesAsync();

            return new RepositoryResult { Success = true };
        }
        catch (Exception ex)
        {
            return new RepositoryResult { Success = false, Error = ex.Message };
        }
    }
}