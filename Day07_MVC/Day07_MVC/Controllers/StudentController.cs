using Day07_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace Day07_MVC.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LuuXuongFile(Student sinhvien, string Save)
        {
            if (Save == "Lưu TXT")
            {
                var chuoi = new string[] {
                    "Mã SV: " + sinhvien.StudentId,
                    "Họ tên SV: " + sinhvien.StudentName,
                    "Điểm: " + sinhvien.Diem,
                    "Xếp loại: " + sinhvien.XepLoai
                };

                var textFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "studentinfo.txt");
                System.IO.File.WriteAllLines(textFilePath, chuoi);
            }

            return View("Index");
        }
    }
}