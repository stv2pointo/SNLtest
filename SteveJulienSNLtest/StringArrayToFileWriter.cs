using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteveJulienSNLtest
{
    public static class StringArrayToFileWriter
    {
        public static void write(string path, string[] array)
        {
            try
            {
                System.IO.File.WriteAllLines(path, array);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
