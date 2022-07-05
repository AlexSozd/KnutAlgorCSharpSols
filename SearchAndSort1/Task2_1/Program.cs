using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task2_1
{
    class Program
    {
        //static string KeyIndexFile = "C:/Users/Lenovo/Documents/Список ссылок_1.txt", KeyValueFile = "C:/Users/Lenovo/Documents/Список записей1_1.txt";
        static string KeyIndexFile = "C:/Users/Lenovo/Documents/Список ссылок_1_1.txt", KeyValueFile = "C:/Users/Lenovo/Documents/Список записей1_1_1.txt";
        static void Main(string[] args)
        {
            CreateFile();
            Console.ReadKey();
        }
        /*static async void CreateFile()
        {
            //int fileSize = 0;
            using (var stream1 = File.Open(KeyValueFile, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                using (var stream2 = File.Open(KeyIndexFile, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    using (var writer1 = new StreamWriter(stream1))
                    {
                        using (var writer2 = new StreamWriter(stream2))
                        {
                            long i = 0;
                            while (i < 500000)
                            {
                                await writer1.WriteLineAsync(string.Format("{0}-{0}{0}{0}{0}{0}{0}{0}{0}qwertyuiop[]asdfghjkl;'zxcvbnm,./{0}{0}{0}", i));
                                await writer2.WriteLineAsync(string.Format("{0}-{0}", i));
                                i++;
                            }
                        }
                    }
                }
            }
        }*/
        static void CreateFile()
        {
            long pos;
            byte[] buf;
            Random rand = new Random(5000);
            /*using (var stream1 = File.Open(KeyValueFile, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                using (var writer1 = new StreamWriter(stream1))
                {
                    long i = 0;
                    while (i < 5000000)
                    {
                        writer1.WriteLine(string.Format("{0}-{0}{0}{0}{0}{0}{0}{0}{0}qwertyuiop[]asdfghjkl;'zxcvbnm,./{0}{0}{0}", i));
                        i++;
                    }
                }
            }*/
            using (FileStream fs = new FileStream(KeyValueFile, FileMode.OpenOrCreate, FileAccess.ReadWrite)) //using (var stream1 = File.Open(KeyValueFile, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                using (var stream2 = File.Open(KeyIndexFile, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    using (var writer2 = new StreamWriter(stream2))
                    {
                        long i = 0;
                        while (i < 50000000)
                        {
                            buf = Encoding.GetEncoding(1251).GetBytes(string.Format("{0}-{0}{0}{0}{0}{0}{0}{0}{0}qwertyuiop[]asdfghjkl;'zxcvbnm,./{0}{0}{0}", i));
                            //fs.Position += buf.Length;
                            pos = fs.Position - buf.Length;
                            byte[] buf1 = new byte[buf.Length];
                            int j = 0;
                            while (fs.Read(buf1, 0, buf.Length) > 0)
                            {
                                for (i = 0; i < buf.Length; i++)
                                {
                                    if (buf[i] == buf1[i])
                                    {
                                        j++;
                                    }
                                }
                                if (j == buf.Length)
                                {
                                    pos = fs.Position - (long)buf.Length;
                                    break;
                                }
                                j = 0;
                                fs.Position -= (buf.Length - 1);
                            }
                            writer2.WriteLine(string.Format("{0} {1} {2} {3}", i, pos, buf.Length, ('A' + rand.Next(25)).ToString()));
                            i++;
                        }
                    }
                }
            }
        }
    }
}
