using API.Context;
using API.Models;
using API.Repository;
using API.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        public IConfiguration configuration;
        private readonly MyContext myContext;

        public TokenController(IConfiguration config, MyContext context)
        {
            this.configuration = config;
            this.myContext = context;
        }

        [HttpPost]
        public IActionResult Post(LoginVM loginVM)
        {
            var alternatif = myContext.Accounts.Find(loginVM.NIK);
            if (alternatif != null)
            {
                var user = myContext.Accounts.FirstOrDefault(a => a.NIK == loginVM.NIK);
                if (user != null && Hashing.ValidatePassword(loginVM.Password, user.Password))
                {
                    //create claims details based on the user information
                    var email = myContext.Employees.Find(user.NIK);
                    var role = myContext.AccountRoles.FirstOrDefault(a => a.AccountId == user.NIK);
                    var find = myContext.Roles.FirstOrDefault(a => a.Id == role.RoleID);
                    var claims = new[] 
                    {
                        //new Claim(JwtRegisteredClaimNames.Sub, configuration["Jwt:Subject"]),
                        //new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        //new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("email", email.Email),
                        new Claim("role", find.Name)
                    };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(configuration["Jwt:Issuer"], configuration["Jwt:Audience"], claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);
                    var show = new JwtSecurityTokenHandler().WriteToken(token);
                    return Ok(new {status = HttpStatusCode.OK ,nik = user.NIK, token = show});
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
