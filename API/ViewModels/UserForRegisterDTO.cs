using FluentValidation;
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
    public class NewUserForRegisterValidation : AbstractValidator<UserForRegisterDTO>
    {
        public NewUserForRegisterValidation()
        {
            RuleFor(x => x.Password).NotEmpty();
            RuleFor(x => x.UserName).NotEmpty().MinimumLength(5).MaximumLength(10);
            RuleFor(x => x.ConfirmPassword).Equal(p => p.Password, StringComparer.OrdinalIgnoreCase).WithMessage("Password and Password Confirmed doesnt match");
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
        }
    }
}
