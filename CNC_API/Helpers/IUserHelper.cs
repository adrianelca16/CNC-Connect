

using CNC_shared.DTOs;
using CNC_shared.Entities;
using Microsoft.AspNetCore.Identity;

namespace CNC_shared.Helpers
{
    public interface IUserHelper
    {
        Task<List<User>> GetAsync();

        Task<User> GetUserAsync(string cedula);

        Task<IdentityResult> AddUserAsync(User User, string password);

        Task CheckRoleAsync(string roleName);

        Task AddUserToRoleAsync(User User, string roleName);

        Task<bool> IsUserInRoleAsync(User User, string roleName);

        Task<SignInResult> LoginAsync(LoginDTO model);

        Task LogoutAsync();
    }
}
