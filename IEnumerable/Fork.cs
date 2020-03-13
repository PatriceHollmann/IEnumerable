using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fork
{
    public static class Fork
    {
        public static string ToDisplay<T>(this IEnumerable<T> its)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            var enr = its.GetEnumerator();
            var i = 0;
            while (enr.MoveNext())
            {
                T item = enr.Current;
                if(i!=0)
                sb.Append(",");

                sb.Append(item.ToString());
                i++;
            }
            sb.Append("]");
            return sb.ToString();
        }
        public static (IEnumerable<T>,IEnumerable<T>) SplitToTwo<T>(this IEnumerable<T> en)
        {
            return (en, en);
        }
        public static IEnumerable<T[]> ToArrays<T>(this IEnumerable<T> en,int chankSize)
        {
            List<T> list = new List<T>();
            foreach (var item in en)
            {
                list.Add(item);
                if (list.Count == chankSize)
                {
                    yield return list.ToArray();
                    list.Clear();
                }
            }
            if (list.Count > 0)
            {
                yield return list.ToArray();
                list.Clear();
            }
        }
        public static IEnumerable<double> SumMass(this  IEnumerable<double> masI, IEnumerable<double> masII)
        {
            var en1 = masI.GetEnumerator();
            var en2 = masII.GetEnumerator();
            var en1end = false;
            var en2end = false;
            while (!(en1end && en2end))
            {
                if (en1.MoveNext())
                {
                    yield return en1.Current;
                }
                if (en2.MoveNext())
                {
                    yield return en2.Current;
                }
            }
        }
  
    }
}
 