using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Algorithm_T;

namespace TreeMaker
{
    class Program
    {
        static void Main(string[] args)
        {
            int len;
            long pos;
            MakeRef(42, out pos, out len);
            Record q = new Record(42, pos, len, "Низа_Крит", 10);
            //q.Beam = null;
            MakeRef(11, out pos, out len);
            q.AddRay(new Record(11, pos, len, "Внешность_Низы", 10));
            MakeRef(25, out pos, out len);
            q.AddRay(new Record(25, pos, len, "Эрг_Ноор", 10));
            MakeRef(37, out pos, out len);
            q.AddRay(new Record(37, pos, len, "Внешность_Ноора", 10));
            MakeRef(45, out pos, out len);
            q.AddRay(new Record(45, pos, len, "Любовь", 10));
            MakeRef(53, out pos, out len);
            q.AddRay(new Record(53, pos, len, "Биологи", 10));
            
            MakeRef(27, out pos, out len);
            q.Beam[1].AddRay(new Record(27, pos, len, "МНУ-11", 10));
            MakeRef(29, out pos, out len);
            q.Beam[1].AddRay(new Record(29, pos, len, "ЭВМ", 10));

            MakeRef(52, out pos, out len);
            q.Beam[4].AddRay(new Record(52, pos, len, "Дежурство", 10));
            MakeRef(55, out pos, out len);
            q.Beam[4].AddRay(new Record(55, pos, len, "Астрономы", 10));
            
            FileStream fs = new FileStream("C:/Users/Lenovo/Documents/RefNaryTree", FileMode.OpenOrCreate);
            BinaryFormatter bf = new BinaryFormatter();
            Algorithm_T.NaryTree bt = new Algorithm_T.NaryTree();
            bt.Root = q;
            bf.Serialize(fs, bt);
            fs.Close();

            Console.WriteLine("All OK");
            Console.ReadKey();
        }
        static void MakeRef(int key, out long pos, out int len)
        {
            pos = 0L;
            len = 0;

            int i, k/*, key*/;
            //key = int.Parse(textBox1.Text);
            string str, buf = null, File1;

            File1 = "C:/Users/Lenovo/Documents/Список записей1.txt";
            using (StreamReader sr = File.OpenText(File1))
            {
                i = 0;
                while ((str = sr.ReadLine()) != null)
                {
                    string[] s1 = str.Split(' ');
                    k = int.Parse(s1[0]);
                    for (int j = 1; j < s1.Length; j++)
                    {
                        buf = buf + s1[j] + " ";
                    }
                    if (k == key)
                    {
                        //label3.Text = buf;
                        break;
                    }
                    buf = null;
                    i++;
                }
            }
            using (FileStream fs = new FileStream(File1, FileMode.Open))
            {
                fs.Position = 0;
                byte[] bstr = Encoding.GetEncoding(1251).GetBytes(key.ToString());
                byte[] buf1;
                byte[] bstr1 = Encoding.GetEncoding(1251).GetBytes(str.ToString());
                buf1 = new byte[bstr.Length];
                int j = 0;
                while (fs.Read(buf1, 0, bstr.Length) > 0)
                {
                    for (i = 0; i < bstr.Length; i++)
                    {
                        if (bstr[i] == buf1[i])
                        {
                            j++;
                        }
                    }
                    if (j == bstr.Length)
                    {
                        pos = fs.Position - (long)bstr.Length;
                        break;
                    }
                    j = 0;
                    fs.Position -= (bstr.Length - 1);
                }
                len = bstr1.Length;
            }
            //MessageBox.Show("All OK");
        }
    }
}
