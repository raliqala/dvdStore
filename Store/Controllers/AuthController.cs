using Microsoft.AspNetCore.Mvc;
using TypeLibrary.Interface;
using DataAccessLayer;
using TypeLibrary.ViewModel;
using BusinessLogicLayer;
using TypeLibrary.Models;
using TypeLibrary;
using Store.Models;
using System.Security.Cryptography;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Store.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IDBHandler _handler;
        private readonly IConfiguration _configuration;

        public AuthController(ILogger<AuthController> logger, IConfiguration configuration, IDBHandler handler)
        {
            _logger = logger;
            _handler = handler;
            _configuration = configuration;

        }

        private string GenerateJwtToken(string userName)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", userName) }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        // [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet(nameof(GetResult))]
        public IActionResult GetResult()
        {
            return Ok("API Validated");
        }

        // POST api/<AuthController>
        [HttpPost("/api/signup")]
        public ActionResult<SuccessResponse> SignUp(SignUp model)
        {
            Admin admin = new()
            {
                Name = model.Name,
                Email = model.Email,
            };

            string EncryptedPassword = Crypto.Encrypt(model.Password);
            admin.Password = EncryptedPassword;

            SuccessResponse success = new()
            {
                Success = true,
                Message = "Registered Successfully",
            };

            var adminExist = _handler.BLL_uspAdminExist(admin.Email);

            if (adminExist != null)
            {
                success.Success = false;
                success.Message = "Sorry account already Exist";
                return success;
            }

            if (_handler.BLL_SignUpAdmin(admin))
            {
                return success;
            }

            success.Success = false;
            success.Message = "Registration failed";
            return success;
        }

        [HttpPost("/api/signin/{email}/{password}")]
        public ActionResult<SignIn> SignIn(string email, string password)
        {
            SignIn success = new()
            {
                Success = false,
                Message = "Sorry incorrect credentials",
            };

            var response = _handler.BLL_SignIn(email);

            if (response != null)
            {
                if (Crypto.Decrypt(response.Password) == password)
                {
                    success.token = this.GenerateJwtToken(email);
                    success.Success = true;
                    success.Message = "Welcome";
                    return success;
                }
            }

            return success;
        }
    }
}
