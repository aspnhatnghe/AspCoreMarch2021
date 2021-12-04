using Microsoft.AspNetCore.Mvc;
using MyCodeDemo.Entities;
using MyCodeDemo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCodeDemo.Controllers
{
    public class AjaxController : Controller
    {
        private readonly eStore20Context _context;

        public AjaxController(eStore20Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public string GetServerTime()
        {
            return DateTime.Now.ToString("dd/MM/yyy hh:mm:sss");
        }


        #region Search
        public IActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ProcessSearch(string keyword)
        {
            var result = _context.HangHoa.AsQueryable();
            if (!string.IsNullOrEmpty(keyword))
            {
                result = result.Where(hh => hh.TenHh.Contains(keyword));
            }
            var data = result.Select(hh => new HangHoaViewModel { 
                TenHh = hh.TenHh,
                MaHh = hh.MaHh,
                DonGia = hh.DonGia.Value,
                Hinh = hh.Hinh
            });

            return PartialView(data.ToList());
        }
        #endregion
    }
}
