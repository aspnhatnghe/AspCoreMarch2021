using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCodeDemo.Controllers
{
    public class DemoLINQController : Controller
    {
        public IActionResult Index()
        {
            var data = new int[]
            {
                1, 9, 11, 34, 99, 8, 12, 75, 68, 15, 34
            };

            var sb = new StringBuilder();

            var dsChan = data.Where(item => item % 2 == 0);
            sb.AppendLine(string.Join(", ", dsChan));

            var gom = data.GroupBy(item => item % 3)
                .Select(item => new { 
                    ID = item.Key,
                    Count = item.Count(),
                    Sum = item.Sum(it => it)
                });
            sb.AppendLine(string.Join(", ", gom));

            var dsSap = data.OrderByDescending(item => item);
            sb.AppendLine(string.Join(", ", dsSap));

            var dsLay = data.Skip(3).Take(5);
            sb.AppendLine(string.Join(", ", dsLay)); //34, 99, 8, 12, 75

            return Content(sb.ToString());
        }
    }
}
