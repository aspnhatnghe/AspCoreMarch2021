using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Day01_Intro.Controllers
{
    public class DemoController : Controller
    {
        public IActionResult InBangCuuChuong()
        {
            return View();
        }

        // host/Demo/KiemTraNam?Nam=2000&Thang=12
        public string KiemTraNam(int Nam, int Thang)
        {
            var laNamNhuan = false;
            //năm nhuận là năm chia hết cho 4 nhưng chia hết cho 100 thì phải chia hết cho 400

            var ngayCuoi2 = new DateTime(2100, 3, 1)
                .AddDays(-1);
            if (Nam % 400 == 0)
            {
                laNamNhuan = true;
            }
            else if (Nam % 4 == 0 && Nam % 100 != 0)
            {
                laNamNhuan = true;
            }
            var soNgay = 0;
            switch (Thang)
            {
                case 1: case 3: case 5: case 7:
                case 8: case 10: case 12:
                    soNgay = 31; break;
                case 4: case 6: case 9: case 11:
                    soNgay = 30; break;
                case 2:
                    soNgay = laNamNhuan ? 29 : 28; 
                    break;
            }

            var ketqua = $"Năm {Nam} {(laNamNhuan ? "là" : "không là")} năm nhuận. Tháng {Thang}/{Nam} có {soNgay} ngày";
            return ketqua;
            //return ngayCuoi2.ToString();
        }

    // host/Demo/CongHaiSo?a=1&b=7
    // host/Demo/CongHaiSo?a=1.1&b=abc
    public IActionResult CongHaiSo(string a, string b)
    {
        try
        {
            var soThuNhat = double.Parse(a);
            var soThuHai = double.Parse(b);
            return Content((soThuNhat + soThuHai).ToString());
        }
        catch (Exception ex)
        {
            return Json(new
            {
                Success = false
            });
        }
    }

    // host/Demo/TinhTuoi?ngaySinh=11/30/2000
    // host/Demo/TinhTuoi?ngaySinh=2000-11-30
    public IActionResult TinhTuoi(string ngaySinh)
    {
        if (DateTime.TryParse(ngaySinh, out DateTime Ngay))
        {
            int Tuoi = DateTime.Now.Year - Ngay.Year;
            return Content($"Sinh {ngaySinh} ==> {Tuoi} tuổi.");
        }
        else
        {
            return Content($"Chuỗi {ngaySinh} không hợp lệ");
        }
    }

    public IActionResult Index()
    {
        return View();
    }
}
}