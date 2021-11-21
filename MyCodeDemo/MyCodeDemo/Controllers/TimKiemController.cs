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
        private readonly int PAGE_SIZE = 5;

        public TimKiemController(eStore20Context context)
        {
            _context = context;
        }

        //public IActionResult Index()
        //{
        //    return View(new List<HangHoaVM>());
        //}

        //[HttpPost]
        public IActionResult Index(string Keyword, double? FromPrice, double? ToPrice, int page = 1)
        {
            //step 1: Xử lý lấy/lọc dữ liệu
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

            //step 2: Sắp xếp
            data = data.OrderBy(hh => hh.MaLoaiNavigation.TenLoai)
                .ThenByDescending(hh => hh.DonGia) //nếu loại giống thì sắp giảm theo giá
                .ThenBy(hh => hh.TenHh);

            //step 3: Phân trang
            var totalPage = (int)Math.Ceiling(data.Count() * 1.0 / PAGE_SIZE);
            var N = (page - 1) * PAGE_SIZE;
            data = data.Skip(N).Take(PAGE_SIZE);
            ViewBag.TotalPage = totalPage;
            ViewBag.CurrentPage = page;

            //step 4: Lấy dữ liệu ra
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

