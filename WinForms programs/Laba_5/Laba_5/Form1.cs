using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laba_5
{
    public partial class Form1 : Form
    {
        private static Form1 form = new Form1();
        public Form1()
        {
            InitializeComponent();
        }

        public static Form1 GetForm
        {
            get
            {
                return form;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AbstractFactory factory1 = new FactoryHorde();
            Client client = new Client(factory1);
            client.Run();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AbstractFactory factory2 = new FactoryAlliance();
            Client client = new Client(factory2);
            client.Run();
        }
    }
}
