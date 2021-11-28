using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyCodeDemo.ViewModels
{
    public class LoginVM
    {
        [Display(Name ="Mã người dùng")]
        [Required]
        public string MaKh { get; set; }

        [Required]
        [Display(Name ="Mật khẩu")]
        public string MatKhau { get; set; }
    }
}
