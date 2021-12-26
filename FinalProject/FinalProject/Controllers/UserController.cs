using AutoMapper;
using FinalProject.Entities;
using FinalProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using FinalProject.Helpers;
using FinalProject.Repositories;

namespace FinalProject.Controllers
{
    public class UserController : Controller
    {
        private readonly NhatNgheDbContext _context;
        private readonly IMapper _mapper;
        private readonly IRoleRepo _roleRepo;

        public UserController(NhatNgheDbContext context, IMapper mapper, IRoleRepo roleRepo)
        {
            _context = context;
            _mapper = mapper;
            _roleRepo = roleRepo;
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
                catch
                {
                    return View();
                }
            }
            return View();
        }

        [HttpGet("/Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("/Login")]
        public IActionResult Login(LoginVM model)
        {
            return View();
        }
    }
}
