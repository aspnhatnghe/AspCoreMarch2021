using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace D10_Layout.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoaiController : Controller
    {
        // host/Admin/Loai/Index
        public IActionResult Index()
        {
            return View();
        }

        // host/Admin/Loai/Random
        public int Random()
        {
            return new Random().Next();
        }
    }
}