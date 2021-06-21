using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsApplication1
{
    // Контроллер — это тот слой триады MVC, который интерпретирует данные, вводимые пользователем
    // В ответ на данные, вводимые пользователем, контроллер посылает команду модели
    // или виду для изменения состояния или выполнения какого-либо действия.

    //  Можно изменять реакцию вида на действия пользователя (например, деактивировать вид),
    //  используя иерархию классов контроллеров (паттерн стратегия)
    public class Form2Controller
    {
            private TrafficLightModel model;
            private Form2 view;
            public Form2Controller(Form2 view, TrafficLightModel model)
            {
                this.model = model;
                this.view = view;
            }
            public void SetIntervals(double red, double yellow, double green)
            {
                if (model.SwitchedOn) throw new Exception("Невозможно изменить интервалы в работающем светофоре!");
                Intervals i = new Intervals(red, yellow, green);
                model.SetIntervals(i);
            }
            public void SwithOn()
            {
                if (model.IntervalsEmpty) throw new Exception("Не заданы интервалы переключения!");
                model.SwitchedOn = true;
            }
            public void SwitchOff()
            {
                model.SwitchedOn = false;
            }
        }
    }

