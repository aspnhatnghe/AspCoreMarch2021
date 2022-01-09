using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinalProject.Entities;
using Microsoft.AspNetCore.Http;
using FinalProject.Helpers;
using FinalProject.Models;
using Microsoft.AspNetCore.Authorization;

namespace FinalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class CategoriesController : Controller
    {
        private readonly NhatNgheDbContext _context;

        public CategoriesController(NhatNgheDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Categories
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categories.ToListAsync());
        }

        // GET: Admin/Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        private List<CategoryDropDownModel> GetCategories()
        {
            var result = new List<CategoryDropDownModel>();
            var data = _context.Categories.ToList();

            var categoriesLevel1 = data.Where(p => p.ParentCategoryId == null).OrderBy(p => p.CategoryName);

            foreach (var item in categoriesLevel1)
            {
                result.Add(new CategoryDropDownModel
                {
                    Id = item.Id,
                    CategoryName = item.CategoryName
                });

                //xử lý tương tự cho level 2
                var categoriesLevel2 = data.Where(p => p.ParentCategoryId == item.Id).OrderBy(p => p.CategoryName);
                if (categoriesLevel2.Count() > 0)
                {
                    foreach (var item2 in categoriesLevel2)
                    {
                        result.Add(new CategoryDropDownModel
                        {
                            Id = item2.Id,
                            CategoryName = "|____" + item2.CategoryName
                        });

                        //xử lý tương tự cho level 3
                        var categoriesLevel3 = data.Where(p => p.ParentCategoryId == item2.Id).OrderBy(p => p.CategoryName);
                        if (categoriesLevel3.Count() > 0)
                        {
                            foreach (var item3 in categoriesLevel3)
                            {
                                result.Add(new CategoryDropDownModel
                                {
                                    Id = item3.Id,
                                    CategoryName = "........|____" + item3.CategoryName
                                });
                            }
                        }
                    }
                }
            }

            return result;
        }

        // GET: Admin/Categories/Create
        public IActionResult Create()
        {
            ViewData["ParentCategoryId"] = new SelectList(GetCategories(), "Id", "CategoryName");
            return View();
        }

        // POST: Admin/Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category, IFormFile Image)
        {
            //if (ModelState.IsValid)
            {
                if (Image != null)
                {
                    category.Image = MyTool.UploadFile("Loai", Image);
                }
                category.SeoUrl = category.CategoryName.ToSeoUrl();
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //return View(category);
        }

        // GET: Admin/Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Admin/Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CategoryName,Image")] Category category, IFormFile Hinh)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (Hinh != null)
                    {
                        category.Image = MyTool.UploadFile("Loai", Hinh);
                    }
                    category.SeoUrl = category.CategoryName.ToSeoUrl();
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Admin/Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Admin/Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}
