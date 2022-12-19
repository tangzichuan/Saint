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
    public partial class Form7 : Form
    {
        public class app
        {
            public static bool flag;
        }
        public Form7()
        {
            InitializeComponent();
            FileStream fs = new FileStream(@"ApplicantInformation.txt", FileMode.Open, FileAccess.Read);
            //二、创建StreamReader对象，便于数据的输出操作
            StreamReader sr = new StreamReader(fs);
            //二、实现数据的读取
            string Text1 = sr.ReadLine();
            string Text2 = sr.ReadLine();
            string Text3 = sr.ReadLine();
            string Text4 = sr.ReadLine();
            string Text5 = sr.ReadLine();
            label7.Text = Text1;
            label8.Text = Text2;
            label9.Text = Text3;
            label10.Text = Text4;
            //四、关闭流
            sr.Close();
            fs.Close();


            if (Text1 == null)
            {
                app.flag = false;
            }
            else
            {
                app.flag = true;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FileInfo file2 = new FileInfo(@"tmp.txt");//创建一个临时txt文件，函数结束时删除
            file2.Delete();//清空tmp.txt文件
            FileStream fs = new FileStream(@"UserInformation.txt", FileMode.Append, FileAccess.Write);//UserInformation.txt只写
            FileStream fs1 = new FileStream(@"tmp.txt", FileMode.Append, FileAccess.Write);//tmp.txt文件只写
            FileStream fs2 = new FileStream(@"ApplicantInformation.txt", FileMode.Open, FileAccess.Read);//ApplicantInformation.txt文件只读
            StreamWriter sw = new StreamWriter(fs1);
            StreamReader sr = new StreamReader(fs2);
            StreamWriter sw_User = new StreamWriter(fs);

            //二、写入新用户的账号和密码
            string Text;
            for (int i = 0; i < 3; i++)
            {
                Text = sr.ReadLine();
            }
            string str1 = sr.ReadLine();
            string str2 = sr.ReadLine();
            Text = str1 + ';' + str2;
            sw_User.Write(Text+'\n');


            //从ApplicantInformation.txt中删除此用户信息
            //更新显示，显示下一位申请者信息
            string Text1 = sr.ReadLine();
            string Text2 = sr.ReadLine();
            string Text3 = sr.ReadLine();
            string Text4 = sr.ReadLine();
            string Text5 = sr.ReadLine();
            label7.Text = Text1;
            label8.Text = Text2;
            label9.Text = Text3;
            label10.Text = Text4;
            sw.Write(Text1 + '\n');
            sw.Write(Text2 + '\n');
            sw.Write(Text3 + '\n');
            sw.Write(Text4 + '\n');
            sw.Write(Text5 + '\n');

            Text =sr.ReadToEnd();
            sw.Write(Text);

            sw.Flush();
            sw.Close();
            fs1.Close();

            sw_User.Flush();
            sw_User.Close();
            fs.Close();

            

            sr.Close();
            fs2.Close();

            FileInfo file = new FileInfo(@"ApplicantInformation.txt");
            file.Delete();//清空tmp.txt文件
            file2.MoveTo("ApplicantInformation.txt");//tmp.txt改名为Information.txt

            MessageBox.Show("success");

            if (Text1 == null)
            {
                app.flag = false;
            }
            else
            {
                app.flag = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FileInfo file2 = new FileInfo(@"tmp.txt");//创建一个临时txt文件，函数结束时删除
            file2.Delete();//清空tmp.txt文件
            FileStream fs1 = new FileStream(@"tmp.txt", FileMode.Append, FileAccess.Write);//tmp.txt文件只写
            FileStream fs2 = new FileStream(@"ApplicantInformation.txt", FileMode.Open, FileAccess.Read);//ApplicantInformation.txt文件只读
            StreamWriter sw = new StreamWriter(fs1);
            StreamReader sr = new StreamReader(fs2);

            
            //从ApplicantInformation.txt中删除此用户信息
            string Text;
            for(int i = 0; i < 5; i++)
            {
                Text=sr.ReadLine();
            }
            //更新显示，显示下一位申请者信息
            string Text1 = sr.ReadLine();
            string Text2 = sr.ReadLine();
            string Text3 = sr.ReadLine();
            string Text4 = sr.ReadLine();
            string Text5 = sr.ReadLine();
            label7.Text = Text1;
            label8.Text = Text2;
            label9.Text = Text3;
            label10.Text = Text4;
            sw.Write(Text1 + '\n');
            sw.Write(Text2 + '\n');
            sw.Write(Text3 + '\n');
            sw.Write(Text4 + '\n');
            sw.Write(Text5 + '\n');

            Text = sr.ReadToEnd();
            sw.Write(Text);

            sw.Flush();
            sw.Close();
            fs1.Close();

            

            //关闭流
            sr.Close();
            fs2.Close();

            FileInfo file = new FileInfo(@"ApplicantInformation.txt");
            file.Delete();//清空tmp.txt文件
            file2.MoveTo("ApplicantInformation.txt");//tmp.txt改名为Information.txt

            MessageBox.Show("success");

            //判断是否还有未处理的申请
            if (Text1 == null)
            {
                app.flag = false;
            }
            else
            {
                app.flag = true;
            }
        }
    }
    
}
