using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Library5;

namespace Laba_5
{
    public abstract class AbstractFactory
    {
        public abstract AbstractProduct get_characteristics();
        public abstract Form CreateWindow(AbstractProduct abstractProperties);
    }
    public class FactoryHorde : AbstractFactory
    {
        public override Form CreateWindow(AbstractProduct abstractProperties)
        {
            return new Horde(abstractProperties);
        }

        public override AbstractProduct get_characteristics()
        {
            return new HordeInf();
        }
    }
    public class FactoryAlliance : AbstractFactory
    {
        public override Form CreateWindow(AbstractProduct abstractProperties)
        {
            return new Alliance(abstractProperties);
        }

        public override AbstractProduct get_characteristics()
        {
            return new AllianceInf();
        }
    }


    class Client
    {
        private Form form;
        private AbstractProduct properties;

        public Client(AbstractFactory factory)
        {
            properties = factory.get_characteristics();
            form = factory.CreateWindow(properties);
        }

        public void Run()
        {
            form.ShowDialog();
        }
    }
}
