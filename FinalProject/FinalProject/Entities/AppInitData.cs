using FinalProject.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Entities
{
    public class AppInitData
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetService<NhatNgheDbContext>();

                //nếu chưa có Role thì tạo role default
                if (!dbContext.Roles.Any())
                {
                    dbContext.Roles.AddRange(
                        new Role { RoleName = MyConstants.Customer},
                        new Role { RoleName = MyConstants.Administartor },
                        new Role { RoleName = MyConstants.Accountant },
                        new Role { RoleName = MyConstants.Shipper }
                    );
                    dbContext.SaveChanges();
                }

                //nếu mà chưa có Category thì tạo default
                if (!dbContext.Categories.Any())
                {
                    dbContext.Categories.AddRange(
                       new Category
                       {
                           CategoryName = "Điện thoại",
                           SeoUrl = "dien-thoai"
                       },
                       new Category
                       {
                           CategoryName = "Máy tính bảng",
                           SeoUrl = "may-tinh-bang"
                       });
                    dbContext.SaveChanges();
                }
            }
        }
    }
}
