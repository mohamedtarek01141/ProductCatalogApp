using ecpmmerceApp.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ecpmmerceApp.Domain.Interface.Authentication
{
    public interface IUserManagment
    {
        Task<(bool Success, List<string> Error)> CreateUser(AppUser user);
        Task<(bool Success, string? Error)> LoginUser(AppUser user );
        Task<AppUser?> GetUserByEmail(string email);
        Task<AppUser?> GetUserById(string id);
        Task<IEnumerable<AppUser?>> GetAllUsers();
        Task<int> DeleteUserByEmail(string email);
        Task<List<Claim>> GetUserClaims(string email);
        Task LogOut();
    }
}
