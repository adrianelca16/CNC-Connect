using CNC_shared.Data;
using CNC_shared.Entities;
using CNC_shared.Enums;
using CNC_shared.Helpers;

namespace CNC_API.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }
        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckRolesAsync();
            await CheckUserAsync("28152090", "Adrian", "Hernandez", "adrian@yopmail.com", "0414 391 3971", UserType.Admin);
        }

        private async Task<User> CheckUserAsync(string cedula, string firstName, string lastName, string email, string phone, UserType userType)
        {
            var user = await _userHelper.GetUserAsync(cedula);
            if (user == null)
            {
                user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = cedula,
                    PhoneNumber = phone,
                    UserType = (UserType)userType,
                    Cedula = cedula
                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, userType.ToString());
            }

            return user;
        }

        private async Task CheckRolesAsync()
        {
            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserType.User.ToString());
        }

    }
}