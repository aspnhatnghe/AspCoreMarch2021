using Microsoft.AspNetCore.Mvc;
using MyCodeDemo.Entities;
using MyCodeDemo.ViewModels;
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
            return View(new List<HangHoaVM>());
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

            var result = data
                .Select(hh => new HangHoaVM
                {
                    MaHh = hh.MaHh,
                    TenHh = hh.TenHh,
                    DonGia = hh.DonGia.Value,
                    GiamGia = hh.GiamGia,
                    Loai = hh.MaLoaiNavigation.TenLoai,
                    NhaCungCap = hh.MaNccNavigation.TenCongTy
                })
                .ToList();
            //ViewBag.KetQua = result;
            return View(result);
        }
    }
}

