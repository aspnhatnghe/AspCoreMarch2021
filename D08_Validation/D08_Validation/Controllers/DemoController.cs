using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace D08_Validation.Controllers
{
    public class DemoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Hàm sinh mã bảo mật
        /// </summary>
        /// <param name="length">Chiều dài mã bảo mật</param>
        /// <returns>chuỗi mã bảo mật</returns>
        public string SinhMaBaoMat(int length = 5)
        {
            var pattern = @"qazwsxedcrfvtgbyhnujmikolp<>[]1234567890";
            var result = new StringBuilder();
            var rd = new Random();
            for(int i = 0; i < length; i++)
            {
                result.Append(pattern[rd.Next(0, pattern.Length)]);
            }

            //Lưu giá trị vào Session
            HttpContext.Session.SetString("MaBaoMat", result.ToString());

            return result.ToString();
        }
        
        public IActionResult DangKy()
        {
            ViewBag.MaBaoMat = SinhMaBaoMat();
            return View();
        }
    }
}