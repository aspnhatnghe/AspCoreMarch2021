using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using D08_Validation.Models;
using Microsoft.AspNetCore.Mvc;

namespace D08_Validation.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult CheckExistEmplyee(string EmployeeId)
        {
            var employeeList = new string[]
            {
                "admin1", "nhatng", "nhvien","huyrua"
            };
            if (employeeList.Contains(EmployeeId))
            {
                return Json($"Mã {EmployeeId} đã có");
            }
            return Json(true);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("loi", "Còn lỗi nhé");
                return View();
            }
            //Lưu xuống file Json
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", $"Employee{employee.Id}.json");
            var jsonContent = JsonSerializer.Serialize(employee);
            System.IO.File.WriteAllText(filePath, jsonContent);
            return RedirectToAction("Register");
        }
    }
}