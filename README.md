# AspCore March 2021
## Đồ án cuối khóa học ASP.NET Core

### Tech stack:
* .NET Core 3.1
* SQL Server
* Entity Framework Core - Code First

* Chọn từng branch để có source code từng buổi (MyProject_xx)

# Project_06 (09/01/2022)

# SERILOG

Cài thư viện:

- Serilog.AspNetCore version 4.1.0
- Serilog.Sinks.File version 5.0.0
- Serilog.Sinks.MSSqlServer version 5.6.1


## Serilog write to text file

Trong file ```Program.cs``, hàm ```Main()``` thêm dòng:

```
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console(new RenderedCompactJsonFormatter())
                .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
```


Trong file ```Program.cs``, hàm ```CreateHostBuilder()``` thêm method ```.UseSerilog()```:

```
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
```

Chỗ nào (class nào) muốn ghi log thì inject ILogger vào:
VD lớp UserController:

```
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }
    }
```

Trong lớp đó tùy level mà ghi log tương ứng:

```
_logger.LogInformation("Error when user register");
_logger.LogError(ex.Message);
```