using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using D06_MVCBasic.Models;
using Microsoft.AspNetCore.Mvc;

namespace D06_MVCBasic.Controllers
{
    public class DemoController : Controller
    {
        public async Task<string> DemoAsync()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var a = Demo.DemoAAsync();
            var b = Demo.DemoBAsync();
            var c = Demo.DemoCAsync();
            await a; await b; await c;
            sw.Stop();
            return $"Chạy hết {sw.ElapsedMilliseconds} ms";
        }
        public string DemoSync()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            Demo.DemoA();//2s
            Demo.DemoB();//5s
            Demo.DemoC();//3s
            sw.Stop();
            return $"Chạy hết {sw.ElapsedMilliseconds} ms";
        }

        public IActionResult Index()
        {
            ViewBag.HoTen = "Tèo";
            ViewData["Ngay Sinh"] = new DateTime(1010, 11, 1);
            return View();
        }

        public IActionResult DemoTempData()
        {
            ViewBag.HoTen = "Tèo";
            ViewData["Ngay Sinh"] = new DateTime(1010, 11, 1);
            TempData["AAA"] = "Dữ liệu gởi từ TempData";
            return RedirectToAction("ProcessTempData");
        }

        public IActionResult ProcessTempData()
        {
            return View("Index");
        }
    }
}