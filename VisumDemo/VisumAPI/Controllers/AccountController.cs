using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using VisumAPI.Models;

namespace VisumAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<Customer> _userManager;
        private readonly SignInManager<Customer> _signInManager;
        private readonly IMapper _mapper;
        private readonly JwtHandler _jwtHandler;

        public AccountController(UserManager<Customer> userManager, IMapper mapper, JwtHandler jwtHandler, SignInManager<Customer> signInManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _jwtHandler = jwtHandler;
            _signInManager = signInManager;
        }

        [HttpPost, Route("login")]
        public async Task<IActionResult> Login([FromBody] Customer userData)
        {
            var user = await _signInManager.Users.FindByNameAsync(userData.UserName);
            if (user == null || !await _userManager.CheckPasswordAsync(user, userData.Password))
                return Unauthorized(new AuthResponse { ErrorMessage = "Invalid Credentials" });

            var signinCredentials = _jwtHandler.GetSigningCredentials();
            var claims = _jwtHandler.GetClaims(user);
            var tokenOptions = _jwtHandler.GenerateTokenOptions(signinCredentials, claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return Ok(new AuthResponse { IsAuthSuccessful = true, Token = token });
    } }
}
