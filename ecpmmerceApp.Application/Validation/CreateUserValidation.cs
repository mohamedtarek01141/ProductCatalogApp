using ecpmmerceApp.Application.DTOs.User;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecpmmerceApp.Application.Validation
{
    public class CreateUserValidation:AbstractValidator<CreateUser>
    {
        public CreateUserValidation()
        {
            RuleFor(u => u.FullName)
                .NotEmpty().WithMessage("UserName is Required");
            
            RuleFor(u => u.Email)
               .NotEmpty().WithMessage("Email is Required")
               .EmailAddress().WithMessage("Invalid Email Format");
            RuleFor(u => u.Password)
                .NotEmpty().WithMessage("Password is Required")
                .MinimumLength(8).WithMessage("Password should be 8 chars or more")
                .Matches(@"[A-Z]").WithMessage("Password should have at least one UpperCase char")
                .Matches(@"[a-z]").WithMessage("Password should have at least one LowerCase char")
                .Matches(@"\d").WithMessage("Password must contain at least on char");
            RuleFor(u => u.ConfirmPassword)
                .Equal(x => x.Password).WithMessage("password not matches");




        }
    }
}
