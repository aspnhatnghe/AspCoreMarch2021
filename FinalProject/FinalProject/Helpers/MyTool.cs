using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    }
}
