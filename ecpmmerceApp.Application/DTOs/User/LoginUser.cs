﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecpmmerceApp.Application.DTOs.User
{
    public class LoginUser
    {
        public string Email { get; set; }=string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
