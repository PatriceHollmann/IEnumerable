using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enumerable
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
                            Console.WriteLine(element);
                            Console.ReadKey();
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

    //public class Selection
    //{
    //    int Value { get; set; }
    //    public static IEnumerable<Selection> Sel(string path1, string path2)
    //    {
    //        using (StreamReader sr = new StreamReader(path1))
    //        {
    //            List<Selection> numbers = new List<Selection>();
    //            Selection sel = new Selection();
    //            while (sr.ReadLine() != null)
    //            {
    //                Int32.TryParse(sr.ReadLine(), out var x);
    //                //sel.Value = (T)Convert.ChangeType(sr.ReadLine(), typeof(T));
    //                sel.Value =  x ;
    //                //sel.Value = Convert.ChangeType((object)x, typeof(T));
    //                //sel.Value= (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromInvariantString(x);
    //                numbers.Add(sel);
    //                if (numbers.Count == 10)
    //                {
    //                    foreach (var element in numbers)
    //                    {
    //                        Console.WriteLine(element);
    //                        Console.ReadKey();
    //                        using (StreamWriter sw = new StreamWriter(path2, true))
    //                        {
    //                            sw.WriteLine(element);
    //                        }
    //                        yield return sel;
    //                        numbers = new List<Selection>();
    //                    }
    //                }
    //            }
    //            if (numbers.Count > 0)
    //            {
    //                foreach (var element in numbers)
    //                {
    //                    using (StreamWriter sw = new StreamWriter(path2, true))
    //                    {
    //                        sw.WriteLine(element);
    //                    }
    //                    yield return sel;
    //                    numbers = new List<Selection>();
    //                }
    //            }
    //        }
    //    }
    //}

    public static class Selection
    {
        public static IEnumerable<T> Sel<T>(this IEnumerable<T> Container, string path1, string path2)
        {
            int Count = 0;
            //List<T> numbers = new List<T>();
            //var num = new Selection();
            using (StreamReader sr = new StreamReader(path1))
            {
                while (sr.ReadLine() != null)
                {
                    var x = sr.ReadLine();

                    foreach (T Element in Container)
                    {
                        if (Count <= 10)
                        {
                            Count++;
                            yield return Element;
                        }

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
            string string2 = @"E:\Документы\massII.txt";
            Selection<int>.Sel(string1, string2);

        }
    }
}


