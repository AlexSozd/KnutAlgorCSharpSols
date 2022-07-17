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

namespace Task2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Автор: Создателев Александр\nГруппа: М8О-113М-17" +
                "\nОписание: программа заполнения файла ссылок для поиска в самоорганизующемся файле");   //Описание
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int i, k, key, len = 0;
                long pos = 0L;
                string str, val;
                string[] str1 = textBox1.Text.Trim(' ').Split(' ');
                key = int.Parse(str1[0]);
                val = str1[1];
                
                string /*pos = "0", len = "0",*/ File1 = "C:/Users/Lenovo/Documents/Список записей1.txt";
                using (StreamReader sr = File.OpenText(File1))
                {
                    i = 0;
                    while ((str = sr.ReadLine()) != null)
                    {
                        string[] s1 = str.Split(' ');
                        k = int.Parse(s1[0]);
                        if (k == key)
                        {
                            break;
                        }
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
                        for(i = 0; i < bstr.Length; i++)
                        {
                            if (bstr[i] == buf1[i])
                            {
                                j++;
                            }
                        }
                        if(j == bstr.Length)
                        {
                            //pos = (fs.Position - bstr.Length).ToString();
                            pos = fs.Position - (long)bstr.Length;
                            break;
                        }
                        j = 0;
                        fs.Position -= (bstr.Length - 1);
                    }
                    //fs.Position = long.Parse(pos);
                    //len = bstr1.Length.ToString();
                    len = bstr1.Length;
                    //fs.Read(buf1, 0, int.Parse(len));
                    //label3.Text = Encoding.GetEncoding(1251).GetString(buf1);
                    MessageBox.Show("All OK");
                }
                File1 = "C:/Users/Lenovo/Documents/Список ссылок.txt";
                using (StreamWriter sr = File.AppendText(File1))
                {
                    /*i = 0;
                    while ((str = sr.ReadLine()) != null)
                    {
                        string[] s1 = str.Split(' ');
                        k = int.Parse(s1[0]);
                        //buf = s1[1];
                        pos = s1[1];
                        len = s1[2];
                        if (k == key)
                        {
                            //label3.Text = buf;
                            break;
                        }
                        //buf = null;
                        i++;
                    }*/
                    sr.WriteLine(key + " " + pos + " " + len + " " + val);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
