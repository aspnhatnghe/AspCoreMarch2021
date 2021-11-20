using MyCodeDemo.Entities;
using System.Collections.Generic;

namespace MyCodeDemo.Services
{
    public interface IHangHoaService
    {
        HangHoa GetById(int id);
        List<HangHoa> GetAll();
        List<HangHoa> Search(string keyword, double? giaTu, double? giaDen);
        HangHoa Add(HangHoa hangHoa);
        bool Update(HangHoa hangHoa);
        bool Remove(int maHh);
    }
}
