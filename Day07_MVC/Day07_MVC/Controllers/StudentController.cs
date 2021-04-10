using Day07_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text;

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

                var textfoldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "sinhvien");
                if (!Directory.Exists(textfoldPath))
                {
                    Directory.CreateDirectory(textfoldPath);
                }
                var textFilePath = Path.Combine(textfoldPath, "studentinfo.txt");
                System.IO.File.WriteAllLines(textFilePath, chuoi);
            }
            else if (Save == "Lưu JSON")
            {
                //object ==> json string 
                var svjsonstr = System.Text.Json.JsonSerializer.Serialize(sinhvien);
                var jsonfilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "sinhvien.json");
                System.IO.File.WriteAllText(jsonfilePath, svjsonstr);
            }
                return View("Index"); // /Student/LuuXuongFile
            //return RedirectToAction("Index");// /Student/Index
        }
    }
}