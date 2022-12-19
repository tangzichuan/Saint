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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)//提交申请
        {
            //一、指定要操作的文件
            FileStream fs = new FileStream(@"ApplicantInformation.txt", FileMode.Append, FileAccess.Write);
            FileStream fs1 = new FileStream(@"UserInformation.txt", FileMode.Open, FileAccess.Read);//ApplicantInformation.txt文件只读
            //二、创建StreamWrite对象，便于数据的输出操作
            StreamWriter sw = new StreamWriter(fs);
            StreamReader sr = new StreamReader(fs1);
            //二、实现数据的写入
            string str1 = textBox1.Text;
            string str2 = textBox2.Text;
            string str3 = textBox3.Text;
            string str4 = textBox4.Text;
            string str5 = textBox5.Text;
            string str6 = textBox6.Text;
            if(str1.Length == 0 || str2.Length == 0 || str3.Length == 0)
            {
                MessageBox.Show("个人信息不可为空");
                sw.Flush();
                sw.Close();
                fs.Close();
                sr.Close();
                fs1.Close();
                return;
            }
            if (str4.Length < 6 || str4.Length > 18)
            {
                MessageBox.Show("账号名称不符合要求");
                sw.Flush();
                sw.Close();
                fs.Close();
                sr.Close();
                fs1.Close();
                return;
            }
            if (str5.Length < 6 || str5.Length > 18)
            {
                MessageBox.Show("密码不符合要求");
                sw.Flush();
                sw.Close();
                fs.Close();
                sr.Close();
                fs1.Close();
                return;
            }
            if (str5 != str6)
            {
                MessageBox.Show("两次密码输入不一致");
                sw.Flush();
                sw.Close();
                fs.Close();
                sr.Close();
                fs1.Close();
                return;
            }

            string account = str4 + ';' + str5;
            string Text = sr.ReadLine();
            while(Text != null)
            {
                if (Text != account)
                {
                    Text = sr.ReadLine();
                    continue;
                }
                else
                {
                    MessageBox.Show("该账号密码已存在");
                    sw.Flush();
                    sw.Close();
                    fs.Close();
                    sr.Close();
                    fs1.Close();
                    return;
                }
            }

            sw.Write(str1 + '\n');
            sw.Write(str2 + '\n');
            sw.Write(str3 + '\n');
            sw.Write(str4 + '\n');
            sw.Write(str5 + '\n');
            //四、关闭流
            sw.Flush();
            sw.Close();
            fs.Close();
            sr.Close();
            fs1.Close();
            MessageBox.Show("已成功提交申请，请等待管理员通过");
            return;
        }
    }
}
