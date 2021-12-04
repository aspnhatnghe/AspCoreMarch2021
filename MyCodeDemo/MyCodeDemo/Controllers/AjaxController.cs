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
        public IActionResult ProcessSearch(string keyword, double? from, double? to)
        {
            var result = _context.HangHoa.AsQueryable();
            if (!string.IsNullOrEmpty(keyword))
            {
                result = result.Where(hh => hh.TenHh.Contains(keyword));
            }
            if (from.HasValue)
            {
                result = result.Where(hh => hh.DonGia >= from.Value);
            }
            if (to.HasValue)
            {
                result = result.Where(hh => hh.DonGia <= to.Value);
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

        #region AjaxReturnJson
        public IActionResult DemoAjaxJson()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ProjectSearchJson(string keyword, double? from, double? to)
        {
            var result = _context.HangHoa.AsQueryable();
            if (!string.IsNullOrEmpty(keyword))
            {
                result = result.Where(hh => hh.TenHh.Contains(keyword));
            }
            if (from.HasValue)
            {
                result = result.Where(hh => hh.DonGia >= from.Value);
            }
            if (to.HasValue)
            {
                result = result.Where(hh => hh.DonGia <= to.Value);
            }
            var data = result.Select(hh => new HangHoaViewModel
            {
                TenHh = hh.TenHh,
                MaHh = hh.MaHh,
                DonGia = hh.DonGia.Value,
                Hinh = hh.Hinh
            });

            return Json(data.ToList());
        }
        #endregion


        #region LoadMore
        const int SO_SP_1_TRANG = 19;
        public IActionResult Products()
        {
            ViewBag.TongSoSanPham = _context.HangHoa.Count();
            ViewBag.TongSoTrang = Math.Ceiling(1.0 * _context.HangHoa.Count() / SO_SP_1_TRANG);
            return View();
        }

        [HttpPost]
        public IActionResult LoadMore(int page)
        {
            var data = _context.HangHoa
                .Skip((page - 1) * SO_SP_1_TRANG).Take(SO_SP_1_TRANG)
                .Select(hh => new HangHoaViewModel
                {
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
