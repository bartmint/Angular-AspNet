﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class AppUser: IdentityUser<int>
    {
        public ICollection<AppUserRole> UserRoles { get; set; }
    }
}
