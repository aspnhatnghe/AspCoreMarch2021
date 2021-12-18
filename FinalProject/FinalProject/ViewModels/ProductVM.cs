using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.ViewModels
{
    public class ProductVM
    {
        [Display(Name = "Tên hàng hóa")]
        public string ProductName { get; set; }

        [Display(Name = "Hình")]
        public string Image { get; set; }

        [Display(Name = "Mô tả")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Giá")]
        [Range(0, double.MaxValue, ErrorMessage ="Giá không âm")]
        public double Price { get; set; }

        [Display(Name = "Giảm giá")]
        public double Discount { get; set; } // lưu từ 0 -> 1
        [Display(Name = "Loại")]
        public int CategoryId { get; set; }
    }
}
