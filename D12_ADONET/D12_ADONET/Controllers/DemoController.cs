using Microsoft.AspNetCore.Mvc;
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
    }
}