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
            result = Regex.Replace(result, @"[éè]", "e");
            result = Regex.Replace(result, @"[ưứừ]", "u");
            result = Regex.Replace(result, @"\s+", @"\s");
            result = Regex.Replace(result, @"[^a-z0-9]", "-");

            return result;
        }
    }
}
