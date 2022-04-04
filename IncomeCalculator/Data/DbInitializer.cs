using IncomeCalculator.Data;
using IncomeCalculator.Shared.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;

namespace IncomeCalculator.Data
{
    public class DbInitializer : IDbInitializer
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly BenefitsContext _db;
        public DbInitializer(UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            BenefitsContext db)
        {
            _db = db;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public void Initialize()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
                if (!_roleManager.RoleExistsAsync(UserType.Admin.ToString()).GetAwaiter().GetResult())
                {
                    _roleManager.CreateAsync(new IdentityRole(UserType.Admin.ToString())).GetAwaiter().GetResult();
                    _roleManager.CreateAsync(new IdentityRole(UserType.Customer.ToString())).GetAwaiter().GetResult();
                }
                else
                {
                    return;
                }

                IdentityUser user = new()
                {
                    UserName = "oamoscovitch@gmail.com",
                    Email = "oamoscovitch@gmail.com",
                    EmailConfirmed = true
                };

                _userManager.CreateAsync(user, "Admin123*").GetAwaiter().GetResult();

                _userManager.AddToRoleAsync(user, UserType.Admin.ToString()).GetAwaiter().GetResult();

            }
            catch (Exception ex)
            {

            }
        }
    }
}
