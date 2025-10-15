using Evote360.Core.Domain;
using Evote360.Core.Domain.Interfaces;
using Evote360.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Evote360.Infrastructure.Persistence.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    private readonly VoteContext _context;
    public UserRepository(VoteContext context) : base(context)
    {
        _context = context;
    }

    public async Task<User?> Login(string username, string password)
    {
        return await _context.Set<User>()
            .Where(u => u.UserName == username && u.PasswordHash == password)
            .FirstOrDefaultAsync();
    }

    public async Task<bool> ThisUserNameExitsAsync(string username, int excludeThisId)
    {
        return await Context.Set<User>().AnyAsync(u => u.UserName == username && u.Id != excludeThisId);
    }

    public async Task SetActiveAsync(int id, bool isActive)
    {
        var entity = await Context.Set<User>().FirstOrDefaultAsync(x => x.Id == id);
        if (entity != null)
        {
            entity.IsActive = isActive;
            await Context.SaveChangesAsync();
        }
    }
    
    public async Task<List<User>> GetAllTActiveAsync()
    {
        return await Context.Set<User>().Where(x => x.IsActive).ToListAsync();
    }
    
    public async Task<bool> IsActiveAsync(int id)
    {
        var user = await Context.Set<User>().FindAsync(id);
        if (user == null)
        {
            return false;
        }
        return user.IsActive;
    }
}