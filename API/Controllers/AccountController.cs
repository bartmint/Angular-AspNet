using API.ViewModels;
using AutoMapper;
using Domain.Abstractions;
using Domain.Models;
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
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController: Controller
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
        public async Task<ActionResult<UserDTO>> Register(UserForRegisterDTO userForRegisterDTO)
        {
            userForRegisterDTO.UserName = userForRegisterDTO.UserName.ToLower();

            if (await _accountRepository.UserEmailExists(userForRegisterDTO.Email))
                return BadRequest("The Email Provided is already taken");

            if (await _accountRepository.UserUsernameExists(userForRegisterDTO.UserName))
                return BadRequest("The Username Provided is already taken"); //do wywalenia

            var user = _mapper.Map<AppUser>(userForRegisterDTO);

            var result =await _userManager.CreateAsync(user, userForRegisterDTO.Password);
            if (!result.Succeeded) 
                return BadRequest(result.Errors);

            //email
            var userToEmail =await _accountRepository.GetUser(user.Email);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(userToEmail);
            if (String.IsNullOrEmpty(code)) return BadRequest("bblabla");
            var link = Url.Action(nameof(VerifyEmail), "Account", new { userId=userToEmail.Id, code });
            await _emailService.SendAsync(userToEmail.Email, "email verify", link);

            var userToReturn = new UserDTO();//zmapowac na zwracane wartosci, narazie zwraca tylko token
            userToReturn.Token =await _tokenRepository.CreateToken(user);

            return userToReturn;
        }
        public IActionResult VerifyEmail(string userId, string code) =>  View();
        public IActionResult EmailVerification() =>  View();
        

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(UserForLoginDTO userForLoginDTO)
        {
            var userFromRepo =await _userManager.FindByEmailAsync(userForLoginDTO.Email);

            if (userFromRepo == null) return Unauthorized();

            var resultLogin =await _signInManager.CheckPasswordSignInAsync(userFromRepo, userForLoginDTO.Password, false);
            if (!resultLogin.Succeeded) return Unauthorized();

            var userToReturn = new UserDTO();
            userToReturn.Token = await _tokenRepository.CreateToken(userFromRepo);

            return userToReturn;
        }
        [HttpGet]
        public ActionResult Get()
        {
            return Ok("retard");
        }
    }
}
