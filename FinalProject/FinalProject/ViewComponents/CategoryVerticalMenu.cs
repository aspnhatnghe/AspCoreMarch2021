using FinalProject.Entities;
using FinalProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.ViewComponents
{
    public class CategoryVerticalMenu : ViewComponent
    {
        private readonly NhatNgheDbContext _context;

        public CategoryVerticalMenu(NhatNgheDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var data = await _context.Categories
                .Select(c => new CategoryVerticalMenuVM { 
                    Id = c.Id,
                    CategoryName = c.CategoryName,
                    SeoUrl = c.SeoUrl,
                    CountOfProduct = c.Products.Count()
                }).ToListAsync();

            return View(data);
        }
    }
}
