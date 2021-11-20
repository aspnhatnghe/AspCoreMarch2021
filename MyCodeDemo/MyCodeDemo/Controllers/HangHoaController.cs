using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCodeDemo.Controllers
{
    public class HangHoaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
