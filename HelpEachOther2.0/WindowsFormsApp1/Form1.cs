using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /*
         *
         *button1是主界面的“我要捐赠”按钮，click后跳转至Form2,打开捐赠界面
         *
         */
        private void button1_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            
            f.ShowDialog();
        }

        /*
         * 
         * label1显示文字“你帮我助”，点击后无任何变化
         * 
         */


        /*
         *
         *button2是主界面的“我有困难”按钮，click后跳转至Form3,打开查询和受赠界面
         *
         */
        private void button2_Click(object sender, EventArgs e)
        {
            Form3 f = new Form3();

            f.ShowDialog();
        }
    }
}
