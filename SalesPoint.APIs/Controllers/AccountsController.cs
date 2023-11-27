using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sales_Point.Core.Entities.Identity;
using Sales_Point.Core.Service;
using SalesPoint.APIs.DTOs;
using SalesPoint.APIs.Errors;
using SalesPoint.APIs.Extensions;
using System.Security.Claims;

namespace SalesPoint.APIs.Controllers
{

    public class AccountsController : APIBaceController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountsController(UserManager<AppUser> user, SignInManager<AppUser> signInManager, ITokenService tokenservice, IMapper mapper)
        {
            _userManager = user;
            _signInManager = signInManager;
            _tokenService = tokenservice;
            _mapper = mapper;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto model)
        {
            if (CheckEmailExist(model.Email).Result.Value is true)
            { return BadRequest(new ApiResponse(400, "This Email is Already Exits")); }

            var user = new AppUser
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                UserName = model.Email.Split('@')[0],
                PhoneNumber = model.PhoneNumber,
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return BadRequest(new ApiResponse(400));
            }

            var userDto = new UserDto
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _tokenService.CreateTokenAsync(user),

            };

            return Ok(userDto);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto model)
        {
            var User = await _userManager.FindByEmailAsync(model.Email);

            if (User is null) return Unauthorized(new ApiResponse(401));

            var result = await _signInManager.CheckPasswordSignInAsync(User, model.Password, false);

            if (!result.Succeeded) return Unauthorized(new ApiResponse(401));

            return Ok(new UserDto
            {
                DisplayName = User.DisplayName,
                Email = User.Email,
                Token = await _tokenService.CreateTokenAsync(User)
            });
        }





        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);
            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _tokenService.CreateTokenAsync(user)

            });
        }


        [Authorize]
        [HttpGet("address")]
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {

            var user = await _userManager.FindUserWithAddressAsync(User);

            var mappedAddress = _mapper.Map<Address, AddressDto>(user.Address);

            return Ok(mappedAddress);

        }

        [Authorize]
        [HttpPut("address")]
        public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto updatedAddress)
        {

            var user = await _userManager.FindUserWithAddressAsync(User);

            if (user is null)
                return Unauthorized(new ApiResponse(401));

            var address = _mapper.Map<AddressDto, Address>(updatedAddress);

            address.Id = user.Address.Id;


            user.Address = address;


            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded) return BadRequest(new ApiResponse(400));

            return Ok(updatedAddress);
        }


        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExist(string email)
        {
            return await _userManager.FindByEmailAsync(email) is not null;

        }


    }
}
