using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyCodeDemo.Entities;
using MyCodeDemo.Helpers;
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

        public object MyTools { get; private set; }

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

            if (!khachHang.HieuLuc) //tài khoản đang deactive
            {
                ViewBag.ReturnUrl = ReturnUrl;
                ViewBag.Message = "Tài khoản đang bị khóa";
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
        public async Task<IActionResult> Register(RegisterVM model, IFormFile Hinh)
        {
            if (ModelState.IsValid)
            {
                var hinh = string.Empty;
                if (Hinh != null)
                {
                    hinh = MyTool.UploadFile("KhachHang", Hinh);
                }

                var khachHang = new KhachHang
                {
                    MaKh = model.MaKh,
                    HoTen = model.HoTen,
                    Email = model.Email,
                    DienThoai = model.DienThoai,
                    DiaChi = model.DiaChi,
                    GioiTinh = model.GioiTinh,
                    Hinh = hinh,
                    MatKhau = model.MatKhau,
                    NgaySinh = model.NgaySinh,
                    HieuLuc = false,
                    VaiTro = 1//default - customer
                };
                _context.Add(khachHang);
                await _context.SaveChangesAsync();

                return RedirectToAction("Login");
            }
            return View();
        }
    }
}
