﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fork
{
    public static class Fork
    {
    public static IEnumerable <double> SumMass (this IEnumerable <double> massSumm, IEnumerable <double> masI, IEnumerable<double> masII)
        {
            List<double> listSumm = new List<double>();
            List<double> listI = new List<double>();
            int k=0;
            foreach (var Element in masI)
            {
                listI.Add(Element);
            }
            List<double> listII = new List<double>();
            foreach (var Element in masII)
            {
                listII.Add(Element);
            }
            if (listI.Count<=listII.Count)
            {
                for (int i=0; i<listI.Count;i+=0)
                {
                    listSumm[k] = listI[i];
                    listI.RemoveAt(i);
                    yield return listSumm[k];
                    k++;
                    listSumm[k] = listII[i];
                    listII.RemoveAt(i);
                    yield return listSumm[k];
                    k++;
                }
                while (listII.Count != 0)
                {
                    for (int j = listII.Count; j < listI.Count; j += 0)
                    {
                        listSumm[k] = listII[j];
                        listII.RemoveAt(j);
                        yield return listSumm[k];
                        k++;
                    }
                }
            }
            if (listI.Count >= listII.Count)
            {
                for (int i = 0; i < listII.Count; i += 0)
                {
                    listSumm[k] = listI[i];
                    listI.RemoveAt(i);
                    yield return listSumm[k];
                    k++;
                    listSumm[k] = listII[i];
                    listII.RemoveAt(i);
                    yield return listSumm[k];
                    k++;
                }
                while (listI.Count != 0)
                {
                    for (int j = listI.Count; j < listI.Count; j += 0)
                    {
                        listSumm[k] = listI[j];
                        listI.RemoveAt(j);
                        yield return listSumm[k];
                        k++;
                    }
                }
            }
            else
            {

            }  
        }
       public static IEnumerable <double> SeparMass (this IEnumerable<double> MassSum, IEnumerable<double>MasI, IEnumerable<double>MasII, int countI,int countII)
        {
            int k = 0;
            List<double> masI = new List<double>();
            //foreach (var Element in MasI)
            //{
            //    masI.Add(Element);
            //}
            List<double> masII = new List<double>();
            //foreach (var Element in MasII)
            //{
            //    masII.Add(Element);
            //}
            List<double> masSum = new List<double>();
            foreach (var Element in MassSum)
            {
                masSum.Add(Element);
            }
            if (masSum.Count<(countI+countII))
            {
                Console.WriteLine("Суммарный массив содержит меньшее количество символов");
            }
            if (countI <= countII)
            {
                for (int i = 0; i < countI; i++)
                {
                    masI[i] = masSum[k];
                    masSum.RemoveAt(k);
                    yield return masI[i];
                    masII[i] = masSum[k];
                    masSum.RemoveAt(k);
                    yield return masII[i];
                }
            for (int j= countI;j<countII;j++)
                {
                    masII[j] = masSum[k];
                    masSum.RemoveAt(k);
                    yield return masII[j];
                }
                masSum.Clear();
            }
        }
    }
}
 