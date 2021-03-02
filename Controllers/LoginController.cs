using CompanyAPI.Models;
using CompanyAPI.Models.DB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace CompanyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public LoginController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        [HttpGet]
        public IActionResult Login(string username, string password) // login request
        {
            DbLogin db = new DbLogin(this._configuration);
            User loginUser = new User();

            loginUser.userName = username;
            loginUser.password = password;

            IActionResult response = Unauthorized();
            if(db.isUser(loginUser))
            {
                var tokenStr = GenerateJSONWebToken(loginUser);
                response = Ok(new { token = tokenStr });
            }
            return response;
        }
        private string GenerateJSONWebToken(User loginUSR) // generate a Json Web Token
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,loginUSR.userName),
                new Claim(JwtRegisteredClaimNames.Sub,loginUSR.password),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Issuer"],
                claims,
                expires:DateTime.Now.AddMinutes(120),
                signingCredentials:credentials);

            var encodeToken = new JwtSecurityTokenHandler().WriteToken(token);
            return encodeToken;
        }
    }
}
