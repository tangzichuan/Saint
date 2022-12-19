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
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
            FileStream fs = new FileStream(@"Types_Properties.txt", FileMode.Open, FileAccess.Read);//Types_properties.txt文件只读
            StreamReader sr = new StreamReader(fs);
            TreeNode node = null;
            TreeNode subnode = null;
            string Text = sr.ReadLine();
            do
            {
                string[] Type = Text.Split(':');
                node = new TreeNode(Type[0]);
                treeView1.Nodes.Add(node);
                node.Name = Type[0];

                string[] property = Type[1].Split(';');
                int length=property.Length;
                for(int i = 0; i < length; i++)
                {
                    subnode = new TreeNode(property[i]);
                    subnode.Name = property[i];
                    node.Nodes.Add(subnode);
                    node.Expand();
                }
                Text = sr.ReadLine();
            } while (Text != null);

            sr.Close();
            fs.Close();
        }




        
        private void button2_Click(object sender, EventArgs e)//添加节点
        {
            string nodeName = textBox1.Text;
            TreeNode node = null;
            TreeNode currentNode = null;
            foreach(TreeNode node1 in treeView1.Nodes)
            {
                if(node1.Checked == true && currentNode == null)
                {
                    currentNode = node1;
                }
                else if(node1.Checked == true && currentNode != null)
                {
                    MessageBox.Show("请只选择一个类型添加属性");
                    return;
                }
            }
            if (treeView1 != null && !string.IsNullOrEmpty(nodeName) && currentNode == null)//添加类型节点
            {
                if (!treeView1.Nodes.ContainsKey(nodeName))
                {
                    //添加节点
                    node = new TreeNode(nodeName);
                    treeView1.Nodes.Add(node);
                    node.Name = nodeName;

                    FileStream fs = new FileStream(@"Types_Properties.txt", FileMode.Append, FileAccess.Write);
                    //创建StreamWrite对象，便于数据的输出操作
                    StreamWriter sw = new StreamWriter(fs);
                    //实现数据的写入
                    string str1 = textBox1.Text;
                    sw.Write(str1 +'\n');

                    //四、关闭流
                    sw.Flush();
                    sw.Close();
                    fs.Close();

                }
                else
                {
                    MessageBox.Show("该类型已存在");
                }
            }
            else if (currentNode != null && !string.IsNullOrEmpty(nodeName))//添加属性节点
            {
                if (!currentNode.Nodes.ContainsKey(nodeName))
                {
                    node = new TreeNode(nodeName);
                    node.Name = nodeName;
                    currentNode.Nodes.Add(node);
                    currentNode.Expand();

                    FileInfo file2 = new FileInfo(@"tmp.txt");//创建一个临时txt文件，函数结束时删除
                    file2.Delete();//清空tmp.txt文件
                    FileStream fs1 = new FileStream(@"tmp.txt", FileMode.Append, FileAccess.Write);//tmp.txt文件只写
                    FileStream fs2 = new FileStream(@"Types_Properties.txt", FileMode.Open, FileAccess.Read);//Types_properties.txt文件只读
                    StreamWriter sw = new StreamWriter(fs1);
                    StreamReader sr = new StreamReader(fs2);
                    string str1 = textBox1.Text;
                    string Text = sr.ReadLine();
                    do
                    {
                        string[] segments = Text.Split(':');
                        if (segments[0] == currentNode.Name)
                        {
                            if (segments.Length == 1)
                            {
                                Text = Text + ':' + str1;
                            }
                            else
                            {
                                Text = Text + ';' + str1;
                            }
                            sw.Write(Text + '\n');
                        }
                        else
                        {
                            sw.Write(Text + '\n');
                        }
                        Text = sr.ReadLine();
                    } while (Text != null);
                    

                    sw.Flush();
                    sw.Close();
                    fs1.Close();
                    sr.Close();
                    fs2.Close();

                    FileInfo file = new FileInfo(@"Types_Properties.txt");
                    file.Delete();//原文件清空
                    file2.MoveTo("Types_Properties.txt");//tmp.txt改名为Information.txt
                }
                else
                {
                    MessageBox.Show("该属性已存在");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)//删除节点
        {
            TreeNode currentNode = null;
            foreach (TreeNode node1 in treeView1.Nodes)
            {
                if (node1.Checked == true && currentNode == null)//删除类型
                {
                    currentNode = node1;
                    string str1 = currentNode.Name;
                    treeView1.Nodes.Remove(currentNode);

                    FileInfo file2 = new FileInfo(@"tmp.txt");//创建一个临时txt文件，函数结束时删除
                    file2.Delete();//清空tmp.txt文件
                    FileStream fs1 = new FileStream(@"tmp.txt", FileMode.Append, FileAccess.Write);//tmp.txt文件只写
                    FileStream fs2 = new FileStream(@"Types_Properties.txt", FileMode.Open, FileAccess.Read);//Types_properties.txt文件只读
                    StreamWriter sw = new StreamWriter(fs1);
                    StreamReader sr = new StreamReader(fs2);
                    
                    string Text = sr.ReadLine();
                    do
                    {
                        string[] segments = Text.Split(':');
                        if (segments[0] != str1)
                        {
                            sw.Write(Text + '\n');
                        }
                        Text = sr.ReadLine();
                    } while (Text != null);


                    sw.Flush();
                    sw.Close();
                    fs1.Close();
                    sr.Close();
                    fs2.Close();

                    FileInfo file = new FileInfo(@"Types_Properties.txt");
                    file.Delete();//原文件清空
                    file2.MoveTo("Types_Properties.txt");//tmp.txt改名为Types_Properties.txt

                    continue;
                }
                else if (node1.Checked == true && currentNode != null)
                {
                    MessageBox.Show("请只选择一个类型删除");
                    return;
                }
                foreach (TreeNode node2 in node1.Nodes)
                {
                    if (node2.Checked == true && currentNode == null)//删除属性
                    {
                        currentNode = node2;
                        string str2 = currentNode.Name;
                        treeView1.Nodes.Remove(currentNode);

                        FileInfo file2 = new FileInfo(@"tmp.txt");//创建一个临时txt文件，函数结束时删除
                        file2.Delete();//清空tmp.txt文件
                        FileStream fs1 = new FileStream(@"tmp.txt", FileMode.Append, FileAccess.Write);//tmp.txt文件只写
                        FileStream fs2 = new FileStream(@"Types_Properties.txt", FileMode.Open, FileAccess.Read);//Types_properties.txt文件只读
                        StreamWriter sw = new StreamWriter(fs1);
                        StreamReader sr = new StreamReader(fs2);

                        string Text = sr.ReadLine();
                        do
                        {
                            string[] segments = Text.Split(':');
                            string[] property=segments[1].Split(';');
                            int length=property.Length;
                            string new_inf = segments[0];
                            for(int i=0; i<length; i++)
                            {
                                if (property[i] != str2)
                                {
                                    if (new_inf != segments[0])
                                    {
                                        new_inf += ';' + property[i];
                                    }
                                    else new_inf += ':' + property[i];
                                }
                            }
                            sw.Write(new_inf + '\n');
                            Text = sr.ReadLine();
                        } while (Text != null);


                        sw.Flush();
                        sw.Close();
                        fs1.Close();
                        sr.Close();
                        fs2.Close();

                        FileInfo file = new FileInfo(@"Types_Properties.txt");
                        file.Delete();//原文件清空
                        file2.MoveTo("Types_Properties.txt");//tmp.txt改名为Types_Properties.txt

                        break;
                    }
                    else if (node2.Checked == true && currentNode != null)
                    {
                        MessageBox.Show("请只选择一个属性删除");
                        return;
                    }
                }

            }
        }

        private void button4_Click(object sender, EventArgs e)//修改节点
        {
            TreeNode currentNode = null;
            foreach (TreeNode node1 in treeView1.Nodes)
            {
                if (node1.Checked == true && currentNode == null)//修改类型
                {
                    string str3 = textBox1.Text;
                    currentNode = node1;
                    string str1 = currentNode.Name;
                    currentNode.Text = str3;
                    currentNode.Name = str3;

                    FileInfo file2 = new FileInfo(@"tmp.txt");//创建一个临时txt文件，函数结束时删除
                    file2.Delete();//清空tmp.txt文件
                    FileStream fs1 = new FileStream(@"tmp.txt", FileMode.Append, FileAccess.Write);//tmp.txt文件只写
                    FileStream fs2 = new FileStream(@"Types_Properties.txt", FileMode.Open, FileAccess.Read);//Types_properties.txt文件只读
                    StreamWriter sw = new StreamWriter(fs1);
                    StreamReader sr = new StreamReader(fs2);

                    string Text = sr.ReadLine();
                    do
                    {
                        string[] segments = Text.Split(':');
                        if (segments[0] != str1)
                        {
                            sw.Write(Text + '\n');
                        }
                        else
                        {
                            Text = str3 + ':' + segments[1];
                            sw.Write(Text + '\n');
                        }
                        Text = sr.ReadLine();
                    } while (Text != null);


                    sw.Flush();
                    sw.Close();
                    fs1.Close();
                    sr.Close();
                    fs2.Close();

                    FileInfo file = new FileInfo(@"Types_Properties.txt");
                    file.Delete();//原文件清空
                    file2.MoveTo("Types_Properties.txt");//tmp.txt改名为Types_Properties.txt

                    continue;
                }
                else if (node1.Checked == true && currentNode != null)
                {
                    MessageBox.Show("请只选择一个类型修改");
                    return;
                }
                foreach (TreeNode node2 in node1.Nodes)
                {
                    if (node2.Checked == true && currentNode == null)//修改属性
                    {
                        string str3 = textBox1.Text;
                        currentNode = node2;
                        string str2 = currentNode.Name;
                        currentNode.Text = str3;
                        currentNode.Name = str3;

                        FileInfo file2 = new FileInfo(@"tmp.txt");//创建一个临时txt文件，函数结束时删除
                        file2.Delete();//清空tmp.txt文件
                        FileStream fs1 = new FileStream(@"tmp.txt", FileMode.Append, FileAccess.Write);//tmp.txt文件只写
                        FileStream fs2 = new FileStream(@"Types_Properties.txt", FileMode.Open, FileAccess.Read);//Types_properties.txt文件只读
                        StreamWriter sw = new StreamWriter(fs1);
                        StreamReader sr = new StreamReader(fs2);

                        string Text = sr.ReadLine();
                        do
                        {
                            string[] segments = Text.Split(':');
                            string[] property = segments[1].Split(';');
                            int length = property.Length;
                            string new_inf = segments[0] + ':';
                            for (int i = 0; i < length; i++)
                            {
                                if (property[i] != str2)
                                {
                                    if (i != 0)
                                    {
                                        new_inf += ';' + property[i];
                                    }
                                    else new_inf += property[i];
                                }
                                else
                                {
                                    if (i != 0)
                                    {
                                        new_inf += ';' + str3;
                                    }
                                    else new_inf += str3;
                                }
                            }
                            sw.Write(new_inf + '\n');
                            Text = sr.ReadLine();
                        } while (Text != null);


                        sw.Flush();
                        sw.Close();
                        fs1.Close();
                        sr.Close();
                        fs2.Close();

                        FileInfo file = new FileInfo(@"Types_Properties.txt");
                        file.Delete();//原文件清空
                        file2.MoveTo("Types_Properties.txt");//tmp.txt改名为Types_Properties.txt

                        continue;
                    }
                    else if (node2.Checked == true && currentNode != null)
                    {
                        MessageBox.Show("请只选择一个属性修改");
                        return;
                    }
                }
            }
        }
    }
}
