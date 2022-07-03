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
using Algorithm_T;

namespace Algorithm_D
{
    public partial class Form1 : Form
    {
        List<string> trview = new List<string>();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Автор: Создателев Александр\nГруппа: М8О-113М-17" +
                "\nОписание: программа удаления узла бинарного дерева записей с ссылками");
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
                //Record T;
                FileStream fs = new FileStream("C:/Users/Lenovo/Documents/RefBinaryTree", FileMode.Open);
                BinaryFormatter bf = new BinaryFormatter();
                BinaryTree bt = (BinaryTree)bf.Deserialize(fs);
                Record /*q,*/ p = bt.Root;
                
                fs.Close();

                //Algorithm_T.Form1 f1 = new Algorithm_T.Form1();
                //f1.Algor_T(q, key, val);
                //trview.Add("Текущее состояние дерева: ");
                textBox2.Text += Environment.NewLine + " *****        Текущее состояние дерева:      ****** " + Environment.NewLine;
                //ShowTree(bt.Root);
                String[] trview1 = ShowTree1(bt.Root);
                for (int i = 0; i < trview1.Length; i++)
                {
                    //trview.Add(trview1[i]);
                    textBox2.Text += trview1[i].ToString() + Environment.NewLine;
                }
                textBox2.Text += Environment.NewLine;
                //textBox2.Lines = trview.ToArray();
                //int k1 = Algor_T1(p, key, val);
                //int k1 = Algor_T1(bt, key, val);
                Algor_T1(bt, key, val);
                /*if(k1 > 0)
                {
                    //trview.Add("Новый вид дерева: ");
                    textBox2.Text += Environment.NewLine + " *****        Новый вид дерева:      ****** " + Environment.NewLine;
                    //ShowTree(bt.Root);
                    trview1 = ShowTree1(bt.Root);
                    for (int i = 0; i < trview1.Length; i++)
                    {
                        //trview.Add(trview1[i]);
                        textBox2.Text += trview1[i].ToString() + Environment.NewLine;
                    }
                    textBox2.Text += Environment.NewLine;
                    //textBox2.Lines = trview.ToArray();
                    //WriteInFile(bt);
                }*/
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        protected void Algor_D(Record q, out Record T)
        {
            T = q;
            if(T.Right == null)
            {
                q = T.Left;
            }
            else
            {
                Record r = T.Right;
                if(r.Left == null)
                {
                    r.Left = T.Left;
                    q = r;
                }
                else
                {
                    Record s = r.Left;
                    while(s.Left != null)
                    {
                        r = s;
                        s = r.Left;
                    }
                    s.Left = T.Left;
                    r.Left = s.Right;
                    s.Right = T.Right;
                    q = s;
                }
            }
        }
        protected void Algor_D(BinaryTree bt, Record q1, Record q, out Record T)
        {
            int a = 0;
            T = q;
            if(q1 != null)
            {
                if (q1.Left == q)
                {
                    a = -1;
                }
                if (q1.Right == q)
                {
                    a = 1;
                }
            }
            if (T.Right == null)
            {
                q = T.Left;
            }
            else
            {
                Record r = T.Right;
                if (r.Left == null)
                {
                    r.Left = T.Left;
                    q = r;
                }
                else
                {
                    Record s = r.Left;
                    while (s.Left != null)
                    {
                        r = s;
                        s = r.Left;
                    }
                    s.Left = T.Left;
                    r.Left = s.Right;
                    s.Right = T.Right;
                    q = s;
                }
            }
            if (a == -1)
            {
                q1.Left = q;
            }
            else if(a == 1)
            {
                q1.Right = q;
            }
            else
            {
                bt.Root = q;
            }
            WriteInFile(bt);
        }
        public int Algor_T1(Record p, int k, string val)
        {
            int k1 = 0;
            while (true)
            {
                if (k == p.Key)
                {
                    break;
                }
                else if (k < p.Key)
                {
                    if (p.Left != null)
                    {
                        p = p.Left;
                    }
                    else
                    {
                        Record q = null;
                        p = q;
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
                        Record q = null;
                        p = q;
                        break;
                    }
                }
            }
            if (p != null)
            {
                Record T;
                Algor_D(p, out T);
                label3.Text = T.Key + " " + T.Position + " " + T.Length + " " + T.Value;
                k1++;
            }
            else
            {
                label3.Text = "Not found.";
            }
            return k1;
        }
        public int Algor_T1(BinaryTree bt, int k, string val)
        {
            int k1 = 0;
            Record p = bt.Root;
            Record p1 = null;
            while (true)
            {
                if (k == p.Key)
                {
                    break;
                }
                else if (k < p.Key)
                {
                    if (p.Left != null)
                    {
                        p1 = p;
                        p = p.Left;
                    }
                    else
                    {
                        Record q = null;
                        p1 = p;
                        p = q;
                        break;
                    }
                }
                else
                {
                    if (p.Right != null)
                    {
                        p1 = p;
                        p = p.Right;
                    }
                    else
                    {
                        Record q = null;
                        p1 = p;
                        p = q;
                        break;
                    }
                }
            }
            if (p != null)
            {
                Record T;
                Algor_D(bt, p1, p, out T);
                label3.Text = T.Key + " " + T.Position + " " + T.Length + " " + T.Value;
                k1++;
                //WriteInFile(bt);
                textBox2.Text += Environment.NewLine + " *****        Новый вид дерева:      ****** " + Environment.NewLine;
                String[] trview1 = ShowTree1(bt.Root);
                for (int i = 0; i < trview1.Length; i++)
                {
                    textBox2.Text += trview1[i].ToString() + Environment.NewLine;
                }
                textBox2.Text += Environment.NewLine;
            }
            else
            {
                label3.Text = "Not found.";
            }
            return k1;
        }
        protected void ShowTree(Record rec)
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
        }
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
        protected void WriteInFile(BinaryTree bt)
        {
            FileStream fs = new FileStream("C:/Users/Lenovo/Documents/RefBinaryTree", FileMode.OpenOrCreate);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, bt);
            fs.Close();
        }
    }
}
