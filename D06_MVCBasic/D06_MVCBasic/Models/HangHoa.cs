using System.ComponentModel.DataAnnotations;

namespace D06_MVCBasic.Models
{
    public class HangHoa
    {
        [Display(Name = "Mã hàng hóa")]
        public int MaHh { get; set; }

        [Display(Name = "Tên hàng hóa")]
        public string TenHh { get; set; }

        [Display(Name = "Đơn giá")]
        public double DonGia { get; set; }

        [Display(Name = "Số lượng")]
        public int SoLuong { get; set; }

        [Display(Name = "Đang bán")]
        public bool ConBan { get; set; }
    }
}
