using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyCodeDemo.ViewModels
{
    public class RegisterVM
    {
        [Required]
        public string MaKh { get; set; }
        [Required]
        public string MatKhau { get; set; }
        [Required]
        public string HoTen { get; set; }
        public bool GioiTinh { get; set; }
        [DataType(DataType.Date)]
        public DateTime NgaySinh { get; set; }
        public string DiaChi { get; set; }
        public string DienThoai { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
