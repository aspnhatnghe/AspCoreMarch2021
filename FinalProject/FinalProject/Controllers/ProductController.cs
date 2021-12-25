using FinalProject.Entities;
using FinalProject.Helpers;
using FinalProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
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
            var data = _context.Products.Where(p => p.Category.SeoUrl == tenLoai)
                .Select(p => new ProductViewModel
                { 
                    CategoryId = p.CategoryId,
                    Description = p.Description,
                    Discount = p.Discount,
                    Image = p.Image,
                    Price = p.Price,
                    ProductName = p.ProductName,
                    Id = p.Id,
                    SeoUrl = p.SeoUrl
                }).ToList();
            return View(data);
        }
    }
}
