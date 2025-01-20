﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecpmmerceApp.Domain.Entities.Identity
{
    public class AppUser:IdentityUser

    {
        public string FullName { get; set; }   = string.Empty;
    }
   
}
