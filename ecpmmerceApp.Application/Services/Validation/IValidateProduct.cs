using ecpmmerceApp.Application.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecpmmerceApp.Application.Services.Validation
{
    public interface IValidateProduct
    {
        Task<ServiceResponse> ValidateProductAsync<T>(T model, IValidator<T> validator);

    }
}
