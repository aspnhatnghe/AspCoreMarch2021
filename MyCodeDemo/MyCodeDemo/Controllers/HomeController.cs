using ASPCore.ADONETLab.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MyCodeDemo.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCodeDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public IConfiguration _configuration { get; }

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public string DanhSachLoai()
        {
            var sb = new StringBuilder();

            //var sqlConnection = new SqlConnection(_configuration.GetConnectionString("MyEstore"));
            //var sqlCommand = new SqlCommand("SELECT * FROM Loai", sqlConnection);
            //var adapter = new SqlDataAdapter(sqlCommand);
            //var tblLoai = new DataTable();
            //adapter.Fill(tblLoai);

            var tblLoai = DataProvider.TruyVan_LayDuLieu("SELECT * FROM Loai");

            foreach (DataRow dr in tblLoai.Rows)
            {
                sb.AppendLine(dr["TenLoai"].ToString());
            }

            return sb.ToString();
        }

        public string ThemLoai(string ten)
        {
            var sb = new StringBuilder();
            try
            {
                var sql = $"INSERT INTO Loai(TenLoai, MoTa) VALUES(N'{ten}', N'{ten}')";

                //var sqlConnection = new SqlConnection(_configuration.GetConnectionString("MyEstore"));
                //sqlConnection.Open();
                //var sqlCommand = new SqlCommand(sql, sqlConnection);
                //sqlCommand.ExecuteNonQuery();
                //sqlConnection.Close();

                DataProvider.TruyVan_XuLy(sql);

                sb.AppendLine("Thành công");
            }
            catch (Exception ex)
            {
                sb.AppendLine($"Lỗi: {ex.Message}");
            }

            return sb.ToString();
        }

        public string ReadConfig()
        {
            var sb = new StringBuilder();
            sb.AppendLine(_configuration["ConnectionStrings:MyEstore"]);
            sb.AppendLine(_configuration.GetConnectionString("MyEstore"));
            sb.AppendLine(_configuration["MySetting:NoiDung:S1"]);
            return sb.ToString();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



    }
}
