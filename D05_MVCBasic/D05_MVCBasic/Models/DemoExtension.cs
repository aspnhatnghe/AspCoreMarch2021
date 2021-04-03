
using System;

namespace D05_MVCBasic.Models
{
    public static class DemoExtension
    {
        //Thêm vào lớp int hàm tính tổng số lẻ
        public static int TinhTongSoLe(this int N)
        {
            var s = 0;
            for(var i = 1; i <= N; i++)
            {
                if(i % 2 == 1)
                {
                    s += i;
                }
            }
            return s;
        }

        //Thêm hàm tính khoảng cách ngày ngày vào lớp DateTime
        public static int KhoangCachNgay(this DateTime ngay1, DateTime ngay2)
        {
            //Gọi d1.KhoangCachNgay(d2)
            return (ngay2 - ngay1).Days;
        }
    }
}
