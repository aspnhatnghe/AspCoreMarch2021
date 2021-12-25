using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddToCart(Guid id, int qty = 1)
        {
            try
            {
                return Json(new { Success = true, Total = 1, Count = 3 });
            }
            catch
            {
                return Json(new
                {
                    Success = false
                });
            }
        }
    }
}
