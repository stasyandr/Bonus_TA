using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bonus
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            label6.Text = "Пустое слово: _\nПереход: >\nКаждое правило с новой строки\nПравила писать без пробелов\nЕсли будет выполнено болеше 200 действий программа будет считать схему зациклевшейся" +
                "\nПрисутвие/отсутвие отступа строки после последнего правила не принципиально";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> d = new Dictionary<string, string>();
            string s = textBox1.Text;
            int i = 0;
            bool flage = false;
            while (i < s.Length)
            {
                try
                {
                    int c = s.IndexOf('\n', i);
                    if (c == -1)
                        c = s.Length - 1;
                    int c1 = s.IndexOf(">", i);
                    string s2 = s.Substring(i, c1 - i);
                    string s3 = s.Substring(c1 + 1, c - c1);
                    if (s3.IndexOf('\n') != -1)
                        s3 = s3.Remove(s3.IndexOf('\n'));
                    if (s3.Length == 0)
                        throw new Exception();
                    if (!d.ContainsKey(s2))
                        d[s2] = s3;
                    i = c + 1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Одно из правил имело неверный формат");
                    flage = true;
                    break;
                }
            }            
            /* foreach (var x in d)
                MessageBox.Show(x.Key + ' ' + x.Value);*/
            s = textBox3.Text;
            if (s.Length == 0)
                s = "_";
            textBox2.Text = s;
            if (!flage)
            {                
                int k = 0;
                while (true)
                {
                    bool flag = false;
                    bool flag1 = false;
                    foreach (var x in d)
                    {
                        int c = s.IndexOf(x.Key);
                        if (c != -1)
                        {
                            string r = x.Value;
                            if (x.Value[0] == '.')
                            {
                                flag1 = true;
                                r = r.Remove(0, 1);
                            }
                            s = s.Remove(c, x.Key.Length);
                            if (r[0] != '_')
                            {
                                s = s.Insert(c, r);
                            }
                            if (s.Length == 0)
                                s = "_";
                            textBox2.Text = textBox2.Text + "->" + s;
                            flag = true;
                            break;
                        }
                        else
                        {
                            if (x.Key.Equals("_"))
                            {
                                string r = x.Value;
                                if (x.Value[0] == '.')
                                {
                                    flag1 = true;
                                    r = r.Remove(0, 1);
                                }
                                if (!r.Equals("_"))
                                    s = s.Insert(0, r);
                                if (s.Length == 0)
                                    s = "_";
                                textBox2.Text = textBox2.Text + "->" + s;
                                flag = true;
                                break;
                            }
                        }
                    }
                    k++;
                    if (!flag || flag1)
                        break;
                    if (k == 200)
                    {
                        MessageBox.Show("Произошло зацикливание");
                        break;
                    }
                }
            }
            textBox4.Text = s;
        }

    }
}
