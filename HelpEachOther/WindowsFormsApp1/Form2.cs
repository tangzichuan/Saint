using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        /*
        * 
        * label1显示文字“请输入要捐赠物品信息”，点击后无任何变化
        * 
        */
        private void label1_Click(object sender, EventArgs e)
        {

        }

        /*
        * 
        * button1是按钮“完成”，功能是将物品信息写入Information.txt中
        * 并且清空三个输入栏，成功后显示“success”
        * Information.txt的位置在HelpEachOther\WindowsFormsApp1\bin\Debug\Information.txt
        * 
        */

        private void button1_Click(object sender, EventArgs e)
        {
            //一、指定要操作的文件
            FileStream fs = new FileStream(@"Information.txt", FileMode.Append, FileAccess.Write);
            //二、创建StreamWrite对象，便于数据的输出操作
            StreamWriter sw = new StreamWriter(fs);
            //二、实现数据的写入
            string str1 = textBox1.Text;
            string str2 = textBox2.Text;
            string str3 = textBox3.Text;
            sw.Write(str1 + ';');
            sw.Write(str2 + ';');
            sw.Write(str3 + '\n');

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            //四、关闭流
            sw.Flush();
            sw.Close();
            fs.Close();
            MessageBox.Show("success");
        }
    }
}
