using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCodeDemo.ViewModels
{
    public class ApiResponseModel
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }

    public class ApiResponseModelWithData : ApiResponseModel
    {
        public object Data { get; set; }
    }
}
