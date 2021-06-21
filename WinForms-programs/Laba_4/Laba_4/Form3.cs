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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        public string mark2f;
        public int mark;
        public string retake;

        private void button1_Click(object sender, EventArgs e)
        {
            mark = (int)numericUpDown1.Value;
            retake = comboBox2.Text;
            Close();
        }
    }
}
