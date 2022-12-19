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
using static WindowsFormsApp1.Form7;

namespace WindowsFormsApp1
{
    /// <summary>
    /// Form5是管理员界面，可用于处理新用户申请，添加新物品类型
    /// </summary>
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
            FileStream fs = new FileStream(@"ApplicantInformation.txt", FileMode.Open, FileAccess.Read);
            //二、创建StreamReader对象，便于数据的输出操作
            StreamReader sr = new StreamReader(fs);
            //二、实现数据的读取
            string Text = sr.ReadLine();
            if (Text != null)
            {
                label1.ForeColor = Color.Red;
                label1.Text = "您有未处理的申请,请及时处理！";
            }
            else
            {
                label1.ForeColor = Color.Black;
                label1.Text = "所有申请已处理";
            }
            //四、关闭流
            sr.Close();
            fs.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form7 f = new Form7();//打开新用户申请处理界面
            timer1.Start();
            f.ShowDialog();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (app.flag)
            {
                label1.ForeColor = Color.Red;
                label1.Text = "您有未处理的申请,请及时处理！";
            }
            else
            {
                label1.ForeColor = Color.Black;
                label1.Text = "所有申请已处理";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form8 f = new Form8();//打开设置物品类型界面
            f.ShowDialog();
        }
    }
}
