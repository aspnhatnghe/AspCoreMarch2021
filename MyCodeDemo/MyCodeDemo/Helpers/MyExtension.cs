using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCodeDemo.Helpers
{
    public static class MyExtension
    {
        public static string ToVnd(this double gia)
        {
            return gia.ToString("#,##0.00") + " đ";
        }
    }
}
