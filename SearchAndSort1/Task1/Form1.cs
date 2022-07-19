using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task1
{
    public partial class Form1 : Form
    {
        private Record[] geom1, geom2, bin1, bin2, clin1, clin2;
        public Form1()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex <= 0)
            {
                MessageBox.Show("Вы не выбрали распределение!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Автор: Создателев Александр\nГруппа: М8О-113М-17" +
                "\nОписание: программа поиска по ключу с известным распределением" + 
                "\nвероятности запроса в упорядоченном и неупорядоченном масиве");   //Описание
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                FillIn();
                int key, count1 = 0, count2 = 0;
                key = int.Parse(textBox1.Text);
                if (comboBox1.SelectedItem.ToString() == "Геометрическое")
                {
                    for (int i = 0; i < 300; i++)
                    {
                        if (geom1[i].Key == key)
                        {
                            count1 = i + 1;
                            break;
                        }
                    }
                    for (int i = 0; i < 300; i++)
                    {
                        if (geom2[i].Key == key)
                        {
                            count2 = i + 1;
                            break;
                        }
                    }
                }
                else if (comboBox1.SelectedItem.ToString() == "Биномиальное")
                {
                    for (int i = 0; i < 300; i++)
                    {
                        if (bin1[i].Key == key)
                        {
                            count1 = i + 1;
                            break;
                        }
                    }
                    for (int i = 0; i < 300; i++)
                    {
                        if (bin2[i].Key == key)
                        {
                            count2 = i + 1;
                            break;
                        }
                    }
                }
                else if (comboBox1.SelectedItem.ToString() == "Клиновидное")
                {
                    for (int i = 0; i < 300; i++)
                    {
                        if (clin1[i].Key == key)
                        {
                            count1 = i + 1;
                            break;
                        }
                    }
                    for (int i = 0; i < 300; i++)
                    {
                        if (clin2[i].Key == key)
                        {
                            count2 = i + 1;
                            break;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Вы не выбрали распределение!");
                }
                label5.Text = count1.ToString();
                label6.Text = count2.ToString();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /*private void FillIn()
        {
            double C = 1.00, ber = 0.00/*, buf = 1.00;
            geom1 = new Record[300];
            for (int i = 0; i < 300; i++)
            {
                geom1[i] = new Record(i + 1, Math.Pow(0.5, i + 1));   //geometric: p = q = 0,5
            }
            geom2 = new Record[300];
            for (int i = 0; i < 300; i++)
            {
                geom2[i] = new Record(i + 1, Math.Pow(0.5, i + 1));   //geometric: p = q = 0,5
            }
            ShakerSort(geom2);
            bin1 = new Record[300];
            for (int i = 0; i < 300; i++)
            {
                //bin1[i] = new Record(i + 1, Math.Pow(0.5, i + 1));   //binominal: p = q = 0,5
                ber = BinomProbablity(i + 1, 300, 0.5, 0.5);
                bin1[i] = new Record(i + 1, ber);   //binominal: p = q = 0,5
            }
            bin2 = new Record[300];
            for (int i = 0; i < 300; i++)
            {
                //bin2[i] = new Record(i + 1, Math.Pow(0.5, i + 1));   //binominal: p = q = 0,5
                /*for (int j = 1; j <= (i + 1); j++)
                {
                    if (j < 300)
                    {
                        buf = (300 - j + 1);
                        buf = buf / j;
                        C = buf * C * 0.5;
                    }
                    else
                    {
                        C = 1.0000 * Math.Pow(0.5, 300);
                    }
                    if (j <= (300 - i - 1))
                    {
                        C = C * 0.5;
                    }
                }
                ber = C;
                if ((300 - i - 1) > (i + 1))
                {
                    for (int j = i + 2; j <= 300 - i; j++)
                    {
                        ber = ber * 0.5;
                    }
                }
                ber = BinomProbablity(i + 1, 300, 0.5, 0.5);
                bin2[i] = new Record(i + 1, ber);   //binominal: p = q = 0,5
                //buf = 1.00;
                //C = 1.00;
            }
            ShakerSort(bin2);
            C = 2.00 / (300 * 299);
            clin1 = new Record[300];
            for (int i = 0; i < 300; i++)
            {
                clin1[i] = new Record(i + 1, (300 - (i + 1)) * C);   //clin
            }
            clin2 = new Record[300];
            for (int i = 0; i < 300; i++)
            {
                clin2[i] = new Record(i + 1, (300 - (i + 1)) * C);   //clin
            }
            ShakerSort(clin2);
        }*/
        private void FillIn()
        {
            //geom1 = new Record[300];
            FileStream fs = new FileStream("C:/Users/Lenovo/Documents/GeomTestData", FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();
            geom1 = ((List<Record>)bf.Deserialize(fs)).ToArray();
            fs.Close();
            
            geom2 = new Record[300];
            for (int i = 0; i < 300; i++)
            {
                //geom2[i] = new Record(i + 1, Math.Pow(0.5, i + 1));   //geometric: p = q = 0,5
                geom2[i] = geom1[i];
            }
            ShakerSort(geom2);
            //bin1 = new Record[300];
            fs = new FileStream("C:/Users/Lenovo/Documents/BinomTestData", FileMode.Open);
            bf = new BinaryFormatter();
            bin1 = ((List<Record>)bf.Deserialize(fs)).ToArray();
            fs.Close();

            bin2 = new Record[300];
            for (int i = 0; i < 300; i++)
            {
                bin2[i] = bin1[i];
            }
            ShakerSort(bin2);
            //C = 2.00 / (300 * 299);
            //clin1 = new Record[300];
            fs = new FileStream("C:/Users/Lenovo/Documents/ClinTestData", FileMode.Open);
            bf = new BinaryFormatter();
            clin1 = ((List<Record>)bf.Deserialize(fs)).ToArray();
            fs.Close();
            clin2 = new Record[300];
            for (int i = 0; i < 300; i++)
            {
                clin2[i] = clin1[i];
            }
            ShakerSort(clin2);
        }
        private void ShakerSort(Record[] arr)                    //Шейкер-сортировка
        {
            int down = 0, n = arr.Length, up = n - 1, t = n - 1;
            Record[] arr1 = new Record[n];
            while (down < up)
            {
                for (int j = down; j < up; j++)
                {
                    if (arr[j].Probability < arr[j + 1].Probability)
                    {
                        t = j;
                        /*K = arr[j].Key;
                        strcpy(R, arr[j].Probability);
                        arr[j].Key = arr[j + 1].Key;
                        strcpy(arr[j].Probability, arr[j + 1].Probability);
                        arr[j + 1].Key = K;
                        strcpy(arr[j + 1].Probability, R);*/
                        Record buf = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = buf;
                    }
                }
                up = t;
                for (int i = up; i > down; i--)
                {
                    if (arr[i].Probability > arr[i - 1].Probability)
                    {
                        t = i;
                        /*K = arr[i].Key;
                        strcpy(R, arr[i].Probability);
                        arr[i].Key = arr[i - 1].Key;
                        strcpy(arr[i].Probability, arr[i - 1].Probability);
                        arr[i - 1].Key = K;
                        strcpy(arr[i - 1].Probability, R);*/
                        Record buf = arr[i];
                        arr[i] = arr[i - 1];
                        arr[i - 1] = buf;
                    }
                }
                down = t;
            }
            /*for(int i = 0; i < n; i++)
            {
                arr1[n - i - 1] = arr[i];
            }
            arr = arr1;*/
        }
        private double BinomProbablity(int n, int N, double p, double q)
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
    [Serializable] public struct Record
    {
        private int key;
        private double exp_value;
        public Record(int k, double val)
        {
            key = k;
            exp_value = val;
        }
        public int Key { get { return key; } }
        public double Probability { get { return exp_value; } }
    }
}
