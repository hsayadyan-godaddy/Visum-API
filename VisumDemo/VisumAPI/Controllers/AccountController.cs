using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using VisumAPI.Models;

namespace VisumAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly DBClient _dbClient;
        //private readonly IMapper _mapper;
        private readonly JwtHandler _jwtHandler;

        public AccountController(
            DBClient dBClient, 
            //IMapper mapper,
            JwtHandler jwtHandler)
        {
            //_mapper = mapper;
            _jwtHandler = jwtHandler;
            _dbClient = dBClient;
        }

        [HttpPost, Route("login")]
        public async Task<IActionResult> Login([FromBody] User userData)
        {
            //TODO password hash method
            var user = await _dbClient.GetUserByUserNameAndpass(userData.UserName, userData.Password);
            if (user == null)
                return Unauthorized(new AuthResponse { ErrorMessage = "Invalid Credentials" });

            var signinCredentials = _jwtHandler.GetSigningCredentials();
            var claims = _jwtHandler.GetClaims(user);
            var tokenOptions = _jwtHandler.GenerateTokenOptions(signinCredentials, claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return Ok(new AuthResponse { IsAuthSuccessful = true, Token = token });
        }


        [HttpGet]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> GetUserInfo(string id)
        {
            var user = await _dbClient.GetUserById(id);
            return Ok(user);
        }
    }
}
