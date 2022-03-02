﻿using AuthApi.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
  
        private readonly ILogger<AuthController> _logger;
        private readonly IConfiguration _configuration;
        public AuthController(IConfiguration configuration, ILogger<AuthController> logger)
        {
            _logger = logger;
            _configuration = configuration;
        }

        // GET: api/<Auth>
        [HttpGet]
        public string Get()
        {
            var jwtSettingsSection = _configuration.GetSection("JWTSettings");
            var jwtSettings = jwtSettingsSection.Get<JWTSettings>();

            string username = "Erick";
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("sub", username),
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, "SALESMAN"),
                    new Claim(ClaimTypes.Role, "USER"),
                    new Claim("userType", "SALESMAN"),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        // GET api/<Auth>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<Auth>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<Auth>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<Auth>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

    }
}
