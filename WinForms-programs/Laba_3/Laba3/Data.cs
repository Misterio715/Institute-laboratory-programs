using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba3
{
    abstract class Data
    {
        protected string name;
        protected string retake;
        protected double mark;
        abstract public int stipuha ();
        abstract public string info();
        public Data(string name) { this.name = name; }
    }
}
