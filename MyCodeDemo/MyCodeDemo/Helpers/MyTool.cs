using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace MyCodeDemo.Helpers
{
    public class MyTool
    {
        private static string FilePath = Path.Combine(Directory.GetCurrentDirectory(), "log.txt");
        public static void StoreLogToTextFile(string content)
        {
            using (var file = new StreamWriter(FilePath, true))
            {
                file.WriteLine(content);
                //file.Close();
            }
        }
    }
}
