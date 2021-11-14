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
            throw new NotImplementedException();
        }

        public Loai ThemLoai(Loai loai)
        {
            throw new NotImplementedException();
        }

        public List<Loai> TimLoai(string keyword)
        {
            throw new NotImplementedException();
        }

        public bool XoaLoai(int maLoai)
        {
            throw new NotImplementedException();
        }
    }
}
