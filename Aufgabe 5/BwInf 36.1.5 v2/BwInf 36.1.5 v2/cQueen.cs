using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BwInf_36._1._5_v2
{
    public class cQueen : cFigure
    {
        public cQueen(int x, int y)
        {
            this.maximumDirection = 8;
            this.minimumDirection = 0;
            this.maximumDistance = 7;
            this.minimumDistance = 0;
            this.x = x;
            this.y = y;
        }
    }
}
