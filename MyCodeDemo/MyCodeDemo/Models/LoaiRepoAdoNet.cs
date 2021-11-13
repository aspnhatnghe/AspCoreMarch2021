using ASPCore.ADONETLab.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MyCodeDemo.Models
{
    public class LoaiRepoAdoNet : ILoaiRepository
    {
        public Loai LayLoai(int maLoai)
        {
            throw new NotImplementedException();
        }

        public List<Loai> LayTatCa()
        {
            var result = DataProvider.SelectData("spLayTatCaLoai", CommandType.StoredProcedure, null);

            var dsLoai = new List<Loai>();

            foreach(DataRow row in result.Rows)
            {
                dsLoai.Add(new Loai {
                    MaLoai = Convert.ToInt32(row["MaLoai"]),
                    TenLoai = row["TenLoai"].ToString(),
                    MoTa = row["MoTa"].ToString(),
                    Hinh = row["Hinh"].ToString()
                });
            }

            return dsLoai;
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
