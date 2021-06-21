using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laba_4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns.Add("", "Предмет");
            dataGridView1.Columns.Add("", "Оценка");
            dataGridView1.Columns.Add("", "Пересдача");

            dataGridView2.Columns.Add("", "Предмет");
            dataGridView2.Columns.Add("", "Оценка");
            dataGridView2.Columns.Add("", "Пересдача");
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        string name;

        private void предметToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 subj = new Form4();
            subj.ShowDialog();
            name = subj.namef;
        }

        string mark1;
        private void данныеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 sem1 = new Form2();
            sem1.ShowDialog();
            sem1.mark1f = mark1;
            dataGridView1.Rows.Insert(0, 1);
            dataGridView1[0, 0].Value = name;
            dataGridView1[1, 0].Value = sem1.mark;
            dataGridView1[2, 0].Value = sem1.retake;
        }

        string mark2;
        private void данныеToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form3 sem2 = new Form3();
            sem2.ShowDialog();
            sem2.mark2f = mark2;
            dataGridView2.Rows.Insert(0, 1);
            dataGridView2[0, 0].Value = name;
            dataGridView2[1, 0].Value = sem2.mark;
            dataGridView2[2, 0].Value = sem2.retake;
        }

        private void стипендияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            double s = 0;
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                s = s + (double)(dataGridView1[2, i].Value);
            for (int i = 0; i < dataGridView2.Rows.Count - 1; i++)
                s = s + (double)(dataGridView2[2, i].Value);
            MessageBox.Show(string.Format("{0:f2}", s), "Итоговая сумма");
        }
    }
}
