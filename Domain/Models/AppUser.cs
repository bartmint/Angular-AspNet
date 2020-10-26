using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class AppUser: IdentityUser<string>
    {
        public ICollection<AppUserRole> UserRoles { get; set; }
    }
}
