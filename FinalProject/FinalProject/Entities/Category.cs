using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string SeoUrl { get; set; } // tự gen, duy nhất
        public string Image { get; set; }

        public int? ParentCategoryId { get; set; }
        public Category ParentCategory { get; set; }
        public ICollection<Category> ChildCategories { get; set; }

        public ICollection<Product> Products { get; set; }
        public Category()
        {
            Products = new HashSet<Product>();
            ChildCategories = new HashSet<Category>();
        }
    }
}
