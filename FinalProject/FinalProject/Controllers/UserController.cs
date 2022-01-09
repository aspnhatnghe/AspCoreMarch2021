using AutoMapper;
using FinalProject.Entities;
using FinalProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using FinalProject.Helpers;
using FinalProject.Repositories;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace FinalProject.Controllers
{
    public class UserController : Controller
    {
        private readonly NhatNgheDbContext _context;
        private readonly IMapper _mapper;
        private readonly IRoleRepo _roleRepo;
        private readonly ILogger<UserController> _logger;

        public UserController(NhatNgheDbContext context, IMapper mapper, IRoleRepo roleRepo, ILogger<UserController> logger)
        {
            _context = context;
            _mapper = mapper;
            _roleRepo = roleRepo;
            _logger = logger;
        }

        [HttpGet("/Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("/Register")]
        public IActionResult Register(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userinfo = _mapper.Map<UserInfo>(model);
                    userinfo.IsActive = false;
                    userinfo.RandomKey = MyTool.GetRandom();
                    userinfo.Password = model.Password.ToSHA512Hash(userinfo.RandomKey);
                    _context.Add(userinfo);

                    var customerRole = _roleRepo.GetRoleByName(MyConstants.Customer);
                    if (customerRole != null)
                    {
                        var ur = new UserRole
                        {
                            UserId = userinfo.Id,
                            RoleId = customerRole.Id
                        };
                        _context.Add(ur);
                    }

                    _context.SaveChanges();
                    return Redirect("/Login");
                }
                catch (Exception ex)
                {
                    _logger.LogInformation("Error when user register");
                    _logger.LogError(ex.Message);
                    return View();
                }
            }
            return View();
        }

        [HttpGet("/Login")]
        public IActionResult Login(string ReturnUrl = null)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [HttpPost("/Login")]
        public async Task<IActionResult> Login(LoginVM model, string ReturnUrl = null)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            var errorMessage = string.Empty;
            if (ModelState.IsValid)
            {
                var user = _context.UserInfos
                    .Include(u => u.UserRoles)
                    .SingleOrDefault(u => u.Username == model.Username);
                if (user == null)
                {
                    ViewBag.ErrorMessage = $"User {model.Username} doesn't exist";
                    return View();
                }

                if (model.Password.ToSHA512Hash(user.RandomKey) == user.Password)
                {
                    //Claims : đặc trưng người dùng (ten, email, role)
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.FullName),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(MyConstants.ClaimCustomerId, user.Id.ToString())
                    };

                    foreach (var item in user.UserRoles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, item.RoleId.ToString()));
                    }


                    var claimIdentity = new ClaimsIdentity(claims, "login");
                    var claimPrincipal = new ClaimsPrincipal(claimIdentity);

                    await HttpContext.SignInAsync(claimPrincipal);

                    if (!string.IsNullOrEmpty(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }

                    //Ghi log
                    _logger.LogInformation($"{user.FullName} login successful at {DateTime.Now}");

                    //nếu là Admin thì chuyển sang Admin
                    var adminRole = _context.Roles.SingleOrDefault(r => r.RoleName == MyConstants.Administartor);
                    if (user.UserRoles.Select(ur => ur.RoleId).Contains(adminRole.Id))
                    {
                        return Redirect("/Admin");
                    }
                    return Redirect("/");
                }
                ViewBag.ErrorMessage = "Invalid password";
                return View();
            }
            ViewBag.ErrorMessage = errorMessage;
            return View();
        }

        [Route("/Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
    }
}
