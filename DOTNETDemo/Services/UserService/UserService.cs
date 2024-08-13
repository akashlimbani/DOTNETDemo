using DOTNETDemo.Models.Entities;
using DOTNETDemo.Models.Request;
using DOTNETDemo.Repositorys.UserRepository;
using DOTNETDemo.Services.UserService;

namespace Stanza.AzureFunctions.Services.UserService;

public class UserService(IUserRepository userRepository) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<User> GetUserAsyncById(int id)
    {
        return await _userRepository.GetUserAsyncById(id);
    }

    public async Task<bool> PostUserDataAsync(UserCardRequest request)
    {
        try
        {
            User user;
            if (request.Id > 0)
            {
                user = await _userRepository.GetUserAsyncById(request.Id);
                if (user == null)
                {
                    return false; // Handle the case where the user is not found
                }
            }
            else
            {
                user = new User();
            }

            user.Name = user.Name ?? string.Empty;
            user.CardNumber = request.CardNumber;
            user.CVC = request.CVC;
            user.ExpiryDate = request.ExpiryDate;

            await _userRepository.AddOrUpdateUserAsync(user);
            return true;
        }
        catch (Exception)
        {
            // Log exception details here if needed
            return false;
        }
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        try
        {
            var user = await _userRepository.GetUserAsyncById(id);
            if (user == null)
            {
                return false; // User not found
            }

            await _userRepository.DeleteUserAsync(user);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
