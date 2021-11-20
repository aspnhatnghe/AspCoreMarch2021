using Microsoft.AspNetCore.Mvc;
using MyCodeDemo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyCodeDemo.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using model = MyCodeDemo.Models;
using Microsoft.AspNetCore.Http;
using MyCodeDemo.Helpers;

namespace MyCodeDemo.Controllers
{
    public class HangHoaController : Controller
    {
        private readonly IHangHoaService _hangHoaService;
        private readonly eStore20Context _context;

        public HangHoaController(IHangHoaService hangHoaService, eStore20Context context)
        {
            _hangHoaService = hangHoaService;
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_hangHoaService.GetAll());
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.MaLoai = new SelectList(_context.Loai.ToList(), "MaLoai", "TenLoai");
            ViewBag.MaNcc = new SelectList(_context.NhaCungCap.ToList(), "MaNcc", "TenCongTy");
            return View();
        }

        [HttpPost]
        public IActionResult Add(HangHoa model, IFormFile FileHinh)
        {
            if (ModelState.IsValid)
            {
                //Hình?
                if(FileHinh != null)
                {
                    //upload file
                    model.Hinh = MyTool.UploadFile("HangHoa", FileHinh);
                }

                _hangHoaService.Add(model);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
    }
}
