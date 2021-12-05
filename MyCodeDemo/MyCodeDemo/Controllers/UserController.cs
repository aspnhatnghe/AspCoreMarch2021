using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyCodeDemo.Entities;
using MyCodeDemo.ViewModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MyCodeDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly eStore20Context _context;
        private readonly IConfiguration _configuration;

        public UserController(eStore20Context context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Authenticate(LoginVM model)
        {
            var user = _context.KhachHang.SingleOrDefault(kh => kh.MaKh == model.MaKh && kh.MatKhau == model.MatKhau);
            if (user == null)//ko đúng user/pass?
            {
                return Ok(new ApiResponseModel
                {
                    Success = false,
                    Message = "Sai thông tin đăng nhập"
                });
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.HoTen),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("MaKh", user.MaKh),
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim(ClaimTypes.Role, "Account")
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["JwtConfig:secret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenData = tokenHandler.WriteToken(token);
            return Ok(new ApiResponseModelWithData
            {
                Success = true,
                Message = "Đăng nhập thành công",
                Data = tokenData
            });
        }

        [HttpGet]
        public IActionResult Find(string hoTen)
        {
            try
            {
                var users = _context.KhachHang.AsQueryable();
                if (!string.IsNullOrEmpty(hoTen))
                {
                    users = users.Where(kh => kh.HoTen.Contains(hoTen));
                }
                return Ok(users);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
