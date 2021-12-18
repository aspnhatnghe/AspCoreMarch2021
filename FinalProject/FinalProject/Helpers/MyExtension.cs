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

            result = Regex.Replace(result, @"[áàảãạăắằẳẵâấầẩẫậ]", "a");
            result = Regex.Replace(result, @"[éèêëę]", "e");
            result = Regex.Replace(result, @"[ìíîïı]", "i");
            result = Regex.Replace(result, @"[òóôõöøőð]", "o");
            result = Regex.Replace(result, @"[ưứừùúûüŭů]", "u");            
            result = Regex.Replace(result, @"[^a-z0-9\s-]", "");// Remove invalid characters for param
            result = Regex.Replace(result, @"\s+", @"-").Trim();

            return result;
        }

        public static string ToVnd(this double gia)
        {
            return gia.ToString("#,##0.00") + " đ";
        }
    }
}
