using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCodeDemo.Models
{
    public interface ILoaiRepository
    {
        List<Loai> LayTatCa();
        List<Loai> TimLoai(string keyword);
        Loai LayLoai(int maLoai);
        bool SuaLoai(Loai loai);
        bool XoaLoai(int maLoai);
        Loai ThemLoai(Loai loai);
    }
}
