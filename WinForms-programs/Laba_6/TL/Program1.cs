using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace  WindowsApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            TrafficLightModel model = new TrafficLightModel();

            TrafficLightView view1 = new TrafficLightView(model);
            ControlForm view3 = new ControlForm(model);
            Form1 view4 = new Form1(model);
            Form2 view5 = new Form2(model);

            TrafficLightView2 clw = new TrafficLightView2(model);

            view1.Show();
            view4.Show();
            view5.Show();

            Application.Run(view3);

            Console.Beep(300, 1500);
        }
    }
}
