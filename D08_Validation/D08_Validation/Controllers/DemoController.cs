using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace D08_Validation.Controllers
{
    public class DemoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult DangKy()
        {
            return View();
        }
    }
}