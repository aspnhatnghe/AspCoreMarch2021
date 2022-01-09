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


## 1. Serilog write to text file

Trong file ```Program.cs```, hàm ```Main()``` thêm dòng:

```
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console(new RenderedCompactJsonFormatter())
    .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
```


Trong file ```Program.cs```, hàm ```CreateHostBuilder()``` thêm method ```.UseSerilog()```:

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
_logger.LogError(ex.Message);
_logger.LogInformation("Infor log");
_logger.LogDebug("Debug logn DEMO");
_logger.LogWarning("Warning log");
_logger.LogCritical("Critical log");
```

## 2. Serilog write log to SQL Server

Bước 1: Định nghĩa Entity Log

Bước 2: Vào DbContext thêm DbSet<Log>

Bước 3: Add Migration

----
Setup config log ở file ```appsettings.json```

Bước 4: Mở file ```appsettings.json``` thêm:

```
{
  "Serilog": {
    "Using": "Serilog.Sinks.MSSqlServer",
    "MinimumLevel": {
      "Default": "Information",
      "Override": {
        "Microsoft": "Warning"
      }
    },
    "WriteTo": [
      {
        "Name": "MSSqlServer",
        "Args": {
          "connectionString": "Server=.; Database=NhatNgheCommerce; Integrated Security=True",
          "tableName":  "Logs"
        }
      }
    ]
  }
}
```

Bước 5: Chỉnh lại hàm ```Main()```

```
var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();
```

Bước 6: Ghi log như bình thường (inject và sử dụng)

======================================================

# GIỎ HÀNG

