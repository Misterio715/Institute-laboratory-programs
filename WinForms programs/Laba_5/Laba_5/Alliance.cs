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
    public partial class Alliance : Form
    {
        private AllianceInf alliance;
        private static Alliance current;

        public static Alliance get_alliance(AbstractProduct product)
        {
            if (current == null)
            {
                current = new Alliance(product);
            }
            return current;
        }

        public Alliance(AbstractProduct product)
        {
            alliance = (AllianceInf)product;
            InitializeComponent();
        }

        private void геройАльянсаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.Visible = true;
        }

        private void информацияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Visible = true;
            richTextBox1.Text = alliance.getcharacteristics();
        }
    }
}
