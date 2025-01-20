using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecpmmerceApp.Application.Services.Logging
{
    public interface IAppLogger<T>
    {
        public void LogError(Exception ex, string message);
        public void LogWarning( string message);
        public void LogInformation(string message);

    }
}
