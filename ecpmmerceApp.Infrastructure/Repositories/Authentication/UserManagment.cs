using ecpmmerceApp.Application.DTOs;
using ecpmmerceApp.Application.DTOs.User;
using ecpmmerceApp.Domain.Entities.Identity;
using ecpmmerceApp.Domain.Interface.Authentication;
using ecpmmerceApp.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ecpmmerceApp.Infrastructure.Repositories.Authentication
{
    internal class UserManagment(IRoleManagment roleManagment,UserManager<AppUser>userManager,SignInManager<AppUser> signInManager,
        ApplicationDbContext context) : IUserManagment
    {
        public async Task<(bool Success, List<string>  Error)> CreateUser(AppUser user)
        { 
       var existingUser = await GetUserByEmail(user.Email!);
    if (existingUser != null)
    {
        return (false, new List<string> { "Email is Already Exist" });
    }

    var result = await userManager.CreateAsync(user, user.PasswordHash!);

    if (!result.Succeeded)
    {
                var errors = result.Errors.Select(e => e.Description).ToList();
                return (false, errors);
            }

            return (true, new List<string>());
        }

        public async Task<int> DeleteUserByEmail(string email)
        {
            var _user=await GetUserByEmail(email);
            if (_user == null)
            {
                return -1;
            }
                    
             context.Users.Remove(_user);
            return await context.SaveChangesAsync();
        }


        public async Task<IEnumerable<AppUser?>> GetAllUsers()=>
           await context.AppUsers.ToListAsync();
        

        public async Task<AppUser?> GetUserByEmail(string email)
        {
            var user=await userManager.FindByEmailAsync(email);
            return user;
        }

        public async Task<AppUser?> GetUserById(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            return user;
        }

        public async Task<List<Claim>> GetUserClaims(string email)
        {
           var user=await GetUserByEmail(email);
            string? roleName=await roleManagment.GetUserRole(email);
            List<Claim> claims =
           [
           new Claim("FullName",user!.FullName),
           new Claim(ClaimTypes.NameIdentifier,user!.Id),
           new Claim( ClaimTypes.Email,user.Email!),
           new Claim(ClaimTypes.Role,roleName!)

           ];
            return claims;
        }

        public async Task<(bool Success, string? Error)> LoginUser(AppUser user)
        {
            var _user =await GetUserByEmail(user.Email!);
            if (_user == null) return (false, "Invalid email.");
            var _role=await roleManagment.GetUserRole(user.Email!);
            if (string.IsNullOrEmpty(_role)) return (false, "User role not assigned or invalid.");
            var res = await signInManager.PasswordSignInAsync(_user.UserName!, user.PasswordHash!, false, false);
            if(!res.Succeeded) return (false, "Invalid password.");
            return  (true,null);
        }
        public async Task LogOut()
        {
            await signInManager.SignOutAsync();
           
        }

    }
}
