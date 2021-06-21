using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Library5;

namespace Laba_5
{
    public partial class Horde : Form
    {
        private HordeInf horde;
        private static Horde current;

        public static Horde get_horde(AbstractProduct product)
        {
            if (current == null)
            {
                current = new Horde(product);
            }
            return current;
        }

        public Horde(AbstractProduct product)
        {
            horde = (HordeInf)product;
            InitializeComponent();
        }

        private void геройОрдыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.Visible = true;
        }

        private void информацияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Visible = true;
            richTextBox1.Text = horde.getcharacteristics();
        }
    }
}
