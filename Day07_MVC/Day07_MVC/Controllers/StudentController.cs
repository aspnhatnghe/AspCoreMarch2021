using Day07_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Text.Json;

namespace Day07_MVC.Controllers
{
    public class StudentController : Controller
    {
        private readonly string jsonfilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "sinhvien.json");

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
                System.IO.File.WriteAllText(jsonfilePath, svjsonstr);
            }
                return View("Index"); // /Student/LuuXuongFile
            //return RedirectToAction("Index");// /Student/Index
        }

        public IActionResult DocTuFile(string type)
        {
            var sinhVien = new Student();
            if(type == "JSON")
            {
                //đọc content file json
                var jsonContent = System.IO.File.ReadAllText(jsonfilePath);
                sinhVien = System.Text.Json.JsonSerializer.Deserialize<Student>(jsonContent);
            }
            else
            {
                var textfilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "sinhvien", "studentinfo.txt");
                var lines = System.IO.File.ReadAllLines(textfilePath);
                sinhVien.StudentId = int.Parse(TachChuoi(lines[0]));
                sinhVien.StudentName = TachChuoi(lines[1]);
                sinhVien.Diem = double.Parse(TachChuoi(lines[2]));
            }
            return View("Index", sinhVien);
        }

        string TachChuoi(string chuoi)
        {
            return chuoi.Split(":")[1].Trim();
        }
    }
}