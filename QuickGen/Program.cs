using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace QuickGen
{
    class Program
    {
        static void Main(string[] args)
        {
            var linia = string.Empty;
            List<string> listaWynikow = new List<string>();
            var sciezkaPliku = @"C:\Users\Public\TestFolder\quickGen.txt";


            byte[] results = new byte[5 * 2118760];
            byte[] current = new byte[5];
            int offset = 0;
            var watch = Stopwatch.StartNew();
            Populate(results, ref offset, current, 0);
            watch.Stop();
            Console.WriteLine("Time to generate: {0}ms", watch.ElapsedMilliseconds);
            Console.WriteLine("Data size: {0}MiB",
                (results.Length * sizeof(byte)) / (1024 * 1024));
            Console.WriteLine("All generated; press any key to show them");
            for (int i = 0; i < 2118760; i++)
            {
                //linia = Format(results,i);
                listaWynikow.Add(Format(results, i));
                //Console.WriteLine(Format(results, i));
            }
            listaWynikow.Distinct();

            Console.WriteLine("Mam już wszystkie wyniki - zapisuję do pliku");
            //Console.ReadKey();
            File.WriteAllLines(sciezkaPliku, listaWynikow);


        }
        static string Format(byte[] results, int index)
        {
            int offset = 5 * index;
            return results[offset++] + "," + results[offset++] + "," +
                   results[offset++] + "," + results[offset++] + "," +
                   results[offset++];
            //results[offset++] + "," + results[offset++];
        }

        static void Populate(byte[] results, ref int offset, byte[] current, int level)
        {
            // pick a new candidate; note since we're doing C not P, assume ascending order
            int last = level == 0 ? 0 : current[level - 1];
            for (byte i = (byte)(last + 1); i <= 50; i++)
            {
                current[level] = i;
                if (level == 4)
                {
                    // write the results
                    results[offset++] = current[0];
                    results[offset++] = current[1];
                    results[offset++] = current[2];
                    results[offset++] = current[3];
                    results[offset++] = current[4];
                    //results[offset++] = current[5];
                }
                else
                {
                    // dive down
                    Populate(results, ref offset, current, level + 1);
                }
            }
        }
    }
}
    

