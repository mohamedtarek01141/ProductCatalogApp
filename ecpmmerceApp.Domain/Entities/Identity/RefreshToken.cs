﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecpmmerceApp.Domain.Entities.Identity
{
    public class RefreshToken
    {
        public Guid Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;

    }
}
