using ASPCore.ADONETLab.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MyCodeDemo.Models
{
    public class LoaiRepoAdoNet : ILoaiRepository
    {
        public Loai LayLoai(int maLoai)
        {
            var loaiParam = new SqlParameter[]
            {
                new SqlParameter("MaLoai", maLoai),
            };
            var result = DataProvider.SelectData("spLayLoai", CommandType.StoredProcedure, loaiParam);
            if (result.Rows.Count > 0)
            {
                return new Loai
                {
                    MaLoai = Convert.ToInt32(result.Rows[0]["MaLoai"]),
                    TenLoai = result.Rows[0]["TenLoai"].ToString(),
                    MoTa = result.Rows[0]["MoTa"].ToString(),
                    Hinh = result.Rows[0]["Hinh"].ToString()
                };
            }
            return null;
        }

        public List<Loai> LayTatCa()
        {
            var result = DataProvider.SelectData("spLayTatCaLoai", CommandType.StoredProcedure, null);

            var dsLoai = new List<Loai>();

            foreach (DataRow row in result.Rows)
            {
                dsLoai.Add(new Loai
                {
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
            try
            {
                var loaiparams = new SqlParameter[]
                {
                    new SqlParameter("MaLoai", loai.MaLoai),
                    new SqlParameter("TenLoai", loai.TenLoai),
                    new SqlParameter("MoTa", loai.MoTa),
                    new SqlParameter("Hinh", loai.Hinh)
                };
                DataProvider.ExcuteNonQuery("spSuaLoai", CommandType.StoredProcedure, loaiparams);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
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
