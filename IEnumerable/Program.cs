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
        static Random random = new Random();
        static IEnumerable<int> randomItems()
        {
            return Enumerable.Range(0, 10).Select(a=> random.Next(1,100));
        }
        static void Main(string[] args)
        {
            List<double> Massive1 = new List<double>(){ 54, 32, 32, 12, 43, 1, 8, 3, 1 };
            foreach (var item in Massive1.ToArrays(2))
            {
                Console.WriteLine(item.ToDisplay());
            }
            var (en1, en2) = randomItems().ToList().SplitToTwo();
            Console.WriteLine(en1.ToDisplay());
            Console.WriteLine(en2.ToDisplay());
            Console.ReadKey();
            string string1 = @"mass.txt";
            FileEnumenator.WriteToFile(string1, 5, Enumerable.Range(5, 500).Select(a => new DataItem { Text = $"I am data {a}" }).Select(JsonConvert.SerializeObject));
            string string2 = @"massII.txt";
            FileEnumenator.File(string1, 5)
                .DeserealizeJson<DataItem>()
                .ForEach(a => a.Text += "end of line ")
                .Where(a => !a.Text.Contains("9"))
                .SerializeJson()
                .WriteToFile(string2, 5);
            Console.WriteLine("It's over!");
            Console.ReadKey();
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
        }
    }
}


