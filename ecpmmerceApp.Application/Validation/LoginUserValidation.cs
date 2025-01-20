using ecpmmerceApp.Application.DTOs.User;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecpmmerceApp.Application.Validation
{
    public class LoginUserValidation : AbstractValidator<LoginUser>
    {
        public LoginUserValidation()
        {
            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("Email is Required")
                .EmailAddress().WithMessage("Invalid Email Format");
            RuleFor(u => u.Password)
                .NotEmpty().WithMessage("Password is Required");

        }


    }
}

