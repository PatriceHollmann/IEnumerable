using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyIEnumerable
{
    public class FileEnumenator
    {
        public static IEnumerable<string> File(string path,ushort bufferSize=10)
        {
            if (bufferSize == 0)
            {
                bufferSize = 10;
            }
            List<string> strings = new List<string>();
            using (StreamReader sr = new StreamReader(path))
            {
                while (!sr.EndOfStream)
                {
                    while (strings.Count < bufferSize)
                    {
                        var line = sr.ReadLine();
                        if (line != null)
                        {
                            strings.Add(line);
                        }
                        else
                        {
                            break;
                        }
                    }
                    for (int i = 0; i < strings.Count; i++)
                    {
                        yield return strings[i];
                    }
                    strings.Clear();
                }
            }
        }
        public static void WriteToFile(string path, int bufferSize,IEnumerable<string> outEnumerable)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                List<string> list = new List<string>();
                foreach (var item in outEnumerable)
                {
                    list.Add(item);
                    if (list.Count >= bufferSize)
                    {
                        sw.WriteLines(list);
                        list.Clear();
                    }
                }
            }
        }
    }
    public static class StreamWriterUtil
    {
        public static void WriteLines(this StreamWriter sw,IEnumerable<string> lines)
        {
            foreach (var item in lines)
            {
                sw.WriteLine(item);
            }
        }
    }
}
