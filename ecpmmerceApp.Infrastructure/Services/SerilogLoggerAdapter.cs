using ecpmmerceApp.Application.Services.Logging;
using Microsoft.Extensions.Logging;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecpmmerceApp.Infrastructure.Services
{
    public class SerilogLoggerAdapter<T> (ILogger<T> logger): IAppLogger<T>
    {
        public void LogError(Exception ex, string message)=> logger.LogError(ex, message);
       

        public void LogInformation(string message)=>logger.LogInformation(message);


        public void LogWarning(string message) =>logger.LogWarning(message);
        
    }
}
