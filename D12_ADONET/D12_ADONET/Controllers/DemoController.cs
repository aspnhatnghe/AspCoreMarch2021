using ASPCore.ADONETLab.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.Data.SqlClient;
namespace D12_ADONET.Controllers
{
    public class DemoController : Controller
    {
        string chuoiKetNoi = @"Data Source=.;Initial Catalog=eStore20;Integrated Security=True";

        public IActionResult LayLoai()
        {
            var sql = "SELECT * FROM Loai ORDER BY TenLoai";
            SqlConnection connection = new SqlConnection(chuoiKetNoi);
            SqlDataAdapter da = new SqlDataAdapter(sql, connection);
            DataTable dtLoai = new DataTable();
            da.Fill(dtLoai);

            return View(dtLoai);
        }

        public IActionResult GetLoai()
        {
            var sql = "SELECT * FROM Loai ORDER BY TenLoai";
            //DataTable dtLoai = DataProvider.TruyVan_LayDuLieu(sql);

            var dtLoai = DataProvider.SelectData(sql, CommandType.Text, null);

            return View("LayLoai", dtLoai);
        }

        public string ThemLoai(string Loai = "Bia")
        {
            try
            {
                var sqlInsert = $"INSERT INTO LOAI(TenLoai) VALUES(N'{Loai}') ";
                SqlConnection con = new SqlConnection(chuoiKetNoi);
                SqlCommand cmd = new SqlCommand(sqlInsert, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return $"Thêm loại {Loai} thành công";
            }
            catch (Exception ex)
            {
                return $"Lỗi: {ex.Message}";
            }
        }
    }
}