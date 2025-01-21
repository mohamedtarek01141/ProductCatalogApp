using ecpmmerceApp.Application.Services.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecpmmerceApp.Infrastructure.Middleware
{
    public class ExceptionHandlingMiddleware(RequestDelegate _next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (DbUpdateException ex) 
            {
                var logger= context.RequestServices.GetRequiredService<IAppLogger<ExceptionHandlingMiddleware>>();
                if (ex.InnerException is SqlException innerexception )
                {
                    logger.LogError(ex.InnerException,"Sql Error");
                    switch (innerexception.Number)
                    {
                        case 2627:
                            context.Response.StatusCode = StatusCodes.Status409Conflict;
                            await context.Response.WriteAsync("Primary Key Violation");
                            break;

                        case 515:
                            context.Response.StatusCode = StatusCodes.Status408RequestTimeout;
                            await context.Response.WriteAsync("Can't Insert Null");
                            break;

                        case  547:
                            context.Response.StatusCode = StatusCodes.Status409Conflict;
                            await context.Response.WriteAsync("Forign Key Constrainst Violation");
                            break;

                        default:

                            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                            await context.Response.WriteAsync("AnError Equire");
                            context.Response.Redirect("/Error/Index");

                            break;
                    }

                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    await context.Response.WriteAsync("AnError Equire");
                    context.Response.Redirect("/Error/Index");

                }
            }
            catch(Exception ex)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync("AnError Equire");
                context.Response.Redirect("/Error/Index");

            }
            finally
            {
                context.Response.Redirect("/Error/Index");

            }
        }
        
    }
}
