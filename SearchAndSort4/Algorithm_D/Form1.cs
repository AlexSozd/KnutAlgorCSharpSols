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

namespace Algorithm_D
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
                "\nОписание: программа цифрового поиска в бинарном дереве записей с ссылками");
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

                FileStream fs = new FileStream("C:/Users/Lenovo/Documents/RefBinaryTree1", FileMode.Open);
                BinaryFormatter bf = new BinaryFormatter();
                BinaryTree bt = (BinaryTree)bf.Deserialize(fs);
                Record p = bt.Root;
                fs.Close();
                if (p != null)
                {
                    //trview.Add("Текущее состояние дерева: ");
                    textBox2.Text += Environment.NewLine + " *****        Начальное состояние дерева:      ****** " + Environment.NewLine;
                    //ShowTree(bt.Root);
                    String[] trview1 = ShowTree1(bt.Root);
                    for (int i = 0; i < trview1.Length; i++)
                    {
                        //trview.Add(trview1[i]);
                        textBox2.Text += trview1[i].ToString() + Environment.NewLine;
                    }
                    textBox2.Text += Environment.NewLine;
                    //textBox2.Lines = trview.ToArray();
                    int k1 = Algor_D(p, key, val);
                    if (k1 > 0)
                    {
                        //label3.Text = p.Key + " " + p.Position + " " + p.Length + " " + p.Value;
                        //trview.Add("Новый вид дерева: ");
                        textBox2.Text += Environment.NewLine + " *****        Текущий вид дерева:      ****** " + Environment.NewLine;
                        //ShowTree(bt.Root);
                        trview1 = ShowTree1(bt.Root);
                        for (int i = 0; i < trview1.Length; i++)
                        {
                            //trview.Add(trview1[i]);
                            textBox2.Text += trview1[i].ToString() + Environment.NewLine;
                        }
                        textBox2.Text += Environment.NewLine;
                        //textBox2.Lines = trview.ToArray();
                        WriteInFile(bt);
                    }
                    /*else
                    {
                        label3.Text = p.Key + " " + p.Position + " " + p.Length + " " + p.Value;
                    }*/
                }
                else
                {
                    int len;
                    long pos;
                    MakeRef(key, out pos, out len);
                    Record q = new Record(key, pos, len, val);
                    q.Left = null;
                    q.Right = null;
                    p = q;
                    bt.Root = q;
                    //trview.Add("Новый вид дерева: ");
                    textBox2.Text += Environment.NewLine + " *****        Новый вид дерева:      ****** " + Environment.NewLine;
                    //ShowTree(bt.Root);
                    String[] trview1 = ShowTree1(bt.Root);
                    for (int i = 0; i < trview1.Length; i++)
                    {
                        //trview.Add(trview1[i]);
                        textBox2.Text += trview1[i].ToString() + Environment.NewLine;
                    }
                    textBox2.Text += Environment.NewLine;
                    //textBox2.Lines = trview.ToArray();
                    WriteInFile(bt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        protected int Algor_D(Record p, int k, string val)
        {
            int i = 0, k1 = 0, count = 0;
            while ((p.Key - Math.Pow(2, count)) > 0)
            {
                count++;
            }
            //count++;
            while (true)
            {
                if (k == p.Key)
                {
                    break;
                }
                else
                {
                    int b = k & (1 << (count - 1 - i));
                    b = b >> (count - 1 - i);
                    if(b == 0)
                    {
                        if(p.Left != null)
                        {
                            p = p.Left;
                        }
                        else
                        {
                            int len;
                            long pos;
                            MakeRef(k, out pos, out len);
                            Record q = new Record(k, pos, len, val);
                            q.Left = null;
                            q.Right = null;
                            p.Left = q;
                            p = p.Left;
                            k1++;
                            break;
                        }
                    }
                    else
                    {
                        if (p.Right != null)
                        {
                            p = p.Right;
                        }
                        else
                        {
                            int len;
                            long pos;
                            MakeRef(k, out pos, out len);
                            Record q = new Record(k, pos, len, val);
                            q.Left = null;
                            q.Right = null;
                            p.Right = q;
                            p = p.Right;
                            k1++;
                            break;
                        }
                    }
                }
                i++;
            }
            label3.Text = p.Key + " " + p.Position + " " + p.Length + " " + p.Value;
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
            if (rec.Right != null)
            {
                String[] r_tree = ShowTree1(rec.Right);
                for (int i = 0; i < r_tree.Length; i++)
                {
                    trview1.Add(" " + r_tree[i]);
                }
            }
            trview1.Add(rec.Key.ToString());
            if (rec.Left != null)
            {
                String[] l_tree = ShowTree1(rec.Left);
                for (int i = 0; i < l_tree.Length; i++)
                {
                    trview1.Add(" " + l_tree[i]);
                }
            }
            return trview1.ToArray();
        }
        protected void MakeRef(int key, out long pos, out int len)
        {
            pos = 0L;
            len = 0;

            int i, k;
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
        protected void WriteInFile(BinaryTree bt)
        {
            FileStream fs = new FileStream("C:/Users/Lenovo/Documents/RefBinaryTree1", FileMode.OpenOrCreate);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, bt);
            fs.Close();
        }
    }
    [Serializable] public class Record
    {
        private int key;
        private long position;
        private int byte_length;
        private string val;
        private Record left;
        private Record right;
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
        public Record Left { get { return left; } set { left = value; } }
        public Record Right { get { return right; } set { right = value; } }
    }
    [Serializable] public class BinaryTree
    {
        protected Record root;
        public BinaryTree()
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
    }
}
