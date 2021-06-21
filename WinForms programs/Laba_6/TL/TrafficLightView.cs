using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WindowsApplication1
{   // ВИД_1 (View_1)
    //  Вид — это тот слой триады MVC, который отображает графическое или текстовое представление
    //  модели. Вид получает всю информацию о состоянии модели от самой модели.
    //  Единственной связью между моделью и пользовательским интерфейсом является связь
    //  через службу уведомления об изменении состояния системы.
    //  Если вид или контроллер заинтересованы в уведомлении об изменении состояния системы, 
    //  они регистрируются в модели через службу регистрации.
    //  Для формирования составного вида используется паттерн компоновщик  

    public partial class TrafficLightView : Form, IObserver
    {

        //              MVC

        private TrafficLightController controller;
        private TrafficLightModel model;
        public TrafficLightView(TrafficLightModel model)
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
            pnGreen.BackColor = Color.Black;
            pnYellow.BackColor = Color.Black;
            pnRed.BackColor = Color.Black;
            if (model.SwitchedOn)

                //  Получение информации от о состоянии модели от самой модели
                switch (model.State)
                {
                    case TrafficLightState.Red:
                        pnRed.BackColor = Color.Red;
                        break;
                    case TrafficLightState.Yellow:
                        pnYellow.BackColor = Color.Yellow;
                        break;
                    case TrafficLightState.Green:
                        pnGreen.BackColor = Color.Green;
                        break;
                }
        }

        //          Подключение контроллера к виду

        //  Этот метод связывает данный контроллер с видом

        public void AttachController(TrafficLightController controller)
        {
            this.controller = controller;
        }

        protected TrafficLightController MakeController()
        {
            //          Передача контроллеру ссылок на вид и модель

            return new TrafficLightController(this, model);
        }



        //          Обработка  и интерпретация входных данных с формы (передача их контроллеру)  

        private void btnSetInt_Click(object sender, EventArgs e)
        {
            double red = 0, yellow = 0, green = 0;
            try
            {
                red = double.Parse(tbRed.Text)*1000;
                yellow = double.Parse(tbYellow.Text)*1000;
                green = double.Parse(tbGreen.Text)*1000;
            }
            catch { MessageBox.Show("Задайте корректные интервалы!"); }
            try
            {
                controller.SetIntervals(red, yellow, green);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnOn_Click(object sender, EventArgs e)
        {
            try
            {
                controller.SwithOn();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnOff_Click(object sender, EventArgs e)
        {
            controller.SwitchOff();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }
    }
}

