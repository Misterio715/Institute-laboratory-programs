using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace Laba3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        ArrayList data;
        Data sum;

        private void Form1_Load(object sender, EventArgs e)
        {
            data = new ArrayList();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDown1.Enabled = !numericUpDown1.Enabled;
            comboBox2.Enabled = !comboBox2.Enabled;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDown2.Visible = !numericUpDown2.Visible;
            comboBox3.Visible = !comboBox3.Visible;
        }

        private void label1_DoubleClick(object sender, EventArgs e)
        {
            if (comboBox1.Text.Length != 0)
                comboBox1.Items.Add(comboBox1.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            textBox4.Text = "";
            if (radioButton1.Checked)
                sum = new sem1(comboBox1.Text, (int)numericUpDown1.Value, comboBox2.Text);
            if (radioButton2.Checked)
                sum = new sem2(comboBox1.Text, (int)numericUpDown2.Value, comboBox3.Text);
            data.Add(sum);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            for (int i = 0; i < data.Count; i++)
            {
                sum = (Data)data[i];
                if (sum.GetType().Name == "sem1")
                    listBox1.Items.Add(sum.info());
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            for (int i = 0; i < data.Count; i++)
            {
                sum = (Data)data[i];
                if (sum.GetType().Name == "sem2")
                    listBox2.Items.Add(sum.info());
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            double s = 0;
            bool s5 = false, s4 = false, s2 = false;
            for (int i = 0; i < data.Count; i++)
            {
                sum = (Data)data[i];
                s = sum.stipuha();
                if (s == 3)
                    s5 = true;
                else if (s == 2)
                    s4 = true;
                else
                    s2 = true;
            }

            if ((s5 == true) && (s4 != true) && (s2 != true))
                s = 5500;
            else if ((s5 != true) && (s4 == true) && (s2 != true))
                s = 4500;
            else
                s = 0;
                textBox4.Text = string.Format("{0:f2}", s);
            s5 = false; s4 = false; s2 = false;
        }
    }
}
