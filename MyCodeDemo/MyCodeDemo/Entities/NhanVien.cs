using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MyCodeDemo.Entities
{
    public partial class NhanVien
    {
        public NhanVien()
        {
            ChuDe = new HashSet<ChuDe>();
            HoaDon = new HashSet<HoaDon>();
            HoiDap = new HashSet<HoiDap>();
        }

        public string MaNv { get; set; }
        public string HoTen { get; set; }
        public string Email { get; set; }
        public string MatKhau { get; set; }

        public virtual ICollection<ChuDe> ChuDe { get; set; }
        public virtual ICollection<HoaDon> HoaDon { get; set; }
        public virtual ICollection<HoiDap> HoiDap { get; set; }
    }
}
