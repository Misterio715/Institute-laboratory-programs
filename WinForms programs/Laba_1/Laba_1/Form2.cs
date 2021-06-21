using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laba_1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            textBox1.BackColor = Color.FromArgb(255,255,254);
        }
        public string Text
        {
            get { return label7.Text; }
            set { label7.Text = value; }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string text = textBox1.Text;
            if (String.IsNullOrWhiteSpace(text) == true) MessageBox.Show("Введите текст");
            else
            {
                string[] words = text.Split(new string[] { " ", ",", " - ", ":", ";", "\t", ".", "?", "!", "...", "/" }, StringSplitOptions.RemoveEmptyEntries);
                int ans = words.Length;
                label3.Text = ans.ToString();
                string[] sentences = text.Split(new string[] { ".", "?", "!","..." }, StringSplitOptions.RemoveEmptyEntries);
                label5.Text = Convert.ToString(sentences.Length);
                string symbols = text.Replace(" ", "").Replace("/t", "");
                label9.Text = Convert.ToString(symbols.Length);
            }
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
