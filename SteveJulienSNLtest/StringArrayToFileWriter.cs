using System;
using System.IO;


namespace SteveJulienSNLtest
{
    public static class StringArrayToFileWriter
    {
        public static void write(string path, string[] array)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
                File.WriteAllLines(path, array);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }

    
}
