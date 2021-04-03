using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace D05_MVCBasic.Models
{
    public class HinhHoc
    {
        public double DienTich { get; set; }
        public double ChuVi { get; set; }
        public virtual void TinhDienTichChuVi() { }
    }

    public class HinhChuNhat : HinhHoc
    {
        public double Dai { get; set; }
        public double Rong { get; set; }
        public override void TinhDienTichChuVi()
        {
            DienTich = Dai * Rong;
            ChuVi = (Dai + Rong) * 2;
        }
        public override string ToString()
        {
            return $"D={Dai}, R = {Rong}, S = {DienTich}, P = {ChuVi}";
        }
    }
    public class HinhTron : HinhHoc
    {
        public double BanKinh { get; set; }
        public override void TinhDienTichChuVi()
        {
            DienTich = BanKinh * BanKinh * Math.PI;
            ChuVi = 2 * BanKinh * Math.PI;
        }
        public override string ToString()
        {
            return $"R = {BanKinh}, S = {DienTich}, P = {ChuVi}";
        }
    }
}
