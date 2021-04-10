using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Day07_MVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Day07_MVC.Controllers
{
    public class DemoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UploadSingleFile(IFormFile MyFile)
        {
            if (MyFile != null)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "MyData", MyFile.FileName);
                using (var file = new FileStream(filePath, FileMode.Create))
                {
                    MyFile.CopyTo(file);
                }
                TempData["ThongBao"] = "Upload file thành công.";
            }
            else
            {
                TempData["ThongBao"] = "Không có được upload.";
            }
            return Redirect("/Demo/Index");
        }

        public IActionResult UploadMultiFile(List<IFormFile> MyFiles)
        {
            foreach(var MyFile in MyFiles)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "MyData", MyFile.FileName);
                using (var file = new FileStream(filePath, FileMode.Create))
                {
                    MyFile.CopyTo(file);
                }
            }
            return Redirect("/Demo/Index");
        }

        public IActionResult ShowFile()
        {
            var folder = Path.Combine(Directory.GetCurrentDirectory(), "MyData");
            var files = Directory.GetFiles(folder);
            var result = new List<FileDetail>();
            foreach(var item in files)
            {
                result.Add(new FileDetail { 
                    FileName = Path.GetFileName(item),
                    FilePath = item
                });
            }

            return View(result);
        }

        public IActionResult Download(string filename)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "MyData", filename);
            var extension = Path.GetExtension(filePath);
            var contentType = "text/plain";
            switch (extension)
            {
                case ".pdf": contentType = "application/pdf"; break;
                case ".docx": contentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document"; break;
                case ".png": contentType = "image/png"; break;
            }
            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, contentType, filename);
        }
    }
}