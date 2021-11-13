using Microsoft.AspNetCore.Mvc;
using MyCodeDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCodeDemo.Controllers
{
    public class LoaiController : Controller
    {
        private readonly ILoaiRepository _loaiRepo;

        public LoaiController(ILoaiRepository loaiRepo)
        {
            _loaiRepo = loaiRepo;
        }

        public IActionResult Index()
        {
            return View(_loaiRepo.LayTatCa());
        }

        [HttpGet]
        public IActionResult Edit(int maloai)
        {
            var loai = _loaiRepo.LayLoai(maloai);
            return View(loai);
        }

        [HttpPost]
        public IActionResult Edit(int maloai, Loai model)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(model.Hinh))
                {
                    model.Hinh = "";
                }
                _loaiRepo.SuaLoai(model);
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
