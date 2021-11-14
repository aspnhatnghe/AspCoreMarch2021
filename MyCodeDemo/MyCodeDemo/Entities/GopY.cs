using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MyCodeDemo.Entities
{
    public partial class GopY
    {
        public string MaGy { get; set; }
        public int MaCd { get; set; }
        public string NoiDung { get; set; }
        public DateTime NgayGy { get; set; }
        public string HoTen { get; set; }
        public string Email { get; set; }
        public string DienThoai { get; set; }
        public bool CanTraLoi { get; set; }
        public string TraLoi { get; set; }
        public DateTime? NgayTl { get; set; }
        public bool? IsDelete { get; set; }

        public virtual ChuDe MaCdNavigation { get; set; }
    }
}
