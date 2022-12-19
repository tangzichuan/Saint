using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static WindowsFormsApp1.Form2;

namespace WindowsFormsApp1
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();

            //选择物品类型处下拉文本框的初始化
            FileStream fs = new FileStream(@"Types_Properties.txt", FileMode.Open, FileAccess.Read);//Information.txt文件只读
            StreamReader sr = new StreamReader(fs);

            IList<Info> infoList = new List<Info>();

            string Text = sr.ReadLine();
            int i = 0;
            do
            {
                string[] type = Text.Split(':');
                Info info = new Info() { Id = i, Name = type[0] };
                infoList.Add(info);
                i++;
                Text = sr.ReadLine();
            } while (Text != null);

            comboBox1.DataSource = infoList;
            comboBox1.ValueMember = "Id";
            comboBox1.DisplayMember = "Name";

            sr.Close();
            fs.Close();
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            FileStream fs = new FileStream(@"Types_Properties.txt", FileMode.Open, FileAccess.Read);//Types_Properties.txt文件只读
            StreamReader sr = new StreamReader(fs);
            int n = comboBox1.SelectedIndex;

            string Text = sr.ReadLine();

            for (int i = 0; i < n; i++)
            {
                Text = sr.ReadLine();
            }

            string[] types = Text.Split(':');
            string[] properties = types[1].Split(';');
            int length = properties.Length;
            string column = "序列\t物品名称\t物品说明\t物品所在地址\t联系人手机\t邮箱";
            for (int i = 0; i < length; i++)
            {
                column += '\t' + properties[i];
            }


            string[] Columns=column.Split('\t');
            for (int i = 0; i < Columns.Length; i++)
            {
                dataGridView1.Columns.Add((i).ToString(), Columns[i]);
            }

            sr.Close();
            fs.Close();
        }

        /*
         * 
         * button1是“查找”按钮
         * 功能：当输入框为空时，显示储存的所有物品信息
         * 当输入框有内容时，查询Information.txt文件中的所有物品信息，显示名称与输入框内容相同的物品信息
         * 
         */
        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            comboBox1_SelectedIndexChanged(sender, e);
            //一、指定要操作的文件
            FileStream fs = new FileStream(@"Information.txt", FileMode.Open, FileAccess.Read);
            //二、创建StreamReader对象，便于数据的输出操作
            StreamReader sr = new StreamReader(fs);
            //二、实现数据的读取
            string Text = sr.ReadLine();
            bool flag = false;
            int i = 1;
            string information = "";
            while (Text != null)
            {
                if (Text == comboBox1.Text)
                {
                    flag = true;
                    Text = sr.ReadLine();
                    continue;
                }
                if (Text == "#####")
                {
                    flag = false;
                    if (information != "" && (textBox1.Text == "" || find(information, textBox1.Text)))
                    {
                        string[] Informations=information.Split('\t');
                        int index = dataGridView1.Rows.Add();
                        for (int k = 0; k < Informations.Length; k++)
                        {
                            dataGridView1.Rows[index].Cells[k].Value = Informations[k];
                        }
                        i++;
                    }
                    information = "";
                }
                if (flag)
                {
                    if (information == "")
                    {
                        information += i.ToString();
                    }
                    string[] Type = Text.Split(':');
                    information += '\t' + Type[1];
                }
                Text = sr.ReadLine();
            }
            //四、关闭流
            sr.Close();
            fs.Close();
        }


        public bool find(string s, string t)//从字符串中寻找子串t，若找到，返回true，否则，返回false。
        {
            bool isfind = false;
            for (int i = 0; i < s.Length - t.Length + 1; i++)
            {
                if (s[i] == t[0])
                {
                    int jc = 0;
                    for (int j = 0; j < t.Length; j++)
                    {
                        if (s[i + j] != t[j])
                        {
                            break;
                        }
                        jc = j;
                    }

                    if (jc == t.Length - 1)
                    {
                        isfind = true;
                    }
                }
            }
            return isfind;
        }

        /*
         * 
         * button2是“删除选中项”按钮
         * 功能：从Information.txt文件中删除选中项的信息
         * 成功后显示“success”
         * 
         */
        private void button2_Click(object sender, EventArgs e)
        {
            string currentRowIndex = dataGridView1.CurrentRow.Cells[0].Value.ToString();

            FileInfo file2 = new FileInfo(@"temporary.txt");//创建一个临时txt文件，函数结束时删除
            file2.Delete();//清空temporary.txt文件
            FileStream fs1 = new FileStream(@"temporary.txt", FileMode.Append, FileAccess.Write);//temporary.txt文件只写
            FileStream fs2 = new FileStream(@"Information.txt", FileMode.Open, FileAccess.Read);//Information.txt文件只读
            StreamWriter sw = new StreamWriter(fs1);
            StreamReader sr = new StreamReader(fs2);
            string Text = sr.ReadLine();
            bool flag = false;
            int i = 1;
            string information = "";
            string spy = "";
            while (Text != null)
            {
                if (Text == comboBox1.Text)
                {
                    flag = true;
                    spy = Text;
                    Text = sr.ReadLine();
                    continue;
                }
                if (Text == "#####")
                {
                    flag = false;
                    if (information != "" && (textBox1.Text == "" || find(information, textBox1.Text)))
                    {
                        string[] Inf = information.Split('\t');
                        if (Inf[0] != currentRowIndex)
                        {
                            sw.Write(spy+'\n');
                            for(int j = 1; j < Inf.Length; j++)
                            {
                                sw.Write(dataGridView1.Columns[j].HeaderText + ':' + Inf[j] + '\n');
                            }
                            sw.Write(Text + '\n');
                        }
                        i++;
                    }
                    else if (information != "") 
                    {
                        string[] Inf = information.Split('\t');
                        sw.Write(spy + '\n');
                        for (int j = 1; j < Inf.Length; j++)
                        {
                            sw.Write(Inf[j] + '\n');
                        }
                        sw.Write(Text + '\n');
                    }
                    else if (information == "")
                    {
                        sw.Write(Text + '\n');
                    }
                    information = "";
                    Text = sr.ReadLine();
                    continue;
                }
                if (flag)
                {
                    if (information == "")
                    {
                        information += i.ToString();
                    }
                    string[] Type = Text.Split(':');
                    information += '\t' + Type[1];
                }
                if (!flag)
                {
                    sw.Write(Text + '\n');
                }
                Text = sr.ReadLine();
            }

            //关闭流
            sw.Flush();
            sw.Close();
            fs1.Close();
            sr.Close();
            fs2.Close();

            FileInfo file = new FileInfo(@"Information.txt");
            file.Delete();//清空temporary.txt文件
            file2.MoveTo("Information.txt");//temporary.txt改名为Information.txt

            button1_Click(sender, new EventArgs());

            MessageBox.Show("success");
        }
    }
}



