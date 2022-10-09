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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp1
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
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
            listBox1.Items.Clear();
            //一、指定要操作的文件
            FileStream fs = new FileStream(@"Information.txt", FileMode.Open, FileAccess.Read);
            //二、创建StreamReader对象，便于数据的输出操作
            StreamReader sr = new StreamReader(fs);
            //二、实现数据的读取
            string Text = sr.ReadLine();
            while (Text!="") {
                if (textBox1.Text == "")//输入框无内容
                {
                    listBox1.Items.Add(Text);
                }
                else//输入框有内容
                {
                    int j = 0;
                    for(; j < textBox1.Text.Length; j++)//逐个字符比较，一旦不同，退出循环
                    {
                        if (textBox1.Text[j] != Text[j]) break;
                    }
                    if(j == textBox1.Text.Length)//检查循环次数
                    {
                        listBox1.Items.Add(Text);//检索成功，显示信息
                    }
                }
                Text = sr.ReadLine();
            }
            //四、关闭流
            sr.Close();
            fs.Close();
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
            FileInfo file2 = new FileInfo(@"tmp.txt");//创建一个临时txt文件，函数结束时删除
            file2.Delete();//清空tmp.txt文件
            FileStream fs1 = new FileStream(@"tmp.txt", FileMode.Append, FileAccess.Write);//tmp.txt文件只写
            FileStream fs2 = new FileStream(@"Information.txt", FileMode.Open, FileAccess.Read);//Information.txt文件只读
            StreamWriter sw = new StreamWriter(fs1);
            StreamReader sr = new StreamReader(fs2);
            string Text;
            do {
                Text = sr.ReadLine();
                bool flag = true;
                for (int i = 0; i < listBox1.SelectedItems.Count; i++)
                {
                    if (listBox1.SelectedItems[i].ToString() == Text)
                    {
                        flag = false;//此条信息被选中
                        break;
                    }
                }
                if (flag)//此条信息未被选中
                {
                    sw.Write(Text + '\n');//写入tmp.txt
                }
            } while(Text!=null);

            sw.Flush();
            sw.Close();
            fs1.Close();
            sr.Close();
            fs2.Close();

            FileInfo file = new FileInfo(@"Information.txt");
            file.Delete();//原文件清空
            file2.MoveTo("Information.txt");//tmp.txt改名为Information.txt

            MessageBox.Show("success");
        }
    }
}
