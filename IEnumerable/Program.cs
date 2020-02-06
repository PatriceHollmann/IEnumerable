using MyIEnumerable;
using Fork;
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
    class Program
    {
        static void Main(string[] args)
        {
            List<double> Massive1 = new List<double>() { 54, 32, 32, 12, 43, 1, 8, 3 };
            List<double> Massive2 = new List<double>() { 12, 3, 4, 98, 67, 34.5, 43, 65,89 };
            List<double> MassiveSumm = new List<double>();

            IEnumerable<double> MasSum = MassiveSumm.SumMass(Massive1, Massive2);
            Console.WriteLine("hey");
            Console.ReadLine();
            foreach (var item in MasSum)
            {
                //MassiveSumm.SumMass(Massive1, Massive2);
                Console.WriteLine(item);
            }
            Console.ReadKey();
            //MassiveSumm.
            //Selection<int> sel = new Selection<int>();
            //string string1 = @"E:\Документы\mass.txt";
            //FileEnumenator.WriteToFile(string1, 5, Enumerable.Range(5, 500).Select(a => new DataItem { Text = $"I am data {a}" }).Select(JsonConvert.SerializeObject));
            //string string2 = @"E:\Документы\massII.txt";

            //FileEnumenator.File(string1, 5)
            //    .DeserealizeJson<DataItem>()
            //    .ForEach(a => a.Text += "end of line ")
            //    .Where(a => !a.Text.Contains("9"))
            //    .SerializeJson()
            //    .WriteToFile(string2, 5);
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
        public static IEnumerable<int> Range(int start, int count, int step)
        {
            for (int i = 0; i < count; i++)
            {
                yield return i * step + start;
            }
        }
        public static void WriteToFile(this IEnumerable<string> en, string path, int bufferSize)
        {
            FileEnumenator.WriteToFile(path, bufferSize, en);
        }
        public static IEnumerable<T> DeserealizeJson<T>(this IEnumerable<string> en)
        {
            return en.Select(a => JsonConvert.DeserializeObject<T>(a));
        }
        public static IEnumerable<string> SerializeJson<T>(this IEnumerable<T> en) where T : class
        {
            return en.Select(JsonConvert.SerializeObject);
        }
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> en, Action<T> action)
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


