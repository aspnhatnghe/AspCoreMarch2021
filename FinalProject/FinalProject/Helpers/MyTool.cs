using FinalProject.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Helpers
{
    public class MyTool
    {
        public static Permission CheckPermission(ClaimsPrincipal User, HttpRequest Request, NhatNgheDbContext _context)
        {
            var roleIds = User.Claims.Where(p => p.Type == ClaimTypes.Role).ToList();
            var roleIdInts = roleIds.Select(r => int.Parse(r.Value)).ToList();

            var url = Request.Path;

            var feature = _context.Features.ToList().SingleOrDefault(f => f.FeatureUrl == url);
            if (feature != null)
            {
                var permissions = _context.Permissions.Where(p => p.FeatureId == feature.FeatureId && roleIdInts.Contains(p.RoleId)).ToList();
                if (permissions.Count > 0)
                {
                    return new Permission
                    {
                        FeatureId = feature.FeatureId,
                        Access = permissions.Any(p => p.Access),
                        Modify = permissions.Any(p => p.Modify),
                        New = permissions.Any(p => p.New),
                        Remove = permissions.Any(p => p.Remove)
                    };
                }
                return null;
            }
            return null;
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
