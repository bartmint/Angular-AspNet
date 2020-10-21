using API.ViewModels;
using AutoMapper;
using Domain.Abstractions;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        public AccountController(SignInManager<AppUser> signInManager, ITokenRepository tokenRepository,
            UserManager<AppUser> userManager, IMapper mapper, IAccountRepository accountRepository)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _mapper = mapper;
            _accountRepository = accountRepository;
            _tokenRepository = tokenRepository;
        }


        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(UserForRegisterDTO userForRegisterDTO)
        {
            userForRegisterDTO.UserName = userForRegisterDTO.UserName.ToLower();

            if (await _accountRepository.UserEmailExists(userForRegisterDTO.Email))
                return BadRequest("The Email Provided is already taken");

            if (await _accountRepository.UserUsernameExists(userForRegisterDTO.UserName))
                return BadRequest("The Username Provided is already taken");

            var user = _mapper.Map<AppUser>(userForRegisterDTO);

            var result =await _userManager.CreateAsync(user, userForRegisterDTO.Password);
            if (!result.Succeeded) 
                return BadRequest(result.Errors);

            var userToReturn = new UserDTO();//zmapowac na zwracane wartosci, narazie zwraca tylko token
            userToReturn.Token =await _tokenRepository.CreateToken(user);

            return userToReturn;
        }
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
    }
}
