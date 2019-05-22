using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using iBlogs.Site.Core.Common.DTO;
using iBlogs.Site.Core.User;
using iBlogs.Site.Core.User.DTO;
using iBlogs.Site.Core.User.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ClaimTypes = iBlogs.Site.Core.Common.ClaimTypes;

namespace iBlogs.Site.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;

        public LoginController(IConfiguration configuration, IUserService userService)
        {
            _configuration = configuration;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public RestResponse<string> Index(LoginParam loginParam)
        {
            var user = _userService.FindUsers(new Users
            {
                Username = loginParam.Username,
                Password = loginParam.Password
            }).FirstOrDefault();
            if (user != null)
                return RestResponse<string>.ok(GenerateJsonWebToken(user));
            return RestResponse<string>.fail("没有找到该用户!");
        }

        private string GenerateJsonWebToken(Users user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Uid, user.Uid.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Username)
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