using MyIEnumerable;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEnumerable
{

    public class Selection<T>
    {
        T Value { get; set; }
        public static IEnumerable<Selection<T>> Sel(string path1, string path2)
        {
            using (StreamReader sr = new StreamReader(path1))
            {
                List<Selection<T>> numbers = new List<Selection<T>>();
                Selection<T> sel = new Selection<T>();
                while (sr.ReadLine() != null)
                {
                    //Int32.TryParse(sr.ReadLine(), out var x);
                    sel.Value = (T)Convert.ChangeType(sr.ReadLine(), typeof(T));
                    //Selection <T>sel = new Selection<T> { Value = x };
                    //sel.Value = Convert.ChangeType((object)x, typeof(T));
                    //sel.Value= (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromInvariantString(x);
                    numbers.Add(sel);
                    if (numbers.Count == 10)
                    {
                        foreach (var element in numbers)
                        {
                            //Console.WriteLine(element);
                            //Console.ReadKey();
                            using (StreamWriter sw = new StreamWriter(path2, true))
                            {
                                sw.WriteLine(element);
                            }
                            yield return sel;
                            numbers = new List<Selection<T>>();
                        }
                    }
                }
                if (numbers.Count > 0)
                {
                    foreach (var element in numbers)
                    {
                        using (StreamWriter sw = new StreamWriter(path2, true))
                        {
                            sw.WriteLine(element);
                        }
                        yield return sel;
                    }
                }
            }
        }
    }




    class Program
    {
        static void Main(string[] args)
        {
            Selection<int> sel = new Selection<int>();
            string string1 = @"E:\Документы\mass.txt";
            FileEnumenator.WriteToFile(string1,5, Enumerable.Range(5,500).Select(a => new DataItem { Text = $"I am data {a}" }).Select(JsonConvert.SerializeObject));
            string string2 = @"E:\Документы\massII.txt";

            FileEnumenator.File(string1, 5)
                .DeserealizeJson<DataItem>()
                .ForEach(a => a.Text += "end of line ")
                .Where(a=>!a.Text.Contains("9"))
                .SerializeJson()
                .WriteToFile(string2,5);
                   }
    }
    public class DataItem
    {
        [JsonProperty("text")]
        public string Text { get; set; }
    }
    public static class Method
    {
        public static IEnumerable<int> RangeAsync(int start, int count, int step)
        {
            for (int i = 0; i < count; i++)
            {
                yield return i * step + start;
            }
        }
        public static IEnumerable<int> Range(int start,int count,int step)
        {
            for (int i = 0; i < count; i++)
            {
                yield return i* step+ start;
            }
        }
        public static void WriteToFile(this IEnumerable<string> en,string path,int bufferSize)
        {
            FileEnumenator.WriteToFile(path, bufferSize, en);
        }
        public static IEnumerable<T> DeserealizeJson<T>(this IEnumerable<string> en)
        {
            return en.Select(a=>JsonConvert.DeserializeObject<T>(a));
        }
        public static IEnumerable<string> SerializeJson<T>(this IEnumerable<T> en) where T:class
        {
            return en.Select(JsonConvert.SerializeObject);
        }
        public static IEnumerable<T> ForEach<T>( this IEnumerable<T> en, Action <T> action)
        {
            foreach (var Element in en)
            {
                action(Element);
                yield return Element;
            }
            /*
            return en.Select(a =>
            {
                action(a);
                return a;
            });*/
        }
    }
}


