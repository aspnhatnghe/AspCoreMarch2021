using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace D05_MVCBasic.Controllers
{
    public class CalculatorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Calculate(double SoHang01, string PhepToan, double SoHang02)
        {
            var ketQua = 0.0;
            switch (PhepToan)
            {
                case "+": ketQua = SoHang01 + SoHang02; break;
                case "-": ketQua = SoHang01 - SoHang02; break;
                case "*": ketQua = SoHang01 * SoHang02; break;
                case "/": ketQua = SoHang01 / SoHang02; break;
                case "%": ketQua = SoHang01 % SoHang02; break;
                case "^": ketQua = Math.Pow(SoHang01, SoHang02); break;
            }
            //return Content($"{SoHang01} {PhepToan} {SoHang02} = {ketQua}");

            ViewBag.A = SoHang01;
            ViewBag.B = SoHang02;
            ViewBag.KQ = ketQua;
            ViewBag.PT = PhepToan;
            return View("Index");
            //return View("~/Views/Home/Index.cshtml");
        }
    }
}