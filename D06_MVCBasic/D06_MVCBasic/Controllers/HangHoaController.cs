using D06_MVCBasic.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace D06_MVCBasic.Controllers
{
    public class HangHoaController : Controller
    {
        static List<HangHoa> HangHoas = new List<HangHoa>();

        public HangHoaController()
        {
            HangHoas.Add(new HangHoa { 
                MaHh = 1, TenHh = "Wakeup 247",
                DonGia = 7000, SoLuong = 101, ConBan = true
            });
            HangHoas.Add(new HangHoa { 
                MaHh = 2, TenHh = "7Up Revice",
                DonGia = 7500, SoLuong = 81, ConBan = true
            });
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}