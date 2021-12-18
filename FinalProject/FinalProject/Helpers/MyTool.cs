using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Helpers
{
    public class MyTool
    {
        public static string UploadFile(string folder, IFormFile file)
        {
            try
            {
                var fileName = $"{DateTime.UtcNow.Ticks}_{file.FileName}";
                var pathFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Hinh", folder, fileName);
                using (var newFile = new FileStream(pathFile, FileMode.Create))
                {
                    file.CopyTo(newFile);
                    return fileName;
                }
            }
            catch
            {
                return null;
            }
        }

        public static string GetRandom(int length = 5)
        {
            var pattern = @"1234567890qazwsxedcrfvtgbyhn@#$%";
            var rd = new Random();
            var sb = new StringBuilder();
            for (int i = 0; i < length; i++)
                sb.Append(pattern[rd.Next(0, pattern.Length)]);

            return sb.ToString();
        }
    }
}
