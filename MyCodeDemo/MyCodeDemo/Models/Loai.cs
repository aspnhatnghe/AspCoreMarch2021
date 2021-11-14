using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyCodeDemo.Models
{
    public class Loai
    {
        [Key]
        public int MaLoai { get; set; }
        [Required]
        [MaxLength(50)]
        public string TenLoai { get; set; }
        public string Hinh { get; set; }
        public string MoTa { get; set; }

        public ICollection<HangHoa> HangHoas { get; set; }

        public Loai()
        {
            HangHoas = new HashSet<HangHoa>();
        }
    }
}
