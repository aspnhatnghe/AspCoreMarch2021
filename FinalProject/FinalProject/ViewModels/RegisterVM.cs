using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.ViewModels
{
    public class RegisterVM
    {
        [Display(Name ="Họ tên")]
        [Required]
        [MaxLength(150)]
        public string FullName { get; set; }

        [Display(Name = "Địa chỉ")]
        [Required]
        [MaxLength(150)]
        public string Address { get; set; }

        [Display(Name = "Số điện thoại")]
        [Required]
        public string Phone { get; set; }

        [Required]
        public string Email { get; set; }

        [Display(Name = "Mã đăng nhập")]
        [Required]
        public string Username { get; set; }

        [Display(Name = "Mật khẩu")]
        [Required]
        public string Password { get; set; }
    }
}
