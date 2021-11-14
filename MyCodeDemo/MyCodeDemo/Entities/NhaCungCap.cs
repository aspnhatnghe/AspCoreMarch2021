using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MyCodeDemo.Entities
{
    public partial class NhaCungCap
    {
        public NhaCungCap()
        {
            HangHoa = new HashSet<HangHoa>();
        }

        public string MaNcc { get; set; }
        public string TenCongTy { get; set; }
        public string Logo { get; set; }
        public string NguoiLienLac { get; set; }
        public string Email { get; set; }
        public string DienThoai { get; set; }
        public string DiaChi { get; set; }
        public string MoTa { get; set; }

        public virtual ICollection<HangHoa> HangHoa { get; set; }
    }
}
