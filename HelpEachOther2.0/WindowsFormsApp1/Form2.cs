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
using static WindowsFormsApp1.Form2;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        public class Info
        {
            public int Id { get; set; }
            public string Name { get; set; }

        }
        public Form2()
        {
            InitializeComponent();


            //选择捐赠类型处下拉文本框的初始化
            FileStream fs = new FileStream(@"Types_Properties.txt", FileMode.Open, FileAccess.Read);//Information.txt文件只读
            StreamReader sr = new StreamReader(fs);
            
            IList<Info> infoList = new List<Info>();

            string Text = sr.ReadLine();
            int i = 0;
            do
            {
                string[] type=Text.Split(':');
                Info info = new Info() { Id = i, Name = type[0] };
                infoList.Add(info);
                i++;
                Text = sr.ReadLine();
            } while(Text != null);
            
            comboBox1.DataSource = infoList;
            comboBox1.ValueMember = "Id";
            comboBox1.DisplayMember = "Name";

            sr.Close();
            fs.Close();

            //属性输入框初始化
            textBox4.Visible = true;
            j = 0;
            button1.Enabled = false;
        }

        /*
        * 
        * button1是按钮“完成”，功能是将物品信息写入Information.txt中
        * 并且清空五个输入栏，属性输入框重新启用，成功后显示“success”
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
            sw.Write(comboBox1.Text + '\n');
            string str1 = textBox1.Text;
            string str2 = textBox2.Text;
            string str3 = textBox3.Text;
            string str4 = textBox5.Text;
            string str5 = textBox6.Text;
            sw.Write(label2.Text + ':' + str1 + '\n');
            sw.Write(label3.Text + ':' + str2 + '\n');
            sw.Write(label4.Text + ':' + str3 + '\n');
            sw.Write(label9.Text + ':' + str4 + '\n');
            sw.Write(label10.Text + ':' + str5 + '\n');

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";

            FileStream fs1 = new FileStream(@"Inf_prop.txt", FileMode.Open, FileAccess.Read);//Types_Properties.txt文件只读
            StreamReader sr = new StreamReader(fs1);

            string Text=sr.ReadToEnd();
            sw.Write(Text);
            sw.Write("#####" + '\n');

            //四、关闭流
            sw.Flush();
            sw.Close();
            fs.Close();
            sr.Close();
            fs1.Close();

            textBox4.Visible = true;
            j = 0;
            button1.Enabled = false;
            comboBox1_SelectedIndexChanged(sender, new EventArgs());
            MessageBox.Show("success");
        }


        /*
         *下拉文本框内容改变时，step2处属性label也要发生改变
         */
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            FileStream fs = new FileStream(@"Types_Properties.txt", FileMode.Open, FileAccess.Read);//Types_Properties.txt文件只读
            StreamReader sr = new StreamReader(fs);
            int n=comboBox1.SelectedIndex;

            string Text= sr.ReadLine();

            for(int i = 0; i < n; i++)
            {
                Text = sr.ReadLine();
            }

            string[] types =Text.Split(':');
            string[] properties = types[1].Split(';');
            int length=properties.Length;
            label8.Text = properties[0];

            sr.Close();
            fs.Close();
        }


        //j用于计数已输入属性的个数，一个物品的所有信息输入完成后需要及时初始化
        int j = 0;


        /*
         *属性输入文本框，当按下enter，就把当前属性的信息写入Inf_prop.txt
         */
        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
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


                sr.Close();
                fs.Close();

                if (j == 0)
                {
                    FileInfo file2 = new FileInfo(@"Inf_prop.txt");//创建一个临时txt文件
                    file2.Delete();//清空Inf_prop.txt文件
                }
                FileStream fs1 = new FileStream(@"Inf_prop.txt", FileMode.Append, FileAccess.Write);//Inf_prop.txt文件只写
                StreamWriter sw = new StreamWriter(fs1);

                sw.Write(label8.Text + ':');
                sw.Write(textBox4.Text + '\n');
                textBox4.Text = "";

                sw.Flush();
                sw.Close();
                fs1.Close();

                j++;
                if (j != length)
                {
                    label8.Text = properties[j];
                }
                else
                {
                    label8.Text = "所有属性已输入，请进行Step3";
                    textBox4.Visible = false;
                    button1.Enabled = true;
                }
            }
            
        }

        
    }
}
