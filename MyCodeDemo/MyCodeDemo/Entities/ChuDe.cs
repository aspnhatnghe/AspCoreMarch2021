using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MyCodeDemo.Entities
{
    public partial class ChuDe
    {
        public ChuDe()
        {
            GopY = new HashSet<GopY>();
        }

        public int MaCd { get; set; }
        public string TenCd { get; set; }
        public string MaNv { get; set; }

        public virtual NhanVien MaNvNavigation { get; set; }
        public virtual ICollection<GopY> GopY { get; set; }
    }
}
