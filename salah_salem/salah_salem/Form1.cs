using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Road_Syste_Smart_Cars;

namespace salah_salem
{
    public partial class Form1 : Form
    {
        public static List<int> neb = new List<int>();
        public static long Num_Original_Query = 0;
        read_Write_files rf = new read_Write_files();
        graph1 graph = new graph1();
        public string final_path;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            final_path = graph.Dijkstra(rf, "OurOutPut1.txt", int.Parse(textBox1.Text), int.Parse(textBox2.Text));
            string dirc = comboBox2.SelectedItem.ToString();
            if (comboBox1.SelectedItem == "Car 1")
            {
                serialPort1.Open();
                serialPort1.Write(dirc);
                serialPort1.Write(final_path);
                serialPort1.Close();
            }
            else if (comboBox1.SelectedItem == "Car 2")
            {
                serialPort2.Open();
                serialPort2.Write(dirc);
                serialPort2.Write(final_path);
                serialPort2.Close();
            }
            else if (comboBox1.SelectedItem == "Car 3")
            {
                serialPort4.Open();
                serialPort4.Write(dirc);
                serialPort4.Write(final_path);
                serialPort4.Close();
            }
            else
            {
                serialPort5.Open();
                serialPort5.Write(dirc);
                serialPort5.Write(final_path);
                serialPort5.Close();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            rf.read_ver_edg("map.txt");
            graph.Add_Adj(rf);
            
        }
    }
}
