# AspCore March 2021
## Đồ án cuối khóa học ASP.NET Core

### Tech stack:
* .NET Core 3.1
* SQL Server
* Entity Framework Core - Code First

* Chọn từng branch để có source code từng buổi (MyProject_xx)

## 1. Enable Razor Runtime (sửa file cshtml ko cần stop)
### 1.1 Install Package
```
Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation
```
### 1.2 Update ConfigureServices() method
Update the project's Startup.ConfigureServices method to include a call to **AddRazorRuntimeCompilation()**.
```
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
        }
```

Ref on [MS Document](https://docs.microsoft.com/en-us/aspnet/core/mvc/views/view-compilation?view=aspnetcore-3.1&tabs=visual-studio)
