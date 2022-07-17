using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task3_1
{
    public partial class Form1 : Form
    {
        private List<Record> arr = new List<Record>();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Автор: Создателев Александр\nГруппа: М8О-113М-17" +
                "\nОписание: программа поиска по ключу в самоорганизующемся файле");   //Описание
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Открыть файл ссылок, пролистать его и найти нужную запись по позиции
            try
            {
                int i, k, key;
                key = int.Parse(textBox1.Text);
                Record rec;
                string str, pos = "0", len = "0", File1 = /*"C:/Users/Lenovo/Documents/Список ссылок_1.txt"*/"C:/Users/Lenovo/Documents/Список ссылок_1_1.txt", val = "0";
                using (StreamReader sr = File.OpenText(File1))
                {
                    i = 0;
                    while ((str = sr.ReadLine()) != null)
                    {
                        string[] s1 = str.Split(' ');
                        k = int.Parse(s1[0]);
                        pos = s1[1];
                        len = s1[2];
                        //val = s1[1];
                        val = s1[3];

                        i++;
                        arr.Add(new Record(k, long.Parse(pos), int.Parse(len), val));
                        //arr.Add(new Record(k, val));
                    }
                }
                for (i = 0; i < arr.Count; i++)
                {
                    if (arr[i].Key == key)
                    {
                        rec = arr[i];
                        val = rec.Value;
                        arr.RemoveAt(i);
                        arr.Insert(0, rec);
                        break;
                    }
                }
                using (StreamWriter sr = File.CreateText(File1))
                {
                    for (i = 0; i < arr.Count; i++)
                    {
                        sr.WriteLine(arr[i].Key + " " + arr[i].Position + " " + arr[i].Length + " " + arr[i].Value);
                        //sr.WriteLine(arr[i].Key + "-" + arr[i].Value);
                    }
                }
                //File1 = "C:/Users/Lenovo/Documents/Список записей1_1.txt";
                File1 = "C:/Users/Lenovo/Documents/Список записей1_1_1.txt";
                using (FileStream fs = new FileStream(File1, FileMode.Open))
                {
                    byte[] buf1;
                    buf1 = new byte[int.Parse(len)];
                    fs.Position = long.Parse(pos);
                    fs.Read(buf1, 0, int.Parse(len));
                    label3.Text = Encoding.GetEncoding(1251).GetString(buf1);
                }
                /*using (StreamReader sr = File.OpenText(File1))
                {
                    i = 0;
                    while (((str = sr.ReadLine()) != null) && (i <= int.Parse(val)))
                    {
                        if (i == int.Parse(val))
                        {
                            label3.Text = str;
                        }
                        i++;
                    }
                }*/
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
    struct Record
    {
        private int key;
        private long position;
        private int byte_length;
        private string val;
        /*public Record(int k, string val)
        {
            key = k;
            this.val = val;
        }*/
        public Record(int k, long pos, int len, string val)
        {
            key = k;
            position = pos;
            byte_length = len;
            this.val = val;
        }
        public int Key { get { return key; } }
        public long Position { get { return position; } }
        public int Length { get { return byte_length; } }
        public string Value { get { return val; } }
    }
}
