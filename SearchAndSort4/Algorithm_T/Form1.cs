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

namespace Algorithm_T
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
                "\nОписание: программа цифрового поиска в n-арном дереве записей с ссылками");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int key;
                string val;
                //key = int.Parse(textBox1.Text);
                string[] str1 = textBox1.Text.Trim(' ').Split(' ');
                key = int.Parse(str1[0]);
                val = str1[1];

                FileStream fs = new FileStream("C:/Users/Lenovo/Documents/RefNaryTree", FileMode.Open);
                BinaryFormatter bf = new BinaryFormatter();
                NaryTree bt = (NaryTree)bf.Deserialize(fs);
                Record p = bt.Root;
                fs.Close();
                if (p != null)
                {
                    //trview.Add("Текущее состояние дерева: ");
                    textBox2.Text += Environment.NewLine + " *****        Дерево:      ****** " + Environment.NewLine;
                    //ShowTree(bt.Root);
                    String[] trview1 = ShowTree1(bt.Root);
                    for (int i = 0; i < trview1.Length; i++)
                    {
                        //trview.Add(trview1[i]);
                        textBox2.Text += trview1[i].ToString() + Environment.NewLine;
                    }
                    textBox2.Text += Environment.NewLine;
                    //textBox2.Lines = trview.ToArray();
                    Algor_T(p, key, val);
                    /*int k1 = Algor_T(p, key, val);
                    if (k1 > 0)
                    {
                        label3.Text = p.Key + " " + p.Position + " " + p.Length + " " + p.Value;
                        //trview.Add("Новый вид дерева: ");
                        /*textBox2.Text += Environment.NewLine + " *****        Новый вид дерева:      ****** " + Environment.NewLine;
                        //ShowTree(bt.Root);
                        trview1 = ShowTree1(bt.Root);
                        for (int i = 0; i < trview1.Length; i++)
                        {
                            //trview.Add(trview1[i]);
                            textBox2.Text += trview1[i].ToString() + Environment.NewLine;
                        }
                        textBox2.Text += Environment.NewLine;
                        //textBox2.Lines = trview.ToArray();
                    }
                    else
                    {
                        label3.Text = "Not found";
                    }*/
                }
                /*else
                {
                    int len;
                    long pos;
                    MakeRef(key, out pos, out len);
                    Record q = new Record(key, pos, len, val, key.ToString().Length);
                    q.Beam = null;
                    p = q;
                    bt.Root = q;
                    //trview.Add("Новый вид дерева: ");
                    //textBox2.Text += Environment.NewLine + " *****        Новый вид дерева:      ****** " + Environment.NewLine;
                    //ShowTree(bt.Root);
                    /*String[] trview1 = ShowTree1(bt.Root);
                    for (int i = 0; i < trview1.Length; i++)
                    {
                        //trview.Add(trview1[i]);
                        textBox2.Text += trview1[i].ToString() + Environment.NewLine;
                    }
                    textBox2.Text += Environment.NewLine;
                    //textBox2.Lines = trview.ToArray();
                    WriteInFile(bt);
                }*/
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        protected int Algor_T(Record p, int k, string val)
        {
            int i = 0, k1 = 0;
            while ((i < p.Beam.Capacity) || (i < p.Beam.Count))
            {
                if (k == p.Key)
                {
                    label3.Text = p.Key + " " + p.Position + " " + p.Length + " " + p.Value;
                    k1++;
                    break;
                }
                else
                {
                    int k2 = 0;
                    for(int j = 0; j < p.Beam.Count; j++)
                    {
                        if (p.Beam[j].Key.ToString()[i] == k.ToString()[i])
                        {
                            p = p.Beam[j];
                            k2++;
                            break;
                        }
                    }
                    if(k2 == 0)
                    {
                        p = null;
                    }
                }
                if(p == null)
                {
                    label3.Text = "Not found";
                    break;
                }
                i++;
            }
            return k1;
        }
        /*protected void ShowTree(Record rec)
        {
            trview.Add(" " + rec.Key);
            if (rec.Right != null)
            {
                ShowTree(rec.Right);
            }
            if (rec.Left != null)
            {
                ShowTree(rec.Left);
            }
        }*/
        protected string[] ShowTree1(Record rec)
        {
            List<string> trview1 = new List<string>();
            trview1.Add(rec.Key.ToString());
            for (int i = 0; i < rec.Beam.Count; i++)
            {
                String[] r_tree = ShowTree1(rec.Beam[i]);
                for (int j = 0; j < r_tree.Length; j++)
                {
                    trview1.Add(" " + r_tree[j]);
                }
            }
            return trview1.ToArray();
        }
        /*protected void MakeRef(int key, out long pos, out int len)
        {
            pos = 0L;
            len = 0;

            int i, k/*, key;
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
        protected void WriteInFile(NaryTree bt)
        {
            FileStream fs = new FileStream("C:/Users/Lenovo/Documents/RefNaryTree", FileMode.OpenOrCreate);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, bt);
            fs.Close();
        }*/
    }
    [Serializable] public class Record
    {
        private int key;
        private long position;
        private int byte_length;
        private string val;
        private List<Record> beam;
        public Record(int k, long pos, int len, string val, int b_count)
        {
            key = k;
            position = pos;
            byte_length = len;
            this.val = val;
            beam = new List<Record>(b_count);
        }
        public void AddRay(Record rec)
        {
            beam.Add(rec);
        }
        public int Key { get { return key; } }
        public long Position { get { return position; } }
        public int Length { get { return byte_length; } }
        public string Value { get { return val; } }
        public List<Record> Beam { get { return beam; } set { beam = value; } }
    }
    [Serializable] public class NaryTree
    {
        protected int beamcount;
        protected Record root;
        public NaryTree()
        {
            root = null;
        }
        public void Clear()
        {
            root = null;
        }
        public Record Root
        {
            get
            {
                return root;
            }
            set
            {
                root = value;
            }
        }
        public int BeamCount { get { return beamcount; } }
    }
}
