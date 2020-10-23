using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.ViewModels
{
    public class UserDTO
    {
        public string UserName { get; set;}
        public string Token { get; set; }
        public string Email { get; set; }
    }
    public class NewUserDTOValidation: AbstractValidator<UserDTO>
    {
        public NewUserDTOValidation()
        {
            RuleFor(x => x.UserName).NotEmpty().MinimumLength(5).MaximumLength(10);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
        }
    }
}
