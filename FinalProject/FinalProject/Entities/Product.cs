using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public string SeoUrl { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }        
        public double Price { get; set; }
        public double Discount { get; set; } // lưu từ 0 -> 1
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }
    }
}
