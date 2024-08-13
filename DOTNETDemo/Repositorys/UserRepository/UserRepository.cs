using DOTNETDemo.Data;
using DOTNETDemo.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DOTNETDemo.Repositorys.UserRepository;

public class UserRepository(AppDbContext dbContext) : IUserRepository
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task<User> GetUserAsyncById(int id)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> AddOrUpdateUserAsync(User user)
    {
        if (user.Id > 0)
        {
            _dbContext.Users.Update(user);
        }
        else
        {
            await _dbContext.Users.AddAsync(user);
        }
        await _dbContext.SaveChangesAsync();
        return true;
    }
}
