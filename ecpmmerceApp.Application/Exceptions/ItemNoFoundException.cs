using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecpmmerceApp.Application.Exceptions
{
    public class ItemNoFoundException:Exception
    {
        public ItemNoFoundException(string message):base(message) 
        { }
    }
}
