using System;
using System.IO;
namespace WindowsApplication1
{
public class TrafficLightView2  : IObserver // ВИД_2 (View_2) - Консоль
 {
 /* это второй (альтернативный) вид (view) модели.
 Первый ее вид реализован в классе TrafficLightView
 второй вид не требует контроллера,
 так как он не принимает событий от пользователя.
 назначение второго вида: печать состояния в командную строку. */

 private  TrafficLightModel model;

 public TrafficLightView2 ( TrafficLightModel model)
 {   this.model = model;
     this.model.Register(this);
 }

 public void UpdateState()
 {
        if (model.SwitchedOn)

        //  Получение информации от о состоянии модели от самой модели
            switch (model.State)
            {
                case TrafficLightState.Red:
                    Console.Out.WriteLine("Текущее состояние: " + "Red");
                    break;
                case TrafficLightState.Yellow:
                    Console.Out.WriteLine("Текущее состояние: " + "Yellow");
                    break;
                case TrafficLightState.Green:
                    Console.Out.WriteLine("Текущее состояние: " + "Green");
                    break;
            }       
 }

 }/////////////////////// конец класса  - 2-й вид
}