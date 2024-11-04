using CNC_shared.Data;
using CNC_shared.DTOs;
using CNC_shared.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CNC_shared.Helpers
{
    public class UserHelper : IUserHelper
    {
        private readonly DataContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;

        public UserHelper(DataContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, SignInManager<User> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }
        public async Task<IdentityResult> AddUserAsync(User User, string password)
        {
            return await _userManager.CreateAsync(User, password);
        }

        public async Task AddUserToRoleAsync(User User, string roleName)
        {
            await _userManager.AddToRoleAsync(User, roleName);
        }

        public async Task CheckRoleAsync(string roleName)
        {
            bool roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole
                {
                    Name = roleName,
                });
            }
        }

        public async Task<List<User>> GetAsync()
        {
            var user = await _context.Users
                .ToListAsync();
            //.FindAsync();
            return user!;
        }

        public async Task<User> GetUserAsync(string cedula)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Cedula == cedula);

            return user!;
        }

        public async Task<bool> IsUserInRoleAsync(User User, string roleName)
        {
            return await _userManager.IsInRoleAsync(User, roleName);
        }

        public async Task<SignInResult> LoginAsync(LoginDTO model)
        {
            return await _signInManager.PasswordSignInAsync(model.Cedula, model.Password, false, false);
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
