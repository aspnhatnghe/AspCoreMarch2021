using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using D08_Validation.Models;
using Microsoft.AspNetCore.Mvc;

namespace D08_Validation.Controllers
{
    public class LayoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MenuPhai()
        {
            return View();
        }

        public IActionResult NoTemplate()
        {
            return View();
        }

        public IActionResult ChildLayout()
        {
            return View();
        }

        public IActionResult DemoPartial()
        {
            return PartialView("_DanhMuc");
        }
        
        public IActionResult DemoPartialWithData()
        {
            var data = new List<Category> {
        new Category{ CategoryId = 1, CategoryName = "Điện máy"},
        new Category{ CategoryId = 2, CategoryName = "Điện tử"},
        new Category{ CategoryId = 3, CategoryName = "Điện lạnh"},
        new Category{ CategoryId = 4, CategoryName = "Điện gia dụng"}
    };
            return PartialView("_DanhMuc", data);
        }
    }
}