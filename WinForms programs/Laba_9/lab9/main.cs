using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab9
{
    public partial class main : Form
    {
        private Form1 f = null;
        private int number;
        User user = null;
        private static main instance = new main();

        public static main Instance {
            get { return instance; }
        }

        public void set_values(string number, Form1 f)
        {
            this.f = f;
            this.number = Convert.ToInt32(number);
            user = new User(this.number);
            InitializeComponent();
            textBox1.Text = number.ToString();
        }

        public void set_result(string result) {
            textBox1.Text = result;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool catch_e = false;
            string @operator = ((Button)sender).Text;
            char[] arr = @operator.ToCharArray();
            char @operator2 = arr[0];

            int operand = 0;
            try { operand = Convert.ToInt32(textBox2.Text); }
            catch
            {
                MessageBox.Show("Ошибка ввода операнда", "Error");
                catch_e = true;
            }

            if (catch_e == false)
            {
                textBox2.Text = "";
                richTextBox1.Text = "";
                textBox1.Text = (user.Compute(@operator2, operand)).ToString();

                for (int i = 0; i < user._commands.Count; i++)
                    richTextBox1.Text += (i + 1).ToString() + ". " + ((CalculatorCommand)user._commands[i]).@operator.ToString() + ((CalculatorCommand)user._commands[i]).@operand.ToString() + "\n";
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = (user.Undo(1)).ToString();
            richTextBox1.Text = "";
            change_richTest();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int a = (user.Redo(1));
            if (a!=-1)
                textBox1.Text = a.ToString();
            richTextBox1.Text = "";
            change_richTest();
        }

        private void change_richTest()
        {
            for (int i = 0; i < user._current; i++)
                richTextBox1.Text += (i + 1).ToString() + ". " + ((CalculatorCommand)user._commands[i]).@operator.ToString() + ((CalculatorCommand)user._commands[i]).@operand.ToString() + "\n";
        }

        private void main_FormClosed(object sender, FormClosedEventArgs e)
        {
            f.Close();
        }
    }
}
