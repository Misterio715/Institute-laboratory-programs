using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba3
{
    class sem1 : Data
    {
        bool stip5;
        bool stip4;
        public sem1(string name, double mark, string retake) : base(name)
        {
            this.mark = mark;
            this.retake = retake;
            stip5 = (mark > 4) && (retake == "Нет");
            stip4 = (mark == 4) && (retake == "Нет");
        }

        public override int stipuha()
        {
            if (stip5 == true)
                return 3;
            else if (stip4 == true)
                return 2;
            else
                return 1;
        }

        public override string info()
        {
            return string.Format("{0,-15}Oценка:{1,1}    Пересдача: {2,1}",
           name, mark, retake);
        }
    }
}
