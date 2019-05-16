using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using iBlogs.Site.Core.Params;
using iBlogs.Site.Core.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace iBlogs.Site.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {
        private readonly IConfiguration _configuration;

        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public RestResponse<string> Index(LoginParam loginParam)
        {
            var tokenString = GenerateJsonWebToken();
            return RestResponse<string>.ok(tokenString);
        }

        private string GenerateJsonWebToken()
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, "test")
            };

            var days = _configuration["Auth:JwtExpireDays"];
            var key = _configuration["Auth:JwtKey"];
            var issuer = _configuration["Auth:JwtIssuer"];

            var expires = DateTime.UtcNow.AddDays(Convert.ToDouble(days));

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var jwtSecurityToken = new JwtSecurityToken
            (
                issuer,
                issuer,
                claims,
                expires: expires,
                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
            );
            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }
    }
}