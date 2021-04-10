
using System.ComponentModel.DataAnnotations;

namespace Day07_MVC.Models
{
    public class Student
    {
        [Display(Name ="Mã sinh viên")]
        public int StudentId { get; set; }
        [Display(Name = "Họ tên")]
        public string StudentName { get; set; }
        [Display(Name = "Điểm")]
        public double Diem { get; set; }
        [Display(Name = "Xếp loại")]
        public string XepLoai
        {
            get
            {
                var xeploai = "Yếu";
                if(Diem >= 9) { xeploai = "Xuất sắc"; }
                else if (Diem >= 8.5) { xeploai = "Giỏi"; }
                else if (Diem >= 7) { xeploai = "Khá"; }
                else if (Diem >= 5) { xeploai = "Trung bình"; }
                return xeploai;
            }
        }
    }
}
