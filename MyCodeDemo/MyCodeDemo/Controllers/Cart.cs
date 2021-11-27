using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyCodeDemo.Entities;
using MyCodeDemo.Helpers;
using MyCodeDemo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCodeDemo.Controllers
{
    public class Cart : Controller
    {
        public Cart(eStore20Context context)
        {
            _context = context;
        }

        const string GIO_HANG = "GioHang";
        private readonly eStore20Context _context;

        public IActionResult AddToCart(int id)
        {
            var gioHang = Carts;
            var item = gioHang.SingleOrDefault(p => p.MaHh == id);
            if (item != null) //có
            {
                item.SoLuong++;
            }
            else
            {
                var hangHoa = _context.HangHoa.SingleOrDefault(hh => hh.MaHh == id);
                item = new CartItem
                {
                    MaHh = id,
                    DonGia = hangHoa.DonGia.Value,
                    Hinh = hangHoa.Hinh,
                    SoLuong = 1,
                    TenHh = hangHoa.TenHh
                };
                gioHang.Add(item);
            }
            HttpContext.Session.Set<List<CartItem>>(GIO_HANG, gioHang);
            return RedirectToAction(nameof(Index));
        }

        public List<CartItem> Carts
        {
            get
            {
                var gioHang = HttpContext.Session.Get<List<CartItem>>(GIO_HANG);
                if (gioHang == null)
                {
                    gioHang = new List<CartItem>();
                }
                return gioHang;
            }
        }

        public IActionResult Index()
        {
            return View(Carts);
        }
    }
}
