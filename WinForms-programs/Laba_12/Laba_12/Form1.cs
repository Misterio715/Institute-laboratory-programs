using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laba_12
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            switch ((string)e.Node.Tag)
            {
                case "1":
                        label3.Text = "бактерии";
                        pictureBox1.Image = Properties.Resources.bakt;
                        break;
                case "2":
                        label3.Text = "водоросли";
                        pictureBox1.Image = Properties.Resources.vodor;
                        break;
                case "3":
                        label3.Text = "багрянка(красные водоросли)";
                        pictureBox1.Image = Properties.Resources.bagr;
                        break;
                case "4":
                        label3.Text = "высшее споровое растение";
                        pictureBox1.Image = Properties.Resources.spor;
                        break;
                case "5":
                        label3.Text = "высшее семенное растение";
                        pictureBox1.Image = Properties.Resources.semen;
                        break;
                case "6":
                        label3.Text = "гриб";
                        pictureBox1.Image = Properties.Resources.grib;
                        break;
            }
        }
    }
}
