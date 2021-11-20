using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace MyCodeDemo.Helpers
{
    public class MyTool
    {
        private static string FilePath = Path.Combine(Directory.GetCurrentDirectory(), "log.txt");
        public static void StoreLogToTextFile(string content)
        {
            using (var file = new StreamWriter(FilePath, true))
            {
                file.WriteLine(content);
                //file.Close();
            }
        }

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
    }
}
