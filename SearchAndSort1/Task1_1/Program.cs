using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Task1;

namespace Task1_1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Сгенерировать рэндомом данные
            int i = 0, buf;
            Random rand = new Random();
            List<Record> geom = new List<Record>();
            while (i < 300)
            {
                int k = 0;
                buf = rand.Next(300) + 1;
                for (int j = 0; j < i; j++)
                {
                    if(buf == geom[j].Key)
                    {
                        k++;
                    }
                }
                if(k == 0)
                {
                    geom.Add(new Record(buf, Math.Pow(0.5, buf)));                       //geometric: p = q = 0,5
                    i++;
                }            
            }
            //Сохранить их в файл
            FileStream fs = new FileStream("C:/Users/Lenovo/Documents/GeomTestData", FileMode.OpenOrCreate);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, geom);
            fs.Close();

            List<Record> bin = new List<Record>();
            i = 0;
            while (i < 300)
            {
                int k = 0;
                buf = rand.Next(300) + 1;
                for (int j = 0; j < i; j++)
                {
                    if (buf == bin[j].Key)
                    {
                        k++;
                    }
                }
                if (k == 0)
                {
                    bin.Add(new Record(buf, BinomProbablity(buf, 300, 0.5, 0.5)));                         //binominal: p = q = 0,5
                    i++;
                }       
            }
            //Сохранить их в файл
            fs = new FileStream("C:/Users/Lenovo/Documents/BinomTestData", FileMode.OpenOrCreate);
            bf = new BinaryFormatter();
            bf.Serialize(fs, bin);
            fs.Close();

            List<Record> clin = new List<Record>();
            i = 0;
            double C = 2.00 / (300 * 299);
            while (i < 300)
            {
                int k = 0;
                buf = rand.Next(300) + 1;
                for (int j = 0; j < i; j++)
                {
                    if (buf == clin[j].Key)
                    {
                        k++;
                    }
                }
                if (k == 0)
                {
                    clin.Add(new Record(buf, (300 - buf) * C));         //clin
                    i++;
                }
            }
            //Сохранить их в файл
            fs = new FileStream("C:/Users/Lenovo/Documents/ClinTestData", FileMode.OpenOrCreate);
            bf = new BinaryFormatter();
            bf.Serialize(fs, clin);
            fs.Close();

            Console.WriteLine("All OK");
            Console.ReadKey();
        }
        private static double BinomProbablity(int n, int N, double p, double q)
        {
            int i;
            double C = 1.00, ber, buf = 1.00;
            for (i = 1; i <= n; i++)
            {
                if (i < N)
                {
                    buf = (N - i + 1);
                    buf = buf / i;
                    C = buf * C * p;
                }
                else
                {
                    C = 1.0000 * Math.Pow(p, N);
                }
                if (i <= N - n)
                {
                    C = C * q;
                }
            }
            ber = C;
            if (N - n > n)
            {
                for (i = n + 1; i <= N - n; i++)
                {
                    ber = ber * q;
                }
            }
            return ber;
        }
    }
}
