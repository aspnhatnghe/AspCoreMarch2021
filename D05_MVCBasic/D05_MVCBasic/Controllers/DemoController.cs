using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using D05_MVCBasic.Models;
using Microsoft.AspNetCore.Mvc;

namespace D05_MVCBasic.Controllers
{
    public class DemoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Edit()
        {
            var sp = new Product
            {
                ProductId = 1, Name = "Beer", Price = 1.3
            };

            return View(sp);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            return View();
        }
    }
}