using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhatsUp.API.ViewModels
{
    public class UserForLoginDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class NewUserForLoginDTOValidation : AbstractValidator<UserForLoginDTO>
    {
        public NewUserForLoginDTOValidation()
        {
            //haslo jest weryfikowane w service
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
        }
    }
}
