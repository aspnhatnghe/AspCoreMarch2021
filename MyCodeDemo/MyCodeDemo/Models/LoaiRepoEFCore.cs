using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCodeDemo.Models
{
    public class LoaiRepoEFCore : ILoaiRepository
    {
        private readonly MyDbContext _context;

        public LoaiRepoEFCore(MyDbContext context)
        {
            _context = context;
        }

        public List<Loai> LayTatCa()
        {
            return _context.Loais.ToList();
        }

        public Loai LayLoai(int maLoai)
        {
            var loai = _context.Loais.SingleOrDefault(lo => lo.MaLoai == maLoai);
            return loai;
        }

        public bool SuaLoai(Loai loai)
        {
            var _loai = _context.Loais.SingleOrDefault(lo => lo.MaLoai == loai.MaLoai);
            if (_loai == null)
            {
                return false;
            }
            try
            {
                _loai.TenLoai = loai.TenLoai;
                _loai.MoTa = loai.MoTa;
                _loai.Hinh = loai.Hinh;
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Loai ThemLoai(Loai loai)
        {
            try
            {
                _context.Add(loai);
                _context.SaveChanges();
                return loai;
            }
            catch
            {
                return null;
            }
        }

        public List<Loai> TimLoai(string keyword)
        {
            throw new NotImplementedException();
        }

        public bool XoaLoai(int maLoai)
        {
            var _loai = _context.Loais.SingleOrDefault(lo => lo.MaLoai == maLoai);
            if (_loai == null)
            {
                return false;
            }
            try
            {
                _context.Remove(_loai);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
