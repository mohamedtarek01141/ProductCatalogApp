using ecpmmerceApp.Domain.Entities.Identity;
using ecpmmerceApp.Domain.Interface.Authentication;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecpmmerceApp.Infrastructure.Repositories.Authentication
{
    internal class RoleManagment(UserManager<AppUser> userManager ) : IRoleManagment
    {
        public async Task<bool> AddUserRole(AppUser user, string roleName)
        {
            bool result = (await userManager.AddToRoleAsync(user, roleName)).Succeeded;
            return result;
        }

        public async Task<string?> GetUserRole(string userEmail)
        {
            var user=await userManager.FindByEmailAsync(userEmail);
           return (await userManager.GetRolesAsync(user!)).FirstOrDefault();
        }
    }
}
