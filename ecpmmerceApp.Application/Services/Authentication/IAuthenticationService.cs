using ecpmmerceApp.Application.DTOs;
using ecpmmerceApp.Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecpmmerceApp.Application.Services.AuthenticationService
{
    public interface IAuthenticationService
    {
        Task<ServiceResponse> LoginUser(LoginUser loginUser);
        Task<ServiceResponse> CreateUser(CreateUser createUser);
        Task LogOut();
    }
}
