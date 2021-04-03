using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace D05_MVCBasic.Models
{
    public class Diem
    {
        //Fields
        private double x;
        double y;

        //Properties
        public double HoanhDo
        {
            get { return x; }
            set { x = value; }
        }

        public double TungDo { get => y; set => y = value; }
        //Automatic property
        public double CaoDo { get; set; }

        public override string ToString()
        {
            return $"({HoanhDo}, {TungDo}, {CaoDo})";
        }

        public double TinhKhoangCach(Diem diem)
        {
            var dx = Math.Pow(diem.x - this.x, 2);
            var dy = Math.Pow(diem.y - this.y, 2);
            return Math.Sqrt(dx + dy);
        }

        public string Xuat()
        {
            return $"({x}, {y}, {CaoDo})";
        }
    }
}
