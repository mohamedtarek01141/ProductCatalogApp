using ecpmmerceApp.Application.DTOs;
using ecpmmerceApp.Domain.Entities.Identity;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecpmmerceApp.Application.Services.Validation
{
    public interface IValidateUser
    {
        Task<ServiceResponse> ValidateUserAsync<T>(T model, IValidator<T> validator);
    }

}
