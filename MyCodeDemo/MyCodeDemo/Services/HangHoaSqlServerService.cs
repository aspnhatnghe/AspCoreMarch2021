using Microsoft.EntityFrameworkCore;
using MyCodeDemo.Entities;
using MyCodeDemo.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCodeDemo.Services
{
    public class HangHoaSqlServerService : IHangHoaService
    {
        private readonly eStore20Context _context;

        public HangHoaSqlServerService(eStore20Context context)
        {
            _context = context;
        }

        public List<HangHoa> GetAll()
        {
            return _context.HangHoa.Include(hh => hh.MaLoaiNavigation)
                .Include(hh => hh.MaNccNavigation).ToList();
        }

        public List<HangHoa> Search(string keyword, double? giaTu, double? giaDen)
        {
            var data = _context.HangHoa.Include(hh => hh.MaLoaiNavigation)
                .Include(hh => hh.MaNccNavigation).AsQueryable();

            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.Where(hh => hh.TenHh.Contains(keyword, StringComparison.InvariantCultureIgnoreCase));
            }
            if (giaTu.HasValue)
            {
                data = data.Where(hh => hh.DonGia.Value >= giaTu.Value);
            }
            if (giaDen.HasValue)
            {
                data = data.Where(hh => hh.DonGia.Value <= giaDen.Value);
            }

            return data.ToList();
        }

        public HangHoa Add(HangHoa hangHoa)
        {
            try
            {
                _context.Add(hangHoa);
                _context.SaveChanges();
                return hangHoa;
            }
            catch (Exception ex)
            {
                MyTool.StoreLogToTextFile(ex.Message);
                return null;
            }
        }

        public bool Update(HangHoa hangHoa)
        {
            try
            {
                _context.Update(hangHoa);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                MyTool.StoreLogToTextFile(ex.Message);
                return false;
            }
        }

        public bool Remove(int maHh)
        {
            var hangHoa = _context.HangHoa.SingleOrDefault(hh => hh.MaHh == maHh);
            if (hangHoa == null)
            {
                return false;
            }
            try
            {
                _context.Remove(hangHoa);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                MyTool.StoreLogToTextFile(ex.Message);
                return false;
            }
        }
    }
}
