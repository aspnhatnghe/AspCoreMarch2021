using D10_Layout.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace D10_Layout.ViewComponents
{
    public class LoaiViewComponent : ViewComponent
    {
        public LoaiViewComponent()
        {

        }

        public IViewComponentResult Invoke()
        {
            //Lấy và xử lý data
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "Loai.json");
            var fileContent = File.ReadAllText(fullPath);

            var loaiData = JsonSerializer.Deserialize<List<Loai>>(fileContent);


            //trả về cho View hiển thị (default Default.cshtml)
            return View(loaiData);
        }
    }
}
