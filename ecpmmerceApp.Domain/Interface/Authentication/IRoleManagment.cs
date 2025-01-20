using ecpmmerceApp.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecpmmerceApp.Domain.Interface.Authentication
{
    public interface IRoleManagment
    {
        Task<string?> GetUserRole(string userEmail);
        Task<bool>AddUserRole(AppUser user,string roleName);
    }
}
