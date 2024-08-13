using DOTNETDemo.Data;
using DOTNETDemo.Models.Entities;
using DOTNETDemo.Models.Request;
using Microsoft.EntityFrameworkCore;

namespace Stanza.AzureFunctions.Services.UserService;

public class UserService(AppDbContext db) : IUserService
{
    private readonly AppDbContext _db = db;

    public async Task<User> GetUserAsyncById(int id)
    {
        return await _db.Users.FirstAsync(x => x.Id == id);
    }

    public async Task<bool> PostUserDataAsync(UserCardRequest request)
    {
        try
        {
            User user;

            if (request.Id > 0)
            {
                user = await GetUserAsyncById(request.Id);
                if (user == null)
                {
                    return false; // Handle the case where the user is not found
                }
            }
            else
            {
                user = new User
                {
                    Name = string.Empty // Assuming name is empty for new users
                };
                _db.Users.Add(user);
            }

            user.CardNumber = request.CardNumber;
            user.CVC = request.CVC;
            user.ExpiryDate = request.ExpiryDate;

            await _db.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
