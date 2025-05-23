﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Zimozi_dotnet_api_assignment.data;
using Zimozi_dotnet_api_assignment.Models.DTO;
using Zimozi_dotnet_api_assignment.Models.Entities;
using Zimozi_dotnet_api_assignment.Repositories;

namespace Zimozi_dotnet_api_assignment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly IUserRepository _users;
        private readonly IConfiguration _configuration;

        public LoginController(IUserRepository users, IConfiguration configuration)
        {
            this._configuration = configuration;
            _users = users;
        }
        // Post: /api/login
        [HttpPost]
        public ActionResult Login([FromBody]UserLoginDto userLogin)
        {
            // Find user based on Username
            var user = _users.GetByUsername(userLogin.Username);
            if (user == null)
            {
                return Forbid();
            }

            // Password verification is not included to simplify showcase

            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            string jwtKey = Environment.GetEnvironmentVariable("Jwt_Key") ?? _configuration["Jwt:Key"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var tokenValidityMins = _configuration.GetValue<int>("Jwt:TokenValidityMins");
            var tokenExpiryTimeStamp = DateTime.UtcNow.AddMinutes(tokenValidityMins);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("UserId", user.Id.ToString()),
                new Claim("Username", user.Username.ToString()),
                new Claim("Role", user.Role.ToString()),
            };

            var signingCred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer,
                audience,
                claims,
                expires: tokenExpiryTimeStamp,
                signingCredentials: signingCred
                );
            string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new { Token = tokenValue, User = user});
        }
    }
}
