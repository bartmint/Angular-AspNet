using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class AppRole: IdentityRole<string>
    {
        public ICollection<AppUserRole> UserRoles { get; set; }
    }
}
