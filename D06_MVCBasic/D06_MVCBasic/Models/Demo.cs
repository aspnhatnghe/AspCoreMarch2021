using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace D06_MVCBasic.Models
{
    public class Demo
    {
        public static int DemoA()
        {
            Thread.Sleep(2000);
            return new Random().Next();
        }
        public static string DemoB()
        {
            Thread.Sleep(5000);
            return "Nhất Nghệ";
        }
        public static void DemoC()
        {
            Thread.Sleep(3000);
        }

        public static async Task<int> DemoAAsync()
        {
            await Task.Delay(2000);
            return new Random().Next();
        }
        public static async Task<string> DemoBAsync()
        {
            await Task.Delay(5000);
            return "Nhất Nghệ";
        }
        public static async Task DemoCAsync()
        {
            await Task.Delay(3000);
        }
    }
}
