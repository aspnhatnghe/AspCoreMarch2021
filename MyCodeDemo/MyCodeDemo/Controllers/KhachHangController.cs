using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyCodeDemo.Entities;
using MyCodeDemo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyCodeDemo.Controllers
{
    [Authorize]
    public class KhachHangController : Controller
    {
        private readonly eStore20Context _context;

        public KhachHangController(eStore20Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Profile()
        {
            return View();
        }

        [Authorize(Roles = "KinhDoanh")]
        public IActionResult KinhDoanh()
        {
            //User.IsInRole("KinhDoanh")
            return View();
        }


        [AllowAnonymous]
        public IActionResult Login(string ReturnUrl = null)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginVM model, string ReturnUrl = null)
        {
            var khachHang = _context.KhachHang.SingleOrDefault(kh => kh.MaKh == model.MaKh && kh.MatKhau == model.MatKhau);

            if (khachHang == null) //chưa thành công
            {
                ViewBag.ReturnUrl = ReturnUrl;
                return View();
            }

            //Claims : đặc trưng người dùng (ten, email, role)
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, khachHang.HoTen),
                new Claim(ClaimTypes.Email, khachHang.Email),
                new Claim("MaKh", khachHang.MaKh),
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim(ClaimTypes.Role, "Account")
            };

            var claimIdentity = new ClaimsIdentity(claims, "login");
            var claimPrincipal = new ClaimsPrincipal(claimIdentity);

            await HttpContext.SignInAsync(claimPrincipal);

            if (!string.IsNullOrEmpty(ReturnUrl))
            {
                return Redirect(ReturnUrl);
            }
            return Redirect("/");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Register(RegisterVM model)
        {
            return View();
        }
    }
}
