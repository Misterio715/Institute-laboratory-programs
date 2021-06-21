
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WindowsApplication1
{   // ВИД_4 (View_4)
    //  Вид — это тот слой триады MVC, который отображает графическое или текстовое представление
    //  модели. Вид получает всю информацию о состоянии модели от самой модели.
    //  Единственной связью между моделью и пользовательским интерфейсом является связь
    //  через службу уведомления об изменении состояния системы.
    //  Если вид или контроллер заинтересованы в уведомлении об изменении состояния системы, 
    //  они регистрируются в модели через службу регистрации.
    //  Для формирования составного вида используется паттерн компоновщик  

    public partial class Form1 : Form, IObserver
    {

        //              MVC

        private Form1Controller controller;
        private TrafficLightModel model;
        public Form1(TrafficLightModel model)
        {
            InitializeComponent();
            this.model = model;
            //  наблюдатели (view) регистрируют свою заинтересованность в модели
            this.model.Register(this);
            AttachController(MakeController());
        }
        //    Обновление вида
        //    Все наблюдатели (view), которые хотят зарегистрироваться у наблюдаемого объекта,
        //    должны реализовать интерфейс Observer

        public void UpdateState()
        {
            tb1.BackColor = Color.Black;
            tb2.BackColor = Color.Black;
            tb3.BackColor = Color.Black;
            if (model.SwitchedOn)

                //  Получение информации от о состоянии модели от самой модели
                switch (model.State)
                {
                    case TrafficLightState.Red:
                        tb1.BackColor = Color.Red;
                        break;
                    case TrafficLightState.Yellow:
                        tb2.BackColor = Color.Yellow;
                        break;
                    case TrafficLightState.Green:
                        tb3.BackColor = Color.Green;
                        break;
                }
        }

        //          Подключение контроллера к виду

        //  Этот метод связывает данный контроллер с видом

        public void AttachController(Form1Controller controller)
        {
            this.controller = controller;
        }

        protected Form1Controller MakeController()
        {
            //          Передача контроллеру ссылок на вид и модель

            return new Form1Controller(this, model);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double red = 0, yellow = 0, green = 0;
            try
            {
                red = double.Parse(tb4.Text)*1000;
                yellow = double.Parse(tb5.Text)*1000;
                green = double.Parse(tb6.Text)*1000;
            }
            catch { MessageBox.Show("Задайте корректные интервалы!"); }
            try
            {
                controller.SetIntervals(red, yellow, green);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

   

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                controller.SwithOn();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
             controller.SwitchOff();
        }
    }
}

