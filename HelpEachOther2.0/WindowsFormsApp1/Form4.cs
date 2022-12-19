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

namespace WindowsFormsApp1
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)//普通用户登录
        { 
            string str1 = textBox1.Text;
            string str2 = textBox2.Text;
            string str3 = str1 + ';' + str2;
            //一、指定要操作的文件
            FileStream fs = new FileStream(@"UserInformation.txt", FileMode.Open, FileAccess.Read);
            //二、创建StreamReader对象，便于数据的输出操作
            StreamReader sr = new StreamReader(fs);
            //用于标记是否成功登录
            bool flag = false;
            string Text = sr.ReadLine();
            while (Text != null)
            {
                if (str3.Length != Text.Length)
                {
                    Text = sr.ReadLine();
                    continue;
                }
                int j = 0;
                for (; j < str3.Length; j++)//逐个字符比较，一旦不同，退出循环
                {
                    if (str3[j] != Text[j]) break;
                }
                if (j == str3.Length)//检查循环次数
                {
                    MessageBox.Show("欢迎使用“你帮我助”");
                    sr.Close();
                    fs.Close();
                    Form1 f = new Form1();
                    f.ShowDialog();
                    flag = true;
                    return;
                }
                Text = sr.ReadLine();
            }
            if (!flag)
            {
                MessageBox.Show("账号密码错误！");
            }

            sr.Close();
            fs.Close();
        }

        private void button2_Click(object sender, EventArgs e)//管理员登录
        {
            string str1 = textBox1.Text;
            string str2 = textBox2.Text;
            string str3 = str1 + ';' + str2;
            //一、指定要操作的文件
            FileStream fs = new FileStream(@"AdministratorInformation.txt", FileMode.Open, FileAccess.Read);
            //二、创建StreamReader对象，便于数据的输出操作
            StreamReader sr = new StreamReader(fs);
            //用于标记是否成功登录
            bool flag = false;
            string Text = sr.ReadLine();
            while (Text != null)
            {
                if (str3.Length != Text.Length)
                {
                    break;
                }
                int j = 0;
                for (; j < str3.Length; j++)//逐个字符比较，一旦不同，退出循环
                {
                    if (str3[j] != Text[j]) break;
                }
                if (j == str3.Length)//检查循环次数
                {
                    MessageBox.Show("欢迎使用“你帮我助”");
                    sr.Close();
                    fs.Close();
                    Form5 f = new Form5();
                    f.ShowDialog();
                    flag = true;
                    return;
                }
                Text = sr.ReadLine();
            }
            if (!flag)
            {
                MessageBox.Show("账号密码错误！");
            }
            sr.Close();
            fs.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form6 f = new Form6();
            f.ShowDialog();
        }
    }


}
