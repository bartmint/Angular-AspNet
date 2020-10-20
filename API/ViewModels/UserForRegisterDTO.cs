using System;
using System.Collections.Generic;
using System.Text;

namespace API.ViewModels
{
    public class UserForRegisterDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
    }
}
