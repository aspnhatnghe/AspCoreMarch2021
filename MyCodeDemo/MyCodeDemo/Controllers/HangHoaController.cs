using Microsoft.AspNetCore.Mvc;
using MyCodeDemo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCodeDemo.Controllers
{
    public class HangHoaController : Controller
    {
        private readonly IHangHoaService _hangHoaService;

        public HangHoaController(IHangHoaService hangHoaService)
        {
            _hangHoaService = hangHoaService;
        }

        public IActionResult Index()
        {
            return View(_hangHoaService.GetAll());
        }
    }
}
