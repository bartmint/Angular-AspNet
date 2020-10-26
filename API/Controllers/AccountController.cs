using API.ViewModels;
using AutoMapper;
using Domain.Abstractions;
using Domain.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NETCore.MailKit.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatsUp.API.ViewModels;

namespace WhatsUp.API.Controllers
{
    
    public class AccountController: BaseApiController
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IAccountRepository _accountRepository;
        private readonly ITokenRepository _tokenRepository;
        private readonly IEmailService _emailService;

        public AccountController(SignInManager<AppUser> signInManager, ITokenRepository tokenRepository,
            UserManager<AppUser> userManager, IMapper mapper, IAccountRepository accountRepository, IEmailService emailService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _mapper = mapper;
            _accountRepository = accountRepository;
            _tokenRepository = tokenRepository;
            _emailService = emailService;
        }


        [HttpPost("register")]
        public async Task<ActionResult> Register(UserForRegisterDTO userForRegisterDTO)
        {
            userForRegisterDTO.UserName = userForRegisterDTO.UserName.ToLower();

            if (await _accountRepository.UserEmailExists(userForRegisterDTO.Email))
                return BadRequest("The Email Provided is already taken");

            var user = _mapper.Map<AppUser>(userForRegisterDTO);

            var result =await _userManager.CreateAsync(user, userForRegisterDTO.Password);
            if (!result.Succeeded) 
                return BadRequest(result.Errors);

            //email
            var userToEmail =await _accountRepository.GetUser(user.Email);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(userToEmail);
            if (String.IsNullOrEmpty(code)) return BadRequest("bblabla");
            var link = Url.Action(nameof(VerifyEmail), "Account", new { userId=userToEmail.Id, code }, Request.Scheme, Request.Host.ToString());
            await _emailService.SendAsync(userToEmail.Email, "Verify email", $"<a href=\"{link}\">Verify email</a>", true);

            return Ok("Registration succedded");
        }
        [HttpGet("VerifyEmail")]
        public async Task<ActionResult> VerifyEmail(string userId, string code)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null) return BadRequest();

            var result = await _userManager.ConfirmEmailAsync(user, code);
            const string link= "http://localhost:4200/succesfull-register";
            if (result.Succeeded) return Redirect(link);

            return BadRequest();

        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(UserForLoginDTO userForLoginDTO)
        {
            var userFromRepo =await _userManager.FindByEmailAsync(userForLoginDTO.Email);

            if (userFromRepo == null) return Unauthorized();

            var resultLogin =await _signInManager.CheckPasswordSignInAsync(userFromRepo, userForLoginDTO.Password, false);
            if (!resultLogin.Succeeded) return Unauthorized();

            var userToReturn = _mapper.Map<UserDTO>(userFromRepo);
            userToReturn.Token = await _tokenRepository.CreateToken(userFromRepo);

            return userToReturn;
        }
        [HttpGet]
        [Authorize]
        public ActionResult<List<string>> Get()
        {
            var list = new List<string>()
            {
                new string("Testowane"),
                new string("uwierzytelnianie"),
                new string("w Account controller.")
            };
            return Ok(list);
        }
        
    }
}
