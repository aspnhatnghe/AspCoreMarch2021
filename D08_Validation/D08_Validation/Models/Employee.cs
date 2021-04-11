using System.ComponentModel.DataAnnotations;

namespace D08_Validation.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Display(Name = "Mã nhân viên")]
        [Required(ErrorMessage ="*")]
        [StringLength(6, MinimumLength = 6, ErrorMessage ="Đúng 6 kí tự")]
        public string EmployeeId { get; set; }

        [Display(Name ="Họ tên")]
        [Required(ErrorMessage ="*")]
        [MinLength(5, ErrorMessage ="Tối thiểu 5 kí tự")]
        public string EmployeeName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Display(Name ="Nhập lại Email")]
        [Compare("Email", ErrorMessage ="Không khớp")]
        public string ConfirmEmail { get; set; }

        [Url]
        public string Website { get; set; }

        [Display(Name ="Ngày sinh")]
        [DataType(DataType.Date)]
        public string BirthDate { get; set; }

        [Required]
        public string Address { get; set; }

        [Display(Name ="Giới tính")]
        public string Gender { get; set; }

        [Phone]
        public string Phone { get; set; }
        
        [Display(Name ="Số tài khoản (quốc tế)")]
        [CreditCard]
        public string AccountNumber { get; set; }

        [Display(Name ="Lương")]
        [Range(0, double.MaxValue)]
        public double Salary { get; set; }

        [MaxLength(250)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }
}
