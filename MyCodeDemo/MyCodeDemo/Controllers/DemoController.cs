using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using MyCodeDemo.Helpers;
using MyCodeDemo.Models;
using MyCodeDemo.Entities;
using MyCodeDemo.ViewModels;
using System.IO;
using OfficeOpenXml;

namespace MyCodeDemo.Controllers
{

    public class DemoController : Controller
    {
        private readonly eStore20Context _context;

        public DemoController(eStore20Context context)
        {
            _context = context;
        }

        public IActionResult HangHoa()
        {
            var result = _context.HangHoa.Select(hh => new HangHoaVM
            {
                MaHh = hh.MaHh,
                TenHh = hh.TenHh,
                Hinh = hh.Hinh,
                DonGia = hh.DonGia.Value,
                GiamGia = hh.GiamGia,
                Loai = hh.MaLoaiNavigation.TenLoai,
                NhaCungCap = hh.MaNccNavigation.TenCongTy
            }).ToList();

            return View(result);
        }

        public IActionResult Index()
        {
            var thongtin = new ThongTin
            {
                Ten = "Trung tâm CNTT Nhất Nghệ",
                NamThanhLap = 2013,
                ConHoatDong = true
            };
            HttpContext.Session.SetInt32("khoa", 19);
            HttpContext.Session.SetString("hocphi", "Ba triệu");
            HttpContext.Session.Set("thongtin", thongtin);

            return View();
        }

        public IActionResult ExportLoai()
        {
            //lấy data cần xuất
            var data = _context.Loai.ToList();

            var filepath = Path.Combine(Directory.GetCurrentDirectory(), "ReportTemplates", "TemplateLoai.xlsx");
            var file = new FileInfo(filepath);
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage(file))
            {
                var sheet = package.Workbook.Worksheets["Sheet1"];
                sheet.Cells["A1"].Value = "Hello World!";
                var index = 6;
                foreach(var lo in data)
                {
                    sheet.Cells[$"A{index}"].Value = lo.MaLoai.ToString();
                    sheet.Cells[$"B{index}"].Value = lo.TenLoai.ToString();
                    sheet.Cells[$"C{index}"].Value = lo.MoTa.ToString();
                    sheet.Cells[$"D{index}"].Value = lo.Hinh?.ToString();
                    index++;
                }

                // Save to file
                package.Save();

                var excelData = package.GetAsByteArray();
                var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                var fileName = "DSLoai.xlsx";
                return File(excelData, contentType, fileName);
            }
        }
    }
}
