# AspCore March 2021
Source code khóa học ASP.NET Core khai giảng 20/03/2021

* Chọn từng branch để có source code học từng buổi

## 1. Enable Razor Runtime
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
