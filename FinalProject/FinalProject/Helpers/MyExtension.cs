using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FinalProject.Helpers
{
    public static class MyExtension
    {
        //h--ng-sdan-ss--sd-ng
        public static string ToSeoUrl(this string keyword)
        {
            var result = (keyword ?? "").ToLower().Trim();

            //Lọc bỏ từ tiếng Việt
            result = Regex.Replace(result, @"[áàạảãâấầậẩẫăắằặẳẵ]", "a");
            result = Regex.Replace(result, @"[éèẹẻẽêếềệểễ]", "e");
            result = Regex.Replace(result, @"[óòọỏõôốồộổỗơớờợởỡ]", "o");
            result = Regex.Replace(result, @"[úùụủũưứừựửữ]", "u");
            result = Regex.Replace(result, @"[íìịỉĩ]", "i");
            result = Regex.Replace(result, @"đ", "d");
            result = Regex.Replace(result, @"[ýỳỵỷỹ]", "y");

            //thay thế theo chuẩn URL friendly
            result = Regex.Replace(result, @"[^a-z0-9\s-]", "");
            result = Regex.Replace(result, @"\s+", "-");
            result = Regex.Replace(result, @"\s", "-");
            result = Regex.Replace(result, @"(-)+", "-");

            return result;
        }

        public static string ToVnd(this double gia)
        {
            return gia.ToString("#,##0.00") + " đ";
        }
    }
}
