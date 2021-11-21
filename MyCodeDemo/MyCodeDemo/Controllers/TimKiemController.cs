using Microsoft.AspNetCore.Mvc;
using MyCodeDemo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCodeDemo.Controllers
{
    public class TimKiemController : Controller
    {
        private readonly eStore20Context _context;

        public TimKiemController(eStore20Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string Keyword, double? FromPrice, double? ToPrice)
        {
            var data = _context.HangHoa.AsQueryable();
            if (!string.IsNullOrEmpty(Keyword))
            {
                data = data.Where(hh => hh.TenHh.Contains(Keyword));
            }
            if (FromPrice.HasValue)
            {
                data = data.Where(hh => hh.DonGia.Value >= FromPrice);
            }
            if (ToPrice.HasValue)
            {
                data = data.Where(hh => hh.DonGia.Value <= ToPrice);
            }

            var result = data.ToList();
            return View();
        }
    }
}

