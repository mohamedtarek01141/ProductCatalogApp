using ecpmmerceApp.Application.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecpmmerceApp.Application.Services.Validation
{
    public class ValidateUser : IValidateUser
    {
        public async Task<ServiceResponse> ValidateUserAsync<T>(T model, IValidator<T> validator)
        {
            var validateResult = await validator.ValidateAsync(model);
            if (validateResult.IsValid == false)
            {
                var errors = validateResult.Errors.Select(x => x.ErrorMessage).ToList();
                string errorsMessage = string.Join("; ", errors);
                return new ServiceResponse{Errors=errors};
            }
            return new ServiceResponse { Success=true};
        }
    }
}
