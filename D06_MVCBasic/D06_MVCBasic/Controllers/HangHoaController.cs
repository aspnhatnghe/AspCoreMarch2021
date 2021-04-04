using D06_MVCBasic.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace D06_MVCBasic.Controllers
{
    public class HangHoaController : Controller
    {
        static List<HangHoa> HangHoas = new List<HangHoa>();

        public HangHoaController()
        {
            //HangHoas.Add(new HangHoa
            //{
            //    MaHh = 1,
            //    TenHh = "Wakeup 247",
            //    DonGia = 7000,
            //    SoLuong = 101,
            //    ConBan = true
            //});
            //HangHoas.Add(new HangHoa
            //{
            //    MaHh = 2,
            //    TenHh = "7Up Revice",
            //    DonGia = 7500,
            //    SoLuong = 81,
            //    ConBan = true
            //});
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            //Lấy hàng hóa có mã là id
            //LINQ: truy vấn trên mảng 
            var sp = HangHoas.SingleOrDefault(hh => hh.MaHh == id);
            if (sp == null)//tìm ko thấy
            {
                return NotFound();
            }

            return View(sp);
        }

        [HttpPost]
        public IActionResult Edit(HangHoa model)
        {
            var sp = HangHoas.SingleOrDefault(hh => hh.MaHh == model.MaHh);
            if(sp == null)
            {
                return NotFound();
            }
            //Update
            sp.TenHh = model.TenHh;
            sp.DonGia = model.DonGia;
            sp.SoLuong = model.SoLuong;
            sp.ConBan = model.ConBan;
            //return View(model);
            return RedirectToAction("Edit", "HangHoa", new { id = model.MaHh});
        }

        public IActionResult Delete(int maHh)
        {
            var sp = HangHoas.SingleOrDefault(hh => hh.MaHh == maHh);
            if(sp != null)//có
            {
                HangHoas.Remove(sp);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            return View(HangHoas);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(HangHoa model)
        {
            var sp = HangHoas.SingleOrDefault(hh => hh.MaHh == model.MaHh);
            if(sp != null)//đã có
            {
                ViewBag.ThongBao = "Hàng hóa này đã có";
                return View(model);
            }
            HangHoas.Add(model);
            return RedirectToAction(actionName: "Index", controllerName: "HangHoa");
            //return RedirectToAction("Index", "HangHoa");
        }
    }
}