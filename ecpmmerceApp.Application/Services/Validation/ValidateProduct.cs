using ecpmmerceApp.Application.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecpmmerceApp.Application.Services.Validation
{
    public class ValidateProduct : IValidateProduct
    {
        public async Task<ServiceResponse> ValidateProductAsync<T>(T model, IValidator<T> validator)
        {
        
            var validateResult = await validator.ValidateAsync(model);
            if (validateResult.IsValid == false)
            {
                var errors = validateResult.Errors.Select(x => x.ErrorMessage).ToList();
                return new ServiceResponse { Errors = errors };
            }
            return new ServiceResponse { Success = true };
        }
    
    }
}
