using FinalProject.Entities;
using FinalProject.Helpers;
using FinalProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Controllers
{
    public class ProductController : Controller
    {
        private readonly NhatNgheDbContext _context;

        public ProductController(NhatNgheDbContext context)
        {
            _context = context;
        }

        [HttpGet("/chung-loai/{tenLoai}")]
        public IActionResult Index(string tenLoai)
        {
            ViewBag.TenLoai = tenLoai;
            //var data = _context.Products.Where(p => p.Category.SeoUrl == tenLoai)
            //    .Select(p => new ProductViewModel
            //    {
            //        CategoryId = p.CategoryId,
            //        Description = p.Description,
            //        Discount = p.Discount,
            //        Image = p.Image,
            //        Price = p.Price,
            //        ProductName = p.ProductName,
            //        Id = p.Id,
            //        SeoUrl = p.SeoUrl
            //    }).ToList();
            //return View(data);

            return View();
        }

        [HttpPost]
        public IActionResult FilterProduct(string tenLoai, string sortBy = "Name", string sortType = "asc", int page = 1)
        {
            var data = _context.Products.Include(p => p.Category).AsQueryable();

            // Filter
            if (!string.IsNullOrEmpty(tenLoai))
            {
                data = data.Where(p => p.Category.SeoUrl == tenLoai);
            }

            var result = data.Select(p => new ProductViewModel
            {
                CategoryId = p.CategoryId,
                Description = p.Description,
                Discount = p.Discount,
                Image = p.Image,
                Price = p.Price,
                ProductName = p.ProductName,
                Id = p.Id,
                SeoUrl = p.SeoUrl,
                LoaiSeoUrl = p.Category.SeoUrl
            });

            //Sort
            switch (sortBy)
            {
                case "Price":
                    if (sortType.ToLower() == "asc")
                    {
                        result = result.OrderBy(p => p.Price);
                    }
                    else
                    {
                        result = result.OrderByDescending(p => p.Price);
                    }
                    break;
                default:
                    if (sortType.ToLower() == "asc")
                    {
                        result = result.OrderBy(p => p.ProductName);
                    }
                    else
                    {
                        result = result.OrderByDescending(p => p.ProductName);
                    }
                    break;
            }

            //Thông tin phân trang
            ViewBag.TongSoTrang = Convert.ToInt32(Math.Ceiling(result.Count() * 1.0 / MyConstants.NumOfPruductPerPage));
            ViewBag.TrangHienTai = page;

            //Paging
            result = result.Skip((page - 1) * MyConstants.NumOfPruductPerPage).Take(MyConstants.NumOfPruductPerPage);


            return PartialView("_ProductPartial", result.ToList());
        }

        [Route("/{loai}/{sanpham}")]
        public IActionResult Detail(string sanpham)
        {
            var data = _context.Products.SingleOrDefault(hh => hh.SeoUrl == sanpham);
            if (data == null)
            {
                return RedirectToAction("Index");
            }

            return View(data);
        }
    }
}
