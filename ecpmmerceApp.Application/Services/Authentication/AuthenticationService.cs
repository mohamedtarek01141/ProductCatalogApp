using AutoMapper;
using ecpmmerceApp.Application.DTOs;
using ecpmmerceApp.Application.DTOs.User;
using ecpmmerceApp.Application.Services.AuthenticationService;
using ecpmmerceApp.Application.Services.Logging;
using ecpmmerceApp.Application.Services.Validation;
using ecpmmerceApp.Domain.Entities.Identity;
using ecpmmerceApp.Domain.Interface.Authentication;
using FluentValidation;
namespace ecpmmerceApp.Application.Services.Authentication
{
    public class AuthenticationService(IUserManagment userManagment,IRoleManagment roleManagment,
        IAppLogger<AuthenticationService> appLogger,IMapper mapper,IValidator<CreateUser> createUserValidator,
        IValidator<LoginUser> loginUserValidator,IValidateUser validateUser) : IAuthenticationService
    {
        public async Task<ServiceResponse> CreateUser(CreateUser createUser)
        {
           var validateRes= await validateUser.ValidateUserAsync(createUser,createUserValidator);
            if (!validateRes.Success) 
                return validateRes; 
            var mappedModel=mapper.Map<AppUser>(createUser);
            mappedModel.Email = createUser.Email;
            mappedModel.PasswordHash = createUser.Password;
            mappedModel.UserName=createUser.FullName;
            var res = await userManagment.CreateUser(mappedModel);
            if (res.Success == false)
                return new ServiceResponse { Errors =res.Error };
            var user = await userManagment.GetUserByEmail(createUser.Email);
            var users=await userManagment.GetAllUsers();
            bool assignedRole = await roleManagment.AddUserRole(user!, users!.Count() > 1 ? "User" : "Admin");
            if (!assignedRole)
            {
                var removeRes=await userManagment.DeleteUserByEmail(createUser.Email);
                if(removeRes<0)
                {
                    appLogger.LogError(new Exception($"Error occured while deleting this email as {createUser.Email}"),
                        "Role can't be assigned");
                    return new ServiceResponse { Errors = new List<string> { "Failed to Create Account" } };
                }

            }

            return new ServiceResponse { Success = true, Message = "Account Created Successfully" };
        }

      
        public async Task<ServiceResponse> LoginUser(LoginUser loginUser)
        {
            var validateRes = await validateUser.ValidateUserAsync(loginUser, loginUserValidator);
            if (!validateRes.Success)
                return new ServiceResponse { Errors=validateRes.Errors };
            var mappedModel = mapper.Map<AppUser>(loginUser);
            mappedModel.PasswordHash = loginUser.Password;
            mappedModel.Email = loginUser.Email;
            var loginRes = await userManagment.LoginUser(mappedModel);
            if (!loginRes.Success)
                return new ServiceResponse { Errors = new List<string> { loginRes.Error! } };
            var user = await userManagment.GetUserByEmail(loginUser.Email);
            var claims = await userManagment.GetUserClaims(loginUser.Email);
            return new ServiceResponse { Success = true };
        }
        public async Task LogOut()
        {
            await userManagment.LogOut();
        }
    }
}
