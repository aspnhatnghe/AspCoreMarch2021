using FinalProject.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.ViewComponents
{
public class CategoryMenu : ViewComponent
    {
        private readonly NhatNgheDbContext _context;

        public CategoryMenu(NhatNgheDbContext context)
        {
            _context = context;
        }

        public async Task    <IViewComponentResult> InvokeAsync()
        {
            var data = await _context.Categories.ToListAsync();

            return View(data);
        }
    }
}
