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
            Console.WriteLine("Enum step 1");
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
                        Console.WriteLine("Enum step 2");
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
                var enr = outEnumerable.GetEnumerator();
                /*while (enr.MoveNext())
                {
                    do
                    {
                        list.Add(enr.Current);
                    }
                    while (list.Count < bufferSize && enr.MoveNext());

                    sw.WriteLines(list);
                    list.Clear();
                }*/
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
