
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WindowsApplication1
{
    public partial class Form2 : Form, IObserver
    {
        //              MVC

        private Form2Controller controller;
        private TrafficLightModel model;
        private bool flag = false;

        private int timeElapsed;


        public Form2(TrafficLightModel model)
        {
            InitializeComponent();

            this.model = model;
            //  наблюдатели (view) регистрируют свою заинтересованность в модели
            this.model.Register(this);
            attachController(makeController());
            dataGridView1.RowHeadersWidth = 4;
            dataGridView1.RowCount = 3;
            dataGridView1[0, 0].Style.BackColor = Color.Red;
            dataGridView1[0, 0].Value = "Красный";
            dataGridView1[0, 1].Style.BackColor = Color.Yellow;
            dataGridView1[0, 1].Value = "Желтый";
            dataGridView1[0, 2].Style.BackColor = Color.Green;
            dataGridView1[0, 2].Value = "Зеленый";
        }
        //    Обновление вида
        //    Все наблюдатели (view), которые хотят зарегистрироваться у наблюдаемого объекта,
        //    должны реализовать интерфейс Observer

        public void UpdateState
                    ()
        {
            switch (model.State)
            {
                case TrafficLightState.Red: 
                    button2.BackColor = Color.Red;
                    button3.BackColor = Color.Black;
                    button4.BackColor = Color.Black;
                    break;
                case TrafficLightState.Yellow:
                    button2.BackColor = Color.Black;
                    button3.BackColor = Color.Yellow;
                    button4.BackColor = Color.Black; 
                    break;
                case TrafficLightState.Green: 
                    button2.BackColor = Color.Black;
                    button3.BackColor = Color.Black;
                    button4.BackColor = Color.Green;
                    break;
                default: this.BackColor = Color.White; break;
            }
            if (flag)
            {
                timeElapsed = 0;
                timer1.Start();
                //toolStripStatusLabel2.Text = "Работает";

                timer1.Interval = 1000;

            }
            else
            {
                timeElapsed = 0;
                timer1.Stop();

                //toolStripStatusLabel2.Text = "Выключен";
            }
        }

        public void attachController(Form2Controller controller) // контроллер
        {
            this.controller = controller;
        }
        protected Form2Controller makeController()
        {
            return new Form2Controller(this, model);
        }



        private void timer1_Tick(object sender, EventArgs e)
        {

            timeElapsed += 1000;
            DateTime dt = DateTime.Now;
            //tbt.Text = dt.Hour + ":" + dt.Minute + ":" + dt.Second;          //String.Format("{0}",timeElapsed/1000);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            flag = true;
            try
            {
                controller.SwithOn();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {

            flag = false;

            controller.SwitchOff();
        }

        private void butto3_Click(object sender, EventArgs e)
        {
            double red = 0, yellow = 0, green = 0;
            try
            {
                red = Convert.ToDouble(dataGridView1[1, 0].Value) * 1000;
                yellow = Convert.ToDouble(dataGridView1[1, 1].Value) * 1000;
                green = Convert.ToDouble(dataGridView1[1, 2].Value) * 1000;
            }
            catch { MessageBox.Show("Задайте правильные интервалы"); }
            try
            {
                controller.SetIntervals(red, yellow, green);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
